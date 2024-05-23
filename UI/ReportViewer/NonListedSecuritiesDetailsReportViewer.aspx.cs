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

public partial class UI_ReportViewer_PortfolioWithNonListedReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1Obj = new Pf1s1DAO();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string fundCode = "";
        string comp_cd = "";
        string balDate = "";


        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            comp_cd = (string)Session["comp_cd"];
            balDate = (string)Session["balDate"];
            //  balDate = (string)Session["balDate"];
        }

        DataTable dtnonlistedDetailsSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
    

        DataTable dtNonlistedSecrities = new DataTable();

        
        sbfilter.Append(" ");


        sbMst.Append("  select x.*,f.F_name from (select e.COMP_CD,e.F_CD,e.COMP_NM,e.NO_SHARES,e.AMOUNT,f.CAT_ID,f.CAT_NM,e.tran_date, e.TOT_MARKET_PRICE ,e.market_rate, e.APPRICIATION_ERROTION , e.PERCENT_OF_APRE_EROSION from ( ");
        sbMst.Append("  select D.COMP_CD,D.F_CD,d.COMP_NM,D.NO_SHARES,D.AMOUNT as AMOUNT ,d.CAT_ID,D.CAT_NM,d.tran_date,d.tot_market_price as TOT_MARKET_PRICE ,d.market_rate,d.APPRICIATION_ERROTION as APPRICIATION_ERROTION,d.PERCENT_OF_APRE_EROSION  from ");
        sbMst.Append("  ( select c.*,  ROUND(c.NO_SHARES * D.MARKET_RATE, 8) AS TOT_MARKET_PRICE,D.MARKET_RATE ,  ROUND(ROUND(c.NO_SHARES * D.MARKET_RATE, 8)-c.AMOUNT  ) AS APPRICIATION_ERROTION, ROUND((c.NO_SHARES * D.MARKET_RATE - c.AMOUNT) / DECODE(c.AMOUNT, 0 , 1, c.AMOUNT* 100), 8) AS PERCENT_OF_APRE_EROSION ");
        sbMst.Append("  from(  select a.*, B.TRAN_DATE from(   select c.COMP_CD,c.F_cd, c.comp_nm, c.NO_SHARES, c.AMOUNT, d.CAT_ID,d.CAT_NM from (select a.COMP_CD,a.F_cd, b.comp_nm, a.totshare as NO_SHARES, a.totalAmmount as AMOUNT, b.CAT_TP from  ( select comp_cd,F_CD, sum(totammount) as totalAmmount, ");
        sbMst.Append("  sum(totshare) as totshare from(select decode(TRAN_TP, 'B', NO_SHARES, 'S', -NO_SHARES)totshare, decode(TRAN_TP, 'B', AMOUNT, 'S', -AMOUNT)totammount, F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP  from(  ");
        sbMst.Append("  Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP from invest.NON_LISTED_SECURITIES_DETAILS  where    INV_DATE <= '" +balDate+"' order by INV_DATE   )) group by  COMP_CD,F_CD     )  a inner join invest.COMP_NONLISTED b on a.comp_cd = b.comp_cd ");
        sbMst.Append(") c inner join invest.NONLISTED_CATEGORY d  on c.CAT_TP = d.CAT_ID order by comp_cd  ) a inner join(select comp_cd, max(tran_date) as tran_date from NONLISTED_MARKET_PRICE WHERE  tran_date <= '" +balDate+"' group by comp_cd) b  on A.COMP_CD = B.COMP_CD ");
        sbMst.Append("  ) c  left outer join  NONLISTED_MARKET_PRICE d on c.comp_cd = d.comp_cd and c.TRAN_DATE = d.TRAN_DATE  order by c.comp_cd   ) d where D.NO_SHARES>0 and D.AMOUNT>0  ) e  inner join NONLISTED_CATEGORY f  on e.CAT_ID = f.CAT_ID )    x inner join Fund f on x.F_cd=f.F_cd Where 1=1   ");

        if (fundCode != "0")
        {
            sbMst.Append(" AND x.F_CD="+fundCode+"");
        }
        if (comp_cd != "0")
        {
            sbMst.Append(" AND x.comp_cd=" + comp_cd + "");
        }
        sbMst.Append(" order by x.F_CD,x.comp_nm asc ");
        sbMst.Append(sbfilter.ToString());

        dtNonlistedSecrities = commonGatewayObj.Select(sbMst.ToString());
        string FundName = "";
        if (fundCode != "0")
        {
            DataTable dtFundName = new DataTable();
            sbMst = new StringBuilder();
            sbMst.Append("SELECT     * from Fund ");
            sbMst.Append("WHERE     F_CD = " + fundCode + " ");
            dtFundName = commonGatewayObj.Select(sbMst.ToString());

           
            if (dtFundName.Rows.Count > 0)
            {
                FundName = dtFundName.Rows[0]["F_NAME"].ToString();
            }
        }

      

        if (dtNonlistedSecrities.Rows.Count > 0)
        {

            dtNonlistedSecrities.TableName = "PortfolioWithNonListedDeatilsReport";
           // dtNonlistedSecrities.WriteXmlSchema(@"D:\Development\PFMS_LIVE\Main_LIVE_SERVER\amclinvanalysisandpfms\UI\ReportViewer\Report\crtNonListedDetailsReport_final.xsd");
            //ReportDocument rdoc = new ReportDocument();
            string Path = "";
            Path = Server.MapPath("Report/crtNonListedReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtNonlistedSecrities);
            CRV_PfolioWithNonListed.ReportSource = rdoc;
            CRV_PfolioWithNonListed.DisplayToolbar = true;
            CRV_PfolioWithNonListed.HasExportButton = true;
            CRV_PfolioWithNonListed.HasPrintButton = true;
            rdoc.SetParameterValue("FUndName", FundName);
            rdoc.SetParameterValue("balDate", balDate);

            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_PfolioWithNonListed.Dispose();
        CRV_PfolioWithNonListed = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
