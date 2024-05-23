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
            //    DataTable dtNonlistedSecurities = NonlistedSecuritiesDetails();

            nonlistedCategoryDropDownList.Enabled = false;

            lblTotalAmmont.Visible = false;
            lblProcessing.Text = "";
            lblerror.Text = "";
            //grdShowDSEMP.DataSource = dtNonlistedSecurities;
            //grdShowDSEMP.DataBind();

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
        }
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        //  GridViewRow row = GridViewNonListedSecurities.Rows[e.NewSelectedIndex];
        lblProcessing.Text = "";
        string confirmValue = HiddenField2.Value;
        if (confirmValue == "Yes")
        {

            GridViewRow row = (GridViewRow)GridViewNonListedSecurities.Rows[e.RowIndex];

            string companyCode = row.Cells[1].Text.ToString();
            string fCd = row.Cells[0].Text.ToString();

            double amount = Convert.ToDouble(row.Cells[3].Text.ToString());

            string strQuery1 = "Select  TO_CHAR(max(INV_DATE), 'DD-MON-YYYY')INV_DATE  from NON_LISTED_SECURITIES where F_CD=" + fCd + "";

            DataTable dtNonListedDetailsMAXInV_DATE = commonGatewayObj.Select(strQuery1);

            string maxInvDate = dtNonListedDetailsMAXInV_DATE.Rows[0]["INV_DATE"].ToString();




            string strQuery2 = "Select  * from NON_LISTED_SECURITIES where F_CD=" + fCd + " and INV_DATE='" + maxInvDate + "' ";

            DataTable dtNonlistedSecuritiesMaxInvDate = commonGatewayObj.Select(strQuery2);

            if (dtNonlistedSecuritiesMaxInvDate.Rows.Count > 0)
            {
                Double FinalAmount = Convert.ToDouble(dtNonlistedSecuritiesMaxInvDate.Rows[0]["INV_AMOUNT"].ToString()) - amount;

                if (FinalAmount >= 0)
                {
                    string strUpdateNonlisted = "UPDATE NON_LISTED_SECURITIES SET INV_AMOUNT = '" + Convert.ToDouble(FinalAmount) + "' WHERE F_CD = " + fCd + " and INV_DATE ='" + maxInvDate + "'";

                    int NumOfRows2 = commonGatewayObj.ExecuteNonQuery(strUpdateNonlisted);
                }
                string strDelQuery = "delete from NON_LISTED_SECURITIES_DETAILS where comp_cd='" + companyCode + "' and f_cd=" + fCd + " and AMOUNT=" + amount + "";
                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);
                FillNonListedSecuritiesGrid();

            }
            


        }
        else
        {
            lblProcessing.Text = "Delete  unsuccessful";
        }

    }

    protected void saveButton_Click(object sender, EventArgs e)
    {

        string pfolioMaxDate = "";
        string loginId = Session["UserID"].ToString();
        string invDate = InvestMentDateTextBox.Text.ToString();
        DataTable dtNonlistedMaxDate = commonGatewayObj.Select("Select TO_CHAR(MAX(INV_DATE), 'DD-MON-YYYY ') as INV_DATE   from NON_LISTED_SECURITIES where f_cd=" + fundNameDropDownList.SelectedValue.ToString() + "");
        if (dtNonlistedMaxDate != null && dtNonlistedMaxDate.Rows.Count > 0)
        {
            pfolioMaxDate = dtNonlistedMaxDate.Rows[0]["INV_DATE"].ToString();
        }
        DateTime dtNonlistedDate = DateTime.Parse(pfolioMaxDate);
        DateTime dtINVDATE = DateTime.ParseExact(invDate, "dd/MM/yyyy", null);

        if (dtNonlistedDate.Date > dtINVDATE.Date)
        {
            // lblerror.Text = "";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Investment date must be greater than or equal to : " + dtNonlistedDate.ToString("dd-MMM-yyyy") + "');", true);
        }
        else
        {
            //It's an earlier or equal date


            DataTable dtnonListedDetails = new DataTable();
            //  string strQuery = "Select F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,ENTRY_BY,ENTRY_DATE from NON_LISTED_SECURITIES_DETAILS where f_cd=" + fundNameDropDownList.SelectedValue.ToString() + " and COMP_CD="+ nonlistedCompanyDropDownList.SelectedValue.ToString() + " and INV_DATE='" + dtINVDATE.ToString("dd-MMM-yyyy") + "' and CAT_ID='"+nonlistedCategoryDropDownList.SelectedValue.ToString()+"'";
            string strQuery = "Select F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,INV_DATE,ENTRY_BY,ENTRY_DATE from NON_LISTED_SECURITIES_DETAILS where f_cd=" + fundNameDropDownList.SelectedValue.ToString() + " and COMP_CD=" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "";
            dtnonListedDetails = commonGatewayObj.Select(strQuery);
            if (dtnonListedDetails != null && dtnonListedDetails.Rows.Count > 0)
            {
                //DateTime invDateNonlisted = DateTime.Parse(dtnonListedDetails.Rows[0]["INV_DATE"].ToString());
                DateTime invDateNonlisted = DateTime.Parse(dtnonListedDetails.Rows[0]["INV_DATE"].ToString());
                //  DateTime invDateNonlisted = Convert.ToDateTime() ;

                if (invDateNonlisted <= dtINVDATE)
                {
                    string strInsQuery2;

                    if (invDateNonlisted < dtINVDATE)
                    {
                        strInsQuery2 = "insert into NON_LISTED_SECURITIES_DETAILS(F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,INV_DATE,ENTRY_BY,ENTRY_DATE,CAT_ID)values('" + fundNameDropDownList.SelectedValue.ToString() + "','" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "','" + amountTextBox.Text.ToString() + "','" + rateTextBox.Text.ToString() + "','" + noOfShareTextBox.Text.ToString() + "','" + dtINVDATE.ToString("dd-MMM-yyyy") + "','" + loginId + "','" + DateTime.Now.ToString("dd-MMM-yyyy") + "'," + nonlistedCategoryDropDownList.SelectedValue.ToString() + ")";
                        int NumOfRows2 = commonGatewayObj.ExecuteNonQuery(strInsQuery2);
                        lblProcessing.Text = "Insert Sucessfully";
                        //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Insert Sucessfully')", true);
                    }
                    else if (invDateNonlisted == dtINVDATE)
                    {
                        string strUPQuery = "update NON_LISTED_SECURITIES_DETAILS SET COMP_CD ='" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "',AMOUNT =" + amountTextBox.Text.ToString() + ",RATE=" + rateTextBox.Text.ToString() + ",NO_SHARES =" + noOfShareTextBox.Text.ToString() + ",ENTRY_BY ='" + loginId + "',ENTRY_DATE ='" + DateTime.Now.ToString("dd-MMM-yyyy") + "', CAT_ID='" + nonlistedCategoryDropDownList.SelectedValue.ToString() + "' where F_CD='" + fundNameDropDownList.SelectedValue.ToString() + "' and  INV_DATE='" + dtINVDATE.ToString("dd-MMM-yyyy") + "'  and comp_cd='" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "'";
                        int NumOfRows3 = commonGatewayObj.ExecuteNonQuery(strUPQuery);
                        lblProcessing.Text = "Update Sucessfully";
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update Sucessfully')", true);
                    }
                }
                else
                {
                    lblerror.Text = "Investment date must be greater than or equal to existing date";
                    //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Investment date must be greater than existing Date')", true);
                }

            }
            else
            {
                string strInsQuery;

                strInsQuery = "insert into NON_LISTED_SECURITIES_DETAILS(F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,INV_DATE,ENTRY_BY,ENTRY_DATE,CAT_ID)values('" + fundNameDropDownList.SelectedValue.ToString() + "','" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "','" + amountTextBox.Text.ToString() + "','" + rateTextBox.Text.ToString() + "','" + noOfShareTextBox.Text.ToString() + "','" + dtINVDATE.ToString("dd-MMM-yyyy") + "','" + loginId + "','" + DateTime.Now.ToString("dd-MMM-yyyy") + "'," + nonlistedCategoryDropDownList.SelectedValue.ToString() + ")";

                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                lblProcessing.Text = "Insert Sucessfully";
                //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Insert Sucessfully')", true);

            }
            //  Response.Redirect("NonListedSecurities.aspx");
            FillNonListedSecuritiesGrid();
            clearText();
        }


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


        string strQuery, strQueryMaxinvDate, invDate, strQuery2, prevInvDate;
        DataTable dt, dtMaxInvDate, dtNonListedMAXInV_DATE;

        strQueryMaxinvDate = "select TO_CHAR(max(INV_DATE), 'DD-MON-YYYY')inv_date from NON_LISTED_SECURITIES_DETAILS where f_cd=" + fundNameDropDownList.SelectedValue.ToString();
        dtMaxInvDate = commonGatewayObj.Select(strQueryMaxinvDate);

        if (!dtMaxInvDate.Rows[0].IsNull("inv_date"))
        {
            invDate = dtMaxInvDate.Rows[0]["inv_date"].ToString();
            //  invDate = Convert.ToDateTime(dt.Rows[0]["vch_dt"].ToString()).ToString("dd-MMM-yyyy");
        }
        else
        {
            invDate = "01-Jan-1970";
        }


        strQuery2 = "Select  TO_CHAR(max(PREV_MAX_INV_DATE), 'DD-MON-YYYY')PREV_MAX_INV_DATE  from NON_LISTED_SECURITIES where F_CD=" + fundNameDropDownList.SelectedValue.ToString() + "";

        dtNonListedMAXInV_DATE = commonGatewayObj.Select(strQuery2);


        if (!dtNonListedMAXInV_DATE.Rows[0].IsNull("PREV_MAX_INV_DATE"))
        {
            prevInvDate = dtNonListedMAXInV_DATE.Rows[0]["PREV_MAX_INV_DATE"].ToString();
            //  invDate = Convert.ToDateTime(dt.Rows[0]["vch_dt"].ToString()).ToString("dd-MMM-yyyy");
        }
        else
        {
            prevInvDate = "";
        }



        strQuery = "Select tab1.F_CD,tab1.COMP_CD,tab2.COMP_NM,tab1.AMOUNT,tab1.RATE,tab1.NO_SHARES, tab1.INV_DATE , tab1.CAT_ID,tab1.CAT_NM  from (SELECT  F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,TO_CHAR(INV_DATE, 'DD-MON-YYYY') as  INV_DATE , NLSD.CAT_ID,NC.CAT_NM FROM NON_LISTED_SECURITIES_DETAILS nlsd" +
            " inner join  NONLISTED_CATEGORY nc ON NLSD.CAT_ID = NC.CAT_ID  where F_CD=" + fundNameDropDownList.SelectedValue.ToString() + " and inv_date between '" + dtNonListedMAXInV_DATE.Rows[0]["PREV_MAX_INV_DATE"].ToString() + "' and '" + dtMaxInvDate.Rows[0]["inv_date"].ToString() + "') tab1 left outer join COMP_NONLISTED tab2 ON tab1.COMP_CD=tab2.COMP_CD";

        try
        {

            dt = commonGatewayObj.Select(strQuery);
            if (dt != null && dt.Rows.Count > 0)
            {
                GridViewNonListedSecurities.Visible = true;
                GridViewNonListedSecurities.DataSource = dt;
                GridViewNonListedSecurities.DataBind();


                decimal totalammount = dt.AsEnumerable().Sum(row => row.Field<decimal>("AMOUNT"));
                lblTotalAmmount.Text = totalammount.ToString();

                lblInvDate.Text = invDate.ToString();



                btnProcess.Visible = true;
                LabelINVDate.Visible = true;
                lblInvDate.Visible = true;
                lblTotalAmmont.Visible = true;
                lblTotalAmmount.Visible = true;

            }
            else
            {
                GridViewNonListedSecurities.Visible = false;
                btnProcess.Visible = false;
                lblTotalAmmont.Visible = false;
                lblTotalAmmount.Visible = false;
                LabelINVDate.Visible = false;
                lblInvDate.Visible = false;
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
            if (ammount > rate)
            {
                double noOfShare = (ammount / rate);
                int share = Convert.ToInt32(noOfShare);

                noOfShareTextBox.Text = share.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(' Amount must be greater than Rate .!!!');", true);
                Label1.Text = "Amount must be greater than Rate .!!!";
            }
           
            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Ammount" + noOfShareTextBox.Text.ToString() + "');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Enter Ammount !');", true);
        }


    }

    public string UpdateAmmountInNonlisted(string FundId, string Date, string Ammount)
    {

        CommonGateway commonGatewayObj = new CommonGateway();

        string strUpdateNonlisted = "";
        string message = "";
        if (FundId != "")
        {

            strUpdateNonlisted = "UPDATE NON_LISTED_SECURITIES SET INV_AMOUNT = '" + Convert.ToDouble(Ammount) + "' WHERE F_CD = " + FundId + " and INV_DATE ='" + Date + "'";

            int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUpdateNonlisted);
            if (NumOfRows > 0)
            {
                message = "Data Updated Successfully !!!";
            }


        }

        return message;

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


    public DataTable NonlistedSecuritiesDetails()
    {
        DataTable dtNonlistedDetails = commonGatewayObj.Select("  SELECT  F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,TO_CHAR(INV_DATE, 'DD-MON-YYYY') as  INV_DATE,ENTRY_BY,  TO_CHAR (ENTRY_DATE, 'DD-MON-YYYY') as  ENTRY_DATE  FROM NON_LISTED_SECURITIES_DETAILS   ");
        return dtNonlistedDetails;
    }
    public DataTable NonlistedSecuritiesDetailsByCOMPID(string COMPCODE)
    {
        DataTable dtNonlistedDetails = commonGatewayObj.Select("  SELECT  F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,TO_CHAR(INV_DATE, 'DD-MON-YYYY') as  INV_DATE,ENTRY_BY,  TO_CHAR (ENTRY_DATE, 'DD-MON-YYYY') as  ENTRY_DATE  FROM NON_LISTED_SECURITIES_DETAILS  where comp_cd=" + COMPCODE + " ");
        return dtNonlistedDetails;
    }

    public void ClearFields()
    {
        amountTextBox.Text = "";

    }

    protected void GridViewNonListedSecurities_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewNonListedSecurities.PageIndex = e.NewPageIndex;
        FillNonListedSecuritiesGrid();
    }


    protected void GridViewNonListedSecurities_SelectedIndexChanged(object sender, EventArgs e)
    {
        nonlistedCompanyDropDownList.SelectedIndex = -1;
        string strQuery;
        DataTable dtNonListedDetails;


        strQuery = "select * from (SELECT  F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,TO_CHAR(INV_DATE, 'DD-MON-YYYY') as  INV_DATE,nlsd.CAT_ID,NLC.CAT_NM FROM NON_LISTED_SECURITIES_DETAILS nlsd inner join NONLISTED_CATEGORY nlc ON NLSD.CAT_ID =NLC.CAT_ID) " +
           " where F_CD=" + GridViewNonListedSecurities.SelectedRow.Cells[0].Text.ToString() + " and inv_date='" + GridViewNonListedSecurities.SelectedRow.Cells[6].Text.ToString() +
           "' and comp_cd=" + GridViewNonListedSecurities.SelectedRow.Cells[1].Text.ToString();


        dtNonListedDetails = commonGatewayObj.Select(strQuery);

        DateTime dtimeInvDate = Convert.ToDateTime(dtNonListedDetails.Rows[0]["INV_DATE"].ToString());


        fundNameDropDownList.SelectedValue = dtNonListedDetails.Rows[0]["F_CD"].ToString();
        nonlistedCompanyDropDownList.SelectedValue = dtNonListedDetails.Rows[0]["COMP_CD"].ToString();
        InvestMentDateTextBox.Text = dtimeInvDate.ToString("dd/MM/yyyy");
        nonlistedCategoryDropDownList.SelectedValue = dtNonListedDetails.Rows[0]["CAT_ID"].ToString();
        amountTextBox.Text = dtNonListedDetails.Rows[0]["AMOUNT"].ToString();
        rateTextBox.Text = dtNonListedDetails.Rows[0]["RATE"].ToString();
        noOfShareTextBox.Text = dtNonListedDetails.Rows[0]["NO_SHARES"].ToString();

    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        string confirmValue = HiddenField1.Value;
        if (confirmValue == "Yes")
        {

            string strQuery1, strQuery2, strQuery3, strQuery4, strQuery5, strQueryMaxinvDate, strQuery6;
            string loginId = Session["UserID"].ToString();
            DataTable dtNonListedDetailsMAXInV_DATE, dtMaxInvDate;
            DataTable dtNonListedMAXInV_DATE;
            DataTable dtNonlistedSecuritiesMaxInvDate;

            DataTable dtNonListedTotalSumofAmmount;

            DataTable dtNonListedID;



            strQuery1 = "Select  MAX(INV_DATE) as INV_DATE from NON_LISTED_SECURITIES_DETAILS where F_CD=" + fundNameDropDownList.SelectedValue.ToString() + "";

            dtNonListedDetailsMAXInV_DATE = commonGatewayObj.Select(strQuery1);

            string invDate = InvestMentDateTextBox.Text.ToString();
            //DateTime dtINVDATE = DateTime.ParseExact(invDate, "dd/MM/yyyy", null);

            strQuery2 = "Select  MAX(PREV_MAX_INV_DATE) as PREV_MAX_INV_DATE  from NON_LISTED_SECURITIES where F_CD=" + fundNameDropDownList.SelectedValue.ToString() + "";

            dtNonListedMAXInV_DATE = commonGatewayObj.Select(strQuery2);

            strQueryMaxinvDate = "select TO_CHAR(max(INV_DATE), 'DD-MON-YYYY')inv_date from NON_LISTED_SECURITIES where f_cd=" + fundNameDropDownList.SelectedValue.ToString();
            dtMaxInvDate = commonGatewayObj.Select(strQueryMaxinvDate);

            if (!dtMaxInvDate.Rows[0].IsNull("inv_date"))
            {
                invDate = dtMaxInvDate.Rows[0]["inv_date"].ToString();
                //  invDate = Convert.ToDateTime(dt.Rows[0]["vch_dt"].ToString()).ToString("dd-MMM-yyyy");
            }
            else
            {
                invDate = "01-Jan-1970";
            }




            strQuery3 = "Select  sum(AMOUNT) as Ammount  from NON_LISTED_SECURITIES_DETAILS where F_CD=" + fundNameDropDownList.SelectedValue.ToString() + " and INV_DATE BETWEEN  '" + Convert.ToDateTime(dtNonListedMAXInV_DATE.Rows[0]["PREV_MAX_INV_DATE"].ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(dtNonListedDetailsMAXInV_DATE.Rows[0]["INV_DATE"].ToString()).ToString("dd-MMM-yyyy") + "'";

            dtNonListedTotalSumofAmmount = commonGatewayObj.Select(strQuery3);




            strQuery4 = "Select  MAX(INV_DATE) as INV_DATE from NON_LISTED_SECURITIES where F_CD=" + fundNameDropDownList.SelectedValue.ToString() + "";

            dtNonlistedSecuritiesMaxInvDate = commonGatewayObj.Select(strQuery4);

            strQuery5 = "Select  MAX(ID)+1 as ID from NON_LISTED_SECURITIES";

            dtNonListedID = commonGatewayObj.Select(strQuery5);

            DateTime NonListedDetailsMAXINVDAte = Convert.ToDateTime(dtNonListedDetailsMAXInV_DATE.Rows[0]["INV_DATE"].ToString());
            DateTime nonListedSecuritiesINVMaxDate = Convert.ToDateTime(dtNonlistedSecuritiesMaxInvDate.Rows[0]["INV_DATE"].ToString());


            strQuery6 = "select max(inv_date) as inv_date from (Select tab1.F_CD,tab1.COMP_CD,tab2.COMP_NM,tab1.AMOUNT,tab1.RATE,tab1.NO_SHARES, tab1.INV_DATE , tab1.CAT_ID,tab1.CAT_NM  from (SELECT  F_CD,COMP_CD,AMOUNT,RATE,NO_SHARES,TO_CHAR(INV_DATE, 'DD-MON-YYYY') as  INV_DATE , NLSD.CAT_ID,NC.CAT_NM FROM NON_LISTED_SECURITIES_DETAILS nlsd" +
           " inner join  NONLISTED_CATEGORY nc ON NLSD.CAT_ID = NC.CAT_ID  where F_CD=" + fundNameDropDownList.SelectedValue.ToString() + " and inv_date between '" + Convert.ToDateTime(dtNonListedMAXInV_DATE.Rows[0]["PREV_MAX_INV_DATE"].ToString()).ToString("dd-MMM-yyyy") + "' and '" + Convert.ToDateTime(dtMaxInvDate.Rows[0]["inv_date"].ToString()).ToString("dd-MMM-yyyy") + "') tab1 left outer join COMP_NONLISTED tab2 ON tab1.COMP_CD=tab2.COMP_CD)";

            DataTable dt = commonGatewayObj.Select(strQuery6);

            if (nonListedSecuritiesINVMaxDate < NonListedDetailsMAXINVDAte)
            {
                string strInsQuery;
                DateTime dtimeCurrentDateTimeForLog = DateTime.Now;
                string strCurrentDateTimeForLog = dtimeCurrentDateTimeForLog.ToString("dd-MMM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
                // (TO_DATE('2003/05/03 21:02:44', 'yyyy/mm/dd hh24:mi:ss'));

                //  strInsQuery = "insert into NON_LISTED_SECURITIES(ID,F_CD,INV_AMOUNT,INV_DATE,ENTRY_BY,ENTRY_DATE,PREV_MAX_INV_DATE)values('" + dtNonListedID.Rows[0]["ID"].ToString() + "','"+fundNameDropDownList.SelectedValue.ToString()+"','"+ dtNonListedTotalSumofAmmount.Rows[0]["Ammount"].ToString() + "','"+ Convert.ToDateTime(dtNonListedDetailsMAXInV_DATE.Rows[0]["INV_DATE"]).ToString("dd-MMM-yyyy") + "','"+ loginId + "','"+ strCurrentDateTimeForLog + "','"+ Convert.ToDateTime(dtNonListedMAXInV_DATE.Rows[0]["PREV_MAX_INV_DATE"].ToString()).ToString("dd-MMM-yyyy") + "')";
                strInsQuery = "insert into NON_LISTED_SECURITIES(ID,F_CD,INV_AMOUNT,INV_DATE,ENTRY_BY,ENTRY_DATE,PREV_MAX_INV_DATE)values(" + dtNonListedID.Rows[0]["ID"].ToString() + "," + fundNameDropDownList.SelectedValue.ToString() + ",'" + dtNonListedTotalSumofAmmount.Rows[0]["Ammount"].ToString() + "','" + Convert.ToDateTime(dtNonListedDetailsMAXInV_DATE.Rows[0]["INV_DATE"]).ToString("dd-MMM-yyyy") + "','" + loginId + "', '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ,'" + Convert.ToDateTime(dtMaxInvDate.Rows[0]["inv_date"].ToString()).ToString("dd-MMM-yyyy") + "')";

                int NumOfRowsInsert = commonGatewayObj.ExecuteNonQuery(strInsQuery);

                lblProcessing.Text = "Processing completed!!!!";
                //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Insert Sucessfully')", true);
            }
            else
            {
                string strUpdateNonlisted = "UPDATE NON_LISTED_SECURITIES SET INV_AMOUNT = '" + Convert.ToDouble(dtNonListedTotalSumofAmmount.Rows[0]["Ammount"].ToString()) + "' WHERE F_CD = " + fundNameDropDownList.SelectedValue.ToString() + " and INV_DATE='" + dt.Rows[0]["inv_date"].ToString() + "' ";

                int NumOfRowsUpdate = commonGatewayObj.ExecuteNonQuery(strUpdateNonlisted);
                lblProcessing.Text = "Processing completed!!!!";

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update Sucessfully')", true);

            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        }



    }

    protected void GridViewNonListedSecurities_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridViewNonListedSecurities, "Select$" + e.Row.RowIndex);
        }
    }

    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillNonListedSecuritiesGrid();
        lblerror.Text = "";
        lblProcessing.Text = "";



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
        lblProcessing.Text = "";
        lblerror.Text = "";
    }

    public void clearText()
    {
        nonlistedCompanyDropDownList.SelectedValue = "0";
        InvestMentDateTextBox.Text = "";
        nonlistedCategoryDropDownList.SelectedValue = "0";
        amountTextBox.Text = "";
        rateTextBox.Text = "";
        noOfShareTextBox.Text = "";
    }
}
