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
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        DropDownList dropDownListObj = new DropDownList();
      
        DataTable dtPosted = commonGatewayObj.Select("select distinct(posted) posted from psdr_fi order by posted");
        if (!IsPostBack)
        {
            postedDropDownList.DataSource = dtPosted;
            postedDropDownList.DataTextField = "posted";
            postedDropDownList.DataValueField = "posted";
            postedDropDownList.DataBind();
        }


        //  companyNameTextBox.Text = "sss";
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        DataTable dtsource = new DataTable();
        DateTime date1 = DateTime.ParseExact(RIssuefromTextBox.Text, "dd/MM/yyyy", null);
        string fundcode = fundcodeTextBox.Text;
        string companycode = companyCodeTextBox.Text;
        string psdrNo = psdrNoTextBox.Text;
        string marketlot = oddormarketlotDropDownList.SelectedValue;
        string securType = securitiestypeDropDownList.SelectedValue;
        string posted = postedDropDownList.SelectedValue;
        string sp_date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
        string folioNo = folioNoTextBox.Text.ToString();
        string alotmentNo = AllotmentNoTextBox.Text.ToString();
        string certificateNo = certificateNoTextBox.Text;
        int ditinctto = Convert.ToInt32(DictincttivetoTextBox.Text);
        int DictincFrom = Convert.ToInt32(DictincttivefromTextBox.Text);
        int noofShares = Convert.ToInt32(noofshareTextBox.Text);
        string loginId = Session["UserID"].ToString();

        DateTime dtimeCurrentDateTimeForLog = DateTime.Now;
        string strCurrentDateTimeForLog = dtimeCurrentDateTimeForLog.ToString("dd-MMM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);

        if (securType == "P" && alotmentNo == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Enter Alotment Number !');", true);
        }
        else 
        {
            if ((certificateNo != "" && DictincFrom != 0))
            {
                ditinctto = (noofShares + DictincFrom) - 1;
            }
           string  Query1 = "select COMP_CD,PSDR_NO,F_CD,ALLOT_NO,NO_SHARES,SH_TYPE,OM_LOT,SP_RATE,SP_DATE,HOWLA_NO,MV_DATE,REF_NO,DIS_NO_FM,DIS_NO_TO,FOLIO_NO,CERT_NO,BK_CD,POSTED,OP_NAME,C_DT,C_DATE from psdr_fi where COMP_CD= " + companyCodeTextBox.Text.ToString() + " and F_CD=" + fundcodeTextBox.Text.ToString() + " and CERT_NO='" + certificateNoTextBox.Text.ToString() + "'  ";

            dtsource = commonGatewayObj.Select(Query1.ToString());
            if (dtsource.Rows.Count > 0)
            {
                string strUPdQuery = "";
                if (alotmentNo != "")
                {

                    strUPdQuery = "update psdr_fi set PSDR_NO ='" + Convert.ToInt32(psdrNo) + "',  SH_TYPE ='" + securType + "',OM_LOT ='" + marketlot + "', SP_DATE='" + sp_date + "'," +
                                               " DIS_NO_FM ='" + DictincFrom + "',DIS_NO_TO ='" + ditinctto + "',  ALLOT_NO='" + alotmentNo + "' ,FOLIO_NO='" + folioNo + "', POSTED='" + posted + "',upd_date_time='" + strCurrentDateTimeForLog + "' where COMP_CD= " + companyCodeTextBox.Text.ToString() + " and F_CD=" + fundcodeTextBox.Text.ToString() + " and CERT_NO='" + certificateNoTextBox.Text.ToString() + "'";
                }
                else
                {
                    strUPdQuery = "update psdr_fi set PSDR_NO ='" + Convert.ToInt32(psdrNo) + "',  SH_TYPE ='" + securType + "',OM_LOT ='" + marketlot + "', SP_DATE='" + sp_date + "'," +
                                             " DIS_NO_FM ='" + DictincFrom + "',DIS_NO_TO ='" + ditinctto + "',  FOLIO_NO='" + folioNo + "', POSTED='"+posted+ "',upd_date_time='" + strCurrentDateTimeForLog + "' where COMP_CD= " + companyCodeTextBox.Text.ToString() + " and F_CD=" + fundcodeTextBox.Text.ToString() + " and CERT_NO='" + certificateNoTextBox.Text.ToString() + "'";
                }


                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strUPdQuery);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Update sucessfully !');", true);
                clearField();

            }
            else
            {
                string strInsQuery = "insert into PSDR_FI(F_CD,comp_cd,PSDR_NO,NO_SHARES,SH_TYPE,OM_LOT,SP_DATE,DIS_NO_FM,DIS_NO_TO, FOLIO_NO,CERT_NO,POSTED,OP_NAME,upd_date_time) values('" + Convert.ToUInt32(fundcode) + "','" + Convert.ToInt32(companycode) + "','" + Convert.ToInt32(psdrNo) + "','" + noofShares + "','" + securType + "','" + marketlot + "','" + sp_date + "','" + DictincFrom + "','" + ditinctto + "','" + folioNo + "','" + certificateNo + "','"+ posted + "','" + loginId + "','"+ strCurrentDateTimeForLog + "')";

                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Insert sucessfully !');", true);

                clearField();

            }



        }
    }



