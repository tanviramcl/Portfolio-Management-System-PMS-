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
using System.Text;

public partial class UI_AMCLCommon : System.Web.UI.MasterPage
{
    CommonGateway commonGatewayObj = new CommonGateway();
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
        string childOfsubmenu="";

     //   Session["menu_list"] = menu_list();

        DataTable dtUserPermmitedMenu = Usermenu_Permission(loginId);

        for (int i = 0; i < dtUserPermmitedMenu.Rows.Count; i++)
        {
            if (childOfsubmenu == "")
            {
                childOfsubmenu = "0";
            }
            childOfsubmenu = childOfsubmenu +","+ dtUserPermmitedMenu.Rows[i]["MENU_ID"].ToString();
        }
        Session["Child_of_submenu"] = Get_Child_of_submenu(childOfsubmenu);

    }

    public DataTable Usermenu_Permission(string loginId)
    {
        DataTable dtMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from MENUPERMISSIONS where USER_ID = '" + loginId + "'");

        sbMst.Append(sbOrderBy.ToString());
        dtMenUName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtMenuName"] = dtMenUName;
        return dtMenUName;
    }

    public DataTable Get_Child_of_submenu(string childOfsubmenu)
    {
        DataTable dtChild_of_submenu = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" SELECT * FROM CHILD_OF_SUBMENU WHERE CHILD_OF_SUBMENU_ID  IN(" + childOfsubmenu + ") order by CHILD_OF_SUBMENU_ID; ");

        sbMst.Append(sbOrderBy.ToString());
        dtChild_of_submenu = commonGatewayObj.Select(sbMst.ToString());

        Session["dtChild_of_submenu"] = dtChild_of_submenu;
        return dtChild_of_submenu;
    }

    public DataTable  menu_list()
    {
        DataTable dtMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from Menu order by MENU_ID ");
       
        sbMst.Append(sbOrderBy.ToString());
        dtMenUName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtMenuName"] = dtMenUName;
        return dtMenUName;
    }

    public DataTable Sub_menu_list( string id)
    {
        DataTable dtSUBMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from submenu where MENU_ID= "+id+ " order by SUBMENU_ID asc ");

        sbMst.Append(sbOrderBy.ToString());
        dtSUBMenUName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtSubMenuName"] = dtSUBMenUName;
        return dtSUBMenUName;
    }

    public DataTable Childof_Sub_menu_list(string menuId, string subMenuId)
    {
        DataTable dtMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from CHILD_OF_SUBMENU where MENU_ID= "+menuId+" and SUBMENU_ID="+subMenuId+" ");

        sbMst.Append(sbOrderBy.ToString());
        dtMenUName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtSubMenuName"] = dtMenUName;
        return dtMenUName;
    }

}
