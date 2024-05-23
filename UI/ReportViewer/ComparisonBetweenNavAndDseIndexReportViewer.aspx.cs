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
        string Fromdate = "";
        string Todate = "";

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            Fromdate = (string)Session["Fromdate"];
            Todate = (string)Session["Todate"];

            //  balDate = (string)Session["balDate"];
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("  select * from (SELECT  TO_CHAR(NAV_MASTER.NAVDATE,'DD-MON-YYYY')NAV_DATE,NAV_MASTER.NAVDATE ,NAV_MASTER.NAVTOTALMARKETPRICE,  ");
        sbMst.Append(" NAV_MASTER.NAVTOTALCOSTPRICE, NAV_MASTER.NAV_PU_CP, NAV_MASTER.NAV_PU_MP, NAV_MASTER.NAV_PU_MP_AFADL, DSE_MKT_INFO.TOTAL_TRADE, ");
        sbMst.Append("DSE_MKT_INFO.TOTAL_VOLUME, DSE_MKT_INFO.TOTAL_VOLUME_TK_MN,  DSE_MKT_INFO.TOTAL_MARKET_CAP_TK_MN,DSE_MKT_INFO.DSEX_INDEX / 10 AS DSEX_INDEX, ");
        sbMst.Append("  DSE_MKT_INFO.DSES_INDEX, DSE_MKT_INFO.DS30_INDEX,  DSE_MKT_INFO.DGEN_INDEX FROM       nav.NAV_MASTER, nav.DSE_MKT_INFO WHERE NAVFUNDID = 2 AND ");
        sbMst.Append("  NAV_MASTER.NAVDATE = DSE_MKT_INFO.T_DATE AND NAVDATE ");
        sbMst.Append("BETWEEN '"+ Fromdate + "' AND '"+Todate+"') ORDER BY NAVDATE ASC ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

     
        if (dtReprtSource.Rows.Count > 0)
        {
            //Decimal totalInvest = 0;
           
            dtReprtSource.TableName = "ComparisonBetweenNavAndDse";
           // dtReprtSource.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\crtComparisonBetweenNavAndDseIndex.xsd");
            ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtComparisonBetweenNavAndDseIndexReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_PortfolioSummary.ReportSource = rdoc;
            CRV_PortfolioSummary.DisplayToolbar = true;
            CRV_PortfolioSummary.HasExportButton = true;
            CRV_PortfolioSummary.HasPrintButton = true;
       //     rdoc.SetParameterValue("prmbalDate", balDate);
         //   rdoc.SetParameterValue("prmTotalInvest", totalInvest);
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
