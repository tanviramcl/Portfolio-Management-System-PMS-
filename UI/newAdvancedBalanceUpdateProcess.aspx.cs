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
using System.Collections.Generic;
using System.Globalization;
//using AMCL.DL;
//using AMCL.BL;
//using AMCL.UTILITY;
//using AMCL.GATEWAY;
//using AMCL.COMMON;


public partial class AdvancedBalanceUpdateProcess : System.Web.UI.Page
{
    DropDownList dropDownListObj = new DropDownList();
    CommonGateway commonGatewayObj = new CommonGateway();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        if (!IsPostBack)
        {
            //Place the code here
            //  checkFundByBalanceDate();
            string strtxtBalanceDate;

            DateTime dtimeCurrentDate = DateTime.Now;

            string currentDate = Convert.ToDateTime(dtimeCurrentDate).ToString("dd-MMM-yyyy");

            if (!string.IsNullOrEmpty(txtBalanceDate.Text))
            {
                strtxtBalanceDate = txtBalanceDate.Text;
            }
            else
            {
                strtxtBalanceDate = currentDate;
            }

            if (!string.IsNullOrEmpty(strtxtBalanceDate))
            {
                txtBalanceDate.Text = strtxtBalanceDate;
                Session["BalanceDate"] = strtxtBalanceDate;
                DataTable tblAllfundInfo = getTblAllFundInfo(strtxtBalanceDate);
                grdShowDSEMP.DataSource = tblAllfundInfo;
                grdShowDSEMP.DataBind();


            }
        }

