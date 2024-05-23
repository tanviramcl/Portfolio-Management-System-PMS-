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

public partial class UI_CloseEndNAVLetterReport : System.Web.UI.Page
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
        DataTable dtSignatoryDropDownList = dropDownListObj.SignatoryDropDownList();
        DataTable dtFundNameDropDownList = dropDownListObj.CloseEndFundNameDropDownList();
        if (!IsPostBack)
        {
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
        string navLetterType = "";
        string letterPrintDate = letterPrintDateTextBox.Text.ToString();
        string navDate = navDateTextBox.Text.ToString();
        string letterTo = letterToDropDownList.SelectedValue.ToString();
        string fundName = fundNameDropDownList.SelectedValue.ToString();
        string signatory = signatoryDropDownList.SelectedValue.ToString();
        if (navLetterRadioButton.Checked)
        {
            navLetterType = "navLetter";
        }
        else
        {
            navLetterType = "pressRelease";
        }

        //Response.Redirect("ReportViewer/CloseEndNAVLetterReportViewer.aspx?letterPrintDate=" + letterPrintDate + "&navDate=" + navDate + "&letterTo=" + letterTo + "&fundName=" + fundName + "&signatory=" + signatory);
        sb.Append("window.open('ReportViewer/CloseEndNAVLetterReportViewer.aspx?letterPrintDate=" + letterPrintDate + "&navDate=" + navDate + "&letterTo=" + letterTo + "&fundName=" + fundName + "&signatory=" + signatory + "&navLetterType=" + navLetterType + "');");
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
    }
    
}
