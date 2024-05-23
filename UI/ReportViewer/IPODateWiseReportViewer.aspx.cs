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

public partial class UI_ReportViewer_IPODateWiseReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    //Pf1s1DAO pf1s1DAOObj = new Pf1s1DAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }

        string howlaDateFrom = "";
        string howlaDateTo = "";

        if (Request.QueryString["howlaDateFrom"] != "")
        {
            howlaDateFrom = Request.QueryString["howlaDateFrom"].ToString();
        }
        if (Request.QueryString["howlaDateTo"] != "")
        {
            howlaDateTo = Request.QueryString["howlaDateTo"].ToString();
        }

        DataTable dtReprtSource = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");

        dtReprtSource.TableName = "IPO_DateWiseReport";

        sbMst.Append("SELECT     COMP.COMP_CD, COMP.COMP_NM || ' (' || COMP.COMP_CD || ')' as COMP_NM , FUND.F_NAME, FUND_TRANS_HB.VCH_DT,");
        sbMst.Append("FUND_TRANS_HB.NO_SHARE, FUND_TRANS_HB.AMOUNT, FUND_TRANS_HB.AMT_AFT_COM ");
        sbMst.Append("FROM COMP INNER JOIN ");
        sbMst.Append("FUND_TRANS_HB ON COMP.COMP_CD = FUND_TRANS_HB.COMP_CD INNER JOIN");
        sbMst.Append(" FUND ON FUND_TRANS_HB.F_CD = FUND.F_CD WHERE(FUND_TRANS_HB.TRAN_TP = 'I')");

        if (howlaDateFrom != "" && howlaDateTo == "")
        {
            sbfilter.Append(" AND  (FUND_TRANS_HB.VCH_DT >='" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "')");
        }
        else if (howlaDateFrom == "" && howlaDateTo != "")
        {
            sbfilter.Append(" AND  (FUND_TRANS_HB.VCH_DT <='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd-MMM-yyyy") + "')");
        }
        else if (howlaDateFrom != "" && howlaDateTo != "")
        {
            sbfilter.Append(" AND  (FUND_TRANS_HB.VCH_DT >='" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "') AND  (FUND_TRANS_HB.VCH_DT <='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd-MMM-yyyy") + "')");
        }


        sbMst.Append(sbfilter.ToString());
        sbMst.Append(" ORDER BY FUND_TRANS_HB.VCH_DT, FUND.F_CD");
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "IPO_DateWiseReport";


        if (dtReprtSource.Rows.Count > 0)
        {
            //dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmIPO_DateWiseReport.xsd");
            
            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/IPO_DateWise.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CrystalReportViewer1.ReportSource = rdoc;
            CrystalReportViewer1.DisplayToolbar = true;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
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
