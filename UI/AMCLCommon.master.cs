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
using System.Collections.Generic;
using System.Reflection;

public partial class UI_AMCLCommon : System.Web.UI.MasterPage
{
    CommonGateway commonGatewayObj = new CommonGateway();
    protected string text = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string referer = Request.ServerVariables["HTTP_REFERER"];
        if (string.IsNullOrEmpty(referer))
        {
            Session["UserID"] = null;
            Response.Redirect("../Default.aspx");
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
            childOfsubmenu = childOfsubmenu + "," + dtUserPermmitedMenu.Rows[i]["MENU_ID"].ToString();
        }
        Session["Child_of_submenu"] = Get_Child_of_submenu(childOfsubmenu);

    }

    public DataTable Usermenu_Permission(string loginId)
    {
        DataTable dtMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from MENUPERMISSIONS where USER_ID = '" + loginId + "' order by MENU_ID");

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

        sbMst.Append(" SELECT * FROM CHILD_OF_SUBMENU WHERE CHILD_OF_SUBMENU_ID  IN(" + childOfsubmenu + ") order by CHILD_OF_SUBMENU_ID ");

        sbMst.Append(sbOrderBy.ToString());
        dtChild_of_submenu = commonGatewayObj.Select(sbMst.ToString());

        Session["dtChild_of_submenu"] = dtChild_of_submenu;
        return dtChild_of_submenu;
    }

    public DataTable  menu_list(string id)
    {
        DataTable dtMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from Menu where MENU_ID  IN(" + id + ") order by MENU_ID ");
       
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

        sbMst.Append(" select * from submenu where SUBMENU_ID IN (" + id+ ") order by SUBMENU_ID asc ");

        sbMst.Append(sbOrderBy.ToString());
        dtSUBMenUName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtSubMenuName"] = dtSUBMenUName;
        return dtSUBMenUName;
    }

    public DataTable GetSub_menu_byMenuId(string subMenuIds,string  menuId)
    {
        DataTable dtSUBMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from submenu where SUBMENU_ID IN (" + subMenuIds + ") and menu_id="+menuId+" order by SUBMENU_ID asc ");

        sbMst.Append(sbOrderBy.ToString());
        dtSUBMenUName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtSubMenuName"] = dtSUBMenUName;
        return dtSUBMenUName;
    }
  
    public DataTable Childof_Sub_menu_list(string childOfSubMenuList, string menuId, string subMenuId)
    {
        DataTable dtMenUName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from CHILD_OF_SUBMENU where CHILD_OF_SUBMENU_ID IN ("+ childOfSubMenuList + ") and MENU_ID= " + menuId+" and SUBMENU_ID="+subMenuId+"  order by CHILD_OF_SUBMENU_ID ");

        sbMst.Append(sbOrderBy.ToString());
        dtMenUName = commonGatewayObj.Select(sbMst.ToString());

        Session["dtSubMenuName"] = dtMenUName;
        return dtMenUName;
    }
    public class SubMenu
    {
        public string SUBMENU_ID { get; set; }
        public string SUBMENU_NAME { get; set; }
        public string MENU_ID { get; set; }
    }

}
