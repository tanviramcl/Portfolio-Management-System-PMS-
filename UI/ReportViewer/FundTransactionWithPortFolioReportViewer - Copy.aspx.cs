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
        int comCode = Convert.ToInt32(Request.QueryString["companyName"]);
        int fundCode = Convert.ToInt32(Request.QueryString["fundName"]);
        string transType = Convert.ToString(Request.QueryString["transType"]).Trim();

         
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();


        if (transType == "C")
        {

            sbfilter.Append(" ");
            sbMst.Append(" select c.* from (select a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM from ( ");
            sbMst.Append("SELECT     FUND_TRANS_HB.COMP_CD, COMP.COMP_NM, FUND_TRANS_HB.VCH_DT, FUND_TRANS_HB.F_CD, ");
            sbMst.Append(" FUND.F_NAME, FUND_TRANS_HB.TRAN_TP, FUND_TRANS_HB.NO_SHARE, FUND_TRANS_HB.RATE,  FUND_TRANS_HB.AMT_AFT_COM ");
            sbMst.Append("FROM         COMP INNER JOIN ");
            sbMst.Append("FUND_TRANS_HB ON COMP.COMP_CD = FUND_TRANS_HB.COMP_CD INNER JOIN FUND ON FUND_TRANS_HB.F_CD = FUND.F_CD ");
            sbMst.Append("WHERE     (FUND_TRANS_HB.VCH_DT BETWEEN '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "') ORDER BY COMP.COMP_NM, FUND_TRANS_HB.F_CD)  ");
            sbMst.Append(" a inner join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, ");
            sbMst.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd-MMM-yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd) c   ");




            if (transType != "0")
            {
                sbMst.Append("  where c.tran_tp='" + transType + "' ");
            }
            if (comCode != 0)
            {
                sbMst.Append(" AND c.COMP_CD =" + comCode + " ");
            }
            if (fundCode != 0)
            {
                sbMst.Append(" AND c.F_CD =" + fundCode + " ");
            }

            // sbMst.Append(" AND (INVEST.FUND_TRANS_HB.COMP_CD in (172,169,167,182,179,173,186,175)) ");
            //sbMst.Append(" AND (INVEST.FUND_TRANS_HB.F_CD = 17) ");

            sbMst.Append(" ORDER BY c.COMP_NM, c.F_CD ");

            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "FundTransactionReport";

            string transTypeDetais = "";
            if (transType == "B")
            {
                transTypeDetais = "Bonus";
            }
            else if (transType == "C")
            {
                transTypeDetais = "Purchase";
            }
            else if (transType == "S")
            {
                transTypeDetais = "Sell";
            }
            else if (transType == "R")
            {
                transTypeDetais = "Right";
            }
            else if (transType == "I")
            {
                transTypeDetais = "IPO";
            }

            if (dtReprtSource.Rows.Count > 0)
            {
                // dtReprtSource.WriteXmlSchema(@"D:\Development\pfms_01_02_2021\amclinvanalysisandpfms\UI\ReportViewer\Report\crtmFundTransactionwithPortfilioReport.xsd");

                //ReportDocument rdoc = new ReportDocument();
                string Path = Server.MapPath("Report/FundTransactionWithPortfolioReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CRV_FundTransaction.ReportSource = rdoc;
                CRV_FundTransaction.DisplayToolbar = true;
                CRV_FundTransaction.HasExportButton = true;
                CRV_FundTransaction.HasPrintButton = true;
                rdoc.SetParameterValue("prmtransTypeDetais", transTypeDetais);
                rdoc.SetParameterValue("prmtransHowladate", Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy"));


                rdoc = ReportFactory.GetReport(rdoc.GetType());
                //rdoc.SetParameterValue("prmLetterPrintDate", letterPrintDateCr);
                //rdoc.SetParameterValue("prmNameOfPerson", nameOfPerson);
            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (transType == "S")
        {
            sbfilter.Append(" ");
            sbMst.Append(" select c.* from (select a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM from ( ");
            sbMst.Append("SELECT     FUND_TRANS_HB.COMP_CD, COMP.COMP_NM, FUND_TRANS_HB.VCH_DT, FUND_TRANS_HB.F_CD, ");
            sbMst.Append(" FUND.F_NAME, FUND_TRANS_HB.TRAN_TP, FUND_TRANS_HB.NO_SHARE, FUND_TRANS_HB.RATE,  FUND_TRANS_HB.AMT_AFT_COM ");
            sbMst.Append("FROM         COMP INNER JOIN ");
            sbMst.Append("FUND_TRANS_HB ON COMP.COMP_CD = FUND_TRANS_HB.COMP_CD INNER JOIN FUND ON FUND_TRANS_HB.F_CD = FUND.F_CD ");
            sbMst.Append("WHERE     (FUND_TRANS_HB.VCH_DT BETWEEN '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "') ORDER BY COMP.COMP_NM, FUND_TRANS_HB.F_CD)  ");
            sbMst.Append(" a inner join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE, ");
            sbMst.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd-MMM-yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd) c   ");




            if (transType != "0")
            {
                sbMst.Append("  where c.tran_tp='" + transType + "' ");
            }
            if (comCode != 0)
            {
                sbMst.Append(" AND c.COMP_CD =" + comCode + " ");
            }
            if (fundCode != 0)
            {
                sbMst.Append(" AND c.F_CD =" + fundCode + " ");
            }

            // sbMst.Append(" AND (INVEST.FUND_TRANS_HB.COMP_CD in (172,169,167,182,179,173,186,175)) ");
            //sbMst.Append(" AND (INVEST.FUND_TRANS_HB.F_CD = 17) ");

            sbMst.Append(" ORDER BY c.COMP_NM, c.F_CD ");

            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "FundTransactionReport";

            string transTypeDetais = "";
            if (transType == "B")
            {
                transTypeDetais = "Bonus";
            }
            else if (transType == "C")
            {
                transTypeDetais = "Purchase";
            }
            else if (transType == "S")
            {
                transTypeDetais = "Sell";
            }
            else if (transType == "R")
            {
                transTypeDetais = "Right";
            }
            else if (transType == "I")
            {
                transTypeDetais = "IPO";
            }

            if (dtReprtSource.Rows.Count > 0)
            {
                // dtReprtSource.WriteXmlSchema(@"D:\Development\pfms_01_02_2021\amclinvanalysisandpfms\UI\ReportViewer\Report\crtmFundTransactionwithPortfilioReport.xsd");

                //ReportDocument rdoc = new ReportDocument();
                string Path = Server.MapPath("Report/FundTransactionWithPortfolioReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CRV_FundTransaction.ReportSource = rdoc;
                CRV_FundTransaction.DisplayToolbar = true;
                CRV_FundTransaction.HasExportButton = true;
                CRV_FundTransaction.HasPrintButton = true;
                rdoc.SetParameterValue("prmtransTypeDetais", transTypeDetais);
                rdoc.SetParameterValue("prmtransHowladate", Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy"));


                rdoc = ReportFactory.GetReport(rdoc.GetType());
                //rdoc.SetParameterValue("prmLetterPrintDate", letterPrintDateCr);
                //rdoc.SetParameterValue("prmNameOfPerson", nameOfPerson);
            }
            else
            {
                Response.Write("No Data Found");
            }

        }



    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_FundTransaction.Dispose();
        CRV_FundTransaction = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
