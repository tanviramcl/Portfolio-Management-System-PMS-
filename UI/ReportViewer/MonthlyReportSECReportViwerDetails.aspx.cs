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
        string pfolioAsOnDate = "";
        string pfolioPreviousMonthDate = "";
       
      

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            //Fromdate = (string)Session["Fromdate"];
            //Todate = (string)Session["Todate"];
            //fundCode = (string)Session["fundCodes"];
            //companycode = (string)Session["companycode"];
            //transtype = (string)Session["transtype"];

            Fromdate = Convert.ToString(Request.QueryString["FromDatedate"]).Trim();
            Todate = Convert.ToString(Request.QueryString["Todatedate"]).Trim();
            pfolioAsOnDate = Convert.ToString(Request.QueryString["pfolioAsOnDate"]).Trim();
            pfolioPreviousMonthDate = Convert.ToString(Request.QueryString["pfolioPreviousMonthDate"]).Trim();
            

        }


        if (pfolioAsOnDate != "" && Fromdate == "" && Todate == "" && pfolioPreviousMonthDate == "")
        {
            sbMst.Append("SELECT     EQUITY.F_NAME, ROUND(DEBT.INVEST_DEBT, 2) AS DEBT, ROUND(DEBT.MINVEST_DEBT , 2) AS MDEBT,          ROUND(EQUITY.INVEST_EQUITY , 2) AS EQUITY, ROUND(EQUITY.MINVEST_EQUITY, 2) AS MEQUITY ");
            sbMst.Append("FROM         (SELECT     FUND.F_CD, FUND.F_NAME, SUM(PFOLIO_BK.TCST_AFT_COM) AS INVEST_EQUITY, SUM(PFOLIO_BK.TOT_NOS * PFOLIO_BK.ADC_RT)    AS MINVEST_EQUITY  FROM          FUND INNER JOIN     PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ");
            sbMst.Append(" WHERE      (PFOLIO_BK.SECT_MAJ_CD <> 89) AND (PFOLIO_BK.BAL_DT_CTRL = '"+pfolioAsOnDate+"') AND (FUND.F_CD NOT IN (1, 3, 5,6,7, 18))  GROUP BY FUND.F_NAME, FUND.F_CD) EQUITY LEFT OUTER JOIN  (SELECT     FUND_1.F_CD, FUND_1.F_NAME, SUM(PFOLIO_BK_1.TCST_AFT_COM) AS INVEST_DEBT, ");
            sbMst.Append("    SUM(PFOLIO_BK_1.TOT_NOS * PFOLIO_BK_1.ADC_RT) AS MINVEST_DEBT  FROM          FUND FUND_1 INNER JOIN  PFOLIO_BK PFOLIO_BK_1 ON FUND_1.F_CD = PFOLIO_BK_1.F_CD  WHERE      (PFOLIO_BK_1.SECT_MAJ_CD = 89) AND (PFOLIO_BK_1.BAL_DT_CTRL = '"+pfolioAsOnDate+"') AND (FUND_1.F_CD NOT IN (1, 3, 5,7,18)) ");
            sbMst.Append(" GROUP BY FUND_1.F_NAME, FUND_1.F_CD) DEBT ON EQUITY.F_CD = DEBT.F_CD ORDER BY EQUITY.F_CD");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "MonthlyReportSEC";
            //dtReprtSource.WriteXmlSchema(@"D:\AMCL_PROJECT\amclinvanalysisandpfms\UI\ReportViewer\Report\CR_MonthlyCheckReportTbl2.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_MonthlyCheckReportTbl2Deatalis.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_MonthlyReportSECReport_tbl2.ReportSource = rdoc;
                CR_MonthlyReportSECReport_tbl2.DisplayToolbar = true;
                CR_MonthlyReportSECReport_tbl2.HasExportButton = true;
                CR_MonthlyReportSECReport_tbl2.HasPrintButton = true;
                rdoc.SetParameterValue("Asondate", pfolioAsOnDate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }

        }
        else if (Fromdate != "" && Todate != "" && pfolioAsOnDate != "" && pfolioPreviousMonthDate != "")
        {
            sbMst.Append(" SELECT     FUND.F_NAME, ROUND(NVL(OPENING_BALANCE.TOTAL_AMT , 0), 2) AS OPENING_BALANCE_COST_PRICE, ");
            sbMst.Append(" ROUND(NVL(OPENING_BALANCE.MARKET_PRICE , 0), 2) AS OPENING_BALANCE_MARKET_PRICE,  ");

            sbMst.Append(" ROUND(NVL(PURCHASE.COST_AMT , 0), 2) AS PURCHASE_AMOUNT, ROUND(NVL(SELL.SELL_AMT , 0), 2) AS SOLD_AMOUNT, ");

            sbMst.Append(" ROUND(NVL(CLOSING_BALANCE.TOTAL_AMT , 0), 2) AS CLOSING_BALANCE_COSTPRICE,  ");
            sbMst.Append(" ROUND(NVL(CLOSING_BALANCE.MARKET_PRICE , 0), 2) AS CLOSING_BALANCE_MARKETPRICE ");
            sbMst.Append(" FROM         (SELECT     F_CD, SUM(AMT_AFT_COM) AS SELL_AMT FROM          FUND_TRANS_HB ");
            sbMst.Append(" WHERE      (VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"') AND (TRAN_TP = 'S') ");
            sbMst.Append(" GROUP BY F_CD) SELL RIGHT OUTER JOIN (SELECT     F_CD, SUM(TCST_AFT_COM) AS TOTAL_AMT, SUM(ADC_RT * TOT_NOS) AS MARKET_PRICE ");
            sbMst.Append(" FROM          PFOLIO_BK PFOLIO_BK_1 WHERE      (BAL_DT_CTRL = '"+pfolioPreviousMonthDate+ "') AND (F_CD NOT IN (1, 3, 5,6,7, 18)) ");
            sbMst.Append(" GROUP BY F_CD) OPENING_BALANCE RIGHT OUTER JOIN(SELECT     F_CD, SUM(TCST_AFT_COM) AS TOTAL_AMT, SUM(ADC_RT * TOT_NOS) AS MARKET_PRICE ");
            sbMst.Append(" FROM          PFOLIO_BK WHERE      (BAL_DT_CTRL = '"+pfolioAsOnDate+ "') AND (F_CD NOT IN (1, 3, 5,6,7, 18)) ");
            sbMst.Append(" GROUP BY F_CD) CLOSING_BALANCE INNER JOIN  (SELECT     F_CD, F_NAME    FROM          FUND FUND_1 ");

            sbMst.Append("     WHERE      (F_CD NOT IN (1, 3, 5,6,7, 18))) FUND ON CLOSING_BALANCE.F_CD = FUND.F_CD ON OPENING_BALANCE.F_CD = FUND.F_CD ON ");
            sbMst.Append("     SELL.F_CD = CLOSING_BALANCE.F_CD LEFT OUTER JOIN (SELECT     F_CD, SUM(AMT_AFT_COM) AS COST_AMT ");
            sbMst.Append(" FROM          FUND_TRANS_HB FUND_TRANS_HB_1       WHERE      (VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"') AND (TRAN_TP = 'C')     ");

            sbMst.Append("  GROUP BY F_CD) PURCHASE ON CLOSING_BALANCE.F_CD = PURCHASE.F_CD ORDER BY FUND.F_CD ");



            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "MonthlyReportSEC";
          //  dtReprtSource.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\CR_MonthlyCheckReportTbl3.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_MonthlyCheckReportTbl3Details.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_MonthlyReportSECReport_tbl2.ReportSource = rdoc;
                CR_MonthlyReportSECReport_tbl2.DisplayToolbar = true;
                CR_MonthlyReportSECReport_tbl2.HasExportButton = true;
                CR_MonthlyReportSECReport_tbl2.HasPrintButton = true;
                rdoc.SetParameterValue("Asondate", pfolioAsOnDate);
                //rdoc.SetParameterValue("Fromdate", Fromdate);
                //rdoc.SetParameterValue("Todate", Todate);
                //rdoc.SetParameterValue("pfolioPreviousMonthDate", pfolioPreviousMonthDate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());
            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (pfolioAsOnDate == "" && Fromdate != "" && Todate != "" && pfolioPreviousMonthDate == "")
        {
            sbMst.Append("SELECT     FUND.F_NAME, COMP.COMP_NM, ROUND(SUM(FUND_TRANS_HB.AMT_AFT_COM), 4) AS AMOUNT_AFT_COM FROM         COMP INNER JOIN ");
            sbMst.Append("  FUND_TRANS_HB ON COMP.COMP_CD = FUND_TRANS_HB.COMP_CD RIGHT OUTER JOIN       FUND ON FUND_TRANS_HB.F_CD = FUND.F_CD  ");
            sbMst.Append("  WHERE     (FUND.F_CD NOT IN (1, 3, 5,6,7, 18)) AND (FUND_TRANS_HB.VCH_DT BETWEEN '" + Fromdate+"' AND '"+Todate+"') AND (COMP.SECT_MAJ_CD = 12) AND ");
            sbMst.Append("        (FUND_TRANS_HB.TRAN_TP IN ('C')) GROUP BY COMP.COMP_NM, FUND.F_NAME, FUND.F_CD ORDER BY FUND.F_CD");
           
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "MonthlyReportSEC";
           // dtReprtSource.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\CR_MonthlyCheckReportTbl6.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {


                string Path = Server.MapPath("Report/CR_MonthlyCheckReportTbl6Deatlis.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_MonthlyReportSECReport_tbl2.ReportSource = rdoc;
                CR_MonthlyReportSECReport_tbl2.DisplayToolbar = true;
                CR_MonthlyReportSECReport_tbl2.HasExportButton = true;
                CR_MonthlyReportSECReport_tbl2.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
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
        CR_MonthlyReportSECReport_tbl2.Dispose();
        CR_MonthlyReportSECReport_tbl2 = null;
        CR_MonthlyReportSECReport_tbl3.Dispose();
        CR_MonthlyReportSECReport_tbl3 = null;
        CR_MonthlyReportSECReport_tbl6.Dispose();
        CR_MonthlyReportSECReport_tbl6 = null;

        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }

}