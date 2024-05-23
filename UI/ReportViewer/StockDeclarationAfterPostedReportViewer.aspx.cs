using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_StockDeclarationBeforePostedReportViewer : System.Web.UI.Page
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

        
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT C.COMP_NM,B.COMP_CD, floor(P.TOT_NOS) tot_nos, p.f_cd,  f.f_name f_name,floor(P.TOT_NOS * B.BONUS / 100) share_alloted,B.FY, B.RECORD_DT, B.BOOK_TO, B.BONUS, B.RIGHT_APPR_DT, B.RIGHT, B.CASH, B.AGM, B.REMARKS, B.POSTED, b.pdate  FROM BOOK_CL B, COMP C, PFOLIO_BK P, fund f WHERE B.COMP_CD = P.COMP_CD AND B.COMP_CD = C.COMP_CD AND P.BAL_DT_CTRL = B.RECORD_DT and B.BONUS is not null and posted is not null and f.IS_F_CLOSE is null and  p.f_cd = f.f_cd order by  C.COMP_NM");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "StockDeclarationAFterPostedReport";
        //dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\crtStockDeclarationAfterPostedReport.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {
            //string Path = Server.MapPath("~/Report/crtNegativeBalanceCheckReport.rpt");
            string Path = Server.MapPath("Report/StockDeclarationAfterPostedReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_StockDeclarationAfterPostedReport.ReportSource = rdoc;
            CR_StockDeclarationAfterPostedReport.DisplayToolbar = true;
            CR_StockDeclarationAfterPostedReport.HasExportButton = true;
            CR_StockDeclarationAfterPostedReport.HasPrintButton = true;
            //rdoc.SetParameterValue("prmtransTypeDetais", transTypeDetais);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else
        {
            Response.Write("No Data Found");
        }


    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_StockDeclarationAfterPostedReport.Dispose();
        CR_StockDeclarationAfterPostedReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}