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
using System.Data.OracleClient;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Text;

/// <summary>
/// Summary description for Pf1s1DAO
/// </summary>
public class Pf1s1DAO
{
    CommonGateway CommonGetwayObj = new CommonGateway();
    public Pf1s1DAO()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable dtGetBankCleraingReport()
    {
        DataTable dtGetBankCleraingReport = new DataTable();
        dtGetBankCleraingReport.Columns.Add("SI", typeof(int));
        dtGetBankCleraingReport.Columns.Add("APP_SI", typeof(string));
        dtGetBankCleraingReport.Columns.Add("EMP_NAME", typeof(string));
        dtGetBankCleraingReport.Columns.Add("EMP_ID", typeof(string));
        dtGetBankCleraingReport.Columns.Add("APP_DATE", typeof(string));
        dtGetBankCleraingReport.Columns.Add("FBC_NO", typeof(string));
        dtGetBankCleraingReport.Columns.Add("AMOUNT", typeof(decimal));
        dtGetBankCleraingReport.Columns.Add("UNIT_OFFERED", typeof(int));
        dtGetBankCleraingReport.Columns.Add("UNIT_ACCEPT", typeof(int));
        dtGetBankCleraingReport.Columns.Add("UNIT_FACE_VALUE", typeof(decimal));
        dtGetBankCleraingReport.Columns.Add("CHEQUE_NO", typeof(string));
        dtGetBankCleraingReport.Columns.Add("BANK_NAME", typeof(string));
        dtGetBankCleraingReport.Columns.Add("BRANCH_NAME", typeof(string));
        dtGetBankCleraingReport.Columns.Add("DESIGNATION", typeof(string));
        dtGetBankCleraingReport.Columns.Add("F_NAME", typeof(string));
        dtGetBankCleraingReport.Columns.Add("M_NAME", typeof(string));
        dtGetBankCleraingReport.Columns.Add("H_NAME", typeof(string));
        dtGetBankCleraingReport.Columns.Add("ADDRESS", typeof(string));
        dtGetBankCleraingReport.Columns.Add("BO_AC", typeof(string));
        dtGetBankCleraingReport.Columns.Add("PAY_STATUS", typeof(string));
        dtGetBankCleraingReport.Columns.Add("MOBIL_TEL_NUMBER",typeof(string));
        dtGetBankCleraingReport.Columns.Add("CHEQUE_ISSUE_DATE", typeof(string));
        dtGetBankCleraingReport.Columns.Add("NO_CQ_DD", typeof(int));


        return dtGetBankCleraingReport;
    }

