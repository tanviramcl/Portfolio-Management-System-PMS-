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

public partial class UI_InvestmentByMFasPerSECrulesReportForm : System.Web.UI.Page
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
        showButton.Visible = false;
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
        Session["balDate"] = portfolioAsOnDropDownList.SelectedValue.ToString();
        Session["assetValue"] = assetValueTextBox.Text;
        //ClientScript.RegisterStartupScript(this.GetType(), "InvestmentByMFasPerSECrulesReportViewer", "window.open('ReportViewer/InvestmentByMFasPerSECrulesReportViewer.aspx')", true);
        // ScriptManager.RegisterStartupScript(this.Page,this.Page.GetType(), "InvestmentByMFasPerSECrulesReportViewer", "window.open('ReportViewer/InvestmentByMFasPerSECrulesReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/InvestmentByMFasPerSECrulesReportViewer.aspx");
        showButton.Visible = false;
    }
    protected void showTotalAssetButton_Click(object sender, EventArgs e)
    {
        showButton.Visible = true;
        DataTable dtAssetValue = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        sbMst.Append(" SELECT      SUM(COSTPRICE) AS ASSET_VALUE ");
        sbMst.Append(" FROM         NAV.NAV_DETAILS ");
        sbMst.Append(" WHERE     (NAVROWTYPE = 'A') AND (navfundid = " + fundNameDropDownList.SelectedValue + ") and (NAVNO = ");
        sbMst.Append(" (SELECT     MAX(NAVNO) AS EXPR1 ");
        sbMst.Append(" FROM          NAV.NAV_MASTER NAV_MASTER_1 ");
        sbMst.Append(" WHERE      (NAVFUNDID = " + fundNameDropDownList.SelectedValue + ") AND (NAVDATE <= '" + portfolioAsOnDropDownList.SelectedValue + "'))) ");
        dtAssetValue = commonGatewayObj.Select(sbMst.ToString());
        assetValueTextBox.Text = dtAssetValue.Rows[0][0].ToString();
        fundCodeTextBox.Text = fundNameDropDownList.SelectedValue.ToString();
    }
}
