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
        Session["user_list"] = GetUserInfo();

        DataTable dtuserlist = (DataTable)Session["user_list"];
       
            Panel1.Visible = false;
            LabelUserRole.Visible = false;
            UserRoleTextBox.Visible = false;
            ButtonRoleAddTextBox.Visible = false;
            lblProcessing.Visible = true;   
       
        
        //  companyNameTextBox.Text = "sss";
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {

      

    }

    private DataTable GetUserInfo()
    {
        DataTable dtUserList = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" Select user_table.ID,user_table.USER_ID,user_table.NAME,user_table.DESIGNATION,USER_ROLE.ROLE_NAME from user_table  INNER JOIN user_role ");
        sbMst.Append("ON user_table.ROLE_ID = user_role.ROLE_ID   ");
        sbOrderBy.Append(" order by user_table.ID  ");

        sbMst.Append(sbOrderBy.ToString());
        dtUserList = commonGatewayObj.Select(sbMst.ToString());

        Session["dtUserInfo"] = dtUserList;
        return dtUserList;
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddUser.aspx");
    }
    protected void ButtonRoleAddTextBox_Click(object sender, EventArgs e)
    {
        string roleName = UserRoleTextBox.Text.ToString();
        DataTable dtRoleList = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" select * from USER_ROLE where ROLE_NAME='"+roleName+"' ");
        sbOrderBy.Append(" order by USER_ROLE.ROLE_ID  ");
        sbMst.Append(sbOrderBy.ToString());

        dtRoleList = commonGatewayObj.Select(sbMst.ToString());

        if (dtRoleList.Rows.Count > 0)
        {
            lblProcessing.Text = "This role already assigned";
        }
        else
        {
            DataTable dtRoleId=new DataTable();
            string strInsQuery;

            string strQuery = "select max(ROLE_ID)+1 as ROLE_ID from  USER_ROLE";
            dtRoleId = commonGatewayObj.Select(strQuery);

            if (dtRoleId.Rows.Count > 0)
            {
                strInsQuery = "insert into USER_ROLE(ROLE_ID,ROLE_NAME)values("+dtRoleId.Rows[0]["ROLE_ID"].ToString() +",'" + roleName + "')";
                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                lblProcessing.Text = "This role sucessfully inserted";
            }

        }

          
        }
    protected void ManageRole_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        LabelUserRole.Visible = true;
        UserRoleTextBox.Visible = true;
        ButtonRoleAddTextBox.Visible = true;
        lblProcessing.Visible = false;
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddFund.aspx");
    }
}