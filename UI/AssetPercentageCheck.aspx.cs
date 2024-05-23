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

public partial class UI_AssetPercentageCheck : System.Web.UI.Page
{
    DBConnector dbConectorObj = new DBConnector();
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO obj = new Pf1s1DAO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
    }
    protected void showTotalAssetButton_Click(object sender, EventArgs e)
    {
        Show();
    }
    void Show()
    {
        DataTable dtNoOfFunds = GetFundName();
        DataTable dtFund = obj.GetFundGridTable();

        if (dtNoOfFunds.Rows.Count > 0)
        {
            int fundSerial = 1;
            dvGridFund.Visible = true;
            DataRow drdtGridFund;
            for (int looper = 0; looper < dtNoOfFunds.Rows.Count; looper++)
            {
                drdtGridFund = dtFund.NewRow();
                drdtGridFund["SI"] = fundSerial;
                drdtGridFund["FUND_CODE"] = dtNoOfFunds.Rows[looper]["F_CD"].ToString().ToUpper();
                drdtGridFund["FUND_NAME"] = dtNoOfFunds.Rows[looper]["F_NAME"].ToString().ToUpper();
                drdtGridFund["ASSET_VALUE"] = ((dtNoOfFunds.Rows[looper]["ASSET_VALUE"]).Equals(DBNull.Value) || (dtNoOfFunds.Rows[looper]["ASSET_VALUE"]).Equals(null)) ? null : (dtNoOfFunds.Rows[looper]["ASSET_VALUE"]).ToString().ToUpper();
                dtFund.Rows.Add(drdtGridFund);
                fundSerial++;        
            }
            grdShowFund.DataSource = dtFund;
            grdShowFund.DataBind();
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

        sbMst.Append("SELECT     FUND.F_CD, FUND.F_NAME, DERIVEDTBL_NAV.ASSET_VALUE ");
        sbMst.Append(" FROM         FUND LEFT OUTER JOIN ");
        sbMst.Append(" (SELECT     NAV.NAV_MASTER.NAVFUNDID, SUM(NAV.NAV_DETAILS.COSTPRICE) AS ASSET_VALUE ");
        sbMst.Append(" FROM          NAV.NAV_MASTER INNER JOIN ");
        sbMst.Append("  NAV.NAV_DETAILS ON NAV.NAV_MASTER.NAVNO = NAV.NAV_DETAILS.NAVNO AND  ");
        sbMst.Append("  NAV.NAV_MASTER.NAVFUNDID = NAV.NAV_DETAILS.NAVFUNDID INNER JOIN ");
        sbMst.Append(" (SELECT     NAVFUNDID, MAX(NAVDATE) AS MAX_NAVDATE ");
        sbMst.Append(" FROM          NAV.NAV_MASTER NAV_MASTER_1 ");
        sbMst.Append(" WHERE      (NAVDATE <= '" + Convert.ToDateTime(transactionDateTextBox.Text).ToString("dd-MMM-yyyy") + "') ");
        sbMst.Append(" GROUP BY NAVFUNDID) DERIVEDTBL_1 ON NAV.NAV_MASTER.NAVFUNDID = DERIVEDTBL_1.NAVFUNDID AND  ");
        sbMst.Append("  NAV.NAV_MASTER.NAVDATE = DERIVEDTBL_1.MAX_NAVDATE ");
        sbMst.Append("  WHERE      (NAV.NAV_DETAILS.NAVROWTYPE = 'A')  ");
        sbMst.Append("  GROUP BY NAV.NAV_MASTER.NAVFUNDID) DERIVEDTBL_NAV ON FUND.F_CD = DERIVEDTBL_NAV.NAVFUNDID ");
        sbMst.Append(" WHERE     (FUND.F_CD BETWEEN 1 AND 32) ");
        sbMst.Append(" GROUP BY FUND.F_CD, FUND.F_NAME, DERIVEDTBL_NAV.ASSET_VALUE ");
        sbOrderBy.Append(" ORDER BY  FUND.F_CD ");

        sbMst.Append(sbOrderBy.ToString());
        dtFundName = commonGatewayObj.Select(sbMst.ToString());

        return dtFundName;
    }
    protected void showReportButton_Click(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        sbFilter.Append(" 1=1 ");
        commonGatewayObj.DeleteByCommand(" ASSET_VALUE", sbFilter.ToString());
        commonGatewayObj.CommitTransaction();

        InsertAssetValue();
        dvGridFund.Visible = false;

        string transactionDate = transactionDateTextBox.Text.ToString();
        string percentageCheck = percentageTextBox.Text.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append("window.open('ReportViewer/AssetPercentageCheckReportViewer.aspx?transactionDate=" + transactionDate + "&percentageCheck=" + percentageCheck +"');");
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
   
    }
    /// <summary>
    /// Mathod for Inserting Individual Fund's Asset Value to the Database.
    /// </summary>
    void InsertAssetValue()
    {
        DataTable dtFundName = GetFundName();
        int iteration = 0;
        int noOfInsertRows = 0;
        int msg = 0;

        Hashtable htAssetValueInfo = new Hashtable();

        foreach (DataGridItem growFund in grdShowFund.Items)
        {
            CheckBox chkFundItem = (CheckBox)growFund.FindControl("chkFund");
            if (chkFundItem.Checked)
            {
                htAssetValueInfo = new Hashtable();
                htAssetValueInfo.Add("F_CD", Convert.ToInt32(dtFundName.Rows[iteration]["F_CD"].ToString()));
                htAssetValueInfo.Add("F_NAME", (dtFundName.Rows[iteration]["F_NAME"]).ToString());
                htAssetValueInfo.Add("ASSET_VALUE", Convert.ToDouble(((TextBox)growFund.FindControl("assetValueTextBox")).Text));

                try
                {
                    noOfInsertRows = noOfInsertRows + commonGatewayObj.Insert(htAssetValueInfo, "ASSET_VALUE");
                    msg = 1;
                }
                catch (Exception ex)
                {
                    commonGatewayObj.RollbackTransaction();
                    msg = 0;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Asset Value Insert Failed!!" + ex.Message.Replace("'", "").ToString() + "');", true);
                }
            }
            iteration++;
        }
        if (msg == 1)
        {
            commonGatewayObj.CommitTransaction();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + noOfInsertRows + " Asset Value Inserted Successfully');", true);
        }
    }
}
