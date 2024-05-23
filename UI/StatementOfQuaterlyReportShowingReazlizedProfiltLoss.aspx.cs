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
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
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



        string howlaDateFrom = howlaDateFromTextBox.Text.ToString();
        string howlaDateTo = howlaDateToTextBox.Text.ToString();



        Session["Fromdate"] = howlaDateFrom;
        Session["Todate"] = howlaDateTo;
        Session["fundCodes"] = fundNameDropDownList.SelectedValue.ToString();
        Session["fundName"] = fundNameDropDownList.SelectedItem.Text.ToString();
        StringBuilder sb = new StringBuilder();

        //  ClientScript.RegisterStartupScript(this.GetType(), "StatementOfProfitOnSaleOfInvestmentReportVeiwer", "window.open('ReportViewer/StatementOfProfitOnSaleOfInvestmentReportVeiwer.aspx')", true);
        Response.Redirect("ReportViewer/StatementOfQuaterlyReportShowingReazlizedProfiltLossReportVeiwer.aspx");
    }

  
}