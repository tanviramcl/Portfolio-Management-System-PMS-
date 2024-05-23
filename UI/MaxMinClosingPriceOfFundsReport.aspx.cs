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

public partial class UI_MaxMinClosingPriceOfFundsReport : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
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
        string fyClosingPriceDateFrom = closingPriceDateFromTextBox.Text.ToString();
        string fyClosingPriceDateTo = closingPriceDateToTextBox.Text.ToString();
        string latestClosingPriceDate = closingPriceDateTextBox.Text.ToString();

        Response.Redirect("ReportViewer/MaxMinClosingPriceOfFundsReportViewer.aspx?fyClosingPriceDateFrom=" + fyClosingPriceDateFrom + "&fyClosingPriceDateTo=" + fyClosingPriceDateTo + "&latestClosingPriceDate=" + latestClosingPriceDate);
    }
}
