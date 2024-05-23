using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_BookCloserDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        CommonGateway commonGatewayObj = new CommonGateway();

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }
        
        string companycode = "";
        string type = "";
        string FY = "";
        //companycode = Convert.ToString(Request.QueryString["comp_cd"]).Trim();
        //type = Convert.ToString(Request.QueryString["Type"]).Trim();
        companycode = (string)Session["comp_cd"];
        type = (string)Session["type"];
        FY= (string)Session["FY"];

        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtFYDropDownList = dropDownListObj.FillFYDropDownList();

        if (!IsPostBack)
        {
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.SelectedValue = companycode;
            companyNameDropDownList.DataBind();

            typeDropDownList.SelectedValue = type;

            FYDropDownList.DataSource = dtFYDropDownList;
            FYDropDownList.DataTextField = "FY_NAME";
            FYDropDownList.DataValueField = "FY_ID";
            FYDropDownList.SelectedValue = FY;
            FYDropDownList.DataBind();

            DataTable bookCloser = commonGatewayObj.Select("SELECT * FROM CORPORATE_DEC WHERE COMP_CD=" + companycode + " and type='" + type + "' and FY='" + FY + "' order by BOOK_CL_ID DESC  ");
            if (bookCloser.Rows.Count > 0)
            {

                recordDateTextBox.Text = Convert.ToDateTime(bookCloser.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
                agmDateTextBox.Text = Convert.ToDateTime(bookCloser.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");

            }

            SEARCH();

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
    protected void FYDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();
        string type = typeDropDownList.SelectedValue.ToString();
        string FY = FYDropDownList.SelectedValue.ToString();

        DataTable bookCloser = commonGatewayObj.Select("SELECT * FROM CORPORATE_DEC WHERE COMP_CD="+ comp_cd + " and type='"+type+"' and FY='"+FY+"' order by BOOK_CL_ID DESC  ");
        if (bookCloser.Rows.Count > 0)
        {

          recordDateTextBox.Text= Convert.ToDateTime(bookCloser.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
          agmDateTextBox.Text = Convert.ToDateTime(bookCloser.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");

        }
    }
    public void postedButton_Click(object sender, EventArgs e)
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        Hashtable httable = new Hashtable();
        string strQuery = "select max(BOOK_CL_DET_ID)+1 AS BOOK_CL_DET_ID From BOOK_CL_DETAILS ";

        DataTable dt = commonGatewayObj.Select(strQuery);

        httable.Add("BOOK_CL_DET_ID", Convert.ToInt16(dt.Rows[0]["BOOK_CL_DET_ID"]));

        BOOK_CL_DETAILS bookclDetails = new BOOK_CL_DETAILS();

        string type = typeDropDownList.SelectedValue.ToString();
        DateTime dtimeCurrentDateTime = DateTime.Now;
        string strCurrentDateTimeForLog = dtimeCurrentDateTime.ToString("dd-MMM-yyyy");
        int countCheck = 0;
        if (type == "C")
        {
            foreach (GridViewRow Drvcash in grdGridCAsh.Rows)
            {
                CheckBox leftCheckBox = (CheckBox)grdGridCAsh.Rows[countCheck].FindControl("leftCheckBox");
                {
                    if (leftCheckBox.Checked)
                    {


                        string strQueryBookCloserCash = "insert into BOOK_CL_DETAILS (BOOK_CL_DET_ID, F_CD, COMP_CD,TOT_NOS,BOOK_CL_ID,DIVIDEND_PER_SHR,GROSS_DIVIDEND,TAX,NET_DIVIDEND) values ('" + Convert.ToInt16(dt.Rows[0]["BOOK_CL_DET_ID"]) + "','" + Convert.ToUInt32(Drvcash.Cells[1].Text.ToString()) + "','" + Convert.ToUInt32(Drvcash.Cells[4].Text.ToString()) + "','" + Convert.ToUInt32(Drvcash.Cells[6].Text.ToString()) + "','" + Convert.ToUInt32(Drvcash.Cells[3].Text.ToString()) + "','" + Convert.ToDecimal(Drvcash.Cells[13].Text.ToString()) + "','" + Convert.ToDecimal(Drvcash.Cells[14].Text.ToString()) + "','" + Convert.ToDecimal(Drvcash.Cells[15].Text.ToString()) + "','" + Convert.ToDecimal(Drvcash.Cells[16].Text.ToString()) + "')";
                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strQueryBookCloserCash);

                        string strupdateQueryCorporateDec = "update CORPORATE_DEC set POSTED='Y',PDATE='" + strCurrentDateTimeForLog + "' where f_cd =" + Drvcash.Cells[1].Text.ToString() + " and COMP_CD=" + Drvcash.Cells[4].Text.ToString() + " and BOOK_CL_ID=" + Drvcash.Cells[3].Text.ToString() + " ";
                        int updateCorporateDecNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryCorporateDec);

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);
                        // httable.Add("ALLOTED_SHARE", Drvcash.Cells[3].Text.ToString());

                    }
                }

            }
        }
        else if (type == "B")
        {
            foreach (GridViewRow Drvbonus in grdGridBonus.Rows)
            {
                CheckBox leftCheckBox = (CheckBox)grdGridBonus.Rows[countCheck].FindControl("leftCheckBox");
                {
                    if (leftCheckBox.Checked)
                    {

                        string strQueryBookCloserCash = "insert into BOOK_CL_DETAILS (BOOK_CL_DET_ID, F_CD, COMP_CD,TOT_NOS,ALLOTED_SHARE,BOOK_CL_ID) values ('" + Convert.ToInt16(dt.Rows[0]["BOOK_CL_DET_ID"]) + "','" + Convert.ToUInt32(Drvbonus.Cells[1].Text.ToString()) + "','" + Convert.ToUInt32(Drvbonus.Cells[4].Text.ToString()) + "','" + Convert.ToUInt32(Drvbonus.Cells[6].Text.ToString()) + "','" + Convert.ToUInt32(Drvbonus.Cells[13].Text.ToString()) + "','" + Convert.ToDecimal(Drvbonus.Cells[3].Text.ToString()) + "')";
                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strQueryBookCloserCash);



                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);


                        httable.Add("F_CD", Convert.ToUInt32(Drvbonus.Cells[1].Text.ToString()));
                        httable.Add("COMP_CD", Convert.ToUInt32(Drvbonus.Cells[4].Text.ToString()));
                        httable.Add("TOT_NOS", Convert.ToUInt32(Drvbonus.Cells[6].Text.ToString()));
                        httable.Add("ALLOTED_SHARE", Convert.ToUInt32(Drvbonus.Cells[13].Text.ToString()));
                        httable.Add("BOOK_CL_ID", Convert.ToUInt32(Drvbonus.Cells[3].Text.ToString()));
                        commonGatewayObj.Insert(httable, "BOOK_CL_DETAILS");
                        httable.Clear();

                       


                        string strupdateQueryCorporateDec = "update CORPORATE_DEC set POSTED='Y',PDATE='" + strCurrentDateTimeForLog + "' where f_cd =" + Drvbonus.Cells[1].Text.ToString() + " and COMP_CD=" + Drvbonus.Cells[4].Text.ToString() + " and BOOK_CL_ID=" + Drvbonus.Cells[3].Text.ToString() + " ";
                        int updateCorporateDecNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryCorporateDec);



                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Saved Successfully');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);
                        // httable.Add("ALLOTED_SHARE", Drvcash.Cells[3].Text.ToString());



                    }
                }

            }

        }

      


        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);
    }

    protected void OnPageIndexChangingCash(object sender, GridViewPageEventArgs e)
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable bookCloserCash = (System.Data.DataTable)Session["bookCloserCash"];
        // System.Data.DataTable dtchildodsubMenuList = (System.Data.DataTable)Session["Child_of_submenu"];
        grdGridCAsh.DataSource = bookCloserCash;
        grdGridCAsh.PageIndex = e.NewPageIndex;
        grdGridCAsh.DataBind();
    }
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        DataTable bookCloserBonus = (System.Data.DataTable)Session["bookCloserBonus"];
        // System.Data.DataTable dtchildodsubMenuList = (System.Data.DataTable)Session["Child_of_submenu"];
        grdGridBonus.DataSource = bookCloserBonus;
        grdGridBonus.PageIndex = e.NewPageIndex;
        grdGridBonus.DataBind();
    }

    //public void searchButton_Click(object sender, EventArgs e)
    //{
    //    CommonGateway commonGatewayObj = new CommonGateway();
    //    string comp_cd = "";
    //    char type = char.Parse(typeDropDownList.SelectedValue.ToString());
    //    string FY = "";
    //    string Record_dt = "";
    //    string Agm_dt = "";
    //    comp_cd = companyNameDropDownList.SelectedValue.ToString();
        
    //    FY = FYDropDownList.SelectedValue.ToString();
    //    Record_dt = recordDateTextBox.Text.ToString();
    //    Agm_dt = agmDateTextBox.Text.ToString();

    //    DataTable bookCloserBonus = new DataTable();
    //    DataTable bookCloserCash = new DataTable();


    //    if (type == 'B')
    //    {
    //        bookCloserBonus = commonGatewayObj.Select("select * from (SELECT C.COMP_NM,B.COMP_CD,  floor(P.TOT_NOS) tot_nos, p.f_cd, f_name f_name,b.BOOK_CL_ID,b.TYPE,floor(P.TOT_NOS * B.QUANTITY / 100) share_alloted,B.FY, B.RECORD_DT," +
    //     " B.QUANTITY, B.APPR_DT, B.AGM, B.REMARKS, B.POSTED, b.pdate  FROM CORPORATE_DEC B, COMP C, PFOLIO_BK P, fund f " +
    //      " WHERE B.COMP_CD = P.COMP_CD AND B.COMP_CD = C.COMP_CD AND P.BAL_DT_CTRL = B.RECORD_DT and B.QUANTITY is not null " +
    //      " and B.TYPE = '" + type + "' and f.IS_F_CLOSE is null and p.f_cd = f.f_cd order by p.f_cd) where comp_cd = " + comp_cd + " and FY = '" + FY + "' Order by f_cd ");
    //        if (bookCloserBonus.Rows.Count > 0)
    //        {

    //            //grdGridBonus.Visible = true;
    //            grdGridBonus.DataSource = bookCloserBonus;
    //            grdGridBonus.DataBind();
    //            postedButton.Visible = true;
    //            Session["bookCloserBonus"] = bookCloserBonus;

    //        }
    //        else {
    //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
    //        }
            
    //    }
    //    else if (type == 'C')
    //    {
    //       string q= ("SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,PFOLIO_BK.TOT_NOS,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, " +
    //  "   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   " +
    //   "  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  " +
    //          "      decode(FUND.F_CD, 1,  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .2, 0) AS TAX,   " +
    //                 "   decode(FUND.F_CD, 1,   PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .8,  " +
    //                        "    PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY) AS NET_DIVIDEND,TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    " +
    //   "              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND " +
    //   " INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND "+
    //   "CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND"+
    //   "(CORPORATE_DEC.RECORD_DT BETWEEN '"+ Record_dt + "' AND '"+ Record_dt +"') AND (CORPORATE_DEC.AGM"+
    //   "  BETWEEN '"+Agm_dt+"' AND '"+Agm_dt+"')   AND COMP.COMP_CD="+comp_cd+ "  ORDER BY AGM ,F_CD ASC  ");

    //        bookCloserCash = commonGatewayObj.Select(q);

    //        if (bookCloserCash.Rows.Count > 0)
    //        {

    //            grdGridCAsh.Visible = true;
    //            grdGridCAsh.DataSource = bookCloserCash;
    //            grdGridCAsh.DataBind();
    //            postedButton.Visible = true;
    //            Session["bookCloserCash"] = bookCloserCash;


    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
    //        }
    //    }



    //}

    public void SEARCH()
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        string comp_cd = "";
        char type = char.Parse(typeDropDownList.SelectedValue.ToString());
        string FY = "";
        string Record_dt = "";
        string Agm_dt = "";
        comp_cd = companyNameDropDownList.SelectedValue.ToString();

        FY = FYDropDownList.SelectedValue.ToString();
        Record_dt = recordDateTextBox.Text.ToString();
        Agm_dt = agmDateTextBox.Text.ToString();

        DataTable bookCloserBonus = new DataTable();
        DataTable bookCloserCash = new DataTable();


        if (type == 'B')
        {
            bookCloserBonus = commonGatewayObj.Select("select * from (SELECT C.COMP_NM,B.COMP_CD,  floor(P.TOT_NOS) tot_nos, p.f_cd, f_name f_name,b.BOOK_CL_ID,b.TYPE,floor(P.TOT_NOS * B.QUANTITY / 100) share_alloted,B.FY, B.RECORD_DT," +
         " B.QUANTITY, B.APPR_DT, B.AGM, B.REMARKS, B.POSTED, b.pdate  FROM CORPORATE_DEC B, COMP C, PFOLIO_BK P, fund f " +
          " WHERE B.COMP_CD = P.COMP_CD AND B.COMP_CD = C.COMP_CD AND P.BAL_DT_CTRL = B.RECORD_DT and B.QUANTITY is not null " +
          " and B.TYPE = '" + type + "' and f.IS_F_CLOSE is null and p.f_cd = f.f_cd order by p.f_cd) where comp_cd = " + comp_cd + " and FY = '" + FY + "' Order by f_cd ");
            if (bookCloserBonus.Rows.Count > 0)
            {

                //grdGridBonus.Visible = true;
                grdGridBonus.DataSource = bookCloserBonus;
                grdGridBonus.DataBind();
                postedButton.Visible = true;
                Session["bookCloserBonus"] = bookCloserBonus;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
            }

        }
        else if (type == 'C')
        {
            string q = (" select * from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,PFOLIO_BK.TOT_NOS,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, " +
       "   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   " +
        "  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  " +
               "      decode(FUND.F_CD, 1,  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .2, 0) AS TAX,   " +
                      "   decode(FUND.F_CD, 1,   PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY * .8,  " +
                             "    PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY) AS NET_DIVIDEND,TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    " +
        "              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND " +
        " INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND " +
        "CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND" +
        "(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM" +
        "  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + comp_cd + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C' ");

            bookCloserCash = commonGatewayObj.Select(q);

            if (bookCloserCash.Rows.Count > 0)
            {

                grdGridCAsh.Visible = true;
                grdGridCAsh.DataSource = bookCloserCash;
                grdGridCAsh.DataBind();
                postedButton.Visible = true;
                Session["bookCloserCash"] = bookCloserCash;


            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
                //Response.Redirect("CorporateDeclarationSecurities.aspx");
            }
        }
    }
}