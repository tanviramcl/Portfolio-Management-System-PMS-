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
        string fundCode = "";

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else {
            Fromdate = (string)Session["Fromdate"];
            Todate = (string)Session["Todate"];
            fundCode = (string)Session["fundCodes"];


        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");

       
            sbMst.Append("SELECT  fund.f_name,fund_trans_hb.vch_dt, sum(no_share) as No_Of_Share,decode(tran_tp,'C',SUM(AMT_AFT_COM),'I',sum(amt_aft_com),'R',sum(amt_aft_com),0) as SALE, ");
            sbMst.Append(" sum(crt_aft_com * no_share) as COST ,SUM(AMT_AFT_COM) -sum(crt_aft_com *no_share) as profit, tran_tp, decode(stock_ex,'C','CSE','D','DSE','Local') stock_ex, ");
            sbMst.Append(" decode(tran_tp,'S',' Credit ','C',' Debit ') db_cr FROM FUND_TRANS_HB,fund  WHERE  fund_trans_hb.f_cd=fund.f_cd and stock_ex in ('D','C','A') and ");
            sbMst.Append(" VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"' and  fund_trans_hb.F_CD='"+fundCode+"' GROUP BY fund.f_name,  vch_dt ,tran_tp,stock_ex order by vch_dt,stock_ex desc ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "ReconCilationDRandCRReport";
          //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_ReconCilationDRandCRReportVeiwer.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_ReconCilationDRandCRReportVeiwer.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_ReconCilationDRandCRReportVeiwer.ReportSource = rdoc;
                CR_ReconCilationDRandCRReportVeiwer.DisplayToolbar = true;
                CR_ReconCilationDRandCRReportVeiwer.HasExportButton = true;
                CR_ReconCilationDRandCRReportVeiwer.HasPrintButton = true;
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
        CR_ReconCilationDRandCRReportVeiwer.Dispose();
        CR_ReconCilationDRandCRReportVeiwer = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }




}