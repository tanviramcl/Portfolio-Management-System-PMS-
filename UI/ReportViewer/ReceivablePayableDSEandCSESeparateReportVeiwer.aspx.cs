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

       
            sbMst.Append("select t.f_cd,vch_dt,f.f_name fund_name,stock_ex,sum(decode(TRAN_TP,'C',AMT_AFT_COM,0)) buy,sum(decode(TRAN_TP,'S',AMT_AFT_COM,0)) Sale, ");
            sbMst.Append(" sum(decode(TRAN_TP,'C', -AMT_AFT_COM,'S',AMT_AFT_COM,0)) Receivable from  fund_trans_hb t ,fund f where vch_dt between '"+Fromdate+"' and '"+Todate+"' ");
            sbMst.Append(" and tran_tp<>'B' and no_share>=1 and stock_ex in ('C','D') and f.f_cd=t.f_cd group by t.f_cd,f.f_name,  vch_dt,stock_ex order by  t.vch_dt,f_cd ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "ReceivablePayableDSEandCSESeparate";
           // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_ReceivablePayableDSEandCSESeparateReportVeiwerReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_ReceivablePayableDSEandCSESeparateReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_ReceivablePayableDSEandCSESeparateReport.ReportSource = rdoc;
                CR_ReceivablePayableDSEandCSESeparateReport.DisplayToolbar = true;
                CR_ReceivablePayableDSEandCSESeparateReport.HasExportButton = true;
                CR_ReceivablePayableDSEandCSESeparateReport.HasPrintButton = true;
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
        CR_ReceivablePayableDSEandCSESeparateReport.Dispose();
        CR_ReceivablePayableDSEandCSESeparateReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }


}