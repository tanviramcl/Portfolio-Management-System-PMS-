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

/// <summary>
/// Summary description for DropDownList
/// </summary>
public class DropDownList
{
    CommonGateway commonGatewayObj = new CommonGateway();
    
	public DropDownList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable FillSectorDropDownList()//Sector Name
    {

        DataTable dtSectorName = commonGatewayObj.Select("SELECT * FROM SECT_MAJ ORDER BY SECT_MAJ_NM");
        DataTable dtSectorNameDropDownList = new DataTable();
        dtSectorNameDropDownList.Columns.Add("SECT_MAJ_NM", typeof(string));
        dtSectorNameDropDownList.Columns.Add("SECT_MAJ_CD", typeof(string));
        DataRow dr = dtSectorNameDropDownList.NewRow();
        dr["SECT_MAJ_NM"] = "--Click Here to Select--";
        dr["SECT_MAJ_CD"] = "0";
        dtSectorNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtSectorName.Rows.Count; loop++)
        {
            dr = dtSectorNameDropDownList.NewRow();
            dr["SECT_MAJ_NM"] = dtSectorName.Rows[loop]["SECT_MAJ_NM"].ToString();
            dr["SECT_MAJ_CD"] = Convert.ToInt32(dtSectorName.Rows[loop]["SECT_MAJ_CD"]);
            dtSectorNameDropDownList.Rows.Add(dr);
        }
        return dtSectorNameDropDownList;
    }
    public DataTable FillFYDropDownList()//For All Company Name
    {
        DataTable dtFY = commonGatewayObj.Select("select distinct(FY) AS FY from CORPORATE_DEC");
        DataTable dtFYDropDownList = new DataTable();
        dtFYDropDownList.Columns.Add("FY_NAME", typeof(string));
        dtFYDropDownList.Columns.Add("FY_ID", typeof(string));

        DataRow dr = dtFYDropDownList.NewRow();
        dr["FY_NAME"] = "--Click Here to Select--";
        dr["FY_ID"] = "0";
        dtFYDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFY.Rows.Count; loop++)
        {
            dr = dtFYDropDownList.NewRow();
            dr["FY_NAME"] = dtFY.Rows[loop]["FY"].ToString();
            dr["FY_ID"] = dtFY.Rows[loop]["FY"].ToString();
            dtFYDropDownList.Rows.Add(dr);
        }
        return dtFYDropDownList;
    }

    public DataTable FillFY_PartDropDownList()//For All Company Name
    {
        DataTable dtFY = commonGatewayObj.Select("select distinct(FY_PART) AS FY_PART from CORPORATE_DEC");
        DataTable dtFYDropDownList = new DataTable();
        dtFYDropDownList.Columns.Add("FYPart_NAME", typeof(string));
        dtFYDropDownList.Columns.Add("FYPart_ID", typeof(string));

        DataRow dr = dtFYDropDownList.NewRow();
        dr["FYPart_NAME"] = "--Click Here to Select--";
        dr["FYPart_ID"] = "0";
        dtFYDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFY.Rows.Count; loop++)
        {
            dr = dtFYDropDownList.NewRow();
            dr["FYPart_NAME"] = dtFY.Rows[loop]["FY_PART"].ToString();
            dr["FYPart_ID"] = dtFY.Rows[loop]["FY_PART"].ToString();
            dtFYDropDownList.Rows.Add(dr);
        }
        return dtFYDropDownList;
    }

    public DataTable FillBookCloserID_DropDownList()//For All Company Name
    {
        DataTable dtBookCloseID = commonGatewayObj.Select("select distinct(BOOK_CL_ID) AS BOOK_CL_ID from BOOK_CL_DETAILS");
        return dtBookCloseID;
    }

    public DataTable FillCompanyNameDropFromBookCloserDetailsDropDownList(string F_cd)//For All Company Name
    {
        DataTable dtCompName = commonGatewayObj.Select("SELECT COMP_NM, COMP_CD FROM COMP Where comp_cd in(select distinct comp_cd from  BOOK_CL_DETAILS where f_cd=" + F_cd + ")  ");
        DataTable dtCompNameDropDownList = new DataTable();
        dtCompNameDropDownList.Columns.Add("COMP_NM", typeof(string));
        dtCompNameDropDownList.Columns.Add("COMP_CD", typeof(string));
        DataRow dr = dtCompNameDropDownList.NewRow();
        dr["COMP_NM"] = "--Click Here to Select--";
        dr["COMP_CD"] = "0";
        dtCompNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtCompName.Rows.Count; loop++)
        {
            dr = dtCompNameDropDownList.NewRow();
            dr["COMP_NM"] = dtCompName.Rows[loop]["COMP_NM"].ToString();
            dr["COMP_CD"] = Convert.ToInt32(dtCompName.Rows[loop]["COMP_CD"]);
            dtCompNameDropDownList.Rows.Add(dr);
        }
        return dtCompNameDropDownList;
    }

    public static explicit operator DropDownList(Control v)
    {
        throw new NotImplementedException();
    }

    public DataTable FillCompanyNameDropDownList()//For All Company Name
    {
        DataTable dtCompName = commonGatewayObj.Select("SELECT COMP_NM, COMP_CD FROM COMP ORDER BY COMP_NM");
        DataTable dtCompNameDropDownList = new DataTable();
        dtCompNameDropDownList.Columns.Add("COMP_NM", typeof(string));
        dtCompNameDropDownList.Columns.Add("COMP_CD", typeof(string));
        DataRow dr = dtCompNameDropDownList.NewRow();
        dr["COMP_NM"] = "--Click Here to Select--";
        dr["COMP_CD"] = "0";
        dtCompNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtCompName.Rows.Count; loop++)
        {
            dr = dtCompNameDropDownList.NewRow();
            dr["COMP_NM"] = dtCompName.Rows[loop]["COMP_NM"].ToString();
            dr["COMP_CD"] = Convert.ToInt32(dtCompName.Rows[loop]["COMP_CD"]);
            dtCompNameDropDownList.Rows.Add(dr);
        }
        return dtCompNameDropDownList;
    }
	public DataTable GetICBTrusteeDropDownList()//Get BAL_DT_CTRL from invest.PFOLIO_BK Table
    {
        DataTable dttrustee = commonGatewayObj.Select("SELECT  TRUSTEE_ID, TRUSTEE_NAME, TRUSTEE_ADDRESS FROM TRUSTEE");
        DataTable dttrusteeDropDownList = new DataTable();
        dttrusteeDropDownList.Columns.Add("TRUSTEE_ID", typeof(string));
        dttrusteeDropDownList.Columns.Add("TRUSTEE_NAME", typeof(string));
        DataRow dr = dttrusteeDropDownList.NewRow();
        dr["TRUSTEE_NAME"] = "--Select--";
        dr["TRUSTEE_ID"] = "0";
        dttrusteeDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dttrustee.Rows.Count; loop++)
        {
            dr = dttrusteeDropDownList.NewRow();
            dr["TRUSTEE_ID"] = dttrustee.Rows[loop]["TRUSTEE_ID"].ToString();
            dr["TRUSTEE_NAME"] = dttrustee.Rows[loop]["TRUSTEE_NAME"].ToString();
            dttrusteeDropDownList.Rows.Add(dr);
        }
        return dttrusteeDropDownList;
    }
    public DataTable FilllistedCategoryDropDownList()//For All Company Name
    {
        DataTable ComplistedCategory = commonGatewayObj.Select(" select flag, DECODE(flag,'D','D listed','N','Nonlisted','L','Listed','C','CLOSE','I','IPO',flag,'IPO') flagValue from (select distinct(flag) as flag from comp)");
        DataTable dtComplistedCategory = new DataTable();
        dtComplistedCategory.Columns.Add("flagValue", typeof(string));
        dtComplistedCategory.Columns.Add("flag", typeof(string));

        DataRow dr = dtComplistedCategory.NewRow();

        dr["flagValue"] = "--Click Here to Select--";
        dr["flag"] = "0";
        dtComplistedCategory.Rows.Add(dr);


        for (int loop = 0; loop < ComplistedCategory.Rows.Count; loop++)
        {
            dr = dtComplistedCategory.NewRow();
            dr["flagValue"] = ComplistedCategory.Rows[loop]["flagValue"].ToString();
            dr["flag"] = ComplistedCategory.Rows[loop]["flag"].ToString();

            dtComplistedCategory.Rows.Add(dr);
        }
        return dtComplistedCategory;
    }


    public DataTable FundName_BookCLOSERDetails_DropDownList()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_CD IN(Select distinct(f_cd) As FUND_CD from BOOK_CL_DETAILS) ORDER BY F_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--Click Here to Select--";
        dr["F_CD"] = "0";
        dtFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }

    public DataTable FundNameDropDownList()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL ORDER BY F_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--Click Here to Select--";
        dr["F_CD"] = "0";
        dtFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }
    public DataTable BANKDROPDOWNLIST()//For All Funds
    {
        DataTable dtbankName = commonGatewayObj.Select("select * from UNIT.BANK_NAME");
        DataTable dtbankNameDropDownList = new DataTable();
        dtbankNameDropDownList.Columns.Add("BANK_CODE", typeof(string));
        dtbankNameDropDownList.Columns.Add("BANK_NAME", typeof(string));
        DataRow dr = dtbankNameDropDownList.NewRow();
        dr["BANK_NAME"] = "--Click Here to Select--";
        dr["BANK_CODE"] = "0";
        dtbankNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtbankName.Rows.Count; loop++)
        {
            dr = dtbankNameDropDownList.NewRow();
            dr["BANK_NAME"] = dtbankName.Rows[loop]["BANK_NAME"].ToString();
            dr["BANK_CODE"] = Convert.ToInt32(dtbankName.Rows[loop]["BANK_CODE"]);
            dtbankNameDropDownList.Rows.Add(dr);
        }
        return dtbankNameDropDownList;
    }
    public DataTable NolistedCompanyCodeNameDropDownList()//For All Funds
    {
        DataTable dtnonListedCompanyName = commonGatewayObj.Select("Select COMP_CD,COMP_NM,SECT_MAJ_CD,ADD1,ADD2,TEL,EMAIL,AUTH_CAP,PAID_CAP,CAT_TP from COMP_NONLISTED order by COMP_NM  ASC");
        DataTable dtnonListedCompanyNameList = new DataTable();
        dtnonListedCompanyNameList.Columns.Add("COMP_NM", typeof(string));
        dtnonListedCompanyNameList.Columns.Add("COMP_CD", typeof(string));
        DataRow dr = dtnonListedCompanyNameList.NewRow();
        dr["COMP_NM"] = "--Click Here to Select--";
        dr["COMP_CD"] = "0";
        dtnonListedCompanyNameList.Rows.Add(dr);
        for (int loop = 0; loop < dtnonListedCompanyName.Rows.Count; loop++)
        {
            dr = dtnonListedCompanyNameList.NewRow();
            dr["COMP_NM"] = dtnonListedCompanyName.Rows[loop]["COMP_NM"].ToString();
            dr["COMP_CD"] = Convert.ToInt32(dtnonListedCompanyName.Rows[loop]["COMP_CD"]);
            dtnonListedCompanyNameList.Rows.Add(dr);
        }
        return dtnonListedCompanyNameList;
    }
    public DataTable NolistedCategoryTypeDropDownList()//For All Funds
    {
        DataTable dtnonListedCategoryName = commonGatewayObj.Select("Select CAT_ID,CAT_NM from NONLISTED_CATEGORY");
        DataTable dtnonListedCategoryNameList = new DataTable();
        dtnonListedCategoryNameList.Columns.Add("CAT_NM", typeof(string));
        dtnonListedCategoryNameList.Columns.Add("CAT_ID", typeof(string));
        DataRow dr = dtnonListedCategoryNameList.NewRow();
        dr["CAT_NM"] = "--Click Here to Select--";
        dr["CAT_ID"] = "0";
        dtnonListedCategoryNameList.Rows.Add(dr);
        for (int loop = 0; loop < dtnonListedCategoryName.Rows.Count; loop++)
        {
            dr = dtnonListedCategoryNameList.NewRow();
            dr["CAT_NM"] = dtnonListedCategoryName.Rows[loop]["CAT_NM"].ToString();
            dr["CAT_ID"] = Convert.ToInt32(dtnonListedCategoryName.Rows[loop]["CAT_ID"]);
            dtnonListedCategoryNameList.Rows.Add(dr);
        }
        return dtnonListedCategoryNameList;
    }
    public DataTable UserNameDropDownList()//For All Funds
    {
        DataTable dtuserList = commonGatewayObj.Select("select a.ID,a.Name,B.ID as DesignationID,B.NAME as DesignationName from(select * from emp_info where valid = 'Y' order by Id asc) a inner join EMP_DESIGNATION  b  on a.DESIG_ID = B.ID");
        DataTable dtuserDropDownList = new DataTable();
        dtuserDropDownList.Columns.Add("DesignationName", typeof(string));
        dtuserDropDownList.Columns.Add("DesignationID", typeof(string));
        dtuserDropDownList.Columns.Add("Name", typeof(string));
        dtuserDropDownList.Columns.Add("ID", typeof(string));
        DataRow dr = dtuserDropDownList.NewRow();
        dr["DesignationName"] = "0";
        dr["DesignationID"] = "0";
        dr["Name"] = "--Click Here to Select--";
        dr["ID"] = "0";
        dtuserDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtuserList.Rows.Count; loop++)
        {
            dr = dtuserDropDownList.NewRow();
            dr["DesignationName"] = dtuserList.Rows[loop]["DesignationName"].ToString();
            dr["DesignationID"] = dtuserList.Rows[loop]["DesignationID"].ToString();
            dr["Name"] = dtuserList.Rows[loop]["Name"].ToString();
            dr["ID"] =dtuserList.Rows[loop]["ID"].ToString();
            dtuserDropDownList.Rows.Add(dr);
        }
        return dtuserDropDownList;
    }

    public DataTable UserTypeDropDownList()//For All Funds
    {
        DataTable dtuserTypeList = commonGatewayObj.Select("select ROLE_ID,ROLE_NAME from user_role");
        DataTable dtuserTypeDropDownList = new DataTable();

        dtuserTypeDropDownList.Columns.Add("ROLE_NAME", typeof(string));
        dtuserTypeDropDownList.Columns.Add("ROLE_ID", typeof(string));
        DataRow dr = dtuserTypeDropDownList.NewRow();
       
        dr["ROLE_NAME"] = "--Click Here to Select--";
        dr["ROLE_ID"] = "0";
        dtuserTypeDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtuserTypeList.Rows.Count; loop++)
        {
            dr = dtuserTypeDropDownList.NewRow();
            dr["ROLE_NAME"] = dtuserTypeList.Rows[loop]["ROLE_NAME"].ToString();
            dr["ROLE_ID"] = dtuserTypeList.Rows[loop]["ROLE_ID"].ToString();
            dtuserTypeDropDownList.Rows.Add(dr);
        }
        return dtuserTypeDropDownList;
    }
    public DataTable CloseEndFundNameDropDownList()//For Close End MF
    {
        DataTable dtCloseEndFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM FUND WHERE (F_CD between 3 and 26) AND F_CD NOT IN(4) ORDER BY F_CD");
        DataTable dtCloseEndFundNameDropDownList = new DataTable();
        dtCloseEndFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtCloseEndFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtCloseEndFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--All Close End Mutual Funds--";
        dr["F_CD"] = "0";
        dtCloseEndFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtCloseEndFundName.Rows.Count; loop++)
        {
            dr = dtCloseEndFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtCloseEndFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtCloseEndFundName.Rows[loop]["F_CD"]);
            dtCloseEndFundNameDropDownList.Rows.Add(dr);
        }
        return dtCloseEndFundNameDropDownList;
    }
    public DataTable OpenEndFundNameDropDownList()//For Open End MF
    {
        DataTable dtOpenEndFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM FUND WHERE F_CD IN(2,4) ORDER BY F_CD");
        DataTable dtOpenEndFundNameDropDownList = new DataTable();
        dtOpenEndFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtOpenEndFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtOpenEndFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--Click Here to Select--";
        dr["F_CD"] = "0";
        dtOpenEndFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtOpenEndFundName.Rows.Count; loop++)
        {
            dr = dtOpenEndFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtOpenEndFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtOpenEndFundName.Rows[loop]["F_CD"]);
            dtOpenEndFundNameDropDownList.Rows.Add(dr);
        }
        return dtOpenEndFundNameDropDownList;
    }
    public DataTable SignatoryDropDownList()//For Authorized Signatory
    {
        DataTable dtSignatoryName = commonGatewayObj.Select("SELECT ID, NAME FROM NAV_LETTER.SIGNATORY");
        DataTable dtSignatoryNameDropDownList = new DataTable();
        dtSignatoryNameDropDownList.Columns.Add("NAME", typeof(string));
        dtSignatoryNameDropDownList.Columns.Add("ID", typeof(string));
        DataRow dr = dtSignatoryNameDropDownList.NewRow();
        //dr["NAME"] = "Tanzina Ahsan";
        //dr["ID"] = "IAMCL411";
        //dtSignatoryNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtSignatoryName.Rows.Count; loop++)
        {
            dr = dtSignatoryNameDropDownList.NewRow();
            dr["NAME"] = dtSignatoryName.Rows[loop]["NAME"].ToString();
            dr["ID"] = Convert.ToString(dtSignatoryName.Rows[loop]["ID"]);
            dtSignatoryNameDropDownList.Rows.Add(dr);
        }
        return dtSignatoryNameDropDownList;
    }
    public DataTable PaperNameDropDownList()//For Authorized Signatory
    {
        DataTable dtPaperName = commonGatewayObj.Select("SELECT ID, NEWS_PAPER_NAME FROM NAV_LETTER.NEWS_PAPER");
        DataTable dtPaperNameDropDownList = new DataTable();
        dtPaperNameDropDownList.Columns.Add("NEWS_PAPER_NAME", typeof(string));
        dtPaperNameDropDownList.Columns.Add("ID", typeof(string));
        DataRow dr = dtPaperNameDropDownList.NewRow();
        for (int loop = 0; loop < dtPaperName.Rows.Count; loop++)
        {
            dr = dtPaperNameDropDownList.NewRow();
            dr["NEWS_PAPER_NAME"] = dtPaperName.Rows[loop]["NEWS_PAPER_NAME"].ToString();
            dr["ID"] = Convert.ToString(dtPaperName.Rows[loop]["ID"]);
            dtPaperNameDropDownList.Rows.Add(dr);
        }
        return dtPaperNameDropDownList;
    }
    public DataTable MonthlyBankAdviceDropDownList()//For Month of Bank Advice
    {
        DataTable dtMonthOfBankAdvice = commonGatewayObj.Select("SELECT  CAL_DATE, TO_CHAR(CAL_DATE, 'MONTH,YYYY') AS MONTH_OF_BANK_ADVICE FROM AMCL_EMP_SALARY GROUP BY CAL_DATE ORDER BY CAL_DATE DESC");
        DataTable dtMonthOfBankAdviceDropDownList = new DataTable();
        dtMonthOfBankAdviceDropDownList.Columns.Add("MONTH_OF_BANK_ADVICE", typeof(string));
        dtMonthOfBankAdviceDropDownList.Columns.Add("CAL_DATE", typeof(string));
        DataRow dr = dtMonthOfBankAdviceDropDownList.NewRow();
        dr["MONTH_OF_BANK_ADVICE"] = "--Select--";
        dr["CAL_DATE"] = "0";
        dtMonthOfBankAdviceDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtMonthOfBankAdvice.Rows.Count; loop++)
        {
            dr = dtMonthOfBankAdviceDropDownList.NewRow();
            dr["MONTH_OF_BANK_ADVICE"] = dtMonthOfBankAdvice.Rows[loop]["MONTH_OF_BANK_ADVICE"].ToString();
            dr["CAL_DATE"] = Convert.ToString(dtMonthOfBankAdvice.Rows[loop]["CAL_DATE"]);
            dtMonthOfBankAdviceDropDownList.Rows.Add(dr);
        }
        return dtMonthOfBankAdviceDropDownList;
    }
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

    public DataTable BalanceDateDropDownList()//Get BAL_DT_CTRL from invest.PFOLIO_BK Table
    {
        DataTable dtHowlaDate = commonGatewayObj.Select("SELECT DISTINCT BAL_DT_CTRL    FROM   PFOLIO_BK  ORDER BY BAL_DT_CTRL DESC");
        DataTable dtHowlaDateDropDownList = new DataTable();
        dtHowlaDateDropDownList.Columns.Add("Howla_Date", typeof(string));
        dtHowlaDateDropDownList.Columns.Add("BAL_DT_CTRL", typeof(string));
        DataRow dr = dtHowlaDateDropDownList.NewRow();
        dr["Howla_Date"] = "--Select--";
        dr["BAL_DT_CTRL"] = "0";
        dtHowlaDateDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtHowlaDate.Rows.Count; loop++)
        {
            dr = dtHowlaDateDropDownList.NewRow();
            dr["Howla_Date"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["BAL_DT_CTRL"]).ToString("dd-MMM-yyyy");
            dr["BAL_DT_CTRL"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["BAL_DT_CTRL"]).ToString("dd-MMM-yyyy");
            dtHowlaDateDropDownList.Rows.Add(dr);
        }
        return dtHowlaDateDropDownList;
    }

    public DataTable pdateDropDownList()//For Authorized Signatory
    {
        DataTable pdate = commonGatewayObj.Select("select max(vch_dt) as vch_dt from fund_trans_hb");
        DataTable pdateDropDownList = new DataTable();
        pdateDropDownList.Columns.Add("p1date", typeof(string));
        pdateDropDownList.Columns.Add("p2date", typeof(string));
        DataRow dr = pdateDropDownList.NewRow();
       
        for (int loop = 0; loop < pdate.Rows.Count; loop++)
        {
            dr = pdateDropDownList.NewRow();
            dr["p1date"] = pdate.Rows[loop]["NAME"].ToString();
            dr["p2date"] = pdate.Rows[loop]["ID"].ToString();
            pdateDropDownList.Rows.Add(dr);
        }
        return pdateDropDownList;
    }

    public DataTable UserDropDownList()//For All Funds
    {
        DataTable dtUserList = commonGatewayObj.Select("select ID,User_ID,password,Name,Designation,Role_name from (select * from  user_table) u inner join user_role ur on U.ROLE_ID=UR.ROLE_ID order by Id ");
        DataTable dtUserListeDropDownList = new DataTable();
        dtUserListeDropDownList.Columns.Add("User_ID", typeof(string));
        dtUserListeDropDownList.Columns.Add("ID", typeof(string));
        DataRow dr = dtUserListeDropDownList.NewRow();
        dr["User_ID"] = "--Click Here to Select--";
        dr["ID"] = "0";
        dtUserListeDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtUserList.Rows.Count; loop++)
        {
            dr = dtUserListeDropDownList.NewRow();
            dr["User_ID"] = dtUserList.Rows[loop]["User_ID"].ToString();
            dr["ID"] = Convert.ToInt32(dtUserList.Rows[loop]["ID"]);
            dtUserListeDropDownList.Rows.Add(dr);
        }
        return dtUserListeDropDownList;
    }

}
