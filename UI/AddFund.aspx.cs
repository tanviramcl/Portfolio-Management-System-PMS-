using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_CompanyInformation : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        if (string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            // not there!
            Session["funds"] = GetFundName();

            DataTable dtNoOfFunds = (DataTable)Session["funds"];
            Button1.Visible = true;
            Button1.Text = "Save";
        }
        else
        {
            string fundID = Convert.ToString(Request.QueryString["ID"]).Trim();
            Session["fundsbyID"] = GetFundName_ByID(fundID);
            DataTable dtFundsbyID = (DataTable)Session["fundsbyID"];

            if (dtFundsbyID.Rows.Count > 0)
            {
                fundcodeTextBox.Text = dtFundsbyID.Rows[0]["F_CD"].ToString();
                txtfundName.Value = dtFundsbyID.Rows[0]["F_NAME"].ToString();
                FundTypeDropDownList.SelectedValue = dtFundsbyID.Rows[0]["F_TYPE"].ToString();
                customerCode.Text = dtFundsbyID.Rows[0]["CUSTOMER"].ToString();
                boIdTextBox.Text = dtFundsbyID.Rows[0]["BOID"].ToString();
                txtCompanyCode.Text = dtFundsbyID.Rows[0]["COMP_CD"].ToString();
                txtfundClose.Text = dtFundsbyID.Rows[0]["IS_F_CLOSE"].ToString();
                txtsellbuycommision.Text = dtFundsbyID.Rows[0]["SL_BUY_COM_PCT"].ToString();

                Button1.Visible = true;
                Button1.Text = "Update";
                //saveButton.Text = "Update";
            }
        }




        //  companyNameTextBox.Text = "sss";
    }


    protected void fundCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        DataTable dtgetfund;
        if (fundcodeTextBox.Text.ToString() != "")
        {
            string strfundcode = "SELECT  *  FROM    FUND WHERE    BOID IS NOT NULL  and f_cd = " + fundcodeTextBox.Text.ToString() + "";
            dtgetfund = commonGatewayObj.Select(strfundcode);
            if (dtgetfund != null && dtgetfund.Rows.Count > 0)
            {
                txtfundName.Value = dtgetfund.Rows[0]["F_NAME"].ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('This fund is already available !');", true);

            }
        }


    }

    private DataTable GetFundName()
    {
        DataTable dtFundName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");
        sbMst.Append(" SELECT     *     FROM         FUND  ");
        sbMst.Append(" WHERE    BOID IS NOT NULL ");
        sbOrderBy.Append(" ORDER BY FUND.F_CD ");

        sbMst.Append(sbOrderBy.ToString());
        dtFundName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtFundName"] = dtFundName;
        return dtFundName;
    }

    private DataTable GetFundName_ByID(string ID)
    {
        DataTable dtFundName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");
        sbMst.Append(" SELECT     *     FROM         FUND  ");
        sbMst.Append(" WHERE    BOID IS NOT NULL and F_CD=" + ID + " ");

        sbMst.Append(sbOrderBy.ToString());
        dtFundName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtFundName"] = dtFundName;
        return dtFundName;
    }
    private void ClearField()
    {

    }

    public class Fund
    {
        public int F_CD { get; set; }
        public string F_NAME { get; set; }
        public int COMP_CD { get; set; }
        public string F_TYPE { get; set; }
        public string IS_F_CLOSE { get; set; }
        public string CUSTOMER { get; set; }
        public string BOID { get; set; }
        public double SL_BUY_COM_PCT { get; set; }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
    }



    [System.Web.Services.WebMethod]

    public static bool InsertandUpdateFund(string FundId, string FundName, string FundType, string customerCode, string boId, string sellbuycommision, string CompanyCode, string fundClose)
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable dtgetfund;
        if (FundId != "")
        {
            string strfundcode = "SELECT  *  FROM    FUND WHERE    BOID IS NOT NULL  and f_cd = " + FundId + "";
            dtgetfund = commonGatewayObj.Select(strfundcode);
            if (dtgetfund != null && dtgetfund.Rows.Count > 0)
            {
                string strUPQuery = "update FUND set F_NAME='" + FundName + "',COMP_CD ='" + CompanyCode + "',F_TYPE ='" + FundType + "',IS_F_CLOSE='" + fundClose + "',CUSTOMER ='" + customerCode + "',BOID ='" + boId + "',SL_BUY_COM_PCT ='" + sellbuycommision + "' where F_CD =" + FundId + "";

                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUPQuery);
                
            }
            else
            {
              
                string strInsQuery;

                strInsQuery = "insert into Fund(F_CD,F_NAME,COMP_CD,F_TYPE,IS_F_CLOSE,CUSTOMER,BOID,SL_BUY_COM_PCT)values('" + FundId + "','" + FundName + "','" + CompanyCode + "','" + FundType + "','" + fundClose + "','" + customerCode + "','" + boId + "','" + sellbuycommision + "')";

                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);

            }
        }

        return true;

    }

}