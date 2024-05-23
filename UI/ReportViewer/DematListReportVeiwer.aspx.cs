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
        string blncdate = "";
        string CompanyName = "";

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundcode = (string)Session["fundcode"];
            companycode = (string)Session["companycode"];
            blncdate = (string)Session["blncdate"];
            companycode = (string)Session["companycode"];
        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("select  b.f_name, a.folio_no, a.cert_no, a.dmat_no, a.dmat_dt, a.allot_no, a.dis_no_fm,a.dis_no_to, a.no_shares, a.sp_date, substr(a.sh_type,1,1) sh_tp ");
        sbMst.Append(" from shr_dmat_fi  a, fund b where a.comp_cd = '"+companycode+"' and a.f_cd =b.f_cd and a.posted is null and a.dmat_dt  ='"+ blncdate + "'  and    a.f_cd = '"+fundcode+"' ");
        sbMst.Append(" order by a.dmat_dt, c_dt, cert_no ");
       
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "DematListReport";
            // dtReprtSource.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtPortfolioWithNonListedReport.xsd");
           // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_DematListReport.xsd");
            ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtDematListReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRV_DematListReport.ReportSource = rdoc;
            CRV_DematListReport.DisplayToolbar = true;
            CRV_DematListReport.HasExportButton = true;
            CRV_DematListReport.HasPrintButton = true;
            rdoc.SetParameterValue("prmFromDate", CompanyName);
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
        CRV_DematListReport.Dispose();
        CRV_DematListReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
