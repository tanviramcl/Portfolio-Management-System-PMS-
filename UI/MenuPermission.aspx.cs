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
        Session["menus"] = GetMENU();

        DataTable dtmenUList = (DataTable)Session["menus"];



        //  companyNameTextBox.Text = "sss";
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {

      

    }

    private DataTable GetMENU()
    {
        DataTable dtMenUList = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from (select sm.MENU_ID,menu.MENU_NAME,sm.SUBMENU_ID,sm.SUBMENU_NAME  from  (select * from Menu ) menu inner join SUBMENU sm on MENU.MENU_ID=SM.MENU_ID) msm inner join ");
        sbMst.Append(" CHILD_OF_SUBMENU cosm on msm.SUBMENU_ID=COSM.SUBMENU_ID  ");
        sbOrderBy.Append(" order by CHILD_OF_SUBMENU_ID asc ");

        sbMst.Append(sbOrderBy.ToString());
        dtMenUList = commonGatewayObj.Select(sbMst.ToString());

        Session["dtMenUList"] = dtMenUList;
        return dtMenUList;
    }

    protected void AssignMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssignMenuByUser.aspx");
    }


   
}