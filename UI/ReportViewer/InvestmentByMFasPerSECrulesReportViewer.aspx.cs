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

public partial class UI_ReportViewer_InvestmentByMFasPerSECrulesReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1Obj = new Pf1s1DAO();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string balDate = "";
        string assetValue = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            balDate = (string)Session["balDate"];
            assetValue = (string)Session["assetValue"];
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append(" SELECT     FUND.F_NAME, PFOLIO_BK.SECT_MAJ_NM, COUNT(PFOLIO_BK.COMP_CD) AS NO_COMPANY,   ");
        sbMst.Append(" ROUND(SUM(PFOLIO_BK.TCST_AFT_COM), 0) AS TOTAL_COST, ROUND(SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT), 0) ");
        sbMst.Append(" AS TOTAL_MARKET_PRICE, ROUND(TRUNC(SUM(PFOLIO_BK.TOT_NOS)) / TRUNC(SUM(COMP.NO_SHRS)) * 100, 2) ");
        sbMst.Append(" AS PERCENT_PAIDUP ");
        sbMst.Append(" FROM         COMP INNER JOIN ");
        sbMst.Append(" PFOLIO_BK ON COMP.COMP_CD = PFOLIO_BK.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (PFOLIO_BK.F_CD = " + fundCode + ") ");
        sbMst.Append(" GROUP BY PFOLIO_BK.SECT_MAJ_NM, PFOLIO_BK.SECT_MAJ_CD, FUND.F_NAME ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_CD ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        DataTable dtNonlistedSecrities = new DataTable();
        sbMst = new StringBuilder();
        sbMst.Append("SELECT      INV_AMOUNT AS COST_PRICE, INV_AMOUNT AS MARKET_PRICE ");
        sbMst.Append("FROM         NON_LISTED_SECURITIES ");
        sbMst.Append("WHERE     (F_CD = " + fundCode + ") AND (INV_DATE = ");
        sbMst.Append(" (SELECT     MAX(INV_DATE) AS EXPR1 ");
        sbMst.Append("FROM          NON_LISTED_SECURITIES NON_LISTED_SECURITIES_1 ");
        sbMst.Append("WHERE      (F_CD = " + fundCode + ") AND (INV_DATE <= '" + balDate + "'))) ");
        dtNonlistedSecrities = commonGatewayObj.Select(sbMst.ToString());
        Decimal nonlistedCostPrice = 0;
        Decimal nonlistedMarketPrice = 0;
        if (dtNonlistedSecrities.Rows.Count > 0)
        {
            nonlistedCostPrice = Convert.ToDecimal(dtNonlistedSecrities.Rows[0][0]);
            nonlistedMarketPrice = Convert.ToDecimal(dtNonlistedSecrities.Rows[0][0]);
        }
        
        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "InvByMFasPerSECrulesReport";
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtInvestmentByMFasPerSECrulesReport.xsd");
            //ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtInvestmentByMFasPerSECrulesReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_InvByMFasPerSECrules.ReportSource = rdoc;
            CRV_InvByMFasPerSECrules.DisplayToolbar = true;
            CRV_InvByMFasPerSECrules.HasExportButton = true;
            CRV_InvByMFasPerSECrules.HasPrintButton = true;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("prmAssetValue", assetValue);
            rdoc.SetParameterValue("prmNonlistedCostPrice", nonlistedCostPrice);
            rdoc.SetParameterValue("prmNonlisteMarketPrice", nonlistedMarketPrice);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_InvByMFasPerSECrules.Dispose();
        CRV_InvByMFasPerSECrules = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
