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

public partial class UI_NonListedSecuritiesInvestmentEntryForm : System.Web.UI.Page
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
       
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        DataTable dtPortfolioAsOnDropDownList = BalanceDateDropDownList();
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();


            PortfolioAsOnDropDownList.DataSource = dtPortfolioAsOnDropDownList;
            PortfolioAsOnDropDownList.DataTextField = "Balance_Date";
            PortfolioAsOnDropDownList.DataValueField = "bal_dt_ctrl";
            PortfolioAsOnDropDownList.DataBind();

        }
    }
    protected void saveButton_Click(object sender, EventArgs e)
    {
       


        string LoginID = Session["UserID"].ToString();
        //string LoginName = Session["UserName"].ToString().ToUpper();
        Hashtable httable = new Hashtable();
        httable.Add("ID", Convert.ToInt32(pf1s1DAOObj.getMaxIDForNonListedSecurities() + 1));       
        if (!fundNameDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("F_CD", Convert.ToInt16(fundNameDropDownList.SelectedValue));
        }
        if (!amountTextBox.Text.Equals(""))
        {
            httable.Add("INV_AMOUNT", Convert.ToDouble(amountTextBox.Text));
        }
        httable.Add("INV_DATE", Convert.ToDateTime(PortfolioAsOnDropDownList.Text).ToString("dd-MMM-yyyy"));
        httable.Add("ENTRY_BY", LoginID);
        httable.Add("ENTRY_DATE", DateTime.Now);




        if (pf1s1DAOObj.IsDuplicateNonListedSecurities(Convert.ToInt32(fundNameDropDownList.SelectedValue.ToString()), Convert.ToDecimal(amountTextBox.Text.Trim().ToString()), Convert.ToDateTime(PortfolioAsOnDropDownList.Text.Trim().ToString()).ToString("dd-MMM-yyyy")))
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Save Failed: You Are Trying to Duplicate entry.');", true);
            string updateMsgNonlistedSecurites = UpdateAmmountInNonlisted(fundNameDropDownList.SelectedValue.ToString(), PortfolioAsOnDropDownList.Text.ToString(), amountTextBox.Text.ToString());

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Updated Successfully');", true);
            ClearFields();
        }
        else
        {
            commonGatewayObj.Insert(httable, "NON_LISTED_SECURITIES");
            ClearFields();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
        }
        fundNameDropDownList.Focus();
    }

    public DataTable BalanceDateDropDownList()//Get Howla Date from invest.fund_trans_hb Table
    {
        DataTable dtHowlaDate = commonGatewayObj.Select("select distinct bal_dt_ctrl from pfolio_bk order by bal_dt_ctrl desc");
        DataTable dtHowlaDateDropDownList = new DataTable();
        dtHowlaDateDropDownList.Columns.Add("Balance_Date", typeof(string));
        dtHowlaDateDropDownList.Columns.Add("bal_dt_ctrl", typeof(string));
        DataRow dr = dtHowlaDateDropDownList.NewRow();
        dr["Balance_Date"] = "--Select--";
        dr["bal_dt_ctrl"] = "0";
        dtHowlaDateDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtHowlaDate.Rows.Count; loop++)
        {
            dr = dtHowlaDateDropDownList.NewRow();
            dr["Balance_Date"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["bal_dt_ctrl"]).ToString("dd-MMM-yyyy");
            dr["bal_dt_ctrl"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["bal_dt_ctrl"]).ToString("dd-MMM-yyyy");
            dtHowlaDateDropDownList.Rows.Add(dr);
        }
        return dtHowlaDateDropDownList;
    }


 

    public string UpdateAmmountInNonlisted(string FundId, string Date,string Ammount)
    {

        CommonGateway commonGatewayObj = new CommonGateway();
       
        string strUpdateNonlisted = "";
        string message = "";
        if (FundId != "")
        {

            strUpdateNonlisted = "UPDATE NON_LISTED_SECURITIES SET INV_AMOUNT = '"+ Convert.ToDouble(Ammount) + "' WHERE F_CD = " + FundId+" and INV_DATE ='"+Date+ "'";

            int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUpdateNonlisted);
            if (NumOfRows > 0)
            {
                message = "Data Updated Successfully";
            }


        }

        return message;

    }

    public void ClearFields()
    {
        amountTextBox.Text = "";
      
    }
}
