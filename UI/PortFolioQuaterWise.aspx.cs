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

            PortfolioAsOnDropDownList.DataSource = dtPortfolioAsOnDropDownList;
            PortfolioAsOnDropDownList.DataTextField = "Balance_Date";
            PortfolioAsOnDropDownList.DataValueField = "bal_dt_ctrl";
            PortfolioAsOnDropDownList.DataBind();

            PreviousquaterEndDropDownList.DataSource = dtPortfolioAsOnDropDownList;
            PreviousquaterEndDropDownList.DataTextField = "Balance_Date";
            PreviousquaterEndDropDownList.DataValueField = "bal_dt_ctrl";
            PreviousquaterEndDropDownList.DataBind();

        }
       
      
    }

    protected void showButton_Click(object sender, EventArgs e)
    {

        string fundcode = fundNameDropDownList.SelectedValue.ToString();
        string blncdate = PortfolioAsOnDropDownList.Text.ToString();
        string prevBalancedate = PreviousquaterEndDropDownList.Text.ToString();

        DateTime date = Convert.ToDateTime(blncdate);
        int quarterNumber = (date.Month - 1) / 3 + 1;
        DateTime firstDayOfQuarter = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
        DateTime lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);


        DateTime date2 = Convert.ToDateTime(prevBalancedate);
        int prevquarterNumber = (date2.Month - 1) / 3 + 1;
        DateTime firstDayOfPrevQuarter = new DateTime(date2.Year, (prevquarterNumber - 1) * 3 + 1, 1);
        DateTime lastDayOfPrevQuarter = firstDayOfPrevQuarter.AddMonths(3).AddDays(-1);

        string qenddate = Convert.ToDateTime(lastDayOfQuarter).ToString("dd-MMM-yyyy");
        string qprevenddate = Convert.ToDateTime(lastDayOfPrevQuarter).ToString("dd-MMM-yyyy");

        if (date > date2)
        {
                TimeSpan t = date - date2;
                double N0OfDays = t.TotalDays;
            if (N0OfDays >= 87 && N0OfDays <= 98)
          //  if (N0OfDays >= 90 && N0OfDays <= 94)
            {
                Session["quaterEndDate"] = blncdate;
                    Session["PrevQuaterEnddate"] = prevBalancedate;
                    Session["fundCodes"] = fundNameDropDownList.SelectedValue.ToString();
                // ClientScript.RegisterStartupScript(this.GetType(), "PortfolioSummaryReportViewer", "window.open('ReportViewer/PortFolioQuaterWiseWithNonListedReportViewer.aspx')", true);
            Response.Redirect("ReportViewer/PortFolioQuaterWiseWithNonListedReportViewer.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Quarter End Date and Previous Quarter End Date  must be between 3 months');", true);
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Quarter End Date must be greater than Previous Quarter End Date');", true);
        }

      
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