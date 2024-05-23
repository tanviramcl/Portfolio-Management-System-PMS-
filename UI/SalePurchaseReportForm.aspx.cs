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

public partial class UI_SalePurchaseReportForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        Session["fromDate"] = howlaDateFromTextBox.Text.ToString();
        Session["toDate"] = howlaDateToTextBox.Text.ToString();
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
        //ClientScript.RegisterStartupScript(this.GetType(), "SalePurchaseReportViewer", "window.open('ReportViewer/SalePurchaseReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/SalePurchaseReportViewer.aspx");
    }
}
