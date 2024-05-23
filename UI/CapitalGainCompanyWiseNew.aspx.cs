using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class UI_BalancechekReport : System.Web.UI.Page
{
    DropDownList dropDownListObj = new DropDownList();
    protected void Page_Load(object sender, EventArgs e)
    {
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

        DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
        DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


        string Fromdate = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
        string Todate = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");
        string fundcode = fundNameDropDownList.SelectedValue.ToString();
        //string Fromdate = Convert.ToDateTime(RIssuefromTextBox.Text).ToString("dd-MMM-yyyy");
        //string Todate = Convert.ToDateTime(RIssueToTextBox.Text).ToString("dd-MMM-yyyy");
        string transtype = transTypeDropDownList.SelectedValue.ToString();
        Response.Redirect("ReportViewer/CapitalGainCompanyWiseNewReportViwer.aspx?fundcode=" + fundcode + "&Fromdate=" + Fromdate + "&Todate="+Todate+ "&transtype="+ transtype + "");

    }
}

   