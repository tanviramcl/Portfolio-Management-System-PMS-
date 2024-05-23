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
//using AMCL.DL;
//using AMCL.BL;
//using AMCL.UTILITY;
//using AMCL.GATEWAY;
//using AMCL.COMMON;


public partial class BalanceUpdateProcess : System.Web.UI.Page
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
        DataTable dtBalanceDate = getbalanceDate();
        DataTable dtmaxpfolioDate = Getmaximumdatefromportfolio_bK();

        lblProcessing.Text = "";
        if (!IsPostBack)
        {
           txtbalanceDate1.Text = dtBalanceDate.Rows[0]["balancedate1"].ToString();
            txtbalanceDate2.Text = dtBalanceDate.Rows[0]["balancedate2"].ToString();

            string maxpfolioDate = Convert.ToDateTime(dtmaxpfolioDate.Rows[0]["date1"]).ToString("dd-MMM-yyyy");
            DataTable dttotalrow = GetTotalrowPortfolio_bk(maxpfolioDate);
            txttotalRowCount.Text = dttotalrow.Rows[0]["TOTALROW"].ToString();

            string loginId = Session["UserID"].ToString();
            if (loginId == "admin")
            {
                btnDelete.Visible = true;
            }

        }



    }
    public DataTable getbalanceDate()//For Authorized Signatory
    {
        DataTable pdate = commonGatewayObj.Select("select max(bal_dt_ctrl)+1 as BalanceDate  from pfolio_bk");
        DataTable pdateDropDownList = new DataTable();
        pdateDropDownList.Columns.Add("balancedate1", typeof(string));
        pdateDropDownList.Columns.Add("balancedate2", typeof(string));
        DataRow dr = pdateDropDownList.NewRow();
        dr = pdateDropDownList.NewRow();
        dr["balancedate1"] = Convert.ToDateTime(pdate.Rows[0]["BalanceDate"]).ToString("dd-MMM-yyyy");
        dr["balancedate2"] = Convert.ToDateTime(pdate.Rows[0]["BalanceDate"]).ToString("dd-MMM-yyyy");
        pdateDropDownList.Rows.Add(dr);

        return pdateDropDownList;
    }
    public DataTable Getmaximumdatefromportfolio_bK()
    {
        DataTable maxdate = commonGatewayObj.Select("select max(bal_dt_ctrl) as date1 from pfolio_bk");

        return maxdate;
    }
    public DataTable Get_maximum_RateUpadatedate_fromComp()
    {
        DataTable maxdate = commonGatewayObj.Select("select max(rt_upd_dt) as date2  from comp");

        return maxdate;
    }
    protected void btnProcessingforBackup_Click(object sender, EventArgs e)
    {
        string confirmValue = HiddenField1.Value;
        if (confirmValue == "Yes")
        {
            DataTable dtBalanceDate = getbalanceDate(); // Here date will be  max(bal_dt_ctrl)+1   from pfolio_bk
                                                        // string Bal_Date1 = Convert.ToDateTime(dtBalanceDate.Rows[0]["balancedate1"]).ToString("dd-MMM-yyyy"); // Here date will be  max(bal_dt_ctrl)+1   from pfolio_bk
            DataTable dtmaximumDatefrompflio = Getmaximumdatefromportfolio_bK(); // Here date will be  max(bal_dt_ctrl)   from pfolio_bk
            DataTable dtRateUpdatedDate = Get_maximum_RateUpadatedate_fromComp(); // Here date will be  max(rt_upd_dt)   from comp

            string Date1 = Convert.ToDateTime(dtmaximumDatefrompflio.Rows[0]["date1"]).ToString("dd-MMM-yyyy"); // Here date will be  max(bal_dt_ctrl)   from pfolio_bk
            string Date2 = Convert.ToDateTime(dtRateUpdatedDate.Rows[0]["date2"]).ToString("dd-MMM-yyyy");  // Here date will be  max(rt_upd_dt)   from comp
                                                                                                            // marketPriceDateTextBox.Text.ToString()

            if (Date1 == Date2)
            {
                //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Already Updated! or Market Price is not current');", true);
                lblProcessing.Text = "Data Already Updated! or Market Price is not current !!!";
            }
            else
            {

                //  DateTime dtbalanceDate1 = DateTime.ParseExact(txtbalanceDate1.Text, "dd/MM/yyyy", null);

                string strdtBalanceDate1 = txtbalanceDate1.Text.ToString();

                DataTable dtsource = new DataTable();
                StringBuilder sbMst = new StringBuilder();
                StringBuilder sbfilter = new StringBuilder();
                sbfilter.Append(" ");
                sbMst.Append("select a.F_CD, a.COMP_CD, a.TOT_NOS,nvl( a.TOT_COST,0) as TOT_COST, a.TCST_AFT_COM, a.BAL_DT, comp.avg_RT, nvl(comp.CSE_RT,0) as CSE_RT ,");
                sbMst.Append("comp.ADC_RT,comp.flag, sect_maj.SECT_MAJ_NM, sect_maj.SECT_MAJ_CD, '" + strdtBalanceDate1 + "' as BalanceDate from comp, mprice_temp, fund_folio_hb a, sect_maj");
                sbMst.Append(" where mprice_temp.f_cd = a.f_cd and comp.comp_cd = a.comp_cd and ");
                sbMst.Append("comp.sect_maj_cd = sect_maj.sect_maj_cd and comp.comp_cd = mprice_temp.comp_cd and a.tot_nos > 0 and comp.flag !='C'");
                sbMst.Append(sbfilter.ToString());
                dtsource = commonGatewayObj.Select(sbMst.ToString());

                List<PortFolioBk> portFoliobkdatalist = new List<PortFolioBk>();
                portFoliobkdatalist = (from DataRow dr in dtsource.Rows
                                       select new PortFolioBk()
                                       {
                                           F_CD = dr["F_CD"].ToString(),
                                           COMP_CD = dr["COMP_CD"].ToString(),
                                           TOT_NOS = dr["TOT_NOS"].ToString(),
                                           TOT_COST = dr["TOT_COST"].ToString(),
                                           TCST_AFT_COM = dr["TCST_AFT_COM"].ToString(),
                                           BAL_DT = Convert.ToDateTime(dr["BAL_DT"]).ToString("dd-MMM-yyyy"), //This date from fund_folio_hb
                                                                                                              // BAL_DT = dr["BAL_DT"].ToString(),
                                           avg_RT = dr["avg_RT"].ToString(),
                                           CSE_RT = dr["CSE_RT"].ToString(),
                                           ADC_RT = dr["ADC_RT"].ToString(),
                                           SECT_MAJ_NM = dr["SECT_MAJ_NM"].ToString(),
                                           SECT_MAJ_CD = dr["SECT_MAJ_CD"].ToString(),
                                           BalanceDate = dr["BalanceDate"].ToString() // Here date will be  given input date
                                       }).ToList();


                foreach (PortFolioBk pk in portFoliobkdatalist)
                {

                        string strInsQuery = "insert into  pfolio_bk(F_CD,COMP_CD,TOT_NOS,TOT_COST,TCST_AFT_COM,BAL_DT,DSE_RT,CSE_RT,ADC_RT,SECT_MAJ_NM,SECT_MAJ_CD,BAL_DT_CTRL)values(" + Convert.ToInt32(pk.F_CD) + "," + Convert.ToInt32(pk.COMP_CD) + "," + Convert.ToDouble(pk.TOT_NOS) + "," + Convert.ToDouble(pk.TOT_COST) + "," + Convert.ToDouble(pk.TCST_AFT_COM) + ",TO_Date('" + pk.BAL_DT + "')," + Convert.ToDouble(pk.avg_RT) + "," + Convert.ToDouble(pk.CSE_RT) + "," + Convert.ToDouble(pk.ADC_RT) + ",'" + pk.SECT_MAJ_NM.ToString() + "','" + pk.SECT_MAJ_CD + "',TO_DATE('" + pk.BalanceDate + "'))";
                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                   
                    
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Data Inserted Successfully');", true);
                DataTable dttotalrow = GetTotalrowPortfolio_bk(Convert.ToDateTime(txtbalanceDate1.Text).ToString("dd-MMM-yyyy"));
                txttotalRowCount.Text = dttotalrow.Rows[0]["TOTALROW"].ToString();
                lblProcessing.Text = "Processing completed!!!!";
            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        }




        //   Response.Redirect("FinalProcessing.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string confirmValue = HiddenField2.Value;
        if (confirmValue == "Yes")
        {
            DataTable dtDate1 = Getmaximumdatefromportfolio_bK();
            DateTime balDate1 = Convert.ToDateTime(dtDate1.Rows[0]["date1"].ToString());
            string Date1 = balDate1.ToString("dd-MMM-yyyy");


            string dt1 = txtbalanceDate2.Text.ToString();

            // lblProcessing.Text = "Processing completed!!!!";
            if (Date1 == dt1)
            {
                DataTable dttotalrow = GetTotalrowPortfolio_bk(dt1);
                int row = Convert.ToInt32(dttotalrow.Rows[0]["TOTALROW"].ToString());
                string strDelQuery = "delete from pfolio_bk where bal_dt_ctrl='" + dt1 + "'";
                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);



                txttotalRowCount.Text = row.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Delete  Successfull');", true);
                ClearFields();
                lblProcessing.Text = "Delete  Successful";


            }
            else
            {
                lblProcessing.Text = "Delete  unsuccessful";
            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data deleted!')", true);
        }




    }
   
    public class PortFolioBk
    {
        public string F_CD { get; set;}
        public string COMP_CD { get; set; }
        public string TOT_NOS { get; set; }
        public string TOT_COST { get; set; }
        public string TCST_AFT_COM { get; set; }
        public string BAL_DT { get; set; }
        public string avg_RT { get; set; }
        public string CSE_RT { get; set; }
        public string ADC_RT { get; set; }
        public string SECT_MAJ_NM { get; set; }
        public string SECT_MAJ_CD { get; set; }
        public string BalanceDate { get; set; }

    }

   

   
  

    public DataTable GetTotalrowPortfolio_bk(string date)
    {
        DataTable totalrow = commonGatewayObj.Select("select count(*) as TotalRow from pfolio_bk where baL_dt_ctrl='"+date+ "'");
        return totalrow;
    }
    private void ClearFields()
    {
        //DataTable dtBalanceDate = getbalanceDate();
        //txtbalanceDate1.Text = dtBalanceDate.Rows[0]["balancedate1"].ToString(); ;
        //txtbalanceDate2.Text = dtBalanceDate.Rows[0]["balancedate2"].ToString();
        txttotalRowCount.Text = "";


    }

}
