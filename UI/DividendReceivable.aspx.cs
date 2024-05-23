using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UI_BookCloserEntry : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        if (!IsPostBack)
        {
            fundDropDownList.DataSource = dtFundNameDropDownList;
            fundDropDownList.Enabled = false;
            fundDropDownList.DataTextField = "F_NAME";
            fundDropDownList.DataValueField = "F_CD";
            fundDropDownList.DataBind();

            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.Enabled = false;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            agmDateTextBox.ReadOnly = true;
            recordDateTextBox.ReadOnly = true;

            if (string.IsNullOrEmpty(Request.QueryString["F_CD"]) && string.IsNullOrEmpty(Request.QueryString["COMP_CD"]) && string.IsNullOrEmpty(Request.QueryString["AGM_DATE"]) && string.IsNullOrEmpty(Request.QueryString["RECORD_DT"]))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please select');", true);
            }
            else
            {
                string fundCode = Convert.ToString(Request.QueryString["F_CD"]).Trim();
                string CompID = Convert.ToString(Request.QueryString["COMP_CD"]).Trim();
                string AGM_DATE = Convert.ToString(Request.QueryString["AGM_DATE"]).Trim();
                string RECORD_DT = Convert.ToString(Request.QueryString["RECORD_DT"]).Trim();
                DateTime dtimeAGM_DATE = Convert.ToDateTime(AGM_DATE);
                string AGMDATE = dtimeAGM_DATE.ToString("dd-MMM-yyyy");
                DateTime dtimeRECORD_DT = Convert.ToDateTime(RECORD_DT);
                string RECORDDATE = dtimeRECORD_DT.ToString("dd-MMM-yyyy");
                DataTable dt_RECEIVABLECASH_DIVIDEND = get_dividend(fundCode, CompID, AGMDATE, RECORDDATE);

                FundCodeTextBox.Text = fundCode;
                fundDropDownList.SelectedValue = fundCode;
                companyNameDropDownList.SelectedValue = CompID;
                companyCodeTextBox.Text = CompID;
                agmDateTextBox.Text = AGMDATE;
                recordDateTextBox.Text = RECORDDATE;
                NoofSahreTextBox.Text = dt_RECEIVABLECASH_DIVIDEND.Rows[0]["TOT_NOS"].ToString();
                gross_dividend_TextBox.Text = dt_RECEIVABLECASH_DIVIDEND.Rows[0]["GROSS_DIVIDEND"].ToString();
                Tax_TextBox.Text = dt_RECEIVABLECASH_DIVIDEND.Rows[0]["TAX"].ToString();
                Net_dividend_TextBox.Text = dt_RECEIVABLECASH_DIVIDEND.Rows[0]["NET_DIVIDEND"].ToString();

                DataTable dtDividendPosted = ISDividendPosted(fundCode, CompID, AGMDATE, RECORDDATE);

                if (dtDividendPosted.Rows.Count > 0)
                {
                    TextBox_divi_receivable.Text = dtDividendPosted.Rows[0]["DIVI_RECEIVAABLE"].ToString();
                    DueExcessTextBox.Text = dtDividendPosted.Rows[0]["DUE_EXCESS_DIVIDEND"].ToString();
                    TextBox_divi_receivable_date.Text = Convert.ToDateTime(dtDividendPosted.Rows[0]["DIVI_RECEIVE_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    TextBox_RemarksDividend_Receive.Text = dtDividendPosted.Rows[0]["DIVI_REMARKS"].ToString();

                    addNewButton.Visible = false;
                    updateButton.Visible = true;


                }



            }


        }



      
        
       
    }
    protected void addNewButton_Click(object sender, EventArgs e)
    {
        InsertData();
    }

    private DataTable get_id()
    {
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();

        sbMst.Append("  select max(Sl)+1 as Sl from RECEIVABLECASH_DIVIDEND ");
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        return dtReprtSource;
    }
        
   
    private void InsertData()
    {
        Hashtable httable = new Hashtable();
        DataTable dtID = get_id();

        if (dtID.Rows.Count > 0)
        {
            httable.Add("SL", Convert.ToInt16(dtID.Rows[0]["Sl"].ToString()));
        }
        else
        {
            httable.Add("SL", Convert.ToInt16(1));
        }


           
        httable.Add("F_CD", Convert.ToInt16(FundCodeTextBox.Text));
        httable.Add("comp_cd", Convert.ToInt16(companyCodeTextBox.Text));

       // httable.Add("fy", financialYearTextBox.Text.ToString());
        if (!recordDateTextBox.Text.Equals(""))
        {
            httable.Add("RECORD_DT", Convert.ToDateTime(recordDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!agmDateTextBox.Text.Equals(""))
        {
            httable.Add("AGM", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!NoofSahreTextBox.Text.Equals(""))
        {
            httable.Add("TOT_NOS", NoofSahreTextBox.Text);
        }

        if (!gross_dividend_TextBox.Text.Equals(""))
        {
            httable.Add("GROSS_DIVIDEND", Convert.ToDecimal(gross_dividend_TextBox.Text.ToString()));
        }
        if (!Tax_TextBox.Text.Equals(""))
        {
            httable.Add("TAX", Convert.ToDecimal(Tax_TextBox.Text));
        }
        if (!Net_dividend_TextBox.Text.Equals(""))
        {
            httable.Add("NET_DIVIDEND", Convert.ToDecimal(Net_dividend_TextBox.Text));
        }
        
        if (!TextBox_divi_receivable.Text.Equals(""))
        {
            httable.Add("DIVI_RECEIVAABLE", TextBox_divi_receivable.Text.ToString());
        }
        if (!DueExcessTextBox.Text.Equals(""))
        {
            httable.Add("DUE_EXCESS_DIVIDEND", DueExcessTextBox.Text.ToString());
        }
        if (!TextBox_divi_receivable_date.Text.Equals(""))
        {
            httable.Add("DIVI_RECEIVE_DATE", Convert.ToDateTime(TextBox_divi_receivable_date.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!TextBox_RemarksDividend_Receive.Text.Equals(""))
        {
            httable.Add("DIVI_REMARKS", TextBox_RemarksDividend_Receive.Text.ToString());
        }
        httable.Add("ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));
        commonGatewayObj.Insert(httable, "RECEIVABLECASH_DIVIDEND");
        ClearFields();
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);
       // Response.Redirect("ReceivableCashDividendEnrry.aspx");

    }

    private DataTable get_dividend(string   fundCode ,string CompID ,string AGM_DATE , string RECORD_DT )
   {
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();

        sbMst.Append(" select a.* from   ( SELECT    FUND.F_CD, FUND.F_NAME,COMP.COMP_CD ,COMP.COMP_NM, BOOK_CL.AGM, BOOK_CL.RECORD_DT, COMP.FC_VAL, BOOK_CL.CASH, ");
        sbMst.Append(" COMP.FC_VAL * BOOK_CL.CASH / 100 AS DIVIDEND_PER_SHARE, PFOLIO_BK.TOT_NOS,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH AS GROSS_DIVIDEND, decode(FUND.F_CD, 1, ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .2, 0) AS TAX, decode(FUND.F_CD, 1,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .8,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH) AS NET_DIVIDEND ");
        sbMst.Append(" FROM         BOOK_CL INNER JOIN ");
        sbMst.Append(" COMP ON BOOK_CL.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND INNER JOIN ");
        sbMst.Append(" PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON BOOK_CL.COMP_CD = PFOLIO_BK.COMP_CD AND ");
        sbMst.Append(" BOOK_CL.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL  ");
        sbMst.Append(" WHERE       (BOOK_CL.CASH IS NOT NULL) ) a  ");

        if ((fundCode != "") && (CompID != "") && (AGM_DATE != "") && (RECORD_DT != ""))
        {
            sbMst.Append(" Where  a.F_CD="+ fundCode + " AND a.COMP_CD="+ CompID + " AND a.AGM='"+ AGM_DATE + "' AND a.RECORD_DT='"+ RECORD_DT + "' ");
        }

        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        return dtReprtSource;
    }
   
    private void ClearFields()
    {
        FundCodeTextBox.Text = "";
        fundDropDownList.SelectedValue = "0";
        companyCodeTextBox.Text = "";
        companyNameDropDownList.SelectedValue = "0";
        agmDateTextBox.Text = "";
        recordDateTextBox.Text = "";
        NoofSahreTextBox.Text = "";
        gross_dividend_TextBox.Text = "";
        Tax_TextBox.Text = "";
        Net_dividend_TextBox.Text = "";
        TextBox_divi_receivable.Text = "";
        DueExcessTextBox.Text = "";
        TextBox_divi_receivable_date.Text = "";
        TextBox_RemarksDividend_Receive.Text = "";
        

    }
    protected void divi_receivable_TextBox_TextChanged(object sender, EventArgs e)
    {
        if (Net_dividend_TextBox.Text != "" && TextBox_divi_receivable.Text != "")
        {
            double net_dividend = Convert.ToDouble(Net_dividend_TextBox.Text);
            double divi_reciable = Convert.ToDouble(TextBox_divi_receivable.Text);
            double Due_Excess_dividend = (net_dividend- divi_reciable);
    
            DueExcessTextBox.Text = Due_Excess_dividend.ToString();

          
        }
        else
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Enter Ammount !');", true);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Please Enter Amount !')", true);
        }



    }



    public DataTable ISDividendPosted(string fundCode, string CompID, string AGM_DATE, string RECORD_DT)
    {
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();

        sbMst.Append("  select * from RECEIVABLECASH_DIVIDEND a ");
        if ((fundCode != "") && (CompID != "") && (AGM_DATE != "") && (RECORD_DT != ""))
        {
            sbMst.Append(" Where  a.F_CD=" + fundCode + " AND a.COMP_CD=" + CompID + " AND a.AGM='" + AGM_DATE + "' AND a.RECORD_DT='" + RECORD_DT + "' ");
        }
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        return dtReprtSource;

      
    }
   
    protected void clearButton_Click(object sender, EventArgs e)
    {
        ClearFields();
    }
    protected void updateButton_Click(object sender, EventArgs e)
    {
        Hashtable httable = new Hashtable();
        httable.Add("F_CD", Convert.ToInt16(FundCodeTextBox.Text));
        httable.Add("COMP_CD", Convert.ToInt16(companyCodeTextBox.Text));

        string reciabe = TextBox_divi_receivable.Text.ToString();

        // httable.Add("fy", financialYearTextBox.Text.ToString());
        if (!recordDateTextBox.Text.Equals(""))
        {
            httable.Add("RECORD_DT", Convert.ToDateTime(recordDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RECORD_DT", DBNull.Value);
        }
        if (!agmDateTextBox.Text.Equals(""))
        {
            httable.Add("AGM", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("AGM", DBNull.Value);
        }
        if (!NoofSahreTextBox.Text.Equals(""))
        {
            httable.Add("TOT_NOS", Convert.ToDecimal(NoofSahreTextBox.Text));
        }
        else
        {
            httable.Add("TOT_NOS", DBNull.Value);
        }

        if (!gross_dividend_TextBox.Text.Equals(""))
        {
            httable.Add("GROSS_DIVIDEND", Convert.ToDecimal(gross_dividend_TextBox.Text.ToString()));
        }
        else
        {
            httable.Add("GROSS_DIVIDEND", DBNull.Value);
        }

        if (!Tax_TextBox.Text.Equals(""))
        {
            httable.Add("TAX", Convert.ToDecimal(Tax_TextBox.Text));
        }
        else
        {
            httable.Add("TAX", DBNull.Value);
        }
        if (!Net_dividend_TextBox.Text.Equals(""))
        {
            httable.Add("NET_DIVIDEND", Convert.ToDecimal(Net_dividend_TextBox.Text));
        }
        else
        {
            httable.Add("NET_DIVIDEND", DBNull.Value);
        }

     
        if (!TextBox_divi_receivable.Text.Equals(""))
        {
            httable.Add("DIVI_RECEIVAABLE", reciabe);
        }
        else
        {
            httable.Add("DIVI_RECEIVAABLE", 0);
        }
        if (!TextBox_divi_receivable_date.Text.Equals(""))
        {
            httable.Add("DIVI_RECEIVE_DATE", Convert.ToDateTime(TextBox_divi_receivable_date.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("DIVI_RECEIVE_DATE", DBNull.Value);
        }
        if (!DueExcessTextBox.Text.Equals(""))
        {
            httable.Add("DUE_EXCESS_DIVIDEND", DueExcessTextBox.Text.ToString());
        }
        else
        {
            httable.Add("DUE_EXCESS_DIVIDEND", null);
        }

        if (!TextBox_RemarksDividend_Receive.Text.Equals(""))
        {
            httable.Add("DIVI_REMARKS", TextBox_RemarksDividend_Receive.Text.ToString());
        }
        else
        {
            httable.Add("DIVI_REMARKS", null);
        }

      
        commonGatewayObj.Update(httable, "RECEIVABLECASH_DIVIDEND", "comp_cd = " + companyCodeTextBox.Text + "and F_CD = '" + FundCodeTextBox.Text.ToString() + "' and RECORD_DT='"+ Convert.ToDateTime(recordDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy") + "' and AGM='"+ Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy") + "'");
        ClearFields();
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Updated Successfully');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Updated Successfully.');", true);
       // Response.Redirect("ReceivableCashDividendEnrry.aspx");


    }
}
