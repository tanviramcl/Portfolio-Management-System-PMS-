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
        string sector = "";
        string category = "";
        string group = "";
        string ipo = "";
        string marketype = "";
        string listedCategory = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = Convert.ToString(Request.QueryString["fundCode"]).Trim();
            balDate = Convert.ToString(Request.QueryString["balDate"]).Trim();
            sector = Convert.ToString(Request.QueryString["sector"]).Trim();
            category = Convert.ToString(Request.QueryString["category"]).Trim();
            group = Convert.ToString(Request.QueryString["group"]).Trim();
            ipo = Convert.ToString(Request.QueryString["ipo"]).Trim();
            marketype = Convert.ToString(Request.QueryString["marketype"]).Trim();
            listedCategory = Convert.ToString(Request.QueryString["listedCategory"]).Trim();
        }



        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("select * from (SELECT    FUND.F_CD, FUND.F_NAME, COMP.COMP_NM,COMP.CAT_TP,COMP.TRADE_METH,COMP.PROS_PUB_DT, COMP.IPOTYPE,COMP. MARKETTYPE,COMP.FLAG, ");
        sbMst.Append("COMP.ISSUE_MNG, PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS, ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, PFOLIO_BK.TCST_AFT_COM, NVL(PFOLIO_BK.DSE_RT, 0) AS DSE_RT,");
        sbMst.Append("NVL(PFOLIO_BK.CSE_RT, 0) AS CSE_RT, ROUND(PFOLIO_BK.ADC_RT, 8) AS AVG_RATE, ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) AS TOT_MARKET_PRICE, ");
        sbMst.Append("ROUND(ROUND(PFOLIO_BK.ADC_RT, 8) - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8), 8) AS RATE_DIFF,");
        sbMst.Append("ROUND(ROUND(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT, 8) - PFOLIO_BK.TCST_AFT_COM, 8) AS APPRICIATION_ERROTION,");
        sbMst.Append("ROUND((PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT - PFOLIO_BK.TCST_AFT_COM)  / PFOLIO_BK.TCST_AFT_COM * 100, 8) AS PERCENT_OF_APRE_EROSION, ");
        sbMst.Append("ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 8) AS PERCENTAGE_OF_PAIDUP FROM    ");
        sbMst.Append("PFOLIO_BK INNER JOIN COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE   (PFOLIO_BK.BAL_DT_CTRL = '"+balDate+"') AND (FUND.F_CD ="+fundCode+") ORDER BY PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM  ) a   ");

        if (string.Compare(listedCategory, "L", true) == 0)
        {
          

            if (sector != "0" && category == "0" && group == "0" && ipo == "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD=" + sector + " and a.MARKETTYPE='" + marketype + "'");

            }
            else if (sector == "0" && category != "0" && group == "0" && ipo == "0")
            {
                sbMst.Append(" where  a.CAT_TP='" + category + "' and a.MARKETTYPE='" + marketype + "'");
            }
            else if (sector == "0" && category == "0" && group != "0" && ipo == "0")
            {
                sbMst.Append(" where  A.TRADE_METH='" + group + "' and a.MARKETTYPE='" + marketype + "'");
            }
            else if (sector == "0" && category == "0" && group == "0" && ipo != "0")
            {
                sbMst.Append(" where   a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "'");

            }
            else if (sector == "0" && category == "0" && group == "0" && ipo == "0" && marketype != "0")
            {
                sbMst.Append(" where   a.MARKETTYPE='" + marketype + "'");
            }
            else if (sector == "0" && category != "0" && group == "0" && ipo != "0" && marketype != "0")
            {
                sbMst.Append(" where  a.CAT_TP='" + category + "'  and a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector == "0" && category != "0" && group == "0" && ipo != "0" && marketype != "0")
            {
                sbMst.Append(" where   a.CAT_TP='" + category + "' and  a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector == "0" && category != "0" && group != "0" && ipo == "0" && marketype != "0")
            {
                sbMst.Append(" where   a.CAT_TP='" + category + "' and A.TRADE_METH='" + group + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector == "0" && category != "0" && group != "0" && ipo != "0" && marketype != "0")
            {
                sbMst.Append(" where   a.CAT_TP='" + category + "' and A.TRADE_METH='" + group + "' and  a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector != "0" && category == "0" && group != "0" && ipo != "0" && marketype != "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "'  and A.TRADE_METH='" + group + "' and a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector != "0" && category == "0" && group == "0" && ipo != "0" && marketype != "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "' and a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector != "0" && category == "0" && group == "0" && ipo == "0" && marketype != "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector != "0" && category != "0" && group == "0" && ipo != "0" && marketype != "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "' and a.CAT_TP='" + category + "' and a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector != "0" && category != "0" && group != "0" && ipo == "0" && marketype != "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "' and a.CAT_TP='" + category + "' and A.TRADE_METH='" + group + "'  and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector != "0" && category != "0" && group != "0" && ipo != "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "' and a.CAT_TP='" + category + "' and A.TRADE_METH='" + group + "' and a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector == "0" && category == "0" && group != "0" && ipo != "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "'  and A.TRADE_METH='" + group + "' and a.IPOTYPE='" + ipo + "' and a.MARKETTYPE='" + marketype + "' ");
            }
            else if (sector != "0" && category == "0" && group != "0" && ipo == "0")
            {
                sbMst.Append(" where  a.SECT_MAJ_CD='" + sector + "'  and A.TRADE_METH='" + group + "'  and a.MARKETTYPE='" + marketype + "' ");
            }
            sbMst.Append(" and  a.FLAG='" + listedCategory + "'");
        }
        else if (string.Compare(listedCategory, "D", true) == 0)
        {
            sbMst.Append(" and  where a.FLAG='" + listedCategory + " '");
        }

            



        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());




        DataTable dtNonlistedSecrities = new DataTable();
        sbMst = new StringBuilder();
        sbMst.Append("SELECT      INV_AMOUNT AS COST_PRICE, INV_AMOUNT AS MARKET_PRICE ");
        sbMst.Append("FROM         NON_LISTED_SECURITIES ");
        sbMst.Append("WHERE     (F_CD = " + fundCode + ") AND (INV_DATE = ");
        sbMst.Append(" (SELECT     MAX(INV_DATE) AS EXPR1 ");
        sbMst.Append("FROM          NON_LISTED_SECURITIES NON_LISTED_SECURITIES_1 ");
        sbMst.Append("WHERE     (F_CD = " + fundCode + ") AND (INV_DATE <= '" + balDate + "'))) ");
        dtNonlistedSecrities = commonGatewayObj.Select(sbMst.ToString());

        Decimal nonlistedCostPrice = 0;
        Decimal nonlistedMarketPrice = 0;
        if (dtNonlistedSecrities.Rows.Count > 0)
        {
            nonlistedCostPrice = Convert.ToDecimal(dtNonlistedSecrities.Rows[0][0]);
            nonlistedMarketPrice = Convert.ToDecimal(dtNonlistedSecrities.Rows[0][0]);
        }
        if (dtReprtSource.Rows.Count > 0)
        {
            Decimal totalInvest = 0;
            for (int loop = 0; loop < dtReprtSource.Rows.Count; loop++)
            {
                totalInvest = totalInvest + Convert.ToDecimal(dtReprtSource.Rows[loop]["TCST_AFT_COM"]);
            }
            dtReprtSource.TableName = "PortfolioWithNonListedReport";
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtPortfolioWithNonListedReport.xsd");
            //ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtPortfolioIndiffrentCriteria.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_PfolioWithdiffrentcriteria.ReportSource = rdoc;
            CRV_PfolioWithdiffrentcriteria.DisplayToolbar = true;
            CRV_PfolioWithdiffrentcriteria.HasExportButton = true;
            CRV_PfolioWithdiffrentcriteria.HasPrintButton = true;
            rdoc.SetParameterValue("prmbalDate", balDate);
            rdoc.SetParameterValue("prmTotalInvest", totalInvest);
            rdoc.SetParameterValue("prmNonlistedCostPrice", nonlistedCostPrice);
            rdoc.SetParameterValue("prmNonlisteMarketPrice", nonlistedMarketPrice);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_PfolioWithdiffrentcriteria.Dispose();
        CRV_PfolioWithdiffrentcriteria = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
