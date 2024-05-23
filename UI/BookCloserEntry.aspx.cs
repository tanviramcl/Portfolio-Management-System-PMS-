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

public partial class UI_BookCloserEntry : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();
        }
       
    }
    protected void addNewButton_Click(object sender, EventArgs e)
    {
        InsertData();
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();
        DataTable dtFind = commonGatewayObj.Select("SELECT * FROM BOOK_CL WHERE COMP_CD=" + comp_cd + " AND fy='" + financialYearTextBox.Text.ToString() + "' ORDER BY RECORD_DT DESC ");
        if (dtFind.Rows.Count > 0)
        {
            addNewButton.Visible = false;
            updateButton.Visible = true;

            recordDateTextBox.Text = dtFind.Rows[0]["RECORD_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
            bookToTextBox.Text = dtFind.Rows[0]["BOOK_TO"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["BOOK_TO"]).ToString("dd-MMM-yyyy");
            stockTextBox.Text = dtFind.Rows[0]["BONUS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["BONUS"].ToString();
            rightApprovalDateTextBox.Text = dtFind.Rows[0]["RIGHT_APPR_DT"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["RIGHT_APPR_DT"]).ToString("dd-MMM-yyyy");
            rightTextBox.Text = dtFind.Rows[0]["RIGHT"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["RIGHT"].ToString();
            cashTextBox.Text = dtFind.Rows[0]["CASH"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["CASH"].ToString();
            agmDateTextBox.Text = dtFind.Rows[0]["AGM"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");
            remarksTextBox.Text = dtFind.Rows[0]["REMARKS"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["REMARKS"].ToString();
            postedTextBox.Text = dtFind.Rows[0]["POSTED"].Equals(DBNull.Value) ? "" : dtFind.Rows[0]["POSTED"].ToString();
            postedDateTextBox.Text = dtFind.Rows[0]["PDATE"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(dtFind.Rows[0]["PDATE"]).ToString("dd-MMM-yyyy");
        }
        else
        {
            searchClearFields();
            addNewButton.Visible = true;
            updateButton.Visible = false;
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No data find');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found.');", true);
            recordDateTextBox.Focus();
        }


    }
    private void InsertData()
    {
        Hashtable httable = new Hashtable();
        httable.Add("comp_cd", Convert.ToInt16(companyCodeTextBox.Text));
        httable.Add("fy", financialYearTextBox.Text.ToString());
        if (!recordDateTextBox.Text.Equals(""))
        {
            httable.Add("RECORD_DT", Convert.ToDateTime(recordDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!bookToTextBox.Text.Equals(""))
        {
            httable.Add("BOOK_TO", Convert.ToDateTime(bookToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!stockTextBox.Text.Equals(""))
        {
            httable.Add("BONUS", Convert.ToDecimal(stockTextBox.Text));
        }

        if (!rightApprovalDateTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT_APPR_DT", Convert.ToDateTime(rightApprovalDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!rightTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT", Convert.ToDecimal(rightTextBox.Text));
        }
        if (!cashTextBox.Text.Equals(""))
        {
            httable.Add("CASH", Convert.ToDecimal(cashTextBox.Text));
        }
        if (!agmDateTextBox.Text.Equals(""))
        {
            httable.Add("AGM", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!remarksTextBox.Text.Equals(""))
        {
            httable.Add("REMARKS", remarksTextBox.Text.ToString());
        }
        if (!postedTextBox.Text.Equals(""))
        {
            httable.Add("POSTED", postedTextBox.Text.ToString());
        }
        if (!postedDateTextBox.Text.Equals(""))
        {
            httable.Add("PDATE", Convert.ToDateTime(postedDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!TextBoxReceivable.Text.Equals(""))
        {
            httable.Add("DIVI_RECEIVAABLE", TextBoxReceivable.Text.ToString());
        }
        httable.Add("ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));
        commonGatewayObj.Insert(httable, "book_cl");
        ClearFields();
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);
    }
    private void SearchClearFields()
    {
      
        recordDateTextBox.Text = "";
        bookToTextBox.Text = "";
        stockTextBox.Text = "";
        rightTextBox.Text = "";
        rightApprovalDateTextBox.Text = "";
        cashTextBox.Text = "";
        agmDateTextBox.Text = "";
        remarksTextBox.Text = "";
        postedTextBox.Text = "";
        postedDateTextBox.Text = "";

    }

   
    private void ClearFields()
    {
        companyCodeTextBox.Text = "";
        companyNameDropDownList.SelectedValue = "0";
        financialYearTextBox.Text = "";
        recordDateTextBox.Text = "";
        bookToTextBox.Text = "";
        stockTextBox.Text = "";
        rightTextBox.Text = "";
        rightApprovalDateTextBox.Text = "";
        cashTextBox.Text = "";
        agmDateTextBox.Text = "";
        remarksTextBox.Text = "";
        postedTextBox.Text = "";
        postedDateTextBox.Text = "";

    }

    private void searchClearFields()
    {
       
        financialYearTextBox.Text = "";
        recordDateTextBox.Text = "";
        bookToTextBox.Text = "";
        stockTextBox.Text = "";
        rightTextBox.Text = "";
        rightApprovalDateTextBox.Text = "";
        cashTextBox.Text = "";
        agmDateTextBox.Text = "";
        remarksTextBox.Text = "";
        postedTextBox.Text = "";
        postedDateTextBox.Text = "";

    }

    protected void companyNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        updateButton.Visible = false;
        addNewButton.Visible = true;
        //companyCodeTextBox.Text = "";
        searchClearFields();
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();

        if (comp_cd != "")
        {
            companyCodeTextBox.Text = companyNameDropDownList.SelectedValue.ToString();
            //financialYearTextBox.Text = "";
            //SearchClearFields();

            //financialYearTextBox.Focus();
        }
        else
        {
            companyCodeTextBox.Text = "";
        }
    }
    protected void clearButton_Click(object sender, EventArgs e)
    {

    }
    protected void updateButton_Click(object sender, EventArgs e)
    {
        Hashtable httable = new Hashtable();
        httable.Add("comp_cd", Convert.ToInt16(companyCodeTextBox.Text));
        httable.Add("fy", financialYearTextBox.Text.ToString());
        if (!recordDateTextBox.Text.Equals(""))
        {
            httable.Add("RECORD_DT", Convert.ToDateTime(recordDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RECORD_DT", DBNull.Value);
        }
        if (!bookToTextBox.Text.Equals(""))
        {
            httable.Add("BOOK_TO", Convert.ToDateTime(bookToTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("BOOK_TO", DBNull.Value);
        }
        if (!stockTextBox.Text.Equals(""))
        {
            httable.Add("BONUS", Convert.ToDecimal(stockTextBox.Text));
        }
        else
        {
            httable.Add("BONUS", DBNull.Value);
        }

        if (!rightApprovalDateTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT_APPR_DT", Convert.ToDateTime(rightApprovalDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("RIGHT_APPR_DT", DBNull.Value);
        }

        if (!rightTextBox.Text.Equals(""))
        {
            httable.Add("RIGHT", Convert.ToDecimal(rightTextBox.Text));
        }
        else
        {
            httable.Add("RIGHT", DBNull.Value);
        }
        if (!cashTextBox.Text.Equals(""))
        {
            httable.Add("CASH", Convert.ToDecimal(cashTextBox.Text));
        }
        else
        {
            httable.Add("CASH", DBNull.Value);
        }
        if (!agmDateTextBox.Text.Equals(""))
        {
            httable.Add("AGM", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("AGM", DBNull.Value);
        }
        if (!remarksTextBox.Text.Equals(""))
        {
            httable.Add("REMARKS", remarksTextBox.Text.ToString());
        }
        else
        {
            httable.Add("REMARKS", null);
        }
       
        if (!postedTextBox.Text.Equals(""))
        {
            httable.Add("POSTED", postedTextBox.Text.ToString());
        }
        else
        {
            httable.Add("POSTED", null);
        }
        if (!postedDateTextBox.Text.Equals(""))
        {
            httable.Add("PDATE", Convert.ToDateTime(postedDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        else
        {
            httable.Add("PDATE", DBNull.Value);
        }
        commonGatewayObj.Update(httable, "book_cl", "comp_cd = " + companyCodeTextBox.Text + "and fy = '" + financialYearTextBox.Text.ToString() + "'");
        ClearFields();
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Updated Successfully');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Updated Successfully.');", true);

    }
}
