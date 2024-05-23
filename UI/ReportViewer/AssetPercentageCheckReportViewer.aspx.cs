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

public partial class UI_ReportViewer_AssetPercentageCheckReportViewer : System.Web.UI.Page
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

        string tranDate = Request.QueryString["transactionDate"].ToString();
        string percentageCheck = Request.QueryString["percentageCheck"].ToString();
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append(" SELECT     ASSET_VALUE.F_NAME, COMP.COMP_NM, PFOLIO_BK.SECT_MAJ_NM, PFOLIO_BK.TOT_NOS,  ");
        sbMst.Append(" PFOLIO_BK.TCST_AFT_COM, ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, ");
        sbMst.Append(" NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 2) ");
        sbMst.Append(" AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 2) AS TOT_MARKET_PRICE,  ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.ADC_RT, 2) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 2) - PFOLIO_BK.TCST_AFT_COM, 2) ");
        sbMst.Append(" AS APPRICIATION_ERROTION, PFOLIO_BK.BAL_DT_CTRL, ASSET_VALUE.ASSET_VALUE,  ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM / ASSET_VALUE.ASSET_VALUE * 100, 2) AS HOLDING_PERCENTAGE_OF_ASSET, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 2) AS PERCENTAGE_OF_PAIDUP ");
        sbMst.Append(" FROM         PFOLIO_BK INNER JOIN  ");
        sbMst.Append(" ASSET_VALUE ON PFOLIO_BK.F_CD = ASSET_VALUE.F_CD INNER JOIN ");
        sbMst.Append(" COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD WHERE ");
        if (percentageCheck != "")
        {
            sbMst.Append(" (ROUND(PFOLIO_BK.TCST_AFT_COM / ASSET_VALUE.ASSET_VALUE * 100, 2) >="+percentageCheck+") and ");
        }
        sbMst.Append(" (PFOLIO_BK.BAL_DT_CTRL = '" + Convert.ToDateTime(Request.QueryString["transactionDate"]).ToString("dd-MMM-yyyy") + "')  ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_NM, COMP.COMP_NM, PFOLIO_BK.F_CD ");


        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "AssetPercentageCheck";

        if (dtReprtSource.Rows.Count > 0)
        {
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmAssetPercentageCheckReport.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/AssetPercentageCheckReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_AssetPercentageCheck.ReportSource = rdoc;
            rdoc.SetParameterValue("prmtransactionDate", tranDate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
            CRV_AssetPercentageCheck.DisplayToolbar = true;
            CRV_AssetPercentageCheck.HasExportButton = true;
            CRV_AssetPercentageCheck.HasPrintButton = true;
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_AssetPercentageCheck.Dispose();
        CRV_AssetPercentageCheck = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }

   
}
