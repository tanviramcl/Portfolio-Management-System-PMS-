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
        string transType = Convert.ToString(Request.QueryString["transType"]).Trim();

        DataTable dtReprtSource = new DataTable();
         DataTable dtReprtSource1 = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        //sbMst.Append("SELECT VoucherDate, FundName, CompanyCode, TransactionType, Ammount FROM(SELECT TO_CHAR(VCH_DT, 'DD.MM.YYYY') AS VoucherDate, TRUNC(F_CD) AS FundName, TRUNC(COMP_CD) AS CompanyCode, TRAN_TP AS TransactionType, NO_SHARE * RATE AS Ammount FROM FUND_TRANS_HB WHERE(VCH_DT BETWEEN TO_DATE('15-JAN-10', 'DD-MON-YY') AND TO_DATE('17-JAN-10', 'DD-MON-YY'))) fundtrans");
        //if (transType !=null )
        //{
        //    sbMst.Append(" where fundtrans.TransactionType='"+transType+"'");
        //}
        sbMst.Append("SELECT * from fund_trans");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "FUND_TRANS_HB";
        StringBuilder sbMst1 = new StringBuilder();

        sbMst1.Append("select * from fund_trans_hb");


        //sbMst1.Append(sbMst1.ToString());
        dtReprtSource1 = commonGatewayObj.Select(sbMst1.ToString());
        dtReprtSource1.TableName = "FUND_TRANS_HB2";
      // dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\crttestFUND_TRANS_HB.xsd");
        // "D:\officialProject\1-8-17\amclpmfs\UI\ReportViewer\Report\crttestFUND_TRANS_HB.xsd"


        if (dtReprtSource.Rows.Count > 0 && dtReprtSource1.Rows.Count>0)
        {
          
            string Path = Server.MapPath("Report/testFUND_TRANS_HB.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            rdoc.SetDataSource(dtReprtSource1);
            CRV_FundTransactionHB.ReportSource = rdoc;
            CRV_FundTransactionHB.DisplayToolbar = true;
            CRV_FundTransactionHB.HasExportButton = true;
            CRV_FundTransactionHB.HasPrintButton = true;
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
        CRV_FundTransactionHB.Dispose();
        CRV_FundTransactionHB = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
