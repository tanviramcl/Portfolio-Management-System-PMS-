using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
/// <summary>
/// Summary description for DividendDAO
/// </summary>
public class DividendDAO
{
    CommonGateway commonGatewayObj = new CommonGateway();
	public DividendDAO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable dtBankNameList()
    {
        DataTable dtBankNameList = commonGatewayObj.Select("SELECT     BANK_CODE, BANK_NAME FROM         UNIT.BANK_NAME where BANK_CODE not in(100) ORDER BY BANK_NAME");
        DataTable dtBankNameListDropDown = new DataTable();
        dtBankNameListDropDown.Columns.Add("BANK_NAME", typeof(string));
        dtBankNameListDropDown.Columns.Add("BANK_CODE", typeof(string));

        DataRow drBankNameListDropDown = dtBankNameListDropDown.NewRow();

        drBankNameListDropDown["BANK_NAME"] = "--Select Bank Name--";
        drBankNameListDropDown["BANK_CODE"] = "0";
        dtBankNameListDropDown.Rows.Add(drBankNameListDropDown);
        for (int loop = 0; loop < dtBankNameList.Rows.Count; loop++)
        {
            drBankNameListDropDown = dtBankNameListDropDown.NewRow();
            drBankNameListDropDown["BANK_NAME"] = dtBankNameList.Rows[loop]["BANK_NAME"].ToString();
            drBankNameListDropDown["BANK_CODE"] = dtBankNameList.Rows[loop]["BANK_CODE"].ToString();
            dtBankNameListDropDown.Rows.Add(drBankNameListDropDown);
        }
        return dtBankNameListDropDown;

    }
    public DataTable dtFundList()
    {
        DataTable dtFundList = commonGatewayObj.Select("SELECT F_CD , F_NAME FROM FUND WHERE F_CD IN(SELECT DISTINCT(FUND_CODE) FROM DIVIDEND_PARA) ORDER BY F_CD");
        DataTable dtFundListDropDown = new DataTable();
        dtFundListDropDown.Columns.Add("F_CD", typeof(string));
        dtFundListDropDown.Columns.Add("F_NAME", typeof(string));

        DataRow drFundListDropDown = dtFundListDropDown.NewRow();

        drFundListDropDown["F_NAME"] = "--Select Fund--- ";
        drFundListDropDown["F_CD"] = "0";
        dtFundListDropDown.Rows.Add(drFundListDropDown);
        for (int loop = 0; loop < dtFundList.Rows.Count; loop++)
        {
            drFundListDropDown = dtFundListDropDown.NewRow();
            drFundListDropDown["F_NAME"] = dtFundList.Rows[loop]["F_NAME"].ToString();// + ", F_CODE [" + dtFundList.Rows[loop]["F_CD"].ToString()+"]";
            drFundListDropDown["F_CD"] = dtFundList.Rows[loop]["F_CD"].ToString();
            dtFundListDropDown.Rows.Add(drFundListDropDown);
        }
        return dtFundListDropDown;

    }
    public DataTable dtFY()
    {

        DataTable dtList = commonGatewayObj.Select("SELECT DISTINCT FY FROM DIVIDEND_PARA ORDER BY FY DESC");
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("FY", typeof(string));
        dtDropDown.Columns.Add("FY_VALUE", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["FY"] = "--Select FY--- ";
        drdtDropDown["FY_VALUE"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["FY"] = dtList.Rows[loop]["FY"].ToString();
            drdtDropDown["FY_VALUE"] = dtList.Rows[loop]["FY"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtWindingUpFundName()
    {
        DataTable dtList = commonGatewayObj.Select("SELECT     F_CD, F_NAME             FROM         FUND         WHERE     (F_CD IN   (SELECT     FUND_CODE   FROM          WINDING_UP_PARA))    ORDER BY F_CD");
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("F_NAME", typeof(string));
        dtDropDown.Columns.Add("F_CD", typeof(string));
      
        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["F_NAME"] = "--Select FY--- ";
        drdtDropDown["F_CD"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["F_NAME"] = dtList.Rows[loop]["F_NAME"].ToString();
            drdtDropDown["F_CD"] = dtList.Rows[loop]["F_CD"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtWindingUpDate(string fundCode)
    {

        DataTable dtList = commonGatewayObj.Select("SELECT TO_CHAR(WINDING_UP_DATE,'DD-MON-YYYY') AS WINDING_UP_DATE from WINDING_UP_PARA where fund_code=" + fundCode);
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("WINDING_UP_DATE", typeof(string));
        dtDropDown.Columns.Add("WINDING_UP_DATE_value", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["WINDING_UP_DATE"] = "--Select Record Date--- ";
        drdtDropDown["WINDING_UP_DATE_value"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["WINDING_UP_DATE"] = dtList.Rows[loop]["WINDING_UP_DATE"].ToString();
            drdtDropDown["WINDING_UP_DATE_value"] = dtList.Rows[loop]["WINDING_UP_DATE"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtRecordDateFYWise(string FY)
    {

        DataTable dtList = commonGatewayObj.Select("SELECT DISTINCT TO_CHAR(RECORD_DATE,'DD-MON-YYYY') AS RECORD_DATE FROM DIVIDEND_PARA WHERE FY='" + FY + "' ORDER BY RECORD_DATE DESC");
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("RECORD_DATE", typeof(string));
        dtDropDown.Columns.Add("RECORD_DATE_VALUE", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["RECORD_DATE"] = "--Select Record Date--- ";
        drdtDropDown["RECORD_DATE_VALUE"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["RECORD_DATE"] = dtList.Rows[loop]["RECORD_DATE"].ToString();
            drdtDropDown["RECORD_DATE_VALUE"] = dtList.Rows[loop]["RECORD_DATE"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtAllOmnibusCompanyFYWise(string FY,string cdblFileType)
    {
        DataTable dtList = new DataTable();
        if (String.Compare(cdblFileType.ToString(), "singleFile", true) == 0)
        {
            dtList = commonGatewayObj.Select("SELECT DISTINCT BO, NAME1 FROM CDBL_DATA WHERE (BO IN (SELECT BO FROM  DIVI_OMNIBUS_BO WHERE VALID IS NULL)) AND (FY = '" + FY + "')");
        }
        else if (String.Compare(cdblFileType.ToString(), "multipleFile", true) == 0)
        {
            dtList = commonGatewayObj.Select("SELECT DISTINCT BO, NAME1 FROM CDBL_BO_HOLDER WHERE (BO IN (SELECT BO FROM  DIVI_OMNIBUS_BO WHERE VALID IS NULL)) AND (FY = '" + FY + "')");
        }
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("BO", typeof(string));
        dtDropDown.Columns.Add("NAME1", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["NAME1"] = "--Select Company Name--- ";
        drdtDropDown["BO"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["NAME1"] = dtList.Rows[loop]["NAME1"].ToString();
            drdtDropDown["BO"] = dtList.Rows[loop]["BO"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtAllFundNameRecordDateWise(string fy, string recordDate)
    {
        DataTable dtList = commonGatewayObj.Select("SELECT Distinct FUND.F_NAME,FUND.F_CD FROM  DIVIDEND_PARA INNER JOIN FUND ON DIVIDEND_PARA.FUND_CODE = FUND.F_CD WHERE (DIVIDEND_PARA.RECORD_DATE = '" + recordDate + "') and (DIVIDEND_PARA.FY = '"+ fy +"') ORDER BY FUND.F_CD");
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("F_CD", typeof(string));
        dtDropDown.Columns.Add("F_NAME", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["F_NAME"] = "--Select Fund Name--- ";
        drdtDropDown["F_CD"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["F_NAME"] = dtList.Rows[loop]["F_NAME"].ToString();
            drdtDropDown["F_CD"] = dtList.Rows[loop]["F_CD"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtAllOnlineBankFYWise(string FY, string recordDate)
    {
        DataTable dtList = new DataTable();

        dtList = commonGatewayObj.Select("SELECT DISTINCT NAME1, BANK_ID       FROM         ONLINE_TRANSFER_SUMMARY  WHERE     (FY = '" + FY + "') AND (RECORD_DATE = '" + recordDate + "')");
       
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("BANK_ID", typeof(string));
        dtDropDown.Columns.Add("NAME1", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["NAME1"] = "--Select Online Bank--- ";
        drdtDropDown["BANK_ID"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["NAME1"] = dtList.Rows[loop]["NAME1"].ToString();
            drdtDropDown["BANK_ID"] = dtList.Rows[loop]["BANK_ID"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable dtAllFundNameOTS(string fy, string recordDate, string bankId)
    {
        DataTable dtList = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbfilter.Append(" ");
        sbOrderBy.Append("");
        sbMst.Append(" SELECT     ONLINE_TRANSFER_SUMMARY.FUND_CODE, FUND.F_NAME ");
        sbMst.Append(" FROM         FUND INNER JOIN ");
        sbMst.Append(" ONLINE_TRANSFER_SUMMARY ON FUND.F_CD = ONLINE_TRANSFER_SUMMARY.FUND_CODE ");
        sbMst.Append(" WHERE     (ONLINE_TRANSFER_SUMMARY.FY = '" + fy + "') AND (ONLINE_TRANSFER_SUMMARY.RECORD_DATE = '" + recordDate + "') AND ");
        sbMst.Append(" (ONLINE_TRANSFER_SUMMARY.BANK_ID =" + bankId + ") AND (ONLINE_TRANSFER_SUMMARY.IS_TRAN_SUCCESS IS NULL) ");
        sbOrderBy.Append(" ORDER BY ONLINE_TRANSFER_SUMMARY.FUND_CODE ");
        sbMst.Append(sbfilter.ToString());
        sbMst.Append(sbOrderBy.ToString());
        
        dtList = commonGatewayObj.Select(sbMst.ToString());
        
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("FUND_CODE", typeof(string));
        dtDropDown.Columns.Add("F_NAME", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["F_NAME"] = "--Select Fund Name--- ";
        drdtDropDown["FUND_CODE"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["F_NAME"] = dtList.Rows[loop]["F_NAME"].ToString();
            drdtDropDown["FUND_CODE"] = dtList.Rows[loop]["FUND_CODE"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }
    public DataTable GetWindingUpPara(int fundCode,  string windingUpDate)
    {

        DataTable dtDividendPara = commonGatewayObj.Select("SELECT * FROM WINDING_UP_PARA WHERE FUND_CODE=" + fundCode + " AND WINDING_UP_DATE='" + Convert.ToDateTime(windingUpDate).ToString("dd-MMM-yyyy") + "'");
        return dtDividendPara;
    }
    public DataTable GetDividendPara(int fundCode, string ficalYear, string recordDate)
    {
        
        DataTable dtDividendPara = commonGatewayObj.Select("SELECT * FROM DIVIDEND_PARA WHERE FUND_CODE=" + fundCode + "AND FY='" + ficalYear + "' AND RECORD_DATE='" + Convert.ToDateTime( recordDate).ToString("dd-MMM-yyyy") + "'");
        return dtDividendPara;
    }
    public DataTable GetDividendPara(int fundCode, string ficalYear)
    {
        DataTable dtDividendPara = commonGatewayObj.Select("SELECT * FROM DIVIDEND_PARA WHERE FUND_CODE=" + fundCode + "AND FY='" + ficalYear + "'");
        return dtDividendPara;
    }    
    public DataTable GetDividendParaTableName(int fundCode, string ficalYear)
    {
        DataTable dtDividendParaTableName = commonGatewayObj.Select("SELECT * FROM DIVIDEND_PARA WHERE FUND_CODE=" + fundCode + "AND FY='" + ficalYear + "'");
        return dtDividendParaTableName;
    }
    public bool IsUnPaidNO(string tableName, string fy, int fundCode, int unPaidNo)
    {
        DataTable dtCompCode = commonGatewayObj.Select("SELECT * FROM " + tableName + "_DIVIDEND WHERE FY = '" + fy + "' AND FUND_CODE = " + fundCode + " AND UNPAID_NO = " + unPaidNo);
        if (dtCompCode.Rows.Count > 0)
            return true;
        else
            return false;
    }
    public DataTable dtGetCDBLData(int fundCode, string ficalYear, string recordDate, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     CDBL_BO_HOLDER.FUND_CODE, CDBL_BO_HOLDER.DIVI_NO, CDBL_BO_HOLDER.FY, DIVIDEND_PARA.RECORD_DATE, ");
        sb.Append(" CDBL_BO_HOLDER.BO, CDBL_BO_ADDRESS.NAME1, CDBL_BO_ADDRESS.NAME2, CDBL_BO_HOLDER.BO_TYPE, ");
        sb.Append(" CDBL_BO_HOLDER.BO_CATAGORY, CDBL_BO_ADDRESS.ADDRESS1, CDBL_BO_ADDRESS.ADDRESS2, CDBL_BO_ADDRESS.ADDRESS3, ");
        sb.Append(" CDBL_BO_ADDRESS.ADDRESS4, CDBL_BO_ADDRESS.CITY, CDBL_BO_ADDRESS.POST_CODE, CDBL_BO_ADDRESS.COUNTRY, ");
        sb.Append(" CDBL_BO_HOLDER.RESIDENCY, CDBL_BO_ADDRESS.EMAIL, CDBL_BO_ADDRESS.PHONE1, CDBL_BO_ADDRESS.PHONE2, CDBL_BO_BANK.BANK, ");
        sb.Append(" CDBL_BO_BANK.BRANCH, CDBL_BO_BANK.BANK_ACC, CDBL_BO_BANK.BANK_ID, CDBL_BO_HOLDER.BALANCE ");
        sb.Append(" FROM         CDBL_BO_HOLDER INNER JOIN ");
        sb.Append(" CDBL_BO_ADDRESS ON CDBL_BO_HOLDER.DIVI_NO = CDBL_BO_ADDRESS.DIVI_NO AND ");
        sb.Append(" CDBL_BO_HOLDER.FUND_CODE = CDBL_BO_ADDRESS.FUND_CODE AND CDBL_BO_HOLDER.FY = CDBL_BO_ADDRESS.FY AND ");
        sb.Append(" CDBL_BO_HOLDER.BO = CDBL_BO_ADDRESS.BO INNER JOIN ");
        sb.Append(" CDBL_BO_BANK ON CDBL_BO_ADDRESS.DIVI_NO = CDBL_BO_BANK.DIVI_NO AND  ");
        sb.Append(" CDBL_BO_ADDRESS.FUND_CODE = CDBL_BO_BANK.FUND_CODE AND CDBL_BO_ADDRESS.FY = CDBL_BO_BANK.FY AND  ");
        sb.Append(" CDBL_BO_ADDRESS.BO = CDBL_BO_BANK.BO INNER JOIN ");
        sb.Append(" DIVIDEND_PARA ON CDBL_BO_HOLDER.DIVI_NO = DIVIDEND_PARA.DIVI_NO AND ");
        sb.Append(" CDBL_BO_HOLDER.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND CDBL_BO_HOLDER.FY = DIVIDEND_PARA.FY AND ");
        sb.Append(" CDBL_BO_BANK.DIVI_NO = DIVIDEND_PARA.DIVI_NO AND CDBL_BO_BANK.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND ");
        sb.Append(" CDBL_BO_BANK.FY = DIVIDEND_PARA.FY  ");
        sb.Append(" WHERE (DIVIDEND_PARA.FUND_CODE = " + fundCode + ") AND (DIVIDEND_PARA.FY = '" + ficalYear + "') AND (DIVIDEND_PARA.RECORD_DATE = '" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "')" + filter);
        
        DataTable dtCDBLData = commonGatewayObj.Select(sb.ToString());
        return dtCDBLData;
    }
    public DataTable dtGetCDBLDataSingleFile(int fundCode, string ficalYear, string recordDate, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT      FUND_CODE, DIVI_NO, FY, RECORD_DATE, BO, NAME1, BALANCE, BO_TYPE, BO_CATAGORY, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, ");
        sb.Append(" CITY, COUNTRY, POST_CODE, PHONE1, PHONE2, RESIDENCY, BANK, BRANCH, BANK_ACC, ROUTING_NO, BANK_ID,IS_BEFTN ");
        sb.Append(" FROM         CDBL_DATA ");
        sb.Append(" WHERE  (VALID IS NULL) AND (FUND_CODE = " + fundCode + ") AND (FY = '" + ficalYear + "') AND (RECORD_DATE = '" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "')" + filter);

        DataTable dtCDBLData = commonGatewayObj.Select(sb.ToString());
        return dtCDBLData;
    }
    public DataTable dtGetCDBLDataIDaccBO(int fundCode, string ficalYear, string recordDate, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT      FUND_CODE, DIVI_NO, FY, RECORD_DATE, SUBSTR(BO, 4, 5) AS DP_ID, BO, NAME1, BO_TYPE, BALANCE ");
        sb.Append(" FROM         CDBL_DATA ");
        sb.Append(" WHERE   (FUND_CODE = " + fundCode + ") AND (FY = '" + ficalYear + "') AND (RECORD_DATE = '" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "')" + filter);

        DataTable dtCDBLData = commonGatewayObj.Select(sb.ToString());
        return dtCDBLData;
    }
    public DataTable dtGetDividendData(int fundCode, int dividendNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM DIVIDEND WHERE (WAR_NO IS NULL) AND (VALID IS NULL) AND (IS_ONLINE IS NULL) AND (NET_DIVIDEND IS NOT NULL) AND FUND_CODE = " + fundCode + " AND DIVI_NO = " + dividendNo + " ORDER BY ID");
        DataTable dtDividendData = commonGatewayObj.Select(sb.ToString());
        return dtDividendData;
    }
    public DataTable dtGetOnlineDividendData(int fundCode, int dividendNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM DIVIDEND WHERE (WAR_NO IS NULL) AND (VALID IS NULL) AND (IS_ONLINE = 'YES') AND (NET_DIVIDEND IS NOT NULL) AND FUND_CODE = " + fundCode + " AND DIVI_NO = " + dividendNo + " ORDER BY BANK_ID,ID");
        DataTable dtDividendData = commonGatewayObj.Select(sb.ToString());
        return dtDividendData;
    }
    public DataTable dtGetNonBEFTNDividendData(int fundCode, int dividendNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM DIVIDEND WHERE (WAR_NO IS NULL) AND (VALID IS NULL) AND (IS_BEFTN ='NO') AND (NET_DIVIDEND IS NOT NULL) AND FUND_CODE = " + fundCode + " AND DIVI_NO = " + dividendNo + " ORDER BY ID");
        DataTable dtDividendData = commonGatewayObj.Select(sb.ToString());
        return dtDividendData;
    }
    public DataTable dtGetBEFTNDividendData(int fundCode, int dividendNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM DIVIDEND WHERE (WAR_NO IS NULL) AND (VALID IS NULL) AND (IS_BEFTN = 'YES') AND (NET_DIVIDEND IS NOT NULL) AND FUND_CODE = " + fundCode + " AND DIVI_NO = " + dividendNo + " ORDER BY ROUTING_NO,ID");
        DataTable dtDividendData = commonGatewayObj.Select(sb.ToString());
        return dtDividendData;
    }
    public DataTable dtGetFolioWindingUpData(int fundCode)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM WINDING_UP_DATA WHERE (VOTTER_NO IS NULL) AND (VALID IS NULL) AND (FOLIO_NO IS NOT NULL) AND FUND_CODE = " + fundCode + " ORDER BY TO_NUMBER(FOLIO_NO)");
        DataTable dtWindingUpData = commonGatewayObj.Select(sb.ToString());
        return dtWindingUpData;
    }
    public DataTable dtGetBoWindingUpData(int fundCode)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT * FROM WINDING_UP_DATA WHERE (VOTTER_NO IS NULL) AND (VALID IS NULL) AND (BO IS NOT NULL) AND FUND_CODE = " + fundCode + " ORDER BY TO_NUMBER(BO)");
        DataTable dtWindingUpData = commonGatewayObj.Select(sb.ToString());
        return dtWindingUpData;
    }
    public DataTable dtGetCDBLData(string fundTableName, string Filter)
    {
        DataTable dtCDBLData = commonGatewayObj.Select("SELECT * FROM " + fundTableName + "_CDBL WHERE " + Filter);


        return dtCDBLData;
    }
    
    public DataTable dtGetFolioOrAllotData(int fundCode,int diviNo, string filter)
    {
        DataTable dtFolioOrAllotData = commonGatewayObj.Select("SELECT * FROM FOLIO_ALLOT_DATA WHERE FUND_CODE = " +fundCode+" AND DIVI_NO = " +diviNo + filter);

        return dtFolioOrAllotData;
    }
    
    public DataTable GetDividendData(string fundTableName, string Filter)
    {
        DataTable dtDividendData = commonGatewayObj.Select("SELECT * FROM " + fundTableName + "_DIVIDEND WHERE " + Filter+" AND VALID IS NULL ORDER BY WAR_NO");
        return dtDividendData;
    }
    public DataTable GetWindingUpData(int fundCode, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     FUND.F_NAME, WINDING_UP_DATA.WINDING_UP_DATE, WINDING_UP_DATA.BO, WINDING_UP_DATA.ALLOT_NO,  ");
        sb.Append(" WINDING_UP_DATA.FOLIO_NO, WINDING_UP_DATA.NAME1, WINDING_UP_DATA.BO_FATHER,WINDING_UP_DATA.BO_MOTHER,WINDING_UP_DATA.BALANCE, WINDING_UP_DATA.BO_TYPE, ");
        sb.Append(" WINDING_UP_DATA.BO_CATAGORY, WINDING_UP_DATA.ADDRESS1, WINDING_UP_DATA.ADDRESS2, WINDING_UP_DATA.ADDRESS3, ");
        sb.Append(" WINDING_UP_DATA.ADDRESS4, WINDING_UP_DATA.CITY, WINDING_UP_DATA.POST_CODE, WINDING_UP_DATA.COUNTRY, ");
        sb.Append(" WINDING_UP_DATA.PHONE1, WINDING_UP_DATA.PHONE2, WINDING_UP_DATA.RESIDENCY, WINDING_UP_DATA.EMAIL, WINDING_UP_DATA.BANK, ");
        sb.Append(" WINDING_UP_DATA.BRANCH, WINDING_UP_DATA.BANK_ACC, WINDING_UP_DATA.ROUTING_NO,WINDING_UP_DATA.VOTTER_NO ");
        sb.Append(" FROM         FUND INNER JOIN ");
        sb.Append(" WINDING_UP_DATA ON FUND.F_CD = WINDING_UP_DATA.FUND_CODE ");
        sb.Append(" WHERE (WINDING_UP_DATA.VALID IS NULL) AND (WINDING_UP_DATA.FUND_CODE = " + fundCode + ") " + filter);
        sb.Append(" ORDER BY WINDING_UP_DATA.VOTTER_NO ");

        DataTable dtWindingUpData = commonGatewayObj.Select(sb.ToString());
        return dtWindingUpData;
    }
    public DataTable GetLetterOfEntitlementofUnitsData(int fundCode, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     FUND.F_NAME, WINDING_UP_PARA.CONVERTED_FUND_NAME, WINDING_UP_PARA.WINDING_UP_DATE, WINDING_UP_PARA.NAV, WINDING_UP_PARA.ISSUED_UNIT_PRICE,    ");
        sb.Append(" WINDING_UP_PARA.SPECIAL_MEETING_DATE, WINDING_UP_DATA.BO, WINDING_UP_DATA.ALLOT_NO, WINDING_UP_DATA.FOLIO_NO,   ");
        sb.Append(" WINDING_UP_DATA.NAME1, WINDING_UP_DATA.BALANCE, WINDING_UP_DATA.BO_TYPE, WINDING_UP_DATA.BO_CATAGORY,  ");
        sb.Append(" WINDING_UP_DATA.BO_FATHER, WINDING_UP_DATA.BO_MOTHER, WINDING_UP_DATA.ADDRESS1, WINDING_UP_DATA.ADDRESS2,  ");
        sb.Append(" WINDING_UP_DATA.ADDRESS3, WINDING_UP_DATA.ADDRESS4, WINDING_UP_DATA.CITY, WINDING_UP_DATA.POST_CODE,  ");
        sb.Append("  WINDING_UP_DATA.COUNTRY, WINDING_UP_DATA.PHONE1, WINDING_UP_DATA.PHONE2, WINDING_UP_DATA.RESIDENCY,  ");
        sb.Append(" WINDING_UP_DATA.EMAIL, WINDING_UP_DATA.BANK, WINDING_UP_DATA.BRANCH, WINDING_UP_DATA.BANK_ACC, ");
        sb.Append(" WINDING_UP_DATA.ROUTING_NO, WINDING_UP_DATA.GROSS_AMOUNT, WINDING_UP_DATA.CONVERTED_UNITS, ");
        sb.Append(" WINDING_UP_DATA.FRACTION_AMT ");
        sb.Append(" FROM         WINDING_UP_DATA INNER JOIN ");
        sb.Append(" WINDING_UP_PARA ON WINDING_UP_DATA.FUND_CODE = WINDING_UP_PARA.FUND_CODE INNER JOIN ");
        sb.Append(" FUND ON WINDING_UP_PARA.FUND_CODE = FUND.F_CD ");
        sb.Append(" WHERE (WINDING_UP_DATA.VALID IS NULL) AND (WINDING_UP_DATA.FUND_CODE = " + fundCode + ") " + filter);
        sb.Append(" ORDER BY WINDING_UP_DATA.VOTTER_NO ");

        DataTable dtLetterOfEntitlementofUnitsData = commonGatewayObj.Select(sb.ToString());
        return dtLetterOfEntitlementofUnitsData;
    }
    public DataTable GetDividendWarrantData(int fundCode, int diviNo, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     FUND.F_NAME, DIVIDEND_PARA.FUND_CODE, DIVIDEND_PARA.DIVI_NO, DIVIDEND_PARA.FY, DIVIDEND_PARA.RECORD_DATE, ");
        sb.Append(" DIVIDEND_PARA.AGM_DATE, DIVIDEND_PARA.ISSUE_DATE, DIVIDEND_PARA.FACE_VALUE, DIVIDEND_PARA.DIVI_RATE, ((DIVIDEND_PARA.FACE_VALUE * DIVIDEND_PARA.DIVI_RATE)/100) AS DIVI_RATE_PER_UNIT, DIVIDEND_PARA.TAX_LIMIT, ");
        sb.Append(" DIVIDEND_PARA.TAX_RATE_INDV, DIVIDEND_PARA.TAX_RATE_ORG, DIVIDEND_PARA.BANK_NAME, DIVIDEND_PARA.BANK_ACC_NO, ");
        sb.Append(" DIVIDEND_PARA.BANK_ADDRS1, DIVIDEND_PARA.BANK_ADDRS2, DIVIDEND_PARA.BANK_ROUTING_NO, DIVIDEND_PARA.BANK_TRANSACTION_NO, ");
        sb.Append(" DIVIDEND.BO, DIVIDEND.ALLOT_NO, DIVIDEND.FOLIO_NO, LPAD(DIVIDEND.WAR_NO,7,'0') AS WAR_NO, DIVIDEND.NAME1, DIVIDEND.NAME2, DIVIDEND.BO_TYPE, ");
        sb.Append(" DIVIDEND.BO_CATAGORY, DIVIDEND.ADDRESS1, DIVIDEND.ADDRESS2, DIVIDEND.ADDRESS3, DIVIDEND.ADDRESS4, DIVIDEND.CITY, ");
        sb.Append(" DIVIDEND.POST_CODE, DIVIDEND.COUNTRY, DIVIDEND.RESIDENCY, DIVIDEND.BALANCE, DIVIDEND.GROSS_DIVIDEND, DIVIDEND.DIDUCT, ");
        sb.Append(" DIVIDEND.NET_DIVIDEND, decode(DIVIDEND.IS_BEFTN,'YES','BEFTN','WARRANT') AS PAY_SYS,DIVIDEND.DP_ID  ");
        sb.Append(" FROM         DIVIDEND_PARA INNER JOIN ");
        sb.Append(" DIVIDEND ON DIVIDEND_PARA.FUND_CODE = DIVIDEND.FUND_CODE AND DIVIDEND_PARA.DIVI_NO = DIVIDEND.DIVI_NO INNER JOIN ");
        sb.Append(" FUND ON DIVIDEND_PARA.FUND_CODE = FUND.F_CD ");
        sb.Append(" WHERE (DIVIDEND.VALID IS NULL) AND (DIVIDEND_PARA.FUND_CODE = " + fundCode + ") AND (DIVIDEND_PARA.DIVI_NO = " + diviNo + ")" + filter);
        sb.Append(" ORDER BY DIVIDEND.WAR_NO ");

        DataTable dtDividendwarrantData = commonGatewayObj.Select(sb.ToString());
        return dtDividendwarrantData;
    }
    public DataTable GetDividendWarrantDataBankList(int fundCode, int diviNo, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     FUND.F_NAME, DIVIDEND_PARA.FUND_CODE, DIVIDEND_PARA.DIVI_NO, DIVIDEND_PARA.FY, DIVIDEND_PARA.RECORD_DATE, ");
        sb.Append(" DIVIDEND_PARA.AGM_DATE, DIVIDEND_PARA.ISSUE_DATE, DIVIDEND_PARA.FACE_VALUE, DIVIDEND_PARA.DIVI_RATE, ((DIVIDEND_PARA.FACE_VALUE * DIVIDEND_PARA.DIVI_RATE)/100) AS DIVI_RATE_PER_UNIT, DIVIDEND_PARA.TAX_LIMIT, ");
        sb.Append(" DIVIDEND_PARA.TAX_RATE_INDV, DIVIDEND_PARA.TAX_RATE_ORG, DIVIDEND_PARA.BANK_NAME, DIVIDEND_PARA.BANK_ACC_NO, ");
        sb.Append(" DIVIDEND_PARA.BANK_ADDRS1, DIVIDEND_PARA.BANK_ADDRS2, DIVIDEND_PARA.BANK_ROUTING_NO, DIVIDEND_PARA.BANK_TRANSACTION_NO, ");
        sb.Append(" DIVIDEND.BO, DIVIDEND.ALLOT_NO, DIVIDEND.FOLIO_NO, LPAD(DIVIDEND.WAR_NO,7,'0') AS WAR_NO, DIVIDEND.NAME1, DIVIDEND.NAME2, DIVIDEND.BO_TYPE, ");
        sb.Append(" DIVIDEND.BO_CATAGORY, DIVIDEND.ADDRESS1, DIVIDEND.ADDRESS2, DIVIDEND.ADDRESS3, DIVIDEND.ADDRESS4, DIVIDEND.CITY, ");
        sb.Append(" DIVIDEND.POST_CODE, DIVIDEND.COUNTRY, DIVIDEND.RESIDENCY, DIVIDEND.BALANCE, DIVIDEND.GROSS_DIVIDEND, DIVIDEND.DIDUCT, ");
        sb.Append(" DIVIDEND.NET_DIVIDEND,DIVIDEND.BANK AS BANK_NAME_HOLDER,DIVIDEND.BRANCH AS BRANCH_NAME_HOLDER,DIVIDEND.BANK_ACC AS BANK_ACC_HOLDER,DIVIDEND.DP_ID ");
        sb.Append(" FROM         DIVIDEND_PARA INNER JOIN ");
        sb.Append(" DIVIDEND ON DIVIDEND_PARA.FUND_CODE = DIVIDEND.FUND_CODE AND DIVIDEND_PARA.DIVI_NO = DIVIDEND.DIVI_NO INNER JOIN ");
        sb.Append(" FUND ON DIVIDEND_PARA.FUND_CODE = FUND.F_CD ");
        sb.Append(" WHERE (DIVIDEND.VALID IS NULL) AND (DIVIDEND_PARA.FUND_CODE = " + fundCode + ") AND (DIVIDEND_PARA.DIVI_NO = " + diviNo + ")" + filter);
        sb.Append(" ORDER BY DIVIDEND.WAR_NO ");

        DataTable dtDividendwarrantData = commonGatewayObj.Select(sb.ToString());
        return dtDividendwarrantData;
    }
    public DataTable GetDividendWarrantPrintData(int fundCode, int diviNo, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     FUND.F_NAME, DIVIDEND_PARA.FY, TO_CHAR(DIVIDEND_PARA.RECORD_DATE, 'DD-MON-YYYY') AS RECORD_DATE,  ");
        sb.Append(" TO_CHAR(DIVIDEND_PARA.AGM_DATE, 'DD-MON-YYYY') AS AGM_DATE, TO_CHAR(DIVIDEND_PARA.ISSUE_DATE, 'DD-MON-YYYY') AS ISSUE_DATE, ");
        sb.Append(" LTRIM(TO_CHAR(DIVIDEND_PARA.FACE_VALUE * DIVIDEND_PARA.DIVI_RATE / 100, '99999999999.99'), ' ') AS DIVI_RATE_PER_UNIT, ");
        sb.Append(" DIVIDEND_PARA.TAX_RATE_INDV || '%' AS TAX_RATE_INDV, DIVIDEND_PARA.TAX_RATE_ORG || '%' AS TAX_RATE_ORG, ");
        sb.Append(" DIVIDEND_PARA.BANK_NAME, DIVIDEND_PARA.BANK_ACC_NO, DIVIDEND_PARA.BANK_ACC_NO_MICR || 'C' AS BANK_ACC_NO_MICR, ");
        sb.Append(" DIVIDEND_PARA.BANK_ADDRS1, DIVIDEND_PARA.BANK_ADDRS2, DIVIDEND_PARA.BANK_ROUTING_NO, ");
        sb.Append(" DIVIDEND_PARA.BANK_ROUTING_NO || 'A' AS BANK_ROUTING_NO_MICR, DIVIDEND_PARA.BANK_TRANSACTION_NO,  ");
        sb.Append(" TO_CHAR(DIVIDEND_PARA.YEAR_END_DATE, 'DD-MON-YYYY') AS YEAR_END_DATE, LPAD(DIVIDEND.WAR_NO, 7, '0') AS WAR_NO,  ");
        sb.Append(" 'C' || LPAD(DIVIDEND.WAR_NO, 7, '0') || 'C' AS WAR_NO_MICR, DIVIDEND.BO, DIVIDEND.FOLIO_NO, DIVIDEND.ALLOT_NO, DIVIDEND.NAME1, ");
        sb.Append(" DIVIDEND.ADDRESS1, DIVIDEND.ADDRESS2, ");
        sb.Append(" DIVIDEND.ADDRESS3 || ' ' || DIVIDEND.ADDRESS4 || ' ' || DIVIDEND.CITY || '-' || DIVIDEND.POST_CODE || ' ' || DIVIDEND.COUNTRY AS ADDRESS3, ");
        sb.Append(" DIVIDEND.BO_TYPE, DIVIDEND.BO_CATAGORY, DIVIDEND.BALANCE AS NO_OF_UNITS, LTRIM(TO_CHAR(DIVIDEND.GROSS_DIVIDEND, ");
        sb.Append(" '999999999999.99'), ' ') AS GROSS_DIVIDEND, DECODE(SIGN(DIVIDEND.DIDUCT), 1, DIVIDEND.DIDUCT, 0) AS TAX_DEDUCTION, LTRIM(TO_CHAR(DIVIDEND.NET_DIVIDEND,   ");
        sb.Append(" '999999999999.99'), ' ') AS NET_DIVIDEND, '=' || LTRIM(TO_CHAR(DIVIDEND.NET_DIVIDEND, '999999999999.99') || '=', ' ') AS NET_DIVIDEND_CHECK,  ");
        sb.Append(" 'Taka ' || trim(DIVIDEND.NET_DIVIDEND_IN_WORDS) || ' Only' AS DIVI_WORD ");
        sb.Append(" FROM         DIVIDEND INNER JOIN ");
        sb.Append(" DIVIDEND_PARA ON DIVIDEND.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DIVIDEND.DIVI_NO = DIVIDEND_PARA.DIVI_NO INNER JOIN ");
        sb.Append(" FUND ON DIVIDEND_PARA.FUND_CODE = FUND.F_CD ");
        sb.Append(" WHERE (DIVIDEND.VALID IS NULL) AND (DIVIDEND.WAR_NO IS NOT NULL) AND (DIVIDEND.FUND_CODE = " + fundCode + ") AND (DIVIDEND.DIVI_NO = " + diviNo + ")" + filter);
        sb.Append(" ORDER BY DIVIDEND.WAR_NO ");

        DataTable dtDividendwarrantPrintData = commonGatewayObj.Select(sb.ToString());
        return dtDividendwarrantPrintData;
    }
    public DataTable GetIntimationData(int fundCode, int diviNo, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     FUND.F_NAME, DIVIDEND_PARA.FY, TO_CHAR(DIVIDEND_PARA.RECORD_DATE, 'DD-MON-YYYY') AS RECORD_DATE,  ");
        sb.Append(" TO_CHAR(DIVIDEND_PARA.AGM_DATE, 'DD-MON-YYYY') AS AGM_DATE, TO_CHAR(DIVIDEND_PARA.ISSUE_DATE, 'DD-MON-YYYY') AS ISSUE_DATE, ");
        sb.Append(" LTRIM(TO_CHAR(DIVIDEND_PARA.FACE_VALUE * DIVIDEND_PARA.DIVI_RATE / 100, '99999999999.99'), ' ') AS DIVI_RATE_PER_UNIT, ");
        sb.Append(" DIVIDEND_PARA.TAX_RATE_INDV || '%' AS TAX_RATE_INDV, DIVIDEND_PARA.TAX_RATE_ORG || '%' AS TAX_RATE_ORG, ");
        sb.Append(" DIVIDEND_PARA.BANK_NAME, DIVIDEND_PARA.BANK_ACC_NO, DIVIDEND_PARA.BANK_ACC_NO_MICR || 'C' AS BANK_ACC_NO_MICR, ");
        sb.Append(" DIVIDEND_PARA.BANK_ADDRS1, DIVIDEND_PARA.BANK_ADDRS2, DIVIDEND_PARA.BANK_ROUTING_NO, ");
        sb.Append(" DIVIDEND_PARA.BANK_ROUTING_NO || 'A' AS BANK_ROUTING_NO_MICR, DIVIDEND_PARA.BANK_TRANSACTION_NO,  ");
        sb.Append(" TO_CHAR(DIVIDEND_PARA.YEAR_END_DATE, 'DD-MON-YYYY') AS YEAR_END_DATE, LPAD(DIVIDEND.WAR_NO, 7, '0') AS WAR_NO,  ");
        sb.Append(" 'C' || LPAD(DIVIDEND.WAR_NO, 7, '0') || 'C' AS WAR_NO_MICR, DIVIDEND.BO, DIVIDEND.FOLIO_NO, DIVIDEND.ALLOT_NO, DIVIDEND.NAME1, ");
        sb.Append(" DIVIDEND.ADDRESS1, DIVIDEND.ADDRESS2,  ");
        sb.Append(" DIVIDEND.ADDRESS3 || ',' || DIVIDEND.ADDRESS4 || ',' || DIVIDEND.CITY || '-' || DIVIDEND.POST_CODE || ',' || DIVIDEND.COUNTRY AS ADDRESS3, ");
        sb.Append(" DIVIDEND.BO_TYPE, DIVIDEND.BO_CATAGORY, DIVIDEND.BALANCE AS NO_OF_UNITS, LTRIM(TO_CHAR(DIVIDEND.GROSS_DIVIDEND, ");
        sb.Append(" '999999999999.99'), ' ') AS GROSS_DIVIDEND, DECODE(SIGN(DIVIDEND.DIDUCT), 1, DIVIDEND.DIDUCT, 0) AS TAX_DEDUCTION, LTRIM(TO_CHAR(DIVIDEND.NET_DIVIDEND,   ");
        sb.Append(" '999999999999.99'), ' ') AS NET_DIVIDEND, '=' || LTRIM(TO_CHAR(DIVIDEND.NET_DIVIDEND, '999999999999.99') || '=', ' ') AS NET_DIVIDEND_CHECK,  ");
        sb.Append(" DIVIDEND.bank as bank_name_unit_holder, DIVIDEND.branch as bank_branch_unit_holder, DIVIDEND.bank_acc as bank_acc_unit_holder, ");
        sb.Append(" 'Taka ' || trim(DIVIDEND.NET_DIVIDEND_IN_WORDS) || ' Only' AS DIVI_WORD ");
        sb.Append(" FROM         DIVIDEND INNER JOIN ");
        sb.Append(" DIVIDEND_PARA ON DIVIDEND.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DIVIDEND.DIVI_NO = DIVIDEND_PARA.DIVI_NO INNER JOIN ");
        sb.Append(" FUND ON DIVIDEND_PARA.FUND_CODE = FUND.F_CD ");
        sb.Append(" WHERE (DIVIDEND.VALID IS NULL) AND (DIVIDEND.WAR_NO IS NOT NULL) AND (DIVIDEND.FUND_CODE = " + fundCode + ") AND (DIVIDEND.DIVI_NO = " + diviNo + ")" + filter);
        sb.Append(" ORDER BY DIVIDEND.WAR_NO ");

        DataTable dtDividendwarrantPrintData = commonGatewayObj.Select(sb.ToString());
        return dtDividendwarrantPrintData;
    }
    public DataTable GetOnlineBankAdviceData(string fy, string recordDate, string filter)
    {
        StringBuilder sbMaster = new StringBuilder();

        sbMaster.Append(" SELECT     ONLINE_TRANSFER_SUMMARY.FY, ONLINE_TRANSFER_SUMMARY.NAME1, TRIM(UNIT.BANK_NAME.BANK_MD_BR_ADDRS) AS BANK_MD_BR_ADDRS, UNIT.BANK_NAME.BANK_MD_FAX,  ");
        sbMaster.Append(" UNIT.BANK_NAME.BANK_MD_PHONE, FUND.F_NAME, LPAD(ONLINE_TRANSFER_SUMMARY.WAR_NO, 7, '0') as WAR_NO,  ");
        sbMaster.Append(" ONLINE_TRANSFER_SUMMARY.NO_OF_WARRANT, ONLINE_TRANSFER_SUMMARY.NET_DIVIDEND ");
        sbMaster.Append(" FROM         UNIT.BANK_NAME INNER JOIN ");
        sbMaster.Append(" ONLINE_TRANSFER_SUMMARY ON UNIT.BANK_NAME.BANK_CODE = ONLINE_TRANSFER_SUMMARY.BANK_ID INNER JOIN ");
        sbMaster.Append(" FUND ON ONLINE_TRANSFER_SUMMARY.FUND_CODE = FUND.F_CD ");
        sbMaster.Append(" WHERE     (ONLINE_TRANSFER_SUMMARY.FY = '" + fy + "') AND (ONLINE_TRANSFER_SUMMARY.RECORD_DATE = '" + recordDate + "') " + filter);
        sbMaster.Append(" ORDER BY ONLINE_TRANSFER_SUMMARY.NAME1, ONLINE_TRANSFER_SUMMARY.FUND_CODE ");

        DataTable dtOnlineBankAdviceData = commonGatewayObj.Select(sbMaster.ToString());
        return dtOnlineBankAdviceData;
    }

    public DataTable GetOnlineReturnWarrantData(int fundCode, int diviNo, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     FUND.F_NAME, DIVIDEND_PARA.FY, TO_CHAR(DIVIDEND_PARA.RECORD_DATE, 'DD-MON-YYYY') AS RECORD_DATE,  ");
        sb.Append(" TO_CHAR(DIVIDEND_PARA.AGM_DATE, 'DD-MON-YYYY') AS AGM_DATE, TO_CHAR(DIVIDEND_PARA.ISSUE_DATE, 'DD-MON-YYYY') AS ISSUE_DATE, ");
        sb.Append(" (DIVIDEND_PARA.FACE_VALUE * DIVIDEND_PARA.DIVI_RATE / 100) AS DIVI_RATE_PER_UNIT, ");
        sb.Append(" DIVIDEND_PARA.TAX_RATE_INDV || '%' AS TAX_RATE_INDV, DIVIDEND_PARA.TAX_RATE_ORG || '%' AS TAX_RATE_ORG, ");
        sb.Append(" DIVIDEND_PARA.BANK_NAME, DIVIDEND_PARA.BANK_ACC_NO, DIVIDEND_PARA.BANK_ACC_NO_MICR || 'C' AS BANK_ACC_NO_MICR, ");
        sb.Append(" DIVIDEND_PARA.BANK_ADDRS1, DIVIDEND_PARA.BANK_ADDRS2, DIVIDEND_PARA.BANK_ROUTING_NO, ");
        sb.Append(" DIVIDEND_PARA.BANK_ROUTING_NO || 'A' AS BANK_ROUTING_NO_MICR, DIVIDEND_PARA.BANK_TRANSACTION_NO,  ");
        sb.Append(" TO_CHAR(DIVIDEND_PARA.YEAR_END_DATE, 'DD-MON-YYYY') AS YEAR_END_DATE, LPAD(DIVIDEND.WAR_NO, 7, '0') AS WAR_NO,  ");
        sb.Append(" 'C' || LPAD(DIVIDEND.WAR_NO, 7, '0') || 'C' AS WAR_NO_MICR, DIVIDEND.BO, DIVIDEND.FOLIO_NO, DIVIDEND.ALLOT_NO, DIVIDEND.NAME1, ");
        sb.Append(" DIVIDEND.ADDRESS1, DIVIDEND.ADDRESS2,  ");
        sb.Append(" DIVIDEND.ADDRESS3 || ',' || DIVIDEND.ADDRESS4 || ',' || DIVIDEND.CITY || '-' || DIVIDEND.POST_CODE || ',' || DIVIDEND.COUNTRY AS ADDRESS3, ");
        sb.Append(" DIVIDEND.BO_TYPE, DIVIDEND.BO_CATAGORY, DIVIDEND.BALANCE AS NO_OF_UNITS, (DIVIDEND.GROSS_DIVIDEND ");
        sb.Append(" ) AS GROSS_DIVIDEND, DECODE(SIGN(DIVIDEND.DIDUCT), 1, DIVIDEND.DIDUCT, 0) AS TAX_DEDUCTION, (DIVIDEND.NET_DIVIDEND  ");
        sb.Append(" ) AS NET_DIVIDEND, '=' || LTRIM(TO_CHAR(DIVIDEND.NET_DIVIDEND, '999999999999.99') || '=', ' ') AS NET_DIVIDEND_CHECK,  ");
        sb.Append(" DIVIDEND.bank as bank_name_unit_holder, DIVIDEND.branch as bank_branch_unit_holder, DIVIDEND.bank_acc as bank_acc_unit_holder, DIVIDEND.BEFTN_RETURN_DATE as ONLINE_RETURN_DATE, ");
        sb.Append(" 'Taka ' || trim(DIVIDEND.NET_DIVIDEND_IN_WORDS) || ' Only' AS DIVI_WORD, Dividend.Routing_no ");
        sb.Append(" FROM         DIVIDEND INNER JOIN ");
        sb.Append(" DIVIDEND_PARA ON DIVIDEND.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DIVIDEND.DIVI_NO = DIVIDEND_PARA.DIVI_NO INNER JOIN ");
        sb.Append(" FUND ON DIVIDEND_PARA.FUND_CODE = FUND.F_CD ");
        sb.Append(" WHERE (DIVIDEND.VALID IS NULL) AND (DIVIDEND.WAR_NO IS NOT NULL) AND (DIVIDEND.FUND_CODE = " + fundCode + ") AND (DIVIDEND.DIVI_NO = " + diviNo + ")" + filter);
        sb.Append(" ORDER BY DIVIDEND.WAR_NO ");

        DataTable dtOnlineReturnWarrantPrintData = commonGatewayObj.Select(sb.ToString());
        return dtOnlineReturnWarrantPrintData;
    }
    
    //public string QueryStringForExportData(int fundCode, int diviNo, string filter)//FOR IFIC BANK
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append(" SELECT     TO_CHAR(DIVIDEND_PARA.ISSUE_DATE, 'DD-MON-YYYY') AS ISSUE_DATE, DIVIDEND.BANK_ACC AS RECEIVING_ACC_NO,   ");
    //    sb.Append(" DIVIDEND.NAME1 AS RECEIVING_ACC_NAME, DIVIDEND.NET_DIVIDEND AS NET_DIVIDEND, ");
    //    sb.Append(" DIVIDEND_PARA.BANK_ROUTING_NO AS ORIGINATING_BR_ROUTING_NO, DIVIDEND.ROUTING_NO AS RECEIVING_BR_ROUTING_NO, ");
    //    sb.Append(" DIVIDEND_PARA.BANK_ACC_NO AS ORIGINATING_ACC_NO, (INVEST.FUND.F_NAME || ' ' || DIVIDEND_PARA.FY) AS ORIGINATOR, DIVIDEND.WAR_NO ");
    //    sb.Append(" FROM         DIVIDEND INNER JOIN ");
    //    sb.Append(" DIVIDEND_PARA ON DIVIDEND.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DIVIDEND.DIVI_NO = DIVIDEND_PARA.DIVI_NO INNER JOIN ");
    //    sb.Append(" INVEST.FUND ON DIVIDEND_PARA.FUND_CODE = INVEST.FUND.F_CD  ");
    //    sb.Append(" WHERE (DIVIDEND.VALID IS NULL) AND (DIVIDEND.WAR_NO IS NOT NULL) AND (DIVIDEND.FUND_CODE = " + fundCode + ") AND (DIVIDEND.DIVI_NO = " + diviNo + ")" + filter);
    //    sb.Append(" ORDER BY DIVIDEND.WAR_NO ");
    //    return sb.ToString();
    //}

    public string QueryStringForExportData(int fundCode, int diviNo, string filter)//FOR SHAHJALAL BANK
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     ROUTING_NO AS RECV_BRANCH_CODE, 22 AS TX_CODE, BANK_ACC AS RECV_AC_NO, 0 AS DEBIT, NET_DIVIDEND AS CREDIT,  ");
        sb.Append(" WAR_NO AS RECV_ID, NAME1 AS RECV_NAME, BO AS PAYMENT_INFO  ");
        sb.Append(" FROM         DIVIDEND ");
        sb.Append(" WHERE (VALID IS NULL) AND (WAR_NO IS NOT NULL) AND (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ")" + filter);
        sb.Append(" ORDER BY RECV_ID ");
        return sb.ToString();
    }
   
    public string QueryStringForAddingRoutingNo(int fundCode, int diviNo, string filter)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     BO, NAME1, BANK, BRANCH, BANK_ACC, ROUTING_NO   ");
        sb.Append(" FROM         CDBL_DATA ");
        sb.Append(" WHERE  (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ")" + filter);
        sb.Append(" ORDER BY ID ");
        return sb.ToString();
    }
    public DataTable GetTotalAmount(int fundCode, int diviNo, string filter, string columnName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT SUM("+columnName+") AS TOTAL_AMOUNT FROM DIVIDEND ");
        sb.Append(" WHERE (VALID IS NULL) AND (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ")" + filter);

        DataTable dtDividendwarrantData = commonGatewayObj.Select(sb.ToString());
        return dtDividendwarrantData;
    }
    public DataTable GetUnpaidData(string fundTableName, string Filter)
    {
        DataTable dtUnpaidData = commonGatewayObj.Select("SELECT * FROM " + fundTableName + "_DIVIDEND WHERE " + Filter);
        return dtUnpaidData;
    }
    public int checkNagativeBalance(string fundTableName, string Filter)
    {
         DataTable dtCheckNagativeBalance = commonGatewayObj.Select("SELECT COUNT(*) AS NAG_BALANCE FROM " + fundTableName + "_CDBL WHERE " + Filter);
         int NagativeBalance = dtCheckNagativeBalance.Rows[0]["NAG_BALANCE"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtCheckNagativeBalance.Rows[0]["NAG_BALANCE"].ToString());
         return NagativeBalance;

    }
    public int checkZeroBalance(string fundTableName, string Filter)
    {
        DataTable dtcheckZeroBalance = commonGatewayObj.Select("SELECT COUNT(*) AS ZERO_BALANCE FROM " + fundTableName + "_CDBL WHERE " + Filter);
        int zeroBalance = dtcheckZeroBalance.Rows[0]["ZERO_BALANCE"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtcheckZeroBalance.Rows[0]["ZERO_BALANCE"].ToString());
        return zeroBalance;

    }
    public int CheckNegativeOrZeroBalance(string tableName, int fundCode, string fisclYear, int diviNo, string filter)
    {
        DataTable dtcheckZeroBalance = commonGatewayObj.Select("SELECT COUNT(*) AS ZERO_NEGATIVE_BALANCE FROM " + tableName + " WHERE FUND_CODE = " + fundCode + " AND FY = '" + fisclYear + "' AND DIVI_NO = " + diviNo + " AND " + filter);
        int zeroOrNegativeBalance = dtcheckZeroBalance.Rows[0]["ZERO_NEGATIVE_BALANCE"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtcheckZeroBalance.Rows[0]["ZERO_NEGATIVE_BALANCE"].ToString());
        return zeroOrNegativeBalance;

    }
    public int CheckBOlengthNot16(string tableName, int fundCode, string fisclYear, int diviNo, string filter)
    {
        DataTable dtcheckBOlengthNot16 = commonGatewayObj.Select("SELECT COUNT(*) AS BO_LENGTH_NOT_16 FROM " + tableName + " WHERE FUND_CODE = " + fundCode + " AND FY = '" + fisclYear + "' AND DIVI_NO = " + diviNo + " AND " + filter);
        int noOfBOlengthNot16 = dtcheckBOlengthNot16.Rows[0]["BO_LENGTH_NOT_16"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtcheckBOlengthNot16.Rows[0]["BO_LENGTH_NOT_16"].ToString());
        return noOfBOlengthNot16;
    }
    public int CheckBOTypeCategoryName(string tableName, int fundCode, string fisclYear, int diviNo, string filter)
    {
        DataTable dtCheckBOTypeCategoryName = commonGatewayObj.Select("SELECT COUNT(*) AS NO_OF_ROWS FROM " + tableName + " WHERE FUND_CODE = " + fundCode + " AND FY = '" + fisclYear + "' AND DIVI_NO = " + diviNo + " AND " + filter);
        int noOfRows = dtCheckBOTypeCategoryName.Rows[0]["NO_OF_ROWS"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtCheckBOTypeCategoryName.Rows[0]["NO_OF_ROWS"].ToString());
        return noOfRows;
    }
    public string GetFundNameByFundCode(int fundCode)
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME FROM FUND WHERE F_CD=" + fundCode);
        string fundName = dtFundName.Rows[0]["F_NAME"].Equals(DBNull.Value) ? "" : dtFundName.Rows[0]["F_NAME"].ToString();
        return fundName;
    }
    public string GetMutualFundBOFromCDBL(string tableName)
    {
        string mutualFundBO = "";
        DataTable dtMutualFundBO = commonGatewayObj.Select("SELECT BO FROM " + tableName.ToString() + "_CDBL WHERE (UPPER(NAME1) LIKE'%MUTUAL FUND%'OR  UPPER(NAME1) LIKE'%UNIT FUND%') AND ALLOT_NO IS NULL AND BO IS NOT NULL");
        if (dtMutualFundBO.Rows.Count > 0)
        {
            for(int loop=0;loop<dtMutualFundBO.Rows.Count;loop++)
            {
                if (mutualFundBO == "")
                {
                    mutualFundBO ="'"+ mutualFundBO + dtMutualFundBO.Rows[loop]["BO"].ToString()+"'";
                }
                else if (mutualFundBO != "")
                {
                    mutualFundBO=mutualFundBO+",'"+ dtMutualFundBO.Rows[loop]["BO"].ToString()+"'";
                }
            }
        }
       
        return mutualFundBO;
    }
    public string GetMutualFundOrOmnibusBO(string tableName)
    {
        string mutualFundOrOmnibusBO = "";
        DataTable dtMutualFundOrOmnibusBO = commonGatewayObj.Select("SELECT BO FROM " + tableName.ToString()+" WHERE VALID IS NULL");
        if (dtMutualFundOrOmnibusBO.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtMutualFundOrOmnibusBO.Rows.Count; loop++)
            {
                if (mutualFundOrOmnibusBO == "")
                {
                    mutualFundOrOmnibusBO = "'" + mutualFundOrOmnibusBO + dtMutualFundOrOmnibusBO.Rows[loop]["BO"].ToString() + "'";
                }
                else if (mutualFundOrOmnibusBO != "")
                {
                    mutualFundOrOmnibusBO = mutualFundOrOmnibusBO + ",'" + dtMutualFundOrOmnibusBO.Rows[loop]["BO"].ToString() + "'";
                }
            }
        }

        return mutualFundOrOmnibusBO;
    }
    public string GetMutualFundFolioInfo(int fundCode, int diviNo)
    {
        string mutualFundFolio = "";
        DataTable dtMutualFundFolio = commonGatewayObj.Select("SELECT FOLIO_NO FROM FOLIO_ALLOT_DATA WHERE (UPPER(NAME1) LIKE'%MUTUAL FUND%'OR  UPPER(NAME1) LIKE'%UNIT FUND%') AND (ALLOT_NO IS NULL) AND (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ")");
        if (dtMutualFundFolio.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtMutualFundFolio.Rows.Count; loop++)
            {
                if (mutualFundFolio == "")
                {
                    mutualFundFolio = "'" + mutualFundFolio + dtMutualFundFolio.Rows[loop]["FOLIO_NO"].ToString() + "'";
                }
                else if (mutualFundFolio != "")
                {
                    mutualFundFolio = mutualFundFolio + ",'" + dtMutualFundFolio.Rows[loop]["FOLIO_NO"].ToString() + "'";
                }
            }
        }

        return mutualFundFolio;
    }
    public string GetMutualFundAllotInfo(int fundCode, int diviNo)
    {
        string mutualFundAllot = "";
        DataTable dtMutualFundAllot = commonGatewayObj.Select("SELECT ALLOT_NO FROM FOLIO_ALLOT_DATA WHERE (UPPER(NAME1) LIKE'%MUTUAL FUND%'OR  UPPER(NAME1) LIKE'%UNIT FUND%') AND (ALLOT_NO IS NOT NULL) AND (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ")");
        if (dtMutualFundAllot.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtMutualFundAllot.Rows.Count; loop++)
            {
                if (mutualFundAllot == "")
                {
                    mutualFundAllot = "'" + mutualFundAllot + dtMutualFundAllot.Rows[loop]["FOLIO_NO"].ToString() + "'";
                }
                else if (mutualFundAllot != "")
                {
                    mutualFundAllot = mutualFundAllot + ",'" + dtMutualFundAllot.Rows[loop]["FOLIO_NO"].ToString() + "'";
                }
            }
        }

        return mutualFundAllot;
    }
    public string GetMutualFundFolioFromCDBL(string tableName)
    {
        string mutualFundFolio = "";
        DataTable dtMutualFundFolio = commonGatewayObj.Select("SELECT FOLIO_NO FROM " + tableName.ToString() + "_CDBL WHERE (UPPER(NAME1) LIKE'%MUTUAL FUND%'OR  UPPER(NAME1) LIKE'%UNIT FUND%') AND BO IS NULL AND FOLIO_NO IS NOT NULL");
        if (dtMutualFundFolio.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtMutualFundFolio.Rows.Count; loop++)
            {
                if (mutualFundFolio == "")
                {
                    mutualFundFolio = "'" + mutualFundFolio + dtMutualFundFolio.Rows[loop]["FOLIO_NO"].ToString() + "'";
                }
                else if (mutualFundFolio != "")
                {
                    mutualFundFolio = mutualFundFolio + ",'" + dtMutualFundFolio.Rows[loop]["FOLIO_NO"].ToString() + "'";
                }
            }
        }

        return mutualFundFolio;
    }
    public string GetMutualFundAllotFromCDBL(string tableName)
    {
        string mutualFundAllot = "";
        DataTable dtMutualFundAllot = commonGatewayObj.Select("SELECT ALLOT_NO FROM " + tableName.ToString() + "_CDBL WHERE (UPPER(NAME1) LIKE'%MUTUAL FUND%'OR  UPPER(NAME1) LIKE'%UNIT FUND%') AND BO IS NULL AND ALLOT_NO IS NOT NULL");
        if (dtMutualFundAllot.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtMutualFundAllot.Rows.Count; loop++)
            {
                if (mutualFundAllot == "")
                {
                    mutualFundAllot = "'" + mutualFundAllot + dtMutualFundAllot.Rows[loop]["ALLOT_NO"].ToString() + "'";
                }
                else if (mutualFundAllot != "")
                {
                    mutualFundAllot = mutualFundAllot + ",'" + dtMutualFundAllot.Rows[loop]["ALLOT_NO"].ToString() + "'";
                }
            }
        }

        return mutualFundAllot;
    }
    public string GetMutualFundBOFromDividend(string tableName)
    {
        string mutualFundBO = "";
        DataTable dtMutualFundBO = commonGatewayObj.Select("SELECT BO FROM " + tableName.ToString() + "_DIVIDEND WHERE UPPER(NAME1) LIKE'%MUTUAL FUND%'OR  UPPER(NAME1) LIKE'%UNIT FUND%'");
        if (dtMutualFundBO.Rows.Count > 0)
        {
            for (int loop = 0; loop < dtMutualFundBO.Rows.Count; loop++)
            {
                if (mutualFundBO == "")
                {
                    mutualFundBO = "'" + mutualFundBO + dtMutualFundBO.Rows[loop]["BO"].ToString() + "'";
                }
                else if (mutualFundBO != "")
                {
                    mutualFundBO = mutualFundBO + ",'" + dtMutualFundBO.Rows[loop]["BO"].ToString() + "'";
                }
            }
        }

        return mutualFundBO;
    }
    public int GetMaxWarrantNo(string tableName, int fundCode, string ficalYear, string recordDate)
    {
        int maxWarrantNo = 0;
        DataTable dtDividendPara = commonGatewayObj.Select("SELECT MAX(WAR_NO) AS WAR_NO FROM " + tableName + "_DIVIDEND WHERE FUND_CODE=" + fundCode + "AND FY='" + ficalYear + "'AND RECORD_DATE='" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "'");
        if (dtDividendPara.Rows.Count > 0)
        {
            maxWarrantNo =dtDividendPara.Rows[0]["WAR_NO"].Equals(DBNull.Value) ? 0: Convert.ToInt32(dtDividendPara.Rows[0]["WAR_NO"].ToString());
        }
        return maxWarrantNo;
    }
    public int GetMaxWarrantNo(int fundCode, int dividendNo)
    {
        int maxWarrantNo = 0;
        DataTable dtDiviWarrantNo = commonGatewayObj.Select("SELECT MAX(WAR_NO) AS WAR_NO FROM DIVIDEND WHERE FUND_CODE = " + fundCode + " AND DIVI_NO = " + dividendNo);
        if (dtDiviWarrantNo.Rows.Count > 0)
        {
            maxWarrantNo = dtDiviWarrantNo.Rows[0]["WAR_NO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtDiviWarrantNo.Rows[0]["WAR_NO"].ToString());
        }
        return maxWarrantNo;
    }
    public long GetID()
    {
        long  maxID = 0;
        DataTable dtDividendID = commonGatewayObj.Select("SELECT MAX(ID) AS ID FROM DIVIDEND ");
        if (dtDividendID.Rows.Count > 0)
        {
            maxID = dtDividendID.Rows[0]["ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dtDividendID.Rows[0]["ID"].ToString());
        }
        return maxID;
    }
    public long GetID(string tableName)
    {
        long maxID = 0;
        DataTable dtMaxID = commonGatewayObj.Select("SELECT MAX(ID) AS ID FROM "+tableName);
        if (dtMaxID.Rows.Count > 0)
        {
            maxID = dtMaxID.Rows[0]["ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dtMaxID.Rows[0]["ID"].ToString());
        }
        return maxID;
    }
    public int GetMaxSI(string tableName, string ColumnName)
    {
        int maxSI = 0;
        DataTable dtMaxSI = commonGatewayObj.Select("SELECT MAX(" + ColumnName + ") AS " + ColumnName + " FROM " + tableName );
        if (dtMaxSI.Rows.Count > 0)
        {
            maxSI = dtMaxSI.Rows[0][ColumnName].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxSI.Rows[0][ColumnName].ToString());
        }
        return maxSI;
    }
    public int GetMaxVotterNo(string tableName, string ColumnName, int fundCode)
    {
        int maxVotterNo = 0;
        DataTable dtMaxVotterNo = commonGatewayObj.Select("SELECT MAX(" + ColumnName + ") AS " + ColumnName + " FROM " + tableName + " WHERE FUND_CODE = "+fundCode);
        if (dtMaxVotterNo.Rows.Count > 0)
        {
            maxVotterNo = dtMaxVotterNo.Rows[0][ColumnName].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxVotterNo.Rows[0][ColumnName].ToString());
        }
        return maxVotterNo;
    }
    public DataTable getWindingUpReportTable()
    {
        DataTable dtReportTable = new DataTable();
        dtReportTable.Columns.Add("SI", typeof(int));
        dtReportTable.Columns.Add("FUND_CODE", typeof(int));
        dtReportTable.Columns.Add("WINDING_UP_DATE", typeof(DateTime));
        dtReportTable.Columns.Add("BO", typeof(string));
        dtReportTable.Columns.Add("ALLOT_NO", typeof(string));
        dtReportTable.Columns.Add("FOLIO_NO", typeof(string));
        dtReportTable.Columns.Add("NAME1", typeof(string));
        dtReportTable.Columns.Add("BO_FATHER", typeof(string));
        dtReportTable.Columns.Add("BO_MOTHER", typeof(string));
        dtReportTable.Columns.Add("BALANCE", typeof(decimal));
        dtReportTable.Columns.Add("BO_TYPE", typeof(string));
        dtReportTable.Columns.Add("BO_CATAGORY", typeof(string));
        dtReportTable.Columns.Add("ADDRESS1", typeof(string));
        dtReportTable.Columns.Add("ADDRESS2", typeof(string));
        dtReportTable.Columns.Add("ADDRESS3", typeof(string));
        dtReportTable.Columns.Add("CITY", typeof(string));
        dtReportTable.Columns.Add("POST_CODE", typeof(string));
        dtReportTable.Columns.Add("COUNTRY", typeof(string));
        dtReportTable.Columns.Add("PHONE1", typeof(string));
        dtReportTable.Columns.Add("PHONE2", typeof(string));
        dtReportTable.Columns.Add("FUND_NAME", typeof(string));
        dtReportTable.Columns.Add("SIGN", System.Type.GetType("System.Byte[]"));

        return dtReportTable;


    }
    public DataTable getReportTable()
    {
        DataTable dtReportTable = new DataTable();
        dtReportTable.Columns.Add("SI", typeof(int));
        dtReportTable.Columns.Add("DIVI_NO", typeof(int));
        dtReportTable.Columns.Add("FUND_CODE", typeof(int));
        dtReportTable.Columns.Add("FY", typeof(string));
        dtReportTable.Columns.Add("RECORD_DATE", typeof(DateTime));
        dtReportTable.Columns.Add("BO", typeof(string));
        dtReportTable.Columns.Add("NAME1", typeof(string));
        dtReportTable.Columns.Add("NAME2", typeof(string));
        dtReportTable.Columns.Add("BO_TYPE", typeof(string));
        dtReportTable.Columns.Add("BO_CATAGORY", typeof(string));
        dtReportTable.Columns.Add("ADDRESS1", typeof(string));
        dtReportTable.Columns.Add("ADDRESS2", typeof(string));
        dtReportTable.Columns.Add("ADDRESS3", typeof(string));
        dtReportTable.Columns.Add("CITY", typeof(string));
        dtReportTable.Columns.Add("POST_CODE", typeof(string));
        dtReportTable.Columns.Add("COUNTRY", typeof(string));
        dtReportTable.Columns.Add("BALANCE", typeof(decimal));
        dtReportTable.Columns.Add("DIVIDEND", typeof(decimal));
        dtReportTable.Columns.Add("DIDUCT", typeof(decimal));
        dtReportTable.Columns.Add("FINAL_DIVIDEND", typeof(decimal));
        dtReportTable.Columns.Add("WAR_NO", typeof(int));
        dtReportTable.Columns.Add("ALLOT_NO", typeof(string));
        dtReportTable.Columns.Add("FOLIO_NO", typeof(string));
        dtReportTable.Columns.Add("REMARKS", typeof(string));
        dtReportTable.Columns.Add("IN_WORDS", typeof(string));
        dtReportTable.Columns.Add("BO_FOLIO",typeof(string));
        dtReportTable.Columns.Add("FUND_NAME", typeof(string));
        //dtReportTable.Columns.Add("SIGN", System.Type.GetType("System.Byte[]"));
        
        return dtReportTable;
       

    }

    public DataTable getdtBOUploadTable()
    {
        DataTable dtBOUploadTable = new DataTable();

        dtBOUploadTable.Columns.Add("SI", typeof(string));
        dtBOUploadTable.Columns.Add("FUND_NAME", typeof(string));
        dtBOUploadTable.Columns.Add("BO", typeof(string));
        dtBOUploadTable.Columns.Add("NAME1", typeof(string));
        dtBOUploadTable.Columns.Add("NAME2", typeof(string));
        dtBOUploadTable.Columns.Add("BO_FATHER", typeof(string));
        dtBOUploadTable.Columns.Add("BO_MOTHER", typeof(string));
        dtBOUploadTable.Columns.Add("BO_TYPE", typeof(string));
        dtBOUploadTable.Columns.Add("BO_CATAGORY", typeof(string));
        dtBOUploadTable.Columns.Add("COUNTRY", typeof(string));
        dtBOUploadTable.Columns.Add("BANK_ACC_NO", typeof(string));
        dtBOUploadTable.Columns.Add("BANK_NAME", typeof(string));
        dtBOUploadTable.Columns.Add("BRANCH_NAME", typeof(string));
        dtBOUploadTable.Columns.Add("RESIDENCY", typeof(string));
        dtBOUploadTable.Columns.Add("BALANCE", typeof(string));
        dtBOUploadTable.Columns.Add("DIVIDEND", typeof(string));
        dtBOUploadTable.Columns.Add("TAX", typeof(string));
        dtBOUploadTable.Columns.Add("FINAL_DIVIDEND", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS1", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS2", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS3", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS4", typeof(string));
        dtBOUploadTable.Columns.Add("CITY", typeof(string));
        dtBOUploadTable.Columns.Add("POST_CODE", typeof(string));
        dtBOUploadTable.Columns.Add("ADDRESS_COUNTRY", typeof(string));
        dtBOUploadTable.Columns.Add("PHONE1", typeof(string));
        dtBOUploadTable.Columns.Add("PHONE2", typeof(string));
        dtBOUploadTable.Columns.Add("EMAIL", typeof(string));
        dtBOUploadTable.Columns.Add("NO_OF_BANKS", typeof(string));
        dtBOUploadTable.Columns.Add("BANK_CODE", typeof(int));
        dtBOUploadTable.Columns.Add("ROUTING_NO", typeof(string));

        //dtReportTable.Columns.Add("SIGN", System.Type.GetType("System.Byte[]"));

        return dtBOUploadTable;


    }
    public DataTable getdtWarrantInfo()
    {
        DataTable dtWarrantInfoTable = new DataTable();

        dtWarrantInfoTable.Columns.Add("SI", typeof(string));
        dtWarrantInfoTable.Columns.Add("ID", typeof(string));
        dtWarrantInfoTable.Columns.Add("FUND_NAME", typeof(string));
        dtWarrantInfoTable.Columns.Add("WAR_NO", typeof(string));
        dtWarrantInfoTable.Columns.Add("NO_OF_UNIT", typeof(string));
        dtWarrantInfoTable.Columns.Add("GROSS_DIVIDEND", typeof(string));
        dtWarrantInfoTable.Columns.Add("TAX_AMOUNT", typeof(string));
        dtWarrantInfoTable.Columns.Add("NET_DIVIDEND", typeof(string));
        dtWarrantInfoTable.Columns.Add("WAR_Status", typeof(string));
        dtWarrantInfoTable.Columns.Add("WAR_RECEIVED_BY", typeof(string));
        dtWarrantInfoTable.Columns.Add("WAR_DELEVERY_DATE", typeof(string));

        return dtWarrantInfoTable;
    }
    public DataTable getdtRecordDateData()
    {
        DataTable dtWarrantInfoTable = new DataTable();

        dtWarrantInfoTable.Columns.Add("SI", typeof(string));
        dtWarrantInfoTable.Columns.Add("ID", typeof(string));
        dtWarrantInfoTable.Columns.Add("FUND_NAME", typeof(string));
        dtWarrantInfoTable.Columns.Add("NO_OF_UNIT", typeof(string));
        dtWarrantInfoTable.Columns.Add("IS_BEFTN", typeof(string));
        dtWarrantInfoTable.Columns.Add("UPDATED_BY", typeof(string));
        dtWarrantInfoTable.Columns.Add("UPDATED_DATE", typeof(string));

        return dtWarrantInfoTable;
    }

    public DataTable getMarchantBankBOInfo()
    {
        DataTable dtMarchantBankBo = new DataTable();
        dtMarchantBankBo.Columns.Add("ID", typeof(string));
        dtMarchantBankBo.Columns.Add("SI", typeof(string));
        dtMarchantBankBo.Columns.Add("NAME", typeof(string));
        dtMarchantBankBo.Columns.Add("BO", typeof(string));
        dtMarchantBankBo.Columns.Add("NO_OF_SHARES", typeof(string));

        return dtMarchantBankBo;
    }

    public DataTable getdtBankPaymentInfo()
    {
        DataTable dtBankPaymentInfoTable = new DataTable();

        dtBankPaymentInfoTable.Columns.Add("SI", typeof(string));
        dtBankPaymentInfoTable.Columns.Add("PaymentDate", typeof(string));
        dtBankPaymentInfoTable.Columns.Add("WarNo", typeof(string));
        dtBankPaymentInfoTable.Columns.Add("Narration", typeof(string));
        dtBankPaymentInfoTable.Columns.Add("PaymentAmount", typeof(string));
        
        return dtBankPaymentInfoTable;
    }

    public string GetSuspenBO(int fundCode)
    {
        string suspenseBo = "";
        DataTable dtSuspenBO = commonGatewayObj.Select("SELECT SUS_BO FROM SUSPEN_BO WHERE FUND_CODE=" + fundCode);
        if (dtSuspenBO.Rows.Count > 0)
        {
            suspenseBo = dtSuspenBO.Rows[0]["SUS_BO"].Equals(DBNull.Value) ? "" : dtSuspenBO.Rows[0]["SUS_BO"].ToString();
        }
        return suspenseBo;
    }
    public DataTable dtOmnibusCompanyList(string fundTable, int fundCode, string ficalYear, string recordDate)
    {
        DataTable dtOmnibusCompanyList = commonGatewayObj.Select("SELECT NAME1 , BO FROM " + fundTable + "_OMNIBUS_DIVIDEND WHERE FUND_CODE=" + fundCode + "AND FY='" + ficalYear + "'AND RECORD_DATE='" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "'ORDER BY NAME1");
        DataTable dtOmnibusCompanyListDropDown = new DataTable();
        dtOmnibusCompanyListDropDown.Columns.Add("BO", typeof(string));
        dtOmnibusCompanyListDropDown.Columns.Add("NAME", typeof(string));

        DataRow drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();

        drOmnibusCompanyListDropDown["NAME"] = "--Select Omnibus Company--- ";
        drOmnibusCompanyListDropDown["BO"] = "0";
        dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        for (int loop = 0; loop < dtOmnibusCompanyList.Rows.Count; loop++)
        {
             drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();
             if (dtOmnibusCompanyList.Rows[loop]["BO"].ToString() == "1201530000004033" || dtOmnibusCompanyList.Rows[loop]["BO"].ToString() == "1202720007596585" || dtOmnibusCompanyList.Rows[loop]["BO"].ToString() == "1301860017154508"||dtOmnibusCompanyList.Rows[loop]["BO"].ToString()=="1204330019657380")
            {
                string address = getBoAddress(fundTable, dtOmnibusCompanyList.Rows[loop]["BO"].ToString());
                drOmnibusCompanyListDropDown["NAME"] = dtOmnibusCompanyList.Rows[loop]["NAME1"].ToString() + " " + address;
                drOmnibusCompanyListDropDown["BO"] = dtOmnibusCompanyList.Rows[loop]["BO"].ToString();
            }
            else
            {
                drOmnibusCompanyListDropDown["NAME"] = dtOmnibusCompanyList.Rows[loop]["NAME1"].ToString();
                drOmnibusCompanyListDropDown["BO"] = dtOmnibusCompanyList.Rows[loop]["BO"].ToString();
            }
           
            
            dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        }
        return dtOmnibusCompanyListDropDown;

    }
    public DataTable dtOmnibusCompanyList(int fundCode, string ficalYear)
    {
        DataTable dtOmnibusCompanyList = commonGatewayObj.Select("SELECT NAME1 , BO FROM OMNI_ADD_09_10 WHERE FUND_CODE=" + fundCode + "AND FY='" + ficalYear + "' ORDER BY NAME1");
        DataTable dtOmnibusCompanyListDropDown = new DataTable();
        dtOmnibusCompanyListDropDown.Columns.Add("BO", typeof(string));
        dtOmnibusCompanyListDropDown.Columns.Add("NAME", typeof(string));

        DataRow drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();

        drOmnibusCompanyListDropDown["NAME"] = "--Select Omnibus Company--- ";
        drOmnibusCompanyListDropDown["BO"] = "0";
        dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        for (int loop = 0; loop < dtOmnibusCompanyList.Rows.Count; loop++)
        {
            drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();
            if (dtOmnibusCompanyList.Rows[loop]["BO"].ToString() == "1201530000004033" || dtOmnibusCompanyList.Rows[loop]["BO"].ToString() == "1202720007596585" || dtOmnibusCompanyList.Rows[loop]["BO"].ToString() == "1301860017154508" || dtOmnibusCompanyList.Rows[loop]["BO"].ToString() == "1204330019657380")
            {
                string address = getBoAddress(dtOmnibusCompanyList.Rows[loop]["BO"].ToString());
                drOmnibusCompanyListDropDown["NAME"] = dtOmnibusCompanyList.Rows[loop]["NAME1"].ToString() + " " + address;
                drOmnibusCompanyListDropDown["BO"] = dtOmnibusCompanyList.Rows[loop]["BO"].ToString();
            }
            else
            {
                drOmnibusCompanyListDropDown["NAME"] = dtOmnibusCompanyList.Rows[loop]["NAME1"].ToString();
                drOmnibusCompanyListDropDown["BO"] = dtOmnibusCompanyList.Rows[loop]["BO"].ToString();
            }


            dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        }
        return dtOmnibusCompanyListDropDown;

    }
    public DataTable dtListOfOmnibusCompany(int fundCode, string fiscalYear, int diviNo, string filter)
    {
        DataTable dtOmnibusCompanyList = commonGatewayObj.Select("SELECT     BO, NAME1 AS NAME, CITY, VALID, IS_PROCESS FROM         OMNIBUS_DIVIDEND WHERE "+filter+ " AND (FUND_CODE = " + fundCode + ")AND (DIVI_NO = " + diviNo + ") AND  (FY = '" + fiscalYear + "') ORDER BY NAME");
        DataTable dtOmnibusCompanyListDropDown = new DataTable();
        dtOmnibusCompanyListDropDown.Columns.Add("BO", typeof(string));
        dtOmnibusCompanyListDropDown.Columns.Add("NAME", typeof(string));

        DataRow drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();

        drOmnibusCompanyListDropDown["NAME"] = "--Select Omnibus Company--- ";
        drOmnibusCompanyListDropDown["BO"] = "0";
        dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        for (int loop = 0; loop < dtOmnibusCompanyList.Rows.Count; loop++)
        {
            drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();
            string city = dtOmnibusCompanyList.Rows[loop]["CITY"].ToString();
            drOmnibusCompanyListDropDown["NAME"] = dtOmnibusCompanyList.Rows[loop]["NAME"].ToString() + " " + city;
            drOmnibusCompanyListDropDown["BO"] = dtOmnibusCompanyList.Rows[loop]["BO"].ToString();
            
            dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        }
        return dtOmnibusCompanyListDropDown;
    }
    public DataTable dtListOfOmnibusCompanyEdit(int fundCode, string fiscalYear, int diviNo, string filter)
    {
        DataTable dtOmnibusCompanyList = commonGatewayObj.Select("SELECT DISTINCT DIVI_OMNIBUS_IDLIST.BO, OMNIBUS_DIVIDEND.NAME1 AS NAME, OMNIBUS_DIVIDEND.CITY     FROM         DIVI_OMNIBUS_IDLIST INNER JOIN        OMNIBUS_DIVIDEND ON DIVI_OMNIBUS_IDLIST.FUND_CODE = OMNIBUS_DIVIDEND.FUND_CODE AND      DIVI_OMNIBUS_IDLIST.DIVI_NO = OMNIBUS_DIVIDEND.DIVI_NO AND DIVI_OMNIBUS_IDLIST.FY = OMNIBUS_DIVIDEND.FY AND   DIVI_OMNIBUS_IDLIST.BO = OMNIBUS_DIVIDEND.BO WHERE " + filter + " AND (DIVI_OMNIBUS_IDLIST.FUND_CODE = " + fundCode + ")AND (DIVI_OMNIBUS_IDLIST.DIVI_NO = " + diviNo + ") AND  (DIVI_OMNIBUS_IDLIST.FY = '" + fiscalYear + "') ORDER BY NAME");
        DataTable dtOmnibusCompanyListDropDown = new DataTable();
        dtOmnibusCompanyListDropDown.Columns.Add("BO", typeof(string));
        dtOmnibusCompanyListDropDown.Columns.Add("NAME", typeof(string));

        DataRow drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();

        drOmnibusCompanyListDropDown["NAME"] = "--Select Omnibus Company--- ";
        drOmnibusCompanyListDropDown["BO"] = "0";
        dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        for (int loop = 0; loop < dtOmnibusCompanyList.Rows.Count; loop++)
        {
            drOmnibusCompanyListDropDown = dtOmnibusCompanyListDropDown.NewRow();
            string city = dtOmnibusCompanyList.Rows[loop]["CITY"].ToString();
            drOmnibusCompanyListDropDown["NAME"] = dtOmnibusCompanyList.Rows[loop]["NAME"].ToString() + " " + city;
            drOmnibusCompanyListDropDown["BO"] = dtOmnibusCompanyList.Rows[loop]["BO"].ToString();

            dtOmnibusCompanyListDropDown.Rows.Add(drOmnibusCompanyListDropDown);
        }
        return dtOmnibusCompanyListDropDown;

    }
    public DataTable dtListOfMarchantBank(int fundCode, string fiscalYear, int diviNo, string filter)
    {
        DataTable dtMarchantBankList = commonGatewayObj.Select("SELECT     DP_ID, DP_NAME || ' (' || DP_ID || ')' AS NAME, IS_PROCESS, VALID          FROM         MARCHANTBANK_DIVIDEND WHERE " + filter + " AND (FUND_CODE = " + fundCode + ")AND (DIVI_NO = " + diviNo + ") AND  (FY = '" + fiscalYear + "') ORDER BY NAME");
        DataTable dtMarchantBankListDropDown = new DataTable();
        dtMarchantBankListDropDown.Columns.Add("DP_ID", typeof(string));
        dtMarchantBankListDropDown.Columns.Add("NAME", typeof(string));

        DataRow drMarchantBankListDropDown = dtMarchantBankListDropDown.NewRow();

        drMarchantBankListDropDown["NAME"] = "--Select Omnibus Company--- ";
        drMarchantBankListDropDown["DP_ID"] = "0";
        dtMarchantBankListDropDown.Rows.Add(drMarchantBankListDropDown);
        for (int loop = 0; loop < dtMarchantBankList.Rows.Count; loop++)
        {
            drMarchantBankListDropDown = dtMarchantBankListDropDown.NewRow();
            drMarchantBankListDropDown["NAME"] = dtMarchantBankList.Rows[loop]["NAME"].ToString();
            drMarchantBankListDropDown["DP_ID"] = dtMarchantBankList.Rows[loop]["DP_ID"].ToString();

            dtMarchantBankListDropDown.Rows.Add(drMarchantBankListDropDown);
        }
        return dtMarchantBankListDropDown;
    }
    public DataTable dtListOfMarchantBankForReport(int fundCode,  int diviNo)
    {
        DataTable dtMarchantBankList = commonGatewayObj.Select("SELECT     MARCHANTBANK.DP_ID, MARCHANTBANK.DP_NAME || MARCHANTBANK.DP_ID AS NAME FROM         MARCHANTBANK INNER JOIN  (SELECT DISTINCT DP_ID   FROM          MARCHANTBANK_DIVIDEND_BO_LIST   WHERE      (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ") AND (NET_DIVIDEND IS NOT NULL)) DERIVEDTBL_1 ON MARCHANTBANK.DP_ID = DERIVEDTBL_1.DP_ID ORDER BY NAME");
        DataTable dtMarchantBankListDropDown = new DataTable();
        dtMarchantBankListDropDown.Columns.Add("DP_ID", typeof(string));
        dtMarchantBankListDropDown.Columns.Add("NAME", typeof(string));

        DataRow drMarchantBankListDropDown = dtMarchantBankListDropDown.NewRow();

        drMarchantBankListDropDown["NAME"] = "--Select Omnibus Company--- ";
        drMarchantBankListDropDown["DP_ID"] = "0";
        dtMarchantBankListDropDown.Rows.Add(drMarchantBankListDropDown);
        for (int loop = 0; loop < dtMarchantBankList.Rows.Count; loop++)
        {
            drMarchantBankListDropDown = dtMarchantBankListDropDown.NewRow();
            drMarchantBankListDropDown["NAME"] = dtMarchantBankList.Rows[loop]["NAME"].ToString();
            drMarchantBankListDropDown["DP_ID"] = dtMarchantBankList.Rows[loop]["DP_ID"].ToString();

            dtMarchantBankListDropDown.Rows.Add(drMarchantBankListDropDown);
        }
        return dtMarchantBankListDropDown;
    }
    public DataTable dtListOfOnlineReturnBank(int fundCode, string fiscalYear, int diviNo, string filter)
    {
        DataTable dtOnlineReturnBankList = commonGatewayObj.Select("SELECT DISTINCT UNIT.BANK_NAME.BANK_NAME, DIVIDEND.BANK_ID      FROM         UNIT.BANK_NAME INNER JOIN       DIVIDEND ON UNIT.BANK_NAME.BANK_CODE = DIVIDEND.BANK_ID WHERE " + filter + " AND (DIVIDEND.FUND_CODE = " + fundCode + ")AND (DIVIDEND.DIVI_NO = " + diviNo + ") AND  (DIVIDEND.FY = '" + fiscalYear + "') ORDER BY 1");
        DataTable dtOnlineReturnBankListDropDown = new DataTable();
        dtOnlineReturnBankListDropDown.Columns.Add("Bank_ID", typeof(string));
        dtOnlineReturnBankListDropDown.Columns.Add("Bank_Name", typeof(string));

        DataRow drOnlineReturnBankListDropDown = dtOnlineReturnBankListDropDown.NewRow();

        drOnlineReturnBankListDropDown["Bank_Name"] = "--Select Online Return Bank--- ";
        drOnlineReturnBankListDropDown["Bank_ID"] = "0";
        dtOnlineReturnBankListDropDown.Rows.Add(drOnlineReturnBankListDropDown);
        for (int loop = 0; loop < dtOnlineReturnBankList.Rows.Count; loop++)
        {
            drOnlineReturnBankListDropDown = dtOnlineReturnBankListDropDown.NewRow();
            drOnlineReturnBankListDropDown["Bank_Name"] = dtOnlineReturnBankList.Rows[loop]["Bank_Name"].ToString();
            drOnlineReturnBankListDropDown["Bank_ID"] = dtOnlineReturnBankList.Rows[loop]["Bank_ID"].ToString();

            dtOnlineReturnBankListDropDown.Rows.Add(drOnlineReturnBankListDropDown);
        }
        return dtOnlineReturnBankListDropDown;

    }
    public string getIDAccountName(string idAccNo, string omnibusBO)
    {
        DataTable dtIDAccountName = commonGatewayObj.Select("SELECT NAME FROM DIVI_OMNIBUS_IDLIST WHERE BO='" + omnibusBO + "' AND ID_AC_NO='" + idAccNo + "' ORDER BY RECORD_ID DESC ");
        string IDAccountName="";
        if (dtIDAccountName.Rows.Count > 0)
        {
            IDAccountName = dtIDAccountName.Rows[0]["NAME"].Equals(DBNull.Value) ? "" : dtIDAccountName.Rows[0]["NAME"].ToString();
            return IDAccountName;
        }
        else
            return IDAccountName;
    }
    public bool IsReEntryIDAccName(int divi_no, int fundCode, string fy, string recordDate, string OmnibusBO, string idAccNo)
    {
        DataTable dtIsEntryIDAccName = commonGatewayObj.Select("SELECT NAME FROM DIVI_OMNIBUS_IDLIST WHERE DIVI_NO=" + divi_no + " AND BO='" + OmnibusBO + "' AND ID_AC_NO='" + idAccNo + "' AND FUND_CODE=" + fundCode + "AND FY='" + fy + "'AND RECORD_DATE='" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "'");
        if (dtIsEntryIDAccName.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public DataTable GetdtIdAccList()
    {
        DataTable dtIDAccList = new DataTable();
        dtIDAccList.Columns.Add("RECORD_ID", typeof(long));
        dtIDAccList.Columns.Add("ID", typeof(long));
        dtIDAccList.Columns.Add("DIVI_NO", typeof(string));
        dtIDAccList.Columns.Add("FUND_CODE", typeof(string));
        dtIDAccList.Columns.Add("FY", typeof(string));
        dtIDAccList.Columns.Add("RECORD_DATE", typeof(string));
        dtIDAccList.Columns.Add("BO", typeof(string));
        dtIDAccList.Columns.Add("ID_AC_NO_PRE", typeof(string));
        dtIDAccList.Columns.Add("ID_AC_NO", typeof(string));
        dtIDAccList.Columns.Add("NAME", typeof(string));
        dtIDAccList.Columns.Add("ID_TYPE", typeof(string));
        dtIDAccList.Columns.Add("BALANCE", typeof(string));
        dtIDAccList.Columns.Add("DIVIDEND", typeof(string));
        dtIDAccList.Columns.Add("DIDUCT", typeof(string));
        dtIDAccList.Columns.Add("FINAL_DIVIDEND", typeof(string));
        dtIDAccList.Columns.Add("ENTRY_BY", typeof(string));
        dtIDAccList.Columns.Add("ENTRY_DATE", typeof(string));
        return dtIDAccList;

    }
    public DataTable getDtIDAccountInfo(int divi_no, int fundCode, string fy, string omnibusBO, string recordDate, string idAccNo)
    {
        DataTable dtIDAccountName = commonGatewayObj.Select("SELECT * FROM OMNIBUS_IDLIST WHERE DIVI_NO="+divi_no+" AND FUND_CODE="+fundCode+" AND FY='"+fy+"' AND BO='"+omnibusBO+"' AND RECORD_DATE='"+ Convert.ToDateTime (recordDate).ToString("dd-MMM-yyyy")+"' AND ID_AC_NO='"+idAccNo+"'");
        return dtIDAccountName;
      
    }
    public DataTable getDtOmnibusIDAccountInfo(string fy, string recordDate, int fundCode, string omnibusBO, string idAccNo)
    {
        DataTable dtOmnibusIdAccountInfo = commonGatewayObj.Select("SELECT * FROM DIVI_OMNIBUS_IDLIST WHERE  FY='" + fy + "' AND RECORD_DATE='" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "' AND FUND_CODE=" + fundCode + " AND BO='" + omnibusBO + "' AND ID_AC_NO='" + idAccNo + "'");
        return dtOmnibusIdAccountInfo;
    }
    public DataTable getDtOmnibusIDAccountInfo(string omnibusBO, string idAccNo)
    {
        DataTable dtOmnibusIdAccountInfo = commonGatewayObj.Select("SELECT * FROM DIVI_OMNIBUS_IDLIST WHERE   BO='" + omnibusBO + "' AND ID_AC_NO='" + idAccNo + "' ORDER BY RECORD_ID DESC");
        return dtOmnibusIdAccountInfo;
    }
    public DataTable getDividendInfo(string fy, int fundCode, int warrantNo, string fundTable)
    {
        DataTable dtWarrantInfo = commonGatewayObj.Select("SELECT * FROM " + fundTable + "_DIVIDEND WHERE FY='" + fy + "' AND FUND_CODE=" + fundCode + " AND WAR_NO=" + warrantNo);
        return dtWarrantInfo;

    }
    public DataTable getWarrantInfo(string fy, int fundCode, string filter, string fundTable)
    {
        DataTable dtWarrantInfo = commonGatewayObj.Select("SELECT * FROM " + fundTable + "_DIVIDEND WHERE FY='" + fy + "' AND FUND_CODE=" + fundCode + filter);
        return dtWarrantInfo;

    }
    public long getMaxIDForIDAcc()
    {
        DataTable dtMaxID = commonGatewayObj.Select("SELECT MAX(NVL(ID,0)) AS ID FROM OMNIBUS_IDLIST");
        long MaxID = dtMaxID.Rows[0]["ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxID.Rows[0]["ID"].ToString());
        return MaxID;
    }
    public long getMaxRecordIDForIDAcc()
    {
        DataTable dtMaxID = commonGatewayObj.Select("SELECT MAX(NVL(RECORD_ID,0)) AS RECORD_ID FROM DIVI_OMNIBUS_IDLIST");
        long MaxID = dtMaxID.Rows[0]["RECORD_ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxID.Rows[0]["RECORD_ID"].ToString());
        return MaxID;
    }
    public DataTable dtOmnibusIDInfoForReport(int fundCode, string fy, string OmnibusBO, string recordDate)
    {
        DataTable dtOmnibusIDInfoForReport = commonGatewayObj.Select("SELECT * FROM OMNIBUS_IDLIST WHERE FUND_CODE=" + fundCode + " AND FY='" + fy + "' AND BO='" + OmnibusBO + "' AND VALID IS NULL ORDER BY TO_NUMBER(ID_AC_NO)");
         return dtOmnibusIDInfoForReport;
    }
    public DataTable dtOmnibusIDInfoForReport(int fundCode, string OmnibusBO, int diviNo)
    {
        DataTable dtOmnibusIDInfoForReport = commonGatewayObj.Select("SELECT * FROM DIVI_OMNIBUS_IDLIST WHERE FUND_CODE=" + fundCode + " AND DIVI_NO=" + diviNo + " AND BO='" + OmnibusBO + "' AND VALID IS NULL ORDER BY TO_NUMBER(ID_AC_NO)");
        return dtOmnibusIDInfoForReport;
    }
    public DataTable dtMarchantBankBoListForReport(int fundCode, string marchantBankDpID, int diviNo)
    {
        DataTable dtMarchantBankBoListForReport = commonGatewayObj.Select("SELECT * FROM MARCHANTBANK_DIVIDEND_BO_LIST WHERE FUND_CODE=" + fundCode + " AND DIVI_NO=" + diviNo + " AND DP_ID='" + marchantBankDpID + "' AND (VALID IS NULL) AND (DIDUCT IS NOT NULL) ORDER BY BO ");
        return dtMarchantBankBoListForReport;
    }
    public string getOmnibusCompanyNameFromCDBL(string fundTable, string filter)
    {
        DataTable dtOmnibusCompanyName = commonGatewayObj.Select("SELECT NAME1 FROM " + fundTable + "_CDBL WHERE " + filter);
        string name = dtOmnibusCompanyName.Rows[0]["NAME1"].Equals(DBNull.Value) ? "" : dtOmnibusCompanyName.Rows[0]["NAME1"].ToString().ToUpper();
        return name;

    }
    public string getOmnibusCompanyNameFromCDBL(string filter)
    {
        DataTable dtOmnibusCompanyName = commonGatewayObj.Select("SELECT NAME1 FROM OMNIBUS_DIVIDEND WHERE " + filter);
        string name = dtOmnibusCompanyName.Rows[0]["NAME1"].Equals(DBNull.Value) ? "" : dtOmnibusCompanyName.Rows[0]["NAME1"].ToString().ToUpper();
        return name;
    }
    public string getMarchantBankName(string filter)
    {
        DataTable dtMarchantBankName = commonGatewayObj.Select("SELECT     DP_NAME FROM         MARCHANTBANK WHERE " + filter);
        string name = dtMarchantBankName.Rows[0]["DP_NAME"].Equals(DBNull.Value) ? "" : dtMarchantBankName.Rows[0]["DP_NAME"].ToString().ToUpper();
        return name;

    }
    public string getBoAddress(string fundTable, string BO)
    {
        DataTable dtAddress = commonGatewayObj.Select("SELECT CITY FROM " + fundTable + "_CDBL WHERE BO='" + BO + "'");
        string address = dtAddress.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtAddress.Rows[0]["CITY"].ToString();
        return address;
    }
    public string getBoAddress(string BO)
    {
        DataTable dtAddress = commonGatewayObj.Select("SELECT CITY FROM CDBL_BO_ADDRESS WHERE BO='" + BO + "'");
        string address = dtAddress.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtAddress.Rows[0]["CITY"].ToString();
        return address;
    }

    public DataTable GetdtOmnibusDividendData(string fundTable, int fundCode, string FY, string recordDate)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT C.BO, C.NAME1, C.NAME2, C.FA_HUS_NAME, C.MO_NAME, C.BO_TYPE, C.BO_CATAGORY, C.ADDRESS1, C.ADDRESS2, C.ADDRESS3, C.CITY,");
        sb.Append(" C.POST_CODE, C.COUNTRY, C.PHONE, C.RESIDENCY, C.BALANCE, C.DP_HST_CCY_CDE, C.ALLOT_NO, C.FOLIO_NO, C.VALID, C.REMARKS,");
        sb.Append(" D.DIVI_NO, D.FUND_CODE, D.FY, D.RECORD_DATE, D.BO AS D_BO, D.NAME1 AS D_NAME1, D.BALANCE AS D_BALANCE, I.BO AS I_BO, I.I_BALANCE,");
        sb.Append(" I.I_DIVIDEND, I.I_DIDUCT, I.I_FINAL_DIVIDEND FROM " + fundTable + "_CDBL C INNER JOIN ");
        sb.Append(" (SELECT DIVI_NO, FUND_CODE, FY, RECORD_DATE, BO, NAME1, BO_CATAGORY, BALANCE, DIVIDEND, DIDUCT, FINAL_DIVIDEND, WAR_NO,");
        sb.Append(" IS_PROCESS, PROCESS_DATE, VALID, REMARKS, ENTRY_BY, ENTRY_DATE FROM " + fundTable + "_OMNIBUS_DIVIDEND ");
        sb.Append(" WHERE (IS_PROCESS IS NULL) AND (VALID IS NULL) AND (FUND_CODE =" + fundCode + ") AND (FY = '" + FY + "') AND (RECORD_DATE = '" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "') ) D ON");
        sb.Append(" C.BO = D.BO INNER JOIN ");
        sb.Append(" (SELECT  BO, SUM(BALANCE) AS I_BALANCE, SUM(DIVIDEND) AS I_DIVIDEND, SUM(DIDUCT) AS I_DIDUCT, SUM(FINAL_DIVIDEND) AS I_FINAL_DIVIDEND ");
        sb.Append(" FROM OMNIBUS_IDLIST  WHERE (FUND_CODE = " + fundCode + ") AND (FY = '" + FY + "') AND (RECORD_DATE = '" + Convert.ToDateTime(recordDate).ToString("dd-MMM-yyyy") + "') GROUP BY BO) I ON D.BO = I.BO");
        sb.Append(" AND D.BALANCE = I.I_BALANCE AND C.BALANCE=I.I_BALANCE");

        DataTable dtOmnibusDividendData = commonGatewayObj.Select(sb.ToString());
        return dtOmnibusDividendData;
        
    }

    public DataTable GetdtOmnibusDividendData(int fundCode, int diviNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     CDBL_BO_HOLDER.FUND_CODE, CDBL_BO_HOLDER.DIVI_NO, CDBL_BO_HOLDER.FY, OMNIBUS_DIVIDEND.RECORD_DATE, ");
        sb.Append(" CDBL_BO_HOLDER.BO, CDBL_BO_ADDRESS.NAME1, CDBL_BO_ADDRESS.NAME2, CDBL_BO_HOLDER.BO_TYPE, ");
        sb.Append(" CDBL_BO_HOLDER.BO_CATAGORY, CDBL_BO_ADDRESS.ADDRESS1, CDBL_BO_ADDRESS.ADDRESS2, CDBL_BO_ADDRESS.ADDRESS3, ");
        sb.Append(" CDBL_BO_ADDRESS.ADDRESS4, CDBL_BO_ADDRESS.CITY, CDBL_BO_ADDRESS.POST_CODE, CDBL_BO_ADDRESS.COUNTRY, ");
        sb.Append(" CDBL_BO_HOLDER.RESIDENCY, CDBL_BO_ADDRESS.EMAIL, CDBL_BO_ADDRESS.PHONE1, CDBL_BO_ADDRESS.PHONE2, CDBL_BO_BANK.BANK, ");
        sb.Append(" CDBL_BO_BANK.BRANCH, CDBL_BO_BANK.BANK_ACC, CDBL_BO_BANK.BANK_ID, OMNIBUS_DIVIDEND.BALANCE,  ");
        sb.Append(" DIVI_OMNIBUS_IDLIST.GROSS_DIVIDEND, DIVI_OMNIBUS_IDLIST.DIDUCT, DIVI_OMNIBUS_IDLIST.NET_DIVIDEND ");
        sb.Append(" FROM         CDBL_BO_HOLDER INNER JOIN ");
        sb.Append(" CDBL_BO_BANK ON CDBL_BO_HOLDER.DIVI_NO = CDBL_BO_BANK.DIVI_NO AND ");
        sb.Append(" CDBL_BO_HOLDER.FUND_CODE = CDBL_BO_BANK.FUND_CODE AND CDBL_BO_HOLDER.BO = CDBL_BO_BANK.BO INNER JOIN ");
        sb.Append(" CDBL_BO_ADDRESS ON CDBL_BO_HOLDER.DIVI_NO = CDBL_BO_ADDRESS.DIVI_NO AND ");
        sb.Append(" CDBL_BO_HOLDER.FUND_CODE = CDBL_BO_ADDRESS.FUND_CODE AND CDBL_BO_HOLDER.BO = CDBL_BO_ADDRESS.BO INNER JOIN ");
        sb.Append(" OMNIBUS_DIVIDEND ON CDBL_BO_BANK.DIVI_NO = OMNIBUS_DIVIDEND.DIVI_NO AND ");
        sb.Append(" CDBL_BO_BANK.FUND_CODE = OMNIBUS_DIVIDEND.FUND_CODE AND CDBL_BO_BANK.BO = OMNIBUS_DIVIDEND.BO INNER JOIN ");
        sb.Append(" (SELECT     FUND_CODE, DIVI_NO, BO, SUM(BALANCE) AS BALANCE, SUM(GROSS_DIVIDEND) AS GROSS_DIVIDEND, SUM(DIDUCT) AS DIDUCT,  ");
        sb.Append(" SUM(NET_DIVIDEND) AS NET_DIVIDEND ");
        sb.Append(" FROM          DIVI_OMNIBUS_IDLIST DIVI_OMNIBUS_IDLIST_1  ");
        sb.Append(" WHERE      (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ") ");
        sb.Append(" GROUP BY BO, FUND_CODE, DIVI_NO) DIVI_OMNIBUS_IDLIST ON DIVI_OMNIBUS_IDLIST.BO = CDBL_BO_ADDRESS.BO AND  ");
        sb.Append(" OMNIBUS_DIVIDEND.BALANCE = DIVI_OMNIBUS_IDLIST.BALANCE AND CDBL_BO_ADDRESS.FUND_CODE = DIVI_OMNIBUS_IDLIST.FUND_CODE AND ");
        sb.Append(" CDBL_BO_ADDRESS.DIVI_NO = DIVI_OMNIBUS_IDLIST.DIVI_NO AND (OMNIBUS_DIVIDEND.IS_PROCESS IS NULL) AND (OMNIBUS_DIVIDEND.VALID IS NULL) ");
        sb.Append(" ORDER BY CDBL_BO_ADDRESS.NAME1 ");

        DataTable dtOmnibusDividendData = commonGatewayObj.Select(sb.ToString());
        return dtOmnibusDividendData;
    }

    public DataTable GetdtOmnibusDividendDataSingleFile(int fundCode, int diviNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     CDBL_DATA.FUND_CODE, CDBL_DATA.DIVI_NO, CDBL_DATA.FY, CDBL_DATA.RECORD_DATE, CDBL_DATA.BO, CDBL_DATA.NAME1, ");
        sb.Append(" CDBL_DATA.BO_TYPE, CDBL_DATA.BO_CATAGORY, CDBL_DATA.ADDRESS1, CDBL_DATA.ADDRESS2, CDBL_DATA.ADDRESS3, ");
        sb.Append(" CDBL_DATA.ADDRESS4, CDBL_DATA.CITY, CDBL_DATA.POST_CODE, CDBL_DATA.COUNTRY, CDBL_DATA.RESIDENCY, CDBL_DATA.PHONE1, ");
        sb.Append(" CDBL_DATA.PHONE2, CDBL_DATA.BANK, CDBL_DATA.BRANCH, CDBL_DATA.BANK_ACC, CDBL_DATA.ROUTING_NO, CDBL_DATA.BANK_ID, CDBL_DATA.IS_BEFTN, OMNIBUS_DIVIDEND.BALANCE, ");
        sb.Append(" DIVI_OMNIBUS_IDLIST.GROSS_DIVIDEND, DIVI_OMNIBUS_IDLIST.DIDUCT, DIVI_OMNIBUS_IDLIST.NET_DIVIDEND ");
        sb.Append(" FROM         (SELECT     FUND_CODE, DIVI_NO, BO, SUM(BALANCE) AS BALANCE, SUM(GROSS_DIVIDEND) AS GROSS_DIVIDEND, SUM(DIDUCT) AS DIDUCT, ");
        sb.Append(" SUM(NET_DIVIDEND) AS NET_DIVIDEND ");
        sb.Append(" FROM          DIVI_OMNIBUS_IDLIST DIVI_OMNIBUS_IDLIST_1 ");
        sb.Append(" WHERE      (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ") ");
        sb.Append(" GROUP BY BO, FUND_CODE, DIVI_NO) DIVI_OMNIBUS_IDLIST INNER JOIN ");
        sb.Append(" OMNIBUS_DIVIDEND ON DIVI_OMNIBUS_IDLIST.BALANCE = OMNIBUS_DIVIDEND.BALANCE AND ");
        sb.Append(" DIVI_OMNIBUS_IDLIST.FUND_CODE = OMNIBUS_DIVIDEND.FUND_CODE AND DIVI_OMNIBUS_IDLIST.DIVI_NO = OMNIBUS_DIVIDEND.DIVI_NO AND ");
        sb.Append(" DIVI_OMNIBUS_IDLIST.BO = OMNIBUS_DIVIDEND.BO INNER JOIN ");
        sb.Append(" CDBL_DATA ON OMNIBUS_DIVIDEND.FUND_CODE = CDBL_DATA.FUND_CODE AND OMNIBUS_DIVIDEND.DIVI_NO = CDBL_DATA.DIVI_NO AND ");
        sb.Append(" OMNIBUS_DIVIDEND.BO = CDBL_DATA.BO AND OMNIBUS_DIVIDEND.IS_PROCESS IS NULL AND OMNIBUS_DIVIDEND.VALID IS NULL ");
        sb.Append(" ORDER BY CDBL_DATA.NAME1  ");
        
        DataTable dtOmnibusDividendData = commonGatewayObj.Select(sb.ToString());
        return dtOmnibusDividendData;
    }
    public DataTable GetdtIDaccBOsDividend(int fundCode, int diviNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     DERIVEDTBL_DIVI_ID_ACC_BO_LIST.FUND_CODE, DERIVEDTBL_DIVI_ID_ACC_BO_LIST.DIVI_NO, DERIVEDTBL_DIVI_ID_ACC_BO_LIST.FY,   ");
        sb.Append(" DERIVEDTBL_DIVI_ID_ACC_BO_LIST.RECORD_DATE, DERIVEDTBL_DIVI_ID_ACC_BO_LIST.DP_ID AS BO,  ");
        sb.Append(" CUSTODIAN_DP.BANK_ACC_NAME AS NAME1, CUSTODIAN_DP.ADDRESS AS ADDRESS1, CUSTODIAN_DP.BANK_NAME AS BANK,  ");
        sb.Append(" CUSTODIAN_DP.BANK_BRANCH AS BRANCH, CUSTODIAN_DP.BANK_ACC_NO AS BANK_ACC, CUSTODIAN_DP.ROUTING_NO, ");
        sb.Append(" DERIVEDTBL_DIVI_ID_ACC_BO_LIST.BALANCE, DERIVEDTBL_DIVI_ID_ACC_BO_LIST.GROSS_DIVIDEND,  ");
        sb.Append(" DERIVEDTBL_DIVI_ID_ACC_BO_LIST.DIDUCT, DERIVEDTBL_DIVI_ID_ACC_BO_LIST.NET_DIVIDEND ");
        sb.Append(" FROM         CUSTODIAN_DP INNER JOIN ");
        sb.Append(" (SELECT     FUND_CODE, DIVI_NO, FY, RECORD_DATE, DP_ID, SUM(BALANCE) AS BALANCE, SUM(GROSS_DIVIDEND) AS GROSS_DIVIDEND,  ");
        sb.Append(" SUM(DIDUCT) AS DIDUCT, SUM(NET_DIVIDEND) AS NET_DIVIDEND ");
        sb.Append(" FROM          DIVI_ID_ACC_BO_LIST ");
        sb.Append(" WHERE      (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ") ");
        sb.Append(" GROUP BY FUND_CODE, DIVI_NO, FY, RECORD_DATE, DP_ID) DERIVEDTBL_DIVI_ID_ACC_BO_LIST ON ");
        sb.Append(" CUSTODIAN_DP.DP_ID = DERIVEDTBL_DIVI_ID_ACC_BO_LIST.DP_ID ");
        sb.Append(" ORDER BY name1  ");

        DataTable dtOmnibusDividendData = commonGatewayObj.Select(sb.ToString());
        return dtOmnibusDividendData;
    }
    public DataTable GetdtMarchantBankDividend(int fundCode, int diviNo,string fy)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     DERIVEDTBL_1.FUND_CODE, DERIVEDTBL_1.DIVI_NO, DERIVEDTBL_1.FY, DIVIDEND_PARA.RECORD_DATE, DERIVEDTBL_1.DP_ID, ");
        sb.Append(" MARCHANTBANK.DP_NAME AS NAME1, MARCHANTBANK.ADDRESS1, MARCHANTBANK.ADDRESS2, MARCHANTBANK.PHONE_NO, MARCHANTBANK.BANK, ");
        sb.Append(" MARCHANTBANK.BRANCH, MARCHANTBANK.BANK_ACC, DERIVEDTBL_1.BALANCE, DERIVEDTBL_1.GROSS_DIVIDEND, DERIVEDTBL_1.DIDUCT,  ");
        sb.Append(" DERIVEDTBL_1.NET_DIVIDEND ");
        sb.Append(" FROM         (SELECT     FUND_CODE, DIVI_NO, FY, DP_ID, SUM(BALANCE) AS BALANCE, SUM(GROSS_DIVIDEND) AS GROSS_DIVIDEND, SUM(DIDUCT) AS DIDUCT,  ");
        sb.Append(" SUM(NET_DIVIDEND) AS NET_DIVIDEND ");
        sb.Append(" FROM          MARCHANTBANK_DIVIDEND_BO_LIST ");
        sb.Append(" WHERE      (FY = '"+fy+"') AND (FUND_CODE = "+fundCode+") AND (DIVI_NO = "+diviNo+") AND (GROSS_DIVIDEND IS NOT NULL) AND (DIDUCT IS NOT NULL) AND  (NET_DIVIDEND IS NOT NULL)  ");
        sb.Append(" GROUP BY FUND_CODE, DIVI_NO, FY, DP_ID) DERIVEDTBL_1 INNER JOIN ");
        sb.Append(" MARCHANTBANK ON DERIVEDTBL_1.DP_ID = MARCHANTBANK.DP_ID INNER JOIN ");
        sb.Append(" DIVIDEND_PARA ON DERIVEDTBL_1.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DERIVEDTBL_1.DIVI_NO = DIVIDEND_PARA.DIVI_NO AND ");
        sb.Append(" DERIVEDTBL_1.FY = DIVIDEND_PARA.FY INNER JOIN ");
        sb.Append(" MARCHANTBANK_DIVIDEND ON DERIVEDTBL_1.FUND_CODE = MARCHANTBANK_DIVIDEND.FUND_CODE AND  ");
        sb.Append(" DERIVEDTBL_1.FY = MARCHANTBANK_DIVIDEND.FY AND DERIVEDTBL_1.DIVI_NO = MARCHANTBANK_DIVIDEND.DIVI_NO AND ");
        sb.Append(" DERIVEDTBL_1.DP_ID = MARCHANTBANK_DIVIDEND.DP_ID AND DERIVEDTBL_1.BALANCE = MARCHANTBANK_DIVIDEND.BALANCE ");
        sb.Append(" WHERE     (MARCHANTBANK_DIVIDEND.IS_PROCESS IS NULL) ");
        sb.Append(" ORDER BY name1  ");

        DataTable dtMachantBankBODividendData = commonGatewayObj.Select(sb.ToString());
        return dtMachantBankBODividendData;
    }

    public DataTable GetdtOnlineTransferSummary(int fundCode, int diviNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     DIVIDEND.FUND_CODE, DIVIDEND.DIVI_NO, DIVIDEND.FY, DIVIDEND.RECORD_DATE, UNIT.BANK_NAME.BANK_NAME, DIVIDEND.BANK_ID, COUNT(1) ");
        sb.Append(" AS NO_OF_WARRANT, SUM(DIVIDEND.BALANCE) AS BALANCE, SUM(DIVIDEND.GROSS_DIVIDEND) AS GROSS_DIVIDEND, SUM(DIVIDEND.DIDUCT)  ");
        sb.Append(" AS DIDUCT, SUM(DIVIDEND.NET_DIVIDEND) AS NET_DIVIDEND, 'ONLINE NET DIVIDEND SUMMARY' AS REMARKS ");
        sb.Append(" FROM         UNIT.BANK_NAME INNER JOIN ");
        sb.Append(" DIVIDEND ON UNIT.BANK_NAME.BANK_CODE = DIVIDEND.BANK_ID ");
        sb.Append(" WHERE     (DIVIDEND.FUND_CODE = "+fundCode+") AND (DIVIDEND.DIVI_NO = "+diviNo+") AND (DIVIDEND.IS_ONLINE = 'YES') ");
        sb.Append(" GROUP BY DIVIDEND.BANK_ID, DIVIDEND.FUND_CODE, DIVIDEND.DIVI_NO, DIVIDEND.FY, DIVIDEND.RECORD_DATE, UNIT.BANK_NAME.BANK_NAME ");
        DataTable dtOnlineTransferSummary = commonGatewayObj.Select(sb.ToString());
        return dtOnlineTransferSummary;
    }

    public DataTable GetdtFundWiseOmnibusCompany(int fundCode, int diviNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     DIVIDEND_PARA.FUND_CODE, DIVIDEND_PARA.DIVI_NO, DIVIDEND_PARA.FY, DIVIDEND_PARA.RECORD_DATE, CDBL_BO_ADDRESS.BO, ");
        sb.Append(" CDBL_BO_ADDRESS.NAME1, CDBL_BO_ADDRESS.CITY, CDBL_BO_HOLDER.BO_CATAGORY, CDBL_BO_HOLDER.BALANCE ");
        sb.Append(" FROM         CDBL_BO_ADDRESS INNER JOIN ");
        sb.Append(" CDBL_BO_HOLDER ON CDBL_BO_ADDRESS.BO = CDBL_BO_HOLDER.BO AND CDBL_BO_ADDRESS.DIVI_NO = CDBL_BO_HOLDER.DIVI_NO AND ");
        sb.Append(" CDBL_BO_ADDRESS.FUND_CODE = CDBL_BO_HOLDER.FUND_CODE INNER JOIN ");
        sb.Append("  DIVIDEND_PARA ON CDBL_BO_HOLDER.DIVI_NO = DIVIDEND_PARA.DIVI_NO AND  ");
        sb.Append(" CDBL_BO_HOLDER.FUND_CODE = DIVIDEND_PARA.FUND_CODE ");
        sb.Append(" WHERE     (CDBL_BO_ADDRESS.FUND_CODE = " + fundCode + ") AND (CDBL_BO_ADDRESS.DIVI_NO = "+diviNo+") AND (CDBL_BO_ADDRESS.BO IN ");
        sb.Append("  (SELECT     BO ");
        sb.Append("  FROM          DIVI_OMNIBUS_BO ");
        sb.Append(" WHERE      (VALID IS NULL))) ");
        sb.Append(" ORDER BY CDBL_BO_ADDRESS.NAME1 ");

        DataTable dtFundWiseOmnibusCompany = commonGatewayObj.Select(sb.ToString());
        return dtFundWiseOmnibusCompany;
    }
    public DataTable GetdtFundWiseOmnibusCompanySingleFile(int fundCode, int diviNo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     CDBL_DATA.FUND_CODE, CDBL_DATA.DIVI_NO, CDBL_DATA.FY, CDBL_DATA.RECORD_DATE, CDBL_DATA.BO, CDBL_DATA.NAME1, ");
        sb.Append(" CDBL_DATA.CITY, CDBL_DATA.BO_CATAGORY, CDBL_DATA.BALANCE ");
        sb.Append(" FROM         DIVI_OMNIBUS_BO INNER JOIN ");
        sb.Append(" CDBL_DATA ON DIVI_OMNIBUS_BO.BO = CDBL_DATA.BO ");
        sb.Append(" WHERE     (DIVI_OMNIBUS_BO.VALID IS NULL) AND (CDBL_DATA.FUND_CODE = " + fundCode + ") AND (CDBL_DATA.DIVI_NO = " + diviNo + ") ");
        sb.Append(" ORDER BY CDBL_DATA.NAME1  ");
        
        DataTable dtFundWiseOmnibusCompany = commonGatewayObj.Select(sb.ToString());
        return dtFundWiseOmnibusCompany;
    }
    public DataTable GetdtCdblData(int fundCode, int diviNo, string bo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     NAME1, BANK_ACC ");
        sb.Append(" FROM         CDBL_DATA  ");
        sb.Append(" WHERE     (VALID IS NULL) AND (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ") AND (BO = '" + bo + "') ");
        
        DataTable dtCdblData= commonGatewayObj.Select(sb.ToString());
        return dtCdblData;
    }
    public DataTable GetdtDividendData(int fundCode, int diviNo, int war_no)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     WAR_NO, NET_DIVIDEND, IS_PAID ");
        sb.Append(" FROM         DIVIDEND  ");
        sb.Append(" WHERE     (VALID IS NULL)  AND (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ") AND (WAR_NO = " +war_no+ ") ");

        DataTable dtDividendData = commonGatewayObj.Select(sb.ToString());
        return dtDividendData;
    }
    public DataTable GetdtDividendData(int fundCode, int diviNo, string war_no)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT     WAR_NO, NET_DIVIDEND, IS_PAID ");
        sb.Append(" FROM         DIVIDEND  ");
        sb.Append(" WHERE     (VALID IS NULL)  AND (FUND_CODE = " + fundCode + ") AND (DIVI_NO = " + diviNo + ") AND (WAR_NO IN " + war_no + ") ");
        sb.Append(" ORDER BY WAR_NO ");
        DataTable dtDividendData = commonGatewayObj.Select(sb.ToString());
        return dtDividendData;
    }

   }
  