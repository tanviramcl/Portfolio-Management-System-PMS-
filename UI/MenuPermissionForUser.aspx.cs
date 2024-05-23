using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
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

         

        

        Session["UserIdList"] = IsUesrPermittedUser();


        DataTable dtUserIdList = (DataTable)Session["UserIdList"];
      
        DataTable dt2 = new DataTable();
        DataTable dtUserList = new DataTable();

        if (dtUserIdList.Rows.Count > 0)
        {
            for (int i = 0; i < dtUserIdList.Rows.Count; i++)
            {
                dt2 = GetUserCountableMenu(dtUserIdList.Rows[i]["user_id"].ToString());

                dtUserList.Merge(dt2);

            }
        }
        Session["PermittedUser"] = dtUserList;

        DataTable dtpermitedUserlist = (DataTable)Session["PermittedUser"];
        
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {

      

    }

    private DataTable GetUserCountableMenu(string userId)
    {
        DataTable dtMenuCountableList = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append("select a.user_id,a.name,a.designation,b.permittedmenu  from (select user_id,name,designation from user_table)a,(select csmwithsm.USER_ID,count(*)as permittedmenu from ");
        sbMst.Append(" (select pcosm.USER_ID,pcosm.CHILD_OF_SUBMENU_ID,pcosm.CHILD_OF_SUBMENU_NAME,pcosm.URL,pcosm.SUBMENU_ID,sm.SUBMENU_NAME,pcosm.PermittedMenu_ID,pcosm.MENU_ID from   ");
        sbMst.Append(" (select USER_ID,CHILD_OF_SUBMENU_ID,CHILD_OF_SUBMENU_NAME,URL,SUBMENU_ID,PermittedMenu_ID,MENU_ID from (select MENU_ID as PermittedMenu_ID ,USER_ID  ");
        sbMst.Append(" from MENUPERMISSIONS where USER_ID='"+ userId + "') mp inner join  CHILD_OF_SUBMENU csm On mp.PermittedMenu_ID =csm.CHILD_OF_SUBMENU_ID) pcosm inner join submenu sm on  ");
        sbMst.Append(" pcosm.SUBMENU_ID=sm.SUBMENU_ID) csmwithsm inner join MENU menu On csmwithsm.MENU_ID=menu.MENU_ID group by user_id)b where a.user_id=b.user_id  order by a.user_id asc");


        sbMst.Append(sbOrderBy.ToString());
        dtMenuCountableList = commonGatewayObj.Select(sbMst.ToString());

        Session["dtUserCountableMenu"] = dtMenuCountableList;

        return dtMenuCountableList;
    }
    public DataTable IsUesrPermittedUser()
    {
        DataTable dtUserInfo = new DataTable();
        dtUserInfo = commonGatewayObj.Select(" SELECT distinct(user_id)user_id FROM MENUPERMISSIONS order by  user_id");
        if (dtUserInfo.Rows.Count > 0)
        {
           

            Session["UserIds"] = dtUserInfo;

            
        }
        return dtUserInfo;
    }
    protected void AssignMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssignMenuByUser.aspx");
    }


   
}