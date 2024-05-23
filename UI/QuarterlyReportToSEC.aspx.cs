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

public partial class UI_QuarterlyReportToSEC : System.Web.UI.Page
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
        DataTable dtHowlaDateDropDownList = dropDownListObj.BalanceDateDropDownList();
        DataTable dtICBTrusteeDropDownList = dropDownListObj.GetICBTrusteeDropDownList();
        if (!IsPostBack)
        {
            quarterEndDateDropDownList.DataSource = dtHowlaDateDropDownList;
            quarterEndDateDropDownList.DataTextField = "Howla_Date";
            quarterEndDateDropDownList.DataValueField = "BAL_DT_CTRL";
            quarterEndDateDropDownList.DataBind();

            DropDownListTrustee.DataSource = dtICBTrusteeDropDownList;
            DropDownListTrustee.DataTextField = "TRUSTEE_NAME";
            DropDownListTrustee.DataValueField = "TRUSTEE_ID";
            DropDownListTrustee.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(quarterStartDateTextBox.Text.Trim()) < Convert.ToDateTime(quarterEndDateDropDownList.SelectedValue))
        {
            string reportType = "";
            //if (BSECRadioButton.Checked)
            //{
            //    reportType = "BSEC";
            //}
            //else if (ICBTRusteeRadioButton.Checked)
            //{
            //    reportType = "ICBTRUSTEE";
            //}
            //else if (ICBCapitalTrusteeRadioButton.Checked)
            //{
            //    reportType = "ICBCAPITALTRUSTEE";
            //}


            reportType = DropDownListTrustee.SelectedValue.ToString();
            Session["quarterStartDate"] = quarterStartDateTextBox.Text.ToString();
            Session["quarterEndDate"] = quarterEndDateDropDownList.SelectedValue.ToString();
            Session["reportType"] = reportType;
            //  ClientScript.RegisterStartupScript(this.GetType(), "QuarterlyReportViewer", "window.open('ReportViewer/QuarterlyReportToSECReportViewer.aspx')", true);
            Response.Redirect("ReportViewer/QuarterlyReportToSECReportViewer.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Is it possible to be Quarter End Date smaller than Quarter Start Date?');", true);
        }
    }
}
