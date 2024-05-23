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

public partial class UI_CompanyInfoUpdate : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtFillCompanyDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtSectorNameDropDownList = dropDownListObj.FillSectorDropDownList();
        if (!IsPostBack)
        {
            sectorDropDownList.DataSource = dtSectorNameDropDownList;
            sectorDropDownList.DataTextField = "SECT_MAJ_NM";
            sectorDropDownList.DataValueField = "SECT_MAJ_CD";
            sectorDropDownList.DataBind();

            companyNameDropDownList.DataSource = dtFillCompanyDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();
        }

    }
    
    private void ClearFields()
    {
        companyNameDropDownList.SelectedValue = "0";
        sectorDropDownList.SelectedValue = "0";
        lastYearEndTextBox.Text = "";
        authorizedCapitalTextBox.Text = "";
        paidupCapitalTextBox.Text = "";
        reserveAndSurplusTextBox.Text = "";
        faceValueTextBox.Text = "";
        totalNoOfSecuritiesTextBox.Text = "";
        marketLotTextBox.Text = "";
        marketCategoryDropDownList.SelectedValue = "0";
        electronicShareDropDownList.SelectedValue = "0";
        firstQuarterEpsTextBox.Text = "";
        secondQuarterTextBox.Text = "";
        thirdQuarterTextBox.Text = "";
        fourthQuarterTextBox.Text = "";
        navTextBox.Text = "";
        stockDividendTextBox.Text = "";
        cashDividendTextBox.Text = "";
        recordDateFromTextBox.Text = "";
        recordDateToTextBox.Text = "";
        agmDateTextBox.Text = "";
        rightShareTextBox.Text = "";
        rightApprovalDateTextBox.Text = "";
        rightRecordDateFromTextBox.Text = "";
        rightRecordDateToTextBox.Text = "";
        cashEpsTextBox.Text = "";
        interimStockDividendTextBox.Text = "";
        interimCashDividendTextBox.Text = "";
        interimRecordDateFromTextBox.Text = "";
        interimRecordDateToTextBox.Text = "";
        shareOfDirectorTextBox.Text = "";
        shareOfPublicTextBox.Text = "";
        shareOfGovtTextBox.Text = "";
        shareOfForeignTextBox.Text = "";
        shareOfInstitutionTextBox.Text = "";
        percentageGrothEpsTextBox.Text = "";
        percentageGrothCashEpsTextBox.Text = "";
        netProfitTextBox.Text = "";
        totalEquityTextBox.Text = "";
        longTermDebtTextBox.Text = "";
    }

    protected void findButton_Click(object sender, EventArgs e)
    {        
        DataTable dtFind = commonGatewayObj.Select("SELECT * FROM ANALYSIS_MST1 WHERE COMP_CD=" + companyNameDropDownList.SelectedValue );
        if (dtFind.Rows.Count > 0)
        {
            sectorDropDownList.SelectedValue = dtFind.Rows[0]["SECTOR_CD"].Equals(DBNull.Value) ? "0" : dtFind.Rows[0]["SECTOR_CD"].ToString();
            lastYearEndTextBox.Text = dtFind.Rows[0]["FINANCIAL_YEAR"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["FINANCIAL_YEAR"].ToString();
            authorizedCapitalTextBox.Text = dtFind.Rows[0]["AUTHO_CAP"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["AUTHO_CAP"].ToString();
            paidupCapitalTextBox.Text = dtFind.Rows[0]["PAIDUP_CAP"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["PAIDUP_CAP"].ToString();
            reserveAndSurplusTextBox.Text = dtFind.Rows[0]["RESERVE_SURPLUS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["RESERVE_SURPLUS"].ToString();
            faceValueTextBox.Text = dtFind.Rows[0]["FACE_VAL"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["FACE_VAL"].ToString();
            totalNoOfSecuritiesTextBox.Text = dtFind.Rows[0]["TOTAL_NO_SHARES"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["TOTAL_NO_SHARES"].ToString();
            marketLotTextBox.Text = dtFind.Rows[0]["MARKET_LOT"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["MARKET_LOT"].ToString();
            marketCategoryDropDownList.SelectedValue = dtFind.Rows[0]["MARKET_CATEGORY"].Equals(DBNull.Value) ? "0" : dtFind.Rows[0]["MARKET_CATEGORY"].ToString();
            electronicShareDropDownList.SelectedValue = dtFind.Rows[0]["ELECTRONIC_SHARE"].Equals(DBNull.Value) ? "0" : dtFind.Rows[0]["ELECTRONIC_SHARE"].ToString();
            firstQuarterEpsTextBox.Text = dtFind.Rows[0]["Q1_EPS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["Q1_EPS"].ToString();
            secondQuarterTextBox.Text = dtFind.Rows[0]["Q2_EPS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["Q2_EPS"].ToString();
            thirdQuarterTextBox.Text = dtFind.Rows[0]["Q3_EPS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["Q3_EPS"].ToString();
            fourthQuarterTextBox.Text = dtFind.Rows[0]["Q4_EPS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["Q4_EPS"].ToString();
            navTextBox.Text = dtFind.Rows[0]["NAV"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["NAV"].ToString();
            stockDividendTextBox.Text = dtFind.Rows[0]["STOCK_DIVIDEND"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["STOCK_DIVIDEND"].ToString();
            cashDividendTextBox.Text = dtFind.Rows[0]["CASH_DIVIDEND"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["CASH_DIVIDEND"].ToString();
            recordDateFromTextBox.Text = dtFind.Rows[0]["RECORD_DATE_FROM"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["RECORD_DATE_FROM"]).ToString("dd-MMM-yyyy");
            recordDateToTextBox.Text = dtFind.Rows[0]["RECORD_DATE_TO"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["RECORD_DATE_TO"]).ToString("dd-MMM-yyyy");
            agmDateTextBox.Text = dtFind.Rows[0]["AGM_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["AGM_DATE"]).ToString("dd-MMM-yyyy");
            rightShareTextBox.Text = dtFind.Rows[0]["RIGHT_SHARE"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["RIGHT_SHARE"].ToString();
            rightApprovalDateTextBox.Text = dtFind.Rows[0]["RIGHT_APPROVAL_DATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["RIGHT_APPROVAL_DATE"]).ToString("dd-MMM-yyyy");
            rightRecordDateFromTextBox.Text = dtFind.Rows[0]["RIGHT_RECORD_DATE_FROM"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["RIGHT_RECORD_DATE_FROM"]).ToString("dd-MMM-yyyy");
            rightRecordDateToTextBox.Text = dtFind.Rows[0]["RIGHT_RECORD_DATE_TO"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["RIGHT_RECORD_DATE_TO"]).ToString("dd-MMM-yyyy");
            cashEpsTextBox.Text = dtFind.Rows[0]["CASH_EPS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["CASH_EPS"].ToString();
            interimStockDividendTextBox.Text = dtFind.Rows[0]["INTERIM_STOCK_DIVIDEND"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["INTERIM_STOCK_DIVIDEND"].ToString();
            interimCashDividendTextBox.Text = dtFind.Rows[0]["INTERIM_CASH_DIVIDEND"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["INTERIM_CASH_DIVIDEND"].ToString();
            interimRecordDateFromTextBox.Text = dtFind.Rows[0]["INTERIM_RECORD_DATE_FROM"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["INTERIM_RECORD_DATE_FROM"]).ToString("dd-MMM-yyyy");
            interimRecordDateToTextBox.Text = dtFind.Rows[0]["INTERIM_RECORD_DATE_TO"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["INTERIM_RECORD_DATE_TO"]).ToString("dd-MMM-yyyy");
            shareOfDirectorTextBox.Text = dtFind.Rows[0]["SHARE_HOLDER_DIR"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["SHARE_HOLDER_DIR"].ToString();
            shareOfPublicTextBox.Text = dtFind.Rows[0]["SHARE_HOLDER_PUB"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["SHARE_HOLDER_PUB"].ToString();
            shareOfGovtTextBox.Text = dtFind.Rows[0]["SHARE_HOLDER_GOVT"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["SHARE_HOLDER_GOVT"].ToString();
            shareOfForeignTextBox.Text = dtFind.Rows[0]["SHARE_HOLDER_FOREIGN"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["SHARE_HOLDER_FOREIGN"].ToString();
            shareOfInstitutionTextBox.Text = dtFind.Rows[0]["SHARE_HOLDER_INSTITUTION"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["SHARE_HOLDER_INSTITUTION"].ToString();
            percentageGrothEpsTextBox.Text = dtFind.Rows[0]["PERCENTAGE_GROTH_EPS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["PERCENTAGE_GROTH_EPS"].ToString();
            percentageGrothCashEpsTextBox.Text = dtFind.Rows[0]["PERCENTAGE_GROTH_CASH_EPS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["PERCENTAGE_GROTH_CASH_EPS"].ToString();
            netProfitTextBox.Text = dtFind.Rows[0]["NET_PROFIT"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["NET_PROFIT"].ToString();
            totalEquityTextBox.Text = dtFind.Rows[0]["TOTAL_EQUITY"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["TOTAL_EQUITY"].ToString();
            longTermDebtTextBox.Text = dtFind.Rows[0]["LONG_TERM_DEBT"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["LONG_TERM_DEBT"].ToString();

        }
        else
        {
            ClearFields();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No data find');", true);
        }

    }
    protected void updateButton_Click(object sender, EventArgs e)
    {
        string LoginID = Session["UserID"].ToString();
        string LoginName = Session["UserName"].ToString().ToUpper();

        Hashtable httable = new Hashtable();
        httable.Add("COMP_CD", Convert.ToInt16(companyNameDropDownList.SelectedValue));
        
        if (!sectorDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("SECTOR_CD", Convert.ToInt16(sectorDropDownList.SelectedValue));
        }

        if (!lastYearEndTextBox.Text.Equals(""))
        {
            httable.Add("FINANCIAL_YEAR", Convert.ToString(lastYearEndTextBox.Text));
        }
        else
        {
            httable.Add("FINANCIAL_YEAR", null);
        }
        if (!authorizedCapitalTextBox.Text.Equals(""))
        {
            httable.Add("AUTHO_CAP", Convert.ToDecimal(authorizedCapitalTextBox.Text));
        }
        else
        {
            httable.Add("AUTHO_CAP", DBNull.Value);
        }
        if (!paidupCapitalTextBox.Text.Equals(""))
        {
            httable.Add("PAIDUP_CAP", Convert.ToDecimal(paidupCapitalTextBox.Text));
        }
        else
        {
            httable.Add("PAIDUP_CAP", DBNull.Value);
        }
        if (!reserveAndSurplusTextBox.Text.Equals(""))
        {
            httable.Add("RESERVE_SURPLUS", Convert.ToDecimal(reserveAndSurplusTextBox.Text));
        }
        else
        {
            httable.Add("RESERVE_SURPLUS", DBNull.Value);
        }
        if (!faceValueTextBox.Text.Equals(""))
        {
            httable.Add("FACE_VAL", Convert.ToDecimal(faceValueTextBox.Text));
        }
        else
        {
            httable.Add("FACE_VAL", DBNull.Value);
        }
        if (!totalNoOfSecuritiesTextBox.Text.Equals(""))
        {
            httable.Add("TOTAL_NO_SHARES", Convert.ToDecimal(totalNoOfSecuritiesTextBox.Text));
        }
        else
        {
            httable.Add("TOTAL_NO_SHARES", DBNull.Value);
        }
        if (!marketLotTextBox.Text.Equals(""))
        {
            httable.Add("MARKET_LOT", Convert.ToDecimal(marketLotTextBox.Text));
        }
        else
        {
            httable.Add("MARKET_LOT", DBNull.Value);
        }
        if (!marketCategoryDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("MARKET_CATEGORY", Convert.ToChar(marketCategoryDropDownList.SelectedValue));
        }
        if (!electronicShareDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("ELECTRONIC_SHARE", Convert.ToChar(electronicShareDropDownList.SelectedValue));
        }
        if (!firstQuarterEpsTextBox.Text.Equals(""))
        {
            httable.Add("Q1_EPS", Convert.ToDecimal(firstQuarterEpsTextBox.Text));
        }
        else
        {
            httable.Add("Q1_EPS", DBNull.Value);
        }
        if (!secondQuarterTextBox.Text.Equals(""))
        {
            httable.Add("Q2_EPS", Convert.ToDecimal(secondQuarterTextBox.Text));
        }
        else
        {
            httable.Add("Q2_EPS", DBNull.Value);
        }
        if (!thirdQuarterTextBox.Text.Equals(""))
        {
            httable.Add("Q3_EPS", Convert.ToDecimal(thirdQuarterTextBox.Text));
        }
        else
        {
            httable.Add("Q3_EPS", DBNull.Value);
        }
        if (!fourthQuarterTextBox.Text.Equals(""))
        {
            httable.Add("Q4_EPS", Convert.ToDecimal(fourthQuarterTextBox.Text));
        }
        else
        {
            httable.Add("Q4_EPS", DBNull.Value);
        }
        if (!navTextBox.Text.Equals(""))
        {
            httable.Add("NAV", Convert.ToDecimal(navTextBox.Text));
        }
        else
        {
            httable.Add("NAV", DBNull.Value);
        }
        if (!stockDividendTextBox.Text.Equals(""))
        {
            httable.Add("STOCK_DIVIDEND", Convert.ToDecimal(stockDividendTextBox.Text));
        }
        else
        {
            httable.Add("STOCK_DIVIDEND", DBNull.Value);
        }
        if (!cashDividendTextBox.Text.Equals(""))
        {
            httable.Add("CASH_DIVIDEND", Convert.ToDecimal(cashDividendTextBox.Text));
        }
        else
        {
            httable.Add("CASH_DIVIDEND", DBNull.Value);
        }
        if (!recordDateFromTextBox.Text.Equals(""))
        {
            httable.Add("RECORD_DATE_FROM", Convert.ToDateTime(recordDateFromTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RECORD_DATE_FROM", DBNull.Value);
        }
        if (!recordDateToTextBox.Text.Equals(""))
        {
            httable.Add("RECORD_DATE_TO", Convert.ToDateTime(recordDateToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RECORD_DATE_TO", DBNull.Value);
        }
        if (!agmDateTextBox.Text.Equals(""))
        {
            httable.Add("AGM_DATE", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("AGM_DATE", DBNull.Value);
        }
        if (!rightShareTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT_SHARE", Convert.ToDecimal(rightShareTextBox.Text));
        }
        else
        {
            httable.Add("RIGHT_SHARE", DBNull.Value);
        }
        if (!rightApprovalDateTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT_APPROVAL_DATE", Convert.ToDateTime(rightApprovalDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RIGHT_APPROVAL_DATE", DBNull.Value);
        }
        if (!rightRecordDateFromTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT_RECORD_DATE_FROM", Convert.ToDateTime(rightRecordDateFromTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RIGHT_RECORD_DATE_FROM", DBNull.Value);
        }
        if (!rightRecordDateToTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT_RECORD_DATE_TO", Convert.ToDateTime(rightRecordDateToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RIGHT_RECORD_DATE_TO", DBNull.Value);
        }
        if (!cashEpsTextBox.Text.Equals(""))
        {
            httable.Add("CASH_EPS", Convert.ToDecimal(cashEpsTextBox.Text));
        }
        else
        {
            httable.Add("CASH_EPS", DBNull.Value);
        }
        if (!interimStockDividendTextBox.Text.Equals(""))
        {
            httable.Add("INTERIM_STOCK_DIVIDEND", Convert.ToDecimal(interimStockDividendTextBox.Text));
        }
        else
        {
            httable.Add("INTERIM_STOCK_DIVIDEND", DBNull.Value);
        }
        if (!interimCashDividendTextBox.Text.Equals(""))
        {
            httable.Add("INTERIM_CASH_DIVIDEND", Convert.ToDecimal(interimCashDividendTextBox.Text));
        }
        else
        {
            httable.Add("INTERIM_CASH_DIVIDEND", DBNull.Value);
        }
        if (!interimRecordDateFromTextBox.Text.Equals(""))
        {
            httable.Add("INTERIM_RECORD_DATE_FROM", Convert.ToDateTime(interimRecordDateFromTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("INTERIM_RECORD_DATE_FROM", DBNull.Value);
        }
        if (!interimRecordDateToTextBox.Text.Equals(""))
        {
            httable.Add("INTERIM_RECORD_DATE_TO", Convert.ToDateTime(interimRecordDateToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("INTERIM_RECORD_DATE_TO", DBNull.Value);
        }
        if (!shareOfDirectorTextBox.Text.Equals(""))
        {
            httable.Add("SHARE_HOLDER_DIR", Convert.ToDecimal(shareOfDirectorTextBox.Text));
        }
        else
        {
            httable.Add("SHARE_HOLDER_DIR", DBNull.Value);
        }
        if (!shareOfPublicTextBox.Text.Equals(""))
        {
            httable.Add("SHARE_HOLDER_PUB", Convert.ToDecimal(shareOfPublicTextBox.Text));
        }
        else
        {
            httable.Add("SHARE_HOLDER_PUB", DBNull.Value);
        }
        if (!shareOfGovtTextBox.Text.Equals(""))
        {
            httable.Add("SHARE_HOLDER_GOVT", Convert.ToDecimal(shareOfGovtTextBox.Text));
        }
        else
        {
            httable.Add("SHARE_HOLDER_GOVT", DBNull.Value);
        }
        if (!shareOfForeignTextBox.Text.Equals(""))
        {
            httable.Add("SHARE_HOLDER_FOREIGN", Convert.ToDecimal(shareOfForeignTextBox.Text));
        }
        else
        {
            httable.Add("SHARE_HOLDER_FOREIGN", DBNull.Value);
        }
        if (!shareOfInstitutionTextBox.Text.Equals(""))
        {
            httable.Add("SHARE_HOLDER_INSTITUTION", Convert.ToDecimal(shareOfInstitutionTextBox.Text));
        }
        else
        {
            httable.Add("SHARE_HOLDER_INSTITUTION", DBNull.Value);
        }
        if (!percentageGrothEpsTextBox.Text.Equals(""))
        {
            httable.Add("PERCENTAGE_GROTH_EPS", Convert.ToDecimal(percentageGrothEpsTextBox.Text));
        }
        else
        {
            httable.Add("PERCENTAGE_GROTH_EPS", DBNull.Value);
        }
        if (!percentageGrothCashEpsTextBox.Text.Equals(""))
        {
            httable.Add("PERCENTAGE_GROTH_CASH_EPS", Convert.ToDecimal(percentageGrothCashEpsTextBox.Text));
        }
        else
        {
            httable.Add("PERCENTAGE_GROTH_CASH_EPS", DBNull.Value);
        }
        if (!netProfitTextBox.Text.Equals(""))
        {
            httable.Add("NET_PROFIT", Convert.ToDecimal(netProfitTextBox.Text));
        }
        else
        {
            httable.Add("NET_PROFIT", DBNull.Value);
        }
        if (!totalEquityTextBox.Text.Equals(""))
        {
            httable.Add("TOTAL_EQUITY", Convert.ToDecimal(totalEquityTextBox.Text));
        }
        else
        {
            httable.Add("TOTAL_EQUITY", DBNull.Value);
        }
        if (!longTermDebtTextBox.Text.Equals(""))
        {
            httable.Add("LONG_TERM_DEBT", Convert.ToDecimal(longTermDebtTextBox.Text));
        }
        else
        {
            httable.Add("LONG_TERM_DEBT", DBNull.Value);
        }

        httable.Add("UPDATE_DATE", DateTime.Now);
        httable.Add("UPDATE_BY", LoginID);

        commonGatewayObj.Update(httable, "ANALYSIS_MST1", "comp_cd = " + companyNameDropDownList.SelectedValue);
        ClearFields();
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Updated Successfully');", true);
    }
}
