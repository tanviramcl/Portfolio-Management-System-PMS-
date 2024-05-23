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
       
        string strSQLForMainReport;
        
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string balDate = "";
        string fundname = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
       
            fundCode = (string)Session["fundCode"];
            balDate = (string)Session["balDate"];
            fundname= (string)Session["fundname"]; 
        }
        CommonGateway commonGatewayObj = new CommonGateway();
      
        DataTable dtRptSrcMainReport = new DataTable();
        DataTable dtRptSrcSubReport = new DataTable();
        DataTable dtnonlistedDetails = new DataTable();
        DataTable dtforTotalShareandTotalCost = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();

    
        strSQLForMainReport = "select e.COMP_CD,e.COMP_NM,e.FACE_VALUE,e.NO_SHARES,e.AMOUNT,Round(e.AMOUNT/e.NO_SHARES,2) As CostRate,f.CAT_ID,f.CAT_NM,e.tran_date, " +
        " e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION,Round(e.TOT_MARKET_PRICE-e.AMOUNT,2) as UnrealizedGain ,ROUND((Round(e.TOT_MARKET_PRICE-e.AMOUNT,2)/e.NO_SHARES),2) As UnrealizedGainPerunit ," +
        " e.NAV,e.nav-(e.NAV*.05) AS DiscountNAV,e.nav-(e.NAV*.05)-Round(e.AMOUNT/e.NO_SHARES,2) As UnrealizedLossPorfitbasedonNAV, " +
        " CASE  WHEN e.APPRICIATION_ERROTION < 0 THEN  e.APPRICIATION_ERROTION WHEN e.APPRICIATION_ERROTION > 0 THEN 0 " +
        " ELSE e.APPRICIATION_ERROTION  END AS RquirredProvisionFinal,0 AS UnrealizedLossRecovry, " +
        " (e.TOT_MARKET_PRICE) AS FairMarketValue  from   ( " +
        " select D.COMP_CD,d.COMP_NM,d.FACE_VALUE,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as"+
        "  TOT_MARKET_PRICE ,d.market_rate,d.NAV,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION " +
        " from ( select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,D.NAV, " +
        " ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
        " ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / DECODE(c.AMOUNT, 0 , 1, c.AMOUNT* 100), 8) AS PERCENT_OF_APRE_EROSION " +
        " from(select a.*, B.TRAN_DATE from(   select c.COMP_CD, c.comp_nm,c.FACE_VALUE, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from ( " +
        " select a.COMP_CD, b.comp_nm,b.FACE_VALUE, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
        "  (    select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from ( select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES,'I',NO_SHARES)totshare, " +
        "  decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT,'I',AMOUNT)totammount, F_CD, COMP_CD,  AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP "+
        " from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP  from NON_LISTED_SECURITIES_DETAILS  where CAT_ID=4 and" +
        " INV_DATE <= '"+balDate+"' and F_cd="+ fundCode + " order by INV_DATE   )) group by COMP_CD   )" +
        "  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d" +
        " on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from " +
        " NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + balDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join" +
        " NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d where D.NO_SHARES>0" +
        //  " and D.AMOUNT>0 ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID Where nav is not null  and e.AMOUNT>e.TOT_MARKET_PRICE order by CAT_ID";
         " and D.AMOUNT>0 ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID Where nav is not null   order by CAT_ID";

        dtRptSrcMainReport = commonGatewayObj.Select(strSQLForMainReport);

       




        //   E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report
        //    dtRptSrcMainReport.WriteXmlSchema(@"D:\Development\PFMS_LIVE\PMCS_SERVER\amclinvanalysisandpfms\UI\ReportViewer\Report\xsdValuationofMutualFunds.xsd");

        if (dtRptSrcMainReport.Rows.Count > 0)
        {
         
            string Path = Server.MapPath("Report/crtValuationofMutualFund.rpt");
            rdoc.Load(Path);
            //ds.Tables[0].Merge(dtRptSrcMainReport);
            //ds.Tables[0].Merge(dtRptSrcSubReport);
            // rdoc.SetDataSource(dtReprtSource);
            rdoc.SetDataSource(dtRptSrcMainReport);
            CRV_protfoliowithNonlisted.ReportSource = rdoc;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("fundname", fundname);



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