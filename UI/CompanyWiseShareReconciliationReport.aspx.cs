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

public partial class UI_CompanyWiseShareReconciliationReport : System.Web.UI.Page
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
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();

            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        Session["fromDate"] = howlaDateFromTextBox.Text.ToString();
        Session["toDate"] = howlaDateToTextBox.Text.ToString();
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
        Session["companyCode"] = companyNameDropDownList.SelectedValue.ToString();
        Session["fundName"] = fundNameDropDownList.SelectedItem.ToString();
        Session["companyName"] = companyNameDropDownList.SelectedItem.ToString();

        ClientScript.RegisterStartupScript(this.GetType(), "ShareReconciliationReportViewer", "window.open('ReportViewer/ShareReconciliationReportViewer.aspx')", true);
    }
}
