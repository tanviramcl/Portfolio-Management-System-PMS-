using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        lblerror.Text = "";
    }
    protected void UserIDTextBox_TextChanged(object sender, EventArgs e)
    {
        DataTable dtgetuser;
        string sessionUserid = Session["UserID"].ToString();



        if (userIdTextBox.Text.ToString() != "")
        {


            if (sessionUserid != userIdTextBox.Text.ToString())
            {
             
                lblerror.Text = "You are not permitted to change the password";
            }
            else
            {

                string strfundcode = "select * from user_table where user_id='" + userIdTextBox.Text.ToString() + "'";
                dtgetuser = commonGatewayObj.Select(strfundcode);



                DataTable dtFromUserList = new DataTable();



                if (dtgetuser != null && dtgetuser.Rows.Count > 0)
                {

                    //   ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('This User is already available !');", true);
                    userIdTextBox.Text = dtgetuser.Rows[0]["USER_ID"].ToString();
                    txtUserName.Text = dtgetuser.Rows[0]["Name"].ToString();
                    userDesignationTextBox.Text = dtgetuser.Rows[0]["DESIGNATION"].ToString();
                    ButtonSave.Text = "Update";

                }
                else
                {
                    ClearText();
                }
            }

        }


    }

   

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
       // System.Threading.Thread.Sleep(5000);
    }

    [System.Web.Services.WebMethod]

    public static bool InsertandUpdateUser(string userId,string Password,string confirmPassword)
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable dtgetUser;
        string passWord = "";

        if (userId != "")
        {
           string strUserID = "select * from user_table where user_id='" +userId+ "'";
            dtgetUser = commonGatewayObj.Select(strUserID);
            if (dtgetUser != null && dtgetUser.Rows.Count > 0)
            {

                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(Password);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        passWord = Convert.ToBase64String(ms.ToArray());
                    }
                }

                string strUPQuery = "update USER_TABLE set PASSWORD ='" + passWord + "' where USER_ID ='" + userId + "'";

                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUPQuery);

            }
            
           
        }

        return true;

    }

    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public void ClearText()
    {
        userIdTextBox.Text = "";
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtconfirmPassword.Text = "";
        userDesignationTextBox.Text = "";
    }

 
}