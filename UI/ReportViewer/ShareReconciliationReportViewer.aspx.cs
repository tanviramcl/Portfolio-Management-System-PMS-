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

public partial class UI_ReportViewer_ShareReconciliationReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string fromDate = "";
        string toDate = "";
        string fundCode = "";
        string companyCode = "";
        string fundName = "";
        string companyName = "";
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
            companyCode = (string)Session["companyCode"];
            fundName = (string)Session["fundName"];
            companyName = (string)Session["companyName"];
        }
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT     DERIVEDTBL_1.VCH_DT AS TranDate, decode(DERIVEDTBL_1.TRAN_TP, 'B', 'BONUS', 'S', 'PayIn', 'C', 'PayOut', 'I', 'IPO', 'P', 'IPO', 'R', 'Right', '0', ");
        sbMst.Append(" 'Opening') AS TranType, DERIVEDTBL_1.DEBIT, DERIVEDTBL_1.CREDIT, PFOLIO_BK.TOT_NOS AS Balance ");
        sbMst.Append(" FROM         PFOLIO_BK RIGHT OUTER JOIN ");
        sbMst.Append(" (SELECT     VCH_DT, F_CD, COMP_CD, TRAN_TP, DEBIT, CREDIT ");
        sbMst.Append(" FROM          (SELECT     VCH_DT, F_CD, COMP_CD, TRAN_TP, SUM(NO_SHARE) AS DEBIT, 0 AS CREDIT ");
        sbMst.Append(" FROM          FUND_TRANS_HB ");
        sbMst.Append(" WHERE      (TRAN_TP = 'S') AND (F_CD = "+fundCode+") AND (COMP_CD = "+companyCode+") AND (VCH_DT BETWEEN '"+fromDate+"' AND '"+toDate+"') ");
        sbMst.Append(" GROUP BY VCH_DT, F_CD, COMP_CD, TRAN_TP) DERIVEDTBL_1_1 ");
        sbMst.Append("  UNION  ");
        sbMst.Append(" SELECT     VCH_DT, F_CD, COMP_CD, TRAN_TP, 0 AS DEBIT, SUM(NO_SHARE) AS CREDIT ");
        sbMst.Append(" FROM         FUND_TRANS_HB FUND_TRANS_HB_1 ");
        sbMst.Append(" WHERE     (TRAN_TP <> 'S') AND (F_CD = " + fundCode + ") AND (COMP_CD = " + companyCode + ") AND (VCH_DT BETWEEN '" + fromDate + "' AND '" + toDate + "') ");
        sbMst.Append(" GROUP BY VCH_DT, F_CD, COMP_CD, TRAN_TP) DERIVEDTBL_1 ON PFOLIO_BK.F_CD = DERIVEDTBL_1.F_CD AND ");
        sbMst.Append(" PFOLIO_BK.COMP_CD = DERIVEDTBL_1.COMP_CD AND PFOLIO_BK.BAL_DT_CTRL = DERIVEDTBL_1.VCH_DT ");
        sbMst.Append(" ORDER BY DERIVEDTBL_1.VCH_DT ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "ShareReconciliationReport";
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtShareReconciliationReport.xsd");
            //ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtShareReconciliationReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_ShareReconciliation.ReportSource = rdoc;
            CRV_ShareReconciliation.DisplayToolbar = true;
            CRV_ShareReconciliation.HasExportButton = true;
            CRV_ShareReconciliation.HasPrintButton = true;
            rdoc.SetParameterValue("prmFundName", fundName);
            rdoc.SetParameterValue("prmCompName", companyName);
            rdoc.SetParameterValue("prmFromDate", fromDate);
            rdoc.SetParameterValue("prmToDate", toDate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_ShareReconciliation.Dispose();
        CRV_ShareReconciliation = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
