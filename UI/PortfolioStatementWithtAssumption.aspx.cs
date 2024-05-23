using System;
using System.Data;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Web.UI;

public partial class UI_PortfolioStatementWithtAssumption : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    private ReportDocument rdoc = new ReportDocument();
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

       
        decimal fundSize = 1;
        DataTable dtFundSize = commonGatewayObj.Select(" SELECT * FROM NAV.NAV_MASTER WHERE (NAVFUNDID = "+ fundNameDropDownList.SelectedValue.ToString() + ") AND (NAVDATE IN (SELECT MAX(NAVDATE) AS NAVDATE FROM NAV.NAV_MASTER NAV_MASTER1 WHERE (NAVFUNDID = " + fundNameDropDownList.SelectedValue.ToString() + ")))");
        if(marketPriceRadioButton.Checked)
        {
            fundSize = Convert.ToDecimal(dtFundSize.Rows[0]["NAVTOTALMARKETPRICE"].ToString());
        }
        else
        {
            fundSize = Convert.ToDecimal(dtFundSize.Rows[0]["NAVTOTALCOSTPRICE"].ToString());
        }

        string fundCode = fundNameDropDownList.SelectedValue.ToString();
        string balDate = portfolioAsOnDropDownList.SelectedValue.ToString();       
        string mpFactor = pricePCTTextBox.Text;
        decimal increased_MP = 0;
        if (Convert.ToDecimal(mpFactor) == 0)
        {
            increased_MP = 0;
        }
        else
        {
            increased_MP = decimal.Round(1 + (Convert.ToDecimal(mpFactor) / 100), 2);
        }
        long amountIn = 0;
        string amountInPM = "";
       if(corereRadioButton.Checked)
        {
            amountIn = 10000000;
            amountInPM="(Crore)";
        }
       else
        {
            amountIn = 100000;
            amountInPM = "(Lac)";
        }
       
        DataTable PortfolioPriceAssumption = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();

        sbMst.Append(" SELECT A.F_NAME, A.COMP_NM, A.INCRE_RATE,A.TOT_SHARES,A.COST_RT,ROUND(A.TOT_COST_PRICE/" + amountIn + ",2) AS TOT_COST_PRICE, ROUND((A.TOT_COST_PRICE/" + fundSize + ")*100,2) AS CURR_INV_PCT, A.MARKET_RT,ROUND(A.TOT_MARKET_PRICE/" + amountIn + ",2) AS TOT_MARKET_PRICE,");
        sbMst.Append(" ROUND(TOT_SHARES*(ROUND((COST_RT-INCRE_RATE),2)/ROUND((INCRE_RATE-MARKET_RT),2)),2) AS SHARE_PURCHASE, ROUND( ROUND(TOT_SHARES*((COST_RT-INCRE_RATE)/(INCRE_RATE-MARKET_RT))*MARKET_RT,2)/" + amountIn + ",2) AS AMOUNT_NEED, ");
        sbMst.Append(" ROUND( ROUND(TOT_COST_PRICE+TOT_SHARES*((COST_RT-INCRE_RATE)/(INCRE_RATE-MARKET_RT))*MARKET_RT,2)/" + amountIn + ",2) AS TOTAL_INVST, ROUND((TOT_COST_PRICE+TOT_SHARES*((COST_RT-INCRE_RATE)/(INCRE_RATE-MARKET_RT))*MARKET_RT)/" + fundSize + "*100,2) AS INV_PCT ");
        sbMst.Append(" FROM (SELECT FUND.F_NAME, COMP.COMP_NM, PFOLIO_BK.SECT_MAJ_NM, PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS, 0) AS TOT_SHARES, ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM,2) AS TOT_COST_PRICE,ROUND(NVL(PFOLIO_BK.DSE_RT, 0),2) AS MARKET_RT, ROUND(PFOLIO_BK.TOT_NOS *( NVL(PFOLIO_BK.DSE_RT, 0)), 2) AS TOT_MARKET_PRICE, ");
        sbMst.Append(" ROUND(NVL(PFOLIO_BK.DSE_RT, 0)* "+ increased_MP + ",2) AS INCRE_RATE FROM  PFOLIO_BK INNER JOIN COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE (PFOLIO_BK.BAL_DT_CTRL ='"+ balDate + "') AND (FUND.F_CD =" + fundNameDropDownList.SelectedValue.ToString() + ") AND (ROUND(ROUND(PFOLIO_BK.TOT_NOS * (NVL(PFOLIO_BK.DSE_RT, 0)* "+ increased_MP + "), 2)  - PFOLIO_BK.TCST_AFT_COM, 2) <0) ");
        sbMst.Append(" ORDER BY COMP.COMP_NM ) A WHERE INCRE_RATE<>MARKET_RT ORDER BY AMOUNT_NEED ");


        PortfolioPriceAssumption = commonGatewayObj.Select(sbMst.ToString());
        if (PortfolioPriceAssumption.Rows.Count > 0)
        {
            Session["PortfolioPriceAssumption"] = PortfolioPriceAssumption;
            Session["balDate"] = balDate;
            Session["incMarketPricePCT"] = pricePCTTextBox.Text.Trim();
            Session["amountInPM"] = amountInPM;
            Response.Write("<script>window.open('ReportViewer/PortfolioPriceAssumptionReportReportViewer.aspx/','_blank');</script>");
            // Response.Redirect("ReportViewer/PortfolioStatementWithtAssumptionRDLCViewer.aspx");

        }
        else
        {
            Session["PortfolioPriceAssumption"] = null;
           ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", "alert('No Data Found')", true);
        }






    }
    
}
