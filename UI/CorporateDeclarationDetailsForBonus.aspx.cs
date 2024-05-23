using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_CorporateDeclarationDetails : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    protected void Page_Load(object sender, EventArgs e)
    {

        string companycode = "";
        string type = "";
       string FY = "";

        companycode = (string)Session["comp_cd"];
        type = (string)Session["type"];
        FY = (string)Session["FY"];
        int BOOK_CL_ID;

        string Record_dt = "";
        string Agm_dt = "";
        DateTime dtimeCurrentDateTime = DateTime.Now;
        string strCurrentDateTimeForLog = dtimeCurrentDateTime.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {

            DataTable bookCloserCorporateDec = commonGatewayObj.Select("SELECT * FROM CORPORATE_DEC WHERE COMP_CD=" + companycode + " and type='" + type + "' and FY='" + FY + "' order by BOOK_CL_ID DESC  ");

            if (bookCloserCorporateDec.Rows.Count > 0)
            {

                Record_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
                Agm_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");

                if (dtimeCurrentDateTime > Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["RECORD_DT"]))
                {
                    postedButton.Visible = true;
                }
                TextBoxRecordDate.Text = Record_dt.ToString();

                BOOK_CL_ID = Convert.ToInt32(bookCloserCorporateDec.Rows[0]["BOOK_CL_ID"].ToString());
                Session["BOOK_CL_ID"] = BOOK_CL_ID;

                DataTable bookCloserDetails = new DataTable();


                string BOOK_CL_DETAILS = "Select distinct(f_cd) as f_cd from BOOK_CL_DETAILS  Where BOOK_CL_ID=" + BOOK_CL_ID + "";
                bookCloserDetails = commonGatewayObj.Select(BOOK_CL_DETAILS);
                string F_cd_withComma = "";
                string F_cd = "";
               
                DataTable bookCloserBonus = new DataTable();
                if (bookCloserDetails.Rows.Count > 0)
                {

                    for (int k = 0; k < bookCloserDetails.Rows.Count; k++)
                    {
                        F_cd_withComma = F_cd_withComma + "," + bookCloserDetails.Rows[k]["f_cd"].ToString().ToLower().Trim();
                    }
                    F_cd = F_cd_withComma.Remove(0, 1);
                    bookCloserBonus = commonGatewayObj.Select("select * from (select * from (SELECT C.COMP_NM,B.COMP_CD,  floor(P.TOT_NOS) tot_nos, p.f_cd, f_name f_name,b.BOOK_CL_ID,b.TYPE,floor(P.TOT_NOS * B.QUANTITY / 100) share_alloted,B.FY, B.RECORD_DT," +
             " B.QUANTITY, B.APPR_DT, B.AGM, B.REMARKS, B.POSTED, b.pdate  FROM CORPORATE_DEC B, COMP C, PFOLIO_BK P, fund f " +
          " WHERE B.COMP_CD = P.COMP_CD AND B.COMP_CD = C.COMP_CD AND P.BAL_DT_CTRL = B.RECORD_DT and B.QUANTITY is not null " +
         " and B.TYPE = '" + type + "' and f.IS_F_CLOSE is null and p.f_cd = f.f_cd order by p.f_cd) where comp_cd = " + companycode + " and FY = '" + FY + "' Order by f_cd) Where F_CD not in ("+ F_cd + ") ");
                    int bookCloserBonus_count = bookCloserBonus.Rows.Count;

                    if (bookCloserBonus.Rows.Count > 0)
                    {

                        //grdGridBonus.Visible = true;
                        leftDataGrid.DataSource = bookCloserBonus;
                        leftDataGrid.DataBind();
                        postedButton.Visible = true;
                        Session["bookCloserBonus"] = bookCloserBonus;

                    }
                    else
                    {
                        DataTable fund_trans_hb = new DataTable();
                        string count_fund_trans_hb = "Select count(f_cd) as f_cd from FUND_TRANS_HB  Where RECORD_DT='" + Record_dt + "' and comp_cd="+ companycode + "";
                        fund_trans_hb = commonGatewayObj.Select(count_fund_trans_hb);
                        int fund_trans_hbrow = Convert.ToInt32(fund_trans_hb.Rows[0]["f_cd"]);

                        if (bookCloserBonus_count == 0)
                        {
                            string strupdateQueryCorporateDec = "update CORPORATE_DEC set POSTED='Y',PDATE='" + strCurrentDateTimeForLog + "' where   COMP_CD=" + companycode + " and BOOK_CL_ID=" + BOOK_CL_ID + " ";
                            int updateCorporateDecNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryCorporateDec);
                        }
                        Response.Redirect("CorporateDeclarationSecurities.aspx");
                    }


                }
                else
                {

                    bookCloserBonus = commonGatewayObj.Select("select * from (select * from (SELECT C.COMP_NM,B.COMP_CD,  floor(P.TOT_NOS) tot_nos, p.f_cd, f_name f_name,b.BOOK_CL_ID,b.TYPE,floor(P.TOT_NOS * B.QUANTITY / 100) share_alloted,B.FY, B.RECORD_DT," +
                       " B.QUANTITY, B.APPR_DT, B.AGM, B.REMARKS, B.POSTED, b.pdate  FROM CORPORATE_DEC B, COMP C, PFOLIO_BK P, fund f " +
                        " WHERE B.COMP_CD = P.COMP_CD AND B.COMP_CD = C.COMP_CD AND P.BAL_DT_CTRL = B.RECORD_DT and B.QUANTITY is not null " +
                        " and B.TYPE = '" + type + "' and f.IS_F_CLOSE is null and p.f_cd = f.f_cd order by p.f_cd) where comp_cd = " + companycode + " and FY = '" + FY + "' Order by f_cd) ");

                    if (bookCloserBonus.Rows.Count > 0)
                    {

                        //grdGridBonus.Visible = true;
                        leftDataGrid.DataSource = bookCloserBonus;
                        leftDataGrid.DataBind();
                        postedButton.Visible = true;
                        Session["bookCloserBonus"] = bookCloserBonus;

                    }
                    else
                    {

                       // Response.Redirect("CorporateDeclarationSecurities.aspx");
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
                    }

                }

               
            }




           
        
        }
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)

        {

            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

        }

    }

    public void postedButton_Click(object sender, EventArgs e)
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        int countcheck = 0;
        int count = 0;
        DateTime dtimeCurrentDateTime = DateTime.Now;
        string strCurrentDateTimeForLog = dtimeCurrentDateTime.ToString("dd-MMM-yyyy");
        string LoginName = Session["UserID"].ToString();

        foreach (DataGridItem gridRow in leftDataGrid.Items)
        {
            CheckBox leftCheckBox = (CheckBox)gridRow.Cells[countcheck].FindControl("leftCheckBox");
            if (leftCheckBox.Checked)
            {
                string strQuery = "select max(BOOK_CL_DET_ID)+1 AS BOOK_CL_DET_ID From BOOK_CL_DETAILS ";

                DataTable dt = commonGatewayObj.Select(strQuery);
                int BOOK_CL_DET_ID = Convert.ToInt32(dt.Rows[0]["BOOK_CL_DET_ID"]);

                //double share_alloted = Convert.ToUInt32(gridRow.Cells[12].Text.ToString());
                //double bookcloser=Convert.ToUInt32(gridRow.Cells[13].Text.ToString());
                Hashtable httable = new Hashtable();

                httable.Add("BOOK_CL_DET_ID", Convert.ToInt16(BOOK_CL_DET_ID));
                httable.Add("F_CD", Convert.ToUInt32(gridRow.Cells[1].Text.ToString()));
                httable.Add("COMP_CD", Convert.ToUInt32(gridRow.Cells[3].Text.ToString()));
                httable.Add("TOT_NOS", Convert.ToUInt32(gridRow.Cells[5].Text.ToString()));
                httable.Add("ALLOTED_SHARE", Convert.ToUInt32(gridRow.Cells[12].Text.ToString()));
                httable.Add("BOOK_CL_ID", Convert.ToUInt32(gridRow.Cells[13].Text.ToString()));
                httable.Add("ENTRY_DATE", strCurrentDateTimeForLog);
                httable.Add("USERNAME", LoginName);
                commonGatewayObj.Insert(httable, "BOOK_CL_DETAILS");



                //string strQueryBookCloserBonus = "insert into BOOK_CL_DETAILS (BOOK_CL_DET_ID, F_CD, COMP_CD,TOT_NOS,share_alloted,BOOK_CL_ID,ENTRY_DATE,USERNAME) values (" + BOOK_CL_DET_ID + "," + Convert.ToUInt32(gridRow.Cells[1].Text.ToString()) + "," + Convert.ToUInt32(gridRow.Cells[3].Text.ToString()) + "," + Convert.ToUInt32(gridRow.Cells[4].Text.ToString()) + "," + share_alloted + "," + bookcloser + ",'" + strCurrentDateTimeForLog + "','" + LoginName + "')";


                //int NumOfRows = commonGatewayObj.ExecuteNonQuery(strQueryBookCloserBonus);



                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);
                count++;
            }
        }




        ///////////////////////................fund trans hb..................................


        string companycode = "";
        string type = "";
        int BOOK_CL_ID;
        string FY = "";

        companycode = (string)Session["comp_cd"];
        type = (string)Session["type"];
        BOOK_CL_ID = (int)Session["BOOK_CL_ID"];
        FY = (string)Session["FY"];



        string strQueryFundTranshb = "select a.*,b.RECORD_DT,b.AGM,b.APPR_DT from (select * From BOOK_CL_DETAILS where comp_cd=" + companycode + " and BOOK_CL_ID=" + BOOK_CL_ID + " ) a inner join CORPORATE_DEC b on A.BOOK_CL_ID=B.BOOK_CL_ID ";

        DataTable dtBookcloserDetails = commonGatewayObj.Select(strQueryFundTranshb);
        DataTable NumOfRowsFund_trans_hb = new DataTable();
        if (dtBookcloserDetails.Rows.Count > 0)
        {
            string Appoval_dt = Convert.ToDateTime(dtBookcloserDetails.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
            if (Appoval_dt != "")
            {
                for (int i = 0; i < dtBookcloserDetails.Rows.Count; i++)
                {

                    string fund_trans_hb= "Select * from fund_trans_hb where F_cd=" + dtBookcloserDetails.Rows[i]["f_cd"].ToString() + " and comp_cd="+ dtBookcloserDetails.Rows[i]["comp_cd"].ToString() + " and RECORD_DT='"+Convert.ToDateTime(dtBookcloserDetails.Rows[i]["RECORD_DT"]).ToString("dd - MMM - yyyy")+"'";
                     NumOfRowsFund_trans_hb = commonGatewayObj.Select(fund_trans_hb.ToString());
                    if (NumOfRowsFund_trans_hb.Rows.Count == 0)
                    {
                        string strInsQuery = "insert into fund_trans_hb(vch_dt, f_cd, comp_cd," +
                 " tran_tp, no_share, rate, amount, stock_ex, amt_aft_com,RECORD_DT,op_name) values('" + strCurrentDateTimeForLog + "'," + dtBookcloserDetails.Rows[i]["f_cd"].ToString() + "," + dtBookcloserDetails.Rows[i]["comp_cd"].ToString() + "," +
                 " 'B'," + dtBookcloserDetails.Rows[i]["ALLOTED_SHARE"].ToString() + ",'0','0','A','0','" + Convert.ToDateTime(dtBookcloserDetails.Rows[i]["RECORD_DT"]).ToString("dd-MMM-yyyy") + "','" + LoginName + "')";

                       int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);
                      // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Bonus Inserted Sucessfully');", true);
                    }
                  

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Bonus can not be inserted without Approval Date');", true);
            }



            DataTable bookCloserDetails = new DataTable();

            string BOOK_CL_DETAILS = "Select distinct(f_cd) as f_cd from BOOK_CL_DETAILS  Where BOOK_CL_ID=" + BOOK_CL_ID + "";
            bookCloserDetails = commonGatewayObj.Select(BOOK_CL_DETAILS);
            string F_cd_withComma = "";
            string F_cd = "";

            if (bookCloserDetails.Rows.Count > 0)
            {

                for (int k = 0; k < bookCloserDetails.Rows.Count; k++)
                {
                    F_cd_withComma = F_cd_withComma + "," + bookCloserDetails.Rows[k]["f_cd"].ToString().ToLower().Trim();
                }
                F_cd = F_cd_withComma.Remove(0, 1);
            }

            DataTable bookCloserBonus = new DataTable();

            bookCloserBonus = commonGatewayObj.Select("select * from (select * from (SELECT C.COMP_NM,B.COMP_CD,  floor(P.TOT_NOS) tot_nos, p.f_cd, f_name f_name,b.BOOK_CL_ID,b.TYPE,floor(P.TOT_NOS * B.QUANTITY / 100) share_alloted,B.FY, B.RECORD_DT," +
            " B.QUANTITY, B.APPR_DT, B.AGM, B.REMARKS, B.POSTED, b.pdate  FROM CORPORATE_DEC B, COMP C, PFOLIO_BK P, fund f " +
         " WHERE B.COMP_CD = P.COMP_CD AND B.COMP_CD = C.COMP_CD AND P.BAL_DT_CTRL = B.RECORD_DT and B.QUANTITY is not null " +
        " and B.TYPE = '" + type + "' and f.IS_F_CLOSE is null and p.f_cd = f.f_cd order by p.f_cd) where comp_cd = " + companycode + " and FY = '" + FY + "' Order by f_cd) Where F_CD not in (" + F_cd + ") ");
            int bookCloserBonus_count = bookCloserBonus.Rows.Count;

            if (bookCloserBonus.Rows.Count == 0)
            {
                string strupdateQueryCorporateDec = "update CORPORATE_DEC set POSTED='Y',PDATE='" + strCurrentDateTimeForLog + "' where   COMP_CD=" + companycode + " and BOOK_CL_ID=" + BOOK_CL_ID + " ";
                int updateCorporateDecNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryCorporateDec);
            }

            // This is insert

                //ClearFields();


        }

        Response.Redirect("CorporateDeclarationSecurities.aspx");

    }
}