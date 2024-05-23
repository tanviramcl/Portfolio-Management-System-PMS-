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

public partial class UI_ReportViewer_SelectionScaleCalculation : System.Web.UI.Page
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

        DataTable dtSelectionScale = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT     AMCL_EMP_SALARY_SELECTION.ID, EMP_INFO.NAME, DESIGNATION.DESIG, EMP_INFO.ICB_ID, ");
        sbMst.Append(" AMCL_EMP_SALARY_SELECTION.MONTH, AMCL_EMP_SALARY_SELECTION.BASIC_AS_30JUN09, ");
        sbMst.Append(" AMCL_EMP_SALARY_SELECTION.BASIC_CURRENT, AMCL_EMP_SALARY_SELECTION.HOUSE_RENT_AS_30JUN09, ");
        sbMst.Append("AMCL_EMP_SALARY_SELECTION.HOUSE_RENT_NEW, AMCL_EMP_SALARY_SELECTION.BASIC_AS_PREV,  ");
        sbMst.Append("AMCL_EMP_SALARY_SELECTION.FIXATION_BASIC, AMCL_EMP_SALARY_SELECTION.ALLOWANCE_BASIC_DIFF, ");
        sbMst.Append("AMCL_EMP_SALARY_SELECTION.ALLOWANCE_HOUSE_RENT, AMCL_EMP_SALARY_SELECTION.ALLOWANCE_PENSION,  ");
        sbMst.Append("AMCL_EMP_SALARY_SELECTION.ALLOWANCE_DEPUTATION, AMCL_EMP_SALARY_SELECTION.GROSS_ALLOWANCE,  ");
        sbMst.Append("AMCL_EMP_SALARY_SELECTION.DEDUCTION_PF_OWN_CON, AMCL_EMP_SALARY_SELECTION.DEDUCTION_PENSION,  ");
        sbMst.Append("AMCL_EMP_SALARY_SELECTION.GROSS_DEDUCTION, AMCL_EMP_SALARY_SELECTION.ARREAR_AMOUNT,AMCL_EMP_SALARY_SELECTION.NET_PAYABLE  ");
        sbMst.Append("FROM       EMP_INFO INNER JOIN  ");
        sbMst.Append("DESIGNATION ON EMP_INFO.RANK = DESIGNATION.RANK INNER JOIN ");
        sbMst.Append("AMCL_EMP_SALARY_SELECTION ON EMP_INFO.ID = AMCL_EMP_SALARY_SELECTION.ID ");

        sbMst.Append(sbfilter.ToString());
        dtSelectionScale = commonGatewayObj.Select(sbMst.ToString());
        dtSelectionScale.TableName = "SelectionScale";

        if (dtSelectionScale.Rows.Count > 0)
        {
            //dtSelectionScale.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmSelectionScaleReport.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/SelectionScaleReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtSelectionScale);
            CrystalReportViewerSelectionScaleCalculation.ReportSource = rdoc;
            CrystalReportViewerSelectionScaleCalculation.DisplayToolbar = true;
            CrystalReportViewerSelectionScaleCalculation.HasExportButton = true;
            CrystalReportViewerSelectionScaleCalculation.HasPrintButton = true;
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CrystalReportViewerSelectionScaleCalculation.Dispose();
        CrystalReportViewerSelectionScaleCalculation = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
