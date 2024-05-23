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

        string fundcode = Request.QueryString["fundcode"].ToString();
        string Fromdate = Request.QueryString["Fromdate"].ToString();
        string Todate = Request.QueryString["Todate"].ToString();
        string transtype = Request.QueryString["transtype"].ToString();



        sbfilter.Append(" ");
        sbMst.Append("SELECT  f.comp_cd, c.comp_nm, fund.f_name fund_name, SUM(AMT_AFT_COM) as purchase, sum(no_share) as No_Of_Share,Round(SUM(CRT_AFT_COM * NO_SHARE),2) AS COST_PRICE," +
                     " ROUND(SUM(AMT_AFT_COM) / sum(no_share),2) as sale_rate, Round(SUM(CRT_AFT_COM * NO_SHARE) / sum(no_share),2) as cost_rate, ROUND(SUM(AMT_AFT_COM) - SUM(CRT_AFT_COM * NO_SHARE)) PROFIT "+
                     " FROM FUND_TRANS_HB f, comp c, fund WHERE F.TRAN_TP = '"+ transtype + "' AND VCH_DT BETWEEN '"+Fromdate+"'AND '"+Todate+"' and f.f_cd ='"+fundcode+"' and c.comp_cd = f.comp_cd and f.f_cd = fund.f_cd GROUP BY fund.f_name, f.comp_cd, c.comp_nm");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "CapitalGainCompanyWiseNewReport";
      //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\CR_CapitalGainCompanyWiseNewReport.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {
            //string Path = Server.MapPath("~/Report/crtNegativeBalanceCheckReport.rpt");
            string Path = Server.MapPath("Report/CR_CapitalGainCompanyWiseNewReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_CapitalGainCompanyWiseNewReport.ReportSource = rdoc;
            CR_CapitalGainCompanyWiseNewReport.DisplayToolbar = true;
            CR_CapitalGainCompanyWiseNewReport.HasExportButton = true;
            CR_CapitalGainCompanyWiseNewReport.HasPrintButton = true;
            rdoc.SetParameterValue("Fromdate", Fromdate);
            rdoc.SetParameterValue("Todate", Todate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else
        {
            Response.Write("No Data Found");
        }


    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_CapitalGainCompanyWiseNewReport.Dispose();
        CR_CapitalGainCompanyWiseNewReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}