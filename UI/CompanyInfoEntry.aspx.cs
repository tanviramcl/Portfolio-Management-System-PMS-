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

public partial class UI_CompanyInfoEntry : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    Pf1s1DAO pf1s1DAOObj = new Pf1s1DAO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtSectorNameDropDownList = dropDownListObj.FillSectorDropDownList();
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        if (!IsPostBack)
        {
            sectorDropDownList.DataSource = dtSectorNameDropDownList;
            sectorDropDownList.DataTextField = "SECT_MAJ_NM";
            sectorDropDownList.DataValueField = "SECT_MAJ_CD";
            sectorDropDownList.DataBind();
            
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();
        }
    }
   
    protected void saveButton_Click(object sender, EventArgs e)
    {
        if (pf1s1DAOObj.IsCompCode(companyNameDropDownList.SelectedValue))
        {
            companyNameDropDownList.Focus();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed!! Company Info already Inserted.');", true);
        }
        else
        {
            string LoginID = Session["UserID"].ToString();
            string LoginName = Session["UserName"].ToString().ToUpper();

            Hashtable httable = new Hashtable();
            httable.Add("comp_cd", Convert.ToInt16(companyNameDropDownList.SelectedValue));
            if (!sectorDropDownList.SelectedValue.Equals("0"))
            {
                httable.Add("SECTOR_CD", Convert.ToInt16(sectorDropDownList.SelectedValue));
            }
            if (!lastYearEndTextBox.Text.Equals(""))
            {
                httable.Add("FINANCIAL_YEAR", Convert.ToString(lastYearEndTextBox.Text));
            }
            if (!authorizedCapitalTextBox.Text.Equals(""))
            {
                httable.Add("AUTHO_CAP", Convert.ToDecimal(authorizedCapitalTextBox.Text));
            }
            if (!paidupCapitalTextBox.Text.Equals(""))
            {
                httable.Add("PAIDUP_CAP", Convert.ToDecimal(paidupCapitalTextBox.Text));
            }
            if (!reserveAndSurplusTextBox.Text.Equals(""))
            {
                httable.Add("RESERVE_SURPLUS", Convert.ToDecimal(reserveAndSurplusTextBox.Text));
            }
            if (!faceValueTextBox.Text.Equals(""))
            {
                httable.Add("FACE_VAL", Convert.ToDecimal(faceValueTextBox.Text));
            }
            if (!totalNoOfSecuritiesTextBox.Text.Equals(""))
            {
                httable.Add("TOTAL_NO_SHARES", Convert.ToDecimal(totalNoOfSecuritiesTextBox.Text));
            }
            if (!marketLotTextBox.Text.Equals(""))
            {
                httable.Add("MARKET_LOT", Convert.ToDecimal(marketLotTextBox.Text));
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
            if (!secondQuarterTextBox.Text.Equals(""))
            {
                httable.Add("Q2_EPS", Convert.ToDecimal(secondQuarterTextBox.Text));
            }
            if (!thirdQuarterTextBox.Text.Equals(""))
            {
                httable.Add("Q3_EPS", Convert.ToDecimal(thirdQuarterTextBox.Text));
            }
            if (!fourthQuarterTextBox.Text.Equals(""))
            {
                httable.Add("Q4_EPS", Convert.ToDecimal(fourthQuarterTextBox.Text));
            }
            if (!navTextBox.Text.Equals(""))
            {
                httable.Add("NAV", Convert.ToDecimal(navTextBox.Text));
            }
            if (!stockDividendTextBox.Text.Equals(""))
            {
                httable.Add("STOCK_DIVIDEND", Convert.ToDecimal(stockDividendTextBox.Text));
            }
            if (!cashDividendTextBox.Text.Equals(""))
            {
                httable.Add("CASH_DIVIDEND", Convert.ToDecimal(cashDividendTextBox.Text));
            }
            if (!recordDateFromTextBox.Text.Equals(""))
            {
                httable.Add("RECORD_DATE_FROM", Convert.ToDateTime(recordDateFromTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!recordDateToTextBox.Text.Equals(""))
            {
                httable.Add("RECORD_DATE_TO", Convert.ToDateTime(recordDateToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!agmDateTextBox.Text.Equals(""))
            {
                httable.Add("AGM_DATE", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!rightShareTextBox.Text.Equals(""))
            {
                httable.Add("RIGHT_SHARE", Convert.ToDecimal(rightShareTextBox.Text));
            }
            if (!rightApprovalDateTextBox.Text.Equals(""))
            {
                httable.Add("RIGHT_APPROVAL_DATE", Convert.ToDateTime(rightApprovalDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!rightRecordDateFromTextBox.Text.Equals(""))
            {
                httable.Add("RIGHT_RECORD_DATE_FROM", Convert.ToDateTime(rightRecordDateFromTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!rightRecordDateToTextBox.Text.Equals(""))
            {
                httable.Add("RIGHT_RECORD_DATE_TO", Convert.ToDateTime(rightRecordDateToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!cashEpsTextBox.Text.Equals(""))
            {
                httable.Add("CASH_EPS", Convert.ToDecimal(cashEpsTextBox.Text));
            }
            if (!interimStockDividendTextBox.Text.Equals(""))
            {
                httable.Add("INTERIM_STOCK_DIVIDEND", Convert.ToDecimal(interimStockDividendTextBox.Text));
            }
            if (!interimCashDividendTextBox.Text.Equals(""))
            {
                httable.Add("INTERIM_CASH_DIVIDEND", Convert.ToDecimal(interimCashDividendTextBox.Text));
            }
            if (!interimRecordDateFromTextBox.Text.Equals(""))
            {
                httable.Add("INTERIM_RECORD_DATE_FROM", Convert.ToDateTime(interimRecordDateFromTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!interimRecordDateToTextBox.Text.Equals(""))
            {
                httable.Add("INTERIM_RECORD_DATE_TO", Convert.ToDateTime(interimRecordDateToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
            }
            if (!shareOfDirectorTextBox.Text.Equals(""))
            {
                httable.Add("SHARE_HOLDER_DIR", Convert.ToDecimal(shareOfDirectorTextBox.Text));
            }
            if (!shareOfPublicTextBox.Text.Equals(""))
            {
                httable.Add("SHARE_HOLDER_PUB", Convert.ToDecimal(shareOfPublicTextBox.Text));
            }
            if (!shareOfGovtTextBox.Text.Equals(""))
            {
                httable.Add("SHARE_HOLDER_GOVT", Convert.ToDecimal(shareOfGovtTextBox.Text));
            }
            if (!shareOfForeignTextBox.Text.Equals(""))
            {
                httable.Add("SHARE_HOLDER_FOREIGN", Convert.ToDecimal(shareOfForeignTextBox.Text));
            }
            if (!shareOfInstitutionTextBox.Text.Equals(""))
            {
                httable.Add("SHARE_HOLDER_INSTITUTION", Convert.ToDecimal(shareOfInstitutionTextBox.Text));
            }
            if (!percentageGrothEpsTextBox.Text.Equals(""))
            {
                httable.Add("PERCENTAGE_GROTH_EPS", Convert.ToDecimal(percentageGrothEpsTextBox.Text));
            }
            if (!percentageGrothCashEpsTextBox.Text.Equals(""))
            {
                httable.Add("PERCENTAGE_GROTH_CASH_EPS", Convert.ToDecimal(percentageGrothCashEpsTextBox.Text));
            }
            if (!netProfitTextBox.Text.Equals(""))
            {
                httable.Add("NET_PROFIT", Convert.ToDecimal(netProfitTextBox.Text));
            }
            if (!totalEquityTextBox.Text.Equals(""))
            {
                httable.Add("TOTAL_EQUITY", Convert.ToDecimal(totalEquityTextBox.Text));
            }
            if (!longTermDebtTextBox.Text.Equals(""))
            {
                httable.Add("LONG_TERM_DEBT", Convert.ToDecimal(longTermDebtTextBox.Text));
            }

            httable.Add("ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));
            httable.Add("ENTRY_BY", LoginID);

            commonGatewayObj.Insert(httable, "analysis_mst1");
            ClearFields();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
        }   
    }
    public void ClearFields()
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

    protected void resetButton_Click(object sender, EventArgs e)
    {

    }
}
