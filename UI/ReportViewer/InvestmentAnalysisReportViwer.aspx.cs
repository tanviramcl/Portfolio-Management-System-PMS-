using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_StockDeclarationBeforePostedReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }

        
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");


    
        string fundCode = "";
        string companycode = "";
        string formate_type = "";
        string fundName = "";



        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = Convert.ToString(Request.QueryString["fundcode"]).Trim();
            companycode = Convert.ToString(Request.QueryString["companycode"]).Trim();
            formate_type = Convert.ToString(Request.QueryString["formate_type"]).Trim();
            fundName = (string)Session["FundName"];

        }

       
        if (fundCode != "0" && companycode != "0" )
        {
            sbMst.Append("select t.VCH_DT, t.F_CD  ,  f.f_name fund_name, t.COMP_CD , c.comp_nm, c.comp_nm  || '('|| t.COMP_CD|| ')',t.TRAN_TP , decode ( t.TRAN_TP, 'C', 'Buy','S','Sale','B','Bonus','I','IPO','R','Right','D','Split',' ') tran_type,");
            sbMst.Append(" t.VCH_NO, t.NO_SHARE, t.RATE ,t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE,2) as avg_rate,t.STOCK_EX ,decode(t.STOCK_EX,'D','DSE','C','CSE',' ') stock_name,t.OP_NAME ");
            sbMst.Append(" from fund_trans_hb t,comp c, fund f where  c.comp_cd=t.comp_cd and c.comp_cd='" + companycode + "' and  t.f_cd="+fundCode+" and t.NO_SHARE>=1 and f.f_cd=t.f_cd order by t.f_cd,t.VCH_DT asc");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

            DataColumn newColAMT_AFT_COM = new DataColumn("FINAL_AMT_AFT_COM", typeof(string));
            newColAMT_AFT_COM.AllowDBNull = true;
            dtReprtSource.Columns.Add(newColAMT_AFT_COM);

            DataColumn newCol_tot_nos = new DataColumn("FINAL_TOTAL_NOS", typeof(string));
            newCol_tot_nos.AllowDBNull = true;
            dtReprtSource.Columns.Add(newCol_tot_nos);


            Double FINAL_AMT_AFT_COM = 0;

            int Final_total_nos = 0;


            if (dtReprtSource.Rows.Count > 0)
            {
                Double AMT_AFT_COM ;
                int total_nos = 0;

                for (int i = 0; i < dtReprtSource.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtReprtSource.Rows[i]["AMT_AFT_COM"].ToString()))
                    {
                        AMT_AFT_COM = Convert.ToDouble(dtReprtSource.Rows[i]["AMT_AFT_COM"].ToString());

                    }

                    else
                    {
                        AMT_AFT_COM = 0;
                    }


                    if (!string.IsNullOrEmpty(dtReprtSource.Rows[i]["NO_SHARE"].ToString()))
                    {

                        total_nos = Convert.ToInt32(dtReprtSource.Rows[i]["NO_SHARE"].ToString().Replace(".", string.Empty));

                    }

                    else
                    {
                        total_nos = 0;
                    }



                    if (dtReprtSource.Rows[i]["tran_tp"].ToString() == "S")
                    {

                        double mcost_rt_acm, m_amt_acm, m_cost_acm;


                        mcost_rt_acm = Math.Round(FINAL_AMT_AFT_COM / Final_total_nos, 2);

                        m_amt_acm = total_nos * mcost_rt_acm;
                       // m_cost_acm = FINAL_AMT_AFT_COM - m_amt_acm;


                        FINAL_AMT_AFT_COM = FINAL_AMT_AFT_COM - m_amt_acm;
                        Final_total_nos = Final_total_nos - total_nos;

                    }
                    else
                    {
                        FINAL_AMT_AFT_COM = FINAL_AMT_AFT_COM + AMT_AFT_COM;
                        Final_total_nos = Final_total_nos + total_nos;
                    }


                    dtReprtSource.Rows[i]["FINAL_AMT_AFT_COM"] = FINAL_AMT_AFT_COM;
                    dtReprtSource.Rows[i]["FINAL_TOTAL_NOS"] = Final_total_nos;





                }
            }





               // dtReprtSource.TableName = "CR_SellBuyCheckReportcompanywise";
           //dtReprtSource.WriteXmlSchema(@"D:\Development\PFMS_Local\RISK Managment\amclinvanalysisandpfms\UI\ReportViewer\Report\CR_Investment_analisis.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/CR_INVESTMENTANALYSIS.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);

                CR_SellBuyCheckReport.ReportSource = rdoc;
                CR_SellBuyCheckReport.DisplayToolbar = true;
                CR_SellBuyCheckReport.HasExportButton = true;
                CR_SellBuyCheckReport.HasPrintButton = true;
                rdoc.SetParameterValue("fundName", fundName);

                rdoc = ReportFactory.GetReport(rdoc.GetType());


                //ExportFormatType formatType = ExportFormatType.NoFormat;
                //switch (formate_type)
                //{
                //    case "Word":
                //        formatType = ExportFormatType.WordForWindows;
                //        break;
                //    case "PDF":
                //        formatType = ExportFormatType.PortableDocFormat;
                //        break;
                //    case "Excel":
                //        formatType = ExportFormatType.ExcelWorkbook;
                //        break;
                //    case "CSV":
                //        formatType = ExportFormatType.CharacterSeparatedValues;
                //        break;
                //}

                //// rdoc.ExportToHttpResponse(formatType, Response, true, "SaleSMS");
                ////rdoc.ExportToDisk(ExportFormatType.ExcelRecord, "report.xls");

                ////rdoc.ExportToDisk(ExportFormatType.ExcelRecord, "D:\\Data\\IAMCL\\Tax-" + sale_number_from + "-" + fund_code + "");

                //rdoc.ExportToHttpResponse(formatType, Response, true, "test_" + fundCode + "_" + DateTime.Now);

                //Response.End();


            }
            else
            {
                Response.Write("No Data Found");
            }
        
        

        }
        else
        {
            Response.Write("No Data Found");
        }


    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_SellBuyCheckReport.Dispose();
        CR_SellBuyCheckReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }

}