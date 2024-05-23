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

       
            sbMst.Append("select b.COMP_NM, a.TRAN_DATE, a.AVG_RT,a.DSE_RT,a.CSE_RT,a.CSE_DT,a.DSE_HIGH,a.DSE_LOW,a.DSE_OPEN from  ");
          sbMst.Append("(SELECT COMP_CD, TRAN_DATE, AVG_RT, DSE_RT, CSE_RT, CSE_DT, DSE_HIGH, DSE_LOW, DSE_OPEN  ");
        sbMst.Append("  FROM INVEST.MARKET_PRICE ) a  inner join comp b on A.COMP_CD=B.COMP_CD where a.TRAN_DATE  ");
        sbMst.Append(" BETWEEN   '" + Fromdate+"' and '"+Todate+"' ");
          
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "MarketPriceReportViewer";
          // dtReprtSource.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\CR_MarketPriceReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_MarketPriceReport.rpt");
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