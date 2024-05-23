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
        string fundcode = Convert.ToString(Request.QueryString["fundcode"]).Trim();
        string balancedate = Convert.ToString(Request.QueryString["balancedate"]).Trim();
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        string fundname = "";

        string strQuerFund = "select F_CD,F_NAME from fund where f_cd=" + fundcode;
         DataTable dtfund = commonGatewayObj.Select(strQuerFund);


        if (!dtfund.Rows[0].IsNull("F_NAME"))
        {
            fundname = dtfund.Rows[0]["F_NAME"].ToString();
            //  invDate = Convert.ToDateTime(dt.Rows[0]["vch_dt"].ToString()).ToString("dd-MMM-yyyy");
        }
        else
        {
            fundname = "";
        }


        sbfilter.Append(" ");
        sbMst.Append("SELECT     SECT_MAJ.SECT_MAJ_NM, SUM(FUND_TRANS_HB.AMT_AFT_COM) AS SELL_AMOUNT FROM         FUND_TRANS_HB INNER JOIN  COMP ON FUND_TRANS_HB.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append("  SECT_MAJ ON COMP.SECT_MAJ_CD = SECT_MAJ.SECT_MAJ_CD WHERE     (FUND_TRANS_HB.F_CD = "+ fundcode + ") AND (FUND_TRANS_HB.VCH_DT <= '" + Convert.ToDateTime(balancedate).ToString("dd-MMM-yyyy") + "') AND (FUND_TRANS_HB.TRAN_TP IN ('S')) GROUP BY SECT_MAJ.SECT_MAJ_NM ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());


        dtReprtSource.TableName = "SectorWiseSelAmount";
      //   dtReprtSource.WriteXmlSchema(@"D:\IAMCL_10-7-17\amclpmfs\amclpmfs\UI\ReportViewer\Report\crtSectorWiseSelAmount.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {

            string Path = Server.MapPath("Report/crtSectorWiseSelAmount.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_crtSectorWiseSelAmount.ReportSource = rdoc;
            CR_crtSectorWiseSelAmount.DisplayToolbar = true;
            CR_crtSectorWiseSelAmount.HasExportButton = true;
            CR_crtSectorWiseSelAmount.HasPrintButton = true;
            rdoc.SetParameterValue("fundName", fundname);
            rdoc.SetParameterValue("prmHowlaDate", balancedate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_crtSectorWiseSelAmount.Dispose();
        CR_crtSectorWiseSelAmount = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
