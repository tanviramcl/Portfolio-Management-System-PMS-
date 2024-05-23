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

public partial class UI_DailyReportToSEC : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            howlaDateDropDownList.DataSource = dtHowlaDateDropDownList;
            howlaDateDropDownList.DataTextField = "Howla_Date";
            howlaDateDropDownList.DataValueField = "BAL_DT_CTRL";
            howlaDateDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        string howlaDate = howlaDateDropDownList.SelectedValue.ToString();

        StringBuilder sb = new StringBuilder();
        sb.Append("window.open('ReportViewer/SEC_ReportDailyReportViewer.aspx?howlaDate=" + howlaDate + "');");
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
    }
}
