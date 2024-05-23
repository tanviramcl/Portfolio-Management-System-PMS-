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

public partial class UI_FundTransactionEntry : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    Pf1s1DAO pf1s1DAOObj = new Pf1s1DAO();

    //double noOfShare = 0.00;
    //double amount = 0.00;
    double rate = 0.00;
    //double amountAfterComission = 0.00;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();

            lblrecordDate.Visible = false;
            recordDateTextBox.Visible = false;
            ImageButton1.Visible = false;

        }
    }
    protected void noOfShareTextBox_TextChanged(object sender, EventArgs e)
    {
        if (transTypeDropDownList.SelectedValue == "B")
        {
            if (noOfShareTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + noOfShareTextBox.ClientID + "').focus();</script>");
            }
            else
            {
                amountTextBox.Text = "0.00";
                rateTextBox.Text = "0.00";
                amountAfterComissionTextBox.Text = "0.00";
                ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + voucherNoTextBox.ClientID + "').focus();</script>");
            }

        }
        if (transTypeDropDownList.SelectedValue != "B")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + amountTextBox.ClientID + "').focus();</script>");
        }
        //else if (transTypeDropDownList.SelectedValue == "C")
        //{

        //}
        //noOfShareTextBox.AutoPostBack = false;
        if (noOfShareTextBox.Text != "" && amountTextBox.Text != "")
        {
            rate = Convert.ToDouble(amountTextBox.Text) / Convert.ToDouble(noOfShareTextBox.Text);
            rateTextBox.Text = rate.ToString();
            amountAfterComissionTextBox.Text = amountTextBox.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + voucherNoTextBox.ClientID + "').focus();</script>");
        }
    }
    protected void amountTextBox_TextChanged(object sender, EventArgs e)
    {
        if (noOfShareTextBox.Text != "")
        {
            if (transTypeDropDownList.SelectedValue != "B")
            {
                if (amountTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + amountTextBox.ClientID + "').focus();</script>");
                    ClearFields();
                }
                else
                {
                    rate = Convert.ToDouble(amountTextBox.Text) / Convert.ToDouble(noOfShareTextBox.Text);
                    rateTextBox.Text = rate.ToString();
                    amountAfterComissionTextBox.Text = amountTextBox.Text;
                    ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + voucherNoTextBox.ClientID + "').focus();</script>");
                }
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Insert No of Share');", true);
            ClearFields();
        }


        //else if (transTypeDropDownList.SelectedValue == "S")
        //{

        //}
        //amountTextBox.AutoPostBack = false;
    }
    protected void transTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //noOfShareTextBox.Text = "";
        //voucherNoTextBox.Text = "";
        //amountTextBox.Text = "";
        //rateTextBox.Text = "";
        //amountAfterComissionTextBox.Text = "";
        lblrecordDate.Visible = false;
        recordDateTextBox.Visible = false;
        ImageButton1.Visible = false;

        string tran_tp = transTypeDropDownList.SelectedValue.ToString();

        if (tran_tp == "B")
        {
            amountTextBox.ReadOnly = true;
            lblrecordDate.Visible = true;
            recordDateTextBox.Visible = true;
            ImageButton1.Visible = true;
        }
        else
        {
            amountTextBox.ReadOnly = false;
        }
        noOfShareTextBox.Text = "";
        fundNameDropDownList.SelectedValue = "0";
        voucherNoTextBox.Text = "";
        amountTextBox.Text = "";
        rateTextBox.Text = "";
        amountAfterComissionTextBox.Text = "";
        recordDateTextBox.Text = "";


    }
    protected void saveButton_Click(object sender, EventArgs e)
    {
        string LoginID = Session["UserID"].ToString();
        string LoginName = Session["UserName"].ToString().ToUpper();
        string record_date = recordDateTextBox.Text.ToString();
        string recordate;
        DateTime? dtrecoddate;
        if (!string.IsNullOrEmpty(record_date))
        {
            dtrecoddate = Convert.ToDateTime(record_date);

            recordate = dtrecoddate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtrecoddate = null;
            recordate = "";
        }



        Hashtable httable = new Hashtable();
        httable.Add("VCH_DT", Convert.ToDateTime(howlaDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        if (!stockExchangeDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("STOCK_EX", Convert.ToChar(stockExchangeDropDownList.SelectedValue));
        }
        if (!fundNameDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("F_CD", Convert.ToInt16(fundNameDropDownList.SelectedValue));
        }
        if (!companyNameDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("COMP_CD", Convert.ToInt16(companyNameDropDownList.SelectedValue));
        }
        if (!transTypeDropDownList.SelectedValue.Equals("0"))
        {
            httable.Add("TRAN_TP", Convert.ToChar(transTypeDropDownList.SelectedValue));
        }
        if (!noOfShareTextBox.Text.Equals(""))
        {
            httable.Add("NO_SHARE", Convert.ToDouble(noOfShareTextBox.Text));
        }
        if (!amountTextBox.Text.Equals(""))
        {
            httable.Add("AMOUNT", Convert.ToDouble(amountTextBox.Text));
        }
        if (!voucherNoTextBox.Text.Equals(""))
        {
            httable.Add("VCH_NO", (voucherNoTextBox.Text).ToString());
        }
        if (!rateTextBox.Text.Equals(""))
        {
            httable.Add("RATE", Convert.ToDouble(rateTextBox.Text));
        }
        if (!amountAfterComissionTextBox.Text.Equals(""))
        {
            httable.Add("AMT_AFT_COM", Convert.ToDouble(amountAfterComissionTextBox.Text));
        }



        //httable.Add("ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));
        httable.Add("OP_NAME", LoginID);

        if (pf1s1DAOObj.IsDuplicateBonusRightEntry(Convert.ToInt32(fundNameDropDownList.SelectedValue.ToString()), Convert.ToInt32(companyNameDropDownList.SelectedValue.ToString()), Convert.ToDateTime(howlaDateTextBox.Text.Trim().ToString()).ToString("dd-MMM-yyyy"), transTypeDropDownList.SelectedValue.ToString(), Convert.ToInt32(noOfShareTextBox.Text.Trim().ToString())))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Duplicate not allowed');", true);
        }
        else
        {

            if (transTypeDropDownList.SelectedValue == "B")
            {
                if (stockExchangeDropDownList.SelectedValue == "A")
                {
                    if (!recordate.Equals(""))
                    {
                        httable.Add("RECORD_DT", recordate);
                    }


                    //.........................For Do manual Posting for Reconciliation ............................
                    //string strQbooKCL = "SELECT COMP_CD, FY, RECORD_DT, BOOK_TO, BONUS, RIGHT_APPR_DT,  RIGHT, CASH, AGM, REMARKS, POSTED, PDATE,  ENTRY_DATE FROM BOOK_CL WHERE  COMP_CD=" + companyNameDropDownList.SelectedValue + " AND RECORD_DT='" + recordate + "'" + " and posted is null";

                    //DataTable dtbookCl = commonGatewayObj.Select(strQbooKCL);
                    //if (dtbookCl != null && dtbookCl.Rows.Count > 0)
                    //{
                    string strRecordateFT = "SELECT VCH_DT, F_CD, COMP_CD, TRAN_TP, VCH_NO, NO_SHARE, RATE, COST_RATE, CRT_AFT_COM,  AMOUNT, AMT_AFT_COM, STOCK_EX, OP_NAME, PVCH_NO, RECORD_DT FROM FUND_TRANS_HB Where  f_cd='" + fundNameDropDownList.SelectedValue.ToString() + "' AND comp_cd=" + companyNameDropDownList.SelectedValue.ToString() + " and RECORD_DT='" + recordate + "'";
                    DataTable dtRecorDateFT = commonGatewayObj.Select(strRecordateFT);

                    if (dtRecorDateFT != null && dtRecorDateFT.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Bonus already posted.');", true);
                        ClearFields();
                    }
                    else
                    {
                        if (httable["TRAN_TP"].ToString() == "B")
                        {
                            commonGatewayObj.Insert(httable, "fund_trans_hb");
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
                        }


                    }
                    //}
                    //else
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No data found in Book Closer Entry');", true);
                    //}

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Select Stock Exchange as ALL');", true);
                }


            }
            else if (transTypeDropDownList.SelectedValue == "I")
            {
                if (httable["TRAN_TP"].ToString() == "I")
                {
                    commonGatewayObj.Insert(httable, "fund_trans_hb");

                    string strupdateQueryAVgRateFromComp = "update comp set AVG_RT='" + Convert.ToDouble(rateTextBox.Text) + "',CSE_RT='" + Convert.ToDouble(rateTextBox.Text) + "',ADC_RT='" + Convert.ToDouble(rateTextBox.Text) + "' where comp_cd =" + companyNameDropDownList.SelectedValue.ToString() + "";

                    int updateQueryAVgRateFromCompNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryAVgRateFromComp);
                    //   ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update Successfully');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
                }

            }
            else
            {
                commonGatewayObj.Insert(httable, "fund_trans_hb");
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
            }

            ClearFields();

        }
        fundNameDropDownList.Focus();
    }

    public void ClearFields()
    {
        //fundNameDropDownList.SelectedValue = "0";
        noOfShareTextBox.Text = "";
        voucherNoTextBox.Text = "";
        amountTextBox.Text = "";
        rateTextBox.Text = "";
        amountAfterComissionTextBox.Text = "";
        recordDateTextBox.Text = "";
    }
    protected void fundNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
