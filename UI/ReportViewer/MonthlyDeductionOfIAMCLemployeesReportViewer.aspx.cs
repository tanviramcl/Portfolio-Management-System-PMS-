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

public partial class UI_ReportViewer_MonthlyDeductionOfIAMCLemployeesReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1Obj = new Pf1s1DAO();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string deductionType = "";
        string calDate = "";
        
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            calDate = (string)Session["calDate"];
            deductionType = (string)Session["deductionType"];
        }
        string deductType = "";
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT     EMP_INFO.ID, decode(EMP_INFO.SEX, 'M', 'Mr. ' || EMP_INFO.NAME, 'Ms. ' || EMP_INFO.NAME) AS NAME, ");
        sbMst.Append("DESIGNATION.DESIG, ");
        if (String.Compare(deductionType.ToString(), "welfareFund", true) == 0)
        {
            sbMst.Append(" AMCL_EMP_SALARY.EMP_BEN_FUND AS DEDUCTION_AMT ");
            deductType = "Benevolent Fund";
        }
        else if (String.Compare(deductionType.ToString(), "groupInsurance", true) == 0)
        {
            sbMst.Append("AMCL_EMP_SALARY.GI_PREM AS DEDUCTION_AMT ");
            deductType = "Group Insurance";
        }
        sbMst.Append("FROM         EMP_INFO INNER JOIN ");
        sbMst.Append("AMCL_EMP_SALARY ON EMP_INFO.ID = AMCL_EMP_SALARY.ID INNER JOIN ");
        sbMst.Append("DESIGNATION ON EMP_INFO.RANK = DESIGNATION.RANK ");
        sbMst.Append("WHERE     (EMP_INFO.ISCOMP = 'Y') AND (AMCL_EMP_SALARY.CAL_DATE = '" +Convert.ToDateTime(calDate).ToString("dd-MMM-yyyy") + "') ");
        sbMst.Append("ORDER BY EMP_INFO.RANK, EMP_INFO.SENIORITY ");
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        if (dtReprtSource.Rows.Count > 0)
        {
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmMonthlyDeductionOfIAMCLemployeesReport.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/crtmMonthlyDeductionOfIAMCLemployeesReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_MonthlyDeductionOfIAMCLemployeesSalary.ReportSource = rdoc;
            CRV_MonthlyDeductionOfIAMCLemployeesSalary.DisplayToolbar = true;
            CRV_MonthlyDeductionOfIAMCLemployeesSalary.HasExportButton = true;
            CRV_MonthlyDeductionOfIAMCLemployeesSalary.HasPrintButton = true;
            rdoc.SetParameterValue("prmCalDate", calDate);
            rdoc.SetParameterValue("prmDeductType", deductType);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_MonthlyDeductionOfIAMCLemployeesSalary.Dispose();
        CRV_MonthlyDeductionOfIAMCLemployeesSalary = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
