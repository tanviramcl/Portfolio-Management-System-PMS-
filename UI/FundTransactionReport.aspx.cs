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

public partial class UI_FundTransactionReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //CommonGateway commonGatewayObj = new CommonGateway();
        DropDownList dropDownListObj = new DropDownList();
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
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        string howlaDateFrom = howlaDateFromTextBox.Text.ToString();
        string howlaDateTo = howlaDateToTextBox.Text.ToString();
        string transType = transTypeDropDownList.SelectedValue.ToString();
        int companyName = Convert.ToInt32(companyNameDropDownList.SelectedValue);
        int fundName = Convert.ToInt32(fundNameDropDownList.SelectedValue);
        StringBuilder sb = new StringBuilder();
        sb.Append("window.open('ReportViewer/FundTransactionReportViewer.aspx?howlaDateFrom=" + howlaDateFrom + "&howlaDateTo= " + howlaDateTo + "&transType= " + transType + "&companyName= " + companyName + "&fundName= " + fundName + "');");
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
    }
}
