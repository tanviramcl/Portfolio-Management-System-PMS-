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

public partial class UI_NAVvsMarketPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        string navDate = navDateTextBox.Text.ToString();
        string marketPriceDate = marketPriceTextBox.Text.ToString();
        string diffSellPurchaseAmount = diffBetnSellRepurchaseUnitTextBox.Text.ToString();

        StringBuilder sb = new StringBuilder();
        sb.Append("window.open('ReportViewer/NAVvsMarketPriceReportViewer.aspx?navDate=" + navDate + "&marketPriceDate= " + marketPriceDate + "&diffSellPurchaseAmount= " + diffSellPurchaseAmount + "');");
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
    }
}
