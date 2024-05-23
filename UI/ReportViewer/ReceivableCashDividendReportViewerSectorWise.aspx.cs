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
       
        sbMst.Append(" select a.*,b.SECT_MAJ_NM from  ( SELECT     FUND.F_NAME, COMP.COMP_NM,COMP.SECT_MAJ_CD , BOOK_CL.AGM, BOOK_CL.RECORD_DT, COMP.FC_VAL, BOOK_CL.CASH, ");
        sbMst.Append(" COMP.FC_VAL * BOOK_CL.CASH / 100 AS DIVIDEND_PER_SHARE, PFOLIO_BK.TOT_NOS,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH AS GROSS_DIVIDEND, decode(FUND.F_CD, 1, ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .2, 0) AS TAX, decode(FUND.F_CD, 1,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .8,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH) AS NET_DIVIDEND ");
        sbMst.Append(" FROM         BOOK_CL INNER JOIN ");
        sbMst.Append(" COMP ON BOOK_CL.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND INNER JOIN ");
        sbMst.Append(" PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON BOOK_CL.COMP_CD = PFOLIO_BK.COMP_CD AND ");
        sbMst.Append(" BOOK_CL.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL ");
        sbMst.Append(" WHERE       (BOOK_CL.CASH IS NOT NULL) ");

        if ((recordDateFrom != "") && (recordDateTo != ""))
        {
            sbMst.Append(" AND (BOOK_CL.RECORD_DT BETWEEN '"+recordDateFrom+"' AND '"+recordDateTo+"')");
        }
        
        if ((agmDateFrom != "") && (agmDateTo == ""))
        {
            sbMst.Append(" AND (BOOK_CL.AGM >= '" + agmDateFrom + "')");
        }
        else if ((agmDateFrom == "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (BOOK_CL.AGM <= '" + agmDateTo + "')");
        }
        else if ((agmDateFrom != "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (BOOK_CL.AGM BETWEEN '" + agmDateFrom + "' AND '" + agmDateTo + "')");
        }

        if (fundCode != "0")
        {
            sbMst.Append(" AND (FUND.F_CD = " + Convert.ToInt16(fundCode.ToString()) + ")");
        }
        sbMst.Append(" ORDER BY AGM ASC  ) a inner join SECT_MAJ b on a.SECT_MAJ_CD=B.SECT_MAJ_CD ");
         
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "ReceivableCashDividendReport";
          //  dtReprtSource.WriteXmlSchema(@"D:\Development\PFMS_LIVE\amcl\amclinvanalysisandpfms\UI\ReportViewer\Report\crtReceivableCashDividendReport_sector.xsd");
            string Path = "";
            Path = Server.MapPath("Report/crtReceivableCashDividendReport_sec.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_ReceivableCashDividend.ReportSource = rdoc;
            CRV_ReceivableCashDividend.DisplayToolbar = true;
            CRV_ReceivableCashDividend.HasExportButton = true;
            CRV_ReceivableCashDividend.HasPrintButton = true;
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
        CRV_ReceivableCashDividend.Dispose();
        CRV_ReceivableCashDividend = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
