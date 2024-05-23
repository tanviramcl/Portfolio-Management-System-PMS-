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

public partial class UI_ReportViewer_QuarterlyReportToSECReportViewer : System.Web.UI.Page
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
        string qStartDate = "";
        string qEndDate = "";
        string reportType = "";
        qStartDate = (string)Session["quarterStartDate"];
        qEndDate = (string)Session["quarterEndDate"];
        reportType = (string)Session["reportType"];

        DataTable dtOppeningBalDate = new DataTable();
        StringBuilder queryString = new StringBuilder();
        queryString.Append("SELECT TO_CHAR(MAX(VCH_DT),'DD-MON-YYYY') AS VCH_DT FROM FUND_TRANS_HB ");
        queryString.Append(" WHERE (VCH_DT < '" + qStartDate + "')");
        dtOppeningBalDate = commonGatewayObj.Select(queryString.ToString());

        string oppeningBalDate = dtOppeningBalDate.Rows[0][0].ToString();
        string openingMonth = Convert.ToDateTime(oppeningBalDate).ToString("MMMM");
        string quarter = Convert.ToDateTime(qStartDate).ToString("MMMM") + "-" + Convert.ToDateTime(qEndDate).ToString("MMMM");
        string quarterShort = Convert.ToDateTime(qStartDate).ToString("MMM") + "-" + Convert.ToDateTime(qEndDate).ToString("MMM");
        int qStartYear = Convert.ToDateTime(qStartDate).Year;
        int qEndYear = Convert.ToDateTime(qEndDate).Year;
        string year = "";
        if (qStartYear == qEndYear)
        {
            year = qStartYear.ToString();
        }
        else if (qStartYear < qEndYear)
        {
            year = qStartYear.ToString() + "-" + qEndYear.ToString();
        }
        string fundFilter = " 1=1 ";
        if (reportType.ToString().ToUpper() == "1") 
        {
            fundFilter = " (F_CD NOT IN (3,6,7,5,18)) ";
        }
        else if (reportType.ToString().ToUpper() == "2") 
        {
            fundFilter = " (F_CD NOT IN (1,2,3,6,5,7,17,18,31,32)) ";
        }
        else if (reportType.ToString().ToUpper() == "3")
        {
            fundFilter = "  (F_CD  IN (31)) ";
        }
        else if (reportType.ToString().ToUpper() == "4")
        {
            fundFilter = "  (F_CD  IN (17,32)) ";
        }
        else if (reportType.ToString().ToUpper() == "5")
        {
            fundFilter = "  (F_CD  IN (2)) ";
        }
        DataTable dtQuarterlyReportSEC = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        //StringBuilder sbfilter = new StringBuilder();
        //sbfilter.Append(" ");
        sbMst.Append("SELECT     FUND.F_NAME, ROUND(NVL(OPENING_BALANCE.TOTAL_AMT / 1000000, 0), 2) AS OPENING_BALANCE_CI,  ");
        sbMst.Append("ROUND(NVL(IPO_RIGHTS_INV.INV_AMT_IPO_RIGHTS / 1000000, 0), 2) AS INV_AMT_IPO_RIGHTS, ROUND(NVL(PURCHASE.COST_AMT / 1000000, 0), 2) ");
        sbMst.Append("AS PURCHASE_AMOUNT, ROUND(NVL(IPO_RIGHTS_INV.INV_AMT_IPO_RIGHTS / 1000000, 0), 2) + ROUND(NVL(PURCHASE.COST_AMT / 1000000, 0), 2) AS TOTAL_INV_Q,  ");
        sbMst.Append("ROUND(NVL(SELL.SELL_AMT / 1000000, 0), 2) AS SOLD_AMOUNT, ROUND(NVL(SELL.CI_OF_SALE / 1000000, 0), 2) AS CI_OF_SALE,  ");
        sbMst.Append("ROUND(NVL(CLOSING_BALANCE.TOTAL_AMT / 1000000, 0), 2) AS CLOSING_BALANCE_COSTPRICE,   ");
        sbMst.Append("ROUND(NVL(CLOSING_BALANCE.MARKET_PRICE / 1000000, 0), 2) AS CLOSING_BALANCE_MARKETPRICE ");
        sbMst.Append("FROM         (SELECT     F_CD, SUM(TCST_AFT_COM) AS TOTAL_AMT ");
        sbMst.Append(" FROM          PFOLIO_BK PFOLIO_BK_1  ");
        sbMst.Append("WHERE      (BAL_DT_CTRL = '" + oppeningBalDate + "') AND " + fundFilter + "  ");
        sbMst.Append("GROUP BY F_CD) OPENING_BALANCE RIGHT OUTER JOIN ");
        sbMst.Append("(SELECT     F_CD, SUM(AMT_AFT_COM) AS INV_AMT_IPO_RIGHTS ");
        sbMst.Append("FROM          FUND_TRANS_HB FUND_TRANS_HB_2  ");
        sbMst.Append("WHERE      (VCH_DT BETWEEN '" + qStartDate + "' AND '" + qEndDate + "') AND (TRAN_TP IN ('I', 'R'))  ");
        sbMst.Append("GROUP BY F_CD) IPO_RIGHTS_INV RIGHT OUTER JOIN ");
        sbMst.Append("(SELECT     F_CD, SUM(TCST_AFT_COM) AS TOTAL_AMT, SUM(ADC_RT * TOT_NOS) AS MARKET_PRICE ");
        sbMst.Append(" FROM          PFOLIO_BK  ");
        sbMst.Append(" WHERE      (BAL_DT_CTRL = '" + qEndDate + "')  AND " + fundFilter + "  ");
        sbMst.Append("GROUP BY F_CD) CLOSING_BALANCE INNER JOIN ");
        sbMst.Append("(SELECT     F_CD, F_NAME ");
        sbMst.Append(" FROM          FUND FUND_1  ");
        sbMst.Append("WHERE      " + fundFilter + " ) FUND ON CLOSING_BALANCE.F_CD = FUND.F_CD ON IPO_RIGHTS_INV.F_CD = FUND.F_CD ON  ");
        sbMst.Append("OPENING_BALANCE.F_CD = FUND.F_CD LEFT OUTER JOIN ");
        sbMst.Append("(SELECT     F_CD, SUM(AMT_AFT_COM) AS SELL_AMT, SUM(NO_SHARE * CRT_AFT_COM) AS CI_OF_SALE  ");
        sbMst.Append(" FROM          FUND_TRANS_HB  ");
        sbMst.Append("WHERE      (VCH_DT BETWEEN '" + qStartDate + "' AND '" + qEndDate + "') AND (TRAN_TP = 'S') ");
        sbMst.Append("GROUP BY F_CD) SELL ON CLOSING_BALANCE.F_CD = SELL.F_CD LEFT OUTER JOIN  ");
        sbMst.Append("(SELECT     F_CD, SUM(AMT_AFT_COM) AS COST_AMT ");
        sbMst.Append(" FROM          FUND_TRANS_HB FUND_TRANS_HB_1  ");
        sbMst.Append(" WHERE      (VCH_DT BETWEEN '" + qStartDate + "' AND '" + qEndDate + "') AND (TRAN_TP = 'C')  ");
        sbMst.Append("GROUP BY F_CD) PURCHASE ON CLOSING_BALANCE.F_CD = PURCHASE.F_CD ");
        sbMst.Append("ORDER BY FUND.F_CD ");

        //sbMst.Append(sbfilter.ToString());

        dtQuarterlyReportSEC = commonGatewayObj.Select(sbMst.ToString());
        dtQuarterlyReportSEC.TableName = "QuarterlyReport";

        if (dtQuarterlyReportSEC.Rows.Count > 0)
        {
            //dtQuarterlyReportSEC.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmQuarterlyReportSEC.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/crtmQuarterlyReportSEC.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtQuarterlyReportSEC);
            CRV_Quarterly.ReportSource = rdoc;
            CRV_Quarterly.DisplayToolbar = true;
            CRV_Quarterly.HasExportButton = true;
            CRV_Quarterly.HasPrintButton = true;
            rdoc.SetParameterValue("prmOpeningMonth", openingMonth);
            rdoc.SetParameterValue("prmQuarter", quarter);
            rdoc.SetParameterValue("prmQuarterShort", quarterShort);
            rdoc.SetParameterValue("prmYear", year);
            rdoc.SetParameterValue("prmQendDate", qEndDate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_Quarterly.Dispose();
        CRV_Quarterly = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
