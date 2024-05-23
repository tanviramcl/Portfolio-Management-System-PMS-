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

public partial class UI_AssetPercentageNAVSummaryAndPortfolio : System.Web.UI.Page
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
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();

            portfolioAsOnDropDownList.DataSource = dtHowlaDateDropDownList;
            portfolioAsOnDropDownList.DataTextField = "Howla_Date";
            portfolioAsOnDropDownList.DataValueField = "BAL_DT_CTRL";
            portfolioAsOnDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        string statementType = "";
        
        if (NAVRadioButton.Checked)
        {
            statementType = "Profit";
        }
        else if (summTotAsValRadioButton.Checked)
        {
            statementType = "Summary (Total Asset Value)";
        }
        else if (portTotAsValRadioButton.Checked)
        {
            statementType = "Portfolio (Total Asset Value)";
        }
        
       

        Session["statementType"] = statementType;
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
        Session["fundName"] = fundNameDropDownList.SelectedItem.Text.ToString();
        Session["balDate"] = portfolioAsOnDropDownList.SelectedValue.ToString();


        //  ClientScript.RegisterStartupScript(this.GetType(), "AssetPercentageNAVSummaryAndPortfolioReportViewer", "window.open('ReportViewer/AssetPercentageNAVSummaryAndPortfolioReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/AssetPercentageNAVSummaryAndPortfolioReportViewer.aspx");
    }
}
