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
        DataTable dtSectorNameDropDownList = dropDownListObj.FillSectorDropDownList();
        if (!IsPostBack)
        {
            sectorDropDownList.DataSource = dtSectorNameDropDownList;
            sectorDropDownList.DataTextField = "SECT_MAJ_NM";
            sectorDropDownList.DataValueField = "SECT_MAJ_CD";
            sectorDropDownList.DataBind();
        }

    }

    protected void showButton_Click(object sender, EventArgs e)
    {


        string sector = sectorDropDownList.SelectedValue.ToString();
        string category = categoryDropDownList.SelectedValue.ToString();
        string group = groupDropDownList.SelectedValue.ToString();
        string ipo = IPODropDownList.SelectedValue.ToString();
        string marketype = marketDropDownList.SelectedValue.ToString();



        //if (sector == "0" && category == "0" && group == "0" && ipo =="0")
        //{
        //    Response.Redirect("ReportViewer/CompanyInfomationReportViewer.aspx?sector=" + sector + "&category= " + category + "&group= " + group + "&ipo= " + ipo + "&marketype= " + marketype + "");
        //}
        //else if (sector != "0" && category == "0" && group == "0" && ipo == "0")
        //{
        //    Response.Redirect("ReportViewer/CompanyInfomationReportViewer.aspx?sector=" + sector + "&category= " + category + "&group= '" + group + "'&ipo= " + ipo + "&marketype= " + marketype + "");

        //}
        //else if (sector == "0" && category != "0" && group == "0" && ipo == "0" )
        //{
        //    Response.Redirect("ReportViewer/CompanyInfomationReportViewer.aspx?sector=" + sector + "&category= " + category + "&group= '" + group + "' &ipo= " + ipo + "&marketype= " + marketype + "");
        //}
        //else if (sector == "0" && category == "0" && group != "0" && ipo == "0" )
        //{

        //    Response.Redirect("ReportViewer/CompanyInfomationReportViewer.aspx?sector=" + sector + "&category= " + category + "&group= '" + group + "' &ipo= " + ipo + "&marketype= " + marketype + "");
        //}
        //else if (sector == "0" && category == "0" && group == "0" && ipo != "0" )
        //{

        //    Response.Redirect("ReportViewer/CompanyInfomationReportViewer.aspx?sector=" + sector + "&category= " + category + "&group= '" + group + "' &ipo= " + ipo + "&marketype= " + marketype + "");
        //}
        //else if ( sector != "0" && category != "0" && group != "0" && ipo != "0")
        //{
            Response.Redirect("ReportViewer/CompanyInfomationReportViewer.aspx?sector=" + sector + "&category= " + category + "&group= " + group + " &ipo= " + ipo + "&marketype= " + marketype + "");
        //}

        //StringBuilder sb = new StringBuilder();
        ////sb.Append("window.open('ReportViewer/NegativeBalanceCheckReportViewer.aspx?p1date=" + p1date + "&p2date= " + p2date + "');");
        ////ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
        //Response.Redirect("ReportViewer/CompanyInfomationReportViewer.aspx");


    }

  





    
}