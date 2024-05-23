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
using CrystalDecisions.CrystalReports.Engine;
using System.Text;

public partial class UI_ReportViewer_MarketValuationWithNonListedSecuritesCompanyAndAllFundsReportViewer : Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    string strSQL;
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string strPortfolioAsOnDate = "";
        string fundCodes = "";
        //string companyCodes = "";
        //string percentageCheck = "";
       
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            strPortfolioAsOnDate = (string)Session["PortfolioAsOnDate"];
            fundCodes = (string)Session["fundCodes"];
            
        }
        
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        //strSQL = "select u.f_cd, u.fund_name,u.bal_dt_ctrl,u.TOTAL_COMPANY_PROFIT,u.TOTAL_SHARE_PROFIT,u.TOTAL_COST_PROFIT,u.TOTAL_MARKET_PRICE_PROFIT,u.PROFIT,u.tp_PROFIT," +
        //         " v.TOTAL_COMPANY_LOSS,v.TOTAL_SHARE_LOSS,v.TOTAL_COST_LOSS,v.TOTAL_MARKET_PRICE_LOSS,v.LOSS,v.tp_LOSS from "+
        //          "(select p.f_cd , f.f_name fund_name,TO_CHAR(bal_dt_ctrl,'dd-MON-yyyy')bal_dt_ctrl,COUNT(p.COMP_CD)TOTAL_COMPANY_PROFIT, sum(trunc(p.tot_nos)) TOTAL_SHARE_PROFIT," +
        //         "sum(p.tcst_aft_com) TOTAL_COST_PROFIT, sum(p.tot_nos * c.adc_rt) TOTAL_MARKET_PRICE_PROFIT, sum(p.tot_nos * c.adc_rt) - sum(p.tcst_aft_com)PROFIT," +
        //         "decode(sum(trunc(p.tot_nos)), 0, 'noneed', 'PROFIT') tp_PROFIT from pfolio_bk p, comp c, fund f " +
        //         " where p.bal_dt_ctrl ='" + strPortfolioAsOnDate.ToString() + "' and f.F_CD IN(" + fundCodes + ") and f.f_cd not in(3,5,18) and p.comp_cd = c.comp_cd " +
        //         " and(round(p.adc_rt, 2) - trunc(p.tcst_aft_com / tot_nos, 2)) * trunc(tot_nos) >= 0 and p.f_cd = f.f_cd " +
        //         " group by p.f_cd, bal_dt_ctrl,f.f_name)u," +
        //         " (select p.f_cd ,f.f_name,TO_CHAR(bal_dt_ctrl,'dd-MON-yyyy')bal_dt_ctrl,COUNT(p.COMP_CD) TOTAL_COMPANY_LOSS, sum(trunc(p.tot_nos)) TOTAL_SHARE_LOSS, sum(p.tcst_aft_com) TOTAL_COST_LOSS," +
        //         " sum(p.tot_nos * c.adc_rt) TOTAL_MARKET_PRICE_LOSS, sum(p.tot_nos * c.adc_rt) - sum(p.tcst_aft_com) LOSS," +
        //         " decode(sum(trunc(p.tot_nos)), 0, 'noneed', 'LOSS') tp_LOSS" +
        //         " from  pfolio_bk p, comp c ,fund f where p.bal_dt_ctrl ='" + strPortfolioAsOnDate.ToString() + "' and f.F_CD IN(" + fundCodes + ") and f.f_cd not in(3,5,18)" +
        //         " and p.comp_cd = c.comp_cd and(round(p.adc_rt, 2) - trunc(p.tcst_aft_com / tot_nos, 2)) * trunc(tot_nos) < 0 " +
        //         " and p.f_cd = f.f_cd group by p.f_cd, bal_dt_ctrl, f.f_name)v"+
        //         " where u.f_cd=v.f_cd order by u.f_cd";




/*    select f_cd, inv_amount, inv_date
FROM(SELECT f_cd, inv_amount,
           inv_date,
           rank() over(partition by f_cd order by inv_date desc) rnk
      FROM NON_LISTED_SECURITIES)
WHERE rnk = 1  
*/

