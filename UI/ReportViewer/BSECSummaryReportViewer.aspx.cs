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
        string transtype = "";
        string strSQLForPurchase, strSQLForSale, strSQLForIPO, strSQLForRight;
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
            transtype = (string)Session["transtype"];

        }
        CommonGateway commonGatewayObj = new CommonGateway();
        //  DataTable dtReprtSource = new DataTable();
        DataTable dtRptSrcMainReport = new DataTable();
        // DataTable dtRptSrcSubReport = new DataTable();


        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();


        if (transtype == "C")
        {
            strSQLForPurchase = "SELECT  t.F_CD f_cd1, f.f_name fund_name1,SUM(AMT_AFT_COM) as COST1, sum(no_share) as No_Of_Share1 FROM FUND_TRANS_HB  t, fund f " +
           " WHERE t.TRAN_TP = 'C' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and t.f_cd = f.f_cd " +
           " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name";

            dtRptSrcMainReport = commonGatewayObj.Select(strSQLForPurchase);
           // dtRptSrcMainReport.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\xsdBSECSummeryPurchase.xsd");
            if (dtRptSrcMainReport.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/crptBSECSummeryPurchase.rpt");
                rdoc.Load(Path);
                //ds.Tables[0].Merge(dtRptSrcMainReport);
                //ds.Tables[0].Merge(dtRptSrcSubReport);
                // rdoc.SetDataSource(dtReprtSource);
                rdoc.SetDataSource(dtRptSrcMainReport);

                CRV_CapitalGainAllFundsReportViewer.ReportSource = rdoc;
                rdoc.SetParameterValue("prmFromdate", strFromdate);
                rdoc.SetParameterValue("prmTodate", strTodate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());


            }
            else
            {
                Response.Write("No Data Found");
            }

        }
        else if (transtype == "I")
        {
            strSQLForIPO = "SELECT  t.F_CD f_cd1, f.f_name fund_name1,SUM(AMT_AFT_COM)  as IPO, sum(no_share) as No_Of_Share1 FROM FUND_TRANS_HB  t, fund f " +
        " WHERE t.TRAN_TP = 'I' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and t.f_cd = f.f_cd " +
        " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name";

            dtRptSrcMainReport = commonGatewayObj.Select(strSQLForIPO);
            dtRptSrcMainReport.TableName = "BSECSummeryReportIPO";
           // dtRptSrcMainReport.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\xsdBSECSummeryIPO.xsd");
            if (dtRptSrcMainReport.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/crptBSECSummeryIPO.rpt");
                rdoc.Load(Path);
                //ds.Tables[0].Merge(dtRptSrcMainReport);
                //ds.Tables[0].Merge(dtRptSrcSubReport);
                // rdoc.SetDataSource(dtReprtSource);
                rdoc.SetDataSource(dtRptSrcMainReport);

                CRV_CapitalGainAllFundsReportViewer.ReportSource = rdoc;
                rdoc.SetParameterValue("prmFromdate", strFromdate);
                rdoc.SetParameterValue("prmTodate", strTodate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());


            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (transtype == "R")
        {

            strSQLForRight = "SELECT  t.F_CD f_cd1, f.f_name fund_name1,SUM(AMT_AFT_COM) as Right, sum(no_share) as No_Of_Share1 FROM FUND_TRANS_HB  t, fund f " +
             " WHERE t.TRAN_TP = 'R' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and t.f_cd = f.f_cd " +
             " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name";

            dtRptSrcMainReport = commonGatewayObj.Select(strSQLForRight);

            dtRptSrcMainReport.TableName = "BSECSummeryReportRight";
           // dtRptSrcMainReport.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\xsdBSECSummeryRight.xsd");
            if (dtRptSrcMainReport.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/crptBSECSummeryRight.rpt");
                rdoc.Load(Path);
                //ds.Tables[0].Merge(dtRptSrcMainReport);
                //ds.Tables[0].Merge(dtRptSrcSubReport);
                // rdoc.SetDataSource(dtReprtSource);
                rdoc.SetDataSource(dtRptSrcMainReport);

                CRV_CapitalGainAllFundsReportViewer.ReportSource = rdoc;
                rdoc.SetParameterValue("prmFromdate", strFromdate);
                rdoc.SetParameterValue("prmTodate", strTodate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());


            }
            else
            {
                Response.Write("No Data Found");
            }
        }
        else if (transtype == "S")
        {

            strSQLForSale = "SELECT t.F_CD f_cd2, f.f_name fund_name2,SUM(AMT_AFT_COM) as SALE2," +
              "sum(no_share) as No_Of_Share2,sum(crt_aft_com * no_share) as COST2,SUM(AMT_AFT_COM) - sum(crt_aft_com * no_share) as profit2" +
               " FROM FUND_TRANS_HB t, fund f WHERE t.TRAN_TP = 'S' AND VCH_DT BETWEEN '" + strFromdate + "' AND '" + strTodate + "' and t.F_CD IN(" + fundCodes + ") and f.f_cd = t.f_cd and t.f_cd <> 3" +
                " GROUP BY t.F_CD, f.f_name ORDER BY t.F_CD, f.f_name";
            dtRptSrcMainReport = commonGatewayObj.Select(strSQLForSale);

            dtRptSrcMainReport.TableName = "BSECSummeryReportSale";
         //   dtRptSrcMainReport.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\xsdBSECSummerySale.xsd");
            if (dtRptSrcMainReport.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/crptBSECSummerySale.rpt");
                rdoc.Load(Path);
                //ds.Tables[0].Merge(dtRptSrcMainReport);
                //ds.Tables[0].Merge(dtRptSrcSubReport);
                // rdoc.SetDataSource(dtReprtSource);
                rdoc.SetDataSource(dtRptSrcMainReport);

                CRV_CapitalGainAllFundsReportViewer.ReportSource = rdoc;
                rdoc.SetParameterValue("prmFromdate", strFromdate);
                rdoc.SetParameterValue("prmTodate", strTodate);
                rdoc = ReportFactory.GetReport(rdoc.GetType());


            }
            else
            {
                Response.Write("No Data Found");
            }
        }







        //   DataSet ds = new DataSet();


        //   DataTable dtCopyForMainRpt = new DataTable();
        //   DataTable dtCopyForSubRpt = new DataTable();
        //   dtRptSrcMainReport.TableName = "dtCopyForMainRpt";
        ////   dtRptSrcSubReport.TableName = "dtCopyForSubRpt";

        //   ds.Tables.Add(dtRptSrcMainReport.Copy());
        ////   ds.Tables.Add(dtRptSrcSubReport.Copy());

        //   //   E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report
        //   // dtReprtSource.WriteXmlSchema(@"E:\amclpmfs\UI\ReportViewer\Report\xsdMarketValuationWithProfitLoss.xsd");
        //   // dtReprtSource.WriteXmlSchema(@"E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report\xsdMarketValuationWithProfitLoss.xsd");

        //  // ds.WriteXmlSchema(@"E:\iamclpfmsnew\amclpmfs\UI\ReportViewer\Report\xsdCapitalGainAllFunds.xsd");

        //   if (dtRptSrcMainReport.Rows.Count>0  )
        //   {
        //       string Path = Server.MapPath("Report/crptCapitalGainAllFundsReport.rpt");
        //       rdoc.Load(Path);
        //       //ds.Tables[0].Merge(dtRptSrcMainReport);
        //       //ds.Tables[0].Merge(dtRptSrcSubReport);
        //       // rdoc.SetDataSource(dtReprtSource);
        //       rdoc.SetDataSource(ds);





        //       CRV_CapitalGainAllFundsReportViewer.ReportSource = rdoc;
        //       rdoc.SetParameterValue("prmFromdate", strFromdate);
        //       rdoc.SetParameterValue("prmTodate", strTodate);
        //       rdoc = ReportFactory.GetReport(rdoc.GetType());


        //   }
        //   else
        //   {
        //       Response.Write("No Data Found");
        //   }

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