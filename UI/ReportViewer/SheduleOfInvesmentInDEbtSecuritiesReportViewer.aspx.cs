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

public partial class UI_ReportViewer_CompanyWiseAllPortfoliosReportDSEonlyReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string howlaDate = "";
        string fundCodes = "";
        double totalGroupwiseInvestMent = 0;
        double totalInvestMent = 0;
        string Assetdate = "";


        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            
            howlaDate = (string)Session["howlaDate"];
            fundCodes = (string)Session["fundCodes"];
           
          

        }
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");

     

        sbMst.Append("  select c.COMP_GROUPNAME,d.* from    ( SELECT     PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD,comp.comp_cd, COMP.COMP_NM,comp.TRADE_METH,comp.PAID_CAP,comp.ATHO_CAP,comp.NO_SHRS,comp.markettype,comp.flag,comp.COMP_GROUPID,COMP.BOND_COMP_CD, FUND.F_NAME, PFOLIO_BK.TOT_NOS, PFOLIO_BK.TCST_AFT_COM, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, NVL(PFOLIO_BK.DSE_RT, ");
        sbMst.Append(" PFOLIO_BK.CSE_RT) AS DSE_RT, ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" AS TOT_MARKET_PRICE, ROUND(ROUND(NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) - PFOLIO_BK.TCST_AFT_COM, 2) ");
        sbMst.Append(" AS APPRICIATION_ERROTION, PFOLIO_BK.BAL_DT_CTRL, ROUND(PFOLIO_BK.TCST_AFT_COM/COMP.PAID_CAP*100 , 3) ");
        sbMst.Append(" AS PERCENTAGE_OF_PAIDUP ");
        sbMst.Append(" FROM         PFOLIO_BK INNER JOIN ");
        sbMst.Append(" COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE     (PFOLIO_BK.BAL_DT_CTRL = '"+howlaDate.ToString()+ "')  AND FUND.F_CD IN("+fundCodes+")   ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_NM, COMP.COMP_NM, PFOLIO_BK.F_CD  ) d left outer join COMP_GROUP c on C.COMP_GROUPID=d.COMP_GROUPID");
        sbMst.Append("    where markettype != 'O' and flag = 'L'  and SECT_MAJ_CD=89   order by  COMP_GROUPNAME Asc ");


      
        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
       

        DataRow drSheduleOfInvestment;


        DataTable dt = new DataTable();
        dt.Columns.Add("COMP_GROUPNAME", typeof(string));
        dt.Columns.Add("SECT_MAJ_NM", typeof(string));
        dt.Columns.Add("SECT_MAJ_CD", typeof(string));
        dt.Columns.Add("comp_cd", typeof(string));
        dt.Columns.Add("COMP_NM", typeof(string));
        dt.Columns.Add("TRADE_METH", typeof(string));
        dt.Columns.Add("ATHO_CAP", typeof(string));
        dt.Columns.Add("NO_SHRS", typeof(double));
        dt.Columns.Add("markettype", typeof(string));
        dt.Columns.Add("flag", typeof(string));
        dt.Columns.Add("COMP_GROUPID", typeof(string));
        dt.Columns.Add("F_NAME", typeof(string));
        dt.Columns.Add("TCST_AFT_COM", typeof(double));
        dt.Columns.Add("COST_RT_PER_SHARE", typeof(double));
        dt.Columns.Add("DSE_RT", typeof(double));
        dt.Columns.Add("TOT_MARKET_PRICE", typeof(double));
        dt.Columns.Add("RATE_DIFF", typeof(double));
        dt.Columns.Add("APPRICIATION_ERROTION", typeof(double));
        dt.Columns.Add("BAL_DT_CTRL", typeof(string));
        dt.Columns.Add("persentageOfTotalTnvestment", typeof(double));
        



        if (dtReprtSource.Rows.Count > 0)
        {

            for (int loop = 0; loop < dtReprtSource.Rows.Count; loop++)
            {
                drSheduleOfInvestment = dt.NewRow();

                drSheduleOfInvestment["COMP_GROUPNAME"] = dtReprtSource.Rows[loop]["COMP_GROUPNAME"].ToString();
                drSheduleOfInvestment["SECT_MAJ_NM"] = dtReprtSource.Rows[loop]["SECT_MAJ_NM"].ToString();
                drSheduleOfInvestment["SECT_MAJ_CD"] = dtReprtSource.Rows[loop]["SECT_MAJ_CD"].ToString();
                drSheduleOfInvestment["comp_cd"] = dtReprtSource.Rows[loop]["comp_cd"].ToString();
                drSheduleOfInvestment["COMP_NM"] = dtReprtSource.Rows[loop]["COMP_NM"].ToString();
                drSheduleOfInvestment["TRADE_METH"] = dtReprtSource.Rows[loop]["TRADE_METH"].ToString();
                drSheduleOfInvestment["ATHO_CAP"] = dtReprtSource.Rows[loop]["ATHO_CAP"].ToString();
                drSheduleOfInvestment["NO_SHRS"] = Convert.ToDouble(dtReprtSource.Rows[loop]["NO_SHRS"].ToString());
                drSheduleOfInvestment["markettype"] = dtReprtSource.Rows[loop]["markettype"].ToString();
                drSheduleOfInvestment["flag"] = dtReprtSource.Rows[loop]["flag"].ToString();
                drSheduleOfInvestment["COMP_GROUPID"] = dtReprtSource.Rows[loop]["COMP_GROUPID"].ToString();
                drSheduleOfInvestment["TRADE_METH"] = dtReprtSource.Rows[loop]["TRADE_METH"].ToString();
                drSheduleOfInvestment["F_NAME"] = dtReprtSource.Rows[loop]["F_NAME"].ToString();
                drSheduleOfInvestment["TCST_AFT_COM"] = Convert.ToDouble(dtReprtSource.Rows[loop]["TCST_AFT_COM"].ToString());
                drSheduleOfInvestment["COST_RT_PER_SHARE"] = Convert.ToDouble(dtReprtSource.Rows[loop]["COST_RT_PER_SHARE"].ToString());
                drSheduleOfInvestment["DSE_RT"] = Convert.ToDouble(dtReprtSource.Rows[loop]["DSE_RT"].ToString());
                drSheduleOfInvestment["TOT_MARKET_PRICE"] = Convert.ToDouble(dtReprtSource.Rows[loop]["TOT_MARKET_PRICE"].ToString());
                drSheduleOfInvestment["RATE_DIFF"] = Convert.ToDouble(dtReprtSource.Rows[loop]["RATE_DIFF"].ToString());
                drSheduleOfInvestment["APPRICIATION_ERROTION"] = Convert.ToDouble(dtReprtSource.Rows[loop]["APPRICIATION_ERROTION"].ToString());
                drSheduleOfInvestment["BAL_DT_CTRL"] = Convert.ToDateTime(dtReprtSource.Rows[loop]["BAL_DT_CTRL"].ToString()).ToString("dd-MMM-yyyy");
               



                DataTable dttotalInvestMent = TotalAssetInvestment( fundCodes);

                totalInvestMent = Convert.ToDouble(dttotalInvestMent.Rows[0]["TCST_AFT_COM"].ToString());
                Assetdate = Convert.ToDateTime(dttotalInvestMent.Rows[0]["BAL_DT_CTRL"].ToString()).ToString("dd-MMM-yyyy");

               
                double costpriceforPersentageOfInvestment = Convert.ToDouble(dtReprtSource.Rows[loop]["TOT_MARKET_PRICE"].ToString());
                double pecentegeofInvestment = (costpriceforPersentageOfInvestment / totalInvestMent) * 100;
                drSheduleOfInvestment["persentageOfTotalTnvestment"] = pecentegeofInvestment;


               

                dt.Rows.Add(drSheduleOfInvestment);

            }

            if (dt.Rows.Count > 0)
            {
                dt.TableName = "SheduleOfInvestment";
              // dt.WriteXmlSchema(@"D:\Development\PFMS_LIVE\PMCS_SERVER\amclinvanalysisandpfms\UI\ReportViewer\Report\SheduleOfInvestment_final_22_05_23.xsd");
                string Path = Server.MapPath("Report/SheduleOfInvestmentInDebtSecurities.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dt);
                rdoc.SetParameterValue("totalGroupwiseInvestMent", totalGroupwiseInvestMent.ToString());
                rdoc.SetParameterValue("totalAsstInvestMent", totalInvestMent.ToString());
                rdoc.SetParameterValue("Assetdate", Assetdate.ToString());
                CRV_CompanyWiseAllPortfolioDSEonly.ReportSource = rdoc;
                rdoc = ReportFactory.GetReport(rdoc.GetType());
            }


         
        }
        else
        {
            Response.Write("No Data Found");
        }
    }

    public DataTable TotalAssetInvestment(string fundCodes)
    {


        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");



        sbMst.Append(" select * from ASSET_INFORMATION_FUNDWISE where Bal_dt_ctrl >=(select max(Bal_dt_ctrl) AS Bal_dt_ctrl from ASSET_INFORMATION_FUNDWISE )and F_cd=" + fundCodes + "");



        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        return dtReprtSource;
    }

    public DataTable GorupWiseTotalAmount( string howlaDate , string fundCodes,string COMP_GROUPID,string BOND_COMP_CD)
    {
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");



        sbMst.Append(" select sum(TCST_AFT_COM) As TCST_AFT_COM from ( select c.COMP_GROUPNAME,d.* from    ( SELECT     PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM,comp.TRADE_METH,comp.PAID_CAP,comp.ATHO_CAP,comp.NO_SHRS,comp.markettype,comp.flag,comp.COMP_GROUPID, FUND.F_NAME, PFOLIO_BK.TOT_NOS, PFOLIO_BK.TCST_AFT_COM, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, NVL(PFOLIO_BK.DSE_RT, ");
        sbMst.Append(" PFOLIO_BK.CSE_RT) AS DSE_RT, ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" AS TOT_MARKET_PRICE, ROUND(ROUND(NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) - PFOLIO_BK.TCST_AFT_COM, 2) ");
        sbMst.Append(" AS APPRICIATION_ERROTION, PFOLIO_BK.BAL_DT_CTRL, ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 3) ");
        sbMst.Append(" AS PERCENTAGE_OF_PAIDUP ");
        sbMst.Append(" FROM         PFOLIO_BK INNER JOIN ");
        sbMst.Append(" COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + howlaDate + "')  AND FUND.F_CD IN(" + fundCodes + ")   ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_NM, COMP.COMP_NM, PFOLIO_BK.F_CD  ) d left outer join COMP_GROUP c on C.COMP_GROUPID=d.COMP_GROUPID");
        sbMst.Append("    where markettype != 'O' and flag = 'L'  order by COMP_GROUPNAME, SECT_MAJ_NM Asc) Where 1=1 ");

      
        if (COMP_GROUPID != "0")
        {
            sbMst.Append(" AND COMP_GROUPID=" + COMP_GROUPID + "");
        }

        if (BOND_COMP_CD != "")
        {
            sbMst.Append(" AND BOND_COMP_CD=" + BOND_COMP_CD + "");
        }




        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        return dtReprtSource;
    }


    public DataTable BondTotalAmount(string howlaDate, string fundCodes,  string BOND_COMP_CD)
    {
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");



        sbMst.Append(" select sum(TCST_AFT_COM) As TCST_AFT_COM from ( select c.COMP_GROUPNAME,d.* from    ( SELECT     PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, comp.comp_cd,BOND_COMP_CD,COMP.COMP_NM,comp.TRADE_METH,comp.PAID_CAP,comp.ATHO_CAP,comp.NO_SHRS,comp.markettype,comp.flag,comp.COMP_GROUPID, FUND.F_NAME, PFOLIO_BK.TOT_NOS, PFOLIO_BK.TCST_AFT_COM, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, NVL(PFOLIO_BK.DSE_RT, ");
        sbMst.Append(" PFOLIO_BK.CSE_RT) AS DSE_RT, ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" AS TOT_MARKET_PRICE, ROUND(ROUND(NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) - PFOLIO_BK.TCST_AFT_COM, 2) ");
        sbMst.Append(" AS APPRICIATION_ERROTION, PFOLIO_BK.BAL_DT_CTRL, ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 3) ");
        sbMst.Append(" AS PERCENTAGE_OF_PAIDUP ");
        sbMst.Append(" FROM         PFOLIO_BK INNER JOIN ");
        sbMst.Append(" COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + howlaDate + "')  AND FUND.F_CD IN(" + fundCodes + ")   ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_NM, COMP.COMP_NM, PFOLIO_BK.F_CD  ) d left outer join COMP_GROUP c on C.COMP_GROUPID=d.COMP_GROUPID");
        sbMst.Append("    where markettype != 'O' and flag = 'L' and SECT_MAJ_CD=89 order by COMP_GROUPNAME, SECT_MAJ_NM Asc) Where 1=1 ");



        if (BOND_COMP_CD != "")
        {
            sbMst.Append(" AND COMP_CD=" + BOND_COMP_CD + "");
        }




        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        return dtReprtSource;
    }


    public DataTable SectorWiseTotalAmount(string howlaDate, string fundCodes, string SectorCode)
    {
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");



        sbMst.Append(" select sum(TCST_AFT_COM) As TCST_AFT_COM from ( select c.COMP_GROUPNAME,d.* from    ( SELECT     PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM,comp.TRADE_METH,comp.PAID_CAP,comp.ATHO_CAP,comp.NO_SHRS,comp.markettype,comp.flag,comp.COMP_GROUPID, FUND.F_NAME, PFOLIO_BK.TOT_NOS, PFOLIO_BK.TCST_AFT_COM, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, NVL(PFOLIO_BK.DSE_RT, ");
        sbMst.Append(" PFOLIO_BK.CSE_RT) AS DSE_RT, ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" AS TOT_MARKET_PRICE, ROUND(ROUND(NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) - PFOLIO_BK.TCST_AFT_COM, 2) ");
        sbMst.Append(" AS APPRICIATION_ERROTION, PFOLIO_BK.BAL_DT_CTRL, ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 3) ");
        sbMst.Append(" AS PERCENTAGE_OF_PAIDUP ");
        sbMst.Append(" FROM         PFOLIO_BK INNER JOIN ");
        sbMst.Append(" COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + howlaDate + "')  AND FUND.F_CD IN(" + fundCodes + ")   ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_NM, COMP.COMP_NM, PFOLIO_BK.F_CD  ) d left outer join COMP_GROUP c on C.COMP_GROUPID=d.COMP_GROUPID");
        sbMst.Append("    where markettype != 'O' and flag = 'L'  order by COMP_GROUPNAME, SECT_MAJ_NM Asc) Where SECT_MAJ_CD=" + SectorCode + " ");



        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        return dtReprtSource;
    }

    public DataTable TotalAmount(string howlaDate, string fundCodes)
    {
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");



        sbMst.Append(" select sum(TCST_AFT_COM) As TCST_AFT_COM from ( select c.COMP_GROUPNAME,d.* from    ( SELECT     PFOLIO_BK.SECT_MAJ_NM,PFOLIO_BK.SECT_MAJ_CD, COMP.COMP_NM,comp.TRADE_METH,comp.PAID_CAP,comp.ATHO_CAP,comp.NO_SHRS,comp.markettype,comp.flag,comp.COMP_GROUPID, FUND.F_NAME, PFOLIO_BK.TOT_NOS, PFOLIO_BK.TCST_AFT_COM, ");
        sbMst.Append(" ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2) AS COST_RT_PER_SHARE, NVL(PFOLIO_BK.DSE_RT, ");
        sbMst.Append(" PFOLIO_BK.CSE_RT) AS DSE_RT, ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" AS TOT_MARKET_PRICE, ROUND(ROUND(NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) ");
        sbMst.Append(" - ROUND(PFOLIO_BK.TCST_AFT_COM / PFOLIO_BK.TOT_NOS, 2), 2) AS RATE_DIFF, ");
        sbMst.Append(" ROUND(ROUND(PFOLIO_BK.TOT_NOS * NVL(PFOLIO_BK.DSE_RT, PFOLIO_BK.CSE_RT), 2) - PFOLIO_BK.TCST_AFT_COM, 2) ");
        sbMst.Append(" AS APPRICIATION_ERROTION, PFOLIO_BK.BAL_DT_CTRL, ROUND(PFOLIO_BK.TOT_NOS / COMP.NO_SHRS * 100, 3) ");
        sbMst.Append(" AS PERCENTAGE_OF_PAIDUP ");
        sbMst.Append(" FROM         PFOLIO_BK INNER JOIN ");
        sbMst.Append(" COMP ON PFOLIO_BK.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND ON PFOLIO_BK.F_CD = FUND.F_CD ");
        sbMst.Append(" WHERE     (PFOLIO_BK.BAL_DT_CTRL = '" + howlaDate + "')  AND FUND.F_CD IN(" + fundCodes + ")   ");
        sbMst.Append(" ORDER BY PFOLIO_BK.SECT_MAJ_NM, COMP.COMP_NM, PFOLIO_BK.F_CD  ) d left outer join COMP_GROUP c on C.COMP_GROUPID=d.COMP_GROUPID");
        sbMst.Append("    where markettype != 'O' and flag = 'L'  order by COMP_GROUPNAME, SECT_MAJ_NM Asc) ");



        sbMst.Append(sbfilter.ToString());
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        return dtReprtSource;
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CRV_CompanyWiseAllPortfolioDSEonly.Dispose();
        CRV_CompanyWiseAllPortfolioDSEonly = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
