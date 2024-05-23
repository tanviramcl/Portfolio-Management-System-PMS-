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

public partial class UI_ReportViewer_NAVvsMarketPriceReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
   // Pf1s1DAO pf1s1DAOObj = new Pf1s1DAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }

        //string navDateCr = Convert.ToDateTime(Request.QueryString["navDate"]).ToString("dd-MMM-yyyy");
        //string marketPriceDateCr = Convert.ToDateTime(Request.QueryString["marketPriceDate"]).ToString("dd-MMM-yyyy");
        int diffSellPurchaseAmountCr = Convert.ToInt16(Request.QueryString["diffSellPurchaseAmount"]);

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT A.F_CD, A.F_NAME, A.NAVDATE, A.NAV_COST_PRICE_PER_UNIT, A.NAV_MARKET_PRICE_PER_UNIT, A.TRAN_DATE, A.MARKET_PRICE FROM  ");
        sbMst.Append("((SELECT     FUND.F_CD, FUND.F_NAME, NAV.NAV_MASTER.NAVDATE, ");
        sbMst.Append("ROUND(NAV.NAV_MASTER.NAVTOTALCOSTPRICE / NAV.NAV_MASTER.NAVTOTALNOOFUNIT,2) AS NAV_COST_PRICE_PER_UNIT, ");
        sbMst.Append("ROUND(NAV.NAV_MASTER.NAVTOTALMARKETPRICE / NAV.NAV_MASTER.NAVTOTALNOOFUNIT,2) AS NAV_MARKET_PRICE_PER_UNIT,  ");
        sbMst.Append("MARKET_PRICE.TRAN_DATE, ROUND(MARKET_PRICE.AVG_RT,2) AS MARKET_PRICE ");
        sbMst.Append("FROM         FUND INNER JOIN  ");
        sbMst.Append("NAV.NAV_MASTER ON FUND.F_CD = NAV.NAV_MASTER.NAVFUNDID INNER JOIN ");
        sbMst.Append("MARKET_PRICE ON FUND.COMP_CD = MARKET_PRICE.COMP_CD ");
        sbMst.Append("WHERE     (NAV.NAV_MASTER.NAVDATE ='" + Convert.ToDateTime(Request.QueryString["navDate"]).ToString("dd-MMM-yyyy") + "')  ");
        sbMst.Append("AND (MARKET_PRICE.TRAN_DATE = '" + Convert.ToDateTime(Request.QueryString["marketPriceDate"]).ToString("dd-MMM-yyyy") + "') AND (FUND.F_TYPE = 'CLOSE END') ");
        sbMst.Append(") UNION  ");
        sbMst.Append("(SELECT     FUND.F_CD, FUND.F_NAME, NAV.NAV_MASTER.NAVDATE, ");
        sbMst.Append("ROUND(NAV.NAV_MASTER.NAVTOTALCOSTPRICE / NAV.NAV_MASTER.NAVTOTALNOOFUNIT,2) AS NAV_COST_PRICE_PER_UNIT, ");
        sbMst.Append("ROUND(NAV.NAV_MASTER.NAVTOTALMARKETPRICE / NAV.NAV_MASTER.NAVTOTALNOOFUNIT,2) AS NAV_MARKET_PRICE_PER_UNIT,  ");
        sbMst.Append("TO_DATE('" + Convert.ToDateTime(Request.QueryString["marketPriceDate"]).ToString("dd-MMM-yyyy") + "', 'DD/MM/YYYY') AS TRAN_DATE,  ");
        sbMst.Append("TO_NUMBER(ROUND((NAV.NAV_MASTER.NAVTOTALMARKETPRICE / NAV.NAV_MASTER.NAVTOTALNOOFUNIT)+"+diffSellPurchaseAmountCr+")) AS MARKET_PRICE  ");
        sbMst.Append("FROM         FUND INNER JOIN  ");
        sbMst.Append("NAV.NAV_MASTER ON FUND.F_CD = NAV.NAV_MASTER.NAVFUNDID  ");
        sbMst.Append("WHERE     (NAV.NAV_MASTER.NAVDATE = '" + Convert.ToDateTime(Request.QueryString["navDate"]).ToString("dd-MMM-yyyy")+ "') AND (FUND.F_TYPE = 'OPEN END')  ");
        sbMst.Append("))A  ");
        sbMst.Append("ORDER BY F_CD");

        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "NAVvsMarketPricereport";

        if (dtReprtSource.Rows.Count > 0)
        {
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmNAVvsMarketPrice.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/NAVvsMarketPrice.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_NAVvsMarketPrice.ReportSource = rdoc;
            CRV_NAVvsMarketPrice.DisplayToolbar = true;
            CRV_NAVvsMarketPrice.HasExportButton = true;
            CRV_NAVvsMarketPrice.HasPrintButton = true;
            //rdoc.SetParameterValue("prmFY", FY);
            //rdoc.SetParameterValue("prmLetterPrintDate", letterPrintDateCr);
            //rdoc.SetParameterValue("prmNameOfPerson", nameOfPerson);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_NAVvsMarketPrice.Dispose();
        CRV_NAVvsMarketPrice = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
