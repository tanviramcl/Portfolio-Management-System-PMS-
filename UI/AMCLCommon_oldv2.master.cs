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
using System.IO;

public partial class UI_AMCLCommon : System.Web.UI.MasterPage
{
   
    protected string text = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/ui/test.txt");
        TextReader reader = File.OpenText(path);
        text = reader.ReadToEnd();

        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
        string loginId = Session["UserID"].ToString();
        string LoginName = Session["UserName"].ToString();
        string userType = Session["UserType"].ToString();
   
        lblLoginName.Text = "Welcome" + "  " + "to" + " " + LoginName.ToString();
       
    }
}
