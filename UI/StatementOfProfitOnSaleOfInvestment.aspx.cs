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

            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

        }

    }
    protected void radio_CheckedChanged(object sender, EventArgs e)
    {
        if (DateWise.Checked)
        {
            fundNameDropDownList.Visible = true;
            fundNameDropDownListlabel.Visible = true;
            comppanynameListlabel.Visible = false;
            companyNameDropDownList.Visible = false;
        }
        else if (CompanyWise.Checked)
        {

            fundNameDropDownList.Visible = true;
            fundNameDropDownListlabel.Visible = true;
            comppanynameListlabel.Visible = false;
            companyNameDropDownList.Visible = false;
        }
        else if (alltype.Checked)
        {
            fundNameDropDownList.Visible = true;
            fundNameDropDownListlabel.Visible = true;
            comppanynameListlabel.Visible = true;
            companyNameDropDownList.Visible = true;
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {



      DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
      DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


        string p1date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
        string p2date = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");

        string statementType = "";

        if (DateWise.Checked)
        {
            statementType = "DateWise";
        }
        else if (CompanyWise.Checked)
        {
            statementType = "CompanyWise";
        }
        else if (alltype.Checked)
        {
            statementType = "alltype";
        }

        Session["Fromdate"] = p1date;
        Session["Todate"] = p2date;
        Session["fundCodes"] = fundNameDropDownList.SelectedValue.ToString();
        Session["COMP_CD"] = companyNameDropDownList.SelectedValue.ToString();
        Session["comp_name"] = companyNameDropDownList.SelectedItem.Text.ToString();
        Session["statementType"] = statementType;
        Session["fundName"] = fundNameDropDownList.SelectedItem.Text.ToString();
        StringBuilder sb = new StringBuilder();

        //  ClientScript.RegisterStartupScript(this.GetType(), "StatementOfProfitOnSaleOfInvestmentReportVeiwer", "window.open('ReportViewer/StatementOfProfitOnSaleOfInvestmentReportVeiwer.aspx')", true);
        Response.Redirect("ReportViewer/StatementOfProfitOnSaleOfInvestmentReportVeiwer.aspx");
    }

  
}