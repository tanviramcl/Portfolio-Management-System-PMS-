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

public partial class UI_CompanyWiseAllPortfoliosReportDSEonly : System.Web.UI.Page
{
    DBConnector dbConectorObj = new DBConnector();
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    Pf1s1DAO obj = new Pf1s1DAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        
        DataTable dtHowlaDateDropDownList = dropDownListObj.BalanceDateDropDownList();
        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtlistedCategoryDropDownList = dropDownListObj.FilllistedCategoryDropDownList();
        DataTable dtSectorNameDropDownList = dropDownListObj.FillSectorDropDownList();
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {

            portfolioAsOnDropDownList.DataSource = dtHowlaDateDropDownList;
            portfolioAsOnDropDownList.DataTextField = "Howla_Date";
            portfolioAsOnDropDownList.DataValueField = "BAL_DT_CTRL";
            portfolioAsOnDropDownList.DataBind();

         

          

            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();


        }
           
   
    }
  
    protected void showButton_Click(object sender, EventArgs e)
    {
        
        Session["fundCodes"] = fundNameDropDownList.SelectedValue.ToString();
        Session["howlaDate"] = portfolioAsOnDropDownList.SelectedValue.ToString();
        
        


        // ClientScript.RegisterStartupScript(this.GetType(), "ReceivableCashDividendReportViewer", "window.open('ReportViewer/CompanyWiseAllPortfoliosReportDSEonlyReportViewer.aspx')", true);
        Response.Redirect("ReportViewer/SheduleOfInvesmentInDEbtSecuritiesReportViewer.aspx");
    }
   

}
