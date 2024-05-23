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
        
        
       
        DataTable dtHowlaDateDropDownList = dropDownListObj.BalanceDateDropDownList();
        DataTable dtHowlaDateDropDownListpreviousMonth = dropDownListObj.BalanceDateDropDownList();

        if (!IsPostBack)
        {

            portfolioAsOnDropDownList.DataSource = dtHowlaDateDropDownList;
            portfolioAsOnDropDownList.DataTextField = "Howla_Date";
            portfolioAsOnDropDownList.DataValueField = "BAL_DT_CTRL";
            portfolioAsOnDropDownList.DataBind();

           // portfolioPreviousMonthDropDownList
                 portfolioPreviousMonthDropDownList.DataSource = dtHowlaDateDropDownListpreviousMonth;
            portfolioPreviousMonthDropDownList.DataTextField = "Howla_Date";
            portfolioPreviousMonthDropDownList.DataValueField = "BAL_DT_CTRL";
            portfolioPreviousMonthDropDownList.DataBind();

        }

    }


    protected void radio_CheckedChanged(object sender, EventArgs e)
    {
        if (tbl2.Checked)
        {

            LabelFromDate.Visible = false;
            LabelToDte.Visible = false;
            RIssuefromTextBox.Visible = false;
            RIssueToTextBox.Visible = false;
            LabelPfolioAsOn.Visible = true;
            portfolioAsOnDropDownList.Visible = true;
            portfolioAsOnDropDownList.SelectedValue = "0";
            RIssuefromTextBox.Text = string.Empty;
            RIssueToTextBox.Text = string.Empty;
            LabelPreviousMonth.Visible = false;
            portfolioPreviousMonthDropDownList.Visible = false;
           
        }
        else if (tbl3.Checked)
        {
            LabelFromDate.Visible = true;
            LabelToDte.Visible = true;
            RIssuefromTextBox.Visible = true;
            RIssuefromTextBox.Text = "";
            RIssueToTextBox.Visible = true;
            RIssueToTextBox.Text = "";
            LabelPfolioAsOn.Visible = true;
            RIssuefromTextBox.Text = string.Empty;
            RIssueToTextBox.Text = string.Empty;
            portfolioAsOnDropDownList.Visible = true;
            portfolioAsOnDropDownList.SelectedValue = "0";
            LabelPreviousMonth.Visible = true;
            portfolioPreviousMonthDropDownList.Visible = true;
            portfolioPreviousMonthDropDownList.SelectedValue = "0";
            


        }
        else if (tbl6.Checked)
        {
            LabelFromDate.Visible = true;
            LabelToDte.Visible = true;
            RIssuefromTextBox.Visible = true;
          //  RIssuefromTextBox.Text = "";
            RIssueToTextBox.Visible = true;
          //  RIssueToTextBox.Text = "";
            RIssuefromTextBox.Text = string.Empty;
            RIssueToTextBox.Text = string.Empty;
            LabelPfolioAsOn.Visible = false;
            portfolioAsOnDropDownList.Visible = false;
            LabelPreviousMonth.Visible = false;
            portfolioPreviousMonthDropDownList.Visible = false;
            portfolioAsOnDropDownList.SelectedValue = "0";
            portfolioPreviousMonthDropDownList.SelectedValue = "0";

        }


    }
   

    protected void showButton_Click(object sender, EventArgs e)
    {
        string FromDatedate, Todatedate, pfolioAsOnDate, pfolioPreviousMonthDate;
        DateTime? date1, date2, date3,date4;

        if (!string.IsNullOrEmpty(RIssuefromTextBox.Text.Trim()))
        {
            date1 = Convert.ToDateTime(RIssuefromTextBox.Text.Trim());
            FromDatedate = date1.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            date1 = null;
            FromDatedate = "";
        }
        if (!string.IsNullOrEmpty(RIssueToTextBox.Text.Trim()))
        {
            date2 = Convert.ToDateTime(RIssueToTextBox.Text.Trim());
            Todatedate = date2.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            date2 = null;
            Todatedate = "";
        }

        if (!string.IsNullOrEmpty((portfolioAsOnDropDownList.Text.ToString())))
        {
            if (portfolioAsOnDropDownList.SelectedValue != "0")
            {
                date3 = Convert.ToDateTime(portfolioAsOnDropDownList.Text.ToString());
                pfolioAsOnDate = date3.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                date3 = null;
                pfolioAsOnDate = "";
            }
        }
        else
        {
            date3 = null;
            pfolioAsOnDate = "";
        }
        if (!string.IsNullOrEmpty((portfolioPreviousMonthDropDownList.Text.ToString())))
        {
            if (portfolioPreviousMonthDropDownList.SelectedValue != "0")
            {
                date4 = Convert.ToDateTime(portfolioPreviousMonthDropDownList.Text.ToString());
                pfolioPreviousMonthDate = date4.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                date4 = null;
                pfolioPreviousMonthDate = "";
            }
        }
        else
        {
            date4 = null;
            pfolioPreviousMonthDate = "";
        }
     


        if (pfolioAsOnDate != "" && FromDatedate == "" && Todatedate == "" && pfolioPreviousMonthDate=="")
        {
            // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked T2!')", true);
            Response.Redirect("ReportViewer/MonthlyReportSECReportViwerDetails.aspx?pfolioAsOnDate=" + pfolioAsOnDate + "&FromDatedate= " + FromDatedate + "&Todatedate= " + Todatedate + "&pfolioPreviousMonthDate= " + pfolioPreviousMonthDate + "");
        }
        else if (FromDatedate != "" && Todatedate != "" && pfolioAsOnDate != "" && pfolioPreviousMonthDate != "")
        {
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked T3!')", true);
             Response.Redirect("ReportViewer/MonthlyReportSECReportViwerDetails.aspx?pfolioAsOnDate=" + pfolioAsOnDate + "&FromDatedate= " + FromDatedate + "&Todatedate= " + Todatedate + "&pfolioPreviousMonthDate= " + pfolioPreviousMonthDate + "");

        }
        else if (pfolioAsOnDate == "" && FromDatedate != "" && Todatedate != "" && pfolioPreviousMonthDate =="")
        {
            // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked T6!')", true);
            Response.Redirect("ReportViewer/MonthlyReportSECReportViwerDetails.aspx?pfolioAsOnDate=" + pfolioAsOnDate + "&FromDatedate= " + FromDatedate + "&Todatedate= " + Todatedate + "&pfolioPreviousMonthDate= " + pfolioPreviousMonthDate + "");
        }
        
        // ClientScript.RegisterStartupScript(this.GetType(), "SellBuyCheckReportViwer", "window.open('ReportViewer/SellBuyCheckReportViwer.aspx')", true);


    }




}

   