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

    }

    protected void showButton_Click(object sender, EventArgs e)
    {

        //DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
       // DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


        string Fromdate = Convert.ToDateTime(RIssuefromTextBox.Text).ToString("dd-MMM-yyyy");
        string Todate = Convert.ToDateTime(RIssueToTextBox.Text).ToString("dd-MMM-yyyy");

        //string Fromdate = Convert.ToDateTime(RIssuefromTextBox.Text).ToString("dd-MMM-yyyy");
        //string Todate = Convert.ToDateTime(RIssueToTextBox.Text).ToString("dd-MMM-yyyy");

        Response.Redirect("ReportViewer/CapitalGainSummeryCompanyWiseDscreportViwer.aspx?Fromdate=" + Fromdate + "&Todate="+Todate+ "");

    }
}

   