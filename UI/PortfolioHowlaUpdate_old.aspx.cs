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
//using System.Data.OracleClient;
using System.IO;
//using AMCL.DL;
//using AMCL.BL;
//using AMCL.UTILITY;
//using AMCL.GATEWAY;
//using AMCL.COMMON;


public partial class UI_PORTFOLIO_PortfolioHowlaUpdate : System.Web.UI.Page
{
    BaseClass bcContent = new BaseClass();
    PfolioBL pfolioBLObj = new PfolioBL();
   // LoginUser userObj = new LoginUser();
    CommonGateway commonGatewayObj = new CommonGateway();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (BaseContent.IsSessionExpired())
        //{
        //    Response.Redirect("../../Default.aspx");
        //    return;
        //}
        bcContent = (BaseClass)Session["BCContent"];      

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
         int count = 0;
        try
        {            
            string dseCustFile = ConfigReader._TRADE_FILE_LOCATION.ToString();
            dseCustFile = dseCustFile + "\\TRADE_CUST_DSE" + "\\" + HowlaDateTextBox.Text.ToString().ToUpper() + "-DSE-ISTBROKER.txt";
            if (File.Exists(dseCustFile))
            {

                DataTable dtHowla = getdtTradeCusTable();
              

                DataRow drHowla;
                StreamReader srFileReader;                
                string line;
                srFileReader = new StreamReader(dseCustFile);
                string[] lineContent;
               
                int serial = 0;
                double lagaCharge = 0.00;
                int tradeQty;
                double tradePrice = 0.00;
                string zeroCompCode = "";

                
                while (srFileReader.Peek() != -1)
                {
                    line = srFileReader.ReadLine();                    
                    lineContent = line.Split('~');
                    if (lineContent.Length > 0)
                    {
                        if (count == 1002)
                        {
                            if (1 == 1)
                            {
                            }
                          
                        }
                        
                        if (pfolioBLObj.getFundCodeByCustomerCode(lineContent[13].ToString()) > 0)
                        {
                            int companyCode = pfolioBLObj.getCompanyCodeByDSECode(lineContent[1].ToString().ToUpper());
                            if (companyCode == 0)
                            {
                                 zeroCompCode = lineContent[1].ToString().ToUpper();
                            }
                            int fundCode = pfolioBLObj.getFundCodeByCustomerCode(lineContent[13].ToString().ToUpper());
                          
                            drHowla = dtHowla.NewRow();
                            serial = serial + 1;
                            drHowla["SI"] = serial;
                            drHowla["F_CD"] = fundCode;                           
                            drHowla["SP_DATE"] = Convert.ToDateTime(DateTime.ParseExact(lineContent[7].ToString(), "dd-MM-yyyy", null)).ToString("dd-MMM-yyyy");
                            drHowla["BK_REF"] = lineContent[0].ToString().Trim().ToUpper();
                            drHowla["HOWLA_NO"] = lineContent[15].ToString().Trim();
                            drHowla["HOWLA_TP"] = lineContent[11].ToString().Trim().ToUpper();
                            if (lineContent[4] == "B")
                            {
                                drHowla["IN_OUT"] = "I";
                            }
                            else if (lineContent[4] == "S")
                            {
                                drHowla["IN_OUT"] = "O";
                            }
                            drHowla["SETTLE_DT"] = Convert.ToDateTime(DateTime.ParseExact(lineContent[7].ToString(), "dd-MM-yyyy", null)).ToString("dd-MMM-yyyy");                       
                            drHowla["COMP_CD"] = companyCode;
                            drHowla["SP_QTY"] = lineContent[5].ToString().Trim();
                            drHowla["SP_RATE"] = lineContent[6].ToString().Trim().ToUpper();
                            drHowla["CL_DATE"] = Convert.ToDateTime(ClearingDateTextBox.Text.Trim()).ToString("dd-MMM-yyyy");
                            drHowla["BK_CD"] = "DSE/129";
                            drHowla["HOWLA_CHG"] = "2";

                            tradeQty = Convert.ToInt32(lineContent[5].Trim());
                            tradePrice = Convert.ToDouble(lineContent[6].Trim());
                            lagaCharge = 0.0002 * tradeQty * tradePrice;

                            drHowla["LAGA_CHG"] = lagaCharge;
                            drHowla["VOUCH_REF"] = lineContent[3].ToString().ToUpper();

                            string LoginID = Session["UserID"].ToString();

                            //drHowla["OP_NAME"] = bcContent.LoginID.ToString();
                            drHowla["OP_NAME"] = LoginID;
                            drHowla["N_P"] = lineContent[10].ToString().Trim().ToUpper();
                            drHowla["ISIN_CD"] = lineContent[2].ToString().Trim().ToUpper();
                            drHowla["FORGN_FLG"] = lineContent[12].ToString().Trim().ToUpper();
                            drHowla["SPOT_ID"] = lineContent[16].ToString().Trim().ToUpper();
                            drHowla["INSTR_GRP"] = lineContent[17].ToString().Trim().ToUpper();
                            drHowla["MARKT_TP"] = lineContent[9].ToString().Trim().ToUpper();
                            drHowla["CUSTOMER"] = lineContent[13].ToString().Trim().ToUpper();
                            drHowla["BOID"] = lineContent[14].ToString().Trim().ToUpper();
                            dtHowla.Rows.Add(drHowla);
                            count++;
                        }

                        
                    }

                }
                if (dtHowla.Rows.Count > 0)
                {
                    if (zeroCompCode == "")
                    {
                        dvGridDSETradeInfo.Visible = true;
                        grdShowDSEMP.DataSource = dtHowla;
                        grdShowDSEMP.DataBind();
                        Session["dtTradeCusetDSE"] = dtHowla;
                        if (pfolioBLObj.getHowlaUpdateStatus(HowlaDateTextBox.Text.ToString(), "DSE"))
                        {
                            DSETradeCustLabel.Text = "DSE Howla Already Saved On That Date";
                            DSETradeCustLabel.Style.Add("color", "#009933");
                        }
                        else
                        {
                            DSETradeCustLabel.Text = "DSE Howla Should Save On That Date";
                            DSETradeCustLabel.Style.Add("color", "red");
                        }
                    }
                    else
                    {
                        Session["dtTradeCusetDSE"] = null;
                        dvGridDSETradeInfo.Visible = false;
                        DSETradeCustLabel.Text = "Data Show Failed: Unknown Company DSE Trading Code:"+zeroCompCode;
                        DSETradeCustLabel.Style.Add("color", "red");
                    }
                }
                else
                {
                    Session["dtTradeCusetDSE"] = null;
                    dvGridDSETradeInfo.Visible = false;
                    DSETradeCustLabel.Text = "No Data Found or File Read Error!!";
                    DSETradeCustLabel.Style.Add("color", "red");
                }
            }
         

        }
        catch (Exception ex)
        {
            int counter = count;
           
            dvGridDSETradeInfo.Visible = false;
            DSETradeCustLabel.Text = "File Read failed Error:" + ex.Message.ToString();
            DSETradeCustLabel.Style.Add("color", "red");
        }
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            int countRows = 0;
            commonGatewayObj.BeginTransaction();

