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
        sbfilter.Append(" ");
     
        sbMst.Append("select * from (select CompanyName,nos_t,bal_dt,rt_acm,tcst_aft_com,c_rt,tot_cost,DSE_rate,CSE_rate,m_rt,m_p,diff,group1,Category,f.f_name,trade_meth,flag,markettype from "+
           "  (select trim(c.comp_nm) as CompanyName, c.trade_meth, c.flag, c.markettype, f_cd, trunc(tot_nos) nos_t, bal_dt, trunc(tcst_aft_com / tot_nos, 2) rt_acm, ROUND(tcst_aft_com, 2)tcst_aft_com,"+
            "  ROUND(tot_cost / tot_nos, 2) c_rt, tot_cost, nvl(a.dse_rt, 0) DSE_rate, nvl(a.cse_rt, 0)  CSE_rate, a.adc_rt m_rt, a.adc_rt * tot_nos m_p, ROUND(a.adc_rt - tcst_aft_com / tot_nos, 2)diff,"+
            "  c.trade_meth group1, decode(c.trade_meth, 'N', 'N Group', 'B', 'B Group', 'Z', 'Z Group', 'A', 'A Group', 'G', 'G Group') Category from pfolio_bk a, comp c where c.comp_cd = a.comp_cd"+
            " and f_cd =" + fundcode + " and a.bal_dt_ctrl ='" + Convert.ToDateTime(balancedate).ToString("dd-MMM-yyyy") + "' order by c.comp_nm) tab1 inner join Fund  f ON tab1.f_cd = f.f_cd order by  tab1.Category,tab1.CompanyName) where flag='L' and markettype !='O' order by CompanyName ");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "PortFolioCategoryWise";
     
        if (dtReprtSource.Rows.Count > 0)
        {
            string Path = Server.MapPath("Report/CR_PortFolioCategoryWise.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_PortFolioCategoryWise.ReportSource = rdoc;
            CR_PortFolioCategoryWise.DisplayToolbar = true;
            CR_PortFolioCategoryWise.HasExportButton = true;
            CR_PortFolioCategoryWise.HasPrintButton = true;
            rdoc.SetParameterValue("balDate", balancedate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_PortFolioCategoryWise.Dispose();
        CR_PortFolioCategoryWise = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}