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

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }


        string p1date = Convert.ToString(Request.QueryString["p1date"]).Trim();
        string p2date = Convert.ToString(Request.QueryString["p2date"]).Trim();


        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("select t.VCH_DT, t.F_CD,decode(t.f_cd, 1, 'ICB Asset Management Company Ltd.', 2, 'ICB AMCL Unit Fund', 3, 'ICB AMCL First Mutual Fund', 4, 'ICB AMCL Pension Holders'' Unit Fund', 5, 'ICB AMCL Islamic Mutual Fund', 6, 'ICB AMCL First NRB Mutual Fund', 7, 'ICB AMCL Second NRB Mutual Fund') fund_name,");
        sbMst.Append("t.COMP_CD, c.comp_nm,c.comp_nm || '(' || t.COMP_CD || ')',t.TRAN_TP,decode(t.TRAN_TP, 'C', 'Purchase', 'S', 'Sell', 'B', 'Bonus', 'R', 'Right', 'P', 'IPO') tran_type,t.VCH_NO, t.NO_SHARE, t.RATE, t.COST_RATE, t.CRT_AFT_COM, t.AMOUNT, t.AMT_AFT_COM,t.AMT_AFT_COM / t.NO_SHARE avg_rate,t.STOCK_EX,");
        sbMst.Append("decode(t.STOCK_EX, 'D', 'DSE', 'C', 'CSE', ' ALL') stock_name,t.OP_NAME from fund_trans_hb t, comp c where vch_dt between '"+p1date+"' and '"+p2date+"' and c.comp_cd = t.comp_cd and c.cds <> 'Y' and t.f_cd = 1 order by f_cd, tran_tp, t.VCH_DT");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "NonDemateSharesCheck";
      //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\1-8-17\amclpmfs\UI\ReportViewer\Report\crtNonDemateSharesCheck.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {

            string Path = Server.MapPath("Report/CRNonDemateSharesCheck.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_NonDemateSharesCheck.ReportSource = rdoc;
            CR_NonDemateSharesCheck.DisplayToolbar = true;
            CR_NonDemateSharesCheck.HasExportButton = true;
            CR_NonDemateSharesCheck.HasPrintButton = true;
            //rdoc.SetParameterValue("prmtransTypeDetais", transTypeDetais);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else
        {
            string Path = Server.MapPath("Report/CRNonDemateSharesCheck.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_NonDemateSharesCheck.ReportSource = rdoc;
            CR_NonDemateSharesCheck.DisplayToolbar = true;
            CR_NonDemateSharesCheck.HasExportButton = true;
            CR_NonDemateSharesCheck.HasPrintButton = true;
            //rdoc.SetParameterValue("prmtransTypeDetais", transTypeDetais);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_NonDemateSharesCheck.Dispose();
        CR_NonDemateSharesCheck = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}