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
        DataTable dtSectorNameDropDownList = dropDownListObj.FillSectorDropDownList();

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


            sectorDropDownList.DataSource = dtSectorNameDropDownList;
            sectorDropDownList.DataTextField = "SECT_MAJ_NM";
            sectorDropDownList.DataValueField = "SECT_MAJ_CD";
            sectorDropDownList.DataBind();


        }

    }


    protected void radio_CheckedChanged(object sender, EventArgs e)
    {
        if (fundwiseRadioButton.Checked)
        {
            //RIssuefromTextBox.Text = string.Empty;
            //RIssueToTextBox.Text = string.Empty;
            fundNameDropDownList.SelectedValue = "0";
            companyNameDropDownList.SelectedValue = "0";
            transTypeDropDownList.SelectedValue = "0";
            sectorDropDownList.SelectedValue = "0";
            fundNameDropDownListlabel.Visible = true;
            fundNameDropDownList.Visible = true;
            LabelSector.Visible = false;
            companyNameDropDownListlabel.Visible = false;
            companyNameDropDownList.Visible = false;
            transTypeDropDownListLabel.Visible = false;
            transTypeDropDownList.Visible = false;
            LabelSector.Visible = false;
            sectorDropDownList.Visible = false;
        }
        else if (CompanyWiseRadioButton.Checked)
        {
            //RIssuefromTextBox.Text = string.Empty;
            //RIssueToTextBox.Text = string.Empty;
            fundNameDropDownList.SelectedValue = "0";
            companyNameDropDownList.SelectedValue = "0";
            transTypeDropDownList.SelectedValue = "0";
            sectorDropDownList.SelectedValue = "0";

            companyNameDropDownListlabel.Visible = true;
            companyNameDropDownList.Visible = true;

            fundNameDropDownListlabel.Visible = false;
            fundNameDropDownList.Visible = false;
            transTypeDropDownListLabel.Visible = false;
            transTypeDropDownList.Visible = false;
            LabelSector.Visible = false;
            sectorDropDownList.Visible = false;
        }
        else if (allRadioButton.Checked)
        {
            //RIssuefromTextBox.Text = string.Empty;
            //RIssueToTextBox.Text = string.Empty;
            fundNameDropDownList.SelectedValue = "0";
            companyNameDropDownList.SelectedValue = "0";
            transTypeDropDownList.SelectedValue = "0";
            sectorDropDownList.SelectedValue = "0";

            fundNameDropDownListlabel.Visible = false;
            fundNameDropDownList.Visible = false;
            companyNameDropDownListlabel.Visible = false;
            companyNameDropDownList.Visible = false;
            transTypeDropDownListLabel.Visible = false;
            transTypeDropDownList.Visible = false;
            LabelSector.Visible = false;
            sectorDropDownList.Visible = false;
        }
        else if (CompanywiseallRadioButton.Checked)
        {
            //RIssuefromTextBox.Text = string.Empty;
            //RIssueToTextBox.Text = string.Empty;
            fundNameDropDownList.SelectedValue = "0";
            companyNameDropDownList.SelectedValue = "0";
            transTypeDropDownList.SelectedValue = "0";
            sectorDropDownList.SelectedValue = "0";

            fundNameDropDownListlabel.Visible = true;
            fundNameDropDownList.Visible = true;
            companyNameDropDownListlabel.Visible = false;
            companyNameDropDownList.Visible = false;
            LabelSector.Visible = false;
            sectorDropDownList.Visible = false;
            transTypeDropDownListLabel.Visible = true;
            transTypeDropDownList.Visible = true;
        }
        else if (SectorWiseallRadioButton.Checked)
        {
            fundNameDropDownList.SelectedValue = "0";
            companyNameDropDownList.SelectedValue = "0";
            transTypeDropDownList.SelectedValue = "0";
            sectorDropDownList.SelectedValue = "0";

            fundNameDropDownListlabel.Visible = false;
            fundNameDropDownList.Visible = false;
            companyNameDropDownListlabel.Visible = false;
            companyNameDropDownList.Visible = false;
            transTypeDropDownListLabel.Visible = true;
            transTypeDropDownList.Visible = true;
            LabelSector.Visible = true;
            sectorDropDownList.Visible = true;
        }

    }


    protected void showButton_Click(object sender, EventArgs e)
    {

        DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
        DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


        string p1date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
        string p2date = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");
      //  Session["Fromdate"] = p1date;
      //  Session["Todate"] = p2date;
        //Session["fundCodes"] = fundNameDropDownList.SelectedValue.ToString();
        //Session["companycode"] = companyNameDropDownList.SelectedValue.ToString();
        //Session["transtype"] = transTypeDropDownList.SelectedValue.ToString();

        string fundcode = fundNameDropDownList.SelectedValue.ToString();
        string companycode= companyNameDropDownList.SelectedValue.ToString();
        string transtype = transTypeDropDownList.SelectedValue.ToString();
        string sector = sectorDropDownList.SelectedValue.ToString();
        Session["sectorName"] = sectorDropDownList.SelectedItem.Text.ToString(); 


        //sb.Append("window.open('ReportViewer/NegativeBalanceCheckReportViewer.aspx?p1date=" + p1date + "&p2date= " + p2date + "');");



        if (fundcode == "0" && companycode == "0" && transtype == "0" && sector == "0")
        {
            Response.Redirect("ReportViewer/SellBuyCheckReportViwer.aspx?p1date=" + p1date + "&p2date= " + p2date + "&fundcode= " + fundcode + "&companycode= " + companycode + "&transtype= " + transtype + "&sector= " + sector + "");
        } 
        else if (fundcode != "0" && companycode == "0" && transtype == "0" && sector == "0")
        {
            Response.Redirect("ReportViewer/SellBuyCheckReportViwer.aspx?p1date=" + p1date + "&p2date= " + p2date + "&fundcode= " + fundcode + "&companycode= " + companycode + "&transtype= " + transtype + "&sector= " + sector + "");

        }
        else if (fundcode == "0" && companycode != "0" && transtype == "0" && sector == "0")
        {
            Response.Redirect("ReportViewer/SellBuyCheckReportViwer.aspx?p1date=" + p1date + "&p2date= " + p2date + "&fundcode= " + fundcode + "&companycode= " + companycode + "&transtype= " + transtype + "&sector= " + sector + "");
        }
        else if (fundcode != "0" && companycode == "0" && transtype != "0" && sector == "0")
        {

            Response.Redirect("ReportViewer/SellBuyCheckReportViwer.aspx?p1date=" + p1date + "&p2date= " + p2date + "&fundcode= " + fundcode + "&companycode= " + companycode + "&transtype= " + transtype + "&sector= " + sector + "");
        }
        else if (fundcode == "0" && companycode == "0" && transtype != "0" && sector != "0" )
        {
            Response.Redirect("ReportViewer/SellBuyCheckReportViwer.aspx?p1date=" + p1date + "&p2date= " + p2date + "&fundcode= " + fundcode + "&companycode= " + companycode + "&transtype= " + transtype + "&sector= " + sector + "");
        }

        // ClientScript.RegisterStartupScript(this.GetType(), "SellBuyCheckReportViwer", "window.open('ReportViewer/SellBuyCheckReportViwer.aspx')", true);


    }




}

   