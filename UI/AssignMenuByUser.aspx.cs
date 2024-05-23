using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_CompanyInformation : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DataTable dtUserDropDownList = dropDownListObj.UserDropDownList();
        if (!IsPostBack)
        {
            userDropDownList.DataSource = dtUserDropDownList;
            userDropDownList.DataTextField = "User_ID";
            userDropDownList.DataValueField = "ID";
            userDropDownList.DataBind();

            DataTable dtMenuList = GetMENU();
            if (dtMenuList.Rows.Count > 0)
            {
                chkFunds.DataSource = dtMenuList;
                chkFunds.DataValueField = "CHILD_OF_SUBMENU_ID";
                chkFunds.DataTextField = "CHILD_OF_SUBMENU_NAME";

                chkFunds.DataBind();

                //int fundSerial = 1;
                dvGridFund.Visible = true;
            }
        }
        else
        {
            dvGridFund.Visible = false;
        }


    }


    private DataTable GetMENU()
    {
        DataTable dtMenUList = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from (select sm.MENU_ID,menu.MENU_NAME,sm.SUBMENU_ID,sm.SUBMENU_NAME  from  (select * from Menu ) menu inner join SUBMENU sm on MENU.MENU_ID=SM.MENU_ID) msm inner join ");
        sbMst.Append(" CHILD_OF_SUBMENU cosm on msm.SUBMENU_ID=COSM.SUBMENU_ID  ");
        sbOrderBy.Append("  order by CHILD_OF_SUBMENU_ID asc ");

        sbMst.Append(sbOrderBy.ToString());
        dtMenUList = commonGatewayObj.Select(sbMst.ToString());

        Session["dtMenUList"] = dtMenUList;

        return dtMenUList;
    }
    protected void saveButton_Click(object sender, EventArgs e)
    {

        Session["MenUList"] = SelectUser();
        Session["UserIdSelected"] = userDropDownList.SelectedItem.Text.ToString();
        string menuIDs = "";
        string UserId = "";
        string strInsQuery;
        DataTable dtmenuExist;
        menuIDs = (string)Session["MenUList"];
        UserId = (string)Session["UserIdSelected"];

        if (string.IsNullOrEmpty(Session["MenUList"] as string))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please check mark at least one Menu');", true);
            dvGridFund.Visible = true;
        }
        else
        {
            

            List<string> menuList = menuIDs.Split(new char[] { ',' }).ToList();

            foreach (var menuid in menuList)
            {

                string strMenuExits = "SELECT  *  FROM    MENUPERMISSIONS WHERE    MENU_ID= " + menuid + "   and USER_ID = '" + UserId + "'";
                dtmenuExist = commonGatewayObj.Select(strMenuExits);




                if (dtmenuExist != null && dtmenuExist.Rows.Count > 0)
                {
                    string strUPQuery = "update MENUPERMISSIONS set MENU_ID='" + menuid + "' where USER_ID ='" + UserId + "' and MENU_ID='" + menuid + "'";

                    int upNumOfRows = commonGatewayObj.ExecuteNonQuery(strUPQuery);

                }
                else
                {
                    strInsQuery = "insert into MENUPERMISSIONS(MENU_ID,USER_ID)values('" + menuid + "','" + UserId + "')";

                    int inNumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                }


            }

            Response.Redirect("MenuPermissionForUser.aspx");

        }


    }

    private string SelectUser()
    {
        DataTable dtmenuId = (DataTable)Session["dtMenUList"];


        string MenuId = "";
        int loop = 0;

        for (int i = 0; i < chkFunds.Items.Count; i++)
        {
            if (chkFunds.Items[i].Selected)
            {
                if (MenuId.ToString() == "")
                {
                    MenuId = dtmenuId.Rows[loop]["CHILD_OF_SUBMENU_ID"].ToString();
                }
                else
                {
                    MenuId = MenuId + "," + dtmenuId.Rows[loop]["CHILD_OF_SUBMENU_ID"].ToString();
                }
            }
            loop++;
        }
        return MenuId;
    }
}