            DataTable dtTradeCustDSE = (DataTable)Session["dtTradeCusetDSE"];
            if (dtTradeCustDSE.Rows.Count>0)
            {
                //   dt.Rows[0]["vch_dt"].ToString();
                string howlaDate = dtTradeCustDSE.Rows[0]["SP_DATE"].ToString();
                DataTable dtMarketPriceByHowladate = new DataTable();
                PfolioBL pfolioBLObj = new PfolioBL();
                if (!pfolioBLObj.getMPUpdateStatus(howlaDate, "AVERAGE"))
                {
                    grdShowDSEMP.Visible = false;
                    DSETradeCustLabel.Text = "Save Failed: Market price is not updated";
                    DSETradeCustLabel.Style.Add("color", "red");
                }
                else
                {
                    if (!pfolioBLObj.getHowlaUpdateStatus(HowlaDateTextBox.Text.ToString(), "DSE"))
                    {

                        if (dvGridDSETradeInfo.Visible == true)
                        {
                            Hashtable htTradeCustDSE = new Hashtable();
                            for (int loop = 0; loop < dtTradeCustDSE.Rows.Count; loop++)
                            {

                                htTradeCustDSE.Add("F_CD", dtTradeCustDSE.Rows[loop]["F_CD"]);
                                htTradeCustDSE.Add("SP_DATE", dtTradeCustDSE.Rows[loop]["SP_DATE"]);
                                htTradeCustDSE.Add("BK_REF", dtTradeCustDSE.Rows[loop]["BK_REF"]);
                                htTradeCustDSE.Add("HOWLA_NO", dtTradeCustDSE.Rows[loop]["HOWLA_NO"]);

                                htTradeCustDSE.Add("HOWLA_TP", dtTradeCustDSE.Rows[loop]["HOWLA_TP"]);
                                htTradeCustDSE.Add("IN_OUT", dtTradeCustDSE.Rows[loop]["IN_OUT"]);
                                htTradeCustDSE.Add("SETTLE_DT", dtTradeCustDSE.Rows[loop]["SETTLE_DT"]);
                                htTradeCustDSE.Add("COMP_CD", dtTradeCustDSE.Rows[loop]["COMP_CD"]);

                                htTradeCustDSE.Add("SP_QTY", dtTradeCustDSE.Rows[loop]["SP_QTY"]);
                                htTradeCustDSE.Add("SP_RATE", dtTradeCustDSE.Rows[loop]["SP_RATE"]);
                                htTradeCustDSE.Add("CL_DATE", dtTradeCustDSE.Rows[loop]["CL_DATE"]);
                                htTradeCustDSE.Add("BK_CD", dtTradeCustDSE.Rows[loop]["BK_CD"]);

                                htTradeCustDSE.Add("HOWLA_CHG", dtTradeCustDSE.Rows[loop]["HOWLA_CHG"]);
                                htTradeCustDSE.Add("LAGA_CHG", dtTradeCustDSE.Rows[loop]["LAGA_CHG"]);
                                htTradeCustDSE.Add("VOUCH_REF", dtTradeCustDSE.Rows[loop]["VOUCH_REF"]);
                                htTradeCustDSE.Add("OP_NAME", dtTradeCustDSE.Rows[loop]["OP_NAME"]);

                                htTradeCustDSE.Add("N_P", dtTradeCustDSE.Rows[loop]["N_P"]);
                                htTradeCustDSE.Add("ISIN_CD", dtTradeCustDSE.Rows[loop]["ISIN_CD"]);
                                htTradeCustDSE.Add("FORGN_FLG", dtTradeCustDSE.Rows[loop]["FORGN_FLG"]);
                                htTradeCustDSE.Add("SPOT_ID", dtTradeCustDSE.Rows[loop]["SPOT_ID"]);

                                htTradeCustDSE.Add("INSTR_GRP", dtTradeCustDSE.Rows[loop]["INSTR_GRP"]);
                                htTradeCustDSE.Add("MARKT_TP", dtTradeCustDSE.Rows[loop]["MARKT_TP"]);
                                htTradeCustDSE.Add("CUSTOMER", dtTradeCustDSE.Rows[loop]["CUSTOMER"]);
                                htTradeCustDSE.Add("BOID", dtTradeCustDSE.Rows[loop]["BOID"]);



                                commonGatewayObj.Insert(htTradeCustDSE, "HOWLA");
                                htTradeCustDSE = new Hashtable();
                                countRows++;
                            }
                            commonGatewayObj.CommitTransaction();
                            dvGridDSETradeInfo.Visible = false;
                            Session["dtTradeCusetDSE"] = null;
                            DSETradeCustLabel.Text = " Howla DSE " + countRows.ToString() + " Rows Save Successfully";
                            DSETradeCustLabel.Style.Add("color", "#009933");

                        }
                        else
                        {
                            DSETradeCustLabel.Text = "Save Failed: DSE Howla Already Saved On That Date";
                            DSETradeCustLabel.Style.Add("color", "red");
                        }

                        
                    }
                    else
                    {

                        DSETradeCustLabel.Text = "Save Failed:Please  Press the Show DSE Howla button";
                        DSETradeCustLabel.Style.Add("color", "red");
                    }

                }

                //string strQuery = "select TO_CHAR(max(vch_dt),'DD-MON-YYYY')last_tr_dt,TO_CHAR(max(vch_dt) + 1,'DD-MON-YYYY')vch_dt  from fund_trans_hb where f_cd =" + dtFundNameDropDownList.Rows[i]["F_CD"].ToString() +
                //                  " and tran_tp in ('C','S') and stock_ex in ('C','A')";
                //dtMarketPriceByHowladate = commonGatewayObj.Select(strQuery);


               
              
            }
        }
        catch (Exception ex)
        {
            
            commonGatewayObj.RollbackTransaction();
            Session["dtTradeCusetDSE"] = null;
            DSETradeCustLabel.Text = " Howla DSE Save failed Error:" + ex.Message.ToString();
            DSETradeCustLabel.Style.Add("color", "red");
        }
    }
    protected void showCseDataButton_Click(object sender, EventArgs e)
    {
        try
        {

            string cseCustFile = ConfigReader._TRADE_FILE_LOCATION.ToString();
            cseCustFile = cseCustFile + "\\TRADE_CUST_CSE" + "\\" + HowlaDateTextBox.Text.ToString().ToUpper() + "-CSE-ISTBROKER.txt";
            if (File.Exists(cseCustFile))
            {

                DataTable dtTradeCusData = getdtTradeCusTable();
                int serial = 1;
                DataRow drTradeCusdata;
                StreamReader srFileReader;
                string line;
                string zeroCompCode = "";

                srFileReader = new StreamReader(cseCustFile);
                string[] lineContent;
                
                while (srFileReader.Peek() != -1)
                {
                    double lagaCharge = 0.00;
                    int tradeQty;
                    double tradePrice = 0.00;
                  
                    line = srFileReader.ReadLine();
                    lineContent = line.Split('|');                   
                    if (pfolioBLObj.getFundCodeByCustomerCode(lineContent[6].ToString()) > 0)
                        
                    {
                        drTradeCusdata = dtTradeCusData.NewRow();
                        drTradeCusdata["SI"] = serial.ToString();
                        int companyCode = pfolioBLObj.getCompanyCodeByDSECode(lineContent[2].ToString().ToUpper());
                        if (companyCode == 0)
                        {
                            zeroCompCode = lineContent[2].ToString().ToUpper();
                        }
                        int fundCode = pfolioBLObj.getFundCodeByCustomerCode(lineContent[6].ToString().ToUpper());
                        
                        drTradeCusdata["F_CD"] = fundCode;
                        drTradeCusdata["COMP_CD"] = companyCode;
                        drTradeCusdata["SP_DATE"] =  Convert.ToDateTime(DateTime.ParseExact(lineContent[10].ToString(), "dd/MM/yyyy", null)).ToString("dd-MMM-yyyy");
                        drTradeCusdata["BK_REF"] = lineContent[1].ToString().Trim().ToUpper();
                        drTradeCusdata["HOWLA_NO"] = lineContent[9].ToString().Trim();
                        drTradeCusdata["HOWLA_TP"] = "N";
                        if (lineContent[3] == "B")
                        {
                            drTradeCusdata["IN_OUT"] = "I";
                        }
                        else if (lineContent[3] == "S")
                        {
                            drTradeCusdata["IN_OUT"] = "O";
                        }
                        drTradeCusdata["SETTLE_DT"] = Convert.ToDateTime(DateTime.ParseExact(lineContent[10].ToString(), "dd/MM/yyyy", null)).ToString("dd-MMM-yyyy");
                       
                        drTradeCusdata["SP_QTY"] = lineContent[4].ToString().Trim();
                        drTradeCusdata["SP_RATE"] = lineContent[5].ToString().Trim().ToUpper();
                        drTradeCusdata["CL_DATE"] = Convert.ToDateTime(ClearingDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy");
                        drTradeCusdata["BK_CD"] = "CSE/711";
                        drTradeCusdata["HOWLA_CHG"] = "3";

                        tradeQty = Convert.ToInt32(lineContent[4].Trim());
                        tradePrice = Convert.ToDouble(lineContent[5].Trim());
                        lagaCharge = 0.00025 * tradeQty * tradePrice;

                        drTradeCusdata["LAGA_CHG"] = lagaCharge;
                        drTradeCusdata["VOUCH_NO"] = lineContent[0].ToString().ToUpper();
                        drTradeCusdata["VOUCH_REF"] = Convert.ToDateTime(DateTime.ParseExact(lineContent[10].ToString(), "dd/MM/yyyy", null)).ToString("dd-MMM-yyyy");
                        // drTradeCusdata["OP_NAME"] = bcContent.LoginID.ToString();
                         string LoginID = Session["UserID"].ToString();
                        drTradeCusdata["OP_NAME"] = LoginID;
                        drTradeCusdata["N_P"] = "P";
                        drTradeCusdata["CUSTOMER"] = lineContent[6].ToString().Trim().ToUpper();

                        dtTradeCusData.Rows.Add(drTradeCusdata);
                        serial++;
                    }
                }
                if (dtTradeCusData.Rows.Count > 0)
                {
                    if (zeroCompCode == "")
                    {
                        dvGridCSETradeInfo.Visible = true;
                        grdShowCSEMP.DataSource = dtTradeCusData;
                        grdShowCSEMP.DataBind();
                        Session["dtTradeCusDataCSE"] = dtTradeCusData;
                        if (pfolioBLObj.getHowlaUpdateStatus(HowlaDateTextBox.Text.ToString(), "CSE"))
                        {
                            CSETradeCustLabel.Text = "CSE Howla Already Saved On That Date";
                            CSETradeCustLabel.Style.Add("color", "#009933");
                        }
                        else
                        {
                            CSETradeCustLabel.Text = "CSE Howla Should Save On That Date";
                            CSETradeCustLabel.Style.Add("color", "red");
                        }
                    }
                    else
                    {
                        Session["dtTradeCusDataCSE"] = null;
                        dvGridCSETradeInfo.Visible = false;
                        CSETradeCustLabel.Text = "Data Show Failed: Unknown Company DSE Trading Code:" + zeroCompCode;
                        CSETradeCustLabel.Style.Add("color", "red");
                    }
                }
                else
                {
                    Session["dtTradeCusDataCSE"] = null;
                    dvGridCSETradeInfo.Visible = false;
                    CSETradeCustLabel.Text = "No Data Found or File Read Error!!";
                    CSETradeCustLabel.Style.Add("color", "red");
                }
            }


        }
        catch (Exception Ex)
        {
            string ex = Ex.Message.ToString();
            dvGridCSETradeInfo.Visible = false;
            CSETradeCustLabel.Text = "File Read failed Error:" + ex.ToString();
            CSETradeCustLabel.Style.Add("color", "red");
        }
    }
    protected void SaveCSEButton_Click(object sender, EventArgs e)
    {

        try
        {
            int countRows = 0;
            DataTable dtTradeCustDSE = (DataTable)Session["dtTradeCusDataCSE"];
            if (dtTradeCustDSE.Rows.Count>0)
            {
                //   dt.Rows[0]["vch_dt"].ToString();
                string howlaDate = dtTradeCustDSE.Rows[0]["SP_DATE"].ToString();
                DataTable dtMarketPriceByHowladate = new DataTable();
                PfolioBL pfolioBLObj = new PfolioBL();
                if (!pfolioBLObj.getMPUpdateStatus(howlaDate, "AVERAGE"))
                {
                    dvGridCSETradeInfo.Visible = false;
                    DSETradeCustLabel.Text = "Save Failed: Market price is not updated";
                    DSETradeCustLabel.Style.Add("color", "red");
                }
                else
                {

                    if (!pfolioBLObj.getHowlaUpdateStatus(HowlaDateTextBox.Text.ToString(), "CSE"))
                    {
                        if (dvGridCSETradeInfo.Visible == true)
                        {

                            Hashtable htTradeCustDSE = new Hashtable();
                            for (int loop = 0; loop < dtTradeCustDSE.Rows.Count; loop++)
                            {

                                htTradeCustDSE.Add("F_CD", dtTradeCustDSE.Rows[loop]["F_CD"]);
                                htTradeCustDSE.Add("COMP_CD", dtTradeCustDSE.Rows[loop]["COMP_CD"]);
                                htTradeCustDSE.Add("SP_DATE", dtTradeCustDSE.Rows[loop]["SP_DATE"]);
                                htTradeCustDSE.Add("BK_REF", dtTradeCustDSE.Rows[loop]["BK_REF"]);

                                htTradeCustDSE.Add("HOWLA_NO", dtTradeCustDSE.Rows[loop]["HOWLA_NO"]);
                                htTradeCustDSE.Add("HOWLA_TP", dtTradeCustDSE.Rows[loop]["HOWLA_TP"]);
                                htTradeCustDSE.Add("IN_OUT", dtTradeCustDSE.Rows[loop]["IN_OUT"]);
                                htTradeCustDSE.Add("SETTLE_DT", dtTradeCustDSE.Rows[loop]["SETTLE_DT"]);


                                htTradeCustDSE.Add("SP_QTY", dtTradeCustDSE.Rows[loop]["SP_QTY"]);
                                htTradeCustDSE.Add("SP_RATE", dtTradeCustDSE.Rows[loop]["SP_RATE"]);
                                htTradeCustDSE.Add("CL_DATE", dtTradeCustDSE.Rows[loop]["CL_DATE"]);
                                htTradeCustDSE.Add("BK_CD", dtTradeCustDSE.Rows[loop]["BK_CD"]);

                                htTradeCustDSE.Add("HOWLA_CHG", dtTradeCustDSE.Rows[loop]["HOWLA_CHG"]);
                                htTradeCustDSE.Add("LAGA_CHG", dtTradeCustDSE.Rows[loop]["LAGA_CHG"]);
                                htTradeCustDSE.Add("VOUCH_NO", dtTradeCustDSE.Rows[loop]["VOUCH_NO"]);
                                htTradeCustDSE.Add("VOUCH_REF", dtTradeCustDSE.Rows[loop]["VOUCH_REF"]);

                                htTradeCustDSE.Add("OP_NAME", dtTradeCustDSE.Rows[loop]["OP_NAME"]);
                                htTradeCustDSE.Add("N_P", dtTradeCustDSE.Rows[loop]["N_P"]);
                                htTradeCustDSE.Add("CUSTOMER", dtTradeCustDSE.Rows[loop]["CUSTOMER"]);

                                commonGatewayObj.Insert(htTradeCustDSE, "HOWLA_CSE");
                                htTradeCustDSE = new Hashtable();
                                countRows++;
                            }
                            commonGatewayObj.CommitTransaction();
                            Session["dtTradeCusDataCSE"] = null;
                            dvGridCSETradeInfo.Visible = false;
                            CSETradeCustLabel.Text = " Howla CSE " + countRows.ToString() + " Rows Save Successfully";
                            CSETradeCustLabel.Style.Add("color", "#009933");
                        }
                        else
                        {
                            CSETradeCustLabel.Text = "Save Failed:Please  Press the Show CSE Howla button";
                            CSETradeCustLabel.Style.Add("color", "red");
                        }
                    }
                    else
                    {

                        CSETradeCustLabel.Text = "Save Failed: CSE Howla Already Saved On That Date";
                        CSETradeCustLabel.Style.Add("color", "red");
                    }

                }

            }
        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            CSETradeCustLabel.Text = " Howla CSE Save failed Error:" + ex.Message.ToString();
            CSETradeCustLabel.Style.Add("color", "red");
            //throw ex;
        }
    }
    
    public DataTable getdtTradeCusTable()
    {
        DataTable dtTradeCusData = new DataTable();

        dtTradeCusData.Columns.Add("SI", typeof(string));
        dtTradeCusData.Columns.Add("F_CD", typeof(string));
        dtTradeCusData.Columns.Add("BR_CD", typeof(string));
        dtTradeCusData.Columns.Add("SP_DATE", typeof(string));
        dtTradeCusData.Columns.Add("BK_REF", typeof(string));
        dtTradeCusData.Columns.Add("HOWLA_NO", typeof(string));
        dtTradeCusData.Columns.Add("HOWLA_TP", typeof(string));
        dtTradeCusData.Columns.Add("IN_OUT", typeof(string));
        dtTradeCusData.Columns.Add("SETTLE_DT", typeof(string));
        dtTradeCusData.Columns.Add("COMP_CD", typeof(string));
        dtTradeCusData.Columns.Add("SP_QTY", typeof(string));
        dtTradeCusData.Columns.Add("SP_RATE", typeof(string));
        dtTradeCusData.Columns.Add("CL_DATE", typeof(string));
        dtTradeCusData.Columns.Add("BK_CD", typeof(string));
        dtTradeCusData.Columns.Add("HOWLA_CHG", typeof(string));
        dtTradeCusData.Columns.Add("LAGA_CHG", typeof(string));
        dtTradeCusData.Columns.Add("VOUCH_NO", typeof(string));
        dtTradeCusData.Columns.Add("VOUCH_DT", typeof(string));
        dtTradeCusData.Columns.Add("VOUCH_REF", typeof(string));
        dtTradeCusData.Columns.Add("CH_AC_NO", typeof(string));
        dtTradeCusData.Columns.Add("BN_NAME", typeof(string));
        dtTradeCusData.Columns.Add("CHQ_DT", typeof(string));
        dtTradeCusData.Columns.Add("CHQ_NO", typeof(string));
        dtTradeCusData.Columns.Add("OP_NAME", typeof(string));
        dtTradeCusData.Columns.Add("N_P", typeof(string));
        dtTradeCusData.Columns.Add("QTY_ALLOC", typeof(string));
        dtTradeCusData.Columns.Add("ISIN_CD", typeof(string));
        dtTradeCusData.Columns.Add("FORGN_FLG", typeof(string));
        dtTradeCusData.Columns.Add("SPOT_ID", typeof(string));
        dtTradeCusData.Columns.Add("INSTR_GRP", typeof(string));
        dtTradeCusData.Columns.Add("MARKT_TP", typeof(string));
        dtTradeCusData.Columns.Add("CUSTOMER", typeof(string));
        dtTradeCusData.Columns.Add("BOID", typeof(string));
        return dtTradeCusData;
    }
    protected void HowlaDateTextBox_TextChanged(object sender, EventArgs e)
    {
        dvGridDSETradeInfo.Visible = false;
        dvGridCSETradeInfo.Visible = false;
       
        if (pfolioBLObj.getHowlaUpdateStatus(HowlaDateTextBox.Text.ToString(), "DSE"))
        {
           
            DSETradeCustLabel.Text = "DSE Howla Already Saved On That Date";
            DSETradeCustLabel.Style.Add("color", "#009933");
        }
        else
        {
            
            DSETradeCustLabel.Text = "DSE Howla Should Save On That Date";
            DSETradeCustLabel.Style.Add("color", "red");
        }

        if (pfolioBLObj.getHowlaUpdateStatus(HowlaDateTextBox.Text.ToString(), "CSE"))
        {

            CSETradeCustLabel.Text = "CSE Howla Already Saved On That Date";
            CSETradeCustLabel.Style.Add("color", "#009933");
        }
        else
        {

            CSETradeCustLabel.Text = "CSE Howla Should Save On That Date";
            CSETradeCustLabel.Style.Add("color", "red");
        }

    }
}
