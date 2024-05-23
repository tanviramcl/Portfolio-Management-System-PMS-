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
        string statementType = "";
        string fundName = "";
        string comp_cd = "";
        string comp_name = "";

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");


        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else {
            Fromdate = (string)Session["Fromdate"];
            Todate = (string)Session["Todate"];
            fundCode = (string)Session["fundCodes"];
            statementType = (string)Session["statementType"];
            comp_cd = (string)Session["COMP_CD"];
            comp_name = (string)Session["comp_name"];
            fundName = (string)Session["fundName"];

        }


        if (string.Compare(statementType, "DateWise", true) == 0)
        {
            sbMst.Append("SELECT   VCH_DT, SUM(decode(TRAN_TP,'S',AMT_AFT_COM,0)) AS SALE_PRICE, SUM(decode(TRAN_TP,'C',AMT_AFT_COM,0)) AS BUY_PRICE, ");
            sbMst.Append(" sum(decode(TRAN_TP,'S',CRT_AFT_COM*NO_SHARE,0)) as COST_PRICE, SUM(decode(TRAN_TP,'S',AMT_AFT_COM,0))-sum(decode(TRAN_TP,'S',CRT_AFT_COM*NO_SHARE,0) )as capital_Gain ");
            sbMst.Append(" FROM   FUND_TRANS_HB WHERE       (FUND_TRANS_HB.VCH_DT BETWEEN '" + Fromdate + "' AND '" + Todate + "') and f_Cd='" + fundCode + "' and stock_ex in ('C','D','A') GROUP BY VCH_DT order by VCH_DT ");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "StatementOfProfitOnSaleOfInvestmentDateWise";
            // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_StatementOfProfitOnSaleOfInvestmentDateWiseReportVeiwer.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_StatementOfProfitOnSaleOfInvestmentDateWiseReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.ReportSource = rdoc;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.DisplayToolbar = true;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.HasExportButton = true;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("prmFundName", fundName);

                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }

        }
        else if (string.Compare(statementType, "CompanyWise", true) == 0)
        {
            sbMst.Append("SELECT        FUND.F_NAME, COMP.COMP_NM, SUM(FUND_TRANS_HB.NO_SHARE) AS NO_OF_SHARE_SOLD, SUM(FUND_TRANS_HB.AMT_AFT_COM) AS SALE_PRICE, ");
            sbMst.Append("    SUM(FUND_TRANS_HB.CRT_AFT_COM * FUND_TRANS_HB.NO_SHARE) AS COSTPRICE, SUM(FUND_TRANS_HB.AMT_AFT_COM) ");
            sbMst.Append("    - SUM(FUND_TRANS_HB.CRT_AFT_COM * FUND_TRANS_HB.NO_SHARE) AS PROFIT_LOSS FROM FUND_TRANS_HB INNER JOIN ");
            sbMst.Append("   COMP ON FUND_TRANS_HB.COMP_CD = COMP.COMP_CD INNER JOIN FUND ON FUND_TRANS_HB.F_CD = FUND.F_CD  ");
            sbMst.Append("   WHERE        (FUND_TRANS_HB.F_CD = '" + fundCode + "') AND (FUND_TRANS_HB.VCH_DT BETWEEN '" + Fromdate + "' AND '" + Todate + "') AND (FUND_TRANS_HB.TRAN_TP = 'S') and  stock_ex in('D','C','A') ");
            sbMst.Append(" GROUP BY COMP.COMP_NM, FUND.F_NAME ORDER BY COMP.COMP_NM");
            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "StatementOfProfitOnSaleOfInvestmentCompanyWise";
            // dtReprtSource.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\CR_StatementOfProfitOnSaleOfInvestmentCompanyWiseReportVeiwer.xsd");
            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_StatementOfProfitOnSaleOfInvestmentCompanyWiseWiseReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.ReportSource = rdoc;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.DisplayToolbar = true;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.HasExportButton = true;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("prmFundName", fundName);

                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }


        }
        else if (string.Compare(statementType, "alltype", true) == 0)
        {
            StringBuilder sbMst1 = new StringBuilder();
            StringBuilder sbfilter1 = new StringBuilder();
            DataTable dtReprtSource1 = new DataTable();
            sbMst1.Append("select * from (SELECT       FUND_TRANS_HB.VCH_Dt,COMP.COMP_CD,COMP.COMP_NM,Fund.f_cd,Fund.F_NAME, SUM(FUND_TRANS_HB.NO_SHARE) AS NO_OF_SHARE_SOLD,");
            sbMst1.Append("  SUM(FUND_TRANS_HB.AMT_AFT_COM) AS SALE_PRICE,     SUM(FUND_TRANS_HB.CRT_AFT_COM * FUND_TRANS_HB.NO_SHARE) AS COSTPRICE, ");
            sbMst1.Append("  SUM(FUND_TRANS_HB.AMT_AFT_COM)     - SUM(FUND_TRANS_HB.CRT_AFT_COM * FUND_TRANS_HB.NO_SHARE) AS PROFIT_LOSS FROM FUND_TRANS_HB INNER JOIN ");
            sbMst1.Append("    COMP ON FUND_TRANS_HB.COMP_CD = COMP.COMP_CD INNER JOIN FUND ON FUND_TRANS_HB.F_CD = FUND.F_CD     WHERE        (FUND_TRANS_HB.F_CD = '"+fundCode+"')  ");
            sbMst1.Append("  AND (FUND_TRANS_HB.VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"') AND (FUND_TRANS_HB.TRAN_TP = 'S') and  stock_ex in('D','C','A')   ");
            sbMst1.Append("  GROUP BY FUND_TRANS_HB.VCH_Dt,COMP.COMP_CD,COMP.COMP_NM,Fund.f_cd,Fund.F_NAME) where f_cd="+fundCode+" and comp_cd='"+comp_cd+"' order by vch_dt asc");
            sbMst1.Append(sbfilter1.ToString());
                    dtReprtSource1 = commonGatewayObj.Select(sbMst1.ToString());
            dtReprtSource1.TableName = "StatementOfProfitOnSaleOfInvestmentAlltypes";
           // dtReprtSource1.WriteXmlSchema(@"D:\AMCLPFMS_Invest_110718\amclpmfs\UI\ReportViewer\Report\CR_StatementOfProfitOnSaleOfInvestmentAllTypes.xsd");
            if (dtReprtSource1.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CR_StatementOfProfitOnSaleOfInvestmentAllTypes.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource1);
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.ReportSource = rdoc;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.DisplayToolbar = true;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.HasExportButton = true;
                CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.HasPrintButton = true;
                rdoc.SetParameterValue("Fromdate", Fromdate);
                rdoc.SetParameterValue("Todate", Todate);
                rdoc.SetParameterValue("prmFundName", fundName);
                rdoc.SetParameterValue("prmcompanyName", comp_name);

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
        CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer.Dispose();
        CR_StatementOfProfitOnSaleOfInvestmentReportVeiwer = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }



}