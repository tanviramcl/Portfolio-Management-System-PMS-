using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_DividendReceived : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    double taxRate = 20.00;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = FundName_BookCLOSERDetails_DropDownList();
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();


            companyNameDropDownList.DataSource = dropDownListObj.FillCompanyNameDropDownList();
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            GrirdViewBookCloserDeatils(taxRate);

          

        }
    }


    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TaxRateTextBox.Text != "")
        {
             taxRate = Convert.ToDouble(TaxRateTextBox.Text.Trim());           
        }
        GrirdViewBookCloserDeatils(taxRate);
    }
    protected void companyNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TaxRateTextBox.Text != "")
        {
              taxRate = Convert.ToDouble(TaxRateTextBox.Text.Trim());
        }
        GrirdViewBookCloserDeatils(taxRate);
    }
   
    //protected void fyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GrirdViewBookCloserDeatils();
    //}

    //protected void fyPartDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GrirdViewBookCloserDeatils();
    //}

    //protected void recordDateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GrirdViewBookCloserDeatils();
    //}

    //protected void agmDateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GrirdViewBookCloserDeatils();
    //}
    protected void postedButton_Click(object sender, EventArgs e)
    {
        string LoginName = Session["UserID"].ToString();
        CommonGateway commonGatewayObj = new CommonGateway();
        int countcheck = 0;
        int count = 0;
        int countUpdate = 0;

    

        string PaymentDt = PaymentDateTextBox.Text.ToString();
        string paymentType = PaymenttypeDropDownList.SelectedValue.ToString();


        foreach (DataGridItem gridRow in leftDataGrid.Items)
        {
            CheckBox leftCheckBox = (CheckBox)gridRow.Cells[0].FindControl("leftCheckBox");

            if (leftCheckBox.Checked)
            {
                
            
                TextBox EXCESSSHORTAGE = (TextBox)gridRow.Cells[0].FindControl("EXCESSSHORTAGE");               
                TextBox remarks = (TextBox)gridRow.Cells[0].FindControl("RemarksTextBox");
                TextBox txtTaxAmount = (TextBox)gridRow.Cells[0].FindControl("TAX_AMTTXT");
                TextBox txtNetAmount = (TextBox)gridRow.Cells[0].FindControl("NET_AMTTXT");
                int fund_code = Convert.ToInt16(gridRow.Cells[1].Text.ToString());               
                decimal RECEIVABLE_excessSortage = Convert.ToDecimal(EXCESSSHORTAGE.Text.ToString());
               
                string comp_name = gridRow.Cells[6].Text.ToString();
                string DIV_RRCIVED_ACC_VCH_NO = gridRow.Cells[2].Text.ToString();
                long totalShare = Convert.ToInt32(gridRow.Cells[7].Text.ToString());
                string fy = gridRow.Cells[11].Text.ToString();
                decimal diviRatePerShare = Convert.ToDecimal(gridRow.Cells[13].Text.ToString());

                Double Gross_amt = Convert.ToDouble(gridRow.Cells[14].Text.ToString());
                double tax_rate = Convert.ToDouble(TaxRateTextBox.Text.ToString());
                double Tax_amt = Convert.ToDouble(txtTaxAmount.Text.ToString());
                double net_amt = Gross_amt - Tax_amt;



                commonGatewayObj.BeginTransaction();
                string accReceivedVCHNo = "";
                accReceivedVCHNo = SaveDividendAccountVoucher(fund_code, totalShare, net_amt, Tax_amt, comp_name, diviRatePerShare, fy, DIV_RRCIVED_ACC_VCH_NO, PaymentDt, LoginName);
                string accFracReceivedVCHNo = "";
                if (RECEIVABLE_excessSortage>0)
                 accFracReceivedVCHNo = SaveFractonDividendAccountVoucher(fund_code, totalShare,Convert.ToDouble( RECEIVABLE_excessSortage), comp_name, diviRatePerShare, fy, DIV_RRCIVED_ACC_VCH_NO, PaymentDt, LoginName);

                string strupdateQueryCorporateDec = "update BOOK_CL_DETAILS set    NET_DIVIDEND="+ net_amt + ",TAX=" + Tax_amt + ",RECEIVABLE=" + Gross_amt + ",FRACTION_DIV_AMT=" + RECEIVABLE_excessSortage + ",PAYMENT_DATE='" + PaymentDt + "',PAYMENT_TYPE='" + paymentType + "' ";
                if(accReceivedVCHNo!="")
                    strupdateQueryCorporateDec = strupdateQueryCorporateDec + ",DIV_RRCIVED_ACC_VCH_NO ='" + accReceivedVCHNo  + "'";
                if(accFracReceivedVCHNo!="")
                    strupdateQueryCorporateDec = strupdateQueryCorporateDec + ", FRAC_DIV_RRCIVED_ACC_VCH_NO='" + accFracReceivedVCHNo + "'";

                strupdateQueryCorporateDec = strupdateQueryCorporateDec + " where  BOOK_CL_DET_ID=" + gridRow.Cells[3].Text.ToString() + " ";

                int updateCorporateDecNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryCorporateDec);
                commonGatewayObj.CommitTransaction();
                if (updateCorporateDecNumOfRows > 0)
                {
                    countUpdate++;
                }

                count++;
            }
            countcheck++;
        }


        if (count == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please Check the checkbox');", true);

        }
        else
        {
            // Response.Redirect("DividendReceived.aspx");
            if (TaxRateTextBox.Text != "")
            {
                taxRate = Convert.ToDouble(TaxRateTextBox.Text.Trim());
            }
            GrirdViewBookCloserDeatils(taxRate);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Save Success!');", true);
        }
    }

    public string SaveDividendAccountVoucher(int fund_code, long totalShare , double totalDivAmt, double taxAmount, string comp_name, decimal diviRate, string fy, string divReceivable_acc_vch_no,string payDate, string LoginName)
    {

        try
        {
            DataTable dtAccountSchema = commonGatewayObj.Select(" SELECT * FROM FUND_PARA WHERE F_CD=" + fund_code);
            string accountSchema = dtAccountSchema.Rows[0]["F_ACC_SCHEMA"].ToString();
           // string accountSchema = "UNIT_BR";

            string fundBankACCCode = dtAccountSchema.Rows[0]["F_ACC_BANK_CODE"].ToString();
            string dividendReceivableCode = "205010000";
            string dividendAdvanceTaxCode = "207030000";
            if (fund_code == 1)
            {
                dividendReceivableCode = "209010000";
                dividendAdvanceTaxCode = "214080000";
            }

            long tranNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TRAN_ID");
            long contrlNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TO_NUMBER(CTRLNO)") - 1;
            int fundCode = fund_code;
                               

            Hashtable htInsert = new Hashtable();
            Hashtable htUpdate = new Hashtable();
            commonGatewayObj.BeginTransaction();
            string VoucherNo = getNexAccountVoucherNo(accountSchema, "11");



            //Dividend Receivable Debit
            contrlNumber++;
            htInsert = new Hashtable();
            htInsert.Add("TRAN_ID", tranNumber + 1);
            htInsert.Add("ACCCODE", fundBankACCCode);
            htInsert.Add("BANKACNO", fundBankACCCode);
            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
            htInsert.Add("TRAN_DATE", payDate);
            htInsert.Add("REMARKS", " Div. Received(of vch_no=" + divReceivable_acc_vch_no + ") from " + comp_name + " for fy:" + fy + " of Share:" + totalShare + " @" + diviRate);
            htInsert.Add("TRAN_TYPE", "D");
            htInsert.Add("VOUCHER_NO", VoucherNo);
            htInsert.Add("TOTAL_AMNT", totalDivAmt);
            htInsert.Add("CTRLNO", contrlNumber);
            htInsert.Add("OP_ID", LoginName);
            htInsert.Add("VOUCHER_TYPE", "11");
            htInsert.Add("RECENT", "y");
            htInsert.Add("LATESTDEL", "m");
            htInsert.Add("ISOUT", "N");
            htInsert.Add("ISREV", "N");
            htInsert.Add("OLDDATA", "N");
            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

            //Dividend Advance Tax Debit
            if (taxAmount > 0)
            {
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividendAdvanceTaxCode);
                htInsert.Add("BANKACNO", dividendAdvanceTaxCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", payDate);
                htInsert.Add("REMARKS", " Div. Tax Diduct (of vch_no=" + divReceivable_acc_vch_no + ") from " + comp_name + " for fy:" + fy + " of Share:" + totalShare + " @" + diviRate);
                htInsert.Add("TRAN_TYPE", "D");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", taxAmount);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "11");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");
                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
            }


            //Dividend Income Debit
            contrlNumber++;
            htInsert = new Hashtable();
            htInsert.Add("TRAN_ID", tranNumber + 1);
            htInsert.Add("ACCCODE", dividendReceivableCode);
            htInsert.Add("BANKACNO_CONTRA", dividendReceivableCode);
            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
            htInsert.Add("TRAN_DATE", payDate);
            htInsert.Add("REMARKS", " Div. Received(of vch_no=" + divReceivable_acc_vch_no + ") from " + comp_name + " for fy:" + fy + " of Share:" + totalShare + " @" + diviRate);
            htInsert.Add("TRAN_TYPE", "C");
            htInsert.Add("VOUCHER_NO", VoucherNo);
            htInsert.Add("TOTAL_AMNT", totalDivAmt+ taxAmount);
            htInsert.Add("CTRLNO", contrlNumber);
            htInsert.Add("OP_ID", LoginName);
            htInsert.Add("VOUCHER_TYPE", "11");
            htInsert.Add("RECENT", "y");
            htInsert.Add("LATESTDEL", "m");
            htInsert.Add("ISOUT", "N");
            htInsert.Add("ISREV", "N");
            htInsert.Add("OLDDATA", "N");

            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");



            //UPDATE TRANSACTION NUMBER AND CONTROL NUMBER
            contrlNumber++;
            tranNumber++;
            commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");

            commonGatewayObj.CommitTransaction();
            return VoucherNo;

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }

    }
    public string SaveFractonDividendAccountVoucher(int fund_code, long totalShare, double totalFracDivAmt, string comp_name, decimal diviRate, string fy, string divReceivable_acc_vch_no, string payDate, string LoginName)
    {

        try
        {
            DataTable dtAccountSchema = commonGatewayObj.Select(" SELECT * FROM FUND_PARA WHERE F_CD=" + fund_code);
            string accountSchema = dtAccountSchema.Rows[0]["F_ACC_SCHEMA"].ToString();
            // string accountSchema = "UNIT_BR";

            string dividnedIncomeCode = "303010000";
            string fundBankACCCode = dtAccountSchema.Rows[0]["F_ACC_BANK_CODE"].ToString();
          

            long tranNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TRAN_ID");
            long contrlNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TO_NUMBER(CTRLNO)") - 1;
            int fundCode = fund_code;


            Hashtable htInsert = new Hashtable();
            Hashtable htUpdate = new Hashtable();
            commonGatewayObj.BeginTransaction();
            string VoucherNo = getNexAccountVoucherNo(accountSchema, "11");

            //Dividend Bank Account Debit
            contrlNumber++;
            htInsert = new Hashtable();
            htInsert.Add("TRAN_ID", tranNumber + 1);
            htInsert.Add("ACCCODE", fundBankACCCode);
            htInsert.Add("BANKACNO", fundBankACCCode);
            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
            htInsert.Add("TRAN_DATE", payDate);
            htInsert.Add("REMARKS", "Fractoin Div. Received  from " + comp_name + " for fy:" + fy + " of Share:" + totalShare + " @" + diviRate);
            htInsert.Add("TRAN_TYPE", "D");
            htInsert.Add("VOUCHER_NO", VoucherNo);
            htInsert.Add("TOTAL_AMNT", totalFracDivAmt);
            htInsert.Add("CTRLNO", contrlNumber);
            htInsert.Add("OP_ID", LoginName);
            htInsert.Add("VOUCHER_TYPE", "11");
            htInsert.Add("RECENT", "y");
            htInsert.Add("LATESTDEL", "m");
            htInsert.Add("ISOUT", "N");
            htInsert.Add("ISREV", "N");
            htInsert.Add("OLDDATA", "N");

            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

            //Dividend Income CREDIT

            contrlNumber++;
            htInsert = new Hashtable();
            htInsert.Add("TRAN_ID", tranNumber + 1);
            htInsert.Add("ACCCODE", dividnedIncomeCode);
            htInsert.Add("BANKACNO_CONTRA", dividnedIncomeCode);
            htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
            htInsert.Add("TRAN_DATE", payDate);
            htInsert.Add("REMARKS", " Fractoin Div. Received from " + comp_name + " for fy:" + fy + " of Share:" + totalShare + " @" + diviRate);
            htInsert.Add("TRAN_TYPE", "C");
            htInsert.Add("VOUCHER_NO", VoucherNo);
            htInsert.Add("TOTAL_AMNT", totalFracDivAmt);
            htInsert.Add("CTRLNO", contrlNumber);
            htInsert.Add("OP_ID", LoginName);
            htInsert.Add("VOUCHER_TYPE", "11");
            htInsert.Add("RECENT", "y");
            htInsert.Add("LATESTDEL", "m");
            htInsert.Add("ISOUT", "N");
            htInsert.Add("ISREV", "N");
            htInsert.Add("OLDDATA", "N");

            commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");
          

            contrlNumber++;
            tranNumber++;
            commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");



            commonGatewayObj.CommitTransaction();
            return VoucherNo;

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }

    }
    public string getNexAccountVoucherNo(string accountSchema, string voucherTypeNo)
    {
        int accVoucherNo = 0;
        DataTable dtAccountVoucherNo = commonGatewayObj.Select("SELECT VOUCHER_NO FROM " + accountSchema + ".GL_TRAN WHERE (CTRLNO=(SELECT MAX(TO_NUMBER(CTRLNO))MAX_CTRLNO FROM " + accountSchema + ".GL_TRAN GL_TRAN_1 WHERE (VOUCHER_TYPE='" + voucherTypeNo + "'))) ");
        if (dtAccountVoucherNo.Rows.Count > 0)
        {
            string voucherNo = dtAccountVoucherNo.Rows[0]["VOUCHER_NO"].ToString().ToUpper();
            accVoucherNo = Convert.ToInt32(voucherNo.ToString()) + 1;
        }
        else
        {
            accVoucherNo = 1;
        }
        return accVoucherNo.ToString();

    }
    

    public void GrirdViewBookCloserDeatils( double taxRate)
    {
        DataTable bookCloser_details = new DataTable();

        StringBuilder sbMst = new StringBuilder();
       
        sbMst.Append(" SELECT A.*,ROUND(A.GROSS_DIVIDEND*(" + taxRate + "/100),2) AS TAX_AMT,A.GROSS_DIVIDEND-ROUND(A.GROSS_DIVIDEND*(" + taxRate + "/100),2) AS NET_AMT,0 AS FRAC_DIV_AMT, B.FY,B.FY_PART,B.AGM,B.RECORD_DT,B.TYPE, C.F_NAME,D.COMP_NM FROM(SELECT * FROM BOOK_CL_DETAILS  WHERE PAYMENT_DATE IS NULL) A ");
        sbMst.Append(" INNER JOIN (SELECT * FROM CORPORATE_DEC) B  ON A.BOOK_CL_ID=B.BOOK_CL_ID INNER JOIN(SELECT * FROM FUND C WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL)C ON A.F_CD=C.F_CD ");
        sbMst.Append(" INNER JOIN (SELECT * FROM COMP) D ON A.COMP_CD=D.COMP_CD WHERE 1=1 AND B.TYPE='C' ");

        if(fundNameDropDownList.SelectedValue.ToString()!="0")
        {
            sbMst.Append(" AND A.F_CD='"+ fundNameDropDownList.SelectedValue.ToString() + "'");
        }
        if (companyNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbMst.Append(" AND A.COMP_CD='" + companyNameDropDownList.SelectedValue.ToString() + "'");
        }

        bookCloser_details = commonGatewayObj.Select(sbMst.ToString());
        if (bookCloser_details.Rows.Count > 0)
        {
            leftDataGrid.DataSource = bookCloser_details;
            leftDataGrid.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data found!');", true);
        }




    }


   
    public DataTable FundName_BookCLOSERDetails_DropDownList()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_CD IN(Select distinct(f_cd) As FUND_CD from BOOK_CL_DETAILS WHERE PAYMENT_DATE IS NULL) ORDER BY F_CD");
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




    protected void TaxRateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (TaxRateTextBox.Text != "")
        {
            taxRate = Convert.ToDouble(TaxRateTextBox.Text.Trim());
            GrirdViewBookCloserDeatils(taxRate);
        }
    }
}