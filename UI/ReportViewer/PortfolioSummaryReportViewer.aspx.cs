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

public partial class UI_ReportViewer_PortfolioSummaryReportViewer : System.Web.UI.Page
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
        sbMst.Append("SELECT     FUND.F_NAME, PFOLIO_BK.SECT_MAJ_NM, TRUNC(SUM(PFOLIO_BK.TOT_NOS),0) AS NO_OF_SHARE, ");
        sbMst.Append("SUM(PFOLIO_BK.TCST_AFT_COM) AS COST_PRICE, SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT) AS MARKET_PRICE, ");
        sbMst.Append("SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT) - SUM(PFOLIO_BK.TCST_AFT_COM) AS APPRE_EROSION, ");
        sbMst.Append("ROUND((SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT) - SUM(PFOLIO_BK.TCST_AFT_COM)) ");
        sbMst.Append("* 100 /DECODE(SUM(PFOLIO_BK.TCST_AFT_COM), 0 , 1 , SUM(PFOLIO_BK.TCST_AFT_COM)), 2) AS PERCENT_APPRE_EROSION ");
        sbMst.Append("FROM        FUND INNER JOIN ");
        sbMst.Append(" PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ");
        sbMst.Append("WHERE     (FUND.F_CD ="+ fundCode + ") AND (PFOLIO_BK.BAL_DT_CTRL = '"+ balDate +"') ");
        sbMst.Append("GROUP BY PFOLIO_BK.SECT_MAJ_NM, FUND.F_NAME,PFOLIO_BK.SECT_MAJ_CD ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_CD ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        DataTable dtNonlistedSecrities = new DataTable();
       

        string strSqlForNonlistedDetails = " select sum(NO_SHARES) as totshare,sum(AMOUNT) as totalAmmount ,sum(TOT_MARKET_PRICE)as totalMarketPrice,sum(APPRICIATION_ERROTION) as APPriciation,sum(PERCENT_OF_APRE_EROSION) as totalPERCENT_OF_APRE_EROSION from("+ 
 "select e.COMP_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION ,e.PERCENT_OF_APRE_EROSION from (  select D.COMP_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from (select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, " +
"ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / DECODE(c.AMOUNT, 0 , 1, c.AMOUNT* 100), 8) AS PERCENT_OF_APRE_EROSION " +
" from(select a.*, B.TRAN_DATE from(select c.COMP_CD, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from(select a.COMP_CD, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from " +
" (select comp_cd, sum(totammount) as totalAmmount, sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, " +
" decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP " +
" from(Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS " +
" where f_cd =  " + fundCode + " and    INV_DATE <= '" + balDate + "' order by INV_DATE)) group by COMP_CD)  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd) c inner join invest.NONLISTED_CATEGORY d " +
" on c.CAT_TP = d.CAT_ID order by comp_cd) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" + balDate + "' group by comp_cd) b  on A.COMP_CD = B.COMP_CD) c left outer join " +
" NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd  ) d where D.NO_SHARES>0 and D.AMOUNT>0 ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID order by CAT_ID)  ";

       



        //sbMst = new StringBuilder();
        //sbMst.Append("SELECT      'NON LISTED SECURITIES' AS SECT_MAJ_NM,INV_AMOUNT AS COST_PRICE, INV_AMOUNT AS MARKET_PRICE, 0 AS APPRE_EROSION, 0 AS PERCENT_APPRE_EROSION ");
        //sbMst.Append("FROM         NON_LISTED_SECURITIES ");
        //sbMst.Append("WHERE     (F_CD = "+ fundCode +") AND (INV_DATE = ");
        //sbMst.Append(" (SELECT     MAX(INV_DATE) AS EXPR1 ");
        //sbMst.Append("FROM          NON_LISTED_SECURITIES NON_LISTED_SECURITIES_1 ");
        //sbMst.Append("WHERE      (F_CD = " + fundCode + ") AND (INV_DATE <= '" + balDate + "'))) ");
        dtNonlistedSecrities = commonGatewayObj.Select(strSqlForNonlistedDetails.ToString());

        if (dtNonlistedSecrities.Rows.Count > 0)
        {
            DataRow drReport;

            if (!string.IsNullOrEmpty(dtNonlistedSecrities.Rows[0]["totalAmmount"].ToString()))
            {
                for (int loop = 0; loop < dtNonlistedSecrities.Rows.Count; loop++)
                {
                    drReport = dtReprtSource.NewRow();
                    drReport["SECT_MAJ_NM"] = "NON LISTED SECURITIES";
                    drReport["NO_OF_SHARE"] = dtNonlistedSecrities.Rows[loop]["totshare"].ToString();
                    drReport["COST_PRICE"] = dtNonlistedSecrities.Rows[loop]["totalAmmount"].ToString();
                    drReport["MARKET_PRICE"] = dtNonlistedSecrities.Rows[loop]["totalMarketPrice"].ToString();
                    drReport["APPRE_EROSION"] = dtNonlistedSecrities.Rows[loop]["APPriciation"].ToString();
                    drReport["PERCENT_APPRE_EROSION"] = (Convert.ToDecimal(dtNonlistedSecrities.Rows[loop]["APPriciation"].ToString())/ Convert.ToDecimal(dtNonlistedSecrities.Rows[loop]["totalAmmount"].ToString())*100);
                    dtReprtSource.Rows.Add(drReport);
                }
            }

           

           
        }
        if (dtReprtSource.Rows.Count > 0)
        {
            Decimal totalInvest = 0;
            for (int loop = 0; loop < dtReprtSource.Rows.Count; loop++)
            {
                totalInvest = totalInvest + Convert.ToDecimal(dtReprtSource.Rows[loop]["COST_PRICE"]); 
            }
            dtReprtSource.TableName = "PortfolioSummaryReport";
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtPortfolioSummaryReport.xsd");
            //ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtPortfolioSummaryReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_PortfolioSummary.ReportSource = rdoc;
            CRV_PortfolioSummary.DisplayToolbar = true;
            CRV_PortfolioSummary.HasExportButton = true;
            CRV_PortfolioSummary.HasPrintButton = true;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("prmTotalInvest", totalInvest);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_PortfolioSummary.Dispose();
        CRV_PortfolioSummary = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
