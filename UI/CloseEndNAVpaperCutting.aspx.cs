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

public partial class UI_CloseEndNAVpaperCutting : System.Web.UI.Page
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

        DataTable PaperNameDropDownList = dropDownListObj.PaperNameDropDownList();
        DataTable dtSignatoryDropDownList = dropDownListObj.SignatoryDropDownList();
        DataTable dtFundNameDropDownList = dropDownListObj.CloseEndFundNameDropDownList();
        
        if (!IsPostBack)
        {
            paperNameDropDownList.DataSource = PaperNameDropDownList;
            paperNameDropDownList.DataTextField = "NEWS_PAPER_NAME";
            paperNameDropDownList.DataValueField = "ID";
            paperNameDropDownList.SelectedValue = "1";
            paperNameDropDownList.DataBind();

            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();

            signatoryDropDownList.DataSource = dtSignatoryDropDownList;
            signatoryDropDownList.DataTextField = "NAME";
            signatoryDropDownList.DataValueField = "ID";
            signatoryDropDownList.SelectedValue = "IAMCL411";
            signatoryDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        
        string letterTo = letterToDropDownList.SelectedValue.ToString();
        string letterPrintDate = letterPrintDateTextBox.Text.ToString();
        string paperName = paperNameDropDownList.SelectedValue.ToString();
        string publishedDate = dateOfPublishTextBox.Text.ToString();
        string fundName = fundNameDropDownList.SelectedValue.ToString();
        string signatory = signatoryDropDownList.SelectedValue.ToString();

        sb.Append("window.open('ReportViewer/CloseEndNavPaperCuttingReportViewer.aspx?letterPrintDate=" + letterPrintDate + "&publishedDate=" + publishedDate + "&letterTo=" + letterTo + "&fundName=" + fundName + "&signatory=" + signatory + "&paperName=" + paperName + "');");
        
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);

    }
}
