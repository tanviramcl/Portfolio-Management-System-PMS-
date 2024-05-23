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

public partial class UI_MonthlyDeductionOfIAMCLemployeesReportForm : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DataTable dtBnkAdviceDropDownList = dropDownListObj.MonthlyBankAdviceDropDownList();
        if (!IsPostBack)
        {
            monthOfDeductionDropDownList.DataSource = dtBnkAdviceDropDownList;
            monthOfDeductionDropDownList.DataTextField = "MONTH_OF_BANK_ADVICE";
            monthOfDeductionDropDownList.DataValueField = "CAL_DATE";
            monthOfDeductionDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        string deductionType = "";
        if (welfareFund.Checked)
            deductionType = "welfareFund";
        else if (groupInsurance.Checked)
            deductionType = "groupInsurance";

        Session["calDate"] = monthOfDeductionDropDownList.SelectedValue.ToString();
        Session["deductionType"] = deductionType.ToString();
        // ClientScript.RegisterStartupScript(this.GetType(), "MonthlyDeductionOfIAMCLemployeesReportViewer", "window.open('ReportViewer/MonthlyDeductionOfIAMCLemployeesReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/MonthlyDeductionOfIAMCLemployeesReportViewer.aspx");
    }
}