/* Commented on 10 April,2019
strSQL = "select n.f_cd ,m.f_name,m.bal_dt_ctrl,m.TOTAL_COMPANY, m.TOTAL_SHARE, m.TOTAL_COST,m.TOTAL_MARKET_PRICE,m.EROSION,n.inv_amount,n.inv_date,(m.TOTAL_COST+n.inv_amount)TOT_COST_nl," +
         "m.TOTAL_MARKET_PRICE+n.inv_amount,n.inv_amount as non_list from" +
         "(select p.f_cd ,f.f_name,TO_CHAR(p.bal_dt_ctrl,'dd-MON-yyyy')bal_dt_ctrl,COUNT(nvl(p.COMP_CD,0)) TOTAL_COMPANY, sum(trunc(nvl(p.tot_nos,0))) TOTAL_SHARE, sum(nvl(p.tcst_aft_com,0)) TOTAL_COST," +
          "sum(nvl(p.tot_nos,0) * nvl(p.adc_rt,0)) TOTAL_MARKET_PRICE, sum(nvl(p.tot_nos,0) * nvl(p.adc_rt,0)) - sum(nvl(p.tcst_aft_com,0)) EROSION" +
         " from  pfolio_bk p ,fund f where p.bal_dt_ctrl ='" + strPortfolioAsOnDate.ToString() + "' and f.F_CD IN(" + fundCodes + ") " +
         " and p.f_cd = f.f_cd group by p.f_cd, bal_dt_ctrl, f.f_name)m,(select f_cd,inv_amount,inv_date FROM (SELECT f_cd,inv_amount, inv_date," +
         "rank() over (partition by f_cd order by inv_date desc) rnk" +
         " FROM NON_LISTED_SECURITIES where  inv_amount>0 and F_CD IN(" + fundCodes + ") and inv_date<='" + strPortfolioAsOnDate.ToString() + "' )" +
         " WHERE rnk = 1)n where m.f_cd(+)=n.f_cd order by n.f_cd";
end of commented on  10 April, 2019*/
/* Commented on 10 15-Jul-20201*/
       //strSQL = "select n.f_cd ,m.f_name,m.bal_dt_ctrl,m.TOTAL_COMPANY, m.TOTAL_SHARE, m.TOTAL_COST,m.TOTAL_MARKET_PRICE,m.EROSION,n.inv_amount,n.inv_date,(m.TOTAL_COST+n.inv_amount)TOT_COST_nl," +
       //         "m.TOTAL_MARKET_PRICE+n.inv_amount,n.inv_amount as non_list from" +
       //         "(select p.f_cd ,f.f_name,TO_CHAR(p.bal_dt_ctrl,'dd-MON-yyyy')bal_dt_ctrl,COUNT(nvl(p.COMP_CD,0)) TOTAL_COMPANY, sum(trunc(nvl(p.tot_nos,0))) TOTAL_SHARE, sum(nvl(p.tcst_aft_com,0)) TOTAL_COST," +
       //          "sum(nvl(p.tot_nos,0) * nvl(p.adc_rt,0)) TOTAL_MARKET_PRICE, sum(nvl(p.tot_nos,0) * nvl(p.adc_rt,0)) - sum(nvl(p.tcst_aft_com,0)) EROSION" +
       //         " from  pfolio_bk p ,fund f where p.bal_dt_ctrl ='" + strPortfolioAsOnDate.ToString() + "' and f.F_CD IN(" + fundCodes + ") " +
       //         " and p.f_cd = f.f_cd group by p.f_cd, bal_dt_ctrl, f.f_name)m,(select f_cd,inv_amount,inv_date FROM (SELECT f_cd,inv_amount, inv_date," +
       //         "rank() over (partition by f_cd order by inv_date desc) rnk" +
       //         " FROM NON_LISTED_SECURITIES where F_CD IN(" + fundCodes + ") and inv_date<='" + strPortfolioAsOnDate.ToString() + "' )" +
       //         " WHERE rnk = 1)n where m.f_cd(+)=n.f_cd order by n.f_cd";

        strSQL = " select m.F_CD, m.F_NAME,m.BAL_DT_CTRL,m.TOTAL_COMPANY, m.TOTAL_SHARE, m.TOTAL_COST, " +
                " m.TOTAL_MARKET_PRICE,m.EROSION  from(select p.f_cd ,f.f_name,TO_CHAR(p.bal_dt_ctrl,'dd-MON-yyyy') bal_dt_ctrl," +
                " COUNT(nvl(p.COMP_CD,0)) TOTAL_COMPANY, sum(trunc(nvl(p.tot_nos,0))) TOTAL_SHARE, sum(nvl(p.tcst_aft_com,0))" +
                " TOTAL_COST,sum(nvl(p.tot_nos,0) * nvl(p.adc_rt,0)) TOTAL_MARKET_PRICE, sum(nvl(p.tot_nos,0) * nvl(p.adc_rt,0)) - sum(nvl(p.tcst_aft_com,0)) " +
                " EROSION from  pfolio_bk p ,fund f where p.bal_dt_ctrl ='" + strPortfolioAsOnDate.ToString() + "' and f.F_CD IN(" + fundCodes + ") " +
                " and p.f_cd = f.f_cd group by p.f_cd, bal_dt_ctrl, f.f_name)m order by m.F_CD";

       DataTable dtPortfolio= commonGatewayObj.Select(strSQL);
       DataTable dtReprtSource = dtPortfolio.Clone();
        dtReprtSource.Columns.Add("NON_LISTED_TOT_SEC", typeof(Decimal));
        dtReprtSource.Columns.Add("NON_LISTED_TOT_NO_SHR", typeof(Decimal));
        dtReprtSource.Columns.Add("NON_LISTED_AMT", typeof(Decimal));
       dtReprtSource.Columns.Add("NON_LISTED_MR_PR", typeof(Decimal));
        dtReprtSource.Columns.Add("NON_LISTED_EROSION", typeof(Decimal));
        dtReprtSource.Columns.Add("TOTAL_INVEST_MARKET", typeof(Decimal));
       dtReprtSource.Columns.Add("TOTAL_INVEST_COST", typeof(Decimal));

       DataRow drReprtSource;

       if (dtPortfolio.Rows.Count > 0)
       {
           for (int looper = 0; looper < dtPortfolio.Rows.Count; looper++)
           {
               drReprtSource = dtReprtSource.NewRow();
               drReprtSource["F_CD"] = dtPortfolio.Rows[looper]["F_CD"];
               drReprtSource["F_NAME"] = dtPortfolio.Rows[looper]["F_NAME"];
               drReprtSource["BAL_DT_CTRL"] = dtPortfolio.Rows[looper]["BAL_DT_CTRL"];
               drReprtSource["TOTAL_COMPANY"] = dtPortfolio.Rows[looper]["TOTAL_COMPANY"];
               drReprtSource["TOTAL_SHARE"] = dtPortfolio.Rows[looper]["TOTAL_SHARE"];
               drReprtSource["TOTAL_COST"] = dtPortfolio.Rows[looper]["TOTAL_COST"];
               drReprtSource["TOTAL_MARKET_PRICE"] = dtPortfolio.Rows[looper]["TOTAL_MARKET_PRICE"];
               drReprtSource["EROSION"] = dtPortfolio.Rows[looper]["EROSION"];

                string queryStringnolisted = " select count (NO_SHARES) as total_securities,sum(NO_SHARES) as total_no_share,sum(AMOUNT) as total_cost_Price ,sum(TOT_MARKET_PRICE) as total_market_price,sum(APPRICIATION_ERROTION) as total_appriciation from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
                " DECODE(c.AMOUNT,0,0,ROUND( (c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8))   AS PERCENT_OF_APRE_EROSION " +
                " from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
                " (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES,'I',NO_SHARES)totshare, " +
                " decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT,'I',AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
                " from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
                " where f_cd IN( " + Convert.ToInt16(dtPortfolio.Rows[looper]["F_CD"]) + ") and    INV_DATE <= '" + strPortfolioAsOnDate.ToString() + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d " +
                " on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + strPortfolioAsOnDate.ToString() + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c inner join " +
                " NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID order by CAT_ID)  ";

                DataTable dtNonListed_securities = commonGatewayObj.Select(queryStringnolisted);

                string queryString = " select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
              " DECODE(c.AMOUNT,0,0,ROUND( (c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8)) AS  PERCENT_OF_APRE_EROSION " +
              " from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
              " (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES,'I',NO_SHARES)totshare, " +
              " decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT,'I',AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
              " from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
              " where f_cd IN( " + Convert.ToInt16(dtPortfolio.Rows[looper]["F_CD"]) + ") and    INV_DATE <= '" + strPortfolioAsOnDate.ToString() + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d " +
              " on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + strPortfolioAsOnDate.ToString() + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c inner join " +
              " NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID order by CAT_ID ";

                DataTable dtNonListed = commonGatewayObj.Select(queryString);

                //if (dtNonListed.Rows.Count > 0)
                //{

                //}




                    //string queryString = "SELECT  INV_AMOUNT, INV_DATE FROM  NON_LISTED_SECURITIES where F_CD IN(" + Convert.ToInt16(dtPortfolio.Rows[looper]["F_CD"]) + ") and INV_DATE<='" + strPortfolioAsOnDate.ToString() + "' ORDER BY INV_DATE DESC ";
                    //DataTable dtNonListed = commonGatewayObj.Select(queryString);


               if (dtNonListed.Rows.Count>0)
               {
                    drReprtSource["NON_LISTED_TOT_SEC"] = dtNonListed_securities.Rows[0]["total_securities"];
                    drReprtSource["NON_LISTED_TOT_NO_SHR"] = dtNonListed_securities.Rows[0]["total_no_share"];
                    drReprtSource["NON_LISTED_AMT"] = dtNonListed_securities.Rows[0]["total_cost_Price"];
                    drReprtSource["NON_LISTED_MR_PR"] = dtNonListed_securities.Rows[0]["total_market_price"];
                    drReprtSource["NON_LISTED_EROSION"] = dtNonListed_securities.Rows[0]["total_appriciation"];
                    drReprtSource["TOTAL_INVEST_MARKET"] = Convert.ToDecimal(dtPortfolio.Rows[looper]["TOTAL_MARKET_PRICE"]) + Convert.ToDecimal(dtNonListed_securities.Rows[0]["total_cost_Price"]);
                   drReprtSource["TOTAL_INVEST_COST"] = Convert.ToDecimal(dtPortfolio.Rows[looper]["TOTAL_COST"]) + Convert.ToDecimal(dtNonListed_securities.Rows[0]["total_cost_Price"]);
               }
               else
               {
                    drReprtSource["NON_LISTED_TOT_SEC"] = 0;
                    drReprtSource["NON_LISTED_AMT"] = 0;
                    drReprtSource["NON_LISTED_MR_PR"] = 0;
                    drReprtSource["NON_LISTED_EROSION"] = 0;
                   drReprtSource["TOTAL_INVEST_MARKET"] = dtPortfolio.Rows[looper]["TOTAL_MARKET_PRICE"];
                   drReprtSource["TOTAL_INVEST_COST"] = dtPortfolio.Rows[looper]["TOTAL_COST"];
               }


                dtReprtSource.Rows.Add(drReprtSource);
           }
       }








       //strSQL = "select p.f_cd , f.f_name fund_name,bal_dt_ctrl,COUNT(p.COMP_CD)TOTAL_COMPANY, sum(trunc(p.tot_nos)) TOTAL_SHARE," +
       //           "sum(p.tcst_aft_com) TOTAL_COST, sum(p.tot_nos * c.adc_rt) TOTAL_MARKET_PRICE, sum(p.tot_nos * c.adc_rt) - sum(p.tcst_aft_com)  EROSION," +
       //           "decode(sum(trunc(p.tot_nos)), 0, 'noneed', 'PROFIT') tp from pfolio_bk p, comp c, fund f " +
       //           " where p.bal_dt_ctrl ='" + strPortfolioAsOnDate.ToString() + "' and f.F_CD IN(" + fundCodes + ") and f.f_cd not in(3,5,18) and p.comp_cd = c.comp_cd " +
       //           " and(round(p.adc_rt, 2) - trunc(p.tcst_aft_com / tot_nos, 2)) * trunc(tot_nos) >= 0 and p.f_cd = f.f_cd " +
       //           " group by p.f_cd, bal_dt_ctrl,f.f_name " +
       //           " union" +
       //           " select p.f_cd ,f.f_name,bal_dt_ctrl,COUNT(p.COMP_CD) TOTAL_COMPANY, sum(trunc(p.tot_nos)) TOTAL_SHARE, sum(p.tcst_aft_com) TOTAL_COST," +
       //           " sum(p.tot_nos * c.adc_rt) TOTAL_MARKET_PRICE, sum(p.tot_nos * c.adc_rt) - sum(p.tcst_aft_com) EROSION," +
       //           " decode(sum(trunc(p.tot_nos)), 0, 'noneed', 'LOSS') tp" +
       //           " from  pfolio_bk p, comp c ,fund f where p.bal_dt_ctrl ='" + strPortfolioAsOnDate.ToString() + "' and f.F_CD IN(" + fundCodes + ") and f.f_cd not in(3,5,18)" +
       //           " and p.comp_cd = c.comp_cd and(round(p.adc_rt, 2) - trunc(p.tcst_aft_com / tot_nos, 2)) * trunc(tot_nos) < 0 " +
       //           " and p.f_cd = f.f_cd group by p.f_cd, bal_dt_ctrl, f.f_name order by(9) desc";



       dtReprtSource.TableName = "MrktValWithNonListedSecuritiesAllFunds";
     //  dtReprtSource.WriteXmlSchema(@"D:\Development\pfms_12_07_2021\amclinvanalysisandpfms\UI\ReportViewer\Report\xsdMrktValWithNonListedSecuritiesAllFunds_2.xsd");
       if (dtReprtSource.Rows.Count > 0)
       {
           string Path = Server.MapPath("Report/crptMarketValuationWithNonListedSecuritesReport.rpt");
           rdoc.Load(Path);
           rdoc.SetDataSource(dtReprtSource);
           CRV_MarketValiationWithProfitLossReportViewer.ReportSource = rdoc;
           //rdoc.SetParameterValue("prmtransactionDate", tranDate);
           rdoc = ReportFactory.GetReport(rdoc.GetType());
       }
       else
       {
           Response.Write("No Data Found");
       }
   }
   protected void Page_Unload(object sender, EventArgs e)
   {
       CRV_MarketValiationWithProfitLossReportViewer.Dispose();
       CRV_MarketValiationWithProfitLossReportViewer = null;
       rdoc.Close();
       rdoc.Dispose();
       rdoc = null;
       GC.Collect();
   }
}
