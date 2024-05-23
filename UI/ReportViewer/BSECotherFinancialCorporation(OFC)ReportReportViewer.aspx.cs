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
      
        string strSQLForMainReport;
  
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string balDate = "";
        string fundName = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            
            fundCode = (string)Session["fundCode"];
            fundName= (string)Session["fundName"];
            balDate = (string)Session["balDate"];
        }
        CommonGateway commonGatewayObj = new CommonGateway();
       
        DataTable dtRptSrcMainReport = new DataTable();
       
        strSQLForMainReport = "select COMP_CD,COMP_NM,NO_SHARES,OFC_CODE_NO, AMOUNT,TOT_MARKET_PRICE,OFC_CODE_NAME from (SELECT     FUND.F_NAME,COMP.COMP_CD, COMP.COMP_NM,COMP.OFC_CODE_NO,OFC_CODE.OFC_CODE_NAME,PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS   NO_SHARES, " +
        "DECODE( PFOLIO_BK.TOT_NOS,0,0,((ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8))))  AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM AS AMOUNT ," +
        "NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT, NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) " +
        "AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, " +
        "ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - DECODE( PFOLIO_BK.TOT_NOS,0,0,((ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8)))), 8) AS RATE_DIFF, " +
        "ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) " +
        "AS APPRICIATION_ERROTION, ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM) " +
        " / DECODE(PFOLIO_BK.TCST_AFT_COM, 0 , 1 , PFOLIO_BK.TCST_AFT_COM* 100), 8) AS PERCENT_OF_APRE_EROSION, " +
        "ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP " +
        "FROM         PFOLIO_BK INNER JOIN " +
        "COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN " +
        "FUND ON PFOLIO_BK.F_CD = FUND.F_CD   INNER JOIN OFC_CODE ON COMP.OFC_CODE_NO=OFC_CODE.OFC_CODE_NO " +
        "WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + balDate + "') AND (FUND.F_CD =" + fundCode + ") " +
        "ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM ) "+
        " Union all  "+
        " select COMP_CD,COMP_NM,NO_SHARES,OFC_CODE_NO,AMOUNT,TOT_MARKET_PRICE,OFC_CODE_NAME from  (select tab1.*,tab2.OFC_CODE_NAME from (select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.OFC_CODE_NO,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date," +
        "e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION from(  select D.COMP_CD,d.COMP_NM,d.OFC_CODE_NO,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as "+
        "TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE , "+
        "ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / DECODE(c.AMOUNT, 0 , 1, c.AMOUNT* 100), 8) AS PERCENT_OF_APRE_EROSION "+
        "from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT,c.OFC_CODE_NO, d.CAT_ID,d.CAT_NM  from(select a.COMP_CD, b.comp_nm,B.OFC_CODE_NO, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from "+
        " (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from (select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, "+
        " decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP "+
        "from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS" +
        " where f_cd =  " + fundCode + " and    INV_DATE <= '" + balDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d " +
        "  on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from  NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + balDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c " +
        " left outer join  NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d where D.NO_SHARES>0 and D.AMOUNT>0 ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID order by CAT_ID) tab1 inner join  OFC_CODE tab2 on tab1.OFC_CODE_NO=tab2.OFC_CODE_NO  ) ";

        dtRptSrcMainReport = commonGatewayObj.Select(strSQLForMainReport);


        dtRptSrcMainReport.TableName = "BSECotherFinancialCorporation(OFC)";

        //dtRptSrcMainReport.WriteXmlSchema(@"D:\Development\PFMS_LIVE\PMCS_SERVER\amclinvanalysisandpfms\UI\ReportViewer\Report\CRT_BSECotherFinancialCorporation.xsd");
        if (dtRptSrcMainReport.Rows.Count > 0)
        {

            string Path = Server.MapPath("Report/BSECotherFinancialCorporation(OFC).rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtRptSrcMainReport);
            CR_BSECotherFinancialCorporation.ReportSource = rdoc;
            CR_BSECotherFinancialCorporation.DisplayToolbar = true;
            CR_BSECotherFinancialCorporation.HasExportButton = true;
            CR_BSECotherFinancialCorporation.HasPrintButton = true;
            rdoc.SetParameterValue("fundName", fundName);
            rdoc.SetParameterValue("prmHowlaDate", balDate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else
        {
            Response.Write("No Data Found");
        }






    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_BSECotherFinancialCorporation.Dispose();
        CR_BSECotherFinancialCorporation = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}