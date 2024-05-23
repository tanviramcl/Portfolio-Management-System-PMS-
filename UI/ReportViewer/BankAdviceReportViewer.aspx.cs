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

public partial class UI_ReportViewer_BankAdviceReportViewer : System.Web.UI.Page
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

        DataTable dtBankAdvice = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT     DECODE(EMP_INFO.SEX, 'M', 'Mr. ' || EMP_INFO.NAME, 'F', 'Ms. ' || EMP_INFO.NAME) AS NAME, EMP_INFO.BKACNO, ");
        sbMst.Append("  AMCL_EMP_SALARY.NET_PAYABLE ");
        sbMst.Append(" FROM         AMCL_EMP_SALARY INNER JOIN ");
        sbMst.Append(" EMP_INFO ON AMCL_EMP_SALARY.ID = EMP_INFO.ID  ");
        
        if (Request.QueryString["calDate"].ToString() != " ")
        {
            sbfilter.Append(" WHERE (AMCL_EMP_SALARY.CAL_DATE ='" + Convert.ToDateTime(Request.QueryString["calDate"]).ToString("dd-MMM-yyyy") + "')");
        }

        sbMst.Append(sbfilter.ToString());
        sbMst.Append(" ORDER BY EMP_INFO.RANK, EMP_INFO.SENIORITY");
        dtBankAdvice = commonGatewayObj.Select(sbMst.ToString());
        dtBankAdvice.TableName = "BankAdvice";
        
        DataTable dtTotalAmount = new DataTable();
        StringBuilder querySring = new StringBuilder();
        querySring.Append("SELECT SUM(AMCL_EMP_SALARY.NET_PAYABLE) AS TOTAL_AMOUNT FROM AMCL_EMP_SALARY");
        querySring.Append(" WHERE (AMCL_EMP_SALARY.CAL_DATE ='" + Convert.ToDateTime(Request.QueryString["calDate"]).ToString("dd-MMM-yyyy") + "')");
        dtTotalAmount = commonGatewayObj.Select(querySring.ToString());
        dtTotalAmount.TableName = "TotalAmount";
        
        NumberToEnglish numberToEnnglishObj = new NumberToEnglish();
        decimal totalAmount = Convert.ToDecimal(dtTotalAmount.Rows[0]["TOTAL_AMOUNT"]);
        string totalAmountInWords = numberToEnnglishObj.changeNumericToWords(totalAmount);
       
        if (dtBankAdvice.Rows.Count > 0)
        {
            //dtBankAdvice.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmBankAdviceReport.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/crtmBankAdviceReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtBankAdvice);
            CRV_BankAdvice.ReportSource = rdoc;
            rdoc.SetParameterValue("prmtotalAmount", totalAmount);
            rdoc.SetParameterValue("prmtotalAmountInWords", totalAmountInWords);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
            CRV_BankAdvice.DisplayToolbar = true;
            CRV_BankAdvice.HasExportButton = true;
            CRV_BankAdvice.HasPrintButton = true;
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_BankAdvice.Dispose();
        CRV_BankAdvice = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
