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

public partial class UI_GeneralReport : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCompanyNameDropDownList();
            FillSectorDropDownList();
            FillMarketCategoryDropDownList();
        }
    }
    private void FillCompanyNameDropDownList()
    {
        DataTable dtCompName = commonGatewayObj.Select("SELECT COMP_NM, COMP_CD FROM COMP ORDER BY COMP_NM");
        DataTable dtCompNameDropDownList = new DataTable();
        dtCompNameDropDownList.Columns.Add("COMP_NM", typeof(string));
        dtCompNameDropDownList.Columns.Add("COMP_CD", typeof(string));
        DataRow dr = dtCompNameDropDownList.NewRow();
        dr["COMP_NM"] = "--Click Here to Select--";
        dr["COMP_CD"] = "0";
        dtCompNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtCompName.Rows.Count; loop++)
        {
            dr = dtCompNameDropDownList.NewRow();
            dr["COMP_NM"] = dtCompName.Rows[loop]["COMP_NM"].ToString();
            dr["COMP_CD"] = Convert.ToInt32(dtCompName.Rows[loop]["COMP_CD"]);
            dtCompNameDropDownList.Rows.Add(dr);
        }

        companyNameDropDownList.DataSource = dtCompNameDropDownList;
        companyNameDropDownList.DataTextField = "COMP_NM";
        companyNameDropDownList.DataValueField = "COMP_CD";
        companyNameDropDownList.DataBind();
    }
    private void FillSectorDropDownList()
    {

        DataTable dtSectorName = commonGatewayObj.Select("SELECT * FROM SECT_MAJ ORDER BY SECT_MAJ_NM");
        DataTable dtSectorNameDropDownList = new DataTable();
        dtSectorNameDropDownList.Columns.Add("SECT_MAJ_NM", typeof(string));
        dtSectorNameDropDownList.Columns.Add("SECT_MAJ_CD", typeof(string));
        DataRow dr = dtSectorNameDropDownList.NewRow();
        dr["SECT_MAJ_NM"] = "--Click Here to Select--";
        dr["SECT_MAJ_CD"] = "0";
        dtSectorNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtSectorName.Rows.Count; loop++)
        {
            dr = dtSectorNameDropDownList.NewRow();
            dr["SECT_MAJ_NM"] = dtSectorName.Rows[loop]["SECT_MAJ_NM"].ToString();
            dr["SECT_MAJ_CD"] = Convert.ToInt32(dtSectorName.Rows[loop]["SECT_MAJ_CD"]);
            dtSectorNameDropDownList.Rows.Add(dr);
        }

        sectorDropDownList.DataSource = dtSectorNameDropDownList;
        sectorDropDownList.DataTextField = "SECT_MAJ_NM";
        sectorDropDownList.DataValueField = "SECT_MAJ_CD";
        sectorDropDownList.DataBind();

    }

    private void FillMarketCategoryDropDownList()
    {
        DataTable dtMarketCategory = commonGatewayObj.Select("SELECT DISTINCT MARKET_CATEGORY FROM ANALYSIS_MST ORDER BY MARKET_CATEGORY");
        DataTable dtMarketCategoryDropDownList = new DataTable();
        dtMarketCategoryDropDownList.Columns.Add("MARKET_CATEGORY", typeof(string));
        dtMarketCategoryDropDownList.Columns.Add("MARKET_CATEGORY1", typeof(string));
        DataRow dr = dtMarketCategoryDropDownList.NewRow();
        dr["MARKET_CATEGORY"] = "-Select-";
        dr["MARKET_CATEGORY1"] = "0";
        dtMarketCategoryDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtMarketCategory.Rows.Count; loop++)
        {
            dr = dtMarketCategoryDropDownList.NewRow();
            dr["MARKET_CATEGORY"] = dtMarketCategory.Rows[loop]["MARKET_CATEGORY"].ToString();
            dr["MARKET_CATEGORY1"] = dtMarketCategory.Rows[loop]["MARKET_CATEGORY"].ToString();
            dtMarketCategoryDropDownList.Rows.Add(dr);
        }

        marketCategoryDropDownList.DataSource = dtMarketCategoryDropDownList;
        marketCategoryDropDownList.DataTextField = "MARKET_CATEGORY";
        marketCategoryDropDownList.DataValueField = "MARKET_CATEGORY1";
        marketCategoryDropDownList.DataBind();
    }
    protected void ShowButton_Click(object sender, EventArgs e)
    {
        string closingPriceDateFrom = fromDateTextBox.Text.ToString();
        string closingPriceDateEnd = toDateTextBox.Text.ToString();
        string yearEndFrom = fromYearEndTextBox.Text.ToString();
        string yearEndTo = toYearEndTextBox.Text.ToString();
        int sectorCode = Convert.ToInt32(sectorDropDownList.SelectedValue);
        string marketCategory = Convert.ToString(marketCategoryDropDownList.SelectedValue);
        string orderby = Convert.ToString(OrderByDropDownList.SelectedValue);

        Response.Redirect("ReportViewer/GeneralReportViewer.aspx?closingPriceDateFrom=" + closingPriceDateFrom + "&closingPriceDateEnd=" + closingPriceDateEnd + "&yearEndFrom=" + yearEndFrom + "&yearEndTo=" + yearEndTo + "&sectorCode=" + sectorCode + "&marketCategory=" + marketCategory + "&orderby=" + orderby);
    }
}
