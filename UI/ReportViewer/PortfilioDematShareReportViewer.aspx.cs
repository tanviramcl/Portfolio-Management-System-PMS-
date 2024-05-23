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
        
        sbMst.Append("select CompanyName,sect_maj_nm,sect_maj_cd,nos_t,bal_dt,rt_acm,tcst_aft_com,c_rt,tot_cost,DSE_rate,m_rt,m_p,diff,gain,f.f_name from (select trim(c.comp_nm) as CompanyName, f_cd,sect_maj_nm, a.sect_maj_cd,"+
        " trunc(tot_nos) nos_t, bal_dt, trunc(tcst_aft_com / tot_nos, 2) rt_acm, ROUND(tcst_aft_com,2) tcst_aft_com, ROUND( tot_cost/tot_nos,2 )c_rt, tot_cost," +
        " nvl(a.dse_rt, 0) DSE_rate, a.adc_rt m_rt, a.adc_rt * tot_nos m_p,(a.adc_rt - trunc(tcst_aft_com / tot_nos, 2)) diff, (round(a.adc_rt, 2) - trunc(tcst_aft_com / tot_nos, 2)) * trunc(tot_nos) gain"+
        " from pfolio_bk a, comp c where c.comp_cd = a.comp_cd and f_cd ="+fundcode+" and a.bal_dt_ctrl = '"+ Convert.ToDateTime(balancedate).ToString("dd - MMM - yyyy") +"' and c.cds = 'Y' " +
        " order by c.comp_nm) tab1 inner join Fund f ON tab1.f_cd = f.f_cd order by  tab1.sect_maj_nm,tab1.CompanyName");
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource.TableName = "PortFolioDemateShare";
      //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\2-13-2017\amclpmfs\UI\ReportViewer\Report\crtPortFolioDemateShareReport.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {
            string Path = Server.MapPath("Report/CR_PortFolioDemateShare.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_PortFolioDemateShare.ReportSource = rdoc;
            CR_PortFolioDemateShare.DisplayToolbar = true;
            CR_PortFolioDemateShare.HasExportButton = true;
            CR_PortFolioDemateShare.HasPrintButton = true;
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
        CR_PortFolioDemateShare.Dispose();
        CR_PortFolioDemateShare = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}