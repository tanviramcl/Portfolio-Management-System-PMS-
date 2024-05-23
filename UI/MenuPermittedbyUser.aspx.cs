using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
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
        if (string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            Response.Write("This user not assigned for menu");
        }
        else
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
          

        }

    }

    protected void grdShowDSEMP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdShowDSEMP.PageIndex = e.NewPageIndex;
        BindGrid();
    }



    protected void saveButton_Click(object sender, EventArgs e)
    {
        //string usrId = TextBox1.Text;
        //string strDelQuery = "delete from MENUPERMISSIONS where USER_ID='" + usrId + "' and MENU_ID";
        //int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);

        //lblProcessing.Text = "Delete  Successfull";


    }
  

    private void BindGrid()
    {

        string userID = Convert.ToString(Request.QueryString["ID"]).Trim();
        Session["menus"] = GetMENU(userID);

        DataTable dtmenUList = (DataTable)Session["menus"];


        grdShowDSEMP.DataSource = dtmenUList;
        grdShowDSEMP.DataBind();
    }

    protected void grdShowDSEMP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdShowDSEMP.Rows[e.RowIndex];
      

        string childOfsubmenuId=  row.Cells[4].Text;
        string userId = row.Cells[0].Text;

      
        string strDelQuery = "delete from MENUPERMISSIONS where MENU_ID='" + childOfsubmenuId + "' and USER_ID='"+ userId + "'";
        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);
        BindGrid();
        
    }
   
    
   

   

    private DataTable GetMENU(string UserId)
    {
        DataTable dtMenUList = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select csmwithsm.USER_ID,csmwithsm.CHILD_OF_SUBMENU_ID,csmwithsm.CHILD_OF_SUBMENU_NAME,csmwithsm.URL,csmwithsm.SUBMENU_ID,csmwithsm.SUBMENU_NAME,csmwithsm.PermittedMenu_ID,csmwithsm.MENU_ID,MENU.MENU_NAME from");
        sbMst.Append(" (select pcosm.USER_ID,pcosm.CHILD_OF_SUBMENU_ID,pcosm.CHILD_OF_SUBMENU_NAME,pcosm.URL,pcosm.SUBMENU_ID,sm.SUBMENU_NAME,pcosm.PermittedMenu_ID,pcosm.MENU_ID from ");
        sbMst.Append(" (select USER_ID,CHILD_OF_SUBMENU_ID,CHILD_OF_SUBMENU_NAME,URL,SUBMENU_ID,PermittedMenu_ID,MENU_ID from (select MENU_ID as PermittedMenu_ID ,USER_ID");
        sbMst.Append(" from MENUPERMISSIONS where USER_ID='"+ UserId + "') mp inner join  CHILD_OF_SUBMENU csm On mp.PermittedMenu_ID =csm.CHILD_OF_SUBMENU_ID) pcosm inner join submenu sm on ");
        sbMst.Append(" pcosm.SUBMENU_ID=sm.SUBMENU_ID) csmwithsm inner join MENU menu On csmwithsm.MENU_ID=menu.MENU_ID  ");
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