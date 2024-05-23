using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class UI_BalancechekReport : System.Web.UI.Page
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
        StringBuilder sb = new StringBuilder();
        sb.Append("window.open('ReportViewer/BalanceCheckReportViewer.aspx');");
        ClientScript.RegisterStartupScript(this.GetType(), "ReportViwer", sb.ToString(), true);
    }
}