private void clearField()
{
    fundcodeTextBox.Text = "";
    companyCodeTextBox.Text = "";
    psdrNoTextBox.Text = "";
    oddormarketlotDropDownList.SelectedValue = "0";
    securitiestypeDropDownList.SelectedValue = "0";
    folioNoTextBox.Text = "";
    AllotmentNoTextBox.Text = "";
    certificateNoTextBox.Text = "";
    DictincttivetoTextBox.Text = "";
    DictincttivefromTextBox.Text = "";
    noofshareTextBox.Text = "";
    RIssuefromTextBox.Text = "";
    lblLabelMarketLotLabel.Text = "";
    companyNameLabe.Visible = false;
    fundLabel.Visible = false;
}

protected void allotmentTextBox_TextChanged(object sender, EventArgs e)
{

    DataTable dtsource = new DataTable();
    DataTable dtsource1 = new DataTable();
    List<PSDR_FI> psdrfilist = new List<PSDR_FI>();
    List<PSDR> psdrlist = new List<PSDR>();


}

protected void DictincttivefromTextBox__TextChanged(object sender, EventArgs e)
{
    if ((certificateNoTextBox.Text).ToString() != "" && (DictincttivefromTextBox.Text).ToString() != "")
    {
        int value = (Convert.ToInt32(noofshareTextBox.Text) + Convert.ToInt32(DictincttivefromTextBox.Text)) - 1;

        DictincttivetoTextBox.Text = value.ToString();
    }
}

protected void certificateNoTextBox_TextChanged(object sender, EventArgs e)
{
    DataTable dtsource = new DataTable();
    DataTable dtsource1 = new DataTable();
    List<PSDR_FI> psdrfilist = new List<PSDR_FI>();
    List<PSDR> psdrlist = new List<PSDR>();
    string Query1 = "";
        
    Query1 = "select COMP_CD,PSDR_NO,F_CD,ALLOT_NO,NO_SHARES,SH_TYPE,OM_LOT,SP_RATE,SP_DATE,HOWLA_NO,MV_DATE,REF_NO,DIS_NO_FM,DIS_NO_TO,FOLIO_NO,CERT_NO,BK_CD,POSTED,OP_NAME,C_DT,C_DATE from psdr_fi where COMP_CD= " + companyCodeTextBox.Text.ToString() + " and F_CD=" + fundcodeTextBox.Text.ToString() + " and CERT_NO='" + certificateNoTextBox.Text.ToString() + "' ";
    
    dtsource = commonGatewayObj.Select(Query1.ToString());
    if (dtsource.Rows.Count > 0)
    {
        psdrfilist = (from DataRow dr in dtsource.Rows
                      select new PSDR_FI()
                      {
                          COMP_CD = dr["COMP_CD"].ToString(),
                          PSDR_NO = dr["PSDR_NO"].ToString(),
                          F_CD = dr["F_CD"].ToString(),
                          ALLOT_NO = dr["ALLOT_NO"].ToString(),
                          NO_SHARES = dr["NO_SHARES"].ToString(),
                          SH_TYPE = dr["SH_TYPE"].ToString(),
                          OM_LOT = dr["OM_LOT"].ToString(),
                          SP_RATE = dr["SP_RATE"].ToString(),
                          HOWLA_NO = dr["HOWLA_NO"].ToString(),
                          MV_DATE = dr["MV_DATE"].ToString(),
                          REF_NO = dr["REF_NO"].ToString(),
                          DIS_NO_FM = dr["DIS_NO_FM"].ToString(),
                          DIS_NO_TO = dr["DIS_NO_TO"].ToString(),
                          FOLIO_NO = dr["FOLIO_NO"].ToString(),
                          CERT_NO = dr["CERT_NO"].ToString(),
                          BK_CD = dr["BK_CD"].ToString(),
                          POSTED = dr["POSTED"].ToString(),
                          OP_NAME = dr["OP_NAME"].ToString(),
                          C_DT = dr["C_DT"].ToString(),
                          C_DATE = dr["C_DATE"].ToString(),
                          SP_DATE = dr["SP_DATE"].ToString()
                      }).ToList();
        foreach (PSDR_FI phdrfi in psdrfilist)
        {
            // companyCodeTextBox.Text = ;
            AllotmentNoTextBox.Text = phdrfi.ALLOT_NO;
            psdrNoTextBox.Text = phdrfi.PSDR_NO;
            oddormarketlotDropDownList.SelectedValue = phdrfi.OM_LOT;
            securitiestypeDropDownList.SelectedValue = phdrfi.SH_TYPE;
            folioNoTextBox.Text = phdrfi.FOLIO_NO;
            AllotmentNoTextBox.Text = phdrfi.ALLOT_NO;
            certificateNoTextBox.Text = phdrfi.CERT_NO;
            DictincttivetoTextBox.Text = phdrfi.DIS_NO_TO;
            DictincttivefromTextBox.Text = phdrfi.DIS_NO_FM;
            postedDropDownList.Text = phdrfi.POSTED;
            noofshareTextBox.Text = phdrfi.NO_SHARES;
            RIssuefromTextBox.Text = Convert.ToDateTime(phdrfi.SP_DATE).ToString("dd/MM/yyyy");
        }


    }



}

