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
using System.Text;
using System.IO;
//using AMCL.DL;
//using AMCL.BL;
//using AMCL.UTILITY;
//using AMCL.GATEWAY;
//using AMCL.COMMON;


public partial class UI_PORTFOLIO_PortfolioFileUpload : System.Web.UI.Page
{
    BaseClass bcContent = new BaseClass();
   
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (BaseContent.IsSessionExpired())
        //{
        //    Response.Redirect("../../Default.aspx");
        //    return;
        //}
        //bcContent = (BaseClass)Session["BCContent"];


        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        if (!IsPostBack)
        {
            //FileUploadDateTextBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            DSETradeCustLabel.Text = "";
            CSETradeCustLabel.Text = "";
            DSEMPLabel.Text = "";
            CSEMPLabel.Text = "";
        }
    
    
   }


    protected void SaveDSEButton_Click(object sender, EventArgs e)
    {
        string fileLocation = ConfigReader._TRADE_FILE_LOCATION.ToString() + "\\TRADE_CUST_DSE\\";
        if ((dseFileUpload.PostedFile != null) && (dseFileUpload.PostedFile.ContentLength > 0))
        {
            string fn = FileUploadDateTextBox.Text.ToString().ToUpper() + "-DSE-ISTBROKER.txt";
            string SaveLocation = fileLocation + fn;
            try
            {
                if (!File.Exists(SaveLocation))
                {
                    dseFileUpload.PostedFile.SaveAs(SaveLocation);
                    DSETradeCustLabel.Text = "File Save Successfully ";
                    DSETradeCustLabel.Style.Add("color", "#009933");
                }
                else
                {
                    DSETradeCustLabel.Text = "Upload Failed!! File Already Saved On That Date ";
                    DSETradeCustLabel.Style.Add("color", "red");
                }
                
            }
            catch (Exception ex)
            {
                DSETradeCustLabel.Style.Add("color", "red");
                DSETradeCustLabel.Text = ex.ToString();
               
            }
        }
        else
        {
            DSETradeCustLabel.Style.Add("color", "red");
            DSETradeCustLabel.Text = "Please select a file to upload.";          
        }
    }
    protected void SaveCSEButton_Click(object sender, EventArgs e)
    {
        string fileLocation = ConfigReader._TRADE_FILE_LOCATION.ToString() + "\\TRADE_CUST_CSE\\";
        if ((cseFileUpload.PostedFile != null) && (cseFileUpload.PostedFile.ContentLength > 0))
        {
            string fn = FileUploadDateTextBox.Text.ToString().ToUpper() + "-CSE-ISTBROKER.txt";
            string SaveLocation = fileLocation + fn;
            try
            {
                if (!File.Exists(SaveLocation))
                {
                    cseFileUpload.PostedFile.SaveAs(SaveLocation);
                    CSETradeCustLabel.Text = "File Save Successfully ";
                    CSETradeCustLabel.Style.Add("color", "#009933");
                }
                else
                {
                    CSETradeCustLabel.Text = "Upload Failed: File Already Saved On That Date ";
                    CSETradeCustLabel.Style.Add("color", "red");
                }
            }
            catch (Exception ex)
            {
                CSETradeCustLabel.Style.Add("color", "red");
                CSETradeCustLabel.Text = ex.ToString();
               
                
            }
        }
        else
        {
            CSETradeCustLabel.Style.Add("color", "red");
            CSETradeCustLabel.Text = "Please select a file to upload.";     
        }

    }
    protected void SaveDSEMPButton_Click(object sender, EventArgs e)
    {

        string fileLocation = ConfigReader._TRADE_FILE_LOCATION.ToString() + "\\DSE_PRICE\\";
        if ((dseMPFileUpload.PostedFile != null) && (dseMPFileUpload.PostedFile.ContentLength > 0))
        {
            string fn = FileUploadDateTextBox.Text.ToString().ToUpper() + "-DSE-MARKET-PRICE.xml";
            string SaveLocation = fileLocation + fn;
            try
            {
                if (!File.Exists(SaveLocation))
                {
                    dseMPFileUpload.PostedFile.SaveAs(SaveLocation);
                    DSEMPLabel.Text = "File Save Successfully ";
                    DSEMPLabel.Style.Add("color", "#009933");
                }
                else
                {

                    DSEMPLabel.Text = "Upload Failed:File Already Saved On That Date ";
                    DSEMPLabel.Style.Add("color", "red");
                }

            }
            catch (Exception ex)
            {
                DSEMPLabel.Style.Add("color", "red");
                DSEMPLabel.Text = ex.ToString();
               
               
            }
        }
        else
        {
            DSEMPLabel.Style.Add("color", "red");
            DSEMPLabel.Text = "Please select a file to upload.";     
        }


    }
    protected void SaveCSEMPButton_Click(object sender, EventArgs e)
    {

        string fileLocation = ConfigReader._TRADE_FILE_LOCATION.ToString() + "\\CSE_PRICE\\";
        if ((cseMPFileUpload.PostedFile != null) && (cseMPFileUpload.PostedFile.ContentLength > 0))
        {
            string fn = FileUploadDateTextBox.Text.ToString().ToUpper() + "-CSE-MARKET-PRICE.txt";
            string SaveLocation = fileLocation + fn;
            try
            {
                if (!File.Exists(SaveLocation))
                {
                    cseMPFileUpload.PostedFile.SaveAs(SaveLocation);
                    CSEMPLabel.Text = "File Save Successfully ";
                    CSEMPLabel.Style.Add("color", "#009933");
                }
                else
                {
                    CSEMPLabel.Text = "Upload Failed: File Already Saved On That Date ";
                    CSEMPLabel.Style.Add("color", "red");
                }


            }
            catch (Exception ex)
            {
                CSEMPLabel.Style.Add("color", "red");
                CSEMPLabel.Text = ex.ToString();
            
            }
        }
        else
        {
            CSEMPLabel.Style.Add("color", "red");
            CSEMPLabel.Text = "Please select a file to upload.";     
        }
    }
    protected void FileUploadDateTextBox_TextChanged(object sender, EventArgs e)
    {
        fleUploadStatus();
    }

    public void fleUploadStatus()
    {
        string fileLocation = ConfigReader._TRADE_FILE_LOCATION.ToString();
        string dseMP = fileLocation + "\\DSE_PRICE\\"+FileUploadDateTextBox.Text.ToString().ToUpper() + "-DSE-MARKET-PRICE.xml";
        string cseMP = fileLocation + "\\CSE_PRICE\\" + FileUploadDateTextBox.Text.ToString().ToUpper() + "-CSE-MARKET-PRICE.txt";
        string dseTradeCust = fileLocation + "\\TRADE_CUST_DSE\\" + FileUploadDateTextBox.Text.ToString().ToUpper() + "-DSE-ISTBROKER.txt";
        string cseTradeCust = fileLocation + "\\TRADE_CUST_CSE\\" + FileUploadDateTextBox.Text.ToString().ToUpper() + "-CSE-ISTBROKER.txt";
        if(File.Exists(dseMP))
        {
             DSEMPLabel.Text = "File Already Saved On That Date ";
             DSEMPLabel.Style.Add("color", "#009933");
        }
        else
        {
            DSEMPLabel.Text = "Please Select File to Upload ";
            DSEMPLabel.Style.Add("color", "red");
        }
        if (File.Exists(cseMP))
        {
            CSEMPLabel.Text = "File Already Saved On That Date ";
            CSEMPLabel.Style.Add("color", "#009933");
        }
        else
        {
            CSEMPLabel.Text = "Please Select File to Upload ";
            CSEMPLabel.Style.Add("color", "red");
        }


        if (File.Exists(dseTradeCust))
        {
            DSETradeCustLabel.Text = "File Already Saved On That Date ";
            DSETradeCustLabel.Style.Add("color", "#009933");
        }
        else
        {
            DSETradeCustLabel.Text = "Please Select File to Upload ";
            DSETradeCustLabel.Style.Add("color", "red");
        }
        if (File.Exists(cseTradeCust))
        {
            CSETradeCustLabel.Text = "File Already Saved On That Date ";
            CSETradeCustLabel.Style.Add("color", "#009933");
        }
        else
        {
            CSETradeCustLabel.Text = "Please Select File to Upload ";
            CSETradeCustLabel.Style.Add("color", "red");
        }
    }
}