        //lblProcessing.Text = "";
        //DataTable tblAllfundInfo = getTblAllFundInfo();
        //grdShowDSEMP.DataSource = tblAllfundInfo;
        //grdShowDSEMP.DataBind();



    }


    //  ClearFields();

    /*
     * Advanced Process start

     */

    protected void balanceDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkFundByBalanceDate();

    }
    public void checkFundByBalanceDate()
    {
        DateTime? dtimeBalanceDate, dtimeLastBalDate;
        DateTime? dtimeLastUpadateDate;
        lblProcessing.Text = "";

        string strtxtBalanceDate, strlastUpdt, dtfundfolioMaxdate = "", strLastUpadateDate;

        DateTime dtimeCurrentDate = DateTime.Now;

        string currentDate = Convert.ToDateTime(dtimeCurrentDate).ToString("dd-MMM-yyyy");

        if (!string.IsNullOrEmpty(txtBalanceDate.Text))
        {
            strtxtBalanceDate = txtBalanceDate.Text;
        }
        else
        {
            strtxtBalanceDate = currentDate;
        }

        if (!string.IsNullOrEmpty(strtxtBalanceDate))
        {
            txtBalanceDate.Text = strtxtBalanceDate;
            Session["BalanceDate"] = strtxtBalanceDate;

        }

        string strNoOfMaxBaLDt = "select count(*)noofmaxbaldt  from fund_control where bal_dt = (select max(bal_dt) from fund_control)";
        DataTable dtnoOfMaxBaLDt = commonGatewayObj.Select(strNoOfMaxBaLDt);

        string strNoOfFund = "SELECT count(*)NoofFund from  FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL ORDER BY F_CD";
        DataTable dtStrNoofFund = commonGatewayObj.Select(strNoOfFund);

        if (dtnoOfMaxBaLDt.Rows[0]["noofmaxbaldt"].ToString() == dtStrNoofFund.Rows[0]["NoofFund"].ToString())
        {
            string strQLastBalDtFrFundControl = "select max(BAL_DT) as BAL_DT from fund_control";
            //select count(*)noofmaxbaldt  from fund_control where bal_dt = (select max(bal_dt) from fund_control)
            //SELECT F_NAME, F_CD FROM FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL ORDER BY F_CD

            DataTable dtFromFundControl = commonGatewayObj.Select(strQLastBalDtFrFundControl);
            if (dtFromFundControl != null && dtFromFundControl.Rows.Count > 0)
            {
                dtfundfolioMaxdate = dtFromFundControl.Rows[0]["BAL_DT"].ToString();

            }
            else
            {
                dtfundfolioMaxdate = "";
            }
        }
        else
        {
            dtfundfolioMaxdate = "";
        }



        if (!string.IsNullOrEmpty(dtfundfolioMaxdate))
        {
            dtimeLastBalDate = Convert.ToDateTime(dtfundfolioMaxdate);


        }
        else
        {
            dtimeLastBalDate = null;

        }

        if (!string.IsNullOrEmpty(strtxtBalanceDate))
        {
            dtimeBalanceDate = Convert.ToDateTime(strtxtBalanceDate);


        }
        else
        {
            dtimeBalanceDate = dtimeCurrentDate;

        }

        if (!string.IsNullOrEmpty(dtfundfolioMaxdate))
        {
            strlastUpdt = Convert.ToDateTime(dtfundfolioMaxdate).ToString("dd-MMM-yyyy");


        }
        else
        {
            strlastUpdt = "";
        }
       

        if (!string.IsNullOrEmpty(strlastUpdt))
        {
            dtimeLastUpadateDate = Convert.ToDateTime(strlastUpdt);
            strLastUpadateDate = dtimeLastUpadateDate.Value.ToString("dd-MMM-yyyy");
           
        }
        else
        {
            dtimeLastUpadateDate = null;
            strLastUpadateDate = "";
            
        }

        if (dtimeLastBalDate > dtimeBalanceDate)
        {

            btnProcess.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Balance date must be greater than " + strlastUpdt + "');", true);
        }
        else if (strtxtBalanceDate == strlastUpdt)
        {
            //lblProcessing.Text = "Data Already Updated";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data already updated!!');", true);
        }
        else
        {
            string strQuery;

            DataTable dtFromFundTransHB = new DataTable();


            strQuery = "select TO_CHAR(vch_dt,'DD-MON-YYYY')vch_dt, f_cd, comp_cd, no_share, rate, nvl(amount,0)amount,amt_aft_com, tran_tp, stock_ex from fund_trans_hb" +
            " where vch_dt='" + strtxtBalanceDate + "'" +
            " order by  vch_dt,comp_cd";

            dtFromFundTransHB = commonGatewayObj.Select(strQuery);

            if (dtFromFundTransHB != null && dtFromFundTransHB.Rows.Count > 0)
            {

                DataTable tblAllfundInfo = getTblAllFundInfo(strtxtBalanceDate);
                grdShowDSEMP.DataSource = tblAllfundInfo;
                grdShowDSEMP.DataBind();


                if (tblAllfundInfo.Rows.Count > 0)
                {
                    btnProcess.Visible = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
                }
            }
            else
            {
                btnProcess.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);

            }


        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {


        // string confirmValue = Request.Form["confirm_value"];
        string confirmValue = HiddenField1.Value;
        if (confirmValue == "Yes")
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
            DateTime? dtimeBalanceDate, dtimeLastBalDate;
            DateTime? dtimeLastUpadateDate;
            lblProcessing.Text = "";

            string strtxtBalanceDate, strlastUpdt, dtfundControlMaxdate = "", strLastUpadateDate ;

            DateTime dtimeCurrentDate = DateTime.Now;

            string currentDate = Convert.ToDateTime(dtimeCurrentDate).ToString("dd-MMM-yyyy");

            if (!string.IsNullOrEmpty(txtBalanceDate.Text))
            {
                strtxtBalanceDate = txtBalanceDate.Text;
            }
            else
            {
                strtxtBalanceDate = currentDate;
            }

            if (!string.IsNullOrEmpty(strtxtBalanceDate))
            {
                txtBalanceDate.Text = strtxtBalanceDate;
                Session["BalanceDate"] = strtxtBalanceDate;

            }

            string strNoOfMaxBaLDt = "select count(*)noofmaxbaldt  from fund_control where bal_dt = (select max(bal_dt) from fund_control)";
            DataTable dtnoOfMaxBaLDt = commonGatewayObj.Select(strNoOfMaxBaLDt);

            string strNoOfFund = "SELECT count(*)NoofFund from  FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL ORDER BY F_CD";
            DataTable dtStrNoofFund = commonGatewayObj.Select(strNoOfFund);

            if (dtnoOfMaxBaLDt.Rows[0]["noofmaxbaldt"].ToString() == dtStrNoofFund.Rows[0]["NoofFund"].ToString())
            {
                string strQLastBalDtFrFundControl = "select max(BAL_DT) as BAL_DT from fund_control";
                //select count(*)noofmaxbaldt  from fund_control where bal_dt = (select max(bal_dt) from fund_control)
                //SELECT F_NAME, F_CD FROM FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL ORDER BY F_CD

                DataTable dtFromFundControl = commonGatewayObj.Select(strQLastBalDtFrFundControl);
                if (dtFromFundControl != null && dtFromFundControl.Rows.Count > 0)
                {
                    dtfundControlMaxdate = dtFromFundControl.Rows[0]["BAL_DT"].ToString();

                }
                else
                {
                    dtfundControlMaxdate = "";
                }
            }
            else
            {
                dtfundControlMaxdate = "";
            }



            if (!string.IsNullOrEmpty(dtfundControlMaxdate))
            {
                dtimeLastBalDate = Convert.ToDateTime(dtfundControlMaxdate);


            }
            else
            {
                dtimeLastBalDate = null;

            }

            if (!string.IsNullOrEmpty(strtxtBalanceDate))
            {
                dtimeBalanceDate = Convert.ToDateTime(strtxtBalanceDate);


            }
            else
            {
                dtimeBalanceDate = dtimeCurrentDate;

            }

            if (!string.IsNullOrEmpty(dtfundControlMaxdate))
            {
                strlastUpdt = Convert.ToDateTime(dtfundControlMaxdate).ToString("dd-MMM-yyyy");


            }
            else
            {
                strlastUpdt = "";
            }


            if (!string.IsNullOrEmpty(strlastUpdt))
            {
                dtimeLastUpadateDate = Convert.ToDateTime(strlastUpdt);
                strLastUpadateDate = dtimeLastUpadateDate.Value.ToString("dd-MMM-yyyy");
                //dtimeLastUpadatePlusOneDate = dtimeLastUpadateDate.Value.AddDays(1);
                //strLastUpadatePlusOneDate = dtimeLastUpadatePlusOneDate.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                dtimeLastUpadateDate = null;
                strLastUpadateDate = "";
                //dtimeLastUpadatePlusOneDate = null;
                //strLastUpadatePlusOneDate = "";
            }

            if (dtimeLastBalDate > dtimeBalanceDate)
            {

                btnProcess.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Balance date must be greater than " + strlastUpdt + "');", true);

            }
            else if (strtxtBalanceDate == strlastUpdt)
            {
                //lblProcessing.Text = "Data Already Updated";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data already updated!!');", true);
                lblProcessing.Text = "Data already updated!!";
            }
            else  // Normal case
            {
                string strQuery;

                DataTable dtFromFundTransHB = new DataTable();


                strQuery = "select TO_CHAR(vch_dt,'DD-MON-YYYY')vch_dt, f_cd, comp_cd, no_share, rate, nvl(amount,0)amount,amt_aft_com, tran_tp, stock_ex from fund_trans_hb" +
                " where vch_dt ='" + strtxtBalanceDate + "'" +
                " order by  vch_dt,comp_cd";

                dtFromFundTransHB = commonGatewayObj.Select(strQuery);

                if (dtFromFundTransHB != null && dtFromFundTransHB.Rows.Count > 0)
                {

                    DataTable tblAllfundInfo = getTblAllFundInfo(strtxtBalanceDate);
                    grdShowDSEMP.DataSource = tblAllfundInfo;
                    grdShowDSEMP.DataBind();


                    if (tblAllfundInfo.Rows.Count > 0)
                    {
                        btnProcess.Visible = true;
                        if (tblAllfundInfo.Rows.Count > 0)
                        {
                            Save(tblAllfundInfo);
                        }
                        else
                        {
                           // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
                            lblProcessing.Text = "No Data Found";
                        }
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
                        btnProcess.Visible = false;
                        lblProcessing.Text = "No Data Found";

                    }
                }
                else
                {
                    btnProcess.Visible = false;
                   // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
                    lblProcessing.Text = "No Data Found";

                }


            }
         

        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        }
       
    }


    public DataTable getTblAllFundInfo(string balanceDate)
    {
        string strQLastBalDtFrFundControl, strBalanceDate, strLastBalDate, strLastUpadateDate, strMarketPriceDate, strLastUpadatePlusOneDate, strQForSellShares, strQForPurchaseShares, strQForMrktPrice;
        DateTime? dtimeBalanceDate, dtimeLastBalDate, dtimeLastUpadateDate, dtMarketPriceDate, dtimeLastUpadatePlusOneDate;

        string strtxtBalanceDate, strtxtLastUpadateDate, strtxtLastBalDate, txtMarketPriceDate, strtxtNoSaleRecord, strtxtNoOfSaleShare, strtxtNoPurchaseRecord, strtxtNoPurchaseShares;
        DataTable dtFromDual = new DataTable();
        DataTable dtFromFundControl = new DataTable();
        DataTable dtFromMarketPrice = new DataTable();
        DataTable dtFromFundTrHBForSale = new DataTable();
        DataTable dtFromFundTrHBForBuy = new DataTable();
        lblProcessing.Text = "";




        DataTable dtFundNameDropDownList = FundNameDropDownList();

        DataTable tblAllfundInfo = new DataTable();
        tblAllfundInfo.Columns.Add("F_CD", typeof(int));
        tblAllfundInfo.Columns.Add("F_NAME", typeof(string));
        tblAllfundInfo.Columns.Add("BalanceDate", typeof(string));
        tblAllfundInfo.Columns.Add("LastUpadateDate", typeof(string));
        tblAllfundInfo.Columns.Add("LastBalDate", typeof(string));
        tblAllfundInfo.Columns.Add("MarketPriceDate", typeof(string));
        tblAllfundInfo.Columns.Add("NoSaleRecord", typeof(string));
        tblAllfundInfo.Columns.Add("NoOfSaleShare", typeof(string));
        tblAllfundInfo.Columns.Add("PurchaseRecord", typeof(string));
        tblAllfundInfo.Columns.Add("PurchaseShares", typeof(string));


        if (dtFundNameDropDownList != null && dtFundNameDropDownList.Rows.Count > 0)
        {

            //lblProcessingRelatedMessage.Visible = true;
            //lblProcessingRelatedMessage.Text = "process is running!!!!";

            //strQuery = "SELECT TO_CHAR(SYSDATE, 'DD-MON-YYYY')currentDate FROM dual";
            //dtFromDual = commonGatewayObj.Select(strQuery);
            //strtxtBalanceDate = dtFromDual.Rows[0]["currentDate"].ToString();

            DateTime dtimeCurrentDate = DateTime.Now;

            string currentDate = Convert.ToDateTime(dtimeCurrentDate).ToString("dd-MMM-yyyy");

            if (!string.IsNullOrEmpty(balanceDate))
            {
                strtxtBalanceDate = txtBalanceDate.Text;
            }
            else
            {
                strtxtBalanceDate = currentDate;
            }

            for (int i = 0; i < dtFundNameDropDownList.Rows.Count; i++)
            {
                //if (dtFromDual.Rows.Count > 0)
                //{
                strQLastBalDtFrFundControl = "select TO_CHAR(bal_dt,'DD-MON-YYYY')lst_bal_dt from fund_control where f_cd =" + dtFundNameDropDownList.Rows[i]["F_CD"].ToString();

                dtFromFundControl = commonGatewayObj.Select(strQLastBalDtFrFundControl);
                if (dtFromFundControl != null && dtFromFundControl.Rows.Count > 0)
                {

                    strtxtLastUpadateDate = dtFromFundControl.Rows[0]["lst_bal_dt"].ToString();
                    strtxtLastBalDate = dtFromFundControl.Rows[0]["lst_bal_dt"].ToString();

                }
                else
                {
                    strtxtLastUpadateDate = "30-JUN-2005";
                    strtxtLastBalDate = "";
                }


                if (!string.IsNullOrEmpty(strtxtBalanceDate))
                {
                    dtimeBalanceDate = Convert.ToDateTime(strtxtBalanceDate);

                    strBalanceDate = dtimeBalanceDate.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    dtimeBalanceDate = null;
                    strBalanceDate = "";
                }


                if (!string.IsNullOrEmpty(strtxtLastBalDate))
                {
                    dtimeLastBalDate = Convert.ToDateTime(strtxtLastBalDate);
                    strLastBalDate = dtimeLastBalDate.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    dtimeLastBalDate = null;
                    strLastBalDate = "";
                }


                if (!string.IsNullOrEmpty(strtxtLastUpadateDate))
                {
                    dtimeLastUpadateDate = Convert.ToDateTime(strtxtLastUpadateDate);
                    strLastUpadateDate = dtimeLastUpadateDate.Value.ToString("dd-MMM-yyyy");
                    dtimeLastUpadatePlusOneDate = dtimeLastUpadateDate.Value.AddDays(1);
                    strLastUpadatePlusOneDate = dtimeLastUpadatePlusOneDate.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    dtimeLastUpadateDate = null;
                    strLastUpadateDate = "";
                    dtimeLastUpadatePlusOneDate = null;
                    strLastUpadatePlusOneDate = "";
                }



                strQForMrktPrice = "select TO_CHAR(max(Tran_date),'DD-MON-YYYY')mp_dt from market_price where tran_date <='" + strBalanceDate + "'";

                dtFromMarketPrice = commonGatewayObj.Select(strQForMrktPrice);
                if (dtFromMarketPrice != null && dtFromMarketPrice.Rows.Count > 0)
                {

                    txtMarketPriceDate = dtFromMarketPrice.Rows[0]["mp_dt"].ToString();


                }
                else
                {
                    txtMarketPriceDate = "";
                }
                if (dtimeBalanceDate <= dtimeLastUpadateDate)
                {

                    strtxtLastUpadateDate = "01-JUL-2002";
                    //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Process will be started from July 2002.');", true);
                }


                if (!string.IsNullOrEmpty(txtMarketPriceDate))
                {
                    dtMarketPriceDate = Convert.ToDateTime(txtMarketPriceDate);
                    strMarketPriceDate = dtMarketPriceDate.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    dtMarketPriceDate = null;
                    strMarketPriceDate = "";
                }




                strQForSellShares = "select  count(*)NoSaleRecord,sum(no_share)NoSaleShares from fund_trans_hb where f_cd =" + dtFundNameDropDownList.Rows[i]["F_CD"].ToString() +
          " and tran_tp = 'S' and vch_dt between '" + strLastUpadatePlusOneDate + "' and '" + strBalanceDate + "'";

                dtFromFundTrHBForSale = commonGatewayObj.Select(strQForSellShares);
                if (dtFromFundTrHBForSale != null && dtFromFundTrHBForSale.Rows.Count > 0)
                {
                    strtxtNoSaleRecord = dtFromFundTrHBForSale.Rows[0]["NoSaleRecord"].ToString();
                    strtxtNoOfSaleShare = dtFromFundTrHBForSale.Rows[0]["NoSaleShares"].ToString();

                }
                else
                {
                    strtxtNoSaleRecord = "0";
                    strtxtNoOfSaleShare = "0";

                }


                strQForPurchaseShares = "select  count(*)NoPurchaseRecord,sum(no_share)NoPurchaseShares from fund_trans_hb where f_cd =" + dtFundNameDropDownList.Rows[i]["F_CD"].ToString() +
           " and tran_tp = 'C' and vch_dt between '" + strLastUpadatePlusOneDate + "' and '" + strBalanceDate + "'";

                dtFromFundTrHBForBuy = commonGatewayObj.Select(strQForPurchaseShares);
                if (dtFromFundTrHBForBuy.Rows.Count > 0)
                {
                    strtxtNoPurchaseRecord = dtFromFundTrHBForBuy.Rows[0]["NoPurchaseRecord"].ToString();
                    strtxtNoPurchaseShares = dtFromFundTrHBForBuy.Rows[0]["NoPurchaseShares"].ToString();

                }
                else
                {
                    strtxtNoPurchaseRecord = "0";
                    strtxtNoPurchaseShares = "0";

                }


                tblAllfundInfo.Rows.Add(Convert.ToInt32(dtFundNameDropDownList.Rows[i]["F_CD"].ToString()), dtFundNameDropDownList.Rows[i]["F_NAME"].ToString(), strBalanceDate, strLastUpadateDate, strLastBalDate, strMarketPriceDate, strtxtNoSaleRecord, strtxtNoOfSaleShare, strtxtNoPurchaseRecord, strtxtNoPurchaseShares);



            }

            //}

        }
        return tblAllfundInfo;
    }


    public void Save(DataTable dt)
    {



        string strBalanceDate, strLastBalDate, strLastUpadateDate, strLastUpadatePlusOneDate, strUpdateFundTransHB, strDelFromFundFolioHB, strMarketPriceDate, temp;

        DateTime? dtimeBalanceDate, dtimeLastBalDate, dtimeLastUpadateDate, dtimeLastUpadatePlusOneDate, dtMarketPriceDate;
        DataTable dtFromFundTransHB = new DataTable();
        DataTable dtFromFundFolioHB = new DataTable();
        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable dtSource = new DataTable();
        DataTable dtSource2 = new DataTable();

        DataTable tblAllfundInfo = dt;


        List<string> fundBalanceDate = (from row in tblAllfundInfo.AsEnumerable()
                                               select row["LastUpadateDate"].ToString()).ToList();
        string lastupdateddate = "01-JUL-2002";
        if (fundBalanceDate.Contains(lastupdateddate.ToString()))
        {
             ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Process will be started from July 2002.');", true);
        }

      
        for (int i = 0; i < tblAllfundInfo.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(tblAllfundInfo.Rows[i]["BalanceDate"].ToString()))
            {
                dtimeBalanceDate = Convert.ToDateTime(tblAllfundInfo.Rows[i]["BalanceDate"].ToString());

                strBalanceDate = dtimeBalanceDate.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                dtimeBalanceDate = null;
                strBalanceDate = "";
            }

            if (!string.IsNullOrEmpty(tblAllfundInfo.Rows[i]["LastBalDate"].ToString()))
            {
                dtimeLastBalDate = Convert.ToDateTime(tblAllfundInfo.Rows[i]["LastBalDate"].ToString());
                strLastBalDate = dtimeLastBalDate.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                dtimeLastBalDate = null;
                strLastBalDate = "";
            }
            if (!string.IsNullOrEmpty(tblAllfundInfo.Rows[i]["LastUpadateDate"].ToString()))
            {
                dtimeLastUpadateDate = Convert.ToDateTime(tblAllfundInfo.Rows[i]["LastUpadateDate"].ToString());
                strLastUpadateDate = dtimeLastUpadateDate.Value.ToString("dd-MMM-yyyy");
                dtimeLastUpadatePlusOneDate = dtimeLastUpadateDate.Value.AddDays(1);
                strLastUpadatePlusOneDate = dtimeLastUpadatePlusOneDate.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                dtimeLastUpadateDate = null;
                strLastUpadateDate = "";
                dtimeLastUpadatePlusOneDate = null;
                strLastUpadatePlusOneDate = "";
            }

            if (!string.IsNullOrEmpty(tblAllfundInfo.Rows[i]["MarketPriceDate"].ToString()))
            {
                dtMarketPriceDate = Convert.ToDateTime(tblAllfundInfo.Rows[i]["MarketPriceDate"].ToString());
                strMarketPriceDate = dtMarketPriceDate.Value.ToString("dd-MMM-yyyy");
            }
            else
            {
                dtMarketPriceDate = null;
                strMarketPriceDate = "";
            }

            if (dtimeBalanceDate > dtimeLastBalDate)
            {

                temp = adv_proc1(strLastUpadatePlusOneDate, strBalanceDate, tblAllfundInfo.Rows[i]["F_CD"].ToString());
                
            }
            else
            {
                commonGatewayObj.BeginTransaction();
                strDelFromFundFolioHB = "delete from fund_folio_hb where f_cd =" + tblAllfundInfo.Rows[i]["F_CD"].ToString();
                int noDelRowsFromFundFolioHB = commonGatewayObj.ExecuteNonQuery(strDelFromFundFolioHB);

                strUpdateFundTransHB = "update fund_trans_hb set cost_rate = null,crt_aft_com = null where f_cd =" + tblAllfundInfo.Rows[i]["F_CD"].ToString();
                int noUpdRowsFundTransHB = commonGatewayObj.ExecuteNonQuery(strUpdateFundTransHB);
                commonGatewayObj.CommitTransaction();

                temp = adv_proc1(strLastUpadatePlusOneDate, strBalanceDate, tblAllfundInfo.Rows[i]["F_CD"].ToString());
               
            }


            string strCompnayTransdate = "select a.comp_cd, max(a.tran_date) tran_date from market_price a, fund_folio_hb b where a.comp_cd = b.comp_cd and b.f_cd =" + tblAllfundInfo.Rows[i]["F_CD"].ToString() + " and a.tran_date <= '" + strBalanceDate + "' group by a.comp_cd order by a.comp_cd";

            dtSource = commonGatewayObj.Select(strCompnayTransdate);

            List<CompanayTransdate> lstCompnayTransdate = new List<CompanayTransdate>();
            List<CompanyAvarageRate> lstCompnayAvgrate = new List<CompanyAvarageRate>();
            lstCompnayTransdate = (from DataRow dr in dtSource.Rows
                                   select new CompanayTransdate()
                                   {
                                       COMP_CD = dr["COMP_CD"].ToString(),
                                       TRAN_DATE = dr["TRAN_DATE"].ToString()
                                   }).ToList();

            string dltQuery = "delete from mprice_temp where f_cd=" + tblAllfundInfo.Rows[i]["F_CD"].ToString();
            int dltNumOfRows = commonGatewayObj.ExecuteNonQuery(dltQuery);

            foreach (CompanayTransdate comtransdate in lstCompnayTransdate)
            {


                string strInsQuery = "select " + tblAllfundInfo.Rows[i]["F_CD"].ToString() + " as FundId, comp_cd, avg_rt from  market_price where comp_cd = " + comtransdate.COMP_CD + " and tran_date = '" + Convert.ToDateTime(comtransdate.TRAN_DATE).ToString("dd-MMM-yyyy") + "'";

                dtSource2 = commonGatewayObj.Select(strInsQuery);

                lstCompnayAvgrate = (from DataRow dr in dtSource2.Rows
                                     select new CompanyAvarageRate()
                                     {
                                         FUNDID = dr["FUNDID"].ToString(),
                                         COMP_CD = dr["COMP_CD"].ToString(),
                                         AVG_RT = dr["AVG_RT"].ToString()
                                     }).ToList();


                foreach (CompanyAvarageRate comAvgrate in lstCompnayAvgrate)
                {

                    string strQueryInsMprice_Temp = "insert into mprice_temp(f_cd, comp_cd, avg_rt) values ('" + comAvgrate.FUNDID + "','" + comAvgrate.COMP_CD + "','" + comAvgrate.AVG_RT + "')";
                    int NumOfRows = commonGatewayObj.ExecuteNonQuery(strQueryInsMprice_Temp);
                }

            }
            string LoginID = Session["UserID"].ToString();
            DateTime dtimeCurrentDateTimeForLog = DateTime.Now;
            string strCurrentDateTimeForLog = dtimeCurrentDateTimeForLog.ToString("dd-MMM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);

            string strupdateQueryfund_control = "update fund_control set op_name='" + LoginID + "',upd_date_time='" + strCurrentDateTimeForLog + "',bal_dt='" + strBalanceDate + "',mprice_dt='" + strMarketPriceDate + "' where f_cd =" + tblAllfundInfo.Rows[i]["F_CD"].ToString() + "";
            int updatefund_controlNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryfund_control);


            if (temp.Trim() == "Processing Completed" || temp.Trim() == "")
            {
                lblProcessing.Text = "Processing completed!!!!";
                // ClearFields();
            }
            else
            {
                lblProcessing.Text = "No data found!!!!";
                //  ClearFields();
            }


        }

       
    
    }



    public DataTable FundNameDropDownList()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL ORDER BY F_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
       
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }



    private string adv_proc1(string vchDtFrom, string vchDtTo, string f_cd)
    {

        string strQuery, strSelFromFundFolioHBQuery, strUpdFundfolioHBForTrTypeS, strUpdFundfolioHBForTrTypeNotS, strInsIntoFundFolioHBForTrTypeNotS, strInsIntoFundFolioHBForTrTypeS, strUpdateFundTransHB, LoginID = Session["UserID"].ToString(), strRetVal;
       
        DataTable dtFromFundTransHB = new DataTable();
        DataTable dtFromFundFolioHB = new DataTable();

        strQuery = "select TO_CHAR(vch_dt,'DD-MON-YYYY')vch_dt, f_cd, comp_cd, no_share, rate, nvl(amount,0)amount,amt_aft_com, tran_tp, stock_ex from fund_trans_hb" +
        " where vch_dt between '" + vchDtFrom + "' and '" + vchDtTo + "' and f_cd=" + f_cd +
        " order by  vch_dt,comp_cd";

        dtFromFundTransHB = commonGatewayObj.Select(strQuery);

        if (dtFromFundTransHB != null && dtFromFundTransHB.Rows.Count > 0)
        {

            //lblProcessingRelatedMessage.Visible = true;
            //lblProcessingRelatedMessage.Text = "process is running!!!!";
            for (int i = 0; i < dtFromFundTransHB.Rows.Count; i++)
            {
                Double cmp = 0, mt_shr = 0, mt_cost = 0, mt_cst_aft_com = 0, mcost_rt = 0, mcost_rt_acm = 0, m_amt, m_amt_acm, m_no = 0, m_cost = 0, m_cost_acm = 0;

                if (!string.IsNullOrEmpty(dtFromFundTransHB.Rows[i]["amount"].ToString()))
                {
                    m_amt = Convert.ToDouble(dtFromFundTransHB.Rows[i]["amount"].ToString());

                }

                else
                {
                    m_amt = 0;
                }
                if (!string.IsNullOrEmpty(dtFromFundTransHB.Rows[i]["amt_aft_com"].ToString()))
                {
                    m_amt_acm = Convert.ToDouble(dtFromFundTransHB.Rows[i]["amt_aft_com"].ToString());

                }

                else
                {

                    m_amt_acm = 0;
                }

                strSelFromFundFolioHBQuery = "select comp_cd, tot_nos, tot_cost, tcst_aft_com from fund_folio_hb" +
                " where f_cd = " + dtFromFundTransHB.Rows[i]["f_cd"].ToString() + " and comp_cd = " + dtFromFundTransHB.Rows[i]["comp_cd"].ToString();

                dtFromFundFolioHB = commonGatewayObj.Select(strSelFromFundFolioHBQuery);

                if (dtFromFundFolioHB != null && dtFromFundFolioHB.Rows.Count > 0)
                {
                    for (int j = 0; j < dtFromFundFolioHB.Rows.Count; j++)
                    {

                        if (!string.IsNullOrEmpty(dtFromFundFolioHB.Rows[j]["comp_cd"].ToString().Trim()))
                        {
                            cmp = Convert.ToDouble(dtFromFundFolioHB.Rows[j]["comp_cd"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dtFromFundFolioHB.Rows[j]["tot_nos"].ToString().Trim()))
                        {
                            mt_shr = Convert.ToDouble(dtFromFundFolioHB.Rows[j]["tot_nos"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dtFromFundFolioHB.Rows[j]["tot_cost"].ToString().Trim()))
                        {
                            mt_cost = Convert.ToDouble(dtFromFundFolioHB.Rows[j]["tot_cost"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dtFromFundFolioHB.Rows[j]["tcst_aft_com"].ToString().Trim()))
                        {
                            mt_cst_aft_com = Convert.ToDouble(dtFromFundFolioHB.Rows[j]["tcst_aft_com"].ToString());
                        }



                        if (mt_shr == 0)
                        {
                            mcost_rt = 0;
                            mcost_rt_acm = 0;

                        }

                        else

                        {
                            mcost_rt = Math.Round(mt_cost / mt_shr, 4);
                            mcost_rt_acm = Math.Round(mt_cst_aft_com / mt_shr, 4);
                        }

                        if (dtFromFundTransHB.Rows[i]["tran_tp"].ToString() == "S")
                        {
                            m_amt = Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) * mcost_rt;
                            m_amt_acm = Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) * mcost_rt_acm;
                            m_no = mt_shr - Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString());
                            m_cost = mt_cost - m_amt;
                            m_cost_acm = mt_cst_aft_com - m_amt_acm;

                            //m_amt:= j.no_share * mcost_rt;
                            //m_amt_acm:= j.no_share * mcost_rt_acm;
                            //m_no:= mt_shr - j.no_share;
                            //m_cost:= mt_cost - m_amt;
                            //m_cost_acm:= mt_cst_aft_com - m_amt_acm;

                            strUpdFundfolioHBForTrTypeS = "update fund_folio_hb set o_no_shr = nvl(o_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) + "," +
                                                   "o_rate = (nvl(o_rate, 0) * nvl(o_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["amount"].ToString()) + ") / (nvl(o_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) + ")," +
                                                   "ort_aft_com = (nvl(ort_aft_com, 0) * nvl(o_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["amt_aft_com"].ToString()) + ")/(nvl(o_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) + ")," +
                                                   "tot_nos =" + m_no +
                                                   ",tot_cost =" + m_cost +
                                                   ",tcst_aft_com =" + m_cost_acm +
                                                   ", bal_dt = '" + dtFromFundTransHB.Rows[i]["vch_dt"].ToString() + "'" +
                                                   ", op_name = '" + LoginID + "'" +
                                                   " where f_cd = " + dtFromFundTransHB.Rows[i]["f_cd"].ToString() + " and comp_cd = " + dtFromFundTransHB.Rows[i]["comp_cd"].ToString();

                            int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUpdFundfolioHBForTrTypeS);


                        }

                        else
                        {


                            m_no = mt_shr + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString());
                            m_cost = mt_cost + m_amt;
                            m_cost_acm = mt_cst_aft_com + m_amt_acm;

                            strUpdFundfolioHBForTrTypeNotS = "update fund_folio_hb set i_no_shr = nvl(i_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) + "," +
                                                    "i_rate = (nvl(i_rate, 0) * nvl(i_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["amount"].ToString()) + ") / (nvl(i_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) + ")," +
                                                    "irt_aft_com = (nvl(irt_aft_com, 0) * nvl(i_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["amt_aft_com"].ToString()) + ")/(nvl(i_no_shr, 0) +" + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) + ")," +
                                                    "tot_nos =" + m_no +
                                                    ",tot_cost =" + m_cost +
                                                    ",tcst_aft_com =" + m_cost_acm +
                                                    ", bal_dt = '" + dtFromFundTransHB.Rows[i]["vch_dt"].ToString() + "'" +
                                                    ", op_name = '" + LoginID + "'" +
                                                    " where f_cd = " + dtFromFundTransHB.Rows[i]["f_cd"].ToString() + " and comp_cd = " + dtFromFundTransHB.Rows[i]["comp_cd"].ToString();

                            int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUpdFundfolioHBForTrTypeNotS);

                        }


                    }

                }


                else

                {

                    /*  Here we had  if (dtFromFundTransHB.Rows[i]["tran_tp"].ToString() == "S")
                     *  before changing to if (dtFromFundTransHB.Rows[i]["tran_tp"].ToString() != "S")
                     *  on 04 jul 2017
                     *    */

                    if (dtFromFundTransHB.Rows[i]["tran_tp"].ToString() != "S")
                    {
                        m_no = mt_shr + Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString());
                        m_cost = mt_cost + m_amt;
                        m_cost_acm = mt_cst_aft_com + m_amt_acm;

                        strInsIntoFundFolioHBForTrTypeNotS = "insert into fund_folio_hb(f_cd, comp_cd, i_no_shr, i_rate,irt_aft_com, bal_dt, tot_nos, tot_cost, op_name, tcst_aft_com)" +
                        " values(" +
                        dtFromFundTransHB.Rows[i]["f_cd"].ToString() + "," +
                        dtFromFundTransHB.Rows[i]["comp_cd"].ToString() + "," +
                        dtFromFundTransHB.Rows[i]["no_share"].ToString() + "," +
                        "nvl(" + dtFromFundTransHB.Rows[i]["amount"].ToString() + ", 0)/nvl(" + dtFromFundTransHB.Rows[i]["no_share"].ToString() + ",1)," +
                        dtFromFundTransHB.Rows[i]["amt_aft_com"].ToString() + "/nvl(" + dtFromFundTransHB.Rows[i]["no_share"].ToString() + ",1),'" +
                        dtFromFundTransHB.Rows[i]["vch_dt"].ToString() + "'," +
                        m_no + "," +
                        m_cost + ",'" +
                        LoginID + "'," +
                        m_cost_acm + ")";

                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsIntoFundFolioHBForTrTypeNotS);
                        //'" + LoginID + "'" 
                    }
                    else
                    {
                        m_amt = Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) * mcost_rt;
                        m_amt_acm = Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString()) * mcost_rt_acm;
                        m_no = mt_shr - Convert.ToDouble(dtFromFundTransHB.Rows[i]["no_share"].ToString());
                        m_cost = mt_cost - m_amt;
                        m_cost_acm = mt_cst_aft_com - m_amt_acm;

                        strInsIntoFundFolioHBForTrTypeS = "insert into fund_folio_hb(f_cd, comp_cd, o_no_shr, o_rate,ort_aft_com, bal_dt, tot_nos, tot_cost, op_name, tcst_aft_com)" +
                       " values(" +
                       dtFromFundTransHB.Rows[i]["f_cd"].ToString() + "," +
                       dtFromFundTransHB.Rows[i]["comp_cd"].ToString() + "," +
                       dtFromFundTransHB.Rows[i]["no_share"].ToString() + "," +
                       "nvl(" + dtFromFundTransHB.Rows[i]["amount"].ToString() + ", 0)/nvl(" + dtFromFundTransHB.Rows[i]["no_share"].ToString() + ",1)," +
                       dtFromFundTransHB.Rows[i]["amt_aft_com"].ToString() + "/nvl(" + dtFromFundTransHB.Rows[i]["no_share"].ToString() + ",1),'" +
                       dtFromFundTransHB.Rows[i]["vch_dt"].ToString() + "'," +
                       m_no + "," +
                       m_cost + ",'" +
                       LoginID + "'," +
                       m_cost_acm + ")";

                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsIntoFundFolioHBForTrTypeS);

                    }

                }

                if (dtFromFundTransHB.Rows[i]["tran_tp"].ToString() == "S")

                {
                    strUpdateFundTransHB = "update fund_trans_hb set " +
                        " cost_rate =" + mcost_rt + "," +
                        " crt_aft_com=" + mcost_rt_acm +
                        " where f_cd =" + dtFromFundTransHB.Rows[i]["f_cd"].ToString() + " and comp_cd=" + dtFromFundTransHB.Rows[i]["comp_cd"].ToString() +
                        " and vch_dt ='" + dtFromFundTransHB.Rows[i]["vch_dt"].ToString() + "' and tran_tp ='S' and stock_ex ='" + dtFromFundTransHB.Rows[i]["stock_ex"].ToString() + "'";

                    int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUpdateFundTransHB);


                }


            }
            strRetVal = "Processing Completed";
            return strRetVal;

        }
        else
        {
            // strRetVal = "No data found !";
            strRetVal = "";
            return strRetVal;
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No data found !');", true);
            //lblProcessing.Text = "No data found !";

        }


    }
    public class CompanayTransdate
    {
        public string COMP_CD { get; set; }
        public string TRAN_DATE { get; set; }
    }
    public class CompanyAvarageRate
    {
        public string FUNDID { get; set; }
        public string COMP_CD { get; set; }
        public string AVG_RT { get; set; }
    }


}
