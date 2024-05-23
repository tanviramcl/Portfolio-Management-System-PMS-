using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;

public partial class UI_ReportViewer_SalePurchaseViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        string Fromdate = "";
        string Todate = "";

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            Fromdate = Convert.ToString(Request.QueryString["howlaDateFrom"]).Trim();
            Todate = Convert.ToString(Request.QueryString["howlaDateTo"]).Trim();
        }

        if (Fromdate != "" && Todate != "0")
        {
            sbMst.Append("select a.*,decode(NO_SHARE,0,100,NO_SHARE) FINAL_SAHRE from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')',t.TRAN_TP , decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type,");
            //sbMst.Append(" t.VCH_NO, ROUND((t.NO_SHARE+t.NO_SHARE*0.10)/100)*100 as NO_SHARE, t.RATE ,t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX ,decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f");
            sbMst.Append(" t.VCH_NO,t.NO_SHARE as MainSahre, ROUND((t.NO_SHARE+t.NO_SHARE*0.10)/100)*100 as NO_SHARE, t.RATE ,t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX ,decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f");
            sbMst.Append(" where vch_dt between '" + Fromdate + "' and '" + Todate + "' and t.TRAN_TP in ('C','S')  and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd,c.comp_nm, tran_tp,t.VCH_DT)  a ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SellBuyCheckReport";
         //   dtReprtSource.WriteXmlSchema(@"D:\Development\12-10-21\amclinvanalysisandpfms\UI\ReportViewer\Report\CR_SellBuyCheckReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/SalePurchase_Committe.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CRV_SalePurchase_Committe.ReportSource = rdoc;
                CRV_SalePurchase_Committe.DisplayToolbar = true;
                CRV_SalePurchase_Committe.HasExportButton = true;
                CRV_SalePurchase_Committe.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }

        }
        //DataTable dtReprtSource = new DataTable();
        //StringBuilder sbMst = new StringBuilder();
        //StringBuilder sbfilter = new StringBuilder();
        //sbfilter.Append(" ");
        //sbMst.Append(" SELECT sb.VCH_DT,sb.COMP_CD,c.comp_nm,c.comp_nm  || '('|| c.COMP_CD|| ')' as CompanyName, decode(sb.tran_tp,'C','Purchase','S','SELL') as  TRANSACTION,sb.AMCL,sb.AMCUF,sb.AMCPF,sb.AIMF,sb.BDF,sb.ANRB2  ");
        //sbMst.Append(" FROM SELL_BUY_DAILY sb INNER JOIN comp c ON sb.COMP_CD = c.COMP_CD WHERE sb.VCH_DT BETWEEN '1-Aug-2019' and '1-Aug-2019'  ");


        //sbMst.Append(sbfilter.ToString());
        //dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        //dtReprtSource.TableName = "Sale-PurchaseCommitteeReport";

        //if (dtReprtSource.Rows.Count > 0)
        //{
        //   // dtReprtSource.WriteXmlSchema(@"D:\Development\PFMS_FINAL\amclinvanalysisandpfms\UI\ReportViewer\Report\Sale-PurchaseCommitteeReport.xsd");

        //    //ReportDocument rdoc = new ReportDocument();
        //    string Path = Server.MapPath("Report/SalePurchaseDaily.rpt");
        //    rdoc.Load(Path);
        //    rdoc.SetDataSource(dtReprtSource);
        //    CRV_SalePurchase_Committe.ReportSource = rdoc;
        //    rdoc = ReportFactory.GetReport(rdoc.GetType());
        //    CRV_SalePurchase_Committe.DisplayToolbar = true;
        //    CRV_SalePurchase_Committe.HasExportButton = true;
        //    CRV_SalePurchase_Committe.HasPrintButton = true;
        //}
        //else
        //{
        //    Response.Write("No Data Found");
        //}




    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_SalePurchase_Committe.Dispose();
        CRV_SalePurchase_Committe = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
 

}
