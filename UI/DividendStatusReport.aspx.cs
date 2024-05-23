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

public partial class UI_ReceivableCashDividend : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();

            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        sbMst.Append("   select tab2.*, C.FY, C.FY_PART, C.RECORD_DT, C.TYPE,   C.QUANTITY, C.AGM, C.REMARKS, C.APPR_DT, C.POSTED, C.PDATE,  ");
        sbMst.Append("    C.ENTRY_DATE As CD_ENTRY from (select tab1.*,f.f_name from (select a.*,c.comp_nm,c.FC_VAL from  BOOK_CL_DETAILS  a  ");
        sbMst.Append("     inner join  comp C  on  a.comp_cd=c.comp_cd  ) tab1  inner join   fund  f on tab1.f_cd=f.F_cd   ) tab2 inner join  ");
        sbMst.Append("       CORPORATE_DEC c on tab2.BOOK_CL_ID=c.BOOK_CL_ID   Where c.TYPE='C'    ");


        if ((recordDateFromTextBox.Text.ToString() != "") && (recordDateToTextBox.Text.ToString() != ""))
        {
            sbMst.Append("  And c.RECORD_DT between '" + recordDateFromTextBox.Text.ToString() + "' AND '" + recordDateToTextBox.Text.ToString() + "'");

        }

        if ((agmDateFromTextBox.Text.ToString() != "") && (agmDateToTextBox.Text.ToString() == ""))
        {
            sbMst.Append(" AND c.AGM >= '" + agmDateFromTextBox.Text.ToString() + "'");
        }
        else if ((agmDateFromTextBox.Text.ToString() == "") && (agmDateToTextBox.Text.ToString() != ""))
        {
            sbMst.Append(" AND c.AGM <= '" + agmDateToTextBox.Text.ToString() + "'");
        }
        else if ((agmDateFromTextBox.Text.ToString() != "") && (agmDateToTextBox.Text.ToString() != ""))
        {
            sbMst.Append(" AND c.AGM BETWEEN '" + agmDateFromTextBox.Text.ToString() + "' AND '" + agmDateToTextBox.Text.ToString() + "'");
        }

        if (fundNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbMst.Append(" AND tab2.F_CD = " + Convert.ToInt16(fundNameDropDownList.SelectedValue.ToString().ToString()) + "");
        }


        if (companyNameDropDownList.SelectedValue.ToString() != "0")
        {
            sbMst.Append(" AND c.comp_cd = " + Convert.ToInt16(companyNameDropDownList.SelectedValue.ToString().ToString()) + "");
        }


        if (ddlTest.SelectedItem.Value.ToString() == "Recevied")
        {
            sbMst.Append(" AND PAYMENT_DATE is not null AND POSTED ='Y' ");
        }
        if (ddlTest.SelectedItem.Value.ToString() == "Pending")
        {
            sbMst.Append(" AND PAYMENT_DATE is  null  ");
        }


        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());

        if (dtReprtSource.Rows.Count > 0)
        {
            leftDataGrid.DataSource = dtReprtSource;
            leftDataGrid.DataBind();
            ddlTest.Visible = true;
            ButtonExport.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);

        }


    }

    protected void ExportButton_Click(object sender, EventArgs e)
    {

        Session["recordDateFrom"] = recordDateFromTextBox.Text.ToString();
        Session["recordDateTo"] = recordDateToTextBox.Text.ToString();
        Session["agmDateFrom"] = agmDateFromTextBox.Text.ToString();
        Session["agmDateTo"] = agmDateToTextBox.Text.ToString();
        Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
        Session["compcd"] = companyNameDropDownList.SelectedValue.ToString();

        string rF = ddlTest.SelectedItem.Value.ToString();
        Session["rF"] = rF.ToString();

        //ClientScript.RegisterStartupScript(this.GetType(), "ReceivableCashDividendReportViewer", "window.open('ReportViewer/ReceivableCashDividendReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/DividendStatusReportViewer.aspx");
    }

    protected void radio_CheckedChanged(object sender, EventArgs e)
    {
        if (allRadioButton.Checked)
        {
            Session["statementType"] = "ALL";
        }
        else if (ReceviedRadioButton.Checked)
        {
            Session["statementType"] = "Recevied";
        }
        else if (pendingRadioButton.Checked)
        {
            Session["statementType"] = "Pending";
        }
    }
}
