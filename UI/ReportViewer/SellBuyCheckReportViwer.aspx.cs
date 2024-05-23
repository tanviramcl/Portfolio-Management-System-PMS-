using CrystalDecisions.CrystalReports.Engine;
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


        string Fromdate = "";
        string Todate = "";
        string fundCode = "";
        string companycode = "";
        string transtype = "";
        string sector = "";
        string SectorName = "";


        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            //Fromdate = (string)Session["Fromdate"];
            //Todate = (string)Session["Todate"];
            //fundCode = (string)Session["fundCodes"];
            //companycode = (string)Session["companycode"];
            SectorName = (string)Session["sectorName"];

            Fromdate = Convert.ToString(Request.QueryString["p1date"]).Trim();
             Todate = Convert.ToString(Request.QueryString["p2date"]).Trim();
            fundCode = Convert.ToString(Request.QueryString["fundcode"]).Trim();
            companycode = Convert.ToString(Request.QueryString["companycode"]).Trim();
            transtype = Convert.ToString(Request.QueryString["transtype"]).Trim();
            sector= Convert.ToString(Request.QueryString["sector"]).Trim();

        }

        if (fundCode == "0" && companycode == "0" && transtype == "0" && sector == "0")
        {
            sbMst.Append("select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')',t.TRAN_TP , decode ( t.TRAN_TP, 'C', 'Cost','S','Sell','B','Bonus','R','Right','I','IPO') tran_type,");
            sbMst.Append(" t.VCH_NO, t.NO_SHARE, t.RATE ,t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX ,decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f");
            sbMst.Append(" where vch_dt between '" + Fromdate + "' and '" + Todate + "' and c.comp_cd=t.comp_cd and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SellBuyCheckReport";
            //dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SellBuyCheckReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_SellBuyCheckReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SellBuyCheckReport.ReportSource = rdoc;
                CR_SellBuyCheckReport.DisplayToolbar = true;
                CR_SellBuyCheckReport.HasExportButton = true;
                CR_SellBuyCheckReport.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }

        }
        else if (fundCode != "0" && companycode == "0" && transtype == "0" && sector == "0")
        {
            sbMst.Append("select t.VCH_DT, t.F_CD  , f.f_name fund_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')',t.TRAN_TP , decode ( t.TRAN_TP, 'C', 'Cost','S','Sell','B','Bonus','R','Right','I','IPO',' ') tran_type,");
            sbMst.Append(" t.VCH_NO, t.NO_SHARE, t.RATE ,t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE,2) as avg_rate,t.STOCK_EX ,");
            sbMst.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE','A','ALL',' ') stock_name, t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt between '" + Fromdate + "' and '" + Todate + "' and c.comp_cd=t.comp_cd and t.f_cd='" + fundCode + "' and t.f_cd=f.f_cd order by t.f_cd,t.VCH_DT");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SellBuyCheckReportfundwise";
            // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SellBuyCheckReportfundwise.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/CR_SellBuyCheckReportfundwise.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SellBuyCheckReport.ReportSource = rdoc;
                CR_SellBuyCheckReport.DisplayToolbar = true;
                CR_SellBuyCheckReport.HasExportButton = true;
                CR_SellBuyCheckReport.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }

        }
        else if (fundCode == "0" && companycode != "0" && transtype == "0" && sector == "0")
        {
            sbMst.Append("select t.VCH_DT, t.F_CD  ,  f.f_name fund_name, t.COMP_CD , c.comp_nm, c.comp_nm  || '('|| t.COMP_CD|| ')',t.TRAN_TP , decode ( t.TRAN_TP, 'C', 'Cost','S','Sale','B','Bonus','I','IPO','R','Right','D','Split',' ') tran_type,");
            sbMst.Append(" t.VCH_NO, t.NO_SHARE, t.RATE ,t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE,2) as avg_rate,t.STOCK_EX ,decode(t.STOCK_EX,'D','DSE','C','CSE',' ') stock_name,t.OP_NAME ");
            sbMst.Append(" from fund_trans_hb t,comp c, fund f where vch_dt between '" + Fromdate + "' and '" + Todate + "' and c.comp_cd=t.comp_cd and c.comp_cd='" + companycode + "' and t.NO_SHARE>=1 and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "CR_SellBuyCheckReportcompanywise";
            //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SellBuyCheckReportCompanywise2.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/CR_SellBuyCheckReportcompanywise.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SellBuyCheckReport.ReportSource = rdoc;
                CR_SellBuyCheckReport.DisplayToolbar = true;
                CR_SellBuyCheckReport.HasExportButton = true;
                CR_SellBuyCheckReport.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (fundCode != "0" && companycode == "0" && transtype != "0" && sector == "0")
        {
            sbMst.Append("SELECT  f.F_CD, f.comp_cd, c.comp_nm, decode(f.f_cd, 1,'ICB Asset Management Company Ltd.',2, 'ICB AMCL Unit Fund',3, 'ICB AMCL First Mutual Fund', 4,'ICB AMCL Pension Holders'' Unit Fund',5,'ICB AMCL Islamic Mutual Fund',6, 'ICB AMCL First NRB Mutual Fund') fund_name,");
            sbMst.Append(" SUM(AMT_AFT_COM) as purchase, sum(no_share) as No_Of_Share   FROM FUND_TRANS_HB f, comp c WHERE F.TRAN_TP= '" + transtype + "' AND VCH_DT BETWEEN '" + Fromdate + "' AND '" + Todate + "' ");
            sbMst.Append(" and f_cd='" + fundCode + "' and c.comp_cd=f.comp_cd GROUP BY f.F_CD,f.comp_cd ,c.comp_nm");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SellBuyCheckReportcompanywiseALL";
            //   dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SellBuyCheckReportcompanywiseALL.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/CR_SellBuyCheckReportcompanywiseALL.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SellBuyCheckReport.ReportSource = rdoc;
                CR_SellBuyCheckReport.DisplayToolbar = true;
                CR_SellBuyCheckReport.HasExportButton = true;
                CR_SellBuyCheckReport.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }

        }
        else if (fundCode == "0" && companycode == "0" && transtype != "0" && sector != "0")
        {
            sbMst.Append(" select b.comp_cd,c.comp_nm,b.f_cd,b.f_name,totalnoshare,totatammount,avg_rate from (select a.*,f.f_name from ( select c.*,ROUND(totatammount/totalnoshare,2) as avg_rate from ");
            sbMst.Append("(select comp_cd,f_cd,sum(no_share) as totalnoshare,sum(AMT_AFT_COM) as totatammount from ( select a.* from fund_trans_hb  a where vch_dt  ");
            sbMst.Append(" between '"+Fromdate+"' and '"+Todate +"' and  tran_tp='"+transtype+"') group by comp_cd,f_Cd) c  where comp_cd in (select comp_Cd from comp where SECT_MAJ_CD="+sector+")   ");
            sbMst.Append(" order by comp_cd,f_cd) a inner join fund f on a.f_cd=f.f_cd) b inner join comp c on b.comp_cd=c.comp_cd order by comp_cd,f_cd   ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SellBuyCheckReportSectorWiseALL";
          //  dtReprtSource.WriteXmlSchema(@"D:\Development\PFMS_LIVE\PMCS_SERVER\amclinvanalysisandpfms\UI\ReportViewer\Report\CR_SellBuyCheckReportSectorWiseeALL.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/CR_SellBuyCheckReportcompanywiseALLSectorWise.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SellBuyCheckReport.ReportSource = rdoc;
                CR_SellBuyCheckReport.DisplayToolbar = true;
                CR_SellBuyCheckReport.HasExportButton = true;
                CR_SellBuyCheckReport.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("SectorName", SectorName);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

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