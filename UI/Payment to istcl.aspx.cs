using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class UI_BalancechekReport : System.Web.UI.Page
{
    DropDownList dropDownListObj = new DropDownList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtBAnkNameDropDownList = dropDownListObj.BANKDROPDOWNLIST();
        if (!IsPostBack)
        {
            if (dtBAnkNameDropDownList.Rows.Count > 0)
            {

                DropDownListbank.DataSource = dtBAnkNameDropDownList;
                DropDownListbank.DataTextField = "BN_NAME";
                DropDownListbank.DataValueField = "BN_CD";
                DropDownListbank.DataBind();

            }
        }

      
    }

    protected void showButton_Click(object sender, EventArgs e)
    {
        DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
        DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


        string p1date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
        string p2date = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");

        Session["Fromdate"] = p1date;
        Session["Todate"] = p2date;
        Session["bankcodes"] = DropDownListbank.SelectedValue.ToString();

        StringBuilder sb = new StringBuilder();

        // ClientScript.RegisterStartupScript(this.GetType(), "ReceivablePayableDSEandCSESeparateReportVeiwer", "window.open('ReportViewer/ReceivablePayableDSEandCSESeparateReportVeiwer.aspx')", true);
        Response.Redirect("ReportViewer/PaymenttoistclReportVeiwer.aspx");
    }
    //protected void Gobutton_Click(object sender, EventArgs e)
    //{
    //    DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
    //    DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


    //    string p1date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
    //    string p2date = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");

    //    DataTable dtFundNameDropDownList = dropDownListObj.PayableToISTCLFundNameDropDownList(p1date, p2date);
    //    if (dtFundNameDropDownList.Rows.Count > 0)
    //    {
    //        fundNameDropDownListlabel.Visible = true;
    //        fundNameDropDownList.Visible = true;

    //        fundNameDropDownList.DataSource = dtFundNameDropDownList;
    //        fundNameDropDownList.DataTextField = "F_NAME";
    //        fundNameDropDownList.DataValueField = "F_CD";
    //        fundNameDropDownList.DataBind();

    //    }
    //    else
    //    {
    //        fundNameDropDownListlabel.Visible = false;
    //        fundNameDropDownList.Visible = false;
    //    }


        
    //}

}