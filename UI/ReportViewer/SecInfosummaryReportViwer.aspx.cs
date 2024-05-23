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
        string transtype = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else {
            Fromdate = (string)Session["Fromdate"];
            Todate = (string)Session["Todate"];
            transtype = (string)Session["transtype"];

        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");

        if (transtype == "C")
        {
            sbMst.Append("SELECT  t.F_CD, f.f_name  fund_name,SUM(AMT_AFT_COM) as COST, sum(no_share) as No_Of_Share FROM FUND_TRANS_HB t, fund f  ");
            sbMst.Append(" WHERE t.TRAN_TP=  '"+transtype+"' AND f.f_cd=t.f_cd and  VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"' GROUP BY t.F_CD, f.f_name   ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SecInfoSummaryCost";
            //dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SecInfoSummaryCostReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_SecInfoSummaryCost.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SecInfosummary.ReportSource = rdoc;
                CR_SecInfosummary.DisplayToolbar = true;
                CR_SecInfosummary.HasExportButton = true;
                CR_SecInfosummary.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("TransType",transtype);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (transtype == "I")
        {
            sbMst.Append("SELECT  t.F_CD, f.f_name  fund_name,SUM(AMT_AFT_COM) as IPO, sum(no_share) as No_Of_Share FROM FUND_TRANS_HB t, fund f   ");
            sbMst.Append(" WHERE t.TRAN_TP=  '" + transtype + "' AND f.f_cd=t.f_cd and  VCH_DT BETWEEN '" + Fromdate + "' AND '" + Todate + "' GROUP BY t.F_CD, f.f_name   ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SecInfoSummaryIPO";
          //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SecInfoSummaryIPOReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_SecInfoSummaryIPO.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SecInfosummary.ReportSource = rdoc;
                CR_SecInfosummary.DisplayToolbar = true;
                CR_SecInfosummary.HasExportButton = true;
                CR_SecInfosummary.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("TransType", transtype);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (transtype == "R")
        {
            sbMst.Append("SELECT  t.F_CD, f.f_name  fund_name,SUM(AMT_AFT_COM) as Right, sum(no_share) as No_Of_Share FROM FUND_TRANS_HB t, fund f      ");
            sbMst.Append(" WHERE t.TRAN_TP=  '" + transtype + "' AND f.f_cd=t.f_cd and  VCH_DT BETWEEN '" + Fromdate + "' AND '" + Todate + "' GROUP BY t.F_CD, f.f_name   ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SecInfoSummaryRight";
            dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SecInfoSummaryRightReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_SecInfoSummaryRight.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SecInfosummary.ReportSource = rdoc;
                CR_SecInfosummary.DisplayToolbar = true;
                CR_SecInfosummary.HasExportButton = true;
                CR_SecInfosummary.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("TransType", transtype);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (transtype == "S")
        {
            sbMst.Append("SELECT  t.F_CD, f.f_name  fund_name,SUM(AMT_AFT_COM) as SELL, sum(no_share) as No_Of_Share ,sum(crt_aft_com * no_share) as COST ,     ");
            sbMst.Append(" (SUM(AMT_AFT_COM) - sum(crt_aft_com *no_share)) as profit FROM FUND_TRANS_HB t, fund f WHERE t.TRAN_TP='"+transtype+"' AND f.f_cd=t.f_cd and    ");
            sbMst.Append(" VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"' GROUP BY t.F_CD, f.f_name ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SecInfoSummarySell";
           // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SecInfoSummarySellReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_SecInfoSummarySell.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_SecInfosummary.ReportSource = rdoc;
                CR_SecInfosummary.DisplayToolbar = true;
                CR_SecInfosummary.HasExportButton = true;
                CR_SecInfosummary.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("TransType", transtype);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }
        }

       

       
      
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_SecInfosummary.Dispose();
        CR_SecInfosummary = null;
        CR_SecInfosummary.Dispose();
        CR_SecInfosummary = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}