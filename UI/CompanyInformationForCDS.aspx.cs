using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        lblmessage.Text = "";
      //  companyNameTextBox.Text = "sss";
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {

        DataTable dtsource = new DataTable();
        string strCDSStartDate, strLastTradingdate;
        DateTime CDSStartDate, LastTradingdate;

        //strCDSStartDate = CDSStartDateTextBox.Text.ToString();
        //strLastTradingdate = lasttradingdateTextBox.Text.ToString();


        CDSStartDate = DateTime.ParseExact(CDSStartDateTextBox.Text.ToString(), "dd/MM/yyyy", null);
        LastTradingdate = DateTime.ParseExact(lasttradingdateTextBox.Text.ToString(), "dd/MM/yyyy", null);

        strCDSStartDate = Convert.ToDateTime(CDSStartDate).ToString("dd-MMM-yyyy");
        strLastTradingdate = Convert.ToDateTime(LastTradingdate).ToString("dd-MMM-yyyy");



        List<CompanyInfo> companyInfolist = new List<CompanyInfo>();
        string Query = "select COMP_CD,comp_nm,mlot,fc_val,avg_rt,rt_upd_dt,instr_cd,cseinstr_cd, cse_sid, trade_meth, cds from comp where comp_cd ='" + companyCodeTextBox.Text + "'";

        dtsource = commonGatewayObj.Select(Query.ToString());

       

        if (dtsource.Rows.Count > 0)
        {
            updatedata();
            // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update Successfully');", true);
        }

        if (dtsource.Rows.Count > 0)
        {

            companyInfolist = (from DataRow dr in dtsource.Rows
                               select new CompanyInfo()
                               {
                                   // COMP_CD = dr["COMP_CD"].ToString(),
                                   COMP_CD = dr["COMP_CD"].ToString(),
                                   cds = dr["cds"].ToString(),

                               }).ToList();


            foreach (CompanyInfo compInfo in companyInfolist)
            {
                if (compInfo.cds == "Y")
                {
                    DataTable dtsourcecdscomcd = new DataTable();
                    List<CompanyInfoCDS> companyInfocds = new List<CompanyInfoCDS>();
                    string Query2 = "  select COMP_CD from comp_cds where comp_cd =" + compInfo.COMP_CD + "";

                    dtsourcecdscomcd = commonGatewayObj.Select(Query2.ToString());

                    if (dtsourcecdscomcd.Rows.Count > 0)
                    {

                        DataTable companyinfocds = new DataTable();


                        string strUPQuery2 = "update comp_cds  set  start_dt ='" + strCDSStartDate + "',isin_cd ='" + isinCode.Text.ToString() + "' where comp_cd =" + compInfo.COMP_CD + "";

                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUPQuery2);
                        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update Successfully');", true);
                        lblmessage.Text = "No Data found";
                        ClearFields();

                    }
                    else
                    {
                        insertComp_CDS(compInfo.COMP_CD, strCDSStartDate);
                        ClearFields();

                    }

                }
                else
                {
                    lblmessage.Text = "No Data found";
                  //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert(No Data found'');", true);
                }


            }
        }


    }



    protected void companyCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        DataTable dtsource = new DataTable();
        List<CompanyInfo> companyInfolist = new List<CompanyInfo>();
        string Query = "select comp_nm,mlot,fc_val,avg_rt,rt_upd_dt,instr_cd,cseinstr_cd, cse_sid, trade_meth, cds from comp where comp_cd ='" + companyCodeTextBox.Text + "'";

        dtsource = commonGatewayObj.Select(Query.ToString());





        DataTable dtsourcecds = new DataTable();
        List<CompanyInfoCDS> companyInfocds = new List<CompanyInfoCDS>();
        string Query2 = "  select start_dt,isin_cd from comp_cds where comp_cd ='" + companyCodeTextBox.Text + "'";

        dtsourcecds = commonGatewayObj.Select(Query2.ToString());



        if (dtsource.Rows.Count > 0)
        {

            companyInfolist = (from DataRow dr in dtsource.Rows
                               select new CompanyInfo()
                               {
                                   // COMP_CD = dr["COMP_CD"].ToString(),
                                   COMP_NM = dr["COMP_NM"].ToString(),

                                   MLOT = dr["MLOT"].ToString(),

                                   AVG_RT = dr["AVG_RT"].ToString(),

                                   FC_VAL = dr["FC_VAL"].ToString(),

                                   RT_UPD_DT = dr["RT_UPD_DT"].ToString(),

                                   INSTR_CD = dr["INSTR_CD"].ToString(),

                                   CSE_INSTR_CD = dr["cseinstr_cd"].ToString(),

                                   TRADE_METH = dr["TRADE_METH"].ToString(),

                                   cse_sid = dr["cse_sid"].ToString(),

                                   cds = dr["cds"].ToString(),

                               }).ToList();


            foreach (CompanyInfo compInfo in companyInfolist)
            {
                // companyCodeTextBox.Text = ;
                GROUPDropDownList.SelectedValue = compInfo.TRADE_METH;

                MarketLotTextBox.Text = compInfo.MLOT;

                avarageMarketRateTextBox.Text = compInfo.AVG_RT;

                dsecodeTextBox.Text = compInfo.INSTR_CD;

                faceValueTextBox.Text = compInfo.FC_VAL;

                lasttradingdateTextBox.Text = Convert.ToDateTime(compInfo.RT_UPD_DT).ToString("dd/MM/yyyy");

                dsecodeTextBox.Text = compInfo.INSTR_CD;

                csecodeTextBox.Text = compInfo.CSE_INSTR_CD;

                cseScriptIdTextBox.Text = compInfo.cse_sid;

                IscdsTextBox.Text = compInfo.cds;

            }
        }
        if (dtsourcecds.Rows.Count > 0)
        {
         
            CDSStartDateTextBox.Text = Convert.ToDateTime(dtsourcecds.Rows[0]["start_dt"]).ToString("dd/MM/yyyy");
            isinCode.Text = dtsourcecds.Rows[0]["isin_cd"].ToString();

        }
        


    }


    private void ClearFields()
    {
        companyCodeTextBox.Text = "";

        dsecodeTextBox.Text = "";

        faceValueTextBox.Text = "";

        MarketLotTextBox.Text = "";

        avarageMarketRateTextBox.Text = "";

        lasttradingdateTextBox.Text = "";

        dsecodeTextBox.Text = "";

        csecodeTextBox.Text = "";

        cseScriptIdTextBox.Text = "";

        IscdsTextBox.Text = "";

        CDSStartDateTextBox.Text = "";

        IscdsTextBox.Text = "";
        isinCode.Text = "";
      



    }
    

    private void updatedata()
    {

        DataTable companyinfoupdate = new DataTable();


        string strUPQuery = "update comp set    instr_cd ='" + dsecodeTextBox.Text.ToString() + "',cseinstr_cd ='" + csecodeTextBox.Text.ToString() + "',cse_sid='" + cseScriptIdTextBox.Text.ToString() + "',trade_meth ='" + GROUPDropDownList.SelectedValue.ToString() + "',cds='" + IscdsTextBox.Text.ToString() + "' where comp_cd =" + companyCodeTextBox.Text.ToString() + "";

        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUPQuery);

        lblmessage.Text = "Update Successfully";
       // ClearFields();

    }
    private void insertComp_CDS( string COMP_CD,string strCDSStartDate)
    {
        string strInsQuery;
      
        DataTable dtsource = new DataTable();
        List<CompanyInfo> companyInfolist = new List<CompanyInfo>();
        string Query = "select comp_nm,mlot,fc_val,avg_rt,rt_upd_dt,instr_cd,cseinstr_cd, cse_sid, trade_meth, cds from comp where comp_cd ='" + COMP_CD + "'";

        dtsource = commonGatewayObj.Select(Query.ToString());

        if (dtsource.Rows.Count > 0)
        {

            companyInfolist = (from DataRow dr in dtsource.Rows
                               select new CompanyInfo()
                               {
                                   // COMP_CD = dr["COMP_CD"].ToString(),
                                   COMP_NM = dr["COMP_NM"].ToString(),

                               }).ToList();


            //DateTime CDSStartDate = DateTime.ParseExact(CDSStartDateTextBox.Text, "dd/MM/yyyy", null);
           
            foreach (CompanyInfo compInfo in companyInfolist)
            {
                // companyCodeTextBox.Text = ;

                strInsQuery = "insert into comp_cds(comp_cd,comp_nm,start_dt,isin_cd) values(" + COMP_CD + ", '" + compInfo.COMP_NM + "', '" + strCDSStartDate + "','" + isinCode.Text.ToString() + "')";
                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                lblmessage.Text = " Insert Data Successfully";
              //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Insert Data Successfully');", true);
            }
        }




    }


    public class CompanyInfo
    {
        public string COMP_CD { get; set; }
        public string COMP_NM { get; set; }
        public string MLOT { get; set; }
        public string AVG_RT { get; set; }
        public string FC_VAL { get; set; }
        public string RT_UPD_DT { get; set; }
        public string INSTR_CD { get; set; }
        public string CSE_INSTR_CD { get; set; }
        public string TRADE_METH { get; set; }
        public string cse_sid { get; set; }
        public string cds { get; set; }

    }
    public class CompanyInfoCDS
    {
        public string COMP_CD { get; set; }
        public string start_dt { get; set; }
        public string isin_cd { get; set; }

    }

}