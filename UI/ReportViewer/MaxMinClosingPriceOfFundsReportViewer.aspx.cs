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


public partial class UI_ReportViewer_MaxMinClosingPriceOfFundsReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }

        string fyClosingPriceDateFrom = "";
        string fyClosingPriceDateTo = "";
        string latestClosingPriceDate = "";
        string FY = "";
       
        DateTime DateFrom = Convert.ToDateTime(Request.QueryString["fyClosingPriceDateFrom"]);
        DateTime DateTo = Convert.ToDateTime(Request.QueryString["fyClosingPriceDateTo"]);
        FY = DateFrom.Year.ToString() + " - " + DateTo.Year.ToString();
       
        if (Request.QueryString["fyClosingPriceDateFrom"] != "")
        {
            fyClosingPriceDateFrom = Request.QueryString["fyClosingPriceDateFrom"].ToString();
        }
        if (Request.QueryString["fyClosingPriceDateTo"] != "")
        {
            fyClosingPriceDateTo = Request.QueryString["fyClosingPriceDateTo"].ToString();
        }
        if (Request.QueryString["latestClosingPriceDate"] != "")
        {
            latestClosingPriceDate = Request.QueryString["latestClosingPriceDate"].ToString();
        }
        
        DataTable dtReprtSource1 = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT A.COMP_CD, A.F_NAME, A.MAX_FY, A.MIN_FY, B.MAX_WL, B.MIN_WL, C.MAX_MIN_CURR_DT ");
        sbMst.Append("FROM (SELECT FUND.COMP_CD, FUND.F_NAME, MAX(MARKET_PRICE.AVG_RT) AS MAX_FY,");
        sbMst.Append("MIN(MARKET_PRICE.AVG_RT) AS MIN_FY FROM FUND INNER JOIN ");
        sbMst.Append("MARKET_PRICE ON FUND.COMP_CD = MARKET_PRICE.COMP_CD ");
        sbMst.Append("WHERE (FUND.COMP_CD = MARKET_PRICE.COMP_CD) AND (MARKET_PRICE.AVG_RT <> 10) AND (MARKET_PRICE.TRAN_DATE >= '" + Convert.ToDateTime(Request.QueryString["fyClosingPriceDateFrom"]).ToString("dd-MMM-yyyy") + "') ");
        sbMst.Append("AND (MARKET_PRICE.TRAN_DATE <= '" + Convert.ToDateTime(Request.QueryString["fyClosingPriceDateTo"]).ToString("dd-MMM-yyyy") + "') ");
        sbMst.Append("GROUP BY FUND.COMP_CD, FUND.F_NAME) A INNER JOIN ");
        sbMst.Append("(SELECT FUND_1.COMP_CD, FUND_1.F_NAME, MAX(MARKET_PRICE_1.AVG_RT) AS MAX_WL, MIN(MARKET_PRICE_1.AVG_RT) AS MIN_WL ");
        sbMst.Append("FROM FUND FUND_1 INNER JOIN ");
        sbMst.Append("MARKET_PRICE MARKET_PRICE_1 ON FUND_1.COMP_CD = MARKET_PRICE_1.COMP_CD ");
        sbMst.Append("WHERE (FUND_1.COMP_CD = MARKET_PRICE_1.COMP_CD) AND (MARKET_PRICE_1.AVG_RT <> 10) ");
        sbMst.Append("GROUP BY FUND_1.COMP_CD, FUND_1.F_NAME) B ON A.COMP_CD = B.COMP_CD INNER JOIN ");
        sbMst.Append("(SELECT FUND_2.COMP_CD, FUND_2.F_NAME, MAX(MARKET_PRICE_2.AVG_RT) AS MAX_MIN_CURR_DT ");
        sbMst.Append("FROM FUND FUND_2 INNER JOIN ");
        sbMst.Append("MARKET_PRICE MARKET_PRICE_2 ON FUND_2.COMP_CD = MARKET_PRICE_2.COMP_CD ");
        sbMst.Append("WHERE (FUND_2.COMP_CD = MARKET_PRICE_2.COMP_CD) AND (MARKET_PRICE_2.TRAN_DATE = '" + Convert.ToDateTime(Request.QueryString["latestClosingPriceDate"]).ToString("dd-MMM-yyyy") + "') ");
        sbMst.Append("GROUP BY FUND_2.COMP_CD, FUND_2.F_NAME) C ON A.COMP_CD = C.COMP_CD");
        
        dtReprtSource1 = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource1.TableName = "MaxMinCosingPriceReport";

        
        if (dtReprtSource1.Rows.Count > 0)
        {
            //dtReprtSource1.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmMaxMinCosingPriceReport.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/MaxMinCosingPriceReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource1);
            CrystalReportViewer1.ReportSource = rdoc;
            CrystalReportViewer1.DisplayToolbar = true;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
            rdoc.SetParameterValue("prmFY", FY);
            rdoc.SetParameterValue("prmLatestDate", latestClosingPriceDate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CrystalReportViewer1.Dispose();
        CrystalReportViewer1 = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
