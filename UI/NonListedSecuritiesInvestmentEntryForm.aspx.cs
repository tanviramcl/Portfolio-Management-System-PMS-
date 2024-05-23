using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
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
        if (!IsPostBack)
        {

            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }


            DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
            DataTable dtCompanyDropdownlist = dropDownListObj.NolistedCompanyCodeNameDropDownList();
            DataTable dtnonlistedCategoryDropdownlist = dropDownListObj.NolistedCategoryTypeDropDownList();
            DataTable dtPortfolioAsOnDropDownList = BalanceDateDropDownList();

            nonlistedCategoryDropDownList.Enabled = false;

           
          
            lblerror.Text = "";
            

            if (!IsPostBack)
            {
                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();

                nonlistedCompanyDropDownList.DataSource = dtCompanyDropdownlist;
                nonlistedCompanyDropDownList.DataTextField = "COMP_NM";
                nonlistedCompanyDropDownList.DataValueField = "COMP_CD";
                nonlistedCompanyDropDownList.DataBind();

                nonlistedCategoryDropDownList.DataSource = dtnonlistedCategoryDropdownlist;
                nonlistedCategoryDropDownList.DataTextField = "CAT_NM";
                nonlistedCategoryDropDownList.DataValueField = "CAT_ID";
                nonlistedCategoryDropDownList.DataBind();


            }


      
            if (string.IsNullOrEmpty(Request.QueryString["ID"]) && string.IsNullOrEmpty(Request.QueryString["COMP_CD"]) && string.IsNullOrEmpty(Request.QueryString["INV_DATE"]) && string.IsNullOrEmpty(Request.QueryString["TRAN_TP"]) )
            {
                // not there!
                Session["ALLFundFillNonListedSecuritiesGrid"] = ALLFundFillNonListedSecuritiesGrid();

                DataTable dtallNonlistedSecurityGrid = (DataTable)Session["ALLFundFillNonListedSecuritiesGrid"];
            }
            else
            {
                string fundCode = Convert.ToString(Request.QueryString["ID"]).Trim();
                string CompID = Convert.ToString(Request.QueryString["COMP_CD"]).Trim();
                string INV_DATE = Convert.ToString(Request.QueryString["INV_DATE"]).Trim();
                string TRAN_TP = Convert.ToString(Request.QueryString["TRAN_TP"]).Trim();
                DateTime dtimeInvestmentDate = Convert.ToDateTime(INV_DATE);
                string investmentdate = dtimeInvestmentDate.ToString("dd-MMM-yyyy");
                Session["updateNOlistedDetailsRow"] = NonListedSecuritiesDeltailsByid(fundCode, CompID, investmentdate, TRAN_TP);

                DataTable dtNonListedDetails = (DataTable)Session["updateNOlistedDetailsRow"];
                if (dtNonListedDetails.Rows.Count > 0)
                {
                    DateTime dtimeInvDate = Convert.ToDateTime(dtNonListedDetails.Rows[0]["INV_DATE"].ToString());
                    fundNameDropDownList.SelectedValue = dtNonListedDetails.Rows[0]["F_CD"].ToString();
                    fundNameDropDownList.Enabled = false;
                    nonlistedCompanyDropDownList.SelectedValue = dtNonListedDetails.Rows[0]["COMP_CD"].ToString();
                    //LabelInvestmentDate.Visible = false;
                    nonlistedCompanyDropDownList.Enabled = false;
                    InvestMentDateTextBox.Text = dtimeInvDate.ToString("dd/MM/yyyy");
                    
                    InvestMentDateTextBox.Visible = false;
                    DropDownListTranTP.SelectedValue = dtNonListedDetails.Rows[0]["TRAN_TP1"].ToString();
                    nonlistedCategoryDropDownList.SelectedValue = dtNonListedDetails.Rows[0]["CAT_ID"].ToString();
                    nonlistedCategoryDropDownList.Enabled = false;
                    //DropDownListTranTP.SelectedValue = dtNonListedDetails.Rows[0]["CAT_ID"].ToString();
                    amountTextBox.Text = dtNonListedDetails.Rows[0]["AMOUNT"].ToString();
                    rateTextBox.Text = dtNonListedDetails.Rows[0]["RATE"].ToString();
                    noOfShareTextBox.Text = dtNonListedDetails.Rows[0]["NO_SHARES"].ToString();
                    saveButton.Text = "Update";
                    

                }
        
            }

        }


      
    }

    public DataTable ALLFundFillNonListedSecuritiesGrid()
    {

        string strQuery;
        DataTable dt;

        strQuery = "select * from (Select tab1.F_CD,tab1.COMP_CD,tab2.COMP_NM,tab1.AMOUNT,tab1.RATE,tab1.NO_SHARES, tab1.INV_DATE , tab1.CAT_ID,tab1.CAT_NM," +
            " decode(tab1.TRAN_TP, 'B', 'Buy Shares',  'S', 'SALE OF INVESTMENT','I','Public Offer') AS TRAN_TP,tab1.TRAN_TP AS TRAN_TP1 from (SELECT  F_CD,COMP_CD,AMOUNT," +
            " RATE,NO_SHARES,INV_DATE ,  NLSD.CAT_ID,NC.CAT_NM,NLSD.TRAN_TP  FROM NON_LISTED_SECURITIES_DETAILS nlsd inner join  NONLISTED_CATEGORY "+
            " nc ON NLSD.CAT_ID = NC.CAT_ID  order by INV_DATE DESC ) tab1 left outer join COMP_NONLISTED tab2 ON tab1.COMP_CD=tab2.COMP_CD ) order by INV_DATE DESC";

        dt = commonGatewayObj.Select(strQuery);
        return dt;
    }
    public DataTable NonListedSecuritiesDeltailsByid(string fundCode, string CompID, string INV_DATE, string TRAN_TP)
    {

        string strQuery;
        DataTable dt;

        strQuery = "select * from (Select tab1.F_CD,tab1.COMP_CD,tab2.COMP_NM,tab1.AMOUNT,tab1.RATE,tab1.NO_SHARES, tab1.INV_DATE , tab1.CAT_ID,tab1.CAT_NM,decode(tab1.TRAN_TP, 'B', 'Buy Shares',  'S', 'SALE OF INVESTMENT','I','Public Offer') AS TRAN_TP,tab1.TRAN_TP AS TRAN_TP1 from (SELECT  F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,INV_DATE ," +
            "  NLSD.CAT_ID,NC.CAT_NM,NLSD.TRAN_TP  FROM NON_LISTED_SECURITIES_DETAILS nlsd inner join  NONLISTED_CATEGORY nc ON NLSD.CAT_ID = NC.CAT_ID  order by INV_DATE DESC ) tab1" +
            " left outer join COMP_NONLISTED tab2 ON tab1.COMP_CD=tab2.COMP_CD) Where F_CD = "+ fundCode +" and COMP_CD = "+ CompID + " and INV_DATE = '"+ INV_DATE.ToString() + "' and TRAN_TP1 = '" + TRAN_TP + "'";
        dt = commonGatewayObj.Select(strQuery);
        return dt;
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        // Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");

        if (string.IsNullOrEmpty(Request.QueryString["ID"]) && string.IsNullOrEmpty(Request.QueryString["COMP_CD"]) && string.IsNullOrEmpty(Request.QueryString["INV_DATE"]) && string.IsNullOrEmpty(Request.QueryString["TRAN_TP"]))
        {
            // not there!
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Please Select an row')", true);

        }
        else
        {
            string confirmValue = HiddenField2.Value;
            //string fundCode, CompID, INV_DATE, TRAN_TP, investmentdate, strDelQuery;
            //DateTime dtimeInvestmentDate;
            //int NumOfRows = 0;
            if (confirmValue == "Yes")
            {
                string fundCode = Convert.ToString(Request.QueryString["ID"]).Trim();
                string CompID = Convert.ToString(Request.QueryString["COMP_CD"]).Trim();
                string INV_DATE = Convert.ToString(Request.QueryString["INV_DATE"]).Trim();
                string TRAN_TP = Convert.ToString(Request.QueryString["TRAN_TP"]).Trim();
                DateTime dtimeInvestmentDate = Convert.ToDateTime(INV_DATE);
                string investmentdate = dtimeInvestmentDate.ToString("dd-MMM-yyyy");
                string strDelQuery = "delete from NON_LISTED_SECURITIES_DETAILS where comp_cd='" + CompID + "' and f_cd=" + fundCode + " and INV_DATE='" + investmentdate + "' and TRAN_TP='" + TRAN_TP + "'";
                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Delete Sucessfully')", true);
                Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");
            }
            else
            {
                Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");
            }

           
        


        //saveBu
        //}
    }

    }
    protected void resetButton_Click(object sender, EventArgs e)
    {
        string fundcode = fundNameDropDownList.SelectedValue.ToString();

        if (!string.IsNullOrEmpty(fundcode))
        {
            FillNonListedSecuritiesGrid();
            clearText();
        }
        else
        {
            Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");
        }

       
    }
   
    protected void saveButton_Click(object sender, EventArgs e)
    {

        string loginId = Session["UserID"].ToString();
        string invDate = InvestMentDateTextBox.Text.ToString();
       // DateTime dtINVDATE = DateTime.ParseExact(invDate, "dd/MM/yyyy", null);
        DateTime dtimeInvDate = Convert.ToDateTime(invDate);
        DataTable dtnonListedDetails = new DataTable();

       

        string strQuery = "Select F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,ENTRY_BY,ENTRY_DATE from NON_LISTED_SECURITIES_DETAILS where f_cd=" + fundNameDropDownList.SelectedValue.ToString() + " and COMP_CD=" + nonlistedCompanyDropDownList.SelectedValue.ToString() + " and INV_DATE='" + dtimeInvDate.ToString("dd-MMM-yyyy") + "' and CAT_ID='" + nonlistedCategoryDropDownList.SelectedValue.ToString() + "'";
          
        dtnonListedDetails = commonGatewayObj.Select(strQuery);
        if (dtnonListedDetails != null && dtnonListedDetails.Rows.Count > 0)
        {
              
                  
                string strUPQuery = "update NON_LISTED_SECURITIES_DETAILS SET COMP_CD ='" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "',AMOUNT =" + amountTextBox.Text.ToString() + ",RATE=" + rateTextBox.Text.ToString() + ",NO_SHARES =" + noOfShareTextBox.Text.ToString() + ",ENTRY_BY ='" + loginId + "',ENTRY_DATE ='" + DateTime.Now.ToString("dd-MMM-yyyy") + "', CAT_ID='" + nonlistedCategoryDropDownList.SelectedValue.ToString() + "',TRAN_TP='"+DropDownListTranTP.SelectedValue.ToString()+"' where F_CD='" + fundNameDropDownList.SelectedValue.ToString() + "' and  INV_DATE='" + dtimeInvDate.ToString("dd-MMM-yyyy") + "'  and comp_cd='" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "'";
                int NumOfRows3 = commonGatewayObj.ExecuteNonQuery(strUPQuery);
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update Sucessfully')", true);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Update Sucessfully')", true);

        }
        else
        {
                string strInsQuery;

                strInsQuery = "insert into NON_LISTED_SECURITIES_DETAILS(F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,INV_DATE,ENTRY_BY,ENTRY_DATE,CAT_ID,TRAN_TP)values('" + fundNameDropDownList.SelectedValue.ToString() + "','" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "','" + amountTextBox.Text.ToString() + "','" + rateTextBox.Text.ToString() + "','" + noOfShareTextBox.Text.ToString() + "','" + dtimeInvDate.ToString("dd-MMM-yyyy") + "','" + loginId + "','" + DateTime.Now.ToString("dd-MMM-yyyy") + "'," + nonlistedCategoryDropDownList.SelectedValue.ToString() + ",'"+DropDownListTranTP.SelectedValue.ToString()+"')";

                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
              
                  //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Insert Sucessfully')", true);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Insert Sucessfully .!!!')", true);
        }

        FillNonListedSecuritiesGrid();
        //Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");
        clearText();
     
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



    private void FillNonListedSecuritiesGrid()
    {
        
        string strQuery, strSqlForNonlistedDetails;
        DataTable dt, dtnonlistedDetails;
        
        strQuery = "Select tab1.F_CD,tab1.COMP_CD,tab2.COMP_NM,tab1.AMOUNT,tab1.RATE,tab1.NO_SHARES, tab1.INV_DATE , tab1.CAT_ID,tab1.CAT_NM,decode(tab1.TRAN_TP, 'B', 'Buy Shares',  'S', 'SALE OF INVESTMENT','I','Public Offer') AS TRAN_TP,tab1.TRAN_TP AS TRAN_TP1 from (SELECT  F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,INV_DATE," +
            "  NLSD.CAT_ID,NC.CAT_NM,NLSD.TRAN_TP  FROM NON_LISTED_SECURITIES_DETAILS nlsd inner join  NONLISTED_CATEGORY nc ON NLSD.CAT_ID = NC.CAT_ID  where F_CD="+fundNameDropDownList.SelectedValue.ToString()+"  order by INV_DATE DESC ) tab1" +
            " left outer join COMP_NONLISTED tab2 ON tab1.COMP_CD=tab2.COMP_CD ";

        try
        {

            dt = commonGatewayObj.Select(strQuery);
            if (dt != null && dt.Rows.Count > 0)
            {
                //    GridViewNonListedSecurities.Visible = true;
                //    GridViewNonListedSecurities.DataSource = dt;
                //    GridViewNonListedSecurities.DataBind();

                Session["ALLFundFillNonListedSecuritiesGrid"] = dt;


                strSqlForNonlistedDetails = "select sum(totammount) as totalAmmount ,sum(totshare) as totshare from (select decode(TRAN_TP,'B',NO_SHARES,'S',-NO_SHARES,'I',NO_SHARES)totshare, decode(TRAN_TP,'B',AMOUNT,'S',-AMOUNT,'I',AMOUNT)totammount," +
                                           " F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP  from (Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE," +
                                           "  CAT_ID, TRAN_TP from  NON_LISTED_SECURITIES_DETAILS where f_cd=" + fundNameDropDownList.SelectedValue.ToString() + ")) ";

                dtnonlistedDetails = commonGatewayObj.Select(strSqlForNonlistedDetails);

                if (dtnonlistedDetails.Rows.Count > 0)
                {
                    lblTotalAmmont.Visible = true;
                    lblsumTotalAmmount.Visible = true;
                    lblsumTotalAmmount.Text = dtnonlistedDetails.Rows[0]["totalAmmount"].ToString(); ;
                }



            }
            else
            {
                //GridViewNonListedSecurities.Visible = false;
              //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No data Found');", true);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('No data Found .!!!')", true);
                //Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");
            }


        }

        catch (Exception err)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('No data found')" + err.ToString(), true);
          
        }

    }



    protected void rateChange_TextChanged(object sender, EventArgs e)
    {

        if (amountTextBox.Text != "" && rateTextBox.Text != "")
        {
            double ammount = Convert.ToDouble(amountTextBox.Text);
            double rate = Convert.ToDouble(rateTextBox.Text);
            if (ammount >= rate)
            {
                double noOfShare = (ammount / rate);
                int share = Convert.ToInt32(noOfShare);

                noOfShareTextBox.Text = share.ToString();

            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(' Amount must be greater than Rate .!!!');", true);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Amount must be greater than Rate .!!!')", true);
                //  Label1.Text = "Amount must be greater than Rate .!!!";
            }
           
            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Ammount" + noOfShareTextBox.Text.ToString() + "');", true);
        }
        else
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Enter Ammount !');", true);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Please Enter Amount !')", true);
        }


    }



    [System.Web.Services.WebMethod]

    public static string RATEONCHANGE(string Ammount, string Rate)
    {
        string noofSare = "";
        if (Ammount != "" && Rate != "")
        {
            double ammount = Convert.ToDouble(Rate);
            double rate = Convert.ToDouble(Rate);
            if (ammount >= rate)
            {
                double noOfShare = (ammount / rate);
                int share = Convert.ToInt32(noOfShare);
                noofSare = share.ToString();
            }
            
           
           
        }

        return noofSare;

    }




    public void ClearFields()
    {
        amountTextBox.Text = "";

    }

    

   
  
    

    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillNonListedSecuritiesGrid();
        lblerror.Text = "";
       


    }

    protected void nonlistedCompanyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillNonListedSecuritiesGrid();

        string strQuery1 = "Select  COMP_CD,COMP_NM ,SECT_MAJ_CD,ADD1,ADD2,TEL,EMAIL, AUTH_CAP,PAID_CAP,CAT_TP from COMP_NONLISTED where COMP_CD=" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "";

        DataTable dtNonListedComp = commonGatewayObj.Select(strQuery1);
        if (dtNonListedComp != null && dtNonListedComp.Rows.Count > 0)
        {
            nonlistedCategoryDropDownList.SelectedValue = dtNonListedComp.Rows[0]["CAT_TP"].ToString();
        }

        InvestMentDateTextBox.Text = "";
        amountTextBox.Text = "";
        rateTextBox.Text = "";
        noOfShareTextBox.Text = "";
        lblerror.Text = "";
    }

    public void clearText()
    {
        nonlistedCompanyDropDownList.SelectedValue = "0";
        InvestMentDateTextBox.Text = "";
        nonlistedCategoryDropDownList.SelectedValue = "0";
        DropDownListTranTP.SelectedValue = "0";
        amountTextBox.Text = "";
        rateTextBox.Text = "";
        noOfShareTextBox.Text = "";
    }

    protected void amountTextBox_TextChanged(object sender, EventArgs e)
    {
        string  strSqlForNonlistedDetails;
        DataTable dtnonlistedDetails;
        Double amount = 0;
        Double totalamount = 0;
        string tranTp = "";

       
        tranTp = DropDownListTranTP.SelectedValue.ToString();
        if (!string.IsNullOrEmpty(tranTp))
        {
            if (tranTp == "S")
            {
                strSqlForNonlistedDetails = "select sum(totammount) as totalAmmount ,sum(totshare) as totshare from (select decode(TRAN_TP,'B',NO_SHARES,'S',-NO_SHARES,'I',NO_SHARES)totshare, decode(TRAN_TP,'B',AMOUNT,'S',-AMOUNT,'I',AMOUNT)totammount," +
                         " F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE, CAT_ID, TRAN_TP  from (Select F_CD, COMP_CD, AMOUNT, RATE, NO_SHARES, INV_DATE, ENTRY_BY, ENTRY_DATE," +
                         "  CAT_ID, TRAN_TP from  NON_LISTED_SECURITIES_DETAILS where f_cd=" + fundNameDropDownList.SelectedValue.ToString() + " and comp_cd=" + nonlistedCompanyDropDownList.SelectedValue.ToString() + ")) ";

                dtnonlistedDetails = commonGatewayObj.Select(strSqlForNonlistedDetails);

               

                if (dtnonlistedDetails != null && dtnonlistedDetails.Rows.Count > 0)
                {
                    string tm = dtnonlistedDetails.Rows[0]["totalAmmount"].ToString();
                    string am = amountTextBox.Text.ToString();
                    if (tm != "" && am != "")
                    {
                        totalamount = Convert.ToDouble(dtnonlistedDetails.Rows[0]["totalAmmount"].ToString());
                        amount = Convert.ToDouble(amountTextBox.Text.ToString());
                        if (amount > totalamount)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Amount must be less than " + totalamount + " !')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('This Company not entry yet')", true);
                        clearText();
                    }

                }
               
            }
          
        }

    }
}
