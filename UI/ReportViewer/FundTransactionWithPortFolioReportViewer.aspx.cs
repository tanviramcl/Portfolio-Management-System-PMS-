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
        //int comCode = Convert.ToInt32(Request.QueryString["companyName"]);
        //int fundCode = Convert.ToInt32(Request.QueryString["fundName"]);
        //string transType = Convert.ToString(Request.QueryString["transType"]).Trim();

         
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();


      

            sbfilter.Append(" ");
            sbMst.Append(" SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
            sbMst.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
            sbMst.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
            sbMst.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
            sbMst.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
            sbMst.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
            sbMst.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='"+ Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT ");





            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "SellPurchageReportwithPortFolio_with_profitloss_2";

        StringBuilder sbMstForBuy = new StringBuilder();
        StringBuilder sbfilterforBuy = new StringBuilder();
        DataTable dtAmmounttotalBuy = new DataTable();

        sbfilterforBuy.Append(" ");
        sbMstForBuy.Append("  select sum(AMT_AFT_COM) as TotalAmountBUY  from (SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,t.TRAN_TP , ");
        sbMstForBuy.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForBuy.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForBuy.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForBuy.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForBuy.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForBuy.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT ) c where c.tran_tp='C'");

        sbMstForBuy.Append(sbfilterforBuy.ToString());

        dtAmmounttotalBuy = commonGatewayObj.Select(sbMstForBuy.ToString());
        Decimal totalBUY = 0;

        if (!string.IsNullOrEmpty(dtAmmounttotalBuy.Rows[0]["TotalAmountBUY"].ToString()))
        {
            if (dtAmmounttotalBuy != null && dtAmmounttotalBuy.Rows.Count > 0)
            {
                totalBUY = Convert.ToDecimal(dtAmmounttotalBuy.Rows[0]["TotalAmountBUY"]);
            }
        }


       
       


        StringBuilder sbMstForSell = new StringBuilder();
        StringBuilder sbfilterforSell = new StringBuilder();
        DataTable dtAmmounttotalSell = new DataTable();


        sbfilterforSell.Append(" ");
        sbMstForSell.Append("  select sum(AMT_AFT_COM) as TotalAmountSell  from (SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,t.TRAN_TP , ");
        sbMstForSell.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForSell.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForSell.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForSell.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForSell.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForSell.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT ) c  where c.tran_tp='S' ");

        sbMstForSell.Append(sbfilterforSell.ToString());

        dtAmmounttotalSell = commonGatewayObj.Select(sbMstForSell.ToString());



        Decimal totalSell = 0;

        if (!string.IsNullOrEmpty(dtAmmounttotalSell.Rows[0]["TotalAmountSell"].ToString()))
        {
            if (dtAmmounttotalSell != null && dtAmmounttotalSell.Rows.Count > 0)
            {
                totalSell = Convert.ToDecimal(dtAmmounttotalSell.Rows[0]["TotalAmountSell"]);
            }
        }

        StringBuilder sbMstForProfitLoss = new StringBuilder();
        StringBuilder sbfilterforProfitLoss = new StringBuilder();
        DataTable dtAmmounttotalProfitLoss = new DataTable();

        sbfilterforProfitLoss.Append(" ");
        sbMstForProfitLoss.Append("  select sum(PROFIT_LOSS) as TotalProfitLoss  from (SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,t.TRAN_TP , ");
        sbMstForProfitLoss.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForProfitLoss.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForProfitLoss.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForProfitLoss.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForProfitLoss.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForProfitLoss.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT ) c  where c.tran_tp='S' ");

        sbfilterforProfitLoss.Append(sbMstForProfitLoss.ToString());

        dtAmmounttotalProfitLoss = commonGatewayObj.Select(sbfilterforProfitLoss.ToString());

        Decimal totalProfitLoss = 0;

        if (!string.IsNullOrEmpty(dtAmmounttotalProfitLoss.Rows[0]["TotalProfitLoss"].ToString()))
        {
            if (dtAmmounttotalProfitLoss != null && dtAmmounttotalProfitLoss.Rows.Count > 0)
            {
                totalProfitLoss = Convert.ToDecimal(dtAmmounttotalProfitLoss.Rows[0]["TotalProfitLoss"]);
            }
        }



        StringBuilder sbMstForACategoryBUY = new StringBuilder();
        StringBuilder sbfilterforACategoryBUY = new StringBuilder();
        DataTable dtAmmounttotalACategoryBUY = new DataTable();

        sbfilterforACategoryBUY.Append(" ");
        sbMstForACategoryBUY.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,decode ( b.TOT_NOS,'',0, b.TOT_NOS) As TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForACategoryBUY.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForACategoryBUY.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForACategoryBUY.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForACategoryBUY.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForACategoryBUY.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForACategoryBUY.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='A' and d.Flag='L' and tran_type='Buy' ");

        sbMstForACategoryBUY.Append(sbfilterforACategoryBUY.ToString());

        dtAmmounttotalACategoryBUY = commonGatewayObj.Select(sbMstForACategoryBUY.ToString());


        DataTable dtAmmounttotalACategorySELL = new DataTable();
        StringBuilder sbMstForACategorySELL = new StringBuilder();
        StringBuilder sbfilterforACategorySELL = new StringBuilder();

        sbfilterforACategorySELL.Append(" ");
        sbMstForACategorySELL.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForACategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForACategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForACategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForACategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForACategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForACategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='A' and d.Flag='L' and tran_type='Sell' ");

        sbMstForACategorySELL.Append(sbfilterforACategorySELL.ToString());

        dtAmmounttotalACategorySELL = commonGatewayObj.Select(sbMstForACategorySELL.ToString());

        int catAtotalnumberofcompany_BUY = 0;
        int catA_totalShare_BUY = 0;
        Decimal CAT_A_total_BUY = 0;

        int catAtotalnumberofcompany_SELL = 0;
        int catA_totalShare_BUY_SELL = 0;
        Decimal CAT_A_total_SELL = 0;

        if (dtAmmounttotalACategoryBUY != null && dtAmmounttotalACategoryBUY.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalACategoryBUY.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catAtotalnumberofcompany_BUY = Convert.ToInt32(dtAmmounttotalACategoryBUY.Rows[0]["TotalnumberOfCompany"]);
                catA_totalShare_BUY = Convert.ToInt32(dtAmmounttotalACategoryBUY.Rows[0]["total_share"]);
                CAT_A_total_BUY = Convert.ToDecimal(dtAmmounttotalACategoryBUY.Rows[0]["total_amount"]);
            }

         
        }
        if (dtAmmounttotalACategorySELL != null && dtAmmounttotalACategorySELL.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalACategorySELL.Rows[0]["TotalnumberOfCompany"]) > 0)
            {

                catAtotalnumberofcompany_SELL = Convert.ToInt32(dtAmmounttotalACategorySELL.Rows[0]["TotalnumberOfCompany"]);
                catA_totalShare_BUY_SELL = Convert.ToInt32(dtAmmounttotalACategorySELL.Rows[0]["total_share"]);
                CAT_A_total_SELL = Convert.ToDecimal(dtAmmounttotalACategorySELL.Rows[0]["total_amount"]);
            }
        }

        string f_cdBCategory = "";
        StringBuilder sbMstForBCategory = new StringBuilder();
        StringBuilder sbfilterforBCategory = new StringBuilder();
        DataTable dtAmmounttotalBCategoryBUY_2 = new DataTable();

        sbfilterforBCategory.Append(" ");
        sbMstForBCategory.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,decode ( b.TOT_NOS,'',0, b.TOT_NOS) As TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForBCategory.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForBCategory.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForBCategory.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForBCategory.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForBCategory.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForBCategory.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='B' and d.Flag='L' and tran_type='Buy'  ");

        sbfilterforBCategory.Append(sbMstForBCategory.ToString());

        dtAmmounttotalBCategoryBUY_2 = commonGatewayObj.Select(sbMstForBCategory.ToString());






        DataTable dtAmmounttotalBCategorySELL = new DataTable();

        StringBuilder sbMstForBCategorySELL = new StringBuilder();
        StringBuilder sbfilterforBCategorySELL = new StringBuilder();


        sbfilterforBCategorySELL.Append(" ");
        sbMstForBCategorySELL.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForBCategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForBCategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForBCategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForBCategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForBCategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForBCategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='B' and d.Flag='L' and tran_type='Sell' ");

        sbMstForBCategorySELL.Append(sbfilterforBCategorySELL.ToString());

        dtAmmounttotalBCategorySELL = commonGatewayObj.Select(sbMstForBCategorySELL.ToString());

        int catBtotalnumberofcompany_BUY = 0;
        int catB_totalShare_BUY = 0;
        Decimal CAT_B_total_BUY = 0;
        string B_categoryBUY = "";
        string B_categorySell = "";

        int catBtotalnumberofcompany_SELL = 0;
        int catB_totalShare_BUY_SELL = 0;
        Decimal CAT_B_total_SELL = 0;

        if (dtAmmounttotalBCategoryBUY_2 != null && dtAmmounttotalBCategoryBUY_2.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalBCategoryBUY_2.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catBtotalnumberofcompany_BUY = Convert.ToInt32(dtAmmounttotalBCategoryBUY_2.Rows[0]["TotalnumberOfCompany"]);
                if (dtAmmounttotalBCategoryBUY_2.Rows[0]["total_share"] != DBNull.Value)
                {
                    catB_totalShare_BUY = Convert.ToInt32(dtAmmounttotalBCategoryBUY_2.Rows[0]["total_share"]);
                }
               
                CAT_B_total_BUY = Convert.ToDecimal(dtAmmounttotalBCategoryBUY_2.Rows[0]["total_amount"]);
            }

            StringBuilder sbMsCountF_CDBCategory = new StringBuilder();
            StringBuilder sbfilterCountF_CDBCategory = new StringBuilder();
            DataTable dtCountF_CDBategoryBUY = new DataTable();
            sbMsCountF_CDBCategory.Append(" ");
            sbMsCountF_CDBCategory.Append(" select distinct(f_cd) as FundCode from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
            sbMsCountF_CDBCategory.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
            sbMsCountF_CDBCategory.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
            sbMsCountF_CDBCategory.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
            sbMsCountF_CDBCategory.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
            sbMsCountF_CDBCategory.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
            sbMsCountF_CDBCategory.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='B' and d.Flag='L' and tran_type='Buy' order by FundCode asc ");

            sbfilterCountF_CDBCategory.Append(sbMsCountF_CDBCategory.ToString());

            dtCountF_CDBategoryBUY = commonGatewayObj.Select(sbfilterCountF_CDBCategory.ToString());

            for (int i = 0; i < dtCountF_CDBategoryBUY.Rows.Count; i++)
            {

                B_categoryBUY = B_categoryBUY + "," + dtCountF_CDBategoryBUY.Rows[i]["FundCode"].ToString();


            }
            if (string.IsNullOrEmpty(B_categoryBUY))
            {
                B_categoryBUY = "";
            }
            else
            {
                B_categoryBUY = B_categoryBUY.Remove(0, 1);
            }



        }
        if (dtAmmounttotalBCategorySELL != null && dtAmmounttotalBCategorySELL.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalBCategorySELL.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catBtotalnumberofcompany_SELL = Convert.ToInt32(dtAmmounttotalBCategorySELL.Rows[0]["TotalnumberOfCompany"]);
                catB_totalShare_BUY_SELL = Convert.ToInt32(dtAmmounttotalBCategorySELL.Rows[0]["total_share"]);
                CAT_B_total_SELL = Convert.ToDecimal(dtAmmounttotalBCategorySELL.Rows[0]["total_amount"]);
            }

            StringBuilder sbMsCountF_CDBCategorySELL = new StringBuilder();
            StringBuilder sbfilterCountF_CDBCategorySEll = new StringBuilder();
            DataTable dtCountF_CDBategorySELL = new DataTable();

            sbfilterCountF_CDBCategorySEll.Append(" ");
            sbMsCountF_CDBCategorySELL.Append(" select distinct(f_cd) as FundCode from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
            sbMsCountF_CDBCategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
            sbMsCountF_CDBCategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
            sbMsCountF_CDBCategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
            sbMsCountF_CDBCategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
            sbMsCountF_CDBCategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
            sbMsCountF_CDBCategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='B' and d.Flag='L' and tran_type='Sell' order by FundCode asc ");

            sbfilterCountF_CDBCategorySEll.Append(sbMsCountF_CDBCategorySELL.ToString());

            dtCountF_CDBategorySELL = commonGatewayObj.Select(sbfilterCountF_CDBCategorySEll.ToString());

            for (int i = 0; i < dtCountF_CDBategorySELL.Rows.Count; i++)
            {

                B_categorySell = B_categorySell + "," + dtCountF_CDBategorySELL.Rows[i]["FundCode"].ToString();


            }
            if (string.IsNullOrEmpty(B_categorySell))
            {
                B_categorySell = "";
            }
            else
            {
                B_categorySell = B_categorySell.Remove(0, 1);
            }


        }

        StringBuilder sbMstForZCategory = new StringBuilder();
        StringBuilder sbfilterforZCategory = new StringBuilder();
        DataTable dtAmmounttotalZategoryBUY = new DataTable();

        sbfilterforZCategory.Append(" ");
        sbMstForZCategory.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,decode ( b.TOT_NOS,'',0, b.TOT_NOS) As TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForZCategory.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForZCategory.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForZCategory.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForZCategory.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForZCategory.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForZCategory.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='Z' and d.Flag='L' and tran_type='Buy' ");

        sbfilterforZCategory.Append(sbMstForZCategory.ToString());

        dtAmmounttotalZategoryBUY = commonGatewayObj.Select(sbfilterforZCategory.ToString());


        int catZtotalnumberofcompany_BUY = 0;
        int catZ_totalShare_BUY = 0;
        Decimal CAT_Z_total_BUY = 0;
        string f_cdZCategory = "";
        string f_cdNCategory = "";
        string f_cdNCategorySELL = "";

        if (dtAmmounttotalZategoryBUY != null && dtAmmounttotalZategoryBUY.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalZategoryBUY.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catZtotalnumberofcompany_BUY = Convert.ToInt32(dtAmmounttotalZategoryBUY.Rows[0]["TotalnumberOfCompany"]);

                bool b1 = string.IsNullOrEmpty(dtAmmounttotalZategoryBUY.Rows[0]["total_share"].ToString());
                if (b1 == false)
                {
                    catZ_totalShare_BUY = Convert.ToInt32(dtAmmounttotalZategoryBUY.Rows[0]["total_share"]);
                }
               
                CAT_Z_total_BUY = Convert.ToDecimal(dtAmmounttotalZategoryBUY.Rows[0]["total_amount"]);
            }

            StringBuilder sbMsCountF_CDZCategory = new StringBuilder();
            StringBuilder sbfilterCountF_CDZCategory = new StringBuilder();
            DataTable dtCountF_CDZategoryBUY = new DataTable();

            sbfilterCountF_CDZCategory.Append(" ");
            sbMsCountF_CDZCategory.Append(" select distinct(f_cd) as FundCode from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
            sbMsCountF_CDZCategory.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
            sbMsCountF_CDZCategory.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
            sbMsCountF_CDZCategory.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
            sbMsCountF_CDZCategory.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
            sbMsCountF_CDZCategory.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
            sbMsCountF_CDZCategory.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='Z' and d.Flag='L' and tran_type='Buy' order by FundCode asc ");

            sbfilterCountF_CDZCategory.Append(sbMsCountF_CDZCategory.ToString());

            dtCountF_CDZategoryBUY = commonGatewayObj.Select(sbfilterCountF_CDZCategory.ToString());
          
            for (int i = 0; i < dtCountF_CDZategoryBUY.Rows.Count; i++)
            {
               
                f_cdZCategory = f_cdZCategory+","+dtCountF_CDZategoryBUY.Rows[i]["FundCode"].ToString();
               

            }
            if (string.IsNullOrEmpty(f_cdZCategory))
            {
                f_cdZCategory = "";
            }
            else
            {
                f_cdZCategory = f_cdZCategory.Remove(0, 1);
            }
           

        }

        StringBuilder sbMstForZCategorySELL = new StringBuilder();
        StringBuilder sbfilterforZCategorySELL = new StringBuilder();
        DataTable dtAmmounttotalZategorySELL = new DataTable();

        sbfilterforZCategorySELL.Append(" ");
        sbMstForZCategorySELL.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForZCategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForZCategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForZCategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForZCategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForZCategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForZCategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='Z' and d.Flag='L' and tran_type='Sell' ");

        sbfilterforZCategorySELL.Append(sbMstForZCategorySELL.ToString());

        dtAmmounttotalZategorySELL = commonGatewayObj.Select(sbMstForZCategorySELL.ToString());

        int catZtotalnumberofcompany_SELL = 0;
        int catZ_totalShare_BUY_SELL = 0;
        Decimal CAT_Z_total_SELL = 0;
        string f_cdZCategorySELL = "";
        if (dtAmmounttotalZategoryBUY != null && dtAmmounttotalZategoryBUY.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalZategorySELL.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catZtotalnumberofcompany_SELL = Convert.ToInt32(dtAmmounttotalZategorySELL.Rows[0]["TotalnumberOfCompany"]);
                catZ_totalShare_BUY_SELL = Convert.ToInt32(dtAmmounttotalZategorySELL.Rows[0]["total_share"]);
                CAT_Z_total_SELL = Convert.ToDecimal(dtAmmounttotalZategorySELL.Rows[0]["total_amount"]);
            }

            StringBuilder sbMsCountF_CDZCategorySELL = new StringBuilder();
            StringBuilder sbfilterCountF_CDZCategorySEll = new StringBuilder();
            DataTable dtCountF_CDZategorySELL = new DataTable();

            sbfilterCountF_CDZCategorySEll.Append(" ");
            sbMsCountF_CDZCategorySELL.Append(" select distinct(f_cd) as FundCode from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
            sbMsCountF_CDZCategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
            sbMsCountF_CDZCategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
            sbMsCountF_CDZCategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
            sbMsCountF_CDZCategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
            sbMsCountF_CDZCategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
            sbMsCountF_CDZCategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='Z' and d.Flag='L' and tran_type='Sell' order by FundCode asc");

            sbfilterCountF_CDZCategorySEll.Append(sbMsCountF_CDZCategorySELL.ToString());

            dtCountF_CDZategorySELL = commonGatewayObj.Select(sbfilterCountF_CDZCategorySEll.ToString());

            for (int i = 0; i < dtCountF_CDZategorySELL.Rows.Count; i++)
            {

                f_cdZCategorySELL = f_cdZCategorySELL + "," + dtCountF_CDZategorySELL.Rows[i]["FundCode"].ToString();


            }
            if (string.IsNullOrEmpty(f_cdZCategorySELL))
            {
                f_cdZCategorySELL = "";
            }
            else
            {
                f_cdZCategorySELL = f_cdZCategorySELL.Remove(0, 1);
            }

        }


        StringBuilder sbMstForNCategory = new StringBuilder();
        StringBuilder sbfilterforNCategory = new StringBuilder();
        DataTable dtAmmounttotalNategoryBUY = new DataTable();

        sbfilterforNCategory.Append(" ");
        sbMstForNCategory.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,decode ( b.TOT_NOS,'',0, b.TOT_NOS) As TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForNCategory.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForNCategory.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForNCategory.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForNCategory.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForNCategory.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForNCategory.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='N' and d.Flag='L' and tran_type='Buy' ");

        sbfilterforNCategory.Append(sbMstForNCategory.ToString());

        dtAmmounttotalNategoryBUY = commonGatewayObj.Select(sbfilterforNCategory.ToString());


        int catNtotalnumberofcompany_BUY = 0;
        int catN_totalShare_BUY = 0;
        Decimal CAT_N_total_BUY = 0;
       

        if (dtAmmounttotalNategoryBUY != null && dtAmmounttotalNategoryBUY.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalNategoryBUY.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catNtotalnumberofcompany_BUY = Convert.ToInt32(dtAmmounttotalNategoryBUY.Rows[0]["TotalnumberOfCompany"]);
                catN_totalShare_BUY = Convert.ToInt32(dtAmmounttotalNategoryBUY.Rows[0]["total_share"]);
                CAT_N_total_BUY = Convert.ToDecimal(dtAmmounttotalNategoryBUY.Rows[0]["total_amount"]);
            }

            StringBuilder sbMsCountF_CDNCategory = new StringBuilder();
            StringBuilder sbfilterCountF_CDNCategory = new StringBuilder();
            DataTable dtCountF_CDNategoryBUY = new DataTable();

            sbfilterCountF_CDNCategory.Append(" ");
            sbMsCountF_CDNCategory.Append(" select distinct(f_cd) as FundCode from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
            sbMsCountF_CDNCategory.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
            sbMsCountF_CDNCategory.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
            sbMsCountF_CDNCategory.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
            sbMsCountF_CDNCategory.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
            sbMsCountF_CDNCategory.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
            sbMsCountF_CDNCategory.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='N' and d.Flag='L' and tran_type='Buy' order by FundCode asc ");

            sbfilterCountF_CDNCategory.Append(sbMsCountF_CDNCategory.ToString());

            dtCountF_CDNategoryBUY = commonGatewayObj.Select(sbfilterCountF_CDNCategory.ToString());
            
            for (int i = 0; i < dtCountF_CDNategoryBUY.Rows.Count; i++)
            {

                f_cdNCategory = f_cdNCategory+dtCountF_CDNategoryBUY.Rows[i]["FundCode"].ToString();
            }
            if (string.IsNullOrEmpty(f_cdNCategory))
            {
                f_cdNCategory = "";
            }
            else
            {
                f_cdNCategory = f_cdNCategory.Remove(0, 1);
            }
            
        }

        StringBuilder sbMstForNCategorySELL = new StringBuilder();
        StringBuilder sbfilterforNCategorySELL = new StringBuilder();
        DataTable dtAmmounttotalNategorySELL = new DataTable();

        sbfilterforNCategorySELL.Append(" ");
        sbMstForNCategorySELL.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForNCategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForNCategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForNCategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForNCategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForNCategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForNCategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='N' and d.Flag='L' and tran_type='Sell' ");

        sbfilterforNCategorySELL.Append(sbMstForNCategorySELL.ToString());

        dtAmmounttotalNategorySELL = commonGatewayObj.Select(sbMstForNCategorySELL.ToString());



        int catNtotalnumberofcompany_SELL = 0;
        int catN_totalShare_SELL = 0;
        Decimal CAT_N_total_SELL = 0;

        if (dtAmmounttotalNategorySELL != null && dtAmmounttotalNategorySELL.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalNategorySELL.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catNtotalnumberofcompany_SELL = Convert.ToInt32(dtAmmounttotalNategorySELL.Rows[0]["TotalnumberOfCompany"]);
                catN_totalShare_SELL = Convert.ToInt32(dtAmmounttotalNategorySELL.Rows[0]["total_share"]);
                CAT_N_total_SELL = Convert.ToDecimal(dtAmmounttotalNategorySELL.Rows[0]["total_amount"]);
            }

            StringBuilder sbMsCountF_CDNCategorySELL = new StringBuilder();
            StringBuilder sbfilterCountF_CDNCategorySell = new StringBuilder();
            DataTable dtCountF_CDNategorSELL = new DataTable();

            sbfilterCountF_CDNCategorySell.Append(" ");
            sbMsCountF_CDNCategorySELL.Append(" select distinct(f_cd) as FundCode from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
            sbMsCountF_CDNCategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
            sbMsCountF_CDNCategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
            sbMsCountF_CDNCategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
            sbMsCountF_CDNCategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
            sbMsCountF_CDNCategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
            sbMsCountF_CDNCategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='N' and d.Flag='L' and tran_type='Sell' order by FundCode asc");

            sbfilterCountF_CDNCategorySell.Append(sbMsCountF_CDNCategorySELL.ToString());

            dtCountF_CDNategorSELL = commonGatewayObj.Select(sbfilterCountF_CDNCategorySell.ToString());

            for (int i = 0; i < dtCountF_CDNategorSELL.Rows.Count; i++)
            {

                f_cdNCategorySELL = f_cdNCategorySELL + "," + dtCountF_CDNategorSELL.Rows[i]["FundCode"].ToString();
            }
            if (string.IsNullOrEmpty(f_cdNCategorySELL))
            {
                f_cdNCategorySELL = "";
            }
            else
            {
                f_cdNCategorySELL = f_cdNCategorySELL.Remove(0, 1);
            }



        }


        StringBuilder sbMstForGCategory = new StringBuilder();
        StringBuilder sbfilterforGCategory = new StringBuilder();
        DataTable dtAmmounttotalGategoryBUY = new DataTable();

        sbfilterforGCategory.Append(" ");
        sbMstForGCategory.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,decode ( b.TOT_NOS,'',0, b.TOT_NOS) As TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForGCategory.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForGCategory.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForGCategory.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForGCategory.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForGCategory.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForGCategory.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='G' and d.Flag='L' and tran_type='Buy'  ");

        sbfilterforGCategory.Append(sbMstForGCategory.ToString());

        dtAmmounttotalGategoryBUY = commonGatewayObj.Select(sbfilterforGCategory.ToString());


     


        int catGtotalnumberofcompany_BUY = 0;
        int catG_totalShare_BUY = 0;
        Decimal CAT_G_total_BUY = 0;

        if (dtAmmounttotalGategoryBUY != null && dtAmmounttotalGategoryBUY.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalGategoryBUY.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catGtotalnumberofcompany_BUY = Convert.ToInt32(dtAmmounttotalGategoryBUY.Rows[0]["TotalnumberOfCompany"]);
                catG_totalShare_BUY = Convert.ToInt32(dtAmmounttotalGategoryBUY.Rows[0]["total_share"]);
                CAT_G_total_BUY = Convert.ToDecimal(dtAmmounttotalGategoryBUY.Rows[0]["total_amount"]);
            }


        }


        StringBuilder sbMstForGCategorySELL = new StringBuilder();
        StringBuilder sbfilterforGCategorySELL = new StringBuilder();
        DataTable dtAmmounttotalGategorySELL = new DataTable();

        sbfilterforGCategorySELL.Append(" ");
        sbMstForGCategorySELL.Append(" select count(comp_cd) as TotalnumberOfCompany  ,sum(TOT_NOS) as total_share,sum(AMT_AFT_COM)  total_amount from ( SELECT     a.*,b.TOT_NOS,b.COST_RT_PER_SHARE,b.TCST_AFT_COM,decode ( a.TRAN_TP, 'C',0,'S',(a.AMT_AFT_COM-(a.CRT_AFT_COM*a.NO_SHARE))) as PROFIT_LOSS from (select t.VCH_DT, t.F_CD  ,f.f_name,t.COMP_CD , c.comp_nm,c.comp_nm  || '('|| t.COMP_CD|| ')' as comp_cd_withcode ,c.TRADE_METH,c.flag,t.TRAN_TP , ");
        sbMstForGCategorySELL.Append("  decode ( t.TRAN_TP, 'C', 'Buy','S','Sell') tran_type, t.VCH_NO,  t.NO_SHARE as NO_SHARE, ROUND(t.AMT_AFT_COM / t.NO_SHARE, 2) AS RATE ,");
        sbMstForGCategorySELL.Append("  t.COST_RATE, t.CRT_AFT_COM , t.AMOUNT , t.AMT_AFT_COM,ROUND(t.AMT_AFT_COM/t.NO_SHARE ,2)avg_rate,t.STOCK_EX , ");
        sbMstForGCategorySELL.Append(" decode(t.STOCK_EX,'D','DSE','C','CSE',' ALL') stock_name,t.OP_NAME   from fund_trans_hb t,comp c , fund f where vch_dt ");
        sbMstForGCategorySELL.Append(" between '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy") + "' and t.TRAN_TP in ('C','S') ");
        sbMstForGCategorySELL.Append(" and c.comp_cd=t.comp_cd  and f.f_cd=t.f_cd order by t.f_cd, tran_tp,t.VCH_DT  ) a  left outer join (select comp_cd,f_cd,TRUNC(PFOLIO_BK.TOT_NOS,0) AS TOT_NOS,ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 8) AS COST_RT_PER_SHARE,  ");
        sbMstForGCategorySELL.Append(" PFOLIO_BK.TCST_AFT_COM from pfolio_bk where bal_dt_ctrl='" + Convert.ToDateTime(Request.QueryString["howlaDateTo"]).ToString("dd - MMM - yyyy") + "') b on a.comp_cd=b.comp_cd and a.f_cd=b.f_cd order by a.f_cd,a.tran_tp,a.comp_nm,a.VCH_DT) d where d.TRADE_METH='G' and d.Flag='L' and tran_type='Sell' ");

        sbfilterforGCategorySELL.Append(sbMstForGCategorySELL.ToString());

        dtAmmounttotalGategorySELL = commonGatewayObj.Select(sbfilterforGCategorySELL.ToString());



        int catGtotalnumberofcompany_SELL = 0;
        int catG_totalShare_SELL = 0;
        Decimal CAT_G_total_SELL = 0;

        if (dtAmmounttotalGategorySELL != null && dtAmmounttotalGategorySELL.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtAmmounttotalGategorySELL.Rows[0]["TotalnumberOfCompany"]) > 0)
            {
                catGtotalnumberofcompany_SELL = Convert.ToInt32(dtAmmounttotalGategorySELL.Rows[0]["TotalnumberOfCompany"]);
                catG_totalShare_SELL = Convert.ToInt32(dtAmmounttotalGategorySELL.Rows[0]["total_share"]);
                CAT_G_total_SELL = Convert.ToDecimal(dtAmmounttotalGategorySELL.Rows[0]["total_amount"]);
            }


        }

        if (dtReprtSource.Rows.Count > 0)
            {
               // dtReprtSource.WriteXmlSchema(@"D:\Development\pfms_01_02_2021\amclinvanalysisandpfms\UI\ReportViewer\Report\crtmSellPurchageReportwithPortFolioReport_withportflio.xsd");

                //ReportDocument rdoc = new ReportDocument();
                string Path = Server.MapPath("Report/SellPurchageReportwithPortFolioReport.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CRV_FundTransaction.ReportSource = rdoc;
                CRV_FundTransaction.DisplayToolbar = true;
                CRV_FundTransaction.HasExportButton = true;
                CRV_FundTransaction.HasPrintButton = true;
       
                rdoc.SetParameterValue("prmtransHowladate", Convert.ToDateTime(Request.QueryString["howlaDateFrom"]).ToString("dd-MMM-yyyy"));
                rdoc.SetParameterValue("prmtotalBUY", totalBUY);
                rdoc.SetParameterValue("prmtotalSell", totalSell);
                rdoc.SetParameterValue("prmtotalProfitLoss", totalProfitLoss);

                rdoc.SetParameterValue("catAtotalnumberofcompany_BUY", catAtotalnumberofcompany_BUY);
                rdoc.SetParameterValue("CAT_A_total_BUY", CAT_A_total_BUY);

                rdoc.SetParameterValue("catAtotalnumberofcompany_SELL", catAtotalnumberofcompany_SELL);
                rdoc.SetParameterValue("CAT_A_total_SELL", CAT_A_total_SELL);


            rdoc.SetParameterValue("catBtotalnumberofcompany_BUY", catBtotalnumberofcompany_BUY);
            rdoc.SetParameterValue("CAT_B_total_BUY", CAT_B_total_BUY);
            rdoc.SetParameterValue("B_categoryBUY", B_categoryBUY);

            rdoc.SetParameterValue("catBtotalnumberofcompany_SELL", catBtotalnumberofcompany_SELL);
            rdoc.SetParameterValue("CAT_B_total_SELL", CAT_B_total_SELL);
            rdoc.SetParameterValue("B_categorySell", B_categorySell);


            rdoc.SetParameterValue("catZtotalnumberofcompany_BUY", catZtotalnumberofcompany_BUY);
            rdoc.SetParameterValue("CAT_Z_total_BUY", CAT_Z_total_BUY);
            rdoc.SetParameterValue("f_cdZCategory", f_cdZCategory);

            rdoc.SetParameterValue("catZtotalnumberofcompany_SELL", catZtotalnumberofcompany_SELL);
            rdoc.SetParameterValue("CAT_Z_total_SELL", CAT_Z_total_SELL);
            rdoc.SetParameterValue("f_cdZCategorySELL", f_cdZCategorySELL);
          
            rdoc.SetParameterValue("catNtotalnumberofcompany_BUY", catNtotalnumberofcompany_BUY);
            rdoc.SetParameterValue("CAT_N_total_BUY", CAT_N_total_BUY);
            rdoc.SetParameterValue("f_cdNCategory", f_cdNCategory);
       

            rdoc.SetParameterValue("catNtotalnumberofcompany_SELL", catNtotalnumberofcompany_SELL);
            rdoc.SetParameterValue("CAT_N_total_SELL", CAT_N_total_SELL);
            rdoc.SetParameterValue("f_cdNCategorySELL", f_cdNCategorySELL);

            rdoc.SetParameterValue("catGtotalnumberofcompany_BUY", catGtotalnumberofcompany_BUY);
            rdoc.SetParameterValue("CAT_G_total_BUY", CAT_G_total_BUY);

            rdoc.SetParameterValue("catGtotalnumberofcompany_SELL", catGtotalnumberofcompany_SELL);
            rdoc.SetParameterValue("CAT_G_total_SELL", CAT_G_total_SELL);



            rdoc = ReportFactory.GetReport(rdoc.GetType());
                //rdoc.SetParameterValue("prmLetterPrintDate", letterPrintDateCr);
                //rdoc.SetParameterValue("prmNameOfPerson", nameOfPerson);
            }
            else
            {
                Response.Write("No Data Found");
            }
      



    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_FundTransaction.Dispose();
        CRV_FundTransaction = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
