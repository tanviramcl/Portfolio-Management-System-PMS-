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

public partial class UI_MarketValuationWithProfitLoss : System.Web.UI.Page
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
        
        if (!IsPostBack)
        {

            
            DataTable dtNoOfFunds = GetFundName();
          //  DataTable dtFund = obj.GetFundGridTable();

            if (dtNoOfFunds.Rows.Count > 0)
            {


                //foreach (DataRow dr in dtNoOfFunds.Rows)
                //{
                //    ListItem newItem = new ListItem(dr["F_CD"].ToString()+ dr["F_NAME"].ToString());
                //    chkFruits.Items.Add(newItem);
                //}

                chkFruits.DataSource = dtNoOfFunds;
                chkFruits.DataValueField = "F_CD";
                chkFruits.DataTextField = "F_NAME";
                
                chkFruits.DataBind();

                //int fundSerial = 1;
                dvGridFund.Visible = true;
                //DataRow drdtGridFund;
                //for (int looper = 0; looper < dtNoOfFunds.Rows.Count; looper++)
                //{
                //    drdtGridFund = dtFund.NewRow();
                //    drdtGridFund["SI"] = fundSerial;
                //    drdtGridFund["FUND_CODE"] = dtNoOfFunds.Rows[looper]["F_CD"].ToString().ToUpper();
                //    drdtGridFund["FUND_NAME"] = dtNoOfFunds.Rows[looper]["F_NAME"].ToString().ToUpper();
                //    dtFund.Rows.Add(drdtGridFund);
                //    fundSerial++;


                //chkFruits.DataSource = dtNoOfFunds;
                //chkFruits.DataBind();


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
        sbMst.Append(" WHERE     IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_CD NOT IN('6','7')");
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

            Session["FromDate"] = FromDateTextBox.Text.ToString();
            Session["ToDate"] = ToDateTextBox.Text.ToString();
           
          

              
            dvGridFund.Visible = true;
            //    ClientScript.RegisterStartupScript(this.GetType(), "CapitalGainAllFundsReportViewer", "window.open('ReportViewer/CapitalGainAllFundsReportViewer.aspx')", true);
            Response.Redirect("ReportViewer/CapitalGainAllFundsReportViewer.aspx");
        }
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
