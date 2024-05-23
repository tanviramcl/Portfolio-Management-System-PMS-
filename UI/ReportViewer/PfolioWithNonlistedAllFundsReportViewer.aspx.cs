using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
        string strSQLForMainReport, strSQLForSubReport, strSqlForNonlistedDetails, strSQLforTotalShareandTotalCost;
        //string companyCodes = "";
        //string percentageCheck = "";
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
            balDate = (string)Session["balDate"];
        }
        CommonGateway commonGatewayObj = new CommonGateway();
        //  DataTable dtReprtSource = new DataTable();
        DataTable dtRptSrcMainReport = new DataTable();
        DataTable dtRptSrcSubReport = new DataTable();
        DataTable dtnonlistedDetails = new DataTable();
        DataTable dtforTotalShareandTotalCost = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();

        //strSQLForMainReport = "SELECT p.f_cd1,q.f_cd2,p.fund_name1,q.fund_name2,p.COST1,q.COST2,q.SALE2,p.No_Of_Share1,q.No_Of_Share2,q.profit2 from " +                                 
        //   "(SELECT  t.F_CD f_cd1, f.f_name fund_name1,SUM(AMT_AFT_COM) as COST1, sum(no_share) as No_Of_Share1 FROM FUND_TRANS_HB  t, fund f " +
        // " WHERE t.TRAN_TP = 'C' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and t.f_cd = f.f_cd and t.f_cd <> 3" +
        // " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name)p, (SELECT t.F_CD f_cd2, f.f_name fund_name2,SUM(AMT_AFT_COM) as SALE2," +
        // "sum(no_share) as No_Of_Share2,sum(crt_aft_com * no_share) as COST2,SUM(AMT_AFT_COM) - sum(crt_aft_com * no_share) as profit2" +
        //  " FROM FUND_TRANS_HB t, fund f WHERE t.TRAN_TP = 'S' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and f.f_cd = t.f_cd and t.f_cd <> 3" +
        //   " GROUP BY t.F_CD, f.f_name)q where p.f_cd1=q.f_cd2 order by p.f_cd1";

        strSQLForMainReport = "SELECT     FUND.F_NAME, COMP.COMP_NM,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, " +
        "DECODE( PFOLIO_BK.TOT_NOS,0,0,((ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8))))  AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM," +
        "NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) " +
        "AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, " +
        "ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - DECODE( PFOLIO_BK.TOT_NOS,0,0,((ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8)))), 8) AS RATE_DIFF, " +
        "ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) " +
        "AS APPRICIATION_ERROTION, ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) " +
        " / DECODE(PFOLIO_BK.TCST_AFT_COM, 0 , 1 , PFOLIO_BK.TCST_AFT_COM* 100), 8) AS PERCENT_OF_APRE_EROSION, " +
        "ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP " +
        "FROM         PFOLIO_BK INNER JOIN " +
        "COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN " +
        "FUND ON PFOLIO_BK.F_CD = FUND.F_CD " +
        "WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (FUND.F_CD =" + fundCode + ") " +
        "ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM ";

        dtRptSrcMainReport = commonGatewayObj.Select(strSQLForMainReport);

        string strSQLForSubReport1 = " select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
"ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / DECODE(c.AMOUNT, 0 , 1, c.AMOUNT* 100), 8) AS PERCENT_OF_APRE_EROSION " +
" from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
" (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES,'I',NO_SHARES)totshare, " +
" decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT,'I',AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
" from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
" where f_cd =  " + fundCode + " and    INV_DATE <= '" + balDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d " +
" on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + balDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join " +
" NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d where D.NO_SHARES>0 and D.AMOUNT>0 ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID order by CAT_ID  ";

        dtRptSrcSubReport = commonGatewayObj.Select(strSQLForSubReport1);


        strSqlForNonlistedDetails = "select sum(totammount) as totalAmmount ,sum(totshare) as totshare from (select decode(TRAN_TP,'B',NO_SHARES,'S',-NO_SHARES,'I',NO_SHARES)totshare, decode(TRAN_TP,'B',AMOUNT,'S',-AMOUNT,'I',AMOUNT)totammount," +
                                   " F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP  from (Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE," +
                                   "  CAT_ID, TRAN_TP from  invest.NON_LISTED_SECURITIES_DETAILS where f_cd=" + fundCode + " and INV_DATE <='" + balDate + "' order by INV_DATE)) ";

        dtnonlistedDetails = commonGatewayObj.Select(strSqlForNonlistedDetails);


        strSQLforTotalShareandTotalCost = "select sum (TOT_NOS) as totalshare,sum(TCST_AFT_COM) as totalTCST_AFT_COM ,sum(TOT_MARKET_PRICE)" +
            "as totalTOT_MARKET_PRICE , ROUND(sum(APPRICIATION_ERROTION),2) as totalAPPRICIATION_ERROTION from (SELECT     FUND.F_NAME, COMP.COMP_NM,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, " +
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
       "ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM) a ";

        dtforTotalShareandTotalCost = commonGatewayObj.Select(strSQLforTotalShareandTotalCost);

        Decimal nonlistedtotalAmmount = 0;
        Decimal nonlistedtotshare = 0;
        Decimal portfiliototalCostAfterCommissionAmmount = 0;
        Decimal portfiliototshare = 0;
        Decimal portfiliototalMarketPrice = 0;
        Decimal portfoliototalErroision = 0;
        if (dtRptSrcSubReport.Rows.Count > 0)
        {
            nonlistedtotalAmmount = Convert.ToDecimal(dtnonlistedDetails.Rows[0]["totalAmmount"]);
            nonlistedtotshare = Convert.ToDecimal(dtnonlistedDetails.Rows[0]["totshare"]);
        }
        if (dtforTotalShareandTotalCost.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dtforTotalShareandTotalCost.Rows[0]["totalTCST_AFT_COM"].ToString()) && !string.IsNullOrEmpty(dtforTotalShareandTotalCost.Rows[0]["totalshare"].ToString()))
            {
                portfiliototalCostAfterCommissionAmmount = Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalTCST_AFT_COM"]);
                portfiliototshare = Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalshare"]);
                portfiliototalMarketPrice = Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalTOT_MARKET_PRICE"]);
                portfoliototalErroision = Convert.ToDecimal(dtforTotalShareandTotalCost.Rows[0]["totalAPPRICIATION_ERROTION"]);
            }
           

        }
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
        dtRptSrcMainReport.TableName = "dtCopyForMainPortfolioRpt_details";
        dtRptSrcSubReport.TableName = "dtCopyForSubNonlistedRpt_dtails";

        ds.Tables.Add(dtRptSrcMainReport.Copy());
        ds.Tables.Add(dtRptSrcSubReport.Copy());

        //   E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report
        // dtReprtSource.WriteXmlSchema(@"E:\amclpmfs\UI\ReportViewer\Report\xsdMarketValuationWithProfitLoss.xsd");
        // dtReprtSource.WriteXmlSchema(@"E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report\xsdMarketValuationWithProfitLoss.xsd");

        //  ds.WriteXmlSchema(@"D:\16-07-19\amclinvanalysisandpfms\UI\ReportViewer\Report\crtPfolioNonListedDetailsReport_1.xsd");
        //  ds.WriteXmlSchema(@"D:\Development\pfms_25_01_2021\amclinvanalysisandpfms\UI\ReportViewer\Report\crtPfolioNonListedDetails_with_company_details_final.xsd");

        // if (dtRptSrcMainReport.Rows.Count>0  && dtRptSrcSubReport.Rows.Count > 0 )
        if (dtRptSrcMainReport.Rows.Count > 0)
        {
            Decimal totalInvest = 0;
            for (int loop = 0; loop < dtRptSrcMainReport.Rows.Count; loop++)
            {
                totalInvest = totalInvest + Convert.ToDecimal(dtRptSrcMainReport.Rows[loop]["TCST_AFT_COM"]);
            }
            string Path = Server.MapPath("Report/crtPortfolioNonListedDetailsReport.rpt");
            rdoc.Load(Path);
            //ds.Tables[0].Merge(dtRptSrcMainReport);
            //ds.Tables[0].Merge(dtRptSrcSubReport);
            // rdoc.SetDataSource(dtReprtSource);
            rdoc.SetDataSource(ds);





            CRV_protfoliowithNonlisted.ReportSource = rdoc;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("prmTotalInvest", totalInvest);
            rdoc.SetParameterValue("nonlistedtotalAmmount", nonlistedtotalAmmount);
            rdoc.SetParameterValue("nonlistedtotshare", nonlistedtotshare);
            rdoc.SetParameterValue("portfiliototalCostAfterCommissionAmmount", portfiliototalCostAfterCommissionAmmount);
            rdoc.SetParameterValue("portfiliototshare", portfiliototshare);
            rdoc.SetParameterValue("portfiliototalMarketPrice", portfiliototalMarketPrice);
            rdoc.SetParameterValue("portfoliototalErroision", portfoliototalErroision);

            //rdoc.SetParameterValue("prmTodate", strTodate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

   


    }
        else
        {
            Response.Write("No Data Found");
        }

       

    }

    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    CRV_protfoliowithNonlisted.Dispose();
    //    CRV_protfoliowithNonlisted = null;
    //    rdoc.Close();
    //    rdoc.Dispose();
    //    rdoc = null;
    //    GC.Collect();
    //}
}