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

public partial class UI_ReportViewer_FundTransactionReportViewer : System.Web.UI.Page
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
        sbMst.Append("select fc.f_cd, TO_CHAR( bal_dt, 'DD.MM.YYYY') AS BalanceDate ,TO_CHAR( mprice_dt, 'DD.MM.YYYY') AS MarketPriceDate ,f_name from fund_control fc, fund f where fc.f_cd = f.f_cd order by fc.f_cd");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "BalanceCheckDataSet";
        //dtReprtSource.WriteXmlSchema(@"D:\officialProject\1-8-17\amclpmfs\UI\ReportViewer\Report\crtBalanceCheck.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {

            string Path = Server.MapPath("Report/CRBalanceCheck.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_BalanceCheck.ReportSource = rdoc;
            CR_BalanceCheck.DisplayToolbar = true;
            CR_BalanceCheck.HasExportButton = true;
            CR_BalanceCheck.HasPrintButton = true;
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
        CR_BalanceCheck.Dispose();
        CR_BalanceCheck = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
