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
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DataTable dtCompanAlllist = NolistedCompanyCodeNameDropDownList();
        DataTable dtCompanyDropdownlist = dropDownListObj.NolistedCompanyCodeNameDropDownList();

        if (!IsPostBack)
        {


            nonlistedCompanyDropDownList.DataSource = dtCompanyDropdownlist;
            nonlistedCompanyDropDownList.DataTextField = "COMP_NM";
            nonlistedCompanyDropDownList.DataValueField = "COMP_CD";
            nonlistedCompanyDropDownList.DataBind();


            if (string.IsNullOrEmpty(Request.QueryString["COMP_CD"]) && string.IsNullOrEmpty(Request.QueryString["TRAN_DATE"]) && string.IsNullOrEmpty(Request.QueryString["MARKET_RATE"]) && string.IsNullOrEmpty(Request.QueryString["NAV"])) 
            {
                DateTime dtimeCurrentDate = DateTime.Now;

                string currentDate = Convert.ToDateTime(dtimeCurrentDate).ToString("dd-MMM-yyyy");

                if (!string.IsNullOrEmpty(currentDate))
                {
                    InvestMentDateTextBox.Text = currentDate;
                }
                else
                {
                    InvestMentDateTextBox.Text = "";
                }

                Session["ALLCompanyInformationNonListed"] = dtCompanAlllist;

                DataTable dtallNonlistedCompany = (DataTable)Session["ALLCompanyInformationNonListed"];

            }
            else
            {
                string CompID = Convert.ToString(Request.QueryString["COMP_CD"]).Trim();


                string TRAN_DATE = Convert.ToString(Request.QueryString["TRAN_DATE"]).Trim();

                string MARKET_RATE = Convert.ToString(Request.QueryString["MARKET_RATE"]).Trim();

                string NAV = Convert.ToString(Request.QueryString["NAV"]).Trim();

                nonlistedCompanyDropDownList.SelectedValue = CompID.ToString();
                DateTime dtimeTRanjectionDate = Convert.ToDateTime(TRAN_DATE);
                string trajectiondate = dtimeTRanjectionDate.ToString("dd-MMM-yyyy");
                InvestMentDateTextBox.Text = trajectiondate;
                rateTextBox.Text = MARKET_RATE;
                NavTextBox.Text = NAV;



            }

              


        }


      
    }


    protected void nonlistedCompanyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillNonListedSecuritiesGrid();



        DataTable dtnonListedCompanyName = commonGatewayObj.Select(" select * from (Select a.COMP_CD,a.COMP_NM,a.SECT_MAJ_CD,a.ADD1,a.ADD2,a.TEL,a.EMAIL,a.AUTH_CAP,a.PAID_CAP,a.CAT_TP,decode(B.NAV,null,0,B.NAV) NAV,B.MARKET_RATE,B.TRAN_DATE from COMP_NONLISTED  a inner join NONLISTED_MARKET_PRICE b on a.COMP_CD=b.COMP_CD )   Where comp_cd=" + nonlistedCompanyDropDownList.SelectedValue.ToString() + " order by TRAN_DATE DESC  ");

        Session["ALLCompanyInformationNonListed"] = dtnonListedCompanyName;

        DataTable dtallNonlistedCompany = (DataTable)Session["ALLCompanyInformationNonListed"];

        
    }

    protected void resetButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompNonListedMArketPriceEntryForm.aspx");

    }


    protected void test_buutonClick(object sender, EventArgs e)
    {
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        string CompID = nonlistedCompanyDropDownList.SelectedValue.ToString();
        string trajectiondate = "";

        string TRAN_DATE = InvestMentDateTextBox.Text.ToString();

        string MARKET_RATE = rateTextBox.Text;
        string NAV = NavTextBox.Text;

        DateTime? dtimeTRanjectionDate ;

        nonlistedCompanyDropDownList.SelectedValue = CompID.ToString();

        if (!string.IsNullOrEmpty(TRAN_DATE))
        {
            dtimeTRanjectionDate = Convert.ToDateTime(TRAN_DATE);

            trajectiondate = dtimeTRanjectionDate.Value.ToString("dd-MMM-yyyy");

           
        }
        else
        {
            dtimeTRanjectionDate = null;
            trajectiondate = "";
        }

        //dtimeTRanjectionDate = Convert.ToDateTime(TRAN_DATE);
      //  string trajectiondate = dtimeTRanjectionDate.ToString("dd-MMM-yyyy");

        string strQuery = " SELECT COMP_CD, TRAN_DATE, MARKET_RATE FROM INVEST.NONLISTED_MARKET_PRICE where COMP_CD =" + CompID + "  and TRAN_DATE='" + trajectiondate + "'";
       DataTable dtnonListedCompany = commonGatewayObj.Select(strQuery);
        if (dtnonListedCompany != null && dtnonListedCompany.Rows.Count > 0)
        {
            string strUPQuery = "update NONLISTED_MARKET_PRICE SET MARKET_RATE ='" + rateTextBox.Text.ToString() + "',NAV='"+NavTextBox.Text.ToString()+"',TRAN_DATE='" + trajectiondate + "'  where  TRAN_DATE='" + trajectiondate + "'  and COMP_CD='" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "'";
            int NumOfRows3 = commonGatewayObj.ExecuteNonQuery(strUPQuery);
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update Sucessfully')", true);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Update Sucessfully')", true);
        }
        else
        {
            string strInsQuery;

            strInsQuery = "insert into NONLISTED_MARKET_PRICE(COMP_CD,TRAN_DATE,MARKET_RATE,NAV)values('" + nonlistedCompanyDropDownList.SelectedValue.ToString() + "','" + trajectiondate + "','" + rateTextBox.Text.ToString() + "','"+NavTextBox.Text.ToString()+"')";

            int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);

            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Insert Sucessfully')", true);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Insert Sucessfully .!!!')", true);
        }
         Response.Redirect("CompNonListedMArketPriceEntryForm.aspx");
        //FillNonListedCompanyGrid();
      //  cleartext();

    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {

        //if (string.IsNullOrEmpty(Request.QueryString["ID"]) && string.IsNullOrEmpty(Request.QueryString["COMP_CD"]) && string.IsNullOrEmpty(Request.QueryString["INV_DATE"]) && string.IsNullOrEmpty(Request.QueryString["TRAN_TP"]))
        //{
        //    // not there!
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Please Select an row')", true);

        //}
        //else
        //{
        //    string confirmValue = HiddenField2.Value;
        //    //string fundCode, CompID, INV_DATE, TRAN_TP, investmentdate, strDelQuery;
        //    //DateTime dtimeInvestmentDate;
        //    //int NumOfRows = 0;
        //    if (confirmValue == "Yes")
        //    {
        //        string fundCode = Convert.ToString(Request.QueryString["ID"]).Trim();
        //        string CompID = Convert.ToString(Request.QueryString["COMP_CD"]).Trim();
        //        string INV_DATE = Convert.ToString(Request.QueryString["INV_DATE"]).Trim();
        //        string TRAN_TP = Convert.ToString(Request.QueryString["TRAN_TP"]).Trim();
        //        DateTime dtimeInvestmentDate = Convert.ToDateTime(INV_DATE);
        //        string investmentdate = dtimeInvestmentDate.ToString("dd-MMM-yyyy");
        //        string strDelQuery = "delete from NON_LISTED_SECURITIES_DETAILS where comp_cd='" + CompID + "' and f_cd=" + fundCode + " and INV_DATE='" + investmentdate + "' and TRAN_TP='" + TRAN_TP + "'";
        //        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Delete Sucessfully')", true);
        //        Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("NonListedSecuritiesInvestmentEntryForm.aspx");
        //    }

        if (string.IsNullOrEmpty(Request.QueryString["COMP_CD"]) && string.IsNullOrEmpty(Request.QueryString["TRAN_DATE"]) && string.IsNullOrEmpty(Request.QueryString["MARKET_RATE"]))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Please Select an row')", true);
        }
        else
        {
            string confirmValue = HiddenField2.Value;
           
            if (confirmValue == "Yes")
            {
                string CompID = nonlistedCompanyDropDownList.SelectedValue.ToString();


                string TRAN_DATE = InvestMentDateTextBox.Text.ToString();

                string MARKET_RATE = rateTextBox.Text;

                nonlistedCompanyDropDownList.SelectedValue = CompID.ToString();
                DateTime dtimeTRanjectionDate = Convert.ToDateTime(TRAN_DATE);
                string trajectiondate = dtimeTRanjectionDate.ToString("dd-MMM-yyyy");

                string strQuery = " SELECT COMP_CD, TRAN_DATE, MARKET_RATE FROM INVEST.NONLISTED_MARKET_PRICE where COMP_CD =" + CompID + "  and TRAN_DATE='" + trajectiondate + "'";
                DataTable dtnonListedCompany = commonGatewayObj.Select(strQuery);
                if (dtnonListedCompany != null && dtnonListedCompany.Rows.Count > 0)
                {
                    string strDelQuery = "delete from NONLISTED_MARKET_PRICE  where COMP_CD =" + CompID + "  and TRAN_DATE='" + trajectiondate + "'";
                    int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Delete Sucessfully')", true);
                    Response.Redirect("CompNonListedMArketPriceEntryForm.aspx");
                }
            }
            else
            {
                Response.Redirect("CompNonListedMArketPriceEntryForm.aspx");
            }

        }
    }

    private void FillNonListedCompanyGrid()
    {
        DataTable dtnonListedCompanyName = commonGatewayObj.Select(" Select a.COMP_CD,a.COMP_NM,a.SECT_MAJ_CD,a.ADD1,a.ADD2,a.TEL,a.EMAIL,a.AUTH_CAP,a.PAID_CAP,a.CAT_TP,B.MARKET_RATE,decode(B.NAV,null,0,B.NAV) NAV,B.TRAN_DATE from COMP_NONLISTED  a inner join NONLISTED_MARKET_PRICE b on a.COMP_CD=b.COMP_CD  order by TRAN_DATE DESC ");
        if (dtnonListedCompanyName != null && dtnonListedCompanyName.Rows.Count > 0)
        {
            Session["ALLCompanyInformationNonListed"] = dtnonListedCompanyName;
        }
    }

    public DataTable NolistedCompanyCodeNameDropDownList()//For All Funds
    {
        DataTable dtnonListedCompanyName = commonGatewayObj.Select(" Select a.COMP_CD,a.COMP_NM,a.SECT_MAJ_CD,a.ADD1,a.ADD2,a.TEL,a.EMAIL,a.AUTH_CAP,a.PAID_CAP,a.CAT_TP,decode(B.NAV,null,0,B.NAV) NAV,B.MARKET_RATE,B.TRAN_DATE from COMP_NONLISTED  a inner join NONLISTED_MARKET_PRICE b on a.COMP_CD=b.COMP_CD order by TRAN_DATE DESC ");
       
        return dtnonListedCompanyName;
    }

    public void cleartext()
    {
        nonlistedCompanyDropDownList.SelectedValue = "0";
        InvestMentDateTextBox.Text = "";
        
        rateTextBox.Text = "";
        
    }


}
