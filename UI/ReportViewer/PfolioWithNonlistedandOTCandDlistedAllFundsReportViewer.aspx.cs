using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_CapitalGainAllFundsReportViewer : System.Web.UI.Page
{
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string strFromdate = "";
        //string strTodate = "";
        //string fundCodes = "";
        string strSQLForMainReport, strSQLForSubReport,strSqlForNonlistedDetails, strSQLforTotalShareandTotalCost, strSQLOTCTotalCost, strSQ_DLISTED_TotalCost;
        //string companyCodes = "";
        //string percentageCheck = "";
        string statementType = "";
        string group = "";
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string balDate = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            //strFromdate = (string)Session["FromDate"];
            //strTodate = (string)Session["ToDate"];
            //fundCodes = (string)Session["fundCodes"];
            fundCode = (string)Session["fundCode"];
            group = (string)Session["group"];
            balDate = (string)Session["balDate"];
            statementType = (string)Session["statementType"];
        }
         CommonGateway commonGatewayObj = new CommonGateway();
      //  DataTable dtReprtSource = new DataTable();
        DataTable dtRptSrcMainReport = new DataTable();
        DataTable dtRptSrcSubReport = new DataTable();
        DataTable dtRptSrcSubReport2 = new DataTable();
        DataTable dtRptSrcSubReport3 = new DataTable();
        DataTable dtnonlistedDetails = new DataTable();
        DataTable dtforTotalShareandTotalCost = new DataTable();
        DataTable dtforDLISTED_andTotalCost = new DataTable();
        DataTable dtforOTCandTotalCost = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();

        sbMst.Append("select * from (SELECT     FUND.F_NAME,comp.comp_cd, COMP.COMP_NM,comp.MARKETTYPE,comp.TRADE_METH,comp.flag,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, ");
        sbMst.Append("ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM,");
        sbMst.Append("NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) ");
        sbMst.Append("AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, ");
        sbMst.Append("ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF, " );
        sbMst.Append("ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) " );
        sbMst.Append("AS APPRICIATION_ERROTION, DECODE(PFOLIO_BK.TCST_AFT_COM,0,0,ROUND( (PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) / PFOLIO_BK.TCST_AFT_COM * 100, 8)) AS PERCENT_OF_APRE_EROSION, ");
        sbMst.Append("ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP ");
        sbMst.Append("FROM         PFOLIO_BK INNER JOIN ");
        sbMst.Append("COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append("FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append("WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (FUND.F_CD =" + fundCode + ") ");
        sbMst.Append("ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM ) a where a.MARKETTYPE !='O'  and a.flag ='L'");


        if (string.Compare(statementType, "Profit", true) == 0)
        {
            sbMst.Append(" and RATE_DIFF>0 ");
            if (string.Compare(group, "A", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'Z' ");
            }
        }
        else if (string.Compare(statementType, "Loss", true) == 0)
        {
            sbMst.Append(" and RATE_DIFF<0 ");
            if (string.Compare(group, "A", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'Z' ");
            }
        }
        else if (string.Compare(statementType, "All", true) == 0)
        {
            if (string.Compare(group, "A", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbMst.Append("  and TRADE_METH = 'Z' ");
            }

        }

        sbMst.Append(sbfilter.ToString());
        dtRptSrcMainReport = commonGatewayObj.Select(sbMst.ToString());
        // non listed securities
        string strSQLForSubReport1 = " SELECT * FROM (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION,  DECODE(e.NO_SHARES,0,0,ROUND(ROUND(e.market_rate, 8) - ROUND(e.AMOUNT /e.NO_SHARES, 8), 8))  AS RATE_DIFF  from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
" DECODE(c.AMOUNT,0,0,ROUND( (c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8)) AS PERCENT_OF_APRE_EROSION " +
" from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
" (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES,'I',NO_SHARES)totshare, " +
" decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT,'I',AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
" from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
" where f_cd =  " + fundCode + " and    INV_DATE <= '" + balDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY_DETAILS d " +
" on c.CAT_TP = d.CAT_ID  wHERE C.NO_SHARES>0 order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + balDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c  inner join " +
" NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d where D.NO_SHARES>0 and D.AMOUNT>0 ) e  inner join NONLISTED_CATEGORY_DETAILS f  on e.CAT_ID = f.CAT_ID order by COMP_NM, CAT_ID  )  ";
        if (string.Compare(statementType, "Profit", true) == 0)
        {
            strSQLForSubReport1 = strSQLForSubReport1 + " Where RATE_DIFF>0";
        }
        else if (string.Compare(statementType, "Loss", true) == 0)
        {
            strSQLForSubReport1 = strSQLForSubReport1 + " Where RATE_DIFF<0";
        }

        dtRptSrcSubReport = commonGatewayObj.Select(strSQLForSubReport1);



        StringBuilder sbMstOTC = new StringBuilder();
        StringBuilder sbfilterOTC = new StringBuilder();
        //otc

        sbMstOTC.Append("select * from (SELECT     FUND.F_CD,FUND.F_NAME,COMP.COMP_CD, COMP.COMP_NM,COMP.TRADE_METH,comp.flag, ");
        sbMstOTC.Append(" COMP.MARKETTYPE,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, ");
        sbMstOTC.Append(" PFOLIO_BK.TCST_AFT_COM, NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, ");
        sbMstOTC.Append(" ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF, ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) AS APPRICIATION_ERROTION, ");
        sbMstOTC.Append("   DECODE(PFOLIO_BK.TCST_AFT_COM,0,0,ROUND( (PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) / PFOLIO_BK.TCST_AFT_COM * 100, 8)) AS PERCENT_OF_APRE_EROSION, ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP FROM ");
        sbMstOTC.Append("   PFOLIO_BK INNER JOIN COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN FUND ON PFOLIO_BK.F_CD = FUND.F_CD WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "')");
        sbMstOTC.Append("  AND (FUND.F_CD in(" + fundCode + "))  And comp.MARKETTYPE='O' and comp.flag='L'  ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM  )  ");

        if (string.Compare(statementType, "Profit", true) == 0)
        {
            sbMstOTC.Append(" where RATE_DIFF>0 ");
            if (string.Compare(group, "A", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'Z' ");
            }
        }
        else if (string.Compare(statementType, "Loss", true) == 0)
        {
            sbMstOTC.Append(" where RATE_DIFF<0 ");
            if (string.Compare(group, "A", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbMstOTC.Append("  and TRADE_METH = 'Z' ");
            }
        }
        else if (string.Compare(statementType, "All", true) == 0)
        {
            if (string.Compare(group, "A", true) == 0)
            {
                sbMstOTC.Append("  where TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbMstOTC.Append("  where TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbMstOTC.Append("  where TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbMstOTC.Append("  where TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbMstOTC.Append("  where TRADE_METH = 'Z' ");
            }

        }




        dtRptSrcSubReport2 = commonGatewayObj.Select(sbMstOTC.ToString());

        //D listed
        StringBuilder sbfilterDlisted = new StringBuilder();

        sbfilterDlisted.Append("select * from (SELECT     FUND.F_NAME, comp.comp_cd,COMP.COMP_NM,COMP.TRADE_METH,comp.flag,COMP.MARKETTYPE,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, ");
       sbfilterDlisted.Append("ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM,");
        sbfilterDlisted.Append("NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) ");
        sbfilterDlisted.Append("AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, ");
        sbfilterDlisted.Append("ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF, ");
        sbfilterDlisted.Append("ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) ");
        sbfilterDlisted.Append("AS APPRICIATION_ERROTION, ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) ");
        sbfilterDlisted.Append(" / PFOLIO_BK.TCST_AFT_COM * 100, 8) AS PERCENT_OF_APRE_EROSION, ");
        sbfilterDlisted.Append("ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP ");
        sbfilterDlisted.Append("FROM         PFOLIO_BK INNER JOIN ");
        sbfilterDlisted.Append("COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbfilterDlisted.Append("FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbfilterDlisted.Append("WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (FUND.F_CD =" + fundCode + ") ");
        sbfilterDlisted.Append("ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM ) a where a.flag='D'  ");

        if (string.Compare(statementType, "Profit", true) == 0)
        {
            sbfilterDlisted.Append(" and RATE_DIFF>0 ");
            if (string.Compare(group, "A", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
            }
        }
        else if (string.Compare(statementType, "Loss", true) == 0)
        {
            sbfilterDlisted.Append(" and RATE_DIFF<0 ");
            if (string.Compare(group, "A", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
            }
        }
        else if (string.Compare(statementType, "All", true) == 0)
        {
            if (string.Compare(group, "A", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

            }
            else if (string.Compare(group, "B", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
            }
            else if (string.Compare(group, "G", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
            }
            else if (string.Compare(group, "N", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
            }
            else if (string.Compare(group, "Z", true) == 0)
            {
                sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
            }

        }




        dtRptSrcSubReport3 = commonGatewayObj.Select(sbfilterDlisted.ToString());



        //strSqlForNonlistedDetails = "select sum(totammount) as totalAmmount ,sum(totshare) as totshare from (select decode(TRAN_TP,'B',NO_SHARES,'S',-NO_SHARES)totshare, decode(TRAN_TP,'B',AMOUNT,'S',-AMOUNT)totammount," +
        //                           " F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP  from (Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE," +
        //                           "  CAT_ID, TRAN_TP from  invest.NON_LISTED_SECURITIES_DETAILS where f_cd="+fundCode+" and INV_DATE <='"+balDate+"' order by INV_DATE)) ";
        if (dtRptSrcSubReport.Rows.Count > 0)
        {
                    strSqlForNonlistedDetails = " select sum(NO_SHARES) as totshare,sum(AMOUNT) as totalAmmount ,sum(TOT_MARKET_PRICE)as totalMarketPrice,sum(APPRICIATION_ERROTION) as APPriciation from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION, ROUND(ROUND(e.market_rate, 8) - ROUND(e.AMOUNT /e.NO_SHARES, 8), 8) AS RATE_DIFF from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
        "ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8) AS PERCENT_OF_APRE_EROSION " +
        " from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
        " (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES,'I',NO_SHARES)totshare, " +
        " decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT,'I',AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
        " from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
        " where f_cd =  " + fundCode + " and    INV_DATE <= '" + balDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY_DETAILS d " +
        " on c.CAT_TP = d.CAT_ID where C.NO_SHARES>0  order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + balDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join " +
        " NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join NONLISTED_CATEGORY_DETAILS f  on e.CAT_ID = f.CAT_ID order by CAT_ID ) ";

            if (string.Compare(statementType, "Profit", true) == 0)
            {
                strSqlForNonlistedDetails = strSqlForNonlistedDetails + " Where RATE_DIFF>0";
            }
            else if (string.Compare(statementType, "Loss", true) == 0)
            {
                strSqlForNonlistedDetails = strSqlForNonlistedDetails + " Where RATE_DIFF<0";
            }
            dtnonlistedDetails = commonGatewayObj.Select(strSqlForNonlistedDetails);

        }
           

        if (dtRptSrcMainReport.Rows.Count > 0)
        {

            strSQLforTotalShareandTotalCost = "select sum (TOT_NOS) as totalshare,sum(TCST_AFT_COM) as totalTCST_AFT_COM ,sum(TOT_MARKET_PRICE)" +
            "as totalTOT_MARKET_PRICE , ROUND(sum(APPRICIATION_ERROTION),2) as totalAPPRICIATION_ERROTION from (select * from (SELECT     FUND.F_NAME,comp.comp_cd, COMP.COMP_NM,comp.MARKETTYPE,comp.TRADE_METH,comp.flag,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, " +
       "ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM," +
       "NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) " +
       "AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, " +
       "ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF, " +
       "ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) " +
       "AS APPRICIATION_ERROTION, ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) " +
       " / PFOLIO_BK.TCST_AFT_COM * 100, 8) AS PERCENT_OF_APRE_EROSION, " +
       "ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP " +
       "FROM         PFOLIO_BK INNER JOIN " +
       "COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN " +
       "FUND ON PFOLIO_BK.F_CD = FUND.F_CD " +
       "WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (FUND.F_CD =" + fundCode + ") " +
       "ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM) b where b.MARKETTYPE !='O'  and b.flag ='L') a ";

            if (string.Compare(statementType, "Profit", true) == 0)
            {
                strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " Where RATE_DIFF>0";


                if (string.Compare(group, "A", true) == 0)
                {

                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'Z'";
                }
            }
            else if (string.Compare(statementType, "Loss", true) == 0)
            {
                strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " Where RATE_DIFF<0";

                if (string.Compare(group, "A", true) == 0)
                {

                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " and TRADE_METH = 'Z'";
                }
            }
            else if (string.Compare(statementType, "All", true) == 0)
            {

                if (string.Compare(group, "A", true) == 0)
                {

                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " where TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " where TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " where TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " where TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQLforTotalShareandTotalCost = strSQLforTotalShareandTotalCost + " where TRADE_METH = 'Z'";
                }
            }

            dtforTotalShareandTotalCost = commonGatewayObj.Select(strSQLforTotalShareandTotalCost);
        }
            

        if(dtRptSrcSubReport2.Rows.Count > 0)
        {
             strSQLOTCTotalCost = "select sum (TOT_NOS) as totalshare,sum(TCST_AFT_COM) as totalTCST_AFT_COM ,sum(TOT_MARKET_PRICE)" +
          "as totalTOT_MARKET_PRICE , ROUND(sum(APPRICIATION_ERROTION),2) as totalAPPRICIATION_ERROTION from (select * from (SELECT     FUND.F_NAME,comp.comp_cd, COMP.COMP_NM,comp.MARKETTYPE,comp.TRADE_METH,comp.flag,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, " +
     "ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM," +
     "NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) " +
     "AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, " +
     "ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF, " +
     "ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) " +
     "AS APPRICIATION_ERROTION, ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) " +
     " / PFOLIO_BK.TCST_AFT_COM * 100, 8) AS PERCENT_OF_APRE_EROSION, " +
     "ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP " +
     "FROM         PFOLIO_BK INNER JOIN " +
     "COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN " +
     "FUND ON PFOLIO_BK.F_CD = FUND.F_CD " +
     "WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (FUND.F_CD =" + fundCode + ") " +
     "ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM) b where b.MARKETTYPE ='O' and b.flag ='L') a ";

            if (string.Compare(statementType, "Profit", true) == 0)
            {
                strSQLOTCTotalCost = strSQLOTCTotalCost + " Where RATE_DIFF>0";


                if (string.Compare(group, "A", true) == 0)
                {

                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'Z'";
                }
            }
            else if (string.Compare(statementType, "Loss", true) == 0)
            {
                strSQLOTCTotalCost = strSQLOTCTotalCost + " Where RATE_DIFF<0";

                if (string.Compare(group, "A", true) == 0)
                {

                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " and TRADE_METH = 'Z'";
                }
            }
            else if (string.Compare(statementType, "All", true) == 0)
            {

                if (string.Compare(group, "A", true) == 0)
                {

                    strSQLOTCTotalCost = strSQLOTCTotalCost + " where TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " where TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " where TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " where TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQLOTCTotalCost = strSQLOTCTotalCost + " where TRADE_METH = 'Z'";
                }
            }


            dtforOTCandTotalCost = commonGatewayObj.Select(strSQLOTCTotalCost);

        }
       


        if (dtRptSrcSubReport3.Rows.Count > 0)
        {
                    strSQ_DLISTED_TotalCost = "select sum (TOT_NOS) as totalshare,sum(TCST_AFT_COM) as totalTCST_AFT_COM ,sum(TOT_MARKET_PRICE)" +
            "as totalTOT_MARKET_PRICE , ROUND(sum(APPRICIATION_ERROTION),2) as totalAPPRICIATION_ERROTION from (select * from (SELECT     FUND.F_NAME,comp.comp_cd, COMP.COMP_NM,comp.MARKETTYPE,comp.TRADE_METH,comp.flag,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, " +
            "ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM," +
            "NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) " +
            "AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, " +
            "ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF, " +
            "ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) " +
            "AS APPRICIATION_ERROTION, ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) " +
            " / PFOLIO_BK.TCST_AFT_COM * 100, 8) AS PERCENT_OF_APRE_EROSION, " +
            "ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP " +
            "FROM         PFOLIO_BK INNER JOIN " +
            "COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN " +
            "FUND ON PFOLIO_BK.F_CD = FUND.F_CD " +
            "WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (FUND.F_CD =" + fundCode + ") " +
            "ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM) b where b.flag ='D') a ";

            if (string.Compare(statementType, "Profit", true) == 0)
            {
                strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " Where RATE_DIFF>0";


                if (string.Compare(group, "A", true) == 0)
                {

                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'Z'";
                }
            }
            else if (string.Compare(statementType, "Loss", true) == 0)
            {
                strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " Where RATE_DIFF<0";

                if (string.Compare(group, "A", true) == 0)
                {

                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " and TRADE_METH = 'Z'";
                }
            }
            else if (string.Compare(statementType, "All", true) == 0)
            {

                if (string.Compare(group, "A", true) == 0)
                {

                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " where TRADE_METH = 'A'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'A' ");

                }
                else if (string.Compare(group, "B", true) == 0)
                {
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " where TRADE_METH = 'B'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'B' ");
                }
                else if (string.Compare(group, "G", true) == 0)
                {
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " where TRADE_METH = 'G'";
                    //sbfilterDlisted.Append("  and TRADE_METH = 'G' ");
                }
                else if (string.Compare(group, "N", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'N' ");
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " where TRADE_METH = 'N'";
                }
                else if (string.Compare(group, "Z", true) == 0)
                {
                    // sbfilterDlisted.Append("  and TRADE_METH = 'Z' ");
                    strSQ_DLISTED_TotalCost = strSQ_DLISTED_TotalCost + " where TRADE_METH = 'Z'";
                }
            }


            dtforDLISTED_andTotalCost = commonGatewayObj.Select(strSQ_DLISTED_TotalCost);

        }





        Decimal nonlistedtotalAmmount = 0;
        Decimal nonlistedtotshare = 0;
        Decimal nonMarketPrice = 0;
        Decimal nonErroision = 0;
        Decimal portfiliototalCostAfterCommissionAmmount = 0;
        Decimal portfiliototshare = 0;
        Decimal portfiliototalMarketPrice = 0;
        Decimal portfoliototalErroision = 0;
        Decimal portfiliototal_OTC_CostAfterCommissionAmmount = 0;
        Decimal portfilio_OTC_totshare = 0;
        Decimal portfiliototal_OTC_MarketPrice = 0;
        Decimal portfoliototal_OTC_Erroision = 0;
        Decimal portfiliototal_DLISTED_CostAfterCommissionAmmount = 0;
        Decimal portfilio_DLISTED_totshare = 0;
        Decimal portfiliototal_DLISTED_MarketPrice = 0;
        Decimal portfoliototal_DLISTED_Erroision = 0;
        if (dtRptSrcSubReport.Rows.Count > 0)
        {
            nonlistedtotalAmmount = Convert.ToDecimal(dtnonlistedDetails.Rows[0]["totalAmmount"]);
            nonlistedtotshare = Convert.ToDecimal(dtnonlistedDetails.Rows[0]["totshare"]);
            nonMarketPrice = Convert.ToDecimal(dtnonlistedDetails.Rows[0]["totalMarketPrice"]);
            nonErroision = Convert.ToDecimal(dtnonlistedDetails.Rows[0]["APPriciation"]);
        }
        if (dtforTotalShareandTotalCost.Rows.Count > 0)
        {
            portfiliototalCostAfterCommissionAmmount = Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalTCST_AFT_COM"]);
            portfiliototshare = Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalshare"]);
            portfiliototalMarketPrice= Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalTOT_MARKET_PRICE"]);
            portfoliototalErroision = Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalAPPRICIATION_ERROTION"]);

        }


        if (dtforOTCandTotalCost.Rows.Count > 0)
        {
            portfiliototal_OTC_CostAfterCommissionAmmount = Convert.ToDecimal(dtforOTCandTotalCost.Rows[0]["totalTCST_AFT_COM"]);
            portfilio_OTC_totshare = Convert.ToDecimal(dtforOTCandTotalCost.Rows[0]["totalshare"]);
            portfiliototal_OTC_MarketPrice = Convert.ToDecimal(dtforOTCandTotalCost.Rows[0]["totalTOT_MARKET_PRICE"]);
            portfoliototal_OTC_Erroision = Convert.ToDecimal(dtforOTCandTotalCost.Rows[0]["totalAPPRICIATION_ERROTION"]);

        }

        if (dtforDLISTED_andTotalCost.Rows.Count > 0)
        {
            portfiliototal_DLISTED_CostAfterCommissionAmmount = Convert.ToDecimal(dtforDLISTED_andTotalCost.Rows[0]["totalTCST_AFT_COM"]);
            portfilio_DLISTED_totshare = Convert.ToDecimal(dtforDLISTED_andTotalCost.Rows[0]["totalshare"]);
            portfiliototal_DLISTED_MarketPrice = Convert.ToDecimal(dtforDLISTED_andTotalCost.Rows[0]["totalTOT_MARKET_PRICE"]);
            portfoliototal_DLISTED_Erroision = Convert.ToDecimal(dtforDLISTED_andTotalCost.Rows[0]["totalAPPRICIATION_ERROTION"]);

        }

        Decimal portfilio_final_CostAfterCommissionAmmount = 0;
        Decimal portfilio_final_totshare = 0;
        Decimal portfilio_final_MarketPrice = 0;
        Decimal portfolio_final_Erroision = 0;
      

        //Decimal portfiliototal_final_MarketPrice = 0;
        //Decimal portfoliototal_final_Erroision = 0;

        portfilio_final_CostAfterCommissionAmmount = portfiliototalCostAfterCommissionAmmount + portfiliototal_OTC_CostAfterCommissionAmmount + portfiliototal_DLISTED_CostAfterCommissionAmmount+ nonlistedtotalAmmount;
        portfilio_final_totshare = nonlistedtotshare + portfiliototshare + portfilio_OTC_totshare + portfilio_DLISTED_totshare;
        portfilio_final_MarketPrice = nonMarketPrice + portfiliototalMarketPrice + portfiliototal_OTC_MarketPrice + portfiliototal_DLISTED_MarketPrice;
        portfolio_final_Erroision = nonErroision + portfoliototalErroision + portfoliototal_OTC_Erroision + portfoliototal_DLISTED_Erroision;
        //portfolio_Grand_total_percent_invest=

     


        DataSet ds = new DataSet();
        //ds.Tables.Add(dtRptSrcMainReport);
        //ds.Tables.Add(dtRptSrcSubReport);



        //        Dim dt1 As DataTable
        //        Dim dt2 As DataTable

        //        dt1 = UnityDataRow()
        //        dt2 = UnityDataRow()

        //dt1.TableName = "Level1"
        //dtRptSrcMainReport.TableName = "dtCopyForMainRpt";
        //dtRptSrcSubReport.TableName = "dtCopyForSubRpt";
        //dt2.TableName = "Level2"

        //HierDS.Tables.Add(dt1) '' no need to write copy method
        //       HierDS.Tables.Add(dt2)

        DataTable dtCopyForMainRpt = new DataTable();
        DataTable dtCopyForSubRpt = new DataTable();
        DataTable dtCopyForSubRpt2 = new DataTable();
        DataTable dtCopyForSubRpt3 = new DataTable();
        dtRptSrcMainReport.TableName = "dtCopyForMainPortfolioRpt_details";
        dtRptSrcSubReport.TableName = "dtCopyForSubNonlistedRpt_dtails";
        dtRptSrcSubReport2.TableName = "dtCopyForSubOTCRpt_dtails";
        dtRptSrcSubReport3.TableName = "dtCopyForSubD_listedRpt_dtails";

        ds.Tables.Add(dtRptSrcMainReport.Copy());
        ds.Tables.Add(dtRptSrcSubReport.Copy());
        ds.Tables.Add(dtRptSrcSubReport2.Copy());
        ds.Tables.Add(dtRptSrcSubReport3.Copy());

        


        // if (dtRptSrcMainReport.Rows.Count>0  && dtRptSrcSubReport.Rows.Count > 0 )
        if (dtRptSrcMainReport.Rows.Count > 0 )
        {
            Decimal totalInvest = 0;
            for (int loop = 0; loop < dtRptSrcMainReport.Rows.Count; loop++)
            {
                totalInvest = totalInvest + Convert.ToDecimal(dtRptSrcMainReport.Rows[loop]["TCST_AFT_COM"]);
            }
            string Path = Server.MapPath("Report/crtPortfolioNonListedAndOTCandDlistedDetailsReport.rpt");
            rdoc.Load(Path);
            //ds.Tables[0].Merge(dtRptSrcMainReport);
            //ds.Tables[0].Merge(dtRptSrcSubReport);
            // rdoc.SetDataSource(dtReprtSource);
            rdoc.SetDataSource(ds);





            CRV_protfoliowithNonlisted.ReportSource = rdoc;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("prmTotalInvest", portfilio_final_CostAfterCommissionAmmount);
            rdoc.SetParameterValue("prmTotalInvestOTC", portfilio_final_CostAfterCommissionAmmount);
            rdoc.SetParameterValue("prmTotalInvestDListed", portfilio_final_CostAfterCommissionAmmount);
            rdoc.SetParameterValue("prmTotalInvestNONListed", portfilio_final_CostAfterCommissionAmmount);

            rdoc.SetParameterValue("portfilio_final_CostAfterCommissionAmmount", portfilio_final_CostAfterCommissionAmmount);
            rdoc.SetParameterValue("portfilio_final_totshare", portfilio_final_totshare);
             rdoc.SetParameterValue("portfilio_final_MarketPrice", portfilio_final_MarketPrice);
            rdoc.SetParameterValue("portfolio_final_Erroision", portfolio_final_Erroision);



            rdoc = ReportFactory.GetReport(rdoc.GetType());


        }
        else
        {
            Response.Write("No Data Found");
        }

        //if (dtReprtSource1.Rows.Count > 0)
        //{
        //    string Path = Server.MapPath("Report/testFUND_TRANS_HB.rpt");
        //    rdoc.Load(Path);
        //    rdoc.SetDataSource(dtReprtSource1);

        //    CrystalReportViewer1.ReportSource = rdoc;

        //    rdoc = ReportFactory.GetReport(rdoc.GetType());
        //}
        //else
        //{
        //    Response.Write("No Data Found");
        //}

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_protfoliowithNonlisted.Dispose();
        CRV_protfoliowithNonlisted = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}