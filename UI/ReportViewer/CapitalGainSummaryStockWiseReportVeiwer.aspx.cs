using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_NonDemateSharesCheckReportViwer : System.Web.UI.Page
{

    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

        string Fromdate = "";
        string Todate = "";
       
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else {
            Fromdate = (string)Session["Fromdate"];
            Todate = (string)Session["Todate"];
          

        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");

       
            sbMst.Append("SELECT  t.F_CD, f.f_name fund_name, SUM(t.AMT_AFT_COM) as SALE, sum(t.no_share) as No_Of_Share, sum(crt_aft_com * no_share) as COST, ");
            sbMst.Append(" SUM(AMT_AFT_COM) - sum(crt_aft_com * no_share) as profit, decode(stock_ex, 'D', 'DSE', 'C', 'CSE','OTHERS') stock_ex FROM FUND_TRANS_HB  t, fund f  ");
            sbMst.Append(" WHERE t.TRAN_TP = 'S' AND VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"'  and stock_ex in ('D', 'C','A') and f.f_cd = t.f_cd GROUP BY t.F_CD,stock_ex,f.f_name  order by t.f_cd asc, stock_ex desc ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "CapitalGainSummaryStockWise";
           // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_CapitalGainSummaryStockWiseReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_CapitalGainSummaryStockWiseReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_CapitalGainSummaryStockWiseReport.ReportSource = rdoc;
                CR_CapitalGainSummaryStockWiseReport.DisplayToolbar = true;
                CR_CapitalGainSummaryStockWiseReport.HasExportButton = true;
                CR_CapitalGainSummaryStockWiseReport.HasPrintButton = true;
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
        CR_CapitalGainSummaryStockWiseReport.Dispose();
        CR_CapitalGainSummaryStockWiseReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }



}