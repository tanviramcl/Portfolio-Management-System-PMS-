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

       
        string compcode = Request.QueryString["companycode"].ToString();

        string p1date = Convert.ToString(Request.QueryString["p1date"]).Trim();
        string p2date = Convert.ToString(Request.QueryString["p2date"]).Trim();


        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("select * from (SELECT  t.F_CD, f.f_name fund_name,SUM(AMT_AFT_COM) as SALE, sum(no_share) as No_Of_Share, sum(crt_aft_com * no_share) as COST ,SUM(AMT_AFT_COM) - sum(crt_aft_com *no_share) as profit,");
        sbMst.Append(" t.comp_cd, c.comp_nm FROM FUND_TRANS_HB t , comp c, fund f WHERE f.f_cd=t.f_cd and t.TRAN_TP=  'S' AND VCH_DT BETWEEN '"+p1date+"' AND '"+p2date+"' and c.comp_cd=t.comp_cd and no_share>=1");
        sbMst.Append(" GROUP BY t.F_CD, t.comp_cd,c.comp_nm,f.f_name) tab1 where tab1.comp_cd='"+compcode+"'");
        
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "CapitalGainCompanyWiseReport";
      //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\crtCapitalGainCompanyWiseReport.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {

            string Path = Server.MapPath("Report/CRCapitalGainCompanyWiseReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CRCapitalGainCompanyWiseReport.ReportSource = rdoc;
            CRCapitalGainCompanyWiseReport.DisplayToolbar = true;
            CRCapitalGainCompanyWiseReport.HasExportButton = true;
            CRCapitalGainCompanyWiseReport.HasPrintButton = true;
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
        CRCapitalGainCompanyWiseReport.Dispose();
        CRCapitalGainCompanyWiseReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}