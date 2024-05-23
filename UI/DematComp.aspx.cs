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
        DataTable dtpdateDropDownList = pdateDropDownList();

        if (!IsPostBack)
        {
            
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            DataTable dtNoOfFunds = GetFundName();
            if (dtNoOfFunds.Rows.Count > 0)
            {
                
                chkFruits.DataSource = dtNoOfFunds;
                chkFruits.DataValueField = "F_CD";
                chkFruits.DataTextField = "F_NAME";

                chkFruits.DataBind();

                //int fundSerial = 1;
                dvGridFund.Visible = true;
                

            }


        }
        else
        {
            dvGridFund.Visible = false;
        }


    }

 

    protected void showButton_Click(object sender, EventArgs e)
    {
        // string fundcode = fundNameDropDownList.SelectedValue.ToString();
        //  string p1date1 = RIssuefromTextBox.Text.ToString();
        //string p2date = RIssueToTextBox.Text.ToString();

        Session["fundCodes"] = SelectFundCode();

        if (string.IsNullOrEmpty(Session["fundCodes"] as string))
        {
            lblheading.Visible = true;
            lblheading.Text = "Please check mark at least one fund!";
            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please check mark at least one fund!');", true);
            dvGridFund.Visible = true;
        }
        else
        {

            DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
            DateTime date2 = DateTime.ParseExact(RIssueToTextBox.Text, "dd/MM/yyyy", null);


            string p1date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
            string p2date = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");
            // string companycode = companyNameDropDownList.SelectedValue.ToString();


            Session["Fromdate"] = p1date;
            Session["Todate"] = p2date;
            Session["companycode"] = companyNameDropDownList.SelectedValue.ToString();
            Session["CompanyName"] = companyNameDropDownList.SelectedItem.Text.ToString();



            Response.Redirect("ReportViewer/DematCompReportViewer.aspx");
        }



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
    private DataTable GetFundName()
    {
        DataTable dtFundName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" SELECT     FUND.F_CD, FUND.F_NAME     FROM         FUND  ");
        sbMst.Append(" WHERE    IS_F_CLOSE IS NULL AND BOID IS NOT NULL ");
        sbOrderBy.Append(" ORDER BY FUND.F_CD ");

        sbMst.Append(sbOrderBy.ToString());
        dtFundName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtFundName"] = dtFundName;
        return dtFundName;
    }

    private string SelectFundCode()
    {
        DataTable dtFundName = (DataTable)Session["dtFundName"];
        string fundCode = "";
        int loop = 0;

        for (int i = 0; i < chkFruits.Items.Count; i++)
        {
            if (chkFruits.Items[i].Selected)
            {
                if (fundCode.ToString() == "")
                {
                    fundCode = dtFundName.Rows[loop]["F_CD"].ToString();
                }
                else
                {
                    fundCode = fundCode + "," + dtFundName.Rows[loop]["F_CD"].ToString();
                }
            }
            loop++;
        }
        return fundCode;

    }


}

   