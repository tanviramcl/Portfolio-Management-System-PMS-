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
using System.Text;
using CrystalDecisions.CrystalReports.Engine;

public partial class UI_ReportViewer_SalePurchaseReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1Obj = new Pf1s1DAO();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string fromDate = "";
        string toDate = "";
        string fundCode = "";
       
        DataTable dtIntimationReport = new DataTable();

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fromDate = (string)Session["fromDate"];
            toDate = (string)Session["toDate"];
            fundCode = (string)Session["fundCode"];
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append(" SELECT DISTINCT     COMP.COMP_NM,COMP.TRADE_METH, NVL(DERIVEDTBL_BUY.NO_SHARE_BUY,0) as NO_SHARE_BUY, NVL(DERIVEDTBL_BUY.COSTRATE,0) as COSTRATE, NVL(DERIVEDTBL_SALE.NO_SHARE_SALE,0) as NO_SHARE_SALE,   NVL(DERIVEDTBL_SALE.SALETRATE,0) as SALETRATE ");
        sbMst.Append(" FROM         FUND_TRANS_HB INNER JOIN ");
        sbMst.Append(" COMP ON FUND_TRANS_HB.COMP_CD = COMP.COMP_CD LEFT OUTER JOIN ");
        sbMst.Append(" (SELECT     COMP_CD, SUM(NO_SHARE) AS NO_SHARE_BUY, ROUND(SUM(AMT_AFT_COM) / SUM(NO_SHARE), 2) AS COSTRATE ");
        sbMst.Append(" FROM          FUND_TRANS_HB FUND_TRANS_HB_2 ");
        sbMst.Append(" WHERE      (VCH_DT BETWEEN '"+fromDate.ToString()+"' AND '"+toDate.ToString()+"') AND (F_CD = "+fundCode.ToString()+") AND (TRAN_TP = 'C') ");
        sbMst.Append(" GROUP BY COMP_CD) DERIVEDTBL_BUY ON FUND_TRANS_HB.COMP_CD = DERIVEDTBL_BUY.COMP_CD LEFT OUTER JOIN ");
        sbMst.Append(" (SELECT     COMP_CD, SUM(NO_SHARE) AS NO_SHARE_SALE, SUM(AMT_AFT_COM) AS SALEAMOUNT, ROUND(SUM(AMT_AFT_COM) ");
        sbMst.Append("  / SUM(NO_SHARE), 2) AS SALETRATE ");
        sbMst.Append(" FROM          FUND_TRANS_HB FUND_TRANS_HB_1 ");
        sbMst.Append(" WHERE      (VCH_DT BETWEEN '" + fromDate.ToString() + "' AND '" + toDate.ToString() + "') AND (TRAN_TP = 'S') AND (F_CD = " + fundCode.ToString() + ") ");
        sbMst.Append(" GROUP BY COMP_CD) DERIVEDTBL_SALE ON FUND_TRANS_HB.COMP_CD = DERIVEDTBL_SALE.COMP_CD ");
        sbMst.Append(" WHERE     (FUND_TRANS_HB.VCH_DT BETWEEN '" + fromDate.ToString() + "' AND '" + toDate.ToString() + "') AND (FUND_TRANS_HB.F_CD IN (" + fundCode.ToString() + ")) AND ");
        sbMst.Append(" (FUND_TRANS_HB.TRAN_TP IN ('C', 'S')) ");
        sbMst.Append(" ORDER BY COMP.COMP_NM ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        DataTable dtFundName = new DataTable();
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.Append(" Select f_name from fund where f_cd="+fundCode.ToString()+" ");
        dtFundName = commonGatewayObj.Select(strBuilder.ToString());
        string fundName = dtFundName.Rows[0]["f_name"].ToString();
        
        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "SalePurchaseReport";
          //  dtReprtSource.WriteXmlSchema(@"D:\Development\pfms_01_02_2021\amclinvanalysisandpfms\UI\ReportViewer\Report\crtSalePurchaseReport_final.xsd");
            
            string Path = "";
            Path = Server.MapPath("Report/crtSalePurchaseReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_SalePurchase.ReportSource = rdoc;
            rdoc.SetParameterValue("prmFromDate", fromDate);
            rdoc.SetParameterValue("prmToDate", toDate);
            rdoc.SetParameterValue("prmFundName", fundName);
            CRV_SalePurchase.DisplayToolbar = true;
            CRV_SalePurchase.HasExportButton = true;
            CRV_SalePurchase.HasPrintButton = true;
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_SalePurchase.Dispose();
        CRV_SalePurchase = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
