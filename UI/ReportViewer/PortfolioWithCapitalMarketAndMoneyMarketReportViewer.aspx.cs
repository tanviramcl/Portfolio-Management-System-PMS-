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
        string balDate = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            balDate = (string)Session["balDate"];
        }



        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
     

        sbMst.Append(" select c.*,D.SECT_MAJ_CAT_NM from (select a.F_NAME, a.COMP_NM, b.SECT_MAJ_NM, b.SECT_MAJ_CD, b.SECT_MAJ_CAT_ID, a.TOT_NOS, a.COST_RT_PER_SHARE,a.TCST_AFT_COM/1000000 AS TCST_AFT_COM, a.DSE_RT, a.CSE_RT, a.AVG_RATE, a.TOT_MARKET_PRICE/1000000 as TOT_MARKET_PRICE , a.RATE_DIFF, a.APPRICIATION_ERROTION/1000000 as APPRICIATION_ERROTION , a.PERCENT_OF_APRE_EROSION, a.PERCENTAGE_OF_PAIDUP ");
        sbMst.Append(" from(SELECT     FUND.F_NAME, COMP.COMP_NM, PFOLIO_BK.SECT_MAJ_NM, PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS, 0) AS TOT_NOS, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM, NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, ");
        sbMst.Append(" NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, ");
        sbMst.Append("  ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) AS APPRICIATION_ERROTION, ");
        sbMst.Append(" ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) / DECODE(PFOLIO_BK.TCST_AFT_COM, 0 , 1 , PFOLIO_BK.TCST_AFT_COM* 100), 8) AS PERCENT_OF_APRE_EROSION, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP FROM         PFOLIO_BK INNER JOIN COMP ON  ");
        sbMst.Append(" PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN FUND ON PFOLIO_BK.F_CD = FUND.F_CD WHERE ");
        sbMst.Append(" (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND(FUND.F_CD = " + fundCode + ") ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM)  a INNER JOIN SECT_MAJ b ON a.SECT_MAJ_CD = b.SECT_MAJ_CD) c inner join SECT_MAJ_CAT d ");
        sbMst.Append(" ON c.SECT_MAJ_CAT_ID = D.SECT_MAJ_CAT_ID  ORDER BY  c.SECT_MAJ_CAT_ID ");



        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());


        string strSQLForSubReport1 = " select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT/1000000 as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price/1000000 as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION/1000000 as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
    " DECODE(c.AMOUNT,0,0,ROUND( (c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / c.AMOUNT * 100, 8)) AS PERCENT_OF_APRE_EROSION " +
 " from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
