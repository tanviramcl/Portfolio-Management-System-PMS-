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

public partial class UI_MarketValuationWithNonListedSecuritesCompanyAndAllFunds : System.Web.UI.Page
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
        
        DataTable dtPortfolioAsOnDropDownList = dropDownListObj.BalanceDateDropDownList();
        if (!IsPostBack)
        {
            PortfolioAsOnDropDownList.DataSource = dtPortfolioAsOnDropDownList;
            PortfolioAsOnDropDownList.DataTextField = "Howla_Date";
            PortfolioAsOnDropDownList.DataValueField = "BAL_DT_CTRL";
            PortfolioAsOnDropDownList.DataBind();
            
            DataTable dtNoOfFunds = GetFundName();
          //  DataTable dtFund = obj.GetFundGridTable();

            if (dtNoOfFunds.Rows.Count > 0)
            {

                chkFunds.DataSource = dtNoOfFunds;
                chkFunds.DataValueField = "F_CD";
                chkFunds.DataTextField = "F_NAME";                
                chkFunds.DataBind();
                dvGridFund.Visible = true;
            }

            
            }
            else
            {
                dvGridFund.Visible = false;
            }
        }
    
    private DataTable GetFundName()
    {
        DataTable dtFundName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" SELECT     FUND.F_CD, FUND.F_NAME     FROM         FUND  ");
        sbMst.Append(" WHERE     IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_CD NOT IN('6','7')  ");
        sbOrderBy.Append(" ORDER BY FUND.F_CD ");

        sbMst.Append(sbOrderBy.ToString());
        dtFundName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtFundName"] = dtFundName;
        return dtFundName;
    }
    protected void showReportButton_Click(object sender, EventArgs e)
    {
    Session["fundCodes"] = SelectFundCode();

        if (string.IsNullOrEmpty(Session["fundCodes"] as string))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please check mark at least one fund!');", true);
            dvGridFund.Visible = true;
        }
        else
        {

            Session["PortfolioAsOnDate"] = PortfolioAsOnDropDownList.SelectedValue.ToString();
            //Session["percentageCheck"] = percentageTextBox.Text.ToString();
            //Session["companyCodes"] = companyCodeTextBox.Text.ToString();
            dvGridFund.Visible = true;
            //ClientScript.RegisterStartupScript(this.GetType(), "MarketValuationWithNonListedSecuritesCompanyAndAllFunds", "window.open('ReportViewer/MarketValuationWithNonListedSecuritesCompanyAndAllFundsReportViewer.aspx')", true);
            Response.Redirect("ReportViewer/MarketValuationWithNonListedSecuritesCompanyAndAllFundsReportViewer.aspx");
        }
    }
    //private string SelectFundCode()
    //{
    //    DataTable dtFundName = (DataTable)Session["dtFundName"];
    //    string fundCode = "";
    //    int loop = 0;

    //    foreach (DataGridItem growFund in grdShowFund.Items)
    //    {
    //        CheckBox chkFundItem = (CheckBox)growFund.FindControl("chkFund");
    //        if (chkFundItem.Checked)
    //        {
    //            if (fundCode.ToString() == "")
    //            {
    //                fundCode = dtFundName.Rows[loop]["F_CD"].ToString();
    //            }
    //            else
    //            {
    //                fundCode = fundCode + "," + dtFundName.Rows[loop]["F_CD"].ToString();
    //            }
    //        }
    //        loop++;
    //    }
    //    return fundCode;
    //}

    private string SelectFundCode()
    {
        DataTable dtFundName = (DataTable)Session["dtFundName"];
        string fundCode = "";
        int loop = 0;

        for (int i = 0; i < chkFunds.Items.Count; i++)
        {
            if (chkFunds.Items[i].Selected)
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



        //string k = "";
        //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        //{
        //    if (CheckBoxList1.Items[i].Selected)
        //    {

        //        k = k + CheckBoxList1.Items[i].Text + "</br>";
        //    }

        //}
        //lbmsg.Text = k;
        //lbmsg.ForeColor = System.Drawing.Color.ForestGreen;



    }
    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}
