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
    CommonGateway commonGatewayObj = new CommonGateway();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();

        DataTable dtPortfolioAsOnDropDownList = BalanceDateDropDownList();
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

        //string fundcode = fundNameDropDownList.SelectedValue.ToString();
        //string blncdate = PortfolioAsOnDropDownList.Text.ToString();
        //Response.Redirect("ReportViewer/PortFolioCategoryWiseReportViewer.aspx?fundcode="+fundcode+"&balancedate="+blncdate+"");
        DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
        DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


        string p1date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
        string p2date = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");

        Session["Fromdate"] = p1date;
        Session["Todate"] = p2date;
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
     
        // ClientScript.RegisterStartupScript(this.GetType(), "PortfolioSummaryInPieChartReportViewer", "window.open('ReportViewer/PortfolioSummaryInPieChartReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/ComparisonBetweenNavAndDseIndexReportViewer.aspx");
       // Response.Redirect("ReportViewer/StockDeclarationBeforePostedReportViewer.aspx");
    }

    public DataTable BalanceDateDropDownList()//Get Howla Date from invest.fund_trans_hb Table
    {
        DataTable dtHowlaDate = commonGatewayObj.Select("select distinct bal_dt_ctrl from pfolio_bk order by bal_dt_ctrl desc");
        DataTable dtHowlaDateDropDownList = new DataTable();
        dtHowlaDateDropDownList.Columns.Add("Balance_Date", typeof(string));
        dtHowlaDateDropDownList.Columns.Add("bal_dt_ctrl", typeof(string));
        DataRow dr = dtHowlaDateDropDownList.NewRow();
        dr["Balance_Date"] = "--Select--";
        dr["bal_dt_ctrl"] = "0";
        dtHowlaDateDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtHowlaDate.Rows.Count; loop++)
        {
            dr = dtHowlaDateDropDownList.NewRow();
            dr["Balance_Date"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["bal_dt_ctrl"]).ToString("dd-MMM-yyyy");
            dr["bal_dt_ctrl"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["bal_dt_ctrl"]).ToString("dd-MMM-yyyy");
            dtHowlaDateDropDownList.Rows.Add(dr);
        }
        return dtHowlaDateDropDownList;
    }


}