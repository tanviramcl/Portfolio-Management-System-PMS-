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

public partial class UI_ReportViewer_SEC_ReportDailyReportViewer : System.Web.UI.Page
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

        string howlaDate = Convert.ToDateTime(Request.QueryString["howlaDate"]).ToString("dd-MMM-yyyy");
        
        DataTable dtOppeningBalDate = new DataTable();
        StringBuilder queryString = new StringBuilder();
        queryString.Append("SELECT TO_CHAR(MAX(VCH_DT),'DD-MON-YYYY') AS VCH_DT FROM FUND_TRANS_HB ");
        queryString.Append(" WHERE (VCH_DT < '" + howlaDate + "')");
        dtOppeningBalDate = commonGatewayObj.Select(queryString.ToString());

        string oppeningBalDate = dtOppeningBalDate.Rows[0][0].ToString();

        DataTable dtDailyReportSEC = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT     FUND.F_NAME, ROUND(NVL(OPENING_BALANCE.TOTAL_AMT / 1000000, 0), 2) AS OPENING_BALANCE_COST_PRICE,  ");
        sbMst.Append("ROUND(NVL(OPENING_BALANCE.MARKET_PRICE / 1000000, 0), 2) AS OPENING_BALANCE_MARKET_PRICE, ");
        sbMst.Append("ROUND(NVL(PURCHASE.COST_AMT / 1000000, 0), 2) AS PURCHASE_AMOUNT, ROUND(NVL(SELL.SELL_AMT / 1000000, 0), 2) AS SOLD_AMOUNT, ");
        sbMst.Append("ROUND(NVL(CLOSING_BALANCE.TOTAL_AMT / 1000000, 0), 2) AS CLOSING_BALANCE_COSTPRICE,  ");
        sbMst.Append("ROUND(NVL(CLOSING_BALANCE.MARKET_PRICE / 1000000, 0), 2) AS CLOSING_BALANCE_MARKETPRICE  ");
        sbMst.Append("FROM         (SELECT     F_CD, SUM(AMT_AFT_COM) AS SELL_AMT ");
        sbMst.Append("FROM          FUND_TRANS_HB ");
        sbMst.Append("WHERE      (VCH_DT = '"+howlaDate+"') AND (TRAN_TP = 'S')  ");
        sbMst.Append("GROUP BY F_CD) SELL RIGHT OUTER JOIN  ");
        sbMst.Append("(SELECT     F_CD, SUM(TCST_AFT_COM) AS TOTAL_AMT, SUM(ADC_RT * TOT_NOS) AS MARKET_PRICE ");
        sbMst.Append("FROM          PFOLIO_BK PFOLIO_BK_1 ");
        sbMst.Append("WHERE      (BAL_DT_CTRL = '"+oppeningBalDate+"') AND (F_CD NOT IN (1,3,5, 6, 18))  ");
        sbMst.Append("GROUP BY F_CD) OPENING_BALANCE RIGHT OUTER JOIN  ");
        sbMst.Append("(SELECT     F_CD, SUM(TCST_AFT_COM) AS TOTAL_AMT, SUM(ADC_RT * TOT_NOS) AS MARKET_PRICE ");
        sbMst.Append("FROM          PFOLIO_BK ");
        sbMst.Append("WHERE      (BAL_DT_CTRL = '" + howlaDate + "') AND (F_CD NOT IN (1,3,5, 6, 18))  ");
        sbMst.Append("GROUP BY F_CD) CLOSING_BALANCE INNER JOIN  ");
        sbMst.Append("(SELECT     F_CD, F_NAME ");
        sbMst.Append("FROM          FUND FUND_1 ");
        sbMst.Append("WHERE      (F_CD NOT IN (1,3,5, 6, 18))) FUND ON CLOSING_BALANCE.F_CD = FUND.F_CD ON OPENING_BALANCE.F_CD = FUND.F_CD ON  ");
        sbMst.Append("SELL.F_CD = CLOSING_BALANCE.F_CD LEFT OUTER JOIN ");
        sbMst.Append("(SELECT     F_CD, SUM(AMT_AFT_COM) AS COST_AMT ");
        sbMst.Append("FROM          FUND_TRANS_HB FUND_TRANS_HB_1  ");
        sbMst.Append("WHERE      (VCH_DT = '" + howlaDate + "') AND (TRAN_TP = 'C')  ");
        sbMst.Append("GROUP BY F_CD) PURCHASE ON CLOSING_BALANCE.F_CD = PURCHASE.F_CD ");
        sbMst.Append("ORDER BY FUND.F_CD  ");
        
        sbMst.Append(sbfilter.ToString());

        dtDailyReportSEC = commonGatewayObj.Select(sbMst.ToString());
        dtDailyReportSEC.TableName = "DailyReportSEC";


        if (dtDailyReportSEC.Rows.Count > 0)
        {
            //dtDailyReportSEC.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmDailyReportSEC.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/crtmDailyReportSEC.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtDailyReportSEC);
            CRV_SEC_Daily.ReportSource = rdoc;
            CRV_SEC_Daily.DisplayToolbar = true;
            CRV_SEC_Daily.HasExportButton = true;
            CRV_SEC_Daily.HasPrintButton = true;
            rdoc.SetParameterValue("prmHowlaDate", howlaDate);
            //rdoc.SetParameterValue("prmtotalAmountInWords", totalAmountInWords);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_SEC_Daily.Dispose();
        CRV_SEC_Daily = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
