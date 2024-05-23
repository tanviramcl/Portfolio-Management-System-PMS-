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
        sbMst.Append("SELECT C.COMP_NM,B.COMP_CD,  floor(P.TOT_NOS) tot_nos, p.f_cd, f_name f_name,floor(P.TOT_NOS * B.BONUS / 100) share_alloted,B.FY, B.RECORD_DT, B.BOOK_TO, B.BONUS, B.RIGHT_APPR_DT, B.RIGHT, B.CASH, B.AGM, B.REMARKS, B.POSTED, b.pdate  FROM BOOK_CL B, COMP C, PFOLIO_BK P, fund f WHERE B.COMP_CD = P.COMP_CD AND B.COMP_CD = C.COMP_CD AND P.BAL_DT_CTRL = B.RECORD_DT and B.BONUS is not null and posted is null and f.IS_F_CLOSE is null and p.f_cd = f.f_cd  and f.f_cd not in(7) order by p.f_cd");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "StockDeclarationBeforePostedReport";
        //dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\crtStockDeclarationBeforePostedReport.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {
            //string Path = Server.MapPath("~/Report/crtNegativeBalanceCheckReport.rpt");
            string Path = Server.MapPath("Report/StockDeclarationBeforePostedReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_StockDeclarationBeforePostedReport.ReportSource = rdoc;
            CR_StockDeclarationBeforePostedReport.DisplayToolbar = true;
            CR_StockDeclarationBeforePostedReport.HasExportButton = true;
            CR_StockDeclarationBeforePostedReport.HasPrintButton = true;
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
        CR_StockDeclarationBeforePostedReport.Dispose();
        CR_StockDeclarationBeforePostedReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}