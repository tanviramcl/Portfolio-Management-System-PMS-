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
        DataTable dtpdateDropDownList = pdateNegativeBalanceCheckDropDownList();
        if (!IsPostBack)
        {
            p1dateDropDownList.DataSource = dtpdateDropDownList;
            p1dateDropDownList.DataTextField = "p1date";
            p1dateDropDownList.DataValueField = "p1date";
            p1dateDropDownList.DataBind();

            p2dateDropDownList.DataSource = dtpdateDropDownList;
            p2dateDropDownList.DataTextField = "p2date";
            p2dateDropDownList.DataValueField = "p2date";
            p2dateDropDownList.DataBind();
        }

    }

    protected void showButton_Click(object sender, EventArgs e)
    {
        string p1date = p1dateDropDownList.Text.ToString();
        string p2date = p2dateDropDownList.Text.ToString();


        StringBuilder sb = new StringBuilder();
        //sb.Append("window.open('ReportViewer/NegativeBalanceCheckReportViewer.aspx?p1date=" + p1date + "&p2date= " + p2date + "');");
        //ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
        Response.Redirect("ReportViewer/NegativeBalanceCheckReportViewer.aspx?p1date=" + p1date + "&p2date=" + p2date);

    }

    public DataTable pdateNegativeBalanceCheckDropDownList()
    {

        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable pdate = commonGatewayObj.Select("select distinct bal_dt from fund_folio_hb order by bal_dt desc");
        DataTable pdateNegativeBalanceCheckDropDownList = new DataTable();
        pdateNegativeBalanceCheckDropDownList.Columns.Add("p1date", typeof(string));
        pdateNegativeBalanceCheckDropDownList.Columns.Add("p2date", typeof(string));
        DataRow dr = pdateNegativeBalanceCheckDropDownList.NewRow();

        for (int loop = 0; loop < pdate.Rows.Count; loop++)
        {
            dr = pdateNegativeBalanceCheckDropDownList.NewRow();
            dr["p1date"] = Convert.ToDateTime(pdate.Rows[loop]["bal_dt"]).ToString("dd-MMM-yyyy");
            dr["p2date"] = Convert.ToDateTime(pdate.Rows[loop]["bal_dt"]).ToString("dd-MMM-yyyy");
            pdateNegativeBalanceCheckDropDownList.Rows.Add(dr);
        }
        return pdateNegativeBalanceCheckDropDownList;
    }




    protected void p1dateDropDownListNegativeBalanceCheck_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void p2dateDropDownListNegativeBalanceCheck_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}