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
        string fundcode = "";
        string companycode = "";
     //   string CompanyName = "";

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundcode = (string)Session["fundcode"];
            companycode = (string)Session["companycode"];
          
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT a.COMP_CD,B.COMP_NM, a.PSDR_NO,a. F_CD, C.F_NAME, a.ALLOT_NO, a.NO_SHARES, substr( a.SH_TYPE,1,1) SH_TYPE, a.OM_LOT, a.SP_RATE, a.SP_DATE, a.HOWLA_NO, a.MV_DATE, a.REF_NO, a.DIS_NO_FM, a.DIS_NO_TO, a.FOLIO_NO, ");
        sbMst.Append("  a.CERT_NO, a.BK_CD, a.POSTED, a.OP_NAME, a.C_DT, a.C_DATE FROM INVEST.PSDR_FI a,COMP b,FUND c where a.comp_cd = '" + companycode+ "' and  a.f_cd =c.f_cd and a.posted= 'A' and A.FOLIO_NO  is not null and  a.comp_cd=B.COMP_CD and    a.f_cd = '" + fundcode+"' ");
        sbMst.Append(" ORDER BY  a.C_DT DESC ");
       
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "PSDRListReport";
       //     dtReprtSource.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\crtPSDRListReport.xsd");
           // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_DematListReport.xsd");
            ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtPSDRListReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_PSDRListReport.ReportSource = rdoc;
            CRV_PSDRListReport.DisplayToolbar = true;
            CRV_PSDRListReport.HasExportButton = true;
            CRV_PSDRListReport.HasPrintButton = true;
            //rdoc.SetParameterValue("prmFromDate", CompanyName);
            rdoc.SetParameterValue("prmFundCode", fundcode);
            rdoc.SetParameterValue("prmcompanycode", companycode);

        rdoc = ReportFactory.GetReport(rdoc.GetType());
    }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_PSDRListReport.Dispose();
        CRV_PSDRListReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
