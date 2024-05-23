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

public partial class UI_Process : System.Web.UI.Page
{   
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO seqObj = new Pf1s1DAO();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        MaxDate();        
    }

    private void MaxDate()
    {
        StringBuilder maxDateSelectString = new StringBuilder();
        DataTable dtMaxDseClosingPriceDate = new DataTable();
        maxDateSelectString.Append("SELECT MAX(TRANS_DT) AS MAX_DATE FROM DSE_CLOSING_PRICE");
        dtMaxDseClosingPriceDate = commonGatewayObj.Select(maxDateSelectString.ToString());
        if (dtMaxDseClosingPriceDate.Rows.Count > 0 && !(dtMaxDseClosingPriceDate.Rows[0]["MAX_DATE"].Equals(DBNull.Value)))
        {
            updateTillTextBox.Text = Convert.ToDateTime(dtMaxDseClosingPriceDate.Rows[0]["MAX_DATE"]).ToString("dd-MMM-yyyy");
        }
        else
        {
            updateTillTextBox.Text = "";
        }
    }
    
    protected void updatePriceButton_Click(object sender, EventArgs e)
    {
        string closingPriceDate = closingPriceDateTextBox.Text.ToString();

        if (closingPriceDate == updateTillTextBox.Text.ToString())
        {
            closingPriceDateTextBox.Focus();
            Response.Redirect("Process.aspx");
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Don't Click Reload/Refresh Button!! Closing Price Already Updated.');", true);
        }
        
        else
        {
            //string closingPriceDate = closingPriceDateTextBox.Text.ToString();
            DataTable dtSelectQuery = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbfilter = new StringBuilder();
            sbfilter.Append(" ");

            sbMst.Append("SELECT COMP.COMP_CD, PUB_TRANS.* FROM COMP INNER JOIN PUB_TRANS ON COMP.INSTR_CD = PUB_TRANS.INST_CD WHERE(PUB_TRANS.TRANS_DT ='" + Convert.ToDateTime(closingPriceDate).ToString("dd-MMM-yyyy") + "')");
            sbMst.Append(sbfilter.ToString());
            dtSelectQuery = commonGatewayObj.Select(sbMst.ToString());

            if (dtSelectQuery.Rows.Count > 0)
            {
                long id = 0;
                Hashtable htDseClosingPrice = new Hashtable();
                for (int looper = 0; looper < dtSelectQuery.Rows.Count; looper++)
                {
                    id = seqObj.Sequence("dse_closing_price_seq");
                    htDseClosingPrice = new Hashtable();
                    htDseClosingPrice.Add("ID", id);
                    htDseClosingPrice.Add("COMP_CD", Convert.ToInt32(dtSelectQuery.Rows[looper]["COMP_CD"].ToString()));
                    htDseClosingPrice.Add("INST_CD", dtSelectQuery.Rows[looper]["INST_CD"].ToString());
                    htDseClosingPrice.Add("OPEN", Convert.ToDecimal(dtSelectQuery.Rows[looper]["OPEN"].ToString()));
                    htDseClosingPrice.Add("HIGH", Convert.ToDecimal(dtSelectQuery.Rows[looper]["HIGH"].ToString()));
                    htDseClosingPrice.Add("LOW", Convert.ToDecimal(dtSelectQuery.Rows[looper]["LOW"].ToString()));
                    htDseClosingPrice.Add("CLOSE", Convert.ToDecimal(dtSelectQuery.Rows[looper]["CLOSE"].ToString()));
                    htDseClosingPrice.Add("CHG", Convert.ToDecimal(dtSelectQuery.Rows[looper]["CHG"].ToString()));
                    htDseClosingPrice.Add("TRADE", Convert.ToDecimal(dtSelectQuery.Rows[looper]["TRADE"].ToString()));
                    htDseClosingPrice.Add("VOL", Convert.ToDecimal(dtSelectQuery.Rows[looper]["VOL"].ToString()));
                    htDseClosingPrice.Add("VAL", Convert.ToDecimal(dtSelectQuery.Rows[looper]["VAL"].ToString()));
                    htDseClosingPrice.Add("TRANS_DT", Convert.ToDateTime(dtSelectQuery.Rows[looper]["TRANS_DT"]).ToString("dd-MMM-yyyy"));
                    commonGatewayObj.Insert(htDseClosingPrice, "DSE_CLOSING_PRICE");
                }

                MaxDate();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Closing Price Updeated Successfully!');", true);
                
                //ClientScript.RegisterStartupScript(this.GetType(), "download", "window.opener.fnRefresh();", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found!');", true);
            }
        }  
    }
}
