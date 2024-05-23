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
using System.Text.RegularExpressions;

public partial class UI_HowlaDSEentryForm : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1s1DAOObj = new Pf1s1DAO();
    DividendDAO dividendDAOObj = new DividendDAO();
    Message msgObj = new Message();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
    }
    
    protected void showDataButton_Click(object sender, EventArgs e)
    {
        string LoginID = Session["UserID"].ToString();
        string LoginName = Session["UserName"].ToString().ToUpper();
        
        try
        {
            if (tradeCusFileUpload.PostedFile.ContentLength > 0)
            {
                string FileName = tradeCusFileUpload.PostedFile.FileName.ToString();
                if (File.Exists(FileName))
                {
                    DataTable dtTradeCusData = pf1s1DAOObj.getdtTradeCusTable();
                    int serial = 1;
                    DataRow drTradeCusdata;
                    StreamReader srFileReader;
                    string line;

                    srFileReader = new StreamReader(FileName);
                    string[] lineContent;
                    while (srFileReader.Peek() != -1)
                    {
                        double lagaCharge = 0.00;
                        int tradeQty,fundCode,compCode;
                        double tradePrice=0.00;

                        line = srFileReader.ReadLine();
                        lineContent = line.Split('~');
                        if (Regex.IsMatch(lineContent[13], "AMCL") || Regex.IsMatch(lineContent[13], "AMCUF") || Regex.IsMatch(lineContent[13], "AMF1") || Regex.IsMatch(lineContent[13], "AMCPF") ||
                            Regex.IsMatch(lineContent[13], "AIMF") || Regex.IsMatch(lineContent[13], "ANRB2") || Regex.IsMatch(lineContent[13], "PR1") || Regex.IsMatch(lineContent[13], "ANRB1") ||
                            Regex.IsMatch(lineContent[13], "PH1") || Regex.IsMatch(lineContent[13], "AMF2") || Regex.IsMatch(lineContent[13], "EPF1") || Regex.IsMatch(lineContent[13], "PBM1") ||
                            Regex.IsMatch(lineContent[13], "ANRB3") || Regex.IsMatch(lineContent[13], "IF1") || Regex.IsMatch(lineContent[13], "1SB") || Regex.IsMatch(lineContent[13], "1AG") ||
                            Regex.IsMatch(lineContent[13], "BDF") || Regex.IsMatch(lineContent[13], "IAEPF") || Regex.IsMatch(lineContent[13], "AMF1") || Regex.IsMatch(lineContent[13], "AMCPF"))
                        {
                            drTradeCusdata = dtTradeCusData.NewRow();
                            drTradeCusdata["SI"] = serial.ToString();
                            fundCode = pf1s1DAOObj.GetFundOrCompCode("TEST.CUSTOMER", "F_CD", "CUST_ID = '" + lineContent[13] + "'");
                            drTradeCusdata["F_CD"] = fundCode;
                            drTradeCusdata["SP_DATE"] = Convert.ToDateTime(tradingDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy");
                            drTradeCusdata["BK_REF"] = lineContent[0].ToString().Trim().ToUpper();
                            drTradeCusdata["HOWLA_NO"] = lineContent[15].ToString().Trim();
                            drTradeCusdata["HOWLA_TP"] = lineContent[11].ToString().Trim().ToUpper();
                            if (lineContent[4] == "B")
                            {
                                drTradeCusdata["IN_OUT"] = "I";
                            }
                            else if (lineContent[4] == "S")
                            {
                                drTradeCusdata["IN_OUT"] = "O";
                            }
                            drTradeCusdata["SETTLE_DT"] = Convert.ToDateTime(tradingDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy");
                            compCode = pf1s1DAOObj.GetFundOrCompCode("COMP", "COMP_CD", "INSTR_CD = '" + lineContent[1] + "'");
                            drTradeCusdata["COMP_CD"] = compCode;
                            drTradeCusdata["SP_QTY"] = lineContent[5].ToString().Trim();
                            drTradeCusdata["SP_RATE"] = lineContent[6].ToString().Trim().ToUpper();
                            drTradeCusdata["CL_DATE"] = Convert.ToDateTime(clearingDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy");
                            drTradeCusdata["BK_CD"] = "DSE/129";
                            drTradeCusdata["HOWLA_CHG"] = "2";
                            
                            tradeQty = Convert.ToInt32(lineContent[5].Trim());
                            tradePrice = Convert.ToDouble(lineContent[6].Trim());
                            lagaCharge = 0.0002 * tradeQty * tradePrice;
                            
                            drTradeCusdata["LAGA_CHG"] = lagaCharge;
                            drTradeCusdata["VOUCH_REF"] = lineContent[3].ToString().ToUpper();
                            drTradeCusdata["OP_NAME"] = LoginID;
                            drTradeCusdata["N_P"] = lineContent[10].ToString().Trim().ToUpper();
                            drTradeCusdata["ISIN_CD"] = lineContent[2].ToString().Trim().ToUpper();
                            drTradeCusdata["FORGN_FLG"] = lineContent[12].ToString().Trim().ToUpper();
                            drTradeCusdata["SPOT_ID"] = lineContent[16].ToString().Trim().ToUpper();
                            drTradeCusdata["INSTR_GRP"] = lineContent[17].ToString().Trim().ToUpper();
                            drTradeCusdata["MARKT_TP"] = lineContent[9].ToString().Trim().ToUpper();
                            drTradeCusdata["CUSTOMER"] = lineContent[13].ToString().Trim().ToUpper();
                            drTradeCusdata["BOID"] = lineContent[14].ToString().Trim().ToUpper();
                            
                            dtTradeCusData.Rows.Add(drTradeCusdata);
                            serial++;
                        }
                    }

                    if (dtTradeCusData.Rows.Count > 0)
                    {
                        grdShowDetails.DataSource = dtTradeCusData;
                        grdShowDetails.DataBind();
                        Session["dtTradeCusData"] = dtTradeCusData;
                        SaveButton.Visible = true;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            string strMessage = string.Format("Upload Failed! {0}", Ex.Message);
            strMessage = strMessage.Replace("\r\n", "");
            ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", string.Format("window.fnAlert(\"{0}\");", strMessage), true);
        }
    }
    protected void saveDataButton_Click(object sender, EventArgs e)
    {
        //DataTable dtDividendPara = dividendDAOObj.GetDividendPara(Convert.ToInt32(fundNameDropDownList.SelectedValue.ToString()), fyDropDownList.SelectedValue.ToString(), recordDateDropDownList.SelectedValue.ToString());
        //Session["dtDividendPara"] = dtDividendPara;
        //string isCdblDataUploadComplete = dtDividendPara.Rows[0]["IS_UPLOAD_CDBL_DATA"].Equals(DBNull.Value) ? "" : dtDividendPara.Rows[0]["IS_UPLOAD_CDBL_DATA"].ToString();

        //if (string.Compare(isCdblDataUploadComplete.ToString(), "Y", true) == 0)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('CDBL Data Upload Already Completed');", true);
        //}
        //else
        //{
        DataTable dtTradeCusData = (DataTable)Session["dtTradeCusData"];
        if (dtTradeCusData.Rows.Count > 0)
        {
            InsertHowlaData(dtTradeCusData);
        }
        //}
    }
    public void InsertHowlaData(DataTable dtTradeCusData)
    {
        if (dtTradeCusData.Rows.Count > 0)
        {
            try
            {
                //long SI = Convert.ToInt32(dividendDAOObj.GetMaxSI("CDBL_DATA", "ID")) + 1;
                int numberOfRows = 0;
                commonGatewayObj.BeginTransaction();
                Hashtable htTradeCusData = new Hashtable();
                for (int loop = 0; loop < dtTradeCusData.Rows.Count; loop++)
                {
                    //htTradeCusData.Add("ID", SI);
                    htTradeCusData.Add("F_CD", Convert.ToInt32(dtTradeCusData.Rows[loop]["F_CD"].ToString()));
                    htTradeCusData.Add("SP_DATE", dtTradeCusData.Rows[loop]["SP_DATE"].ToString());
                    htTradeCusData.Add("BK_REF", dtTradeCusData.Rows[loop]["BK_REF"].ToString().ToUpper());
                    htTradeCusData.Add("HOWLA_NO", dtTradeCusData.Rows[loop]["HOWLA_NO"].ToString());
                    htTradeCusData.Add("HOWLA_TP", dtTradeCusData.Rows[loop]["HOWLA_TP"].ToString());
                    htTradeCusData.Add("IN_OUT", dtTradeCusData.Rows[loop]["IN_OUT"].ToString().ToUpper());
                    htTradeCusData.Add("SETTLE_DT", dtTradeCusData.Rows[loop]["SETTLE_DT"].ToString());
                    htTradeCusData.Add("COMP_CD", Convert.ToInt32(dtTradeCusData.Rows[loop]["COMP_CD"].ToString()));
                    htTradeCusData.Add("SP_QTY", dtTradeCusData.Rows[loop]["SP_QTY"].ToString());
                    htTradeCusData.Add("SP_RATE", dtTradeCusData.Rows[loop]["SP_RATE"].ToString());
                    htTradeCusData.Add("CL_DATE", dtTradeCusData.Rows[loop]["CL_DATE"].ToString());
                    htTradeCusData.Add("BK_CD", dtTradeCusData.Rows[loop]["BK_CD"].ToString());
                    htTradeCusData.Add("HOWLA_CHG", dtTradeCusData.Rows[loop]["HOWLA_CHG"].ToString());
                    htTradeCusData.Add("LAGA_CHG", dtTradeCusData.Rows[loop]["LAGA_CHG"].ToString());
                    htTradeCusData.Add("VOUCH_REF", dtTradeCusData.Rows[loop]["VOUCH_REF"].ToString());
                    htTradeCusData.Add("OP_NAME", dtTradeCusData.Rows[loop]["OP_NAME"].ToString());
                    htTradeCusData.Add("N_P", dtTradeCusData.Rows[loop]["N_P"].ToString());
                    htTradeCusData.Add("ISIN_CD", dtTradeCusData.Rows[loop]["ISIN_CD"].ToString());
                    htTradeCusData.Add("FORGN_FLG", dtTradeCusData.Rows[loop]["FORGN_FLG"].ToString());
                    htTradeCusData.Add("SPOT_ID", dtTradeCusData.Rows[loop]["SPOT_ID"].ToString());
                    htTradeCusData.Add("INSTR_GRP", dtTradeCusData.Rows[loop]["INSTR_GRP"].ToString());
                    htTradeCusData.Add("MARKT_TP", dtTradeCusData.Rows[loop]["MARKT_TP"].ToString());
                    htTradeCusData.Add("CUSTOMER", dtTradeCusData.Rows[loop]["CUSTOMER"].ToString());
                    htTradeCusData.Add("BOID", dtTradeCusData.Rows[loop]["BOID"].ToString());
                    
                    commonGatewayObj.Insert(htTradeCusData, "TEST.HOWLA");
                    htTradeCusData = new Hashtable();
                   // SI++;
                    numberOfRows++;
                }
                //Hashtable htDividendParaUpdate = new Hashtable();
                //htDividendParaUpdate.Add("IS_UPLOAD_CDBL_DATA", "Y");
                //commonGatewayObj.Update(htDividendParaUpdate, "DIVIDEND_PARA", "FUND_CODE=" + Convert.ToInt32(fundNameDropDownList.SelectedValue.ToString()) + " AND FY='" + fyDropDownList.Text.ToString() + "'AND RECORD_DATE='" + recordDateDropDownList.Text.ToString() + "'");
                commonGatewayObj.CommitTransaction();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('" + numberOfRows + " Record(s) Save Successfully');", true);
            }
            catch (Exception ex)
            {
                commonGatewayObj.RollbackTransaction();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert ('" + msgObj.Error().ToString() + " " + ex.ToString() + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found.');", true);
        }
    }
}
