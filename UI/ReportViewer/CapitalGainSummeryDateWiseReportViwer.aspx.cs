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
        sbMst.Append("SELECT  fund_trans_hb.F_CD, fund_trans_hb.vch_dt, f.f_name fund_name,SUM(AMT_AFT_COM) as SALE, sum(no_share) as No_Of_Share,");
        sbMst.Append("sum(crt_aft_com * no_share) as COST ,SUM(AMT_AFT_COM) -sum(crt_aft_com *no_share) as profit FROM FUND_TRANS_HB, pfolio_bk, fund f ");
        sbMst.Append("WHERE TRAN_TP='S' AND fund_trans_hb.f_cd=f.f_cd and  VCH_DT BETWEEN '"+p1date+"' AND '"+p2date+"'");
        sbMst.Append("and bal_dt_ctrl='"+p2date+"' and fund_trans_hb.F_CD=pfolio_bk.f_cd and fund_trans_hb.comp_CD=pfolio_bk.comp_cd GROUP BY fund_trans_hb.F_CD , pfolio_bk.comp_cd , vch_dt, f.f_name order by F_CD");

        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "CapitalgainSummeryDateWise";
       // dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\crtCapitalgainSummeryDateWise.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {

            string Path = Server.MapPath("Report/CRCapitalgainSummeryDateWise.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_CapitalGainSummeryDateWiseReport.ReportSource = rdoc;
            CR_CapitalGainSummeryDateWiseReport.DisplayToolbar = true;
            CR_CapitalGainSummeryDateWiseReport.HasExportButton = true;
            CR_CapitalGainSummeryDateWiseReport.HasPrintButton = true;
            rdoc.SetParameterValue("p1date", p1date);
            rdoc.SetParameterValue("p2date", p2date);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else
        {
            Response.Write("No Data Found");
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_CapitalGainSummeryDateWiseReport.Dispose();
        CR_CapitalGainSummeryDateWiseReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}