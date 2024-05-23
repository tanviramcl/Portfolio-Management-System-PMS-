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
using System.Text;

public partial class UI_BookCloserReport : System.Web.UI.Page
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

    protected void viewReportButton_Click(object sender, EventArgs e)
    {
        string entryDate = entryDateTextBox.Text.ToString();
        string toEntryDate = toEntryDateTextBox.Text.ToString();
        int compCode = Convert.ToInt32(companyNameDropDownList.SelectedValue);
        StringBuilder sb = new StringBuilder();
      
        Session["checkbokOld"] = oldRadioButton.Checked;
        sb.Append("window.open('ReportViewer/BookCloserEntryViewer.aspx?entryDate=" + entryDate + "&toEntryDate=" + toEntryDate + "&compCode=" + compCode + "');");
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
    }



        
      
}
