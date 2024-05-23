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
using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using System.Text;

public partial class UI_ReportViewer_GeneralReportViewer : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string closingPriceDateFrom = "";
        string closingPriceDateTo = "";
        string yearEndFrom = "";
        string yearEndTo = "";
        string marketCategory = "";
        string orderBy = "";

        int sectorCode = Convert.ToInt32(Request.QueryString["sectorCode"]);

        if (Request.QueryString["closingPriceDateFrom"] != "")
        {
            closingPriceDateFrom = Request.QueryString["closingPriceDateFrom"].ToString();
        }
        if (Request.QueryString["closingPriceDateEnd"] != "")
        {
            closingPriceDateTo = Request.QueryString["closingPriceDateEnd"].ToString();
        }
        if (Request.QueryString["yearEndFrom"] != "")
        {
            yearEndFrom = Request.QueryString["yearEndFrom"].ToString();
        }
        if (Request.QueryString["yearEndTo"] != "")
        {
            yearEndTo = Request.QueryString["yearEndTo"].ToString();
        }
        if (Request.QueryString["marketCategory"] != "")
        {
            marketCategory = Request.QueryString["marketCategory"].ToString();
        }
        if (Request.QueryString["orderby"] != "")
        {
            orderBy = Request.QueryString["orderby"].ToString();
        }
        
        
        DataTable dtReprtSource = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");

        dtReprtSource.TableName = "Report";

        //sbMst.Append("SELECT COMP.COMP_NM, BOOK_CL.COMP_CD, BOOK_CL.FY, BOOK_CL.RECORD_DT, BOOK_CL.BOOK_TO, BOOK_CL.BONUS, BOOK_CL.RIGHT_APPR_DT, BOOK_CL.RIGHT,");
        //sbMst.Append("BOOK_CL.CASH, BOOK_CL.AGM, BOOK_CL.REMARKS, BOOK_CL.POSTED, BOOK_CL.PDATE FROM COMP INNER JOIN BOOK_CL ON COMP.COMP_CD = BOOK_CL.COMP_CD WHERE(1 = 1)");

        //if (entryDate != "" && toEntryDate == "")
        //{
        //    sbfilter.Append(" AND  (BOOK_CL.ENTRY_DATE >='" + Convert.ToDateTime(Request.QueryString["entryDate"]).ToString("dd-MMM-yyyy") + "')");
        //}
        //else if (entryDate == "" && toEntryDate != "")
        //{
        //    sbfilter.Append(" AND  (BOOK_CL.ENTRY_DATE <='" + Convert.ToDateTime(Request.QueryString["toEntryDate"]).ToString("dd-MMM-yyyy") + "')");
        //}
        //else if (entryDate != "" && toEntryDate != "")
        //{
        //    sbfilter.Append(" AND  (BOOK_CL.ENTRY_DATE >='" + Convert.ToDateTime(Request.QueryString["entryDate"]).ToString("dd-MMM-yyyy") + "') AND  (BOOK_CL.ENTRY_DATE <='" + Convert.ToDateTime(Request.QueryString["toEntryDate"]).ToString("dd-MMM-yyyy") + "')");
        //}

        //if (comCode != 0)
        //{
        //    sbfilter.Append(" AND (BOOK_CL.COMP_CD =" + comCode + ")");
        //}

        //sbMst.Append(sbfilter.ToString());
        //sbMst.Append(" ORDER BY TO_NUMBER(COMP.COMP_CD)");
        //dtReprtSource = commonGatewayObj.Select(sbMst.ToString());


        //if (dtReprtSource.Rows.Count > 0)
        //{
        //    //dtReprtSource.WriteXmlSchema(@"G:\AMCL.BookCloser\UI\ReportViewer\Report\crtmReport.xsd");


        //    ReportDocument rdoc = new ReportDocument();
        //    string Path = Server.MapPath("Report/crtmReport.rpt");
        //    rdoc.Load(Path);
        //    rdoc.SetDataSource(dtReprtSource);
        //    CrystalReportViewer1.ReportSource = rdoc;
        //    CrystalReportViewer1.DisplayToolbar = true;
        //    CrystalReportViewer1.HasExportButton = true;
        //    CrystalReportViewer1.HasPrintButton = true;

        //}
        //else
        //{
        //    Response.Write("No Data Found");
        //}
    }

}
