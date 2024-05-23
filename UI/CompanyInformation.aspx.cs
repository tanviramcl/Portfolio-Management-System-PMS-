using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_CompanyInformation : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();

    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DataTable dtSectorNameDropDownList = dropDownListObj.FillSectorDropDownList();
        DataTable dtlistedCategoryDropDownList = dropDownListObj.FilllistedCategoryDropDownList();
        if (!IsPostBack)
        {
            sectorDropDownList.DataSource = dtSectorNameDropDownList;
            sectorDropDownList.DataTextField = "SECT_MAJ_NM";
            sectorDropDownList.DataValueField = "SECT_MAJ_CD";
            sectorDropDownList.DataBind();

            listedCategoryDropDownList.DataSource = dtlistedCategoryDropDownList;
            listedCategoryDropDownList.DataTextField = "flagValue";
            listedCategoryDropDownList.DataValueField = "flag";
            listedCategoryDropDownList.DataBind();
        }
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(5000);
    }



    [System.Web.Services.WebMethod]

    public static bool InsertandUpdateCompany(string CompanyCode, string companyName, string dsecode, string PaidUpCapital, string atho_cap, string totalshare, string faceValue, string MarketLot, string sector, string product, string category, string avarageMarketRate, string baserate, string baseupdateDate, string lasttradingdate, string flug, string group, string floatdatefrom, string floatdateto, string csecode, string address1, string address2, string regoffice, string phnNo, string openingdate, string premium, string RIssuefrom, string RIssueto, string mergin, string IsBuySellChargeApplicable, string Additionalbuysellcharge, string AdditionalbuysellCommision,string propectusPublishDate,string IpoType,string marketType,string ISSUE_MNG)
     {


        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable dtsource = new DataTable();
        string strUPQuery = "";
        string strInsQuery = "";

        string strIsBuySellChargeApplicable = "";
        string strAdditionalbuysellcharge = "";
        string strAdditionalbuysellCommision = "";
        string strLastTradindate, stropeningdate,strbaseupdatedDate, strfloatdatefromdate, strfloatdateTodate, strRIssuefromdate, strRIssuetodate, strProsPublishDate,strgroup;
        string strcategory, strIpoType, strmarketType;
        DateTime? dtimeLastTradindate, dtimeopeningdate,dtimebaseUpdateddate, dtpropectuspublishingdate,dtimefloatdatefromdate, dtimefloatdateTo, dtimeRIssuefromfromdate, dtimeRIssuetodate;


        string userId = (string)HttpContext.Current.Session["UserID"].ToString();
        DateTime dtimeCurrentDateTimeForLog = DateTime.Now;
        string strCurrentDateTimeForLog = dtimeCurrentDateTimeForLog.ToString("dd-MMM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);


        if (!string.IsNullOrEmpty(lasttradingdate))
        {

           // string lasttradingdate = Convert.ToDateTime(lasttradingdate).ToString("dd-MMM-yyyy");
           // dtimeLastTradindate = Convert.ToDateTime(lasttradingdate);

            strLastTradindate = Convert.ToDateTime(lasttradingdate).ToString("dd-MMM-yyyy");
        }
        else
        {
            dtimeLastTradindate = null;
            strLastTradindate = "";
        }

        if (!string.IsNullOrEmpty(openingdate))
        {
            dtimeopeningdate = Convert.ToDateTime(openingdate);

            stropeningdate = dtimeopeningdate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtimeopeningdate = null;
            stropeningdate = "";
        }

     
        if (!string.IsNullOrEmpty(baseupdateDate))
        {
            dtimebaseUpdateddate = Convert.ToDateTime(baseupdateDate);

            strbaseupdatedDate = dtimebaseUpdateddate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtimebaseUpdateddate = null;
            strbaseupdatedDate = "";
        }
        if (!string.IsNullOrEmpty(propectusPublishDate))
        {
            dtpropectuspublishingdate = Convert.ToDateTime(propectusPublishDate);

            strProsPublishDate = dtpropectuspublishingdate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtpropectuspublishingdate = null;
            strProsPublishDate = "";
        }


        if (!string.IsNullOrEmpty(floatdatefrom))
        {
            dtimefloatdatefromdate = Convert.ToDateTime(floatdatefrom);

            strfloatdatefromdate = dtimefloatdatefromdate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtimefloatdatefromdate = null;
            strfloatdatefromdate = "";
        }

        if (!string.IsNullOrEmpty(floatdateto))
        {
            dtimefloatdateTo = Convert.ToDateTime(floatdateto);

            strfloatdateTodate = dtimefloatdateTo.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtimefloatdateTo = null;
            strfloatdateTodate = "";
        }


        if (!string.IsNullOrEmpty(RIssuefrom))
        {
            dtimeRIssuefromfromdate = Convert.ToDateTime(RIssuefrom);

            strRIssuefromdate = dtimeRIssuefromfromdate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtimeRIssuefromfromdate = null;
            strRIssuefromdate = "";
        }

        if (!string.IsNullOrEmpty(RIssueto))
        {
            dtimeRIssuetodate = Convert.ToDateTime(RIssueto);

            strRIssuetodate = dtimeRIssuetodate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtimeRIssuetodate = null;
            strRIssuetodate = "";
        }

        if (IsBuySellChargeApplicable == "undefined")
        {
            strIsBuySellChargeApplicable = "";
        }
        else
        {
            strIsBuySellChargeApplicable = IsBuySellChargeApplicable;
        }

        if (Additionalbuysellcharge == "undefined")
        {
            strAdditionalbuysellcharge = "";
        }
        else
        {
            strAdditionalbuysellcharge = Additionalbuysellcharge;
        }

        if (AdditionalbuysellCommision == "undefined")
        {
            strAdditionalbuysellCommision = "";
        }
        else
        {
            strAdditionalbuysellCommision = Additionalbuysellcharge;
        }

        if (category == "0")
        {
            strcategory = "";
        }
        else
        {
            strcategory = category;
        }


        if (group == "0")
        {
            strgroup = "";
        }
        else
        {
            strgroup = group;
        }

        if (IpoType == "0")
        {
            strIpoType = "";
        }
        else
        {
            strIpoType = IpoType;
        }


        if (marketType == "0")
        {
            strmarketType = "";
        }
        else
        {
            strmarketType = marketType;
        }
        if (CompanyCode != "")
        {
            string Query = "select comp_nm,mlot,fc_val,avg_rt,rt_upd_dt,instr_cd,cseinstr_cd,trade_meth,sect_maj_cd,cat_tp,add1,add2,reg_off,opn_dt,tel,prod,atho_cap,paid_cap,no_shrs,sbase_rt,base_upd_dt,flot_dt_fm,flot_dt_to,margin,flag,premium,rissu_dt_fm,rissu_dt_to, UPD_DATE_TIME,  OP_NAME, ISSUE_MNG, MARKETTYPE,IPOTYPE, PROS_PUB_DT from comp where comp_cd ='" + CompanyCode + "'";

            dtsource = commonGatewayObj.Select(Query.ToString());
            if (dtsource.Rows.Count > 0)
            {
                 
                DataTable companyinfoupdate = new DataTable();


                if (IsBuySellChargeApplicable == "N")
                {
                    string strDlQuery = "update comp set ISADD_HOWLACHARGE_DSE =null,ADD_HOWLACHARGE_AMTDSE =0,EXCEP_BUYSL_COMPCT_DSE =0 where comp_cd =" + CompanyCode.ToString() + "";

                    int delNumOfRows = commonGatewayObj.ExecuteNonQuery(strDlQuery);
                
                    strUPQuery = "update comp set instr_cd ='" + dsecode.ToString() + "',comp_nm ='" + companyName.ToString() + "',cseinstr_cd ='" + csecode.ToString() + "',trade_meth ='" + strgroup.ToString() + "',flag='" + flug + "',add1 ='" + address1.ToString() + "',add2 ='" + address2.ToString() + "',reg_off ='" + regoffice.ToString() + "',tel ='" + phnNo.ToString() + "',atho_cap = '" + atho_cap.ToString() + "',paid_cap =" + PaidUpCapital.ToString() + ",no_shrs =" + totalshare.ToString() + ",fc_val ='" + faceValue.ToString() + "',mlot ='" + MarketLot.ToString() + "',flot_dt_fm ='" + strfloatdatefromdate + "',flot_dt_to ='" + strfloatdateTodate + "',rissu_dt_fm ='" + strRIssuefromdate + "',rissu_dt_to ='" + strRIssuetodate + "',sbase_rt =" + baserate.ToString() + ",base_upd_dt ='" + strbaseupdatedDate + "',avg_rt ='" + avarageMarketRate.ToString() + "',rt_upd_dt ='" + strLastTradindate + "',margin = '" + mergin.ToString() + "',PROD='" + product + "',premium ='" + premium.ToString() + "',sect_maj_cd ='" + sector.ToString() + "',opn_dt ='" + stropeningdate + "',cat_tp ='" + strcategory.ToString() + "',PROS_PUB_DT='"+ strProsPublishDate + "',IPOTYPE='"+ strIpoType + "',MARKETTYPE='"+ strmarketType + "',ISSUE_MNG='"+ ISSUE_MNG + "',OP_NAME='"+userId+"',UPD_DATE_TIME='"+strCurrentDateTimeForLog+"' where comp_cd =" + CompanyCode.ToString() + "";
                }
                else
                {
                    strUPQuery = "update comp set instr_cd ='" + dsecode.ToString() + "',comp_nm ='" + companyName.ToString() + "',cseinstr_cd ='" + csecode.ToString() + "',trade_meth ='" + strgroup.ToString() + "',flag='"+ flug + "',add1 ='" + address1.ToString() + "',add2 ='" + address2.ToString() + "',reg_off ='" + regoffice.ToString() + "',tel ='" + phnNo.ToString() + "',atho_cap = '" + atho_cap.ToString() + "',paid_cap =" + PaidUpCapital.ToString() + ",no_shrs =" + totalshare.ToString() + ",fc_val ='" + faceValue.ToString() + "',mlot ='" + MarketLot.ToString() + "',flot_dt_fm ='" + strfloatdatefromdate + "',flot_dt_to ='" + strfloatdateTodate + "',rissu_dt_fm ='" + strRIssuefromdate + "',rissu_dt_to ='" + strRIssuetodate + "',sbase_rt =" + baserate.ToString() + ",base_upd_dt ='" + strbaseupdatedDate + "',avg_rt ='" + avarageMarketRate.ToString() + "',rt_upd_dt ='" + strLastTradindate + "',margin = '" + mergin.ToString() + "',PROD='" + product + "',premium ='" + premium.ToString() + "',sect_maj_cd ='" + sector.ToString() + "',opn_dt ='" + stropeningdate + "',cat_tp ='" + strcategory.ToString() + "',ISADD_HOWLACHARGE_DSE ='" + strIsBuySellChargeApplicable.ToString() + "',ADD_HOWLACHARGE_AMTDSE ='" + strAdditionalbuysellcharge.ToString() + "',EXCEP_BUYSL_COMPCT_DSE ='" + strAdditionalbuysellCommision.ToString() + "',PROS_PUB_DT='" + strProsPublishDate + "',IPOTYPE='" + strIpoType + "',MARKETTYPE='" + strmarketType  + "',ISSUE_MNG='" + ISSUE_MNG + "',OP_NAME='" + userId + "',UPD_DATE_TIME='" + strCurrentDateTimeForLog + "' where comp_cd =" + CompanyCode.ToString() + "";
                }



                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUPQuery);
                return true;

            }
            else
            {

                DataTable companyinfo = new DataTable();

                if (IsBuySellChargeApplicable == "N")
                {
                    strInsQuery = "insert into comp(comp_cd,comp_nm,instr_cd,paid_cap,no_shrs,ATHO_CAP,fc_val,mlot, sbase_rt, base_upd_dt, avg_rt,rt_upd_dt,cat_tp,sect_maj_cd,trade_meth,flot_dt_fm,flot_dt_to,cseinstr_cd,flag,add1,add2,reg_off,tel,opn_dt,premium,rissu_dt_fm,rissu_dt_to,margin,PROS_PUB_DT,IPOTYPE,MARKETTYPE,ISSUE_MNG,OP_NAME,UPD_DATE_TIME)values(" + CompanyCode + ",'" + companyName.ToString() + "','" + dsecode.ToString() + "','" + Convert.ToDouble(PaidUpCapital) + "','" + totalshare + "','"+ atho_cap + "','" + Convert.ToUInt32(faceValue.ToString()) + "','" + MarketLot + "','" + Convert.ToDouble(baserate) + "','" + strbaseupdatedDate + "','" + avarageMarketRate + "','" + strLastTradindate + "','" + strcategory + "','" + sector.ToString() + "','" + strgroup + "','" + strfloatdatefromdate + "','" + strfloatdatefromdate + "','" + csecode + "','" + flug + "','" + address1.ToString() + "','" + address2.ToString() + "','" + regoffice.ToString() + "','" + phnNo.ToString() + "','" + stropeningdate + "','" + premium.ToString() + "','" + strRIssuefromdate + "','" + strRIssuetodate + "','" + mergin.ToString() + "','"+ strProsPublishDate + "','"+ strIpoType + "','"+ strmarketType + "','"+ISSUE_MNG+"','"+ userId + "','"+strCurrentDateTimeForLog+"')";
                }
                else
                {
                    strInsQuery = "insert into comp(comp_cd,comp_nm,instr_cd,paid_cap,no_shrs,ATHO_CAP,fc_val,mlot, sbase_rt, base_upd_dt, avg_rt,rt_upd_dt,cat_tp,sect_maj_cd,trade_meth,flot_dt_fm,flot_dt_to,cseinstr_cd,flag,add1,add2,reg_off,tel,opn_dt,premium,rissu_dt_fm,rissu_dt_to,margin,ISADD_HOWLACHARGE_DSE,ADD_HOWLACHARGE_AMTDSE,EXCEP_BUYSL_COMPCT_DSE,PROS_PUB_DT,IPOTYPE,MARKETTYPE,ISSUE_MNG,OP_NAME,UPD_DATE_TIME)values(" + CompanyCode + ",'" + companyName.ToString() + "','" + dsecode.ToString() + "','" + Convert.ToDouble(PaidUpCapital) + "','" + totalshare + "','"+ atho_cap + "','" + Convert.ToUInt32(faceValue.ToString()) + "','" + MarketLot + "','" + Convert.ToDouble(baserate) + "','" + strbaseupdatedDate + "','" + avarageMarketRate + "','" + strLastTradindate + "','" + strcategory + "','" + sector.ToString() + "','" + strgroup + "','" + strfloatdatefromdate + "','" + strfloatdateTodate + "','" + csecode + "','" + flug + "','" + address1.ToString() + "','" + address2.ToString() + "','" + regoffice.ToString() + "','" + phnNo.ToString() + "','" + stropeningdate + "','" + premium.ToString() + "','" + strRIssuefromdate + "','" + strRIssuetodate + "','" + mergin.ToString() + "','" + strIsBuySellChargeApplicable.ToString() + "','" + strAdditionalbuysellcharge.ToString() + "','" + strAdditionalbuysellCommision.ToString() + "','" + strProsPublishDate + "','" + strIpoType + "','" + strmarketType + "','" + ISSUE_MNG + "','" + userId + "','" + strCurrentDateTimeForLog + "')";
                }


                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                return true;


            }
        }


        return false;
    }

   

    protected void ddlSellBuyCharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;
        lblTexAdditionalbuysellcharge.Visible = false;
        txtTexAdditionalbuysellcharge.Visible = false;
        txtEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;

        string sellBuyChage = ddltxtIsBuySellChargeApplicable.Text.ToString();

        if (sellBuyChage == "Y")
        {
            lblEXCEP_BUYSL_COMPCT_APPLDSE.Visible = true;
            lblTexAdditionalbuysellcharge.Visible = true;
            txtTexAdditionalbuysellcharge.Visible = true;
            txtEXCEP_BUYSL_COMPCT_APPLDSE.Visible = true;
        }
        else
        {
            lblEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;
            lblTexAdditionalbuysellcharge.Visible = false;
            txtTexAdditionalbuysellcharge.Visible = false;
            txtEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;
        }
    }
    protected void companyCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        DataTable dtsource = new DataTable();
        List<CompanyInfo> companyInfolist = new List<CompanyInfo>();
        string Query = "select comp_cd,comp_nm,mlot,fc_val,avg_rt,TO_CHAR(rt_upd_dt,'dd-MON-yyyy')rt_upd_dt,instr_cd,cseinstr_cd,trade_meth,sect_maj_cd,cat_tp,add1,add2,reg_off,TO_CHAR(opn_dt,'dd-MON-yyyy')opn_dt,tel,prod,atho_cap,paid_cap,no_shrs,sbase_rt,TO_CHAR(base_upd_dt,'dd-MON-yyyy')base_upd_dt,TO_Char(flot_dt_fm,'dd-MON-yyyy')flot_dt_fm,TO_CHAR(flot_dt_to,'dd-MON-yyyy')flot_dt_to,margin,flag,premium,TO_CHAR(rissu_dt_fm,'dd-MON-yyyy')rissu_dt_fm,TO_CHAR(rissu_dt_to,'dd-MON-yyyy')rissu_dt_to,ISADD_HOWLACHARGE_DSE,ADD_HOWLACHARGE_AMTDSE,EXCEP_BUYSL_COMPCT_DSE,UPD_DATE_TIME,  OP_NAME, ISSUE_MNG, MARKETTYPE,IPOTYPE,TO_CHAR(PROS_PUB_DT,'dd-MON-yyyy')PROS_PUB_DT  from comp where comp_cd ='" + companyCodeTextBox.Text + "'";

        dtsource = commonGatewayObj.Select(Query.ToString());

        if (dtsource.Rows.Count > 0)
        {

            companyInfolist = (from DataRow dr in dtsource.Rows
                               select new CompanyInfo()
                               {
                                   COMP_CD = dr["COMP_CD"].ToString(),
                                   COMP_NM = dr["COMP_NM"].ToString(),
                                   SECT_MAJ_CD = dr["SECT_MAJ_CD"].ToString(),
                                   CAT_TP = dr["CAT_TP"].ToString(),
                                   MLOT = dr["MLOT"].ToString(),
                                   AVG_RT = dr["AVG_RT"].ToString(),
                                   FC_VAL = dr["FC_VAL"].ToString(),
                                   RT_UPD_DT = dr["RT_UPD_DT"].ToString(),
                                   INSTR_CD = dr["INSTR_CD"].ToString(),
                                   CSE_INSTR_CD = dr["cseinstr_cd"].ToString(),
                                   TRADE_METH = dr["TRADE_METH"].ToString(),
                                   OPN_DT = dr["OPN_DT"].ToString(),
                                   ADD1 = dr["ADD1"].ToString(),
                                   ADD2 = dr["ADD2"].ToString(),
                                   TEL = dr["TEL"].ToString(),
                                   ATHO_CAP = dr["ATHO_CAP"].ToString(),
                                   REG_OFF = dr["REG_OFF"].ToString(),
                                   PAID_CAP = dr["PAID_CAP"].ToString(),
                                   NO_SHRS = dr["NO_SHRS"].ToString(),
                                   SBASE_RT = dr["SBASE_RT"].ToString(),
                                   PROD = dr["PROD"].ToString(),
                                   BASE_UPD_DT = dr["BASE_UPD_DT"].ToString(),
                                   FLOT_DT_FM = dr["FLOT_DT_FM"].ToString(),
                                   FLOT_DT_TO = dr["FLOT_DT_TO"].ToString(),
                                   RISSU_DT_FM = dr["RISSU_DT_FM"].ToString(),
                                   RISSU_DT_TO = dr["RISSU_DT_TO"].ToString(),
                                   MARGIN = dr["MARGIN"].ToString(),
                                   PREMIUM = dr["PREMIUM"].ToString(),
                                   FLAG = dr["FLAG"].ToString(),
                                   ISADD_HOWLACHARGE_DSE = dr["ISADD_HOWLACHARGE_DSE"].ToString(),
                                   ADD_HOWLACHARGE_AMTDSE = dr["ADD_HOWLACHARGE_AMTDSE"].ToString(),
                                   EXCEP_BUYSL_COMPCT_DSE = dr["EXCEP_BUYSL_COMPCT_DSE"].ToString(),
                                   UPD_DATE_TIME = dr["UPD_DATE_TIME"].ToString(),
                                   OP_NAME = dr["OP_NAME"].ToString(),
                                   ISSUE_MNG= dr["ISSUE_MNG"].ToString(),
                                   MARKETTYPE= dr["MARKETTYPE"].ToString(),
                                   IPOTYPE= dr["IPOTYPE"].ToString(),
                                   PROS_PUB_DT= dr["PROS_PUB_DT"].ToString(),


                               }).ToList();


            foreach (CompanyInfo compInfo in companyInfolist)
            {
                companyCodeTextBox.Text = compInfo.COMP_CD;
                companyNameTextBox.Text = compInfo.COMP_NM;
                dsecodeTextBox.Text = compInfo.INSTR_CD;
                TextBoxPaidUpCapital.Text = compInfo.PAID_CAP;
                totalshareTextBox.Text = compInfo.NO_SHRS;
                faceValueTextBox.Text = compInfo.FC_VAL;
                TextBoxPaidUpCapital.Text = compInfo.PAID_CAP;
                MarketLotTextBox.Text = compInfo.MLOT;
                baserateTextBox.Text = compInfo.SBASE_RT;
                productTextBox.Text = compInfo.PROD;
                baseupdateDateTextBox.Text = compInfo.BASE_UPD_DT;
                avarageMarketRateTextBox.Text = compInfo.AVG_RT;
                lasttradingdateTextBox.Text = compInfo.RT_UPD_DT;
                categoryDropDownList.SelectedValue = compInfo.CAT_TP;
                sectorDropDownList.SelectedValue = compInfo.SECT_MAJ_CD;
                groupDropDownList.SelectedValue = compInfo.TRADE_METH;
                floatdatefromTextBox.Text = compInfo.FLOT_DT_FM;
                floatdatetoTextBox.Text = compInfo.FLOT_DT_TO;
                csecodeTextBox.Text = compInfo.CSE_INSTR_CD;
                listedCategoryDropDownList.SelectedValue = compInfo.FLAG;
                addressTextBox1.Value = compInfo.ADD1;
                addressTextBox2.Value = compInfo.ADD2;
                regofficeTextBox2.Text = compInfo.REG_OFF;
                phnNoTextBox.Text = compInfo.TEL;
                openingdateTextBox.Text = compInfo.OPN_DT;
                premiumTextBox.Text = compInfo.PREMIUM;
                RIssuefromTextBox.Text = compInfo.RISSU_DT_FM;
                
                RIssuetoTextBox.Text = compInfo.RISSU_DT_TO;
                merginTextBox.Text = compInfo.MARGIN;
                TextBoxAuthorizedcapital.Text = compInfo.ATHO_CAP;
                ddltxtIsBuySellChargeApplicable.DataValueField = compInfo.ISADD_HOWLACHARGE_DSE;
                txtProspectusPublishDate.Text = compInfo.PROS_PUB_DT;
                txtissueTextBox.Text = compInfo.ISSUE_MNG;
                marketDropDownList.SelectedValue = compInfo.MARKETTYPE;
                IPODropDownList.SelectedValue = compInfo.IPOTYPE;



                if (ddltxtIsBuySellChargeApplicable.DataValueField == "Y")
                {
                    lblEXCEP_BUYSL_COMPCT_APPLDSE.Visible = true;
                    lblTexAdditionalbuysellcharge.Visible = true;
                    txtTexAdditionalbuysellcharge.Visible = true;
                    txtEXCEP_BUYSL_COMPCT_APPLDSE.Visible = true;
                    ddltxtIsBuySellChargeApplicable.Text = "Y";
                    txtTexAdditionalbuysellcharge.Text = compInfo.ADD_HOWLACHARGE_AMTDSE;
                    txtEXCEP_BUYSL_COMPCT_APPLDSE.Text = compInfo.EXCEP_BUYSL_COMPCT_DSE;
                }


            }
        }
        else
        {
            ClearFields();

        }

    }

    protected void txtIsBuySellChargeApplicable_TextChanged(object sender, EventArgs e)
    {


        lblEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;
        lblTexAdditionalbuysellcharge.Visible = false;
        txtTexAdditionalbuysellcharge.Visible = false;

        txtEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;

        string sellBuyChage = ddltxtIsBuySellChargeApplicable.Text.ToString();

        if (sellBuyChage == "y" || sellBuyChage == "Y")
        {
            lblEXCEP_BUYSL_COMPCT_APPLDSE.Visible = true;
            lblTexAdditionalbuysellcharge.Visible = true;
            txtTexAdditionalbuysellcharge.Visible = true;
            txtEXCEP_BUYSL_COMPCT_APPLDSE.Visible = true;
        }
        else
        {
            lblEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;
            lblTexAdditionalbuysellcharge.Visible = false;
            txtTexAdditionalbuysellcharge.Visible = false;
            txtEXCEP_BUYSL_COMPCT_APPLDSE.Visible = false;
        }

    }



    private void ClearFields()
    {
        //  companyCodeTextBox.Text = "";
        companyNameTextBox.Text = "";
        dsecodeTextBox.Text = "";
        TextBoxPaidUpCapital.Text = "";
        totalshareTextBox.Text = "";
        faceValueTextBox.Text = "";
        TextBoxPaidUpCapital.Text = "";
        MarketLotTextBox.Text = "";
        baserateTextBox.Text = "";
        baseupdateDateTextBox.Text = "";
        avarageMarketRateTextBox.Text = "";
        lasttradingdateTextBox.Text = "";
        categoryDropDownList.SelectedValue = "0";
        sectorDropDownList.Text = "0";
        groupDropDownList.SelectedValue = "0";
        floatdatefromTextBox.Text = "";
        floatdatetoTextBox.Text = "";
        csecodeTextBox.Text = "";
        listedCategoryDropDownList.SelectedValue = "L";
        addressTextBox1.Value = "";
        addressTextBox2.Value = "";
        regofficeTextBox2.Text = "";
        phnNoTextBox.Text = "";
        openingdateTextBox.Text = "";
        premiumTextBox.Text = "";
        RIssuefromTextBox.Text = "";
        RIssuetoTextBox.Text = "";
        merginTextBox.Text = "";
        TextBoxAuthorizedcapital.Text = "";
        productTextBox.Text = "";
        marketDropDownList.SelectedValue = "0";
        IPODropDownList.SelectedValue = "0";
        txtProspectusPublishDate.Text = "";
        txtissueTextBox.Text = "";



    }

    public class CompanyInfo
    {
        public string COMP_CD { get; set; }
        public string COMP_NM { get; set; }
        public string SECT_MAJ_CD { get; set; }
        public string CAT_TP { get; set; }
        public string MLOT { get; set; }
        public string AVG_RT { get; set; }
        public string FC_VAL { get; set; }
        public string RT_UPD_DT { get; set; }
        public string INSTR_CD { get; set; }
        public string CSE_INSTR_CD { get; set; }
        public string TRADE_METH { get; set; }
        public string OPN_DT { get; set; }
        public string ADD1 { get; set; }
        public string ADD2 { get; set; }
        public string TEL { get; set; }
        public string ATHO_CAP { get; set; }
        public string REG_OFF { get; set; }
        public string PAID_CAP { get; set; }
        public string NO_SHRS { get; set; }
        public string SBASE_RT { get; set; }
        public string PROD { get; set; }
        public string BASE_UPD_DT { get; set; }
        public string FLOT_DT_FM { get; set; }
        public string FLOT_DT_TO { get; set; }
        public string RISSU_DT_FM { get; set; }
        public string RISSU_DT_TO { get; set; }
        public string MARGIN { get; set; }
        public string PREMIUM { get; set; }
        public string FLAG { get; set; }
        public string ADD_HOWLACHARGE_AMTDSE { get; set; }
        public string ISADD_HOWLACHARGE_DSE { get; set; }
        public string EXCEP_BUYSL_COMPCT_DSE { get; set; }
        public string UPD_DATE_TIME { get; set; }
        public string OP_NAME { get; set; }
        public string ISSUE_MNG { get; set; }
        public string MARKETTYPE { get; set; }
        public string IPOTYPE { get; set; }
        public string PROS_PUB_DT { get; set; }

    }


}