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
using CrystalDecisions.Shared;
public partial class UI_ReportViewer_NegativeBalanceCheckReportViewer : System.Web.UI.Page
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

        string p1date = Convert.ToString(Request.QueryString["p1date"]).Trim();
        string p2date = Convert.ToString(Request.QueryString["p2date"]).Trim();

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("select f.F_CD, f_name  fund_name,f.COMP_CD, c.comp_nm,I_NO_SHR,O_NO_SHR,TOT_NOS,I_RATE,O_RATE,IRT_AFT_COM,ORT_AFT_COM,TOT_COST,TCST_AFT_COM,BAL_DT,f.OP_NAME");
        sbMst.Append(" from fund_folio_hb f , comp c, fund n where tot_nos <= 0 and bal_dt between '" + p1date + "' and '" + p2date + "' and c.comp_cd = f.comp_cd and f.f_cd = n.f_cd order by f.bal_dt");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "NegativeBalanceCheck";
        //dtReprtSource.WriteXmlSchema(@"D:\officialProject\1-8-17\amclpmfs\UI\ReportViewer\Report\crtNegativeBalanceCheckReport.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {

            //string Path = Server.MapPath("~/Report/crtNegativeBalanceCheckReport.rpt");
            string Path = Server.MapPath("Report/CRNegativeBalanceCheck.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_NegativeBalanceCheck.ReportSource = rdoc;
            CR_NegativeBalanceCheck.DisplayToolbar = true;
            CR_NegativeBalanceCheck.HasExportButton = true;
            CR_NegativeBalanceCheck.HasPrintButton = true;
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
        CR_NegativeBalanceCheck.Dispose();
        CR_NegativeBalanceCheck = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}