" (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES,'I',NO_SHARES)totshare, " +
 " decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT,'I',AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
 " from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
 " where f_cd =  " + fundCode + " and    INV_DATE <= '" + balDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d " +
 " on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '"+balDate+ "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c  inner join " +
 " NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d where D.NO_SHARES>0 and D.AMOUNT>0 ) e  full outer join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID order by CAT_ID  ";

        DataTable dtRptSrcSubReport1 = commonGatewayObj.Select(strSQLForSubReport1);

        string strSQLForSubReport2 = "  select c.ID,c.FDR_NO,c.FDR_OPEN_DT ,c.BANK_NAME,d.BRANCH_CODE,D.BRANCH_NAME,D.BRANCH_ADDRS1,D.BRANCH_ADDRS2,c.FDR_RATE,c.FDR_AMOUNT /1000000 as FDR_AMOUNT ,(((c.FDR_MATUR_DT -c.FDR_OPEN_DT +1 ) *FDR_RATE*FDR_AMOUNT))  As MaturityValue  from (Select a.*, B.BANK_NAME from(SELECT  ID, FDR_NO, FUND_CD, FUND_BR_CODE, FDR_OPEN_DT, FDR_RATE, FDR_AMOUNT, FDR_MATUR_DT, BANK_CODE, BANK_BRANC_CODE, FDR_STATUS, FDR_REN_NO, " +
            " VALID, EDIT_TYPE, ENTRY_BY, ENTRY_DATE, REMARKS, FDR_CLOS_DT, FDR_TAX_DIDUCT, FDR_EXCI_DIDUCT, FDR_CLOS_TYPE, FDR_RECV_INTEREST, PAY_BANK_CODE, PAY_BANK_BR_CODE, " +
           " PAY_TYPE, PAY_CHQ_DD_NO, PAY_CHQ_DD_DT, PAY_AMOUNT, FDR_DURATION, FDR_DURATION_TYPE, FDR_CURRENCY_ID, ACC_DEBIT_HEAD, ACC_DEBIT_VOUCHER_NO, " +
          " ACC_DEBIT_DT, ACC_CREDIT_HEAD, ACC_CREDIT_VOUCHER_NO, ACC_CREDIT_DT, FDR_INSTRUMNT_NO, FDR_INSTRUMNT_DT, FDR_BANK_ACC, FDR_ACC_REFERENC, FDR_CLOSE_BY, " +
         "  FDR_CLOSE_BY_DT FROM INVEST.FDR_INFO) a INNER JOIN  UNIT.BANK_NAME  b on a.BANK_CODE = b.BANK_CODE " +
   //      "  WHERE  a.VALID is NULL   and a.FDR_REN_NO is null  and FDR_OPEN_DT  <=  '" + balDate + "' and FDR_MATUR_DT >='" + balDate + "'  and a.FUND_CD = " + fundCode + " order by BANK_NAME) c  " +
   "  WHERE  a.VALID is NULL   and FDR_OPEN_DT  <=  '" + balDate + "' and FDR_MATUR_DT >'" + balDate + "'  and a.FUND_CD = " + fundCode + " order by BANK_NAME) c  " +
             " inner join UNIT.BANK_BRANCH d on c.BANK_CODE = d.BANK_CODE and c.BANK_BRANC_CODE = d.BRANCH_CODE  ";

        DataTable dtRptSrcSubReport2 = commonGatewayObj.Select(strSQLForSubReport2);



       

        string strSQLForSubReport3 = "  Select A.F_CD,A.BANK_CODE,B.BANK_NAME,A.ACCOUNT_NUMBER, A.NATURE_OF_ACCOUNT,   A. RATE_OF_INTEREST, A.AVAILABLE_BALANCE, A.MARKET_VALUE,   A. ENTRY_DATE FROM INVEST.CASH_AT_BANK a inner join UNIT.BANK_NAME b on a.BANK_CODE=b.BANK_CODE Where a.F_CD="+fundCode+" and a.ENTRY_DATE='"+balDate+"' ";
        DataTable dtRptSrcSubReport3 = commonGatewayObj.Select(strSQLForSubReport3);

        DataSet ds = new DataSet();
        dtReprtSource.TableName = "dtCopyForMainRpt_CM";
        dtRptSrcSubReport1.TableName = "dtCopyForSubRpt1_CM";
        dtRptSrcSubReport2.TableName = "dtCopyForSubRpt2_CM";
        dtRptSrcSubReport3.TableName = "dtCopyForSubRpt3_CM";


        ds.Tables.Add(dtReprtSource.Copy());
        ds.Tables.Add(dtRptSrcSubReport1.Copy());
        ds.Tables.Add(dtRptSrcSubReport2.Copy());
        ds.Tables.Add(dtRptSrcSubReport3.Copy());


        // dtRptSrcSubReport2.WriteXmlSchema(@"E:\pfms_11-10_20\amclinvanalysisandpfms\UI\ReportViewer\Report\dtRptSrcSubReport2.xsd");

        // ds.WriteXmlSchema(@"E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report\xsdCapitalGainAllFunds.xsd");
  //      ds.WriteXmlSchema(@"E:\pfms_11-10_20\amclinvanalysisandpfms\UI\ReportViewer\Report\crtPortfolioWithCapitalAndMoneyMarket_FinaL_test.xsd");




        if (dtReprtSource.Rows.Count > 0)
        {
            Decimal totalInvest = 0;
            for (int loop = 0; loop < dtReprtSource.Rows.Count; loop++)
            {
                totalInvest = totalInvest + Convert.ToDecimal(dtReprtSource.Rows[loop]["TCST_AFT_COM"]);
            }

            Decimal totalPfolioMarketPrice = 0;
            for (int loop = 0; loop < dtReprtSource.Rows.Count; loop++)
            {
                totalPfolioMarketPrice = totalPfolioMarketPrice + Convert.ToDecimal(dtReprtSource.Rows[loop]["TOT_MARKET_PRICE"]);
            }

            Decimal totalInvstNonListed = 0;
           
          //  DataTable dt = new DataTable();
            foreach (DataRow dr in dtRptSrcSubReport1.Rows)
            {
                if (dr["AMOUNT"] != DBNull.Value)
                {
                    //Do something
                    totalInvstNonListed = totalInvstNonListed + Convert.ToDecimal(dr["AMOUNT"]);
                }
                else
                {
                    //Do something
                }
            }
            Decimal totalInvstNonListedMarketPrice = 0;
            foreach (DataRow dr in dtRptSrcSubReport1.Rows)
            {
                if (dr["TOT_MARKET_PRICE"] != DBNull.Value)
                {
                    //Do something
                    totalInvstNonListedMarketPrice = totalInvstNonListedMarketPrice + Convert.ToDecimal(dr["TOT_MARKET_PRICE"]);
                }
                else
                {
                    //Do something
                }
            }


             Decimal totalFDRAmount = 0;


            for (int i = 0; i < dtRptSrcSubReport2.Rows.Count; i++)
            {
                totalFDRAmount = totalFDRAmount + Convert.ToDecimal(dtRptSrcSubReport2.Rows[i]["FDR_AMOUNT"]);
            }

            Decimal totalFDRMaturityVAlue = 0;


            for (int i = 0; i < dtRptSrcSubReport2.Rows.Count; i++)
            {
                decimal maturityValue = Convert.ToDecimal(dtRptSrcSubReport2.Rows[i]["FDR_AMOUNT"]) + (Convert.ToDecimal(dtRptSrcSubReport2.Rows[i]["MaturityValue"]) / (360 * 100)) / 1000000;

                totalFDRMaturityVAlue = totalFDRMaturityVAlue + maturityValue;
            }


            Decimal totalAvailavleBalance = 0;

            for (int i = 0; i < dtRptSrcSubReport3.Rows.Count; i++)
            {
                totalAvailavleBalance = totalAvailavleBalance + Convert.ToDecimal(dtRptSrcSubReport3.Rows[i]["AVAILABLE_BALANCE"]);
            }

            Decimal totalCashInBankMaturtyValue = 0;

            for (int i = 0; i < dtRptSrcSubReport3.Rows.Count; i++)
            {
                totalCashInBankMaturtyValue = totalCashInBankMaturtyValue + Convert.ToDecimal(dtRptSrcSubReport3.Rows[i]["MARKET_VALUE"]);
            }

            Decimal TotalFinalInvestment =0;

            TotalFinalInvestment = totalInvest + totalInvstNonListed + totalFDRAmount + totalAvailavleBalance;

            Decimal TotalFinalMarketValue = 0;
             TotalFinalMarketValue = totalPfolioMarketPrice + totalInvstNonListedMarketPrice + totalFDRMaturityVAlue+ totalCashInBankMaturtyValue;

             Decimal TotalFDRandCashInBankInvestment = 0;

            TotalFDRandCashInBankInvestment= totalFDRAmount + totalAvailavleBalance;

            Decimal TotalFDRandCashInBankMaturityValue = 0;

            TotalFDRandCashInBankMaturityValue = totalFDRMaturityVAlue + totalCashInBankMaturtyValue;




            string Path = "";
            Path = Server.MapPath("Report/crtPortfolioWithNonListedReport_capital_money.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(ds);
            CRV_PfolioWithNonListed.ReportSource = rdoc;
            CRV_PfolioWithNonListed.DisplayToolbar = true;
            CRV_PfolioWithNonListed.HasExportButton = true;
            CRV_PfolioWithNonListed.HasPrintButton = true;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("prmTotalInvest", totalInvest);
            rdoc.SetParameterValue("prmNonlistedCostPrice", totalInvstNonListed);
            rdoc.SetParameterValue("prmTotalFinalInvestment", TotalFinalInvestment);
            rdoc.SetParameterValue("prmTotalFinalMarketValue", TotalFinalMarketValue);
            rdoc.SetParameterValue("prmTotalFDRandCashInBankInvestment", TotalFDRandCashInBankInvestment);
            rdoc.SetParameterValue("prmTotalFDRandCashInBankMaturityValue", TotalFDRandCashInBankMaturityValue);
            //  rdoc.SetParameterValue("prmNonlisteMarketPrice", nonlistedMarketPrice);
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
