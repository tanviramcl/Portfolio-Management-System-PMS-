﻿using System;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
public partial class ReportViwer_CustodianFeeViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    AllReportDAO reportDAO = new AllReportDAO();
    protected void Page_Load(object sender, EventArgs e)
     {
        string FY = "";
        DateTime DateFrom = Convert.ToDateTime( Request.QueryString["dateFrom"].ToString());
        DateTime DateTo =Convert.ToDateTime(Request.QueryString["Todate"].ToString());
        //FY = DateFrom.ToString("dd - MMM - yyyy") + " to " + DateTo.ToString("dd - MMM - yyyy");
        int fundCode = Convert.ToInt32(Request.QueryString["FundCod"]);
        string fundName = Request.QueryString["FundName"].ToString();
        string RepotType = Request.QueryString["RepotType"].ToString();


        TimeSpan dateDifference = Convert.ToDateTime(DateTo.ToString("dd-MMM-yyyy")).Subtract(Convert.ToDateTime(DateFrom.ToString("dd-MMM-yyyy")));
        int days = dateDifference.Days + 1;
        if (days > 365)
        {
            days = 365;
        }
       
     
        DataTable dtReprtSource = new DataTable();
        DataTable dtTotal = new DataTable();
        decimal TotalSecurities = 0;
        decimal Average = 0;
        decimal totalCustoFee = 0;
        dtReprtSource.TableName = "Report";
        StringBuilder sbSQLSecurities = new StringBuilder();
        StringBuilder sbSQLTotalSecurities = new StringBuilder();
        if (RepotType.ToString() == "N")
        {
            sbSQLSecurities.Append(" SELECT TO_CHAR(NAV_MASTER.NAVDATE, 'DD-MON-YYYY') AS NAVDATE, NAVLASTVAULTVALUE AS MARKETPRICE FROM NAV_MASTER WHERE (NAV_MASTER.NAVDATE IN");
            sbSQLSecurities.Append(" (SELECT MAX(NAVDATE) AS MAXNAVDATE FROM NAV_MASTER NAV_MASTER_1 WHERE (NAV_MASTER_1.NAVDATE BETWEEN '" + Convert.ToDateTime(DateFrom).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(DateTo).ToString("dd-MMM-yyyy") + "') AND (NAV_MASTER_1.NAVFUNDID = " + fundCode + ")");
            sbSQLSecurities.Append(" GROUP BY TO_CHAR(NAV_MASTER_1.NAVDATE, 'MM'))) AND (NAV_MASTER.NAVFUNDID = " + fundCode + ") ORDER BY TO_DATE(NAV_MASTER.NAVDATE, 'DD-MON-YYYY')");

            sbSQLTotalSecurities.Append(" SELECT SUM(NAVLASTVAULTVALUE) AS TOTAL, ROUND(AVG(NAVLASTVAULTVALUE),2) AS AVERAGE FROM NAV_MASTER WHERE (NAVDATE IN");
            sbSQLTotalSecurities.Append(" (SELECT MAX(NAVDATE) AS MAXNAVDATE FROM NAV_MASTER NAV_MASTER_1 WHERE (NAV_MASTER_1.NAVDATE BETWEEN '" + Convert.ToDateTime(DateFrom).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(DateTo).ToString("dd-MMM-yyyy") + "') AND (NAV_MASTER_1.NAVFUNDID = " + fundCode + ")");
            sbSQLTotalSecurities.Append(" GROUP BY TO_CHAR(NAV_MASTER_1.NAVDATE, 'MM'))) AND (NAV_MASTER.NAVFUNDID = " + fundCode + ")");
        }
        else
        {
            sbSQLSecurities.Append("  SELECT NAVDATE, SUM(MARKETPRICE) AS MARKETPRICE FROM (SELECT  TO_CHAR(BAL_DT_CTRL, 'DD-MON-YYYY') AS NAVDATE, TOT_NOS * ADC_RT AS MARKETPRICE ");
            sbSQLSecurities.Append("  FROM  INVEST.PFOLIO_BK  WHERE (BAL_DT_CTRL IN (SELECT  MAX(BAL_DT_CTRL) AS MAXNAVDATE FROM  INVEST.PFOLIO_BK PFOLIO_BK_1 ");
            sbSQLSecurities.Append("  WHERE (BAL_DT_CTRL BETWEEN '" + Convert.ToDateTime(DateFrom).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(DateTo).ToString("dd-MMM-yyyy") + "') AND (F_CD  = " + fundCode + ")");
            sbSQLSecurities.Append("  GROUP BY TO_CHAR(BAL_DT_CTRL, 'MM')))  AND (F_CD = " + fundCode + ")) A  GROUP BY NAVDATE ORDER BY TO_DATE(NAVDATE, 'DD-MON-YYYY')");

        }
        dtReprtSource = commonGatewayObj.Select(sbSQLSecurities.ToString());


        DataTable dtCustodian = new DataTable();
        dtCustodian.Columns.Add("NAVDATE", typeof(string));
        dtCustodian.Columns.Add("LISTED_SECURITES_MP", typeof(decimal));
        dtCustodian.Columns.Add("NOLISTED_CP", typeof(decimal));
        dtCustodian.Columns.Add("FDR_Fee", typeof(decimal));
        dtCustodian.Columns.Add("TOTAL_PRICE", typeof(decimal));
        DataRow drCustodian;
        
        for (int loop = 0; loop < dtReprtSource.Rows.Count; loop++)
        {





            drCustodian = dtCustodian.NewRow();
            drCustodian["NAVDATE"] = dtReprtSource.Rows[loop]["NAVDATE"];



            string queryStringnolisted = " select count (NO_SHARES) as total_securities,sum(NO_SHARES) as total_no_share,sum(AMOUNT) as total_cost_Price ,sum(TOT_MARKET_PRICE) as total_market_price,sum(APPRICIATION_ERROTION) as total_appriciation from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
                " DECODE(c.AMOUNT,0,0,ROUND( (c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8))   AS PERCENT_OF_APRE_EROSION " +
                " from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
                " (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, " +
                " decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
                " from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
                " where f_cd IN( " + fundCode + ") and    INV_DATE <= '" + dtReprtSource.Rows[loop]["NAVDATE"] + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d " +
                " on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from invest.NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + dtReprtSource.Rows[loop]["NAVDATE"] + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c inner join " +
                " invest.NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join invest.NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID order by CAT_ID)  ";

            DataTable dtNonListed_securities = commonGatewayObj.Select(queryStringnolisted);


            string FdrList = "   select sum(FDR_AMOUNT)  As FDR from ( select c.ID,c.FDR_NO,c.FDR_OPEN_DT ,c.BANK_NAME,d.BRANCH_CODE,D.BRANCH_NAME,D.BRANCH_ADDRS1,D.BRANCH_ADDRS2,c.FDR_RATE,c.FDR_AMOUNT  as FDR_AMOUNT ,(((c.FDR_MATUR_DT -c.FDR_OPEN_DT +1 ) *FDR_RATE*FDR_AMOUNT))  As MaturityValue  from (Select a.*, B.BANK_NAME from(SELECT  ID, FDR_NO, FUND_CD, FUND_BR_CODE, FDR_OPEN_DT, FDR_RATE, FDR_AMOUNT, FDR_MATUR_DT, BANK_CODE, BANK_BRANC_CODE, FDR_STATUS, FDR_REN_NO, " +
            " VALID, EDIT_TYPE, ENTRY_BY, ENTRY_DATE, REMARKS, FDR_CLOS_DT, FDR_TAX_DIDUCT, FDR_EXCI_DIDUCT, FDR_CLOS_TYPE, FDR_RECV_INTEREST, PAY_BANK_CODE, PAY_BANK_BR_CODE, " +
           " PAY_TYPE, PAY_CHQ_DD_NO, PAY_CHQ_DD_DT, PAY_AMOUNT, FDR_DURATION, FDR_DURATION_TYPE, FDR_CURRENCY_ID, ACC_DEBIT_HEAD, ACC_DEBIT_VOUCHER_NO, " +
          " ACC_DEBIT_DT, ACC_CREDIT_HEAD, ACC_CREDIT_VOUCHER_NO, ACC_CREDIT_DT, FDR_INSTRUMNT_NO, FDR_INSTRUMNT_DT, FDR_BANK_ACC, FDR_ACC_REFERENC, FDR_CLOSE_BY, " +
         "  FDR_CLOSE_BY_DT FROM INVEST.FDR_INFO) a INNER JOIN  UNIT.BANK_NAME  b on a.BANK_CODE = b.BANK_CODE " +
   //      "  WHERE  a.VALID is NULL   and a.FDR_REN_NO is null  and FDR_OPEN_DT  <=  '" + balDate + "' and FDR_MATUR_DT >='" + balDate + "'  and a.FUND_CD = " + fundCode + " order by BANK_NAME) c  " +
   "  WHERE  a.VALID is NULL   and FDR_OPEN_DT  <=  '" + dtReprtSource.Rows[loop]["NAVDATE"] + "' and FDR_MATUR_DT >'"  +dtReprtSource.Rows[loop]["NAVDATE"] +  "'  and a.FUND_CD = " + fundCode + " order by BANK_NAME) c  " +
             " inner join UNIT.BANK_BRANCH d on c.BANK_CODE = d.BANK_CODE and c.BANK_BRANC_CODE = d.BRANCH_CODE )  ";

            DataTable dtFdrList = commonGatewayObj.Select(FdrList);


            //  DataTable dtNonListed = commonGatewayObj.Select("SELECT  INV_AMOUNT, INV_DATE FROM  INVEST.NON_LISTED_SECURITIES WHERE  F_CD=" + fundCode + " and INV_DATE <= '" + dtReprtSource.Rows[loop]["NAVDATE"] + "' ORDER BY INV_DATE DESC ");
            if (dtNonListed_securities.Rows.Count > 0)
            {
                drCustodian["LISTED_SECURITES_MP"] = Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) ;
                if (Convert.ToInt32(dtNonListed_securities.Rows[0]["total_securities"]) > 0)
                {
                    
                    drCustodian["NOLISTED_CP"] = Convert.ToDecimal(dtNonListed_securities.Rows[0]["total_market_price"]);
                    string fdr = dtFdrList.Rows[0]["FDR"].ToString();

                    if (dtFdrList.Rows.Count > 0 && fdr !="")
                    {
                        drCustodian["FDR_Fee"] = Convert.ToDecimal(dtFdrList.Rows[0]["FDR"]);

                        drCustodian["TOTAL_PRICE"] = Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) + Convert.ToDecimal(dtNonListed_securities.Rows[0]["total_market_price"]) + Convert.ToDecimal(dtFdrList.Rows[0]["FDR"]);
                        TotalSecurities = TotalSecurities + Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) + Convert.ToDecimal(dtNonListed_securities.Rows[0]["total_market_price"]) + Convert.ToDecimal(dtFdrList.Rows[0]["FDR"]);
                    }
                    else
                    {
                        drCustodian["FDR_Fee"] = 0;
                        drCustodian["TOTAL_PRICE"] = Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) + Convert.ToDecimal(dtNonListed_securities.Rows[0]["total_market_price"]) + 0;
                        TotalSecurities = TotalSecurities + Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) + Convert.ToDecimal(dtNonListed_securities.Rows[0]["total_market_price"]) + 0;
                    }
                    

                   

                }
                else
                {
                    drCustodian["TOTAL_PRICE"] = Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) + 0+0;
                    drCustodian["NOLISTED_CP"] = 0;
                    drCustodian["FDR_Fee"] = 0;
                    TotalSecurities = TotalSecurities + Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) + 0+0;
                }

              
            }
            else
            {
                drCustodian["LISTED_SECURITES_MP"] = Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]);
                drCustodian["NOLISTED_CP"] =0;
                drCustodian["TOTAL_PRICE"] = Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]);
                TotalSecurities = TotalSecurities + Convert.ToDecimal(dtReprtSource.Rows[loop]["MARKETPRICE"]) ;
            }
            dtCustodian.Rows.Add(drCustodian);
        }


        Average =decimal.Round(Convert.ToDecimal(TotalSecurities / dtReprtSource.Rows.Count),4);
     



       //  dtTotal = commonGatewayObj.Select(sbSQLTotalSecurities.ToString());
        string totalCustoFeeWords = "";
        string totalCustoFeeString = "";

         if (dtCustodian.Rows.Count > 0)
         {
             if (fundCode == 15|| fundCode == 16)//for ICB AMCL Sonali and Agrani Bank 1st MF
             {
                // TotalSecurities = Convert.ToDecimal(dtTotal.Rows[0]["TOTAL"]);
               //  Average = Convert.ToDecimal(dtTotal.Rows[0]["AVERAGE"]);
                 totalCustoFee = (Average * Convert.ToDecimal(0.075)) / 100;
                 totalCustoFee = (totalCustoFee * days) / 365;
                 totalCustoFee = Decimal.Round(totalCustoFee, 2);
                 totalCustoFeeString = " " + Average.ToString() + "*0.075%";
               // dtCustodian.WriteXmlSchema(@"D:\Development\AMCL.AllReport\UI\ReportViewer\Report\crtCustoFee_final.xsd");

                NumberToEnglish numberToEnglishObj = new NumberToEnglish();

                 totalCustoFeeWords = "(Taka " + numberToEnglishObj.changeNumericToWords(Decimal.Round(totalCustoFee, 2)) + ")";
                 ReportDocument rdoc = new ReportDocument();
                 string Path = Server.MapPath("Report/crtCustoFee.rpt");
                 rdoc.Load(Path);
                 rdoc.SetDataSource(dtCustodian);
                 CrystalReportViewer1.ReportSource = rdoc;
                 rdoc.SetParameterValue("prmFundName", fundName);
                 rdoc.SetParameterValue("prmFY", FY);
                 rdoc.SetParameterValue("prmTotal", TotalSecurities.ToString());
                 rdoc.SetParameterValue("prmAvg", Average.ToString());
                 rdoc.SetParameterValue("prmTotalCustoFee", totalCustoFee.ToString());
                 rdoc.SetParameterValue("prmCustoFeeString", totalCustoFeeString);
                 rdoc.SetParameterValue("prmTotalCustoFeeWords", totalCustoFeeWords);
             }
             else
             {
               //  TotalSecurities = Convert.ToDecimal(dtTotal.Rows[0]["TOTAL"]);
               //  Average = Convert.ToDecimal(dtTotal.Rows[0]["AVERAGE"]);
                 totalCustoFee = (Average * Convert.ToDecimal(0.1)) / 100;
                 totalCustoFee = (totalCustoFee * days) / 365;
                 totalCustoFee = Decimal.Round(totalCustoFee, 2);
                 totalCustoFeeString = " " + Average.ToString() + "*0.10%";
                dtCustodian.TableName = "CUSTODIAN";

              //  dtCustodian.WriteXmlSchema(@"D:\Development\AMCL.AllReport\UI\ReportViewer\Report\crtCustoFee_final.xsd");

                NumberToEnglish numberToEnglishObj = new NumberToEnglish();

                 totalCustoFeeWords = "(Taka " + numberToEnglishObj.changeNumericToWords(Decimal.Round(totalCustoFee, 2)) + ")";
                 ReportDocument rdoc = new ReportDocument();
                 string Path = Server.MapPath("Report/crtCustoFee.rpt");
                 rdoc.Load(Path);
                 rdoc.SetDataSource(dtCustodian);
                 CrystalReportViewer1.ReportSource = rdoc;
                 rdoc.SetParameterValue("prmFundName", fundName);
                 rdoc.SetParameterValue("prmFY", FY);
                 rdoc.SetParameterValue("prmTotal", TotalSecurities.ToString());
                 rdoc.SetParameterValue("prmAvg", Average.ToString());
                 rdoc.SetParameterValue("prmTotalCustoFee", totalCustoFee.ToString());
                 rdoc.SetParameterValue("prmCustoFeeString", totalCustoFeeString);
                 rdoc.SetParameterValue("prmTotalCustoFeeWords", totalCustoFeeWords);
             }
         }
         else
         {
             Response.Write("No Data Found");
         }
       
    }
}
