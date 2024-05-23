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
using CrystalDecisions.CrystalReports.Engine;

public partial class UI_ReportViewer_PortfolioWithNonListedReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1Obj = new Pf1s1DAO();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string quaterEndDate = "";
        string prevQuaterEndDate = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCodes"];
            quaterEndDate = (string)Session["quaterEndDate"];
            prevQuaterEndDate = (string)Session["PrevQuaterEnddate"];
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append(" select nvl(quarterend.f_name,prevquarterend.f_name) as f_name, nvl(quarterend.COMP_NM,prevquarterend.COMP_nm) as COMP_NM ,nvl(quarterend.SECT_MAJ_NM,prevquarterend.SECT_MAJ_NM) as SECT_MAJ_NM,nvl(quarterend.SECT_MAJ_CD,prevquarterend.SECT_MAJ_CD) as SECT_MAJ_CD,nvl(quarterend.TOT_NOS,0) as TOT_NOS,nvl(quarterend.TOT_MARKET_PRICE,0)as TOT_MARKET_PRICE,nvl(quarterend.TCST_AFT_COM,0) as TCST_AFT_COM,");
        sbMst.Append(" nvl(quarterend.APPRICIATION_ERROTION,0) as APPRICIATION_ERROTION,prevquarterend.f_name as prevfname,prevquarterend.COMP_nm as prevcomp,prevquarterend.SECT_MAJ_NM as prevSECT_MAJ_NM ,prevquarterend.SECT_MAJ_CD as prevSECT_MAJ_CD, ");
        sbMst.Append(" prevquarterend.TOT_NOS as prevTOT_NOS,prevquarterend.TOT_MARKET_PRICE as prevTOT_MARKET_PRICE ,prevquarterend.TCST_AFT_COM as prevTCST_AFT_COM, prevquarterend.PERCENT_OF_APRE_EROSION prevAPPRICIATION_ERROTION ");
        sbMst.Append(" from (SELECT     FUND.f_cd, FUND.F_NAME, COMP.COMP_NM, COMP.COMP_cd, PFOLIO_BK.SECT_MAJ_NM, PFOLIO_BK.SECT_MAJ_CD, ");
        sbMst.Append(" TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, ");
        sbMst.Append(" PFOLIO_BK.TCST_AFT_COM, NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 2) AS AVG_RATE, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 2) AS TOT_MARKET_PRICE, ");
        sbMst.Append("  ROUND(ROUND(PFOLIO_BK.ADC_RT, 2) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF,  ");
        sbMst.Append("  ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 2) - PFOLIO_BK.TCST_AFT_COM, 2) AS APPRICIATION_ERROTION, ");
        sbMst.Append("  ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM)  / PFOLIO_BK.TCST_AFT_COM * 100, 2) AS PERCENT_OF_APRE_EROSION, ");
        sbMst.Append("  ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 2) AS PERCENTAGE_OF_PAIDUP FROM  ");
        sbMst.Append("  PFOLIO_BK INNER JOIN COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD ");
        sbMst.Append(" INNER JOIN FUND ON PFOLIO_BK.F_CD = FUND.F_CD WHERE     (PFOLIO_BK.BAL_DT_CTRL = '"+ quaterEndDate + "') AND (FUND.F_CD ="+fundCode+") ORDER BY ");
        sbMst.Append(" PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM)quarterend full outer join ");
        sbMst.Append(" ( SELECT    FUND.f_cd, FUND.F_NAME, COMP.COMP_NM,  COMP.COMP_cd,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, ");
        sbMst.Append(" TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, ");
        sbMst.Append("  PFOLIO_BK.TCST_AFT_COM, NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 2) AS AVG_RATE, ");
        sbMst.Append("  ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 2) AS TOT_MARKET_PRICE, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.ADC_RT, 2) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF, ");
        sbMst.Append(" ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM)  / PFOLIO_BK.TCST_AFT_COM * 100, 2) AS PERCENT_OF_APRE_EROSION, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 2) AS PERCENTAGE_OF_PAIDUP FROM  ");
        sbMst.Append("  PFOLIO_BK INNER JOIN COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD ");
        sbMst.Append(" INNER JOIN FUND ON PFOLIO_BK.F_CD = FUND.F_CD WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + prevQuaterEndDate + "') AND (FUND.F_CD =" + fundCode + ") ORDER BY ");
        sbMst.Append(" PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM )prevquarterend on quarterend.f_cd=prevquarterend.f_cd ");
        sbMst.Append(" and quarterend.comp_Cd=prevquarterend.comp_Cd ");
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        //DataTable dtNonlistedSecritiesQuarterEnddate = new DataTable();
        //sbMst = new StringBuilder();
        //sbMst.Append("SELECT      INV_AMOUNT AS COST_PRICE, INV_AMOUNT AS MARKET_PRICE ");
        //sbMst.Append("FROM         NON_LISTED_SECURITIES ");
        //sbMst.Append("WHERE     (F_CD = " + fundCode + ") AND (INV_DATE = ");
        //sbMst.Append(" (SELECT     MAX(INV_DATE) AS EXPR1 ");
        //sbMst.Append("FROM          NON_LISTED_SECURITIES NON_LISTED_SECURITIES_1 ");
        //sbMst.Append("WHERE     (F_CD = " + fundCode + ") AND (INV_DATE <= '" + quaterEndDate + "'))) ");
        //dtNonlistedSecritiesQuarterEnddate = commonGatewayObj.Select(sbMst.ToString());


        DataTable dtNonlistedSecritiesQuarterEnddate = new DataTable();

        string strSqlForNonlistedDetailsQuarterEnddate = " select sum(NO_SHARES) as totshare,sum(AMOUNT) as totalAmmount ,sum(TOT_MARKET_PRICE)as totalMarketPrice,sum(APPRICIATION_ERROTION) as APPriciation from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION, ROUND(ROUND(e.market_rate, 8) - ROUND(e.AMOUNT /e.NO_SHARES, 8), 8) AS RATE_DIFF from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
        "ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8) AS PERCENT_OF_APRE_EROSION " +
        " from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
        " (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, " +
        " decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
        " from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
        " where f_cd =  " + fundCode + " and    INV_DATE <= '" + quaterEndDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY_DETAILS d " +
        " on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + quaterEndDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join " +
        " NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join NONLISTED_CATEGORY_DETAILS f  on e.CAT_ID = f.CAT_ID order by CAT_ID ) ";


        // string strSqlForNonlistedDetailsQuarterEnddate = " select sum(NO_SHARES) as totshare,sum(AMOUNT) as totalAmmount ,sum(TOT_MARKET_PRICE)as totalMarketPrice,sum(APPRICIATION_ERROTION) as APPriciation,sum(PERCENT_OF_APRE_EROSION) as totalPERCENT_OF_APRE_EROSION from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION, ROUND(ROUND(e.market_rate, 8) - ROUND(e.AMOUNT /e.NO_SHARES, 8), 8) AS RATE_DIFF from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
        //"  DECODE(c.AMOUNT,0,0,ROUND( (c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8)) AS PERCENT_OF_APRE_EROSION   " +
        //" from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
        //" (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, " +
        //" decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
        //" from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
        //" where f_cd =  " + fundCode + " and    INV_DATE <= '" + quaterEndDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY_DETAILS d " +
        //" on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + quaterEndDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join " +
        //" NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join NONLISTED_CATEGORY_DETAILS f  on e.CAT_ID = f.CAT_ID order by CAT_ID ) ";


        dtNonlistedSecritiesQuarterEnddate = commonGatewayObj.Select(strSqlForNonlistedDetailsQuarterEnddate.ToString());

        Decimal nonlistedCostPriceQuarterEnddate = 0;
        Decimal nonlistedMarketPriceQuarterEnddate = 0;
        if (dtNonlistedSecritiesQuarterEnddate.Rows.Count > 0)
        {

            if (!string.IsNullOrEmpty(dtNonlistedSecritiesQuarterEnddate.Rows[0]["totalAmmount"].ToString()))
            {
                nonlistedCostPriceQuarterEnddate = Convert.ToDecimal(dtNonlistedSecritiesQuarterEnddate.Rows[0]["totalAmmount"]);
            }

            if (!string.IsNullOrEmpty(dtNonlistedSecritiesQuarterEnddate.Rows[0]["totalMarketPrice"].ToString()))
            {
                nonlistedMarketPriceQuarterEnddate = Convert.ToDecimal(dtNonlistedSecritiesQuarterEnddate.Rows[0]["totalMarketPrice"]);
            }   
                   
        }

        
        //if (dtNonlistedSecritiesQuarterEnddate.Rows.Count > 0)
        //{
        //    nonlistedCostPriceQuarterEnddate = Convert.ToDecimal(dtNonlistedSecritiesQuarterEnddate.Rows[0][0]);
        //    nonlistedMarketPriceQuarterEnddate = Convert.ToDecimal(dtNonlistedSecritiesQuarterEnddate.Rows[0][0]);
        //}

        ////DataTable dtNonlistedSecritiesprevQuaterEndDate = new DataTable();
        //sbMst = new StringBuilder();
        //sbMst.Append("SELECT      INV_AMOUNT AS COST_PRICE, INV_AMOUNT AS MARKET_PRICE ");
        //sbMst.Append("FROM         NON_LISTED_SECURITIES ");
        //sbMst.Append("WHERE     (F_CD = " + fundCode + ") AND (INV_DATE = ");
        //sbMst.Append(" (SELECT     MAX(INV_DATE) AS EXPR1 ");
        //sbMst.Append("FROM          NON_LISTED_SECURITIES NON_LISTED_SECURITIES_1 ");
        //sbMst.Append("WHERE     (F_CD = " + fundCode + ") AND (INV_DATE <= '" + prevQuaterEndDate + "'))) ");
        //dtNonlistedSecritiesQuarterEnddate = commonGatewayObj.Select(sbMst.ToString());


        DataTable dtNonlistedSecritiesprevQuaterEndDate = new DataTable();

        // string strSqlForNonlistedDetailsprevQuaterEndDate = " select sum(NO_SHARES) as totshare,sum(AMOUNT) as totalAmmount ,sum(TOT_MARKET_PRICE)as totalMarketPrice,sum(APPRICIATION_ERROTION) as APPriciation,sum(PERCENT_OF_APRE_EROSION) as totalPERCENT_OF_APRE_EROSION from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION, ROUND(ROUND(e.market_rate, 8) - ROUND(e.AMOUNT /e.NO_SHARES, 8), 8) AS RATE_DIFF from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
        //"  DECODE(c.AMOUNT,0,0,ROUND( (c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8)) AS PERCENT_OF_APRE_EROSION   " +
        //" from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
        //" (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, " +
        //" decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
        //" from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
        //" where f_cd =  " + fundCode + " and    INV_DATE <= '" + prevQuaterEndDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY_DETAILS d " +
        //" on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + prevQuaterEndDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join " +
        //" NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join NONLISTED_CATEGORY_DETAILS f  on e.CAT_ID = f.CAT_ID order by CAT_ID ) ";

        string strSqlForNonlistedDetailsprevQuaterEndDate = " select sum(NO_SHARES) as totshare,sum(AMOUNT) as totalAmmount ,sum(TOT_MARKET_PRICE)as totalMarketPrice,sum(APPRICIATION_ERROTION) as APPriciation from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION, ROUND(ROUND(e.market_rate, 8) - ROUND(e.AMOUNT /e.NO_SHARES, 8), 8) AS RATE_DIFF from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
"ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8) AS PERCENT_OF_APRE_EROSION " +
" from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
" (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, " +
" decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
" from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
" where f_cd =  " + fundCode + " and    INV_DATE <= '" + prevQuaterEndDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY_DETAILS d " +
" on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + prevQuaterEndDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join " +
" NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join NONLISTED_CATEGORY_DETAILS f  on e.CAT_ID = f.CAT_ID order by CAT_ID ) ";


        dtNonlistedSecritiesprevQuaterEndDate = commonGatewayObj.Select(strSqlForNonlistedDetailsprevQuaterEndDate.ToString());

        Decimal nonlistedCostPriceprevQuaterEndDate = 0;
        Decimal nonlistedMarketPriceprevQuaterEndDate = 0;
        if (dtNonlistedSecritiesQuarterEnddate.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dtNonlistedSecritiesprevQuaterEndDate.Rows[0]["totalAmmount"].ToString()))
            {
                nonlistedCostPriceprevQuaterEndDate = Convert.ToDecimal(dtNonlistedSecritiesprevQuaterEndDate.Rows[0]["totalAmmount"]);
            }

            if (!string.IsNullOrEmpty(dtNonlistedSecritiesprevQuaterEndDate.Rows[0]["totalMarketPrice"].ToString()))
            {
                nonlistedMarketPriceprevQuaterEndDate = Convert.ToDecimal(dtNonlistedSecritiesprevQuaterEndDate.Rows[0]["totalMarketPrice"]);
            } 
            
        }



        if (dtReprtSource.Rows.Count > 0)
        {
          
            dtReprtSource.TableName = "PortfolioQuarterlyReport";
           // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_PortfolioQuarterlyReport.xsd");
            ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtPortfolioQuaterlyReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_PfolioWithNonListed.ReportSource = rdoc;
            CRV_PfolioWithNonListed.DisplayToolbar = true;
            CRV_PfolioWithNonListed.HasExportButton = true;
            CRV_PfolioWithNonListed.HasPrintButton = true;
            rdoc.SetParameterValue("prmbalDate", quaterEndDate);
            rdoc.SetParameterValue("prmbalDate2", prevQuaterEndDate);
            //rdoc.SetParameterValue("prmTotalInvest", totalInvest);
            rdoc.SetParameterValue("prmNonlistedCostPriceQuarterEnddate", nonlistedCostPriceQuarterEnddate);
            rdoc.SetParameterValue("prmNonlisteMarketPriceQuarterEnddate", nonlistedMarketPriceQuarterEnddate);
            rdoc.SetParameterValue("prmNonlistedCostPriceprevQuaterEndDate", nonlistedCostPriceprevQuaterEndDate);
            rdoc.SetParameterValue("prmNonlisteMarketPriceprevQuaterEndDate", nonlistedMarketPriceprevQuaterEndDate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_PfolioWithNonListed.Dispose();
        CRV_PfolioWithNonListed = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
