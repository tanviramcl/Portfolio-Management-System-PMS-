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
using System.Text;

public partial class UI_ReportViewer_ReceivableCashDividendReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string recordDateFrom = "";
        string recordDateTo = "";
        string agmDateFrom = "";
        string agmDateTo = "";
        string fundCode = "";
        
        DataTable dtIntimationReport = new DataTable();

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            recordDateFrom = (string)Session["recordDateFrom"];
            recordDateTo = (string)Session["recordDateTo"];
            agmDateFrom = (string)Session["agmDateFrom"];
            agmDateTo = (string)Session["agmDateTo"];
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        sbMst.Append("select a.*,b.DIVI_RECEIVAABLE,b.DUE_EXCESS_DIVIDEND as FinalDividend,B.DIVI_RECEIVE_DATE,B.DIVI_REMARKS from   (  ");
        sbMst.Append(" SELECT    FUND.F_CD, FUND.F_NAME,COMP.COMP_CD ,COMP.COMP_NM,  BOOK_CL.AGM, BOOK_CL.RECORD_DT, COMP.FC_VAL, BOOK_CL.CASH, ");
        sbMst.Append(" COMP.FC_VAL * BOOK_CL.CASH / 100 AS DIVIDEND_PER_SHARE, PFOLIO_BK.TOT_NOS,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH AS GROSS_DIVIDEND, decode(FUND.F_CD, 1, ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .2, 0) AS TAX, decode(FUND.F_CD, 1,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .8,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH) AS NET_DIVIDEND ");
        sbMst.Append(" FROM  BOOK_CL INNER JOIN ");
        sbMst.Append(" COMP ON BOOK_CL.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND INNER JOIN ");
        sbMst.Append(" PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON BOOK_CL.COMP_CD = PFOLIO_BK.COMP_CD AND ");
        sbMst.Append(" BOOK_CL.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL ");
        sbMst.Append(" WHERE       (BOOK_CL.CASH IS NOT NULL) ) a ");
        sbMst.Append("  left outer JOIN RECEIVABLECASH_DIVIDEND  b ON a.F_CD = b.F_CD and a.comp_cd=b.comp_cd     ");

        //if ((recordDateFrom != "") && (recordDateTo != ""))
        //{
        //    sbMst.Append(" AND (BOOK_CL.RECORD_DT BETWEEN '"+recordDateFrom+"' AND '"+recordDateTo+"')");
        //}

        //if ((agmDateFrom != "") && (agmDateTo == ""))
        //{
        //    sbMst.Append(" AND (BOOK_CL.AGM >= '" + agmDateFrom + "')");
        //}
        //else if ((agmDateFrom == "") && (agmDateTo != ""))
        //{
        //    sbMst.Append(" AND (BOOK_CL.AGM <= '" + agmDateTo + "')");
        //}
        //else if ((agmDateFrom != "") && (agmDateTo != ""))
        //{
        //    sbMst.Append(" AND (BOOK_CL.AGM BETWEEN '" + agmDateFrom + "' AND '" + agmDateTo + "')");
        //}

        //if (fundCode != "0")
        //{
        //    sbMst.Append(" AND (FUND.F_CD = " + Convert.ToInt16(fundCode.ToString()) + ")");
        //}
        //sbMst.Append(" ORDER BY 2 ");

        if ((recordDateFrom != "") && (recordDateTo != ""))
        {
            sbMst.Append(" Where a.RECORD_DT BETWEEN '" + recordDateFrom + "' AND '" + recordDateTo + "'");
        }

        if ((agmDateFrom != "") && (agmDateTo == ""))
        {
            sbMst.Append(" AND a.AGM >= '" + agmDateFrom + "'");
        }
        else if ((agmDateFrom == "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND a.AGM <= '" + agmDateTo + "'");
        }
        else if ((agmDateFrom != "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND a.AGM BETWEEN '" + agmDateFrom + "' AND '" + agmDateTo + "'");
        }

        if (fundCode != "0")
        {
            sbMst.Append(" AND a.F_CD = " + Convert.ToInt16(fundCode.ToString()) + "");
        }
 //       sbMst.Append(" ORDER BY 2 ");

        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "crtReceivableCashDividendReport_final_1";
           // dtReprtSource.WriteXmlSchema(@"E:\PFMS\amclinvanalysisandpfms\UI\ReportViewer\Report\crtReceivableCashDividendReport_final_1.xsd");
            string Path = "";
            Path = Server.MapPath("Report/crtDividendStatusReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            crtDividendStatusReport.ReportSource = rdoc;
            crtDividendStatusReport.DisplayToolbar = true;
            crtDividendStatusReport.HasExportButton = true;
            crtDividendStatusReport.HasPrintButton = true;
            rdoc.SetParameterValue("prmRecordDateFrom", recordDateFrom);
            rdoc.SetParameterValue("prmRecordDateTo", recordDateTo);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        crtDividendStatusReport.Dispose();
        crtDividendStatusReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
