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

public partial class UI_ReportViewer_WeeklyReportToSECreportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        string weekEndDate = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            weekEndDate = (string)Session["weekEndDate"];
        }
      
        DataTable dtWeeklyReportSEC = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT     EQUITY.F_NAME, ROUND(DEBT.INVEST_DEBT / 1000000, 2) AS DEBT, ROUND(EQUITY.INVEST_EQUITY / 1000000, 2) AS EQUITY  ");
        sbMst.Append("FROM         (SELECT     FUND.F_CD, FUND.F_NAME, SUM(PFOLIO_BK.TCST_AFT_COM) AS INVEST_EQUITY ");
        sbMst.Append("FROM          FUND INNER JOIN ");
        sbMst.Append("PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD  ");
        sbMst.Append("WHERE      (PFOLIO_BK.SECT_MAJ_CD <> 89) AND (PFOLIO_BK.BAL_DT_CTRL = '" + weekEndDate + "') AND (FUND.F_CD NOT IN (1,3,5,6,18))  ");
        sbMst.Append("GROUP BY FUND.F_NAME, FUND.F_CD) EQUITY LEFT OUTER JOIN ");
        sbMst.Append("(SELECT     FUND_1.F_CD, FUND_1.F_NAME, SUM(PFOLIO_BK_1.TCST_AFT_COM) AS INVEST_DEBT ");
        sbMst.Append("FROM          FUND FUND_1 INNER JOIN  ");
        sbMst.Append(" PFOLIO_BK PFOLIO_BK_1 ON FUND_1.F_CD = PFOLIO_BK_1.F_CD  ");
        sbMst.Append("WHERE      (PFOLIO_BK_1.SECT_MAJ_CD = 89) AND (PFOLIO_BK_1.BAL_DT_CTRL = '" + weekEndDate + "') AND (FUND_1.F_CD NOT IN (1,3,5,6,18)) ");
        sbMst.Append("GROUP BY FUND_1.F_NAME, FUND_1.F_CD) DEBT ON EQUITY.F_CD = DEBT.F_CD ");
        sbMst.Append("ORDER BY EQUITY.F_CD  ");
        sbMst.Append(sbfilter.ToString());

        dtWeeklyReportSEC = commonGatewayObj.Select(sbMst.ToString());
        dtWeeklyReportSEC.TableName = "WeeklyReportSEC";


        if (dtWeeklyReportSEC.Rows.Count > 0)
        {
            //dtWeeklyReportSEC.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmWeeklyReportSEC.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/crtmWeeklyReportSEC.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtWeeklyReportSEC);
            CRV_SEC_Weekly.ReportSource = rdoc;
            CRV_SEC_Weekly.DisplayToolbar = true;
            CRV_SEC_Weekly.HasExportButton = true;
            CRV_SEC_Weekly.HasPrintButton = true;
            rdoc.SetParameterValue("prmWeekEndDate", weekEndDate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_SEC_Weekly.Dispose();
        CRV_SEC_Weekly = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
