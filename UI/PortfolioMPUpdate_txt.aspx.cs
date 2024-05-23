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
using System.Data.OracleClient;
using System.IO;
//using AMCL.DL;
//using AMCL.BL;
//using AMCL.UTILITY;
//using AMCL.GATEWAY;
//using AMCL.COMMON;


public partial class UI_PORTFOLIO_PortfolioMPUpdate : System.Web.UI.Page
{
    BaseClass bcContent = new BaseClass();
    PfolioBL pfolioBLObj = new PfolioBL();
    CommonGateway commonGatewayObj = new CommonGateway();
    
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
           
            
        }
    
    
   }


    protected void showDataButton_Click(object sender, EventArgs e)
    {
        try
        {
            int zeroCompanyCode = 0;
            string dseMPFile = ConfigReader._TRADE_FILE_LOCATION.ToString();
            dseMPFile = dseMPFile + "\\DSE_PRICE" + "\\" + marketPriceDateTextBox.Text.ToString().ToUpper() + "-DSE-MARKET-PRICE.txt";
            if (File.Exists(dseMPFile))
            {

                DataTable dtMP = new DataTable();
                dtMP.Columns.Add("ID", typeof(int));
                dtMP.Columns.Add("TRADE_CODE", typeof(string));
                dtMP.Columns.Add("COMP_CD", typeof(int));
                dtMP.Columns.Add("OPEN", typeof(string));
                dtMP.Columns.Add("HIGH", typeof(string));
                dtMP.Columns.Add("LOW", typeof(string));
                dtMP.Columns.Add("CLOSE", typeof(string));


                DataRow drMP;
                StreamReader srFileReader;
                string line;
                srFileReader = new StreamReader(dseMPFile);
                string[] lineContent;
                int count = 0;
                int serial = 0;

                while (srFileReader.Peek() != -1)
                {
                    line = srFileReader.ReadLine();
                    char[] delimiters = new char[] { ' ' };
                    lineContent = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    if (lineContent.Length > 0)
                    {
                        int companyCode = pfolioBLObj.getCompanyCodeByDSECode(lineContent[0].ToString().ToUpper());
                        if (companyCode == 0)
                        {
                            zeroCompanyCode++;
                        }
                        drMP = dtMP.NewRow();
                        serial = serial + 1;
                        drMP["ID"] = serial;
                        drMP["TRADE_CODE"] = lineContent[0].ToString().ToUpper();
                        drMP["COMP_CD"] = companyCode;
                        drMP["OPEN"] = lineContent[1].ToString();
                        drMP["HIGH"] = lineContent[2].ToString();
                        drMP["LOW"] = lineContent[3].ToString();
                        drMP["CLOSE"] = lineContent[4].ToString();
                        dtMP.Rows.Add(drMP);
                    }

                    count++;

                }
                if (dtMP.Rows.Count > 0)
                {
                    dvGridDSEMPInfo.Visible = true;
                    grdShowDSEMP.DataSource = dtMP;
                    grdShowDSEMP.DataBind();
                    Session["dtMPDSE"] = dtMP;
                    if (pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "DSE"))
                    {
                        dsePriceLabel.Text = "Price Already Saved On That Date";
                        dsePriceLabel.Style.Add("color", "#009933");
                    }
                    else
                    {
                        dsePriceLabel.Text = "Price Should Save On That Date";
                        dsePriceLabel.Style.Add("color", "red");
                    }
                }
                else
                {
                    Session["dtMPDSE"] = null;
                    dsePriceLabel.Text = "No DSE Price Found On That Date";
                    dsePriceLabel.Style.Add("color", "red");
                }
            }


        }
        catch (Exception ex)
        {
            dvGridDSEMPInfo.Visible = false;
            dsePriceLabel.Text = "File Read failed Error:" + ex.Message.ToString();
            dsePriceLabel.Style.Add("color", "red");
        }
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            commonGatewayObj.BeginTransaction();

            DataTable dtMPDSE = (DataTable)Session["dtMPDSE"];
            // Hashtable htInsertMP = new Hashtable();
            Hashtable htUpdateMP = new Hashtable();
            if (!pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "DSE"))
            {
                for (int loop = 0; loop < dtMPDSE.Rows.Count; loop++)
                {
                    htUpdateMP.Add("RT_UPD_DT", marketPriceDateTextBox.Text.ToString());
                    htUpdateMP.Add("AVG_RT", Convert.ToDecimal(dtMPDSE.Rows[loop]["CLOSE"].ToString()));
                    htUpdateMP.Add("DSE_HIGH", Convert.ToDecimal(dtMPDSE.Rows[loop]["HIGH"].ToString()));
                    htUpdateMP.Add("DSE_LOW", Convert.ToDecimal(dtMPDSE.Rows[loop]["LOW"].ToString()));
                    htUpdateMP.Add("DSE_OPEN", Convert.ToDecimal(dtMPDSE.Rows[loop]["OPEN"].ToString()));

                    commonGatewayObj.Update(htUpdateMP, "COMP", "COMP_CD=" + Convert.ToInt32(dtMPDSE.Rows[loop]["COMP_CD"].ToString()));
                    htUpdateMP = new Hashtable();

                }
                commonGatewayObj.CommitTransaction();
                dvGridDSEMPInfo.Visible = false;
                dsePriceLabel.Text = "Price  Save Successfully";
                dsePriceLabel.Style.Add("color", "#009933");
            }
            else
            {
                dsePriceLabel.Text = "Price Already Saved On That Date";
                dsePriceLabel.Style.Add("color", "red");
            }

        }
        catch (Exception ex)
        {
            // dvGridDSEMPInfo.Visible = false;
            commonGatewayObj.RollbackTransaction();
            dsePriceLabel.Text = "Price  Save failed Error:" + ex.Message.ToString();
            dsePriceLabel.Style.Add("color", "red");
        }
    }
    protected void showCseDataButton_Click(object sender, EventArgs e)
    {
        try
        {
           // int intexpCount = 0;
            int zeroCompanyCode = 0;
            string cseMPFile = ConfigReader._TRADE_FILE_LOCATION.ToString();
            cseMPFile = cseMPFile + "\\CSE_PRICE" + "\\" + marketPriceDateTextBox.Text.ToString().ToUpper() + "-CSE-MARKET-PRICE.txt";
            // cseMPFile = "F:\\Fdrive\\tradeSummary_ts_2017101633.txt";
            if (File.Exists(cseMPFile))
            {

                DataTable dtMP = new DataTable();
                dtMP.Columns.Add("ID", typeof(int));
                dtMP.Columns.Add("TRADE_CODE", typeof(string));
                dtMP.Columns.Add("COMP_CD", typeof(int));
                dtMP.Columns.Add("COMP_NAME", typeof(string));
                dtMP.Columns.Add("CLOSE", typeof(string));

                string[] lines = System.IO.File.ReadAllLines(cseMPFile);

                if (lines.Length > 0)
                {
                    string firstline = lines[0];

                    string[] headerslable = firstline.Split(',');

                    //foreach (string head in headerslable)
                    //{
                    //    dtMP.Columns.Add(new DataColumn(head));
                    //}


                    int count = lines.Length;

                    for (int r = 1; r <count; r++)
                    {
                        string []datawords = lines[r].Split(',');
                         DataRow drMP =dtMP.NewRow();
                     //   int columnIndex = 0;

                        foreach (string head in headerslable)
                        {
                            // drMP[head] = datawords[columnIndex++];

                            int companyCode = pfolioBLObj.getCompanyCodeByCSECode(datawords[2].Trim().ToString().ToUpper());
                            if (companyCode == 0)
                            {
                                zeroCompanyCode++;
                            }

                            drMP["ID"] = r;
                            drMP["TRADE_CODE"] = datawords[2].ToString().Trim().ToUpper();
                            drMP["COMP_CD"] = companyCode;
                            drMP["COMP_NAME"] = datawords[1].ToString().Trim().ToUpper();
                            drMP["CLOSE"] = datawords[7].Trim().ToUpper();
                        }
                        dtMP.Rows.Add(drMP);
                    }

                   // int a = 1;
                }



                if (dtMP.Rows.Count > 0)
                {
                    dvGridCSEMPInfo.Visible = true;
                    grdShowCSEMP.DataSource = dtMP;
                    grdShowCSEMP.DataBind();
                    Session["dtMPCSE"] = dtMP;
                    if (pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "CSE"))
                    {
                        csePriceLabel.Text = "Price Already Saved On That Date";
                        csePriceLabel.Style.Add("color", "#009933");
                    }
                    else
                    {
                        csePriceLabel.Text = "Price Should Save On That Date";
                        csePriceLabel.Style.Add("color", "red");
                    }
                }
                else
                {
                    Session["dtMPCSE"] = null;
                    dvGridCSEMPInfo.Visible = false;
                    csePriceLabel.Text = "No CSE Price Found On That Date";
                    csePriceLabel.Style.Add("color", "red");
                }
            }


        }
        catch (Exception ex)
        {

            dvGridCSEMPInfo.Visible = false;
            csePriceLabel.Text = "File Read failed Error:" + ex.Message.ToString();
            csePriceLabel.Style.Add("color", "red");
        }
    }
    protected void SaveCSEButton_Click(object sender, EventArgs e)
    {

        try
        {
            commonGatewayObj.BeginTransaction();

            DataTable dtMPCSE = (DataTable)Session["dtMPCSE"];

            Hashtable htUpdateMP = new Hashtable();

            if (!pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "CSE"))
            {
                for (int loop = 0; loop < dtMPCSE.Rows.Count; loop++)
                {
                    htUpdateMP.Add("CSE_DT", marketPriceDateTextBox.Text.ToString());
                    htUpdateMP.Add("CSE_RT", Convert.ToDecimal(dtMPCSE.Rows[loop]["CLOSE"].ToString()));

                    commonGatewayObj.Update(htUpdateMP, "COMP", "COMP_CD=" + Convert.ToInt32(dtMPCSE.Rows[loop]["COMP_CD"].ToString()));
                    htUpdateMP = new Hashtable();
                }
                commonGatewayObj.CommitTransaction();
                dvGridCSEMPInfo.Visible = false;
                csePriceLabel.Text = "Price  Save Successfully";
                csePriceLabel.Style.Add("color", "#009933");
            }
            else
            {
                csePriceLabel.Text = "Price Already Saved On That Date";
                csePriceLabel.Style.Add("color", "red");
            }


        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            csePriceLabel.Text = "Price  Save failed Error:" + ex.Message.ToString();
            csePriceLabel.Style.Add("color", "red");
        }
    }
    protected void avgPriceButton_Click(object sender, EventArgs e)
    {

        try
        {
            StringBuilder sbQueryInsert = new StringBuilder();
            commonGatewayObj.BeginTransaction();

            if (!pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "AVERAGE"))
            {

                commonGatewayObj.ExecuteNonQuery("UPDATE COMP SET ADC_RT=ROUND((NVL(AVG_RT,CSE_RT)+NVL(CSE_RT,AVG_RT))/2,2)  WHERE RT_UPD_DT='" + marketPriceDateTextBox.Text.ToString() + "'");
                sbQueryInsert.Append(" INSERT INTO MARKET_PRICE SELECT COMP_CD,'" + marketPriceDateTextBox.Text.ToString() + "' AS EXPR1, ADC_RT, AVG_RT, CSE_RT, ");
                sbQueryInsert.Append(" DECODE(CSE_DT, NULL, NULL, '" + marketPriceDateTextBox.Text.ToString() + "') AS EXPR2, DSE_HIGH, DSE_LOW, DSE_OPEN  FROM  COMP WHERE  VALID IS NULL ");
                commonGatewayObj.ExecuteNonQuery(sbQueryInsert.ToString());

                commonGatewayObj.CommitTransaction();
                avegLabel.Text = "Price  Average Successfully";
                avegLabel.Style.Add("color", "#009933");
            }
            else
            {
                avegLabel.Text = "Price Already Avaraged On That Date";
                avegLabel.Style.Add("color", "red");
            }

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            avegLabel.Text = "Price  Average failed Error:" + ex.Message.ToString();
            avegLabel.Style.Add("color", "red");
        }
    }
    protected void marketPriceDateTextBox_TextChanged(object sender, EventArgs e)
    {
        dvGridCSEMPInfo.Visible = false;
        dvGridDSEMPInfo.Visible = false;
        if (pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "DSE"))
        {
            dsePriceLabel.Text = "Price Already Saved On That Date";
            dsePriceLabel.Style.Add("color", "#009933");
        }
        else
        {
            dsePriceLabel.Text = "Price Should Save On That Date";
            dsePriceLabel.Style.Add("color", "red");
        }
        if (pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "CSE"))
        {
            csePriceLabel.Text = "Price Already Saved On That Date";
            csePriceLabel.Style.Add("color", "#009933");
        }
        else
        {
            csePriceLabel.Text = "Price Should Save On That Date";
            csePriceLabel.Style.Add("color", "red");
        }
        if (pfolioBLObj.getMPUpdateStatus(marketPriceDateTextBox.Text.ToString(), "AVERAGE"))
        {

            avegLabel.Text = "Price Already Averaged On That Date";
            avegLabel.Style.Add("color", "#009933");
        }
        else
        {
            avegLabel.Text = "Price Should Avarage On That Date";
            avegLabel.Style.Add("color", "red");
        }
    }
}
