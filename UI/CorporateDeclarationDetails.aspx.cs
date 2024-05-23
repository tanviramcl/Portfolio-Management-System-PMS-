using System;
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





        if (!IsPostBack)
        {
            DataTable bookCloserCorporateDec = commonGatewayObj.Select("SELECT * FROM CORPORATE_DEC WHERE COMP_CD=" + companycode + " and type='" + type + "' and FY='" + FY + "' order by BOOK_CL_ID DESC  ");
            if (bookCloserCorporateDec.Rows.Count > 0)
            {

                Record_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
                Agm_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");


                BOOK_CL_ID = Convert.ToInt32(bookCloserCorporateDec.Rows[0]["BOOK_CL_ID"].ToString());
                DataTable bookCloserDetails = new DataTable();

                string BOOK_CL_DETAILS = "Select distinct(f_cd) as f_cd from BOOK_CL_DETAILS  Where BOOK_CL_ID=" + BOOK_CL_ID + "";
                bookCloserDetails = commonGatewayObj.Select(BOOK_CL_DETAILS);
                string F_cd_withComma = "";
                string F_cd = "";
                string q = "";

                if (bookCloserDetails.Rows.Count > 0)
                {

                    for (int k = 0; k < bookCloserDetails.Rows.Count; k++)
                    {
                        F_cd_withComma = F_cd_withComma + "," + bookCloserDetails.Rows[k]["f_cd"].ToString().ToLower().Trim();
                    }
                    F_cd = F_cd_withComma.Remove(0, 1);

                    q = (" select * from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,PFOLIO_BK.TOT_NOS,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, " +
             "   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   " +
              "  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  " +
                     "      decode(FUND.F_CD, 1,  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .2, 0) AS TAX,   " +
                            "   decode(FUND.F_CD, 1,   PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .8,  " +
                                   "    PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY) AS NET_DIVIDEND,TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    " +
              "              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND " +
              " INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND " +
              "CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND" +
              "(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM" +
              "  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'  AND F_CD not in(" + F_cd + ")  ");

                }
                else
                {

                    q = (" select * from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,PFOLIO_BK.TOT_NOS,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, " +
              "   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   " +
               "  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  " +
                      "      decode(FUND.F_CD, 1,  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .2, 0) AS TAX,   " +
                             "   decode(FUND.F_CD, 1,   PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .8,  " +
                                    "    PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY) AS NET_DIVIDEND,TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    " +
               "              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND " +
               " INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND " +
               "CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND" +
               "(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM" +
               "  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'   ");

                }




                //DataTable bookCloser = commonGatewayObj.Select(" select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd WHERE POSTED is null order by RECORD_DT DESC) order by BOOK_CL_ID DESC  ");
                DataTable bookCloser = commonGatewayObj.Select(q);
                if (bookCloser.Rows.Count > 0)
                {


                    leftDataGrid.DataSource = bookCloser;
                    leftDataGrid.DataBind();

                    Session["dtBoolCloser"] = bookCloser;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
                    postedButton.Visible = false;
                    Response.Redirect("CorporateDeclarationSecurities.aspx");



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
        BOOK_CL_DETAILS bookclDetails = new BOOK_CL_DETAILS();
        string type = (string)Session["type"];
        DateTime dtimeCurrentDateTime = DateTime.Now;
        string strCurrentDateTimeForLog = dtimeCurrentDateTime.ToString("dd-MMM-yyyy");
        string LoginName = Session["UserName"].ToString();
        int countCheck = 0;
        int count = 0;
        if (type == "C")
        {
            foreach (DataGridItem Drvcash in leftDataGrid.Items)
            {
                CheckBox leftCheckBox = (CheckBox)Drvcash.Cells[countCheck].FindControl("leftCheckBox");
                {

                    if (leftCheckBox.Checked)
                    {



                        string strQuery = "select max(BOOK_CL_DET_ID)+1 AS BOOK_CL_DET_ID From BOOK_CL_DETAILS ";

                        DataTable dt = commonGatewayObj.Select(strQuery);
                        int BOOK_CL_DET_ID = Convert.ToInt32(dt.Rows[0]["BOOK_CL_DET_ID"]);


                        string strQueryBookCloserCash = "insert into BOOK_CL_DETAILS (BOOK_CL_DET_ID, F_CD, COMP_CD,TOT_NOS,BOOK_CL_ID,DIVIDEND_PER_SHR,GROSS_DIVIDEND,TAX,NET_DIVIDEND,ENTRY_DATE,USERNAME) values (" + BOOK_CL_DET_ID + "," + Convert.ToUInt32(Drvcash.Cells[1].Text.ToString()) + "," + Convert.ToUInt32(Drvcash.Cells[4].Text.ToString()) + "," + Convert.ToUInt32(Drvcash.Cells[6].Text.ToString()) + "," + Convert.ToUInt32(Drvcash.Cells[3].Text.ToString()) + "," + Convert.ToDecimal(Drvcash.Cells[11].Text.ToString()) + ",'" + Convert.ToDecimal(Drvcash.Cells[12].Text.ToString()) + "','" + Convert.ToDecimal(Drvcash.Cells[13].Text.ToString()) + "','" + Convert.ToDecimal(Drvcash.Cells[14].Text.ToString()) + "','" + strCurrentDateTimeForLog + "','" + LoginName + "')";
                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strQueryBookCloserCash);


                        int book_cl_id = Convert.ToInt16(Drvcash.Cells[3].Text.ToString());
                        DataTable totalRow = CountRowSEARCH(book_cl_id,type);
                        int total_fcd = Convert.ToInt32(totalRow.Rows[0]["TotalF_cd"]);
                        DataTable bookCloserDetails = new DataTable();

                        string BOOK_CL_DETAILS = "Select count(f_cd) as Tf_cd from BOOK_CL_DETAILS  Where BOOK_CL_ID=" + book_cl_id + "";
                        bookCloserDetails = commonGatewayObj.Select(BOOK_CL_DETAILS);
                        int bookCloserDetailsrow = Convert.ToInt32(bookCloserDetails.Rows[0]["Tf_cd"]);


                        if (total_fcd == bookCloserDetailsrow)
                        {


                            string strupdateQueryCorporateDec = "update CORPORATE_DEC set POSTED='Y',PDATE='" + strCurrentDateTimeForLog + "' where   COMP_CD=" + Drvcash.Cells[4].Text.ToString() + " and BOOK_CL_ID=" + Drvcash.Cells[3].Text.ToString() + " ";
                            int updateCorporateDecNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryCorporateDec);
                        }
                        

                     
                        
                        count++;
                        //Response.Redirect("CorporateDeclarationSecurities.aspx");

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);

                    }
                    
                }

            }
            if (count == 0)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please Check the checkbox');", true);

            }
            else
            {
                Response.Redirect("CorporateDeclarationDetails.aspx");
            }

        }







    }



    public DataTable CountRowSEARCH(int BOOK_CL_ID,string type)
    {
        CommonGateway commonGatewayObj = new CommonGateway();

        string companycode = "";

        string FY = "";

        companycode = (string)Session["comp_cd"];
        FY = (string)Session["FY"];

        string Record_dt = "";
        string Agm_dt = "";

        DataTable bookCloserCorporateDec = commonGatewayObj.Select("SELECT * FROM CORPORATE_DEC WHERE COMP_CD=" + companycode + " and type='" + type + "' and FY='" + FY + "' order by BOOK_CL_ID DESC  ");
        if (bookCloserCorporateDec.Rows.Count > 0)
        {
            Record_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
            Agm_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");

        }

        string q = (" select count( F_CD) as TotalF_cd from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,PFOLIO_BK.TOT_NOS,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, " +
              "   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   " +
               "  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  " +
                      "      decode(FUND.F_CD, 1,  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .2, 0) AS TAX,   " +
                             "   decode(FUND.F_CD, 1,   PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .8,  " +
                                    "    PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY) AS NET_DIVIDEND,TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    " +
               "              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND " +
               " INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND " +
               "CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND" +
               "(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM" +
               "  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'   ");
        DataTable bookCloser = commonGatewayObj.Select(q);

        return bookCloser;
    }
}