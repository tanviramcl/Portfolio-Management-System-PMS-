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

       
        string Fromdate = Request.QueryString["Fromdate"].ToString();
        string Todate = Request.QueryString["Todate"].ToString();
       
        sbMst.Append("SELECT  SUM(AMT_AFT_COM) as SALE, sum(no_share) as No_Of_Share, sum(crt_aft_com * no_share) as COST ,SUM(AMT_AFT_COM) - sum(crt_aft_com *no_share) as profit," +
                     " FUND_TRANS_HB.comp_cd, c.comp_nm FROM FUND_TRANS_HB , comp c WHERE FUND_TRANS_HB.TRAN_TP=  'S' AND VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"' " +
                     " and c.comp_cd=fund_trans_hb.comp_cd and no_share>=1 GROUP BY  fund_trans_hb.comp_cd,c.comp_nm order by SUM(AMT_AFT_COM) - sum(crt_aft_com *no_share) desc ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "CapitalGainSummeryCompanyWiseDscreport";
     //   dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\CapitalGainSummeryCompanyWiseDscreport.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {
            //string Path = Server.MapPath("~/Report/crtNegativeBalanceCheckReport.rpt");
            string Path = Server.MapPath("Report/CapitalGainSummeryCompanyWiseDscreport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_CapitalGainSummeryCompanyWiseDscreportViwerReport.ReportSource = rdoc;
            CR_CapitalGainSummeryCompanyWiseDscreportViwerReport.DisplayToolbar = true;
            CR_CapitalGainSummeryCompanyWiseDscreportViwerReport.HasExportButton = true;
            CR_CapitalGainSummeryCompanyWiseDscreportViwerReport.HasPrintButton = true;
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
        CR_CapitalGainSummeryCompanyWiseDscreportViwerReport.Dispose();
        CR_CapitalGainSummeryCompanyWiseDscreportViwerReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}