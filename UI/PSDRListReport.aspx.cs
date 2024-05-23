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
    CommonGateway commonGatewayObj = new CommonGateway();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();

        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();

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

        string fundcode = fundNameDropDownList.SelectedValue.ToString();
        string companycode = companyNameDropDownList.Text.ToString();
       

        Session["fundcode"] = fundcode;
        Session["companycode"] = companycode;
       
        Session["CompanyName"] = companyNameDropDownList.SelectedItem.Text.ToString();
        //  ClientScript.RegisterStartupScript(this.GetType(), "DematListReportVeiwer", "window.open('ReportViewer/DematListReportVeiwer.aspx')", true);

        Response.Redirect("ReportViewer/PSDRListReportVeiwer.aspx");
    }

}