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

public partial class UI_ReceivableCashDividend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        Session["recordDateFrom"] = recordDateFromTextBox.Text.ToString();
        Session["recordDateTo"] = recordDateToTextBox.Text.ToString();
        Session["agmDateFrom"] = agmDateFromTextBox.Text.ToString();
        Session["agmDateTo"] = agmDateToTextBox.Text.ToString();
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();

        if (allRadioButton.Checked)
        {
            Response.Redirect("ReportViewer/ReceivableCashDividendReportViewer.aspx");
        }
        else if (SectorWiseallRadioButton.Checked)
        {
            Response.Redirect("ReportViewer/ReceivableCashDividendReportViewerSectorWise.aspx");
        }
            //ClientScript.RegisterStartupScript(this.GetType(), "ReceivableCashDividendReportViewer", "window.open('ReportViewer/ReceivableCashDividendReportViewer.aspx')", true);
           
    }
}
