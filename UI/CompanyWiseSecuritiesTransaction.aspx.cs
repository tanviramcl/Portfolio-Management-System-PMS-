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

public partial class UI_CompanyWiseSecuritiesTransaction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        string bdf = "";
        if (includingBDF.Checked)
            bdf = "yes";
        else if (excludingBDF.Checked)
            bdf = "no";
        Session["fromDate"] = howlaDateFromTextBox.Text.ToString();
        Session["toDate"] = howlaDateToTextBox.Text.ToString();
        Session["transType"] = transTypeDropDownList.SelectedValue.ToString();
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
        Session["companyCode"] = companyNameDropDownList.SelectedValue.ToString();
        Session["companyCode"] = companyNameDropDownList.SelectedValue.ToString();
        Session["bdf"] = bdf.ToString();
        //  ClientScript.RegisterStartupScript(this.GetType(), "CompanyWiseSecuritiesTransactionReportViewer", "window.open('ReportViewer/CompanyWiseSecuritiesTransactionReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/CompanyWiseSecuritiesTransactionReportViewer.aspx");
    }
}
