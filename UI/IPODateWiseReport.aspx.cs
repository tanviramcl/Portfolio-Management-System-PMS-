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

public partial class UI_IPODateWiseReport : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    //DropDownList dropDownListObj = new DropDownList();
    //Pf1s1DAO pf1s1DAOObj = new Pf1s1DAO();

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
        string howlaDateFrom = howlaDateFromTextBox.Text.ToString();
        string howlaDateTo = howlaDateToTextBox.Text.ToString();

        Response.Redirect("ReportViewer/IPODateWiseReportViewer.aspx?howlaDateFrom=" + howlaDateFrom + "&howlaDateTo=" + howlaDateTo);
    }
}
