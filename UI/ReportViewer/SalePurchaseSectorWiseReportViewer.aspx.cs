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
using CrystalDecisions.CrystalReports.Engine;
using System.Text;

public partial class UI_ReportViewer_SalePurchaseViewer : System.Web.UI.Page
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

      
        string howlaDateFrom = Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy");
        string  howlaDateTo = Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd-MMM-yyyy");

        StringBuilder sbMstFor_Sector_BuY_SELL = new StringBuilder();
        StringBuilder sbfilterfor_Sector_BuY_SELL = new StringBuilder();
        DataTable dtAmmount_Sector_BuY_SELL = new DataTable();

        sbfilterfor_Sector_BuY_SELL.Append(" ");
        sbMstFor_Sector_BuY_SELL.Append(" select SECT_MAJ_NM,decode(tran_tp,'C',SUM(no_share),'I',sum(no_share),'R',sum(no_share),0) as Total_BUY_NOSHARE ,decode(tran_tp,'C',SUM(AMT_AFT_COM),'I',sum(amt_aft_com),'R',sum(amt_aft_com),0) as Total_buy ,decode(tran_tp,'S',SUM(no_share),0) as Total_SELL_NOSHARE,");
        sbMstFor_Sector_BuY_SELL.Append("  decode(tran_tp,'S',SUM(amt_aft_com),0) as Total_SELL from (select tab1.*,tab2.SECT_MAJ_NM from (SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM, decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD");
        sbMstFor_Sector_BuY_SELL.Append("  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode,c.SECT_MAJ_CD ,t.TRAN_TP ,   decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE , ");
        sbMstFor_Sector_BuY_SELL.Append("   t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstFor_Sector_BuY_SELL.Append("   between '" + howlaDateFrom+ "' and '" + howlaDateTo + "' and t.TRAN_TP in ('C','S')  and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT   ) a left outer join (select comp_cd,f_cd, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,");
        sbMstFor_Sector_BuY_SELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + howlaDateTo + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd    order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) tab1 inner join SECT_MAJ tab2 on tab1.SECT_MAJ_CD=tab2.SECT_MAJ_CD   ) tab3 group by SECT_MAJ_NM,tran_tp order by SECT_MAJ_NM ");

        sbfilterfor_Sector_BuY_SELL.Append(sbMstFor_Sector_BuY_SELL.ToString());
        dtAmmount_Sector_BuY_SELL = commonGatewayObj.Select(sbfilterfor_Sector_BuY_SELL.ToString());



        StringBuilder sbMstFor_Sector_Total_BuY_SELL = new StringBuilder();
        StringBuilder sbfilterfor_Sector__Total__BuY_SELL = new StringBuilder();
        DataTable dtAmmount__Total__Sector_BuY_SELL = new DataTable();

        sbfilterfor_Sector__Total__BuY_SELL.Append(" ");
        sbMstFor_Sector_Total_BuY_SELL.Append("  select sum(Total_buy) As GrandTotalBUY,SUM(Total_SELL) As GrandTotalSell  from (select SECT_MAJ_NM,decode(tran_tp,'C',SUM(no_share),'I',sum(no_share),'R',sum(no_share),0) as Total_BUY_NOSHARE ,decode(tran_tp,'C',SUM(AMT_AFT_COM),'I',sum(amt_aft_com),'R',sum(amt_aft_com),0) as Total_buy ,decode(tran_tp,'S',SUM(no_share),0) as Total_SELL_NOSHARE,");
        sbMstFor_Sector_Total_BuY_SELL.Append("  decode(tran_tp,'S',SUM(amt_aft_com),0) as Total_SELL from (select tab1.*,tab2.SECT_MAJ_NM from (SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM, decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD");
        sbMstFor_Sector_Total_BuY_SELL.Append("  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode,c.SECT_MAJ_CD ,t.TRAN_TP ,   decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE , ");
        sbMstFor_Sector_Total_BuY_SELL.Append("   t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstFor_Sector_Total_BuY_SELL.Append("   between '" + howlaDateFrom + "' and '" + howlaDateTo + "' and t.TRAN_TP in ('C','S')  and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT   ) a left outer join (select comp_cd,f_cd, TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,");
        sbMstFor_Sector_Total_BuY_SELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + howlaDateTo + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd    order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) tab1 inner join SECT_MAJ tab2 on tab1.SECT_MAJ_CD=tab2.SECT_MAJ_CD   ) tab3 group by SECT_MAJ_NM,tran_tp order by SECT_MAJ_NM )");

        sbfilterfor_Sector__Total__BuY_SELL.Append(sbMstFor_Sector_Total_BuY_SELL.ToString());
        dtAmmount__Total__Sector_BuY_SELL = commonGatewayObj.Select(sbfilterfor_Sector__Total__BuY_SELL.ToString());

        if (dtAmmount_Sector_BuY_SELL.Rows.Count > 0)
        {



        //    dtAmmount_Sector_BuY_SELL.WriteXmlSchema(@"D:\Development\PFMS_LIVE\New folder (2)\amclinvanalysisandpfms\UI\ReportViewer\Report\crtmSalePurchaseSectorWiseReport.xsd");

            //ReportDocument rdoc = new ReportDocument();
            string Path = Server.MapPath("Report/crtSalePurchaseSectorWiseReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtAmmount_Sector_BuY_SELL);
            CRV_SalePurchaseSectorWiseSummary.ReportSource = rdoc;
            CRV_SalePurchaseSectorWiseSummary.DisplayToolbar = true;
            CRV_SalePurchaseSectorWiseSummary.HasExportButton = true;
            CRV_SalePurchaseSectorWiseSummary.HasPrintButton = true;
            rdoc.SetParameterValue("prmFromDate", howlaDateFrom);
            rdoc.SetParameterValue("prmToDate", howlaDateTo);

            if (dtAmmount__Total__Sector_BuY_SELL.Rows.Count > 0)
            {
                rdoc.SetParameterValue("GrandTotalBUY", dtAmmount__Total__Sector_BuY_SELL.Rows[0]["GrandTotalBUY"].ToString());
                rdoc.SetParameterValue("GrandTotalSell", dtAmmount__Total__Sector_BuY_SELL.Rows[0]["GrandTotalSell"].ToString());
            }

            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_SalePurchaseSectorWiseSummary.Dispose();
        CRV_SalePurchaseSectorWiseSummary = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
   

}
