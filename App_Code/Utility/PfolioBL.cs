using System;
using System.Text;
using System.Data;


public class PfolioBL
    {
        CommonGateway commonGatewayObj = new CommonGateway();

        public PfolioBL()
        {

        }
        public int getCompanyCodeByDSECode(string TradingCode)
        {
           int companyCode = 0;
           DataTable dtCompanyCode = commonGatewayObj.Select("SELECT * FROM COMP WHERE INSTR_CD='"+TradingCode.ToString().ToUpper()+"'");
           if (dtCompanyCode.Rows.Count > 0)
           {
               companyCode =Convert.ToInt32( dtCompanyCode.Rows[0]["COMP_CD"].ToString());
           }
           return companyCode;

        }
    public int getFundCodeByCustomerCode(string CustomerCode)
    {
        int fundCode = 0;
        DataTable dtFundCode = commonGatewayObj.Select("SELECT * FROM FUND WHERE IS_F_CLOSE IS NULL AND CUSTOMER='" + CustomerCode.ToString().ToUpper() + "'");
        if (dtFundCode.Rows.Count > 0)
        {
            fundCode = Convert.ToInt32(dtFundCode.Rows[0]["F_CD"].ToString());
        }
        return fundCode;

    }
    public int getCompanyCodeByCSECode(string TradingCode)
    {
        int companyCode = 0;
        DataTable dtCompanyCode = commonGatewayObj.Select("SELECT * FROM COMP WHERE CSEINSTR_CD='" + TradingCode.ToString().ToUpper() + "'");
        if (dtCompanyCode.Rows.Count > 0)
        {
            companyCode = Convert.ToInt32(dtCompanyCode.Rows[0]["COMP_CD"].ToString());
        }
        return companyCode;

    }
    //public string getFundNameByF_CD(int F_CD)
    //{
    //    string fundName = "";
    //    DataTable dtfundName = commonGatewayObj.Select("SELECT * FROM FUND WHERE F_CD=" + F_CD);
    //    if (dtfundName.Rows.Count > 0)
    //    {
    //        fundName = dtfundName.Rows[0]["F_NAME"].ToString();
    //    }
    //    return fundName;

        //}
        //public DataTable dtCompnayNameforDropDownlist()
        //{
        //    DataTable dtCompany = commonGatewayObj.Select("SELECT * FROM COMP WHERE  (FLAG = 'L') ORDER BY COMP_NM");

        //   DataTable dtCompanyforDropDownlist = new DataTable();
        //   dtCompanyforDropDownlist.Columns.Add("COMP_CD", typeof(string));
        //   dtCompanyforDropDownlist.Columns.Add("COMP_NM", typeof(string));

        //   DataRow drCompanyNameforDropDownlist = dtCompanyforDropDownlist.NewRow();
        //   drCompanyNameforDropDownlist["COMP_NM"] = "--Select Compnay Name--- ";
        //   drCompanyNameforDropDownlist["COMP_CD"] = "0";

        //   dtCompanyforDropDownlist.Rows.Add(drCompanyNameforDropDownlist);

        //   for (int loop = 0; loop < dtCompany.Rows.Count; loop++)
        //   {
        //       drCompanyNameforDropDownlist = dtCompanyforDropDownlist.NewRow();
        //       drCompanyNameforDropDownlist["COMP_NM"] = dtCompany.Rows[loop]["COMP_NM"].ToString();
        //       drCompanyNameforDropDownlist["COMP_CD"] = dtCompany.Rows[loop]["COMP_CD"].ToString();
        //       dtCompanyforDropDownlist.Rows.Add(drCompanyNameforDropDownlist);
        //   }
        //   return dtCompanyforDropDownlist;
        //}
        //public DataTable dtFundNameforDropDownlist()
        //{
        //    DataTable dtFundName = commonGatewayObj.Select("SELECT * FROM FUND WHERE  (IS_F_CLOSE IS NULL) AND (BOID IS NOT NULL) AND F_CD NOT IN (18) ORDER BY F_CD");
        //    DataTable dtFundNameforDropDownlist = new DataTable();
        //    dtFundNameforDropDownlist.Columns.Add("F_CD",typeof(string));
        //    dtFundNameforDropDownlist.Columns.Add("F_NAME", typeof(string));

        //    DataRow drFundNameforDropDownlist = dtFundNameforDropDownlist.NewRow();
        //    drFundNameforDropDownlist["F_NAME"] = "----All Fund ---- ";
        //    drFundNameforDropDownlist["F_CD"] = "0";

        //    dtFundNameforDropDownlist.Rows.Add(drFundNameforDropDownlist);

        //    for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        //    {
        //        drFundNameforDropDownlist = dtFundNameforDropDownlist.NewRow();
        //        drFundNameforDropDownlist["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
        //        drFundNameforDropDownlist["F_CD"] = dtFundName.Rows[loop]["F_CD"].ToString();
        //        dtFundNameforDropDownlist.Rows.Add(drFundNameforDropDownlist);              
        //    }
        //    return dtFundNameforDropDownlist;
        //}
        //public DataTable dtGetSaleBuyTable()
        //{
        //    DataTable dtSaleBuy = new DataTable();
        //    dtSaleBuy.Columns.Add("SI", typeof(int));
        //    dtSaleBuy.Columns.Add("COMP_CD", typeof(int));
        //    dtSaleBuy.Columns.Add("SECT_MAJ_CD", typeof(int));
        //    dtSaleBuy.Columns.Add("COMP_NM", typeof(string));
        //    dtSaleBuy.Columns.Add("SECT_MAJ_NM", typeof(string));
        //    dtSaleBuy.Columns.Add("SL_SHARE", typeof(decimal));
        //    dtSaleBuy.Columns.Add("SL_RATE", typeof(decimal));
        //    dtSaleBuy.Columns.Add("SL_AMT", typeof(decimal));
        //    dtSaleBuy.Columns.Add("BUY_SHARE", typeof(decimal));
        //    dtSaleBuy.Columns.Add("BUY_RATE", typeof(decimal));
        //    dtSaleBuy.Columns.Add("BUY_AMT", typeof(decimal));
        //    return dtSaleBuy;


        //}
        //public DataTable dtSaleInfo(string toDate, string fromDate, string f_cd,int comp_Cd)
        //{
        //    StringBuilder sbQuery = new StringBuilder();
        //    sbQuery.Append("SELECT NVL(SUM(NO_SHARE), 0) AS SL_SHARE, NVL(SUM(AMT_AFT_COM), 0) AS SL_AMT, NVL(ROUND(SUM(AMT_AFT_COM) / SUM(NO_SHARE), 2), 0) AS SL_RATE ");
        //    sbQuery.Append(" FROM FUND_TRANS_HB WHERE (VCH_DT BETWEEN '" + fromDate + "' AND '" + toDate + "') AND (F_CD IN ( " +f_cd + ") ) AND COMP_CD=" + comp_Cd + "  AND (TRAN_TP IN('S'))");
        //    DataTable dtSaleInfo = commonGatewayObj.Select(sbQuery.ToString());
        //    return dtSaleInfo;
        //}
        //public DataTable dtPurchaseInfo(string toDate, string fromDate, string f_cd, int comp_Cd)
        //{
        //    StringBuilder sbQuery = new StringBuilder();
        //    sbQuery.Append("SELECT NVL(SUM(NO_SHARE), 0) AS BUY_SHARE, NVL(SUM(AMT_AFT_COM), 0) AS BUY_AMT, NVL(ROUND(SUM(AMT_AFT_COM) / SUM(NO_SHARE), 2), 0) AS BUY_RATE ");
        //    sbQuery.Append(" FROM FUND_TRANS_HB WHERE (VCH_DT BETWEEN '" + fromDate + "' AND '" + toDate + "')  AND (F_CD IN ( " + f_cd + ")) AND COMP_CD=" + comp_Cd + "  AND (TRAN_TP IN('C','R','I','P'))");
        //    DataTable dtPurchaseInfo = commonGatewayObj.Select(sbQuery.ToString());
        //    return dtPurchaseInfo;
        //}
        //public DataTable HowlaDateDropDownList()//Get Howla Date from invest.fund_trans_hb Table
        //{
        //    DataTable dtHowlaDate = commonGatewayObj.Select("SELECT DISTINCT VCH_DT    FROM   FUND_TRANS_HB  ORDER BY VCH_DT DESC");
        //    DataTable dtHowlaDateDropDownList = new DataTable();
        //    dtHowlaDateDropDownList.Columns.Add("Howla_Date", typeof(string));
        //    dtHowlaDateDropDownList.Columns.Add("VCH_DT", typeof(string));
        //    DataRow dr = dtHowlaDateDropDownList.NewRow();
        //    dr["Howla_Date"] = "--Select--";
        //    dr["VCH_DT"] = "0";
        //    dtHowlaDateDropDownList.Rows.Add(dr);
        //    for (int loop = 0; loop < dtHowlaDate.Rows.Count; loop++)
        //    {
        //        dr = dtHowlaDateDropDownList.NewRow();
        //        dr["Howla_Date"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["VCH_DT"]).ToString("dd-MMM-yyyy");
        //        dr["VCH_DT"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["VCH_DT"]).ToString("dd-MMM-yyyy");
        //        dtHowlaDateDropDownList.Rows.Add(dr);
        //    }
        //    return dtHowlaDateDropDownList;
        //}
        //public DataTable FundNameDropDownList()//For All Funds
        //{
        //    DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM INVEST.FUND WHERE F_CD < 27 ORDER BY F_CD");
        //    DataTable dtFundNameDropDownList = new DataTable();
        //    dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        //    dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        //    DataRow dr = dtFundNameDropDownList.NewRow();
        //    dr["F_NAME"] = "--Click Here to Select--";
        //    dr["F_CD"] = "0";
        //    dtFundNameDropDownList.Rows.Add(dr);
        //    for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        //    {
        //        dr = dtFundNameDropDownList.NewRow();
        //        dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
        //        dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
        //        dtFundNameDropDownList.Rows.Add(dr);
        //    }
        //    return dtFundNameDropDownList;
        //}
    public bool getMPUpdateStatus(string MPDate, string type)
    {
        bool MPUpdateStatus = false;
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT * FROM MARKET_PRICE WHERE TRAN_DATE='" + MPDate + "'");
        if (type == "DSE")
        {
            sbQuery.Append(" AND DSE_RT IS NOT NULL ");
        }
        else if (type == "CSE")
        {
            sbQuery.Append(" AND CSE_RT IS NOT NULL ");
        }
        else if (type == "AVERAGE")
        {
            sbQuery.Append(" AND AVG_RT IS NOT NULL ");
        }

        DataTable dtMPUpdateStatus = commonGatewayObj.Select(sbQuery.ToString());
        if (dtMPUpdateStatus.Rows.Count > 0)
        {
            MPUpdateStatus = true;
        }
        return MPUpdateStatus;
    }

    public bool getCompUpdateStatus(string MPDate)
    {
        bool MPUpdateStatus = false;
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("SELECT * FROM COMP WHERE rt_upd_dt ='" + MPDate + "' and CSE_DT='" + MPDate + "'");
      

        DataTable dtMPUpdateStatus = commonGatewayObj.Select(sbQuery.ToString());
        if (dtMPUpdateStatus.Rows.Count > 0)
        {
            MPUpdateStatus = true;
        }
        return MPUpdateStatus;
    }
    public bool getHowlaUpdateStatus(string MPDate, string type)
    {
        bool status = false;
        StringBuilder sbQuery = new StringBuilder();
        if (type == "DSE")
        {
            sbQuery.Append("SELECT * FROM HOWLA WHERE SP_DATE='" + MPDate + "'");
        }
        else if (type == "CSE")
        {
            sbQuery.Append("SELECT * FROM HOWLA_CSE WHERE SP_DATE='" + MPDate + "'");
        }

        DataTable dtHowlaUpdateStatus = commonGatewayObj.Select(sbQuery.ToString());
        if (dtHowlaUpdateStatus.Rows.Count > 0)
        {
            status = true;
        }
        return status;
    }

}


