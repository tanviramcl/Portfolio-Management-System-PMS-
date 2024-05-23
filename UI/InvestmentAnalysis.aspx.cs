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

       



        string fundcode = fundNameDropDownList.SelectedValue.ToString();
        string companycode= companyNameDropDownList.SelectedValue.ToString();
        Session["FundName"] = fundNameDropDownList.SelectedItem.Text.ToString();
        string formate_type = "";




       if (fundcode != "0"  && companycode != "0" )
       {
            Response.Redirect("ReportViewer/InvestmentAnalysisReportViwer.aspx?fundcode= " + fundcode + "&companycode= " + companycode + "&formate_type="+formate_type+"");
       }
        

    }




}

   