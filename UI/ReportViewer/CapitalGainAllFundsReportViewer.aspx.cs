using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_CapitalGainAllFundsReportViewer : System.Web.UI.Page
{
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strFromdate = "";
        string strTodate = "";
        string fundCodes = "";
        string strSQLForMainReport, strSQLForSubReport;
        //string companyCodes = "";
        //string percentageCheck = "";

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            strFromdate = (string)Session["FromDate"];
            strTodate = (string)Session["ToDate"];
            fundCodes = (string)Session["fundCodes"];

        }
         CommonGateway commonGatewayObj = new CommonGateway();
      //  DataTable dtReprtSource = new DataTable();
        DataTable dtRptSrcMainReport = new DataTable();
        DataTable dtRptSrcSubReport = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();

           //strSQLForMainReport = "SELECT p.f_cd1,q.f_cd2,p.fund_name1,q.fund_name2,p.COST1,q.COST2,q.SALE2,p.No_Of_Share1,q.No_Of_Share2,q.profit2 from " +                                 
           //   "(SELECT  t.F_CD f_cd1, f.f_name fund_name1,SUM(AMT_AFT_COM) as COST1, sum(no_share) as No_Of_Share1 FROM FUND_TRANS_HB  t, fund f " +
           // " WHERE t.TRAN_TP = 'C' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and t.f_cd = f.f_cd and t.f_cd <> 3" +
           // " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name)p, (SELECT t.F_CD f_cd2, f.f_name fund_name2,SUM(AMT_AFT_COM) as SALE2," +
           // "sum(no_share) as No_Of_Share2,sum(crt_aft_com * no_share) as COST2,SUM(AMT_AFT_COM) - sum(crt_aft_com * no_share) as profit2" +
           //  " FROM FUND_TRANS_HB t, fund f WHERE t.TRAN_TP = 'S' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and f.f_cd = t.f_cd and t.f_cd <> 3" +
           //   " GROUP BY t.F_CD, f.f_name)q where p.f_cd1=q.f_cd2 order by p.f_cd1";

        strSQLForMainReport = "SELECT  t.F_CD f_cd1, f.f_name fund_name1,SUM(AMT_AFT_COM) as COST1, sum(no_share) as No_Of_Share1 FROM FUND_TRANS_HB  t, fund f " +
           " WHERE t.TRAN_TP = 'C' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and t.f_cd = f.f_cd and t.f_cd <> 3" +
           " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name";

        dtRptSrcMainReport = commonGatewayObj.Select(strSQLForMainReport);

        strSQLForSubReport = "SELECT t.F_CD f_cd2, f.f_name fund_name2,SUM(AMT_AFT_COM) as SALE2," +
          "sum(no_share) as No_Of_Share2,sum(crt_aft_com * no_share) as COST2,SUM(AMT_AFT_COM) - sum(crt_aft_com * no_share) as profit2" +
           " FROM FUND_TRANS_HB t, fund f WHERE t.TRAN_TP = 'S' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and f.f_cd = t.f_cd and t.f_cd <> 3" +
            " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name";
        dtRptSrcSubReport = commonGatewayObj.Select(strSQLForSubReport);

        DataSet ds = new DataSet();
        //ds.Tables.Add(dtRptSrcMainReport);
        //ds.Tables.Add(dtRptSrcSubReport);



        //        Dim dt1 As DataTable
        //        Dim dt2 As DataTable

        //        dt1 = UnityDataRow()
        //        dt2 = UnityDataRow()

        //dt1.TableName = "Level1"
        //dtRptSrcMainReport.TableName = "dtCopyForMainRpt";
        //dtRptSrcSubReport.TableName = "dtCopyForSubRpt";
        //dt2.TableName = "Level2"

        //HierDS.Tables.Add(dt1) '' no need to write copy method
        //       HierDS.Tables.Add(dt2)

        DataTable dtCopyForMainRpt = new DataTable();
        DataTable dtCopyForSubRpt = new DataTable();
        dtRptSrcMainReport.TableName = "dtCopyForMainRpt";
        dtRptSrcSubReport.TableName = "dtCopyForSubRpt";

        ds.Tables.Add(dtRptSrcMainReport.Copy());
        ds.Tables.Add(dtRptSrcSubReport.Copy());

        //   E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report
        // dtReprtSource.WriteXmlSchema(@"E:\amclpmfs\UI\ReportViewer\Report\xsdMarketValuationWithProfitLoss.xsd");
        // dtReprtSource.WriteXmlSchema(@"E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report\xsdMarketValuationWithProfitLoss.xsd");

       // ds.WriteXmlSchema(@"E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report\xsdCapitalGainAllFunds.xsd");
      
        if (dtRptSrcMainReport.Rows.Count>0  || dtRptSrcSubReport.Rows.Count > 0 )
        {
            string Path = Server.MapPath("Report/crptCapitalGainAllFundsReport.rpt");
            rdoc.Load(Path);
            //ds.Tables[0].Merge(dtRptSrcMainReport);
            //ds.Tables[0].Merge(dtRptSrcSubReport);
            // rdoc.SetDataSource(dtReprtSource);
            rdoc.SetDataSource(ds);
           




            CRV_CapitalGainAllFundsReportViewer.ReportSource = rdoc;
            rdoc.SetParameterValue("prmFromdate", strFromdate);
            rdoc.SetParameterValue("prmTodate", strTodate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());


        }
        else
        {
            Response.Write("No Data Found");
        }

        //if (dtReprtSource1.Rows.Count > 0)
        //{
        //    string Path = Server.MapPath("Report/testFUND_TRANS_HB.rpt");
        //    rdoc.Load(Path);
        //    rdoc.SetDataSource(dtReprtSource1);

        //    CrystalReportViewer1.ReportSource = rdoc;

        //    rdoc = ReportFactory.GetReport(rdoc.GetType());
        //}
        //else
        //{
        //    Response.Write("No Data Found");
        //}

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_CapitalGainAllFundsReportViewer.Dispose();
        CRV_CapitalGainAllFundsReportViewer = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}