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
using CrystalDecisions.CrystalReports.Engine;

public partial class UI_ReportViewer_SalePurchaseViewer : System.Web.UI.Page
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

        DataTable dtTransactionDate = commonGatewayObj.Select("SELECT DISTINCT VCH_DT  FROM FUND_TRANS_HB WHERE VCH_DT BETWEEN '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd-MMM-yyyy") + "' AND TRAN_TP IN ('C','S')");
        string howlaDateFrom = Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy");
        string  howlaDateTo = Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd-MMM-yyyy");
        if (dtTransactionDate.Rows.Count > 0)
        {
            DataTable dtReport = getReportTable();
            DataRow drReport;

            for (int loop = 0; loop < dtTransactionDate.Rows.Count; loop++)
            {
                drReport = dtReport.NewRow();
                drReport["TRANSACTION_DATE"] = Convert.ToDateTime(dtTransactionDate.Rows[loop]["VCH_DT"].ToString()).ToString("dd-MMM-yyyy");
                drReport["AMCL_BUY"] = SalePurchase("SELECT NVL(ROUND(SUM(AMT_AFT_COM)/10000000,2),0)AS SP_VALUE FROM FUND_TRANS_HB WHERE F_CD IN (1) AND TRAN_TP='C' AND VCH_DT = '" + Convert.ToDateTime(dtTransactionDate.Rows[loop]["VCH_DT"].ToString()).ToString("dd-MMM-yyyy") + "'");
                drReport["AMCL_SALE"] = SalePurchase("SELECT NVL(ROUND(SUM(AMT_AFT_COM)/10000000,2),0)AS SP_VALUE FROM FUND_TRANS_HB WHERE F_CD IN (1) AND TRAN_TP='S' AND VCH_DT = '" + Convert.ToDateTime(dtTransactionDate.Rows[loop]["VCH_DT"].ToString()).ToString("dd-MMM-yyyy") + "'");

                drReport["UNIT_BUY"] = SalePurchase("SELECT NVL(ROUND(SUM(AMT_AFT_COM)/10000000,2),0)AS SP_VALUE FROM FUND_TRANS_HB  WHERE F_CD IN (2,4) AND TRAN_TP='C' AND VCH_DT = '" + Convert.ToDateTime(dtTransactionDate.Rows[loop]["VCH_DT"].ToString()).ToString("dd-MMM-yyyy") + "'");
                drReport["UNIT_SALE"] = SalePurchase("SELECT NVL(ROUND(SUM(AMT_AFT_COM)/10000000,2),0)AS SP_VALUE FROM FUND_TRANS_HB WHERE F_CD IN (2,4) AND TRAN_TP='S' AND VCH_DT = '" + Convert.ToDateTime(dtTransactionDate.Rows[loop]["VCH_DT"].ToString()).ToString("dd-MMM-yyyy") + "'");

                drReport["MF_BUY"] = SalePurchase("SELECT NVL(ROUND(SUM(AMT_AFT_COM)/10000000,2),0)AS SP_VALUE FROM FUND_TRANS_HB WHERE F_CD NOT IN (1,2,4) AND TRAN_TP='C' AND VCH_DT = '" + Convert.ToDateTime(dtTransactionDate.Rows[loop]["VCH_DT"].ToString()).ToString("dd-MMM-yyyy") + "'");
                drReport["MF_SALE"] = SalePurchase("SELECT NVL(ROUND(SUM(AMT_AFT_COM)/10000000,2),0)AS SP_VALUE FROM FUND_TRANS_HB WHERE F_CD NOT IN (1,2,4) AND TRAN_TP='S' AND VCH_DT = '" + Convert.ToDateTime(dtTransactionDate.Rows[loop]["VCH_DT"].ToString()).ToString("dd-MMM-yyyy") + "'");
                dtReport.Rows.Add(drReport);
            }
            dtReport.TableName = "Transaction";

            //dtReport.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmSalePurchaseReport.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/SalePurchaseReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReport);
            CRV_SalePurchaseSummary.ReportSource = rdoc;
            CRV_SalePurchaseSummary.DisplayToolbar = true;
            CRV_SalePurchaseSummary.HasExportButton = true;
            CRV_SalePurchaseSummary.HasPrintButton = true;
            rdoc.SetParameterValue("prmFromDate", howlaDateFrom);
            rdoc.SetParameterValue("prmToDate", howlaDateTo);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_SalePurchaseSummary.Dispose();
        CRV_SalePurchaseSummary = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
    public DataTable getReportTable()
    {
        DataTable dtReportTable = new DataTable();
        dtReportTable.Columns.Add("TRANSACTION_DATE", typeof(DateTime));
        dtReportTable.Columns.Add("AMCL_BUY", typeof(decimal));
        dtReportTable.Columns.Add("AMCL_SALE", typeof(decimal));
        dtReportTable.Columns.Add("UNIT_BUY", typeof(decimal));
        dtReportTable.Columns.Add("UNIT_SALE", typeof(decimal));
        dtReportTable.Columns.Add("MF_BUY", typeof(decimal));
        dtReportTable.Columns.Add("MF_SALE", typeof(decimal));
        return dtReportTable;
    }
    public decimal SalePurchase(string queryString)
    {
        DataTable dtSalePurchase = commonGatewayObj.Select(queryString);
        decimal salePurchaseValue = Convert.ToDecimal(dtSalePurchase.Rows[0]["SP_VALUE"].ToString());
        return salePurchaseValue;
    }

}
