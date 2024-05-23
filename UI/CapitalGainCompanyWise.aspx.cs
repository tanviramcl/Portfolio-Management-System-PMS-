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
        DataTable dtpdateDropDownList = pdateDropDownList();

        if (!IsPostBack)
        {

            //fundNameDropDownList.DataSource = dtFundNameDropDownList;
            //fundNameDropDownList.DataTextField = "F_NAME";
            //fundNameDropDownList.DataValueField = "F_CD";
            //fundNameDropDownList.DataBind();


            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            //p1dateDropDownList.DataSource = dtpdateDropDownList;
            //p1dateDropDownList.DataTextField = "p1date";
            //p1dateDropDownList.DataValueField = "p1date";
            //p1dateDropDownList.DataBind();

            //p2dateDropDownList.DataSource = dtpdateDropDownList;
            //p2dateDropDownList.DataTextField = "p2date";
            //p2dateDropDownList.DataValueField = "p2date";
            //p2dateDropDownList.DataBind();




        }

    }

    protected void showButton_Click(object sender, EventArgs e)
    {
        // string fundcode = fundNameDropDownList.SelectedValue.ToString();
        //  string p1date1 = RIssuefromTextBox.Text.ToString();
        //string p2date = RIssueToTextBox.Text.ToString();
        DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
        DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


        string p1date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
        string p2date = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");
        string companycode = companyNameDropDownList.SelectedValue.ToString();
        Response.Redirect("ReportViewer/CapitalGainCompanyWiseReportViwer.aspx?companycode="+companycode+"&p1date=" + p1date + "&p2date=" + p2date + "");

    }

    public DataTable pdateDropDownList()//For Authorized Signatory
    {

        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable p1date = commonGatewayObj.Select("SELECT MIN( VCH_DT) as p1date FROM FUND_TRANS_HB");
        DataTable p2date = commonGatewayObj.Select("SELECT MAX(VCH_DT) as p2date FROM FUND_TRANS_HB");
        DataTable pdateDropDownList = new DataTable();
        pdateDropDownList.Columns.Add("p1date", typeof(string));
        pdateDropDownList.Columns.Add("p2date", typeof(string));
        DataRow dr = pdateDropDownList.NewRow();
        DataRow dr1 = pdateDropDownList.NewRow();

        for (int loop = 0; loop < p1date.Rows.Count; loop++)
        {
            //dr = pdateDropDownList.NewRow();
            dr["p1date"] = Convert.ToDateTime(p1date.Rows[loop]["p1date"]).ToString("dd-MMM-yyyy");
            // dr["p2date"] = Convert.ToDateTime(pdate.Rows[loop]["vch_dt"]).ToString("dd-MMM-yyyy");
            pdateDropDownList.Rows.Add(dr);
        }
        for (int loop = 0; loop < p2date.Rows.Count; loop++)
        {
            // dr = pdateDropDownList.NewRow();

            dr["p2date"] = Convert.ToDateTime(p2date.Rows[loop]["p2date"]).ToString("dd-MMM-yyyy");
            pdateDropDownList.Rows.Add(dr1);
        }
        return pdateDropDownList;
    }
}

   