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

            sbMst.Append("SELECT A.F_CD, A.VCH_DT,A.FUND_NAME, A.STOCK_EX,A.BUY,A.SALE ,DECODE(SIGN(A.SALE-A.BUY),1,A.SALE-A.BUY) AS RECEIVABLE,DECODE(SIGN(A.SALE-A.BUY),-1,A.SALE-A.BUY) AS PAYABLE FROM( ");
            sbMst.Append(" select t.f_cd,vch_dt,f.f_name fund_name, stock_ex,sum(decode(TRAN_TP,'C',AMT_AFT_COM,0)) buy,sum(decode(TRAN_TP,'S',AMT_AFT_COM,0)) Sale, ");
            sbMst.Append(" sum(decode(TRAN_TP,'C', -AMT_AFT_COM,'S',AMT_AFT_COM,0)) Receivable from  fund_trans_hb t, fund f where vch_dt between '"+Fromdate+"' and '"+Todate+"'");
            sbMst.Append(" and t.f_cd='"+fundCode+ "' and f.f_cd=t.f_cd and tran_tp<>'B' and no_share>=1 and stock_ex in ('D','C') group by f.f_name,t.f_cd,vch_dt, stock_ex order by vch_dt  ) A");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "ReceivablePayableDSEandCSESummarySeparate";
          //  dtReprtSource.WriteXmlSchema(@"D:\PFMS\amclinvanalysisandpfms\UI\ReportViewer\Report\CR_ReceivablePayableFundwiseSummaryReportVeiwer.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_ReceivablePayableFundwiseSummeryReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_ReceivablePayableFundwiseReportVeiwer.ReportSource = rdoc;
                CR_ReceivablePayableFundwiseReportVeiwer.DisplayToolbar = true;
                CR_ReceivablePayableFundwiseReportVeiwer.HasExportButton = true;
                CR_ReceivablePayableFundwiseReportVeiwer.HasPrintButton = true;
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
        CR_ReceivablePayableFundwiseReportVeiwer.Dispose();
        CR_ReceivablePayableFundwiseReportVeiwer = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }



}