protected void FundCodeTextBox_TextChanged(object sender, EventArgs e)
{
    // Fund name must be shown on label

    string FundNameByFundId = "";
    FundNameByFundId = "select f_cd,F_NAME from fund where f_cd = " + fundcodeTextBox.Text.ToString() + " ";
    DataTable dtFundName = commonGatewayObj.Select(FundNameByFundId.ToString());

    if (dtFundName.Rows.Count > 0)
    {
        fundLabel.Text = dtFundName.Rows[0]["F_NAME"].ToString();
    }
    else
    {
        fundLabel.Text = "Fund not found ..!!! ";
    }
}

protected void compCodeTextBox_TextChanged(object sender, EventArgs e)
{

    //        if :psdr.comp_cd = 150 then
    //   message('Invalid Company Code');
    //        raise form_trigger_failure;
    //else
    //  get_comp_cd(:psdr.comp_cd,:psdr.comp_name);
    //        select mlot into: psdr.mlot from invest.comp
    //                 where comp_cd =:psdr.comp_cd;
    //        end if;
    // Company name must be shown on label

    string companyCode = companyCodeTextBox.Text.ToString();

    if (companyCode == "150")
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Invalid Company Code", true);
        companyNameLabe.Text = "Invalid Company Code";
    }
    else
    {
        string companyCodeByCompId = "";
        if (companyCodeTextBox.Text.ToString() != "")
        {
            companyCodeByCompId = "select COMP_CD,COMP_NM,MLOT from COMP where COMP_CD = " + companyCodeTextBox.Text.ToString() + " ";
            DataTable dtCompanyName = commonGatewayObj.Select(companyCodeByCompId.ToString());
            if (dtCompanyName.Rows.Count > 0)
            {
                companyNameLabe.Text = dtCompanyName.Rows[0]["COMP_NM"].ToString();
                lblLabelMarketLotLabel.Text = dtCompanyName.Rows[0]["MLOT"].ToString();
            }
            else
            {
                companyNameLabe.Text = "Company name not found ";
            }



        }
    }


}



public class PSDR_FI
{
    public string COMP_CD { get; set; }
    public string PSDR_NO { get; set; }
    public string F_CD { get; set; }
    public string ALLOT_NO { get; set; }
    public string NO_SHARES { get; set; }
    public string SH_TYPE { get; set; }
    public string OM_LOT { get; set; }
    public string SP_RATE { get; set; }
    public string HOWLA_NO { get; set; }
    public string MV_DATE { get; set; }
    public string REF_NO { get; set; }
    public string DIS_NO_FM { get; set; }
    public string DIS_NO_TO { get; set; }
    public string FOLIO_NO { get; set; }
    public string CERT_NO { get; set; }
    public string BK_CD { get; set; }
    public string POSTED { get; set; }
    public string OP_NAME { get; set; }
    public string C_DT { get; set; }
    public string C_DATE { get; set; }
    public string SP_DATE { get; set; }
}

public class PSDR
{
    public string allot_no { get; set; }
    public string cert_no { get; set; }
}


}