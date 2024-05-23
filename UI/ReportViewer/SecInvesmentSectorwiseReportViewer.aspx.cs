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
using CrystalDecisions.Shared;
public partial class UI_ReportViewer_NegativeBalanceCheckReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1Obj = new Pf1s1DAO();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string balDate = "";
        string fundname = "";
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            balDate = (string)Session["balDate"];
            fundname = (string)Session["fundName"];


        }
        String strSQL;
        double cs_asset, cf_unlist, TotalAssetValue_Mar;
        DataTable dtNAV = new DataTable();
        DataTable dtNonListed = new DataTable();
        strSQL = "select nvl(sum(costprice),0) total_asset_c,nvl(sum(marketprice),0) total_asset_m from nav.nav_details n, nav.nav_master m where " +
                  " n.NAVROWTYPE = 'A' and n.navfundid =  " + fundCode + " and  m.navfundid = " + fundCode + " and m.navno = (select max(navno) from nav.nav_master m where m.navfundid = " + fundCode + " AND m.NAVDATE <='" + balDate + "' )" +
                  " and n.navfundid = m.navfundid and m.navno = n.navno";



        dtNAV = commonGatewayObj.Select(strSQL);
        cs_asset = Convert.ToDouble(dtNAV.Rows[0]["total_asset_c"].ToString());
        if (cs_asset == 0)
        {
            cs_asset = 1;
        }
        TotalAssetValue_Mar = Convert.ToDouble(dtNAV.Rows[0]["total_asset_m"].ToString());
        strSQL = "select f_cd, inv_amount, inv_date FROM(SELECT f_cd, inv_amount, inv_date, " +
               "rank() over (partition by f_cd order by inv_date desc) rnk FROM NON_LISTED_SECURITIES where  inv_amount>0 and F_CD IN(" + fundCode + ") and inv_date<='" + balDate + "' )" +
               " WHERE rnk = 1";
        dtNonListed = commonGatewayObj.Select(strSQL);
        if (dtNonListed.Rows.Count > 0)
        {
            cf_unlist = Convert.ToDouble(dtNonListed.Rows[0]["inv_amount"].ToString());
        }
        else
        {
            cf_unlist = 0;
        }
        

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append(" select trim(c.comp_nm) as CompanyName , sect_maj_nm, a.sect_maj_cd,trunc(tot_nos) nos_t,  bal_dt , trunc(tcst_aft_com/tot_nos,2) rt_acm,  trunc (tcst_aft_com,2) tcst_aft_com, Round(tot_cost/tot_nos,2) c_rt,");
        sbMst.Append(" tot_cost, ROUND(tcst_aft_com/ '" + cs_asset + "'*100,2) cost_percent,nvl(a.dse_rt,0) DSE_rate, a.adc_rt m_rt,    a.adc_rt* tot_nos m_p,(a.dse_rt - trunc(tcst_aft_com/tot_nos,2)) diff , ");
        sbMst.Append(" (round(a.dse_rt,2) - trunc(tcst_aft_com/tot_nos,2)) * trunc(tot_nos) gain  ,ROUND(trunc(tot_nos)/c.no_shrs*100,2)  paid_cap,'" + cs_asset + "' asset,ROUND('" + cf_unlist + "'/'" + cs_asset + "'*100,2) unl_p ");
        sbMst.Append(" from pfolio_bk a, comp c ,  nav.nav_master n where c.comp_cd=a.comp_cd and f_cd='" + fundCode + "' and a.bal_dt_ctrl='" + balDate + "' and n.navfundid= '" + fundCode + "' and  n.navfundid=f_cd ");
        sbMst.Append(" and navno=(select max(navno) from nav.nav_master where navfundid=f_cd   AND NAVDATE<='" + balDate + "' ) order by c.comp_nm ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());


        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "SecInvesmentsectorwisereport";
           // dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_SecInvestmentSectorWiseReport.xsd");
            //ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtSecInvectorwisereport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_Sec_Invesment_sectorwise_report.ReportSource = rdoc;
            CR_Sec_Invesment_sectorwise_report.DisplayToolbar = true;
            CR_Sec_Invesment_sectorwise_report.HasExportButton = true;
            CR_Sec_Invesment_sectorwise_report.HasPrintButton = true;
            rdoc.SetParameterValue("balDate", balDate);
            rdoc.SetParameterValue("fundName", fundname);
            rdoc.SetParameterValue("TotalAssetValue_Mar", cs_asset);
            rdoc.SetParameterValue("NonListedValue", cf_unlist);
           // rdoc.SetParameterValue("prmNonlistedCostPrice", nonlistedCostPrice);
            //rdoc.SetParameterValue("prmNonlisteMarketPrice", nonlistedMarketPrice);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_Sec_Invesment_sectorwise_report.Dispose();
        CR_Sec_Invesment_sectorwise_report = null;
        CR_Sec_Invesment_sectorwise_report.Dispose();
        CR_Sec_Invesment_sectorwise_report = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }

}