    public DataTable GetFundGridTable()
    {
        DataTable dtReportTable = new DataTable();
        dtReportTable.Columns.Add("SI", typeof(int));
        dtReportTable.Columns.Add("FUND_CODE", typeof(int));
        dtReportTable.Columns.Add("FUND_NAME", typeof(string));
        dtReportTable.Columns.Add("ASSET_VALUE", typeof(string));
        return dtReportTable;
    }
    public DataTable getdtTradeCusTable()
    {
        DataTable dtTradeCusData = new DataTable();

        dtTradeCusData.Columns.Add("SI", typeof(string));
        dtTradeCusData.Columns.Add("F_CD", typeof(string));
        dtTradeCusData.Columns.Add("BR_CD", typeof(string));
        dtTradeCusData.Columns.Add("SP_DATE", typeof(string));
        dtTradeCusData.Columns.Add("BK_REF", typeof(string));
        dtTradeCusData.Columns.Add("HOWLA_NO", typeof(string));
        dtTradeCusData.Columns.Add("HOWLA_TP", typeof(string));
        dtTradeCusData.Columns.Add("IN_OUT", typeof(string));
        dtTradeCusData.Columns.Add("SETTLE_DT", typeof(string));
        dtTradeCusData.Columns.Add("COMP_CD", typeof(string));
        dtTradeCusData.Columns.Add("SP_QTY", typeof(string));
        dtTradeCusData.Columns.Add("SP_RATE", typeof(string));
        dtTradeCusData.Columns.Add("CL_DATE", typeof(string));
        dtTradeCusData.Columns.Add("BK_CD", typeof(string));
        dtTradeCusData.Columns.Add("HOWLA_CHG", typeof(string));
        dtTradeCusData.Columns.Add("LAGA_CHG", typeof(string));
        dtTradeCusData.Columns.Add("VOUCH_NO", typeof(string));
        dtTradeCusData.Columns.Add("VOUCH_DT", typeof(string));
        dtTradeCusData.Columns.Add("VOUCH_REF", typeof(string));
        dtTradeCusData.Columns.Add("CH_AC_NO", typeof(string));
        dtTradeCusData.Columns.Add("BN_NAME", typeof(string));
        dtTradeCusData.Columns.Add("CHQ_DT", typeof(string));
        dtTradeCusData.Columns.Add("CHQ_NO", typeof(string));
        dtTradeCusData.Columns.Add("OP_NAME", typeof(string));
        dtTradeCusData.Columns.Add("N_P", typeof(string));
        dtTradeCusData.Columns.Add("QTY_ALLOC", typeof(string));
        dtTradeCusData.Columns.Add("ISIN_CD", typeof(string));
        dtTradeCusData.Columns.Add("FORGN_FLG", typeof(string));
        dtTradeCusData.Columns.Add("SPOT_ID", typeof(string));
        dtTradeCusData.Columns.Add("INSTR_GRP", typeof(string));
        dtTradeCusData.Columns.Add("MARKT_TP", typeof(string));
        dtTradeCusData.Columns.Add("CUSTOMER", typeof(string));
        dtTradeCusData.Columns.Add("BOID", typeof(string));
        return dtTradeCusData;
    }
    public long GetApplicantIDByFormSerial(string FormSerialNo)
    {
        StringBuilder sbApplicantID = new StringBuilder();
        sbApplicantID.Append("SELECT APPLICANT.ID FROM APPLICANT INNER JOIN UNIT_INFO ON APPLICANT.ID = UNIT_INFO.APPLICANT_ID");
        sbApplicantID.Append(" WHERE (UNIT_INFO.FORM_SERIAL = '" + FormSerialNo + "')");
        DataTable dtApplicntID = CommonGetwayObj.Select(sbApplicantID.ToString());
        long ApplicantID = Convert.ToInt64(dtApplicntID.Rows[0]["ID"]);
        return ApplicantID;
    }
    public bool IsFormSerial(string FormSerial)
    {
        DataTable dtFormSerial = CommonGetwayObj.Select("SELECT * FROM UNIT_INFO WHERE FORM_SERIAL='" + FormSerial + "'");
        if (dtFormSerial.Rows.Count > 0)
            return true;
        else
            return false;
    }
     public bool IsCompCode(string compCode)
    {
        DataTable dtCompCode = CommonGetwayObj.Select("SELECT * FROM ANALYSIS_MST1 WHERE COMP_CD = '" + compCode + "'");
        if (dtCompCode.Rows.Count > 0)
            return true;
        else
            return false;
    }
     public bool IsDuplicateBonusRightEntry(int fund_code, int comp_code, string howla_date, string tran_tp, int no_of_shares)
     {
         DataTable dtDuplicateEntry = CommonGetwayObj.Select("SELECT * FROM fund_trans_hb WHERE F_CD = " + fund_code + " AND COMP_CD = " + comp_code + " AND VCH_DT = '" + howla_date + "' AND NO_SHARE = " +no_of_shares);
         if (dtDuplicateEntry.Rows.Count > 0)
             return true;
         else
             return false;
     }
    public bool IsDuplicateNonListedSecurities(int fund_code, decimal investmentAmount, string investmentDate)
    {
       
        DataTable dtDuplicateEntry = CommonGetwayObj.Select("SELECT * FROM NON_LISTED_SECURITIES WHERE F_CD = " + fund_code + " AND INV_DATE = '" + investmentDate+"' ");
        if (dtDuplicateEntry.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
           
     }
   
    public DataTable GetEmployeeInfo(string EmpID)
    {
       DataTable dtEmployeeInfo=new DataTable();
        return dtEmployeeInfo =CommonGetwayObj.Select("SELECT * FROM ICB_EMP WHERE EMP_ID='"+ EmpID + "'");
    }
    public long Sequence(string seqName)
    {
        DataTable dtNextVal = new DataTable();
        dtNextVal = CommonGetwayObj.Select("SELECT " + seqName + ".nextval FROM dual");
        long NextValue = Convert.ToInt32(dtNextVal.Rows[0]["NEXTVAL"]);
        return NextValue;
    }

    public DataTable GetSignatoryInfo(string EmpID)
    {
        DataTable dtSignatoryInfo = new DataTable();
        return dtSignatoryInfo = CommonGetwayObj.Select("SELECT NAME, DESIGNATION, DESIG_SHORT FROM NAV_LETTER.SIGNATORY WHERE ID ='" + EmpID + "'");
    }
    public DataTable GetPaperInfo(int ID)
    {
        DataTable dtPaperInfo = new DataTable();
        return dtPaperInfo = CommonGetwayObj.Select("SELECT NEWS_PAPER_NAME FROM NAV_LETTER.NEWS_PAPER WHERE ID =" + ID);
    }
    public long getMaxIDForNonListedSecurities()
    {
        DataTable dtMaxID = CommonGetwayObj.Select("SELECT MAX(NVL(ID,0)) AS ID FROM NON_LISTED_SECURITIES");
        long MaxID = dtMaxID.Rows[0]["ID"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtMaxID.Rows[0]["ID"].ToString());
        return MaxID;
    }
    public int GetFundOrCompCode(string tableName, string ColumnName, string filter)
    {
        int fundOrCompCode = 0;
        DataTable dtFundOrCompCode = CommonGetwayObj.Select("SELECT " + ColumnName + " AS " + ColumnName + " FROM " + tableName + " WHERE " + filter);
        if (dtFundOrCompCode.Rows.Count > 0)
        {
            fundOrCompCode = dtFundOrCompCode.Rows[0][ColumnName].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtFundOrCompCode.Rows[0][ColumnName].ToString());
        }
        return fundOrCompCode;
    }
}

