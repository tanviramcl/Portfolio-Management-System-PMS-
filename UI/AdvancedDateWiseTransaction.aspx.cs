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



public partial class DateWiseTransaction : System.Web.UI.Page
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


            ChecKVoucherDate();
        }


     }

    public void ChecKVoucherDate()
    {
        lblProcessing.Text = " ";
        dvGridDSETradeInfo.Visible = false;
        string strtxtBalanceDate, lastHowla;

        DateTime dtimeCurrentDate = DateTime.Now;

        string currentDate = Convert.ToDateTime(dtimeCurrentDate).ToString("dd-MMM-yyyy");

        if (!string.IsNullOrEmpty(txtBalanceDate.Text))
        {
            strtxtBalanceDate = txtBalanceDate.Text;
        }
        else
        {
            txtBalanceDate.Text = currentDate;
            strtxtBalanceDate = txtBalanceDate.Text; ;
        }


        DataTable tblAllfundInforDSE = getTblAllFundFromByDate(strtxtBalanceDate);


   
        if (tblAllfundInforDSE != null && tblAllfundInforDSE.Rows.Count > 0)
        {

       

            List<TblFundInfo> tblAllfundInfolistDSE = new List<TblFundInfo>();


            tblAllfundInfolistDSE = (from DataRow dr in tblAllfundInforDSE.Rows
                                     select new TblFundInfo()
                                     {
                                         // COMP_CD = dr["COMP_CD"].ToString(),
                                         F_CD = dr["F_CD"].ToString(),
                                         F_NAME = dr["F_NAME"].ToString(),
                                         Howla_Date_From = dr["Howla_Date_From"].ToString(),
                                         Howla_LastUpdated_Date = dr["Howla_LastUpdated_Date"].ToString(),
                                         Howla_Date_To = dr["Howla_Date_To"].ToString(),
                                         // Stock_Exchange = dr["Stock_Exchange"].ToString(),

                                     }).ToList();
            if (tblAllfundInfolistDSE.Count > 0)
            {

                dvGridDSETradeInfo.Visible = true;
                var dtFundinfolist = tblAllfundInfolistDSE.OrderByDescending(fund => fund.Stock_Exchange).ToList();
                grdShowDSE.DataSource = dtFundinfolist;
                grdShowDSE.DataBind();
            }
            
        }
        else
        {
            //string strQuery = "select TO_CHAR(max(SP_DATE),'DD-MON-YYYY')last_tr_dt  from howla ";

            //DataTable dt = commonGatewayObj.Select(strQuery);
            //if (dt.Rows.Count > 0)
            //{
            //    string last_howla_date = dt.Rows[0]["last_tr_dt"].ToString();
            //    DateTime lastHowlA = Convert.ToDateTime(dt.Rows[0]["last_tr_dt"].ToString());
            //    lastHowla = lastHowlA.ToString("dd-MMM-yyyy");
            //    lblProcessing.Text = "Voucher date should be:" + lastHowla + " !!!";
            //}

            lblProcessing.Text = "No data Found..!!!";
        }


    }

    protected void balanceDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChecKVoucherDate();

    }

    public DataTable GeTBondComp()//For All Funds
    {


        DataTable dtBondComp = commonGatewayObj.Select("SELECT  distinct(comp_cd) as comp_cd FROM COMP Where SECT_MAJ_CD = 89  ");

        return dtBondComp;
    }

    public DataTable getTblAllFundFromByDate(string voucherdate)
    {
        DataTable dt = new DataTable();

        string strQuery;
        string strtxtHowlaDateFrom, strTXTvoucherdate, strtxtLastHowlaDate, strtHowlaDateFrom, strHowlaDateTo, strLastHowlaDate, strHowlaDateto;
        DateTime? dtimeHowlaDateFrom, dtimeLastHowlaDate, dtHowlaDateto,dtvoucherDate;
        DataTable dtFundNameDropDownList = GeTFundNameFromHowla(voucherdate);

        DataTable tblAllfundInfo = new DataTable();
        tblAllfundInfo.Columns.Add("F_CD", typeof(int));
        tblAllfundInfo.Columns.Add("F_NAME", typeof(string));
        tblAllfundInfo.Columns.Add("Howla_Date_From", typeof(string));
        tblAllfundInfo.Columns.Add("Howla_LastUpdated_Date", typeof(string));
        tblAllfundInfo.Columns.Add("Howla_Date_To", typeof(string));

        if (dtFundNameDropDownList != null && dtFundNameDropDownList.Rows.Count > 0)
        {
            for (int i = 0; i < dtFundNameDropDownList.Rows.Count; i++)
            {
                strQuery = "select TO_CHAR(max(vch_dt),'DD-MON-YYYY')last_tr_dt,TO_CHAR(max(vch_dt) + 1,'DD-MON-YYYY')vch_dt  from fund_trans_hb where f_cd =" + dtFundNameDropDownList.Rows[i]["F_CD"].ToString() +
                            " and tran_tp in ('C','S') and stock_ex in ('D','C','A')";
                dt = commonGatewayObj.Select(strQuery);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!dt.Rows[0].IsNull("vch_dt"))
                    {

                        strtxtHowlaDateFrom = dt.Rows[0]["vch_dt"].ToString();
                        strtxtLastHowlaDate = dt.Rows[0]["last_tr_dt"].ToString();

                        if (!string.IsNullOrEmpty(strtxtHowlaDateFrom))
                        {
                            dtimeHowlaDateFrom = Convert.ToDateTime(strtxtHowlaDateFrom);

                            strtHowlaDateFrom = dtimeHowlaDateFrom.Value.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            dtimeHowlaDateFrom = null;
                            strtHowlaDateFrom = "";
                        }


                        if (!string.IsNullOrEmpty(voucherdate))
                        {
                            dtvoucherDate = Convert.ToDateTime(voucherdate);

                            strTXTvoucherdate = dtvoucherDate.Value.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            dtvoucherDate = null;
                            strTXTvoucherdate = "";
                        }




                        if (!string.IsNullOrEmpty(strtxtLastHowlaDate))
                        {
                            dtimeLastHowlaDate = Convert.ToDateTime(strtxtLastHowlaDate);

                            strLastHowlaDate = dtimeLastHowlaDate.Value.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            dtimeLastHowlaDate = null;
                            strLastHowlaDate = "";
                        }


                        dtHowlaDateto = DateTime.Now;

                        strHowlaDateto = Convert.ToDateTime(dtHowlaDateto).ToString("dd-MMM-yyyy");

                        if (!string.IsNullOrEmpty(strHowlaDateto))
                        {

                            strHowlaDateTo = strHowlaDateto;
                        }

                        if (dtvoucherDate < dtimeHowlaDateFrom || dtvoucherDate > dtHowlaDateto)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Last Voucher Date:" + strLastHowlaDate + "');", true);

                            txtBalanceDate.Text = strHowlaDateto;

                            tblAllfundInfo.Rows.Add(Convert.ToInt32(dtFundNameDropDownList.Rows[i]["F_CD"].ToString()), dtFundNameDropDownList.Rows[i]["F_NAME"].ToString(), strtHowlaDateFrom, strtxtLastHowlaDate, strHowlaDateto);
                        }
                        else
                        {
                            tblAllfundInfo.Rows.Add(Convert.ToInt32(dtFundNameDropDownList.Rows[i]["F_CD"].ToString()), dtFundNameDropDownList.Rows[i]["F_NAME"].ToString(), strTXTvoucherdate, strtxtLastHowlaDate, strHowlaDateto);
                        }


                    }
                }



            }
        }


      
        return tblAllfundInfo;
    }

    public DataTable GeTFundNameFromHowla(string voucherdate)//For All Funds
    {

        string strTXTvoucherdate;

        DateTime ? dtvoucherDate;



        if (!string.IsNullOrEmpty(voucherdate))
        {
            dtvoucherDate = Convert.ToDateTime(voucherdate);

            strTXTvoucherdate = dtvoucherDate.Value.ToString("dd-MMM-yyyy");
        }
        else
        {
            dtvoucherDate = null;
            strTXTvoucherdate = "";
        }

        DataTable LastHowlaDate = commonGatewayObj.Select(" select max(sp_date) as lasthowlaDate from howla");

       DateTime dtimeLastHowlaDate = Convert.ToDateTime(LastHowlaDate.Rows[0]["lasthowlaDate"].ToString());
       string  strLastHowlaDate = dtimeLastHowlaDate.ToString("dd-MMM-yyyy");
        DataTable dtFundName = new DataTable();
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        if (dtvoucherDate == dtimeLastHowlaDate)
        {
            dtFundName = commonGatewayObj.Select("select tab1.F_CD,f.F_NAME from (SELECT distinct(F_CD) FROM HOWLA where sp_date = '" + strTXTvoucherdate + "') tab1 inner join Fund f on tab1.F_CD = f.F_cd order by F_CD asc");
            for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
            {
                dr = dtFundNameDropDownList.NewRow();
                dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
                dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
                dtFundNameDropDownList.Rows.Add(dr);
            }
        }
        else
        {
          
            lblProcessing.Text = "Last Howla Date:" + strLastHowlaDate + "'";
        }
       
        return dtFundNameDropDownList;
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {

        string confirmValue = HiddenField1.Value;
        if (confirmValue == "Yes")
        {
            string strlblProcessing = "", strProcessing = "", strHowlaDate, strLastHowlaDate, strHowlaDateTo, strSelFromFundTransHBQuery;
            DateTime dtimeHowlaDate, dtimeLastHowlaDate, dtimeHowlaDateTo;



            string strtxtBalanceDate;

            DateTime dtimeCurrentDate = DateTime.Now;

            string currentDate = Convert.ToDateTime(dtimeCurrentDate).ToString("dd-MMM-yyyy");

            if (!string.IsNullOrEmpty(txtBalanceDate.Text))
            {
                strtxtBalanceDate = txtBalanceDate.Text;
            }
            else
            {
                txtBalanceDate.Text = currentDate;
                strtxtBalanceDate = txtBalanceDate.Text; ;
            }


            DataTable tblAllfundInfo = getTblAllFundFromByDate(strtxtBalanceDate);


            // DataTable tblAllfundInfo = getTblAllFundFromHowlaInfo();
            DataTable dtExcepChargableBondFromHowla = new DataTable();

            string LoginID = Session["UserID"].ToString();




            string comp_cd_bond = "";
            DataTable BondCompCd = GeTBondComp();
            DataTable dtFromFundTrans = new DataTable();

            for (int comp_cd = 0; comp_cd < BondCompCd.Rows.Count; comp_cd++)
            {
                comp_cd_bond = comp_cd_bond + BondCompCd.Rows[comp_cd]["comp_cd"].ToString() + ",";
            }
            string CompanyCodeForBond = comp_cd_bond.Remove(comp_cd_bond.Length - 1);



            if (tblAllfundInfo != null && tblAllfundInfo.Rows.Count > 0)
            {
                //  Save(dtDseabdCseFundINfo);
                int countRows, countRowsbond = 0;

                try
                {
                    for (int l = 0; l < tblAllfundInfo.Rows.Count; l++)
                    {
                        dtimeHowlaDate = Convert.ToDateTime(tblAllfundInfo.Rows[l]["Howla_Date_From"].ToString());
                        strHowlaDate = dtimeHowlaDate.ToString("dd-MMM-yyyy");

                        dtimeLastHowlaDate = Convert.ToDateTime(tblAllfundInfo.Rows[l]["Howla_LastUpdated_Date"].ToString());
                        strLastHowlaDate = dtimeLastHowlaDate.ToString("dd-MMM-yyyy");
                        dtimeHowlaDateTo = Convert.ToDateTime(tblAllfundInfo.Rows[l]["Howla_Date_To"].ToString());
                        strHowlaDateTo = dtimeHowlaDateTo.ToString("dd-MMM-yyyy");

                        if ((dtimeHowlaDate < dtimeLastHowlaDate) || (dtimeHowlaDateTo <= dtimeLastHowlaDate))
                        {
                            strProcessing = "This Trading Date is already allocated !!";

                        }
                        else
                        {
                            commonGatewayObj.BeginTransaction();
                            StringBuilder sbQuery = new StringBuilder();
                            StringBuilder sbQueryBond = new StringBuilder();


                            strSelFromFundTransHBQuery = "select * from fund_trans_hb where vch_dt ='" + strHowlaDate + "' and f_cd =" + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString()) +
                                      " and tran_tp  in ('C','S')  and stock_ex in ('D','A')";
                            dtFromFundTrans = commonGatewayObj.Select(strSelFromFundTransHBQuery);
                            if (dtFromFundTrans.Rows.Count > 0)
                            {
                                strProcessing = "Data is already allocated !!";
                            }
                            else
                            {
                                sbQuery.Append(" INSERT INTO FUND_TRANS_HB (VCH_DT, F_CD, COMP_CD,TRAN_TP, NO_SHARE,RATE,AMOUNT,AMT_AFT_COM,STOCK_EX) ");//insert  information

                                sbQuery.Append("select * from ( SELECT SP_DATE AS VCH_DT, F_CD, COMP_CD, 'C' AS TRAN_TP, SUM(SP_QTY) AS NO_SHARE, ROUND(SUM(SP_QTY * SP_RATE) ");
                                sbQuery.Append(" / SUM(SP_QTY), 2) AS RATE, SUM(SP_QTY * SP_RATE) AS AMOUNT, ROUND(SUM((SP_QTY * SP_RATE) * (1 + SL_BUY_COM_PCT / 100)), 2) AS AMT_AFT_COM,STOCK_EX ");
                                sbQuery.Append(" FROM (SELECT HOWLA.F_CD, HOWLA.SP_DATE, HOWLA.IN_OUT, HOWLA.SETTLE_DT, HOWLA.COMP_CD, HOWLA.SP_QTY, HOWLA.SP_RATE,  FUND.SL_BUY_COM_PCT,HOWLA.STOCK_EX ");
                                sbQuery.Append(" FROM HOWLA INNER JOIN   FUND ON HOWLA.F_CD = FUND.F_CD  WHERE (HOWLA.SP_DATE = '" + strHowlaDate + "' ) AND (HOWLA.IN_OUT = 'I') AND (HOWLA.STOCK_EX  IN( 'D','C'))) A WHERE (1 = 1) ");
                                if (tblAllfundInfo.Rows[l]["F_CD"].ToString() != "0")
                                {
                                    sbQuery.Append("AND (F_CD = " + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString()) + ")");
                                }
                                sbQuery.Append(" GROUP BY IN_OUT, F_CD, COMP_CD, SP_DATE,STOCK_EX ");


                                sbQuery.Append("  UNION ALL ");

                                sbQuery.Append(" SELECT SP_DATE AS VCH_DT, F_CD, COMP_CD, 'S' AS TRAN_TP, SUM(SP_QTY) AS NO_SHARE, ROUND(SUM(SP_QTY * SP_RATE) ");
                                sbQuery.Append(" / SUM(SP_QTY), 2) AS RATE, SUM(SP_QTY * SP_RATE) AS AMOUNT, ROUND(SUM((SP_QTY * SP_RATE) * (1 - SL_BUY_COM_PCT / 100)), 2) AS AMT_AFT_COM,STOCK_EX ");
                                sbQuery.Append(" FROM (SELECT HOWLA.F_CD, HOWLA.SP_DATE, HOWLA.IN_OUT, HOWLA.SETTLE_DT, HOWLA.COMP_CD, HOWLA.SP_QTY, HOWLA.SP_RATE,  FUND.SL_BUY_COM_PCT,HOWLA.STOCK_EX ");
                                sbQuery.Append(" FROM HOWLA INNER JOIN   FUND ON HOWLA.F_CD = FUND.F_CD  WHERE (HOWLA.SP_DATE = '" + strHowlaDate + "' ) AND (HOWLA.IN_OUT = 'O') AND (HOWLA.STOCK_EX  IN( 'D','C'))) A WHERE (1 = 1) ");
                                if (tblAllfundInfo.Rows[l]["F_CD"].ToString() != "0")
                                {
                                    sbQuery.Append("AND (F_CD = " + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString()) + ")");
                                }
                                sbQuery.Append(" GROUP BY IN_OUT, F_CD, COMP_CD, SP_DATE,STOCK_EX ) a  ");

                                if (!string.IsNullOrEmpty(CompanyCodeForBond))
                                {
                                    sbQuery.Append("  where comp_cd not in(" + CompanyCodeForBond + ")  ");
                                }


                                countRows = commonGatewayObj.ExecuteNonQuery(sbQuery.ToString());

                                // commonGatewayObj.CommitTransaction();



                                //...............bond.................................................................//


                                //commonGatewayObj.BeginTransaction();
                                string strBOndComp = "select BOND_HOWLA_CHARGE_DSE,BOND_HAWLA_CHARGE_CSE,BOND_SL_BY_TRAN_AMT  from FUND where F_CD=" + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString());
                                DataTable dtSelExtCharge = commonGatewayObj.Select(strBOndComp);
                                Double AddBuySlChargeAmtDSE = Convert.ToDouble(dtSelExtCharge.Rows[0]["BOND_HOWLA_CHARGE_DSE"].ToString());
                                Double AddBuySlChargeAmtCSE = Convert.ToDouble(dtSelExtCharge.Rows[0]["BOND_HAWLA_CHARGE_CSE"].ToString());
                                Double EXCEP_BUYSL_COM = Convert.ToDouble(dtSelExtCharge.Rows[0]["BOND_SL_BY_TRAN_AMT"].ToString());


                                sbQueryBond.Append(" INSERT INTO FUND_TRANS_HB (VCH_DT, F_CD, COMP_CD,TRAN_TP, NO_SHARE,RATE,AMOUNT,AMT_AFT_COM,STOCK_EX) ");//insert  information

                                sbQueryBond.Append("select * from (select * from (SELECT A.SP_DATE AS VCH_DT, A.F_CD, COMP_CD, 'C' AS TRAN_TP, SUM(A.SP_QTY) AS NO_SHARE, ROUND(SUM(A.SP_QTY * SP_RATE)/ SUM(A.SP_QTY), 2) AS RATE,");
                                sbQueryBond.Append(" SUM(SP_QTY * SP_RATE) AS AMOUNT, SUM(SP_QTY * SP_RATE) +(COUNT(*)*" + AddBuySlChargeAmtDSE + ")+" + EXCEP_BUYSL_COM + " AS AMT_AFT_COM,A.STOCK_EX FROM (SELECT HOWLA.F_CD, HOWLA.SP_DATE, HOWLA.IN_OUT, HOWLA.SETTLE_DT, ");
                                sbQueryBond.Append(" HOWLA.COMP_CD, HOWLA.SP_QTY, HOWLA.SP_RATE,  HOWLA.STOCK_EX FROM HOWLA WHERE  (HOWLA.SP_DATE = '" + strHowlaDate + "') AND (HOWLA.IN_OUT = 'I') AND ( HOWLA.F_CD = " + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString()) + ") AND HOWLA.COMP_CD IN (" + CompanyCodeForBond + ")  ) A ");
                                sbQueryBond.Append("   GROUP BY IN_OUT, F_CD, COMP_CD, SP_DATE,STOCK_EX ");

                                sbQueryBond.Append("   union all  ");

                                sbQueryBond.Append("   SELECT A.SP_DATE AS VCH_DT, A.F_CD, COMP_CD, 'S' AS TRAN_TP, SUM(A.SP_QTY) AS NO_SHARE, ROUND(SUM(A.SP_QTY * SP_RATE)/ SUM(A.SP_QTY), 2) AS RATE, SUM(SP_QTY * SP_RATE) AS AMOUNT,  ");
                                sbQueryBond.Append("   SUM(SP_QTY * SP_RATE) -(COUNT(*)*" + AddBuySlChargeAmtDSE + ")-" + EXCEP_BUYSL_COM + " AS AMT_AFT_COM,A.STOCK_EX FROM (SELECT HOWLA.F_CD, HOWLA.SP_DATE, HOWLA.IN_OUT, HOWLA.SETTLE_DT, HOWLA.COMP_CD, HOWLA.SP_QTY, ");
                                sbQueryBond.Append("   HOWLA.SP_RATE,  HOWLA.STOCK_EX FROM HOWLA WHERE  (HOWLA.SP_DATE = '" + strHowlaDate + "') AND (HOWLA.IN_OUT = 'O') AND ( HOWLA.F_CD = " + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString()) + ") AND HOWLA.COMP_CD IN (" + CompanyCodeForBond + ")  ) A ");
                                sbQueryBond.Append("   GROUP BY IN_OUT, F_CD, COMP_CD, SP_DATE,STOCK_EX  ) where stock_ex='D' ");


                                sbQueryBond.Append("   union all  ");

                                sbQueryBond.Append("   select * from ( select * from (SELECT A.SP_DATE AS VCH_DT, A.F_CD, COMP_CD, 'C' AS TRAN_TP, SUM(A.SP_QTY) AS NO_SHARE, ROUND(SUM(A.SP_QTY * SP_RATE)/ SUM(A.SP_QTY), 2) AS RATE, ");
                                sbQueryBond.Append("  SUM(SP_QTY * SP_RATE) AS AMOUNT, SUM(SP_QTY * SP_RATE) +(COUNT(*)*" + AddBuySlChargeAmtCSE + ")+" + EXCEP_BUYSL_COM + " AS AMT_AFT_COM,A.STOCK_EX FROM (SELECT HOWLA.F_CD, HOWLA.SP_DATE, HOWLA.IN_OUT, HOWLA.SETTLE_DT, ");
                                sbQueryBond.Append("    HOWLA.COMP_CD, HOWLA.SP_QTY, HOWLA.SP_RATE,  HOWLA.STOCK_EX FROM HOWLA WHERE  (HOWLA.SP_DATE = '" + strHowlaDate + "') AND (HOWLA.IN_OUT = 'I') AND ( HOWLA.F_CD = " + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString()) + ") AND HOWLA.COMP_CD IN (" + CompanyCodeForBond + ")  ) A ");
                                sbQueryBond.Append("     GROUP BY IN_OUT, F_CD, COMP_CD, SP_DATE,STOCK_EX ");




                                sbQueryBond.Append("   union all  ");

                                sbQueryBond.Append(" SELECT A.SP_DATE AS VCH_DT, A.F_CD, COMP_CD, 'S' AS TRAN_TP, SUM(A.SP_QTY) AS NO_SHARE, ROUND(SUM(A.SP_QTY * SP_RATE)/ SUM(A.SP_QTY), 2) AS RATE, SUM(SP_QTY * SP_RATE) AS AMOUNT,");
                                sbQueryBond.Append("  SUM(SP_QTY * SP_RATE) -(COUNT(*)*" + AddBuySlChargeAmtCSE + ")-" + EXCEP_BUYSL_COM + " AS AMT_AFT_COM,A.STOCK_EX FROM (SELECT HOWLA.F_CD, HOWLA.SP_DATE, HOWLA.IN_OUT, HOWLA.SETTLE_DT, HOWLA.COMP_CD, HOWLA.SP_QTY, HOWLA.SP_RATE, ");
                                sbQueryBond.Append(" HOWLA.STOCK_EX FROM HOWLA WHERE  (HOWLA.SP_DATE = '" + strHowlaDate + "') AND (HOWLA.IN_OUT = 'O') AND ( HOWLA.F_CD = " + Convert.ToInt32(tblAllfundInfo.Rows[l]["F_CD"].ToString()) + ") AND HOWLA.COMP_CD IN (" + CompanyCodeForBond + ")  ) A ");
                                sbQueryBond.Append("   GROUP BY IN_OUT, F_CD, COMP_CD, SP_DATE,STOCK_EX)) where stock_ex='C')");

                                countRowsbond = commonGatewayObj.ExecuteNonQuery(sbQueryBond.ToString());

                                if (countRowsbond > 0)
                                {
                                    countRows = countRows + countRowsbond;
                                }
                                else
                                {
                                    countRows=+countRows;
                                }

                                

                                commonGatewayObj.CommitTransaction();

                                strProcessing = "Processing completed!!!!";
                            }



                        }


                    }


                    if (!string.IsNullOrEmpty(strProcessing))
                    {
                        strlblProcessing = strProcessing;
                    }
                    else
                    {
                        strlblProcessing = "";
                    }
                    lblProcessing.Text = strlblProcessing;
                }
                catch (Exception ex)
                {


                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + ex.Message.ToString() + "');", true);
                }

            }
            else
            {

                strProcessing = "No data found!!!!";

                if (!string.IsNullOrEmpty(strProcessing))
                {
                    lblProcessing.Text = strProcessing;
                }
                else
                {
                    lblProcessing.Text = "";
                }

            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        }




    }

    public class TblFundInfo
    {
        public string F_CD { get; set; }
        public string F_NAME { get; set; }
        public string Howla_Date_From { get; set; }
        public string Howla_LastUpdated_Date { get; set; }
        public string Howla_Date_To { get; set; }
        public string Stock_Exchange { get; set; }
    }
}






