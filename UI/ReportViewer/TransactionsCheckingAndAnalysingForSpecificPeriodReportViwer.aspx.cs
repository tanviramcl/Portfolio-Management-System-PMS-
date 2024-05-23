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
        sbfilter.Append(" ");


        string Fromdate = "";
        string Todate = "";
        string fundCode = "";
     
        string transtype = "";

        string textTransType = "";
      

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            
             Fromdate = Convert.ToString(Request.QueryString["p1date"]).Trim();
             Todate = Convert.ToString(Request.QueryString["p2date"]).Trim();
             fundCode = Convert.ToString(Request.QueryString["fundcode"]).Trim();
        
            transtype = Convert.ToString(Request.QueryString["transtype"]).Trim();
            textTransType= Convert.ToString(Request.QueryString["textTransType"]).Trim();
        }
      
      
        if (fundCode != "0" && transtype != "0")
        {
            sbMst.Append("select c.FUND_NAME,c.COMP_NM,c.SECT_MAJ_NM,c.SECT_MAJ_CD,c.NO_OF_SHARE as TOT_NOS,c.COST_RT_PER_SHARE,c.PURCHASE as TCST_AFT_COM,NVL(c.DSE_RT, c.CSE_RT) AS DSE_RT,");
            sbMst.Append("  ROUND(c.No_Of_Share *c.DSE_RT,2 )AS TOT_MARKET_PRICE,ROUND(ROUND(c.DSE_RT - ROUND(c.PURCHASE / c.NO_OF_SHARE, 2), 2)) AS RATE_DIFF,ROUND(ROUND(c.No_Of_Share * c.DSE_RT, 2) - c.purchase, 2) AS APPRICIATION_ERROTION ");
            sbMst.Append("  from (select  a.*,ROUND(purchase / No_Of_Share, 2) AS COST_RT_PER_SHARE,b.DSE_RT,b.CSE_RT,b.ADC_RT from (SELECT  f.F_CD, f.comp_cd, c.comp_nm,s.SECT_MAJ_CD,S.SECT_MAJ_NM, ");
            sbMst.Append("  decode(f.f_cd, 1,'ICB Asset Management Company Ltd.',2, 'ICB AMCL Unit Fund',3, 'ICB AMCL First Mutual Fund', 4,'ICB AMCL Pension Holders'' Unit Fund',5,'ICB AMCL Islamic Mutual Fund',6, 'ICB AMCL First NRB Mutual Fund', ");
            sbMst.Append(" 7,'ICB AMCL SECOND NRB MUTUAL FUND',8,'PRIME FINANCE FIRST MUTUAL FUND',9,'ICB AMCL SECOND MUTUAL FUND',10,'ICB EMPLOYEES PROVIDENT MUTUAL FUND-1:SCH-01',11,'PRIME BANK 1ST ICB AMCL MUTUAL FUND',12,'ICB AMCL THIRD NRB MUTUAL FUND',13,'PHOENIX FINANCE 1ST MUTUAL FUND', ");
            sbMst.Append(" 14,'IFIL ISLAMIC MUTUAL FUND-1',15,'ICB AMCL SONALI BANK LIMITED 1ST MUTUAL FUND',16,'ICB AMCL FIRST AGRANI BANK MUTUAL FUND',17,'BANGLADESH FUND',18,'ICB AMCL CONVERTED FIRST UNIT FUND',19,'ICB AMCL CONVERTED FIRST UNIT FUND',20,'ICB AMCL ISLAMIC UNIT FUND',21, ");
            sbMst.Append(" 'ICB Asset Mgmt Co. Ltd Employees Benevolent Fund',22,'FIRST ICB UNIT FUND',23,'SECOND  ICB UNIT FUND',24,'THIRD ICB UNIT FUND',25,'FOURTH ICB UNIT FUND',26,'FIFTH ICB UNIT FUND',27,'SIXTH  ICB UNIT FUND',28,'SEVENTH ICB UNIT FUND',29,'EIGHTH ICB  UNIT FUND',30,'ICB AMCL SECOND NRB UNIT FUND') fund_name, ");
            sbMst.Append("  SUM(AMT_AFT_COM) as purchase, sum(no_share) as No_Of_Share   FROM FUND_TRANS_HB f, comp c ,SECT_MAJ s WHERE F.TRAN_TP= '"+transtype+"' AND VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"'  and f_cd='"+fundCode+"' and c.comp_cd=f.comp_cd  and c.SECT_MAJ_CD =s.SECT_MAJ_CD GROUP BY f.F_CD,f.comp_cd ,c.comp_nm,s.SECT_MAJ_CD,s.SECT_MAJ_NM ) ");
            sbMst.Append("   a inner join  ( select * from pfolio_bk where BAL_DT_CTRL ='"+ Todate + "')  b ON a.F_CD = b.F_CD and a.comp_cd=b.comp_cd and a.SECT_MAJ_CD =b.SECT_MAJ_CD) c  ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "TransactionsCheckingAndAnalysingForSpecificPeriodReport";
           //dtReprtSource.WriteXmlSchema(@"D:\16-07-19\amclinvanalysisandpfms\UI\ReportViewer\Report\CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport.ReportSource = rdoc;
                CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport.DisplayToolbar = true;
                CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport.HasExportButton = true;
                CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport.HasPrintButton = true;
               rdoc.SetParameterValue("Fromdate", Fromdate);
               rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("textTransType", textTransType);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("Market Price not found on :"+Todate+"");
            }

        }
        else
        {
            Response.Write("No Data Found");
        }


    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport.Dispose();
        CR_TransactionsCheckingAndAnalysingForSpecificPeriodReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }

}