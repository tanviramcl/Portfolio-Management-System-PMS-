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
      
        string fundName = "";
        

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
           
            fundName = (string)Session["fundName"];

        }


    
        StringBuilder sbMst1 = new StringBuilder();
        StringBuilder sbfilter1 = new StringBuilder();
        DataTable dtReprtSource1 = new DataTable();
        sbMst1.Append("       select tab1.*,TAB2.SECT_MAJ_NM from ( (SELECT       FUND_TRANS_HB.VCH_Dt,COMP.COMP_CD,COMP.COMP_NM,COMP.SECT_MAJ_CD,Fund.f_cd,Fund.F_NAME, SUM(FUND_TRANS_HB.NO_SHARE) AS NO_OF_SHARE_SOLD,");
        sbMst1.Append("  SUM(FUND_TRANS_HB.AMT_AFT_COM) AS SALE_PRICE,     SUM(FUND_TRANS_HB.CRT_AFT_COM * FUND_TRANS_HB.NO_SHARE) AS COSTPRICE, ");
        sbMst1.Append("  SUM(FUND_TRANS_HB.AMT_AFT_COM)     - SUM(FUND_TRANS_HB.CRT_AFT_COM * FUND_TRANS_HB.NO_SHARE) AS PROFIT_LOSS FROM FUND_TRANS_HB INNER JOIN ");
        sbMst1.Append("    COMP ON FUND_TRANS_HB.COMP_CD = COMP.COMP_CD INNER JOIN FUND ON FUND_TRANS_HB.F_CD = FUND.F_CD     WHERE        (FUND_TRANS_HB.F_CD = '"+fundCode+"')  ");
        sbMst1.Append("  AND (FUND_TRANS_HB.VCH_DT BETWEEN '"+Fromdate+"' AND '"+Todate+"') AND (FUND_TRANS_HB.TRAN_TP = 'S') and  stock_ex in('D','C','A')   ");
        sbMst1.Append("  GROUP BY FUND_TRANS_HB.VCH_Dt,COMP.COMP_CD,COMP.COMP_NM,COMP.SECT_MAJ_CD,Fund.f_cd,Fund.F_NAME) )  tab1 inner join SECT_MAJ tab2 on tab1.SECT_MAJ_CD= tab2.SECT_MAJ_CD where f_Cd=" + fundCode + "   order by vch_dt desc");
        sbMst1.Append(sbfilter1.ToString());
        dtReprtSource1 = commonGatewayObj.Select(sbMst1.ToString());
        dtReprtSource1.TableName = "StatementOfQuaterlyReportShowingReazlizedProfiltLoss";
        // dtReprtSource1.WriteXmlSchema(@"D:\Development\PFMS_LIVE\PMCS_SERVER\amclinvanalysisandpfms\UI\ReportViewer\Report\CR_StatementOfQuaterlyReportShowingReazlizedProfiltLoss.xsd");
        if (dtReprtSource1.Rows.Count > 0)
        {

            string Path = Server.MapPath("Report/CR_StatementOfQuaterlyReportShowingReazlizedProfiltLoss.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource1);
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