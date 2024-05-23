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

public partial class UI_ReportViewer_PortfolioSummaryReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1Obj = new Pf1s1DAO();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string balDate = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            balDate = (string)Session["balDate"];
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT     FUND.F_NAME, PFOLIO_BK.SECT_MAJ_NM, TRUNC(SUM(PFOLIO_BK.TOT_NOS),0) AS NO_OF_SHARE, ");
        sbMst.Append("SUM(PFOLIO_BK.TCST_AFT_COM) AS COST_PRICE, SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT) AS MARKET_PRICE, ");
        sbMst.Append("SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT) - SUM(PFOLIO_BK.TCST_AFT_COM) AS APPRE_EROSION, ");
        sbMst.Append("ROUND((SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT) - SUM(PFOLIO_BK.TCST_AFT_COM)) ");
        sbMst.Append("* 100 / SUM(PFOLIO_BK.TCST_AFT_COM), 2) AS PERCENT_APPRE_EROSION ");
        sbMst.Append("FROM         FUND INNER JOIN ");
        sbMst.Append("PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ");
        sbMst.Append("WHERE     (FUND.F_CD ="+ fundCode + ") AND (PFOLIO_BK.BAL_DT_CTRL = '"+ balDate +"') ");
        sbMst.Append("GROUP BY PFOLIO_BK.SECT_MAJ_NM, FUND.F_NAME,PFOLIO_BK.SECT_MAJ_CD ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_CD ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        DataTable dtNonlistedSecrities = new DataTable();
        sbMst = new StringBuilder();
        sbMst.Append("SELECT      'NON LISTED SECURITIES' AS SECT_MAJ_NM,INV_AMOUNT AS COST_PRICE, INV_AMOUNT AS MARKET_PRICE, 0 AS APPRE_EROSION, 0 AS PERCENT_APPRE_EROSION ");
        sbMst.Append("FROM         NON_LISTED_SECURITIES ");
        sbMst.Append("WHERE     (F_CD = "+ fundCode +") AND (INV_DATE = ");
        sbMst.Append(" (SELECT     MAX(INV_DATE) AS EXPR1 ");
        sbMst.Append("FROM          NON_LISTED_SECURITIES NON_LISTED_SECURITIES_1 ");
        sbMst.Append("WHERE      (F_CD = " + fundCode + ") AND (INV_DATE <= '" + balDate + "'))) ");
        dtNonlistedSecrities = commonGatewayObj.Select(sbMst.ToString());

        if (dtNonlistedSecrities.Rows.Count > 0)
        {
            DataRow drReport;
            for (int loop = 0; loop < dtNonlistedSecrities.Rows.Count; loop++)
            {
                drReport = dtReprtSource.NewRow();
                drReport["SECT_MAJ_NM"] = dtNonlistedSecrities.Rows[loop]["SECT_MAJ_NM"].ToString();
                drReport["COST_PRICE"] = dtNonlistedSecrities.Rows[loop]["COST_PRICE"].ToString();
                drReport["MARKET_PRICE"] = dtNonlistedSecrities.Rows[loop]["MARKET_PRICE"].ToString();
                drReport["APPRE_EROSION"] = dtNonlistedSecrities.Rows[loop]["APPRE_EROSION"].ToString();
                drReport["PERCENT_APPRE_EROSION"] = dtNonlistedSecrities.Rows[loop]["PERCENT_APPRE_EROSION"].ToString();
                dtReprtSource.Rows.Add(drReport);
            }
        }
        if (dtReprtSource.Rows.Count > 0)
        {
            Decimal totalInvest = 0;
            for (int loop = 0; loop < dtReprtSource.Rows.Count; loop++)
            {
                totalInvest = totalInvest + Convert.ToDecimal(dtReprtSource.Rows[loop]["COST_PRICE"]); 
            }
            dtReprtSource.TableName = "PortfolioSummaryReport";
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtPortfolioSummaryReport.xsd");
            ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtPortfolioSummaryInPieChartReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_PortfolioSummary.ReportSource = rdoc;
            CRV_PortfolioSummary.DisplayToolbar = true;
            CRV_PortfolioSummary.HasExportButton = true;
            CRV_PortfolioSummary.HasPrintButton = true;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("prmTotalInvest", totalInvest);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_PortfolioSummary.Dispose();
        CRV_PortfolioSummary = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
