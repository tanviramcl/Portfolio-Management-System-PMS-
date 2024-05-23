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

public partial class UI_CashatBank : System.Web.UI.Page
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
        
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        DataTable dtBankDropDownList = dropDownListObj.BANKDROPDOWNLIST();

        if (!IsPostBack)
        {
         
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();

            DropDownListBank.DataSource = dtBankDropDownList;
            DropDownListBank.DataTextField = "BANK_NAME";
            DropDownListBank.DataValueField = "BANK_CODE";
            DropDownListBank.DataBind();
        }
       
    }

    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BankNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void addNewButton_Click(object sender, EventArgs e)
    {
        InsertData();
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        


    }
    private void InsertData()
    {
        Hashtable httable = new Hashtable();


        httable.Add("F_CD", fundNameDropDownList.SelectedValue.ToString());
        httable.Add("BANK_CODE", DropDownListBank.Text.ToString());
        if (!TextBox_ACC_NUMBER.Text.Equals(""))
        {
            httable.Add("ACCOUNT_NUMBER", TextBox_ACC_NUMBER.Text.ToString());
        }
        if (!TextBox1.Text.Equals(""))
        {
            httable.Add("NATURE_OF_ACCOUNT",TextBox1.Text.ToString());
        }
        if (!TextBoxRATE_OF_INTEREST.Text.Equals(""))
        {
            httable.Add("RATE_OF_INTEREST", TextBoxRATE_OF_INTEREST.Text.ToString());
        }

        if (!TextBox_AVAILABLE_BALANCE.Text.Equals(""))
        {
            httable.Add("AVAILABLE_BALANCE",TextBox_AVAILABLE_BALANCE.Text.ToString());
        }
        if (!TextBox_MARKET_VALUE.Text.Equals(""))
        {
            httable.Add("MARKET_VALUE", Convert.ToDecimal(TextBox_MARKET_VALUE.Text));
        }
        httable.Add("ENTRY_DATE", EntryDateTextBox.Text.ToString());
        commonGatewayObj.Insert(httable, "CASH_AT_BANK");
        ClearFields();
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);
    }
    private void SearchClearFields()
    {
      
      

    }

   
    private void ClearFields()
    {
      
        fundNameDropDownList.SelectedValue = "0";
        DropDownListBank.SelectedValue = "0";
        TextBox_ACC_NUMBER.Text = "";
        TextBox1.Text = "";
        TextBoxRATE_OF_INTEREST.Text = "";
        TextBox_AVAILABLE_BALANCE.Text = "";
        TextBox_MARKET_VALUE.Text = "";
        EntryDateTextBox.Text = "";
        

    }

    private void searchClearFields()
    {
       

    }

    protected void companyNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //updateButton.Visible = false;
        //addNewButton.Visible = true;
        ////companyCodeTextBox.Text = "";
        //searchClearFields();
        //string comp_cd = companyNameDropDownList.SelectedValue.ToString();

        //if (comp_cd != "")
        //{
        //    companyCodeTextBox.Text = companyNameDropDownList.SelectedValue.ToString();
        //    //financialYearTextBox.Text = "";
        //    //SearchClearFields();

        //    //financialYearTextBox.Focus();
        //}
        //else
        //{
        //    companyCodeTextBox.Text = "";
        //}
    }
    protected void clearButton_Click(object sender, EventArgs e)
    {

    }
    protected void updateButton_Click(object sender, EventArgs e)
    {
        //Hashtable httable = new Hashtable();
        //httable.Add("comp_cd", Convert.ToInt16(companyCodeTextBox.Text));
        //httable.Add("fy", financialYearTextBox.Text.ToString());
        //if (!recordDateTextBox.Text.Equals(""))
        //{
        //    httable.Add("RECORD_DT", Convert.ToDateTime(recordDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        //}
        //else
        //{
        //    httable.Add("RECORD_DT", DBNull.Value);
        //}
        //if (!bookToTextBox.Text.Equals(""))
        //{
        //    httable.Add("BOOK_TO", Convert.ToDateTime(bookToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        //}
        //else
        //{
        //    httable.Add("BOOK_TO", DBNull.Value);
        //}
        //if (!stockTextBox.Text.Equals(""))
        //{
        //    httable.Add("BONUS", Convert.ToDecimal(stockTextBox.Text));
        //}
        //else
        //{
        //    httable.Add("BONUS", DBNull.Value);
        //}

        //if (!rightApprovalDateTextBox.Text.Equals(""))
        //{
        //    httable.Add("RIGHT_APPR_DT", Convert.ToDateTime(rightApprovalDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        //}
        //else
        //{
        //    httable.Add("RIGHT_APPR_DT", DBNull.Value);
        //}

        //if (!rightTextBox.Text.Equals(""))
        //{
        //    httable.Add("RIGHT", Convert.ToDecimal(rightTextBox.Text));
        //}
        //else
        //{
        //    httable.Add("RIGHT", DBNull.Value);
        //}
        //if (!cashTextBox.Text.Equals(""))
        //{
        //    httable.Add("CASH", Convert.ToDecimal(cashTextBox.Text));
        //}
        //else
        //{
        //    httable.Add("CASH", DBNull.Value);
        //}
        //if (!agmDateTextBox.Text.Equals(""))
        //{
        //    httable.Add("AGM", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        //}
        //else
        //{
        //    httable.Add("AGM", DBNull.Value);
        //}
        //if (!remarksTextBox.Text.Equals(""))
        //{
        //    httable.Add("REMARKS", remarksTextBox.Text.ToString());
        //}
        //else
        //{
        //    httable.Add("REMARKS", null);
        //}
       
        //if (!postedTextBox.Text.Equals(""))
        //{
        //    httable.Add("POSTED", postedTextBox.Text.ToString());
        //}
        //else
        //{
        //    httable.Add("POSTED", null);
        //}
        //if (!postedDateTextBox.Text.Equals(""))
        //{
        //    httable.Add("PDATE", Convert.ToDateTime(postedDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        //}
        //else
        //{
        //    httable.Add("PDATE", DBNull.Value);
        //}
        //commonGatewayObj.Update(httable, "book_cl", "comp_cd = " + companyCodeTextBox.Text + "and fy = '" + financialYearTextBox.Text.ToString() + "'");
        //ClearFields();
        ////ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Updated Successfully');", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Updated Successfully.');", true);

    }
}
