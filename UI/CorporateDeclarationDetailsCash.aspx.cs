using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_CorporateDeclarationDetailsCash : System.Web.UI.Page
{
    decimal taxRate = 15;
    CommonGateway commonGatewayObj = new CommonGateway();
    protected void Page_Load(object sender, EventArgs e)
    {

        string companycode = "";
        string type = "";
        string FY = "";
        int BOOK_CL_ID = 0;
        companycode = (string)Session["comp_cd"];
        type = (string)Session["type"];
        FY = (string)Session["FY"];
        BOOK_CL_ID = (int)Session["book_cl_id"];

        string Record_dt = "";
        string Agm_dt = "";





        if (!IsPostBack)
        {
            DataTable bookCloserCorporateDec = commonGatewayObj.Select("SELECT * FROM CORPORATE_DEC WHERE BOOK_CL_ID="+ BOOK_CL_ID + " AND COMP_CD=" + companycode + " and type='" + type + "' and FY='" + FY + "' order by BOOK_CL_ID DESC  ");
            if (bookCloserCorporateDec.Rows.Count > 0)
            {

                Record_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
                Agm_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");

                TextBoxRecordDate.Text = Record_dt.ToString();


                DataTable bookCloserDetails = new DataTable();

                string BOOK_CL_DETAILS = "Select distinct(f_cd) as f_cd from BOOK_CL_DETAILS  Where BOOK_CL_ID=" + BOOK_CL_ID + "";
                bookCloserDetails = commonGatewayObj.Select(BOOK_CL_DETAILS);
                string F_cd_withComma = "";
                string F_cd = "";
                taxRate = Convert.ToDecimal(TaxRateTextBox.Text.ToString());
                StringBuilder sbMst = new StringBuilder();
                if (bookCloserDetails.Rows.Count > 0)
                {

                    for (int k = 0; k < bookCloserDetails.Rows.Count; k++)
                    {
                        F_cd_withComma = F_cd_withComma + "," + bookCloserDetails.Rows[k]["f_cd"].ToString().ToLower().Trim();
                    }
                    F_cd = F_cd_withComma.Remove(0, 1);

                    sbMst.Append(" select * from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,TRUNC(PFOLIO_BK.TOT_NOS) AS TOT_NOS ,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, ");
                    sbMst.Append("   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   ");
                    sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  ");
                    sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS TAX_AMT,");
                    sbMst.Append("      " + taxRate + "   AS TAX,   ");
                    sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY -PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS  NET_AMT,  ");
                    sbMst.Append("     TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    ");
                    sbMst.Append("              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND ");
                    sbMst.Append(" INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND ");
                    sbMst.Append("CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL AND  FUND.IS_F_CLOSE IS NULL AND FUND.BOID IS NOT NULL WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND");
                    sbMst.Append("(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM");
                    sbMst.Append("  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'  AND F_CD not in(" + F_cd + ")  ");

                }
                else
                {

                    sbMst.Append(" select * from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,TRUNC(PFOLIO_BK.TOT_NOS) AS TOT_NOS ,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, ");
                    sbMst.Append("   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   ");
                    sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  ");
                    sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS TAX_AMT,");
                    sbMst.Append("      " + taxRate + "   AS TAX,   ");
                    sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY -PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS  NET_AMT,  ");
                    sbMst.Append("     TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    ");
                    sbMst.Append("              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND ");
                    sbMst.Append(" INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND ");
                    sbMst.Append("CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL AND  FUND.IS_F_CLOSE IS NULL AND FUND.BOID IS NOT NULL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND");
                    sbMst.Append("(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM");
                    sbMst.Append("  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'   ");

                }




                //DataTable bookCloser = commonGatewayObj.Select(" select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd WHERE POSTED is null order by RECORD_DT DESC) order by BOOK_CL_ID DESC  ");
                DataTable bookCloser = commonGatewayObj.Select(sbMst.ToString());
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
                    Session["dtBoolCloser"] = null;
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


        int BOOK_CL_ID = (int)Session["book_cl_id"];

        DateTime Record_dt = Convert.ToDateTime(TextBoxRecordDate.Text.ToString());

        CommonGateway commonGatewayObj = new CommonGateway();
        BOOK_CL_DETAILS bookclDetails = new BOOK_CL_DETAILS();
        string type = (string)Session["type"];
        DateTime dtimeCurrentDateTime = DateTime.Now;
        string strCurrentDateTimeForLog = dtimeCurrentDateTime.ToString("dd-MMM-yyyy");
        string LoginName = Session["UserID"].ToString();
        int countCheck = 0;
        int count = 0;        
        string declarationType = "";
        if (type == "C")
        {
            foreach (DataGridItem Drvcash in leftDataGrid.Items)
            {
                CheckBox leftCheckBox = (CheckBox)Drvcash.Cells[0].FindControl("leftCheckBox");
                {

                    if (leftCheckBox.Checked)
                    {
                        declarationType= Drvcash.Cells[7].Text.ToString().ToUpper();
                        if (declarationType == "C")
                        {
                            //Cash Receibale Voucher Entry
                            int fund_code = Convert.ToInt16(Drvcash.Cells[1].Text.ToString());
                             string accSchema = getFundSchema(fund_code);
                            //string accSchema = "UNIT_BR";
                            int comp_code = Convert.ToInt16(Drvcash.Cells[4].Text.ToString());
                            string comp_name = Drvcash.Cells[5].Text.ToString();
                            long totalShare = Convert.ToInt32(Drvcash.Cells[6].Text.ToString());
                            string fy = Drvcash.Cells[8].Text.ToString();
                            DateTime agmDate = Convert.ToDateTime(Drvcash.Cells[9].Text.ToString());                       
                            decimal diviRatePerShare= Convert.ToDecimal(Drvcash.Cells[11].Text.ToString());
                           // decimal grossDividend = Convert.ToDecimal(Drvcash.Cells[12].Text.ToString());

                            TextBox txtTaxAmount = (TextBox)Drvcash.Cells[0].FindControl("TAX_AMTTXT");
                            TextBox txtNetAmount = (TextBox)Drvcash.Cells[0].FindControl("NET_AMTTXT");


                            Double grossDividend = Convert.ToDouble(Drvcash.Cells[12].Text.ToString());
                            double tax_rate = Convert.ToDouble(TaxRateTextBox.Text.ToString());
                            double Tax_amt = Convert.ToDouble(txtTaxAmount.Text.ToString());
                            double net_amt = Convert.ToDouble(txtNetAmount.Text.ToString());
                            //double net_amt = Gross_amt - Tax_amt;

                            // Cash Receibale Voucher Entry
                            string accVoucherNo = "";
                            string strBOndComp = "select *  from COMP where COMP_CD=" +comp_code;
                            DataTable dtBOndComp = commonGatewayObj.Select(strBOndComp);
                            if (dtBOndComp.Rows.Count > 0)
                            {

                                if (dtBOndComp.Rows[0]["SECT_MAJ_CD"].ToString() == "89")
                                {
                                    //For Record Date
                                    accVoucherNo = SaveSaleUniAccountVoucherForBond(accSchema, totalShare, grossDividend, tax_rate, Tax_amt, net_amt, comp_name, diviRatePerShare, fy, fund_code, Record_dt, LoginName);

                                    //For Agm Date
                                    //accVoucherNo = SaveSaleUniAccountVoucherForBond(accSchema, totalShare, grossDividend, tax_rate, Tax_amt, net_amt, comp_name, diviRatePerShare, fy, fund_code, agmDate, LoginName);
                                }
                                else
                                {
                                    //For Record Date
                                    accVoucherNo = SaveSaleUniAccountVoucher(accSchema, totalShare, grossDividend, tax_rate, Tax_amt, net_amt, comp_name, diviRatePerShare, fy, fund_code, Record_dt, LoginName);

                                    //For Agm Date
                                    // accVoucherNo = SaveSaleUniAccountVoucher(accSchema, totalShare, grossDividend, tax_rate, Tax_amt, net_amt, comp_name, diviRatePerShare, fy, fund_code, agmDate, LoginName);
                                }

                            }


                            DateTime dtrecorddate = Convert.ToDateTime(TextBoxRecordDate.Text.ToString());


                            if (dtimeCurrentDateTime > dtrecorddate)
                            {
                                commonGatewayObj.BeginTransaction();
                                string strQuery = "select NVL(max(BOOK_CL_DET_ID),0)+1 AS BOOK_CL_DET_ID From BOOK_CL_DETAILS ";
                                DataTable dt = commonGatewayObj.Select(strQuery);
                                int BOOK_CL_DET_ID = Convert.ToInt32(dt.Rows[0]["BOOK_CL_DET_ID"]);
                                string strQueryBookCloserCash = "insert into BOOK_CL_DETAILS (BOOK_CL_DET_ID, F_CD, COMP_CD,TOT_NOS,BOOK_CL_ID,DIVIDEND_PER_SHR,GROSS_DIVIDEND,ENTRY_DATE,USERNAME, DIV_RRCIVABLE_ACC_VCH_NO) values (" + BOOK_CL_DET_ID + "," + fund_code + "," + comp_code + "," + totalShare + "," + Convert.ToInt32(Drvcash.Cells[3].Text.ToString()) + "," + diviRatePerShare + ",'" + grossDividend + "','" + strCurrentDateTimeForLog + "','" + LoginName + "', '" + accVoucherNo + "')";


                                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strQueryBookCloserCash);
                                commonGatewayObj.CommitTransaction();



                                int book_cl_id = Convert.ToInt16(Drvcash.Cells[3].Text.ToString());
                                DataTable totalRow = CountRowSEARCH(book_cl_id, type);
                                int total_fcd = Convert.ToInt32(totalRow.Rows[0]["TotalF_cd"]);
                                DataTable bookCloserDetails = new DataTable();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please Check  Record Date');", true);
                            }

                       

                           


                            count++;
                          
                        }
                    }
                    countCheck++;

                }

            }
            if(count== countCheck)
            {
                string strupdateQueryCorporateDec = "UPDATE CORPORATE_DEC SET POSTED='Y',PDATE='" + strCurrentDateTimeForLog + "' WHERE    BOOK_CL_ID="+ BOOK_CL_ID;
                int updateCorporateDecNumOfRows = commonGatewayObj.ExecuteNonQuery(strupdateQueryCorporateDec);
                commonGatewayObj.CommitTransaction();
            }
            if (count == 0)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please  Check the checkbox');", true);

            }
            else
            {

                Response.Redirect("CorporateDeclarationSecurities.aspx");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Save Sucessfully');", true);
            }

        }




    }

    protected void TaxRateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (TaxRateTextBox.Text != "")
        {
            string companycode = "";
            string type = "";
            string FY = "";
            int BOOK_CL_ID = 0;
            companycode = (string)Session["comp_cd"];
            type = (string)Session["type"];
            FY = (string)Session["FY"];
            BOOK_CL_ID = (int)Session["book_cl_id"];

            string Record_dt = "";
            string Agm_dt = "";

                DataTable bookCloserCorporateDec = commonGatewayObj.Select("SELECT * FROM CORPORATE_DEC WHERE BOOK_CL_ID=" + BOOK_CL_ID + " AND COMP_CD=" + companycode + " and type='" + type + "' and FY='" + FY + "' order by BOOK_CL_ID DESC  ");
                if (bookCloserCorporateDec.Rows.Count > 0)
                {

                    Record_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["RECORD_DT"]).ToString("dd-MMM-yyyy");
                    Agm_dt = Convert.ToDateTime(bookCloserCorporateDec.Rows[0]["AGM"]).ToString("dd-MMM-yyyy");


                    DataTable bookCloserDetails = new DataTable();

                    string BOOK_CL_DETAILS = "Select distinct(f_cd) as f_cd from BOOK_CL_DETAILS  Where BOOK_CL_ID=" + BOOK_CL_ID + "";
                    bookCloserDetails = commonGatewayObj.Select(BOOK_CL_DETAILS);
                    string F_cd_withComma = "";
                    string F_cd = "";
                    taxRate = Convert.ToDecimal(TaxRateTextBox.Text.ToString());
                    StringBuilder sbMst = new StringBuilder();
                    if (bookCloserDetails.Rows.Count > 0)
                    {

                        for (int k = 0; k < bookCloserDetails.Rows.Count; k++)
                        {
                            F_cd_withComma = F_cd_withComma + "," + bookCloserDetails.Rows[k]["f_cd"].ToString().ToLower().Trim();
                        }
                        F_cd = F_cd_withComma.Remove(0, 1);

                        sbMst.Append(" select * from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,TRUNC(PFOLIO_BK.TOT_NOS) AS TOT_NOS ,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, ");
                        sbMst.Append("   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   ");
                        sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  ");
                        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS TAX_AMT,");
                        sbMst.Append("      " + taxRate + "   AS TAX,   ");
                        sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY -PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS  NET_AMT,  ");
                        sbMst.Append("     TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    ");
                        sbMst.Append("              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND ");
                        sbMst.Append(" INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND ");
                        sbMst.Append("CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL AND  FUND.IS_F_CLOSE IS NULL AND FUND.BOID IS NOT NULL WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND");
                        sbMst.Append("(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM");
                        sbMst.Append("  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'  AND F_CD not in(" + F_cd + ")  ");

                    }
                    else
                    {

                        sbMst.Append(" select * from (SELECT     FUND.F_CD,FUND.F_NAME, COMP.COMP_CD,COMP.COMP_NM,TRUNC(PFOLIO_BK.TOT_NOS) AS TOT_NOS ,CORPORATE_DEC.FY,CORPORATE_DEC.BOOK_CL_ID,CORPORATE_DEC.TYPE ,CORPORATE_DEC.RECORD_DT, CORPORATE_DEC.AGM,CORPORATE_DEC.APPR_DT, ");
                        sbMst.Append("   COMP.FC_VAL, CORPORATE_DEC.QUANTITY, COMP.FC_VAL * CORPORATE_DEC.QUANTITY / 100 AS DIVIDEND_PER_SHARE,   ");
                        sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY AS GROSS_DIVIDEND,  ");
                        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS TAX_AMT,");
                        sbMst.Append("      " + taxRate + "   AS TAX,   ");
                        sbMst.Append("  PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY -PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * CORPORATE_DEC.QUANTITY*ROUND(" + taxRate + "/100,2) AS  NET_AMT,  ");
                        sbMst.Append("     TO_CHAR(SYSDATE, 'MM-DD-YYYY') As PDATE  FROM    ");
                        sbMst.Append("              CORPORATE_DEC  INNER JOIN  COMP ON CORPORATE_DEC.COMP_CD = COMP.COMP_CD INNER JOIN  FUND ");
                        sbMst.Append(" INNER JOIN  PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON CORPORATE_DEC.COMP_CD = PFOLIO_BK.COMP_CD AND ");
                        sbMst.Append("CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL AND  FUND.IS_F_CLOSE IS NULL AND FUND.BOID IS NOT NULL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND");
                        sbMst.Append("(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM");
                        sbMst.Append("  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'   ");

                    }




                    //DataTable bookCloser = commonGatewayObj.Select(" select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd WHERE POSTED is null order by RECORD_DT DESC) order by BOOK_CL_ID DESC  ");
                    DataTable bookCloser = commonGatewayObj.Select(sbMst.ToString());
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
                        Session["dtBoolCloser"] = null;
                        Response.Redirect("CorporateDeclarationSecurities.aspx");



                    }

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
               "CORPORATE_DEC.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL AND  FUND.IS_F_CLOSE IS NULL AND FUND.BOID IS NOT NULL  WHERE       (CORPORATE_DEC.QUANTITY IS NOT NULL)  AND" +
               "(CORPORATE_DEC.RECORD_DT BETWEEN '" + Record_dt + "' AND '" + Record_dt + "') AND (CORPORATE_DEC.AGM" +
               "  BETWEEN '" + Agm_dt + "' AND '" + Agm_dt + "')   AND COMP.COMP_CD=" + companycode + "  ORDER BY AGM ,F_CD ASC  ) Where TYPE='C'   ");
        DataTable bookCloser = commonGatewayObj.Select(q);

        return bookCloser;
    }

    public string SaveSaleUniAccountVoucherForBond(string accountSchema, long totalShare, double totalDivAmt, double tax_rate, double Tax_amt, double net_amt, string comp_name, decimal diviRate, string fy, int fund_code, DateTime agmDate, string LoginName)
    {

        try
        {
            long tranNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TRAN_ID");
            long contrlNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TO_NUMBER(CTRLNO)") - 1;
            int fundCode = fund_code;

            string dividendReceivableCode = "206060000";
            string dividendAdvanceTaxCode = "207030000";
            string dividnedIncomeCode = "301040000";


            Hashtable htInsert = new Hashtable();
            Hashtable htUpdate = new Hashtable();
            commonGatewayObj.BeginTransaction();
            string VoucherNo = getNexAccountVoucherNo(accountSchema, "10");


            if (fundCode == 1)
            {
                dividendReceivableCode = "205100000";
                dividnedIncomeCode = "301050000";
                //Dividend Receivable Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividendReceivableCode);
                htInsert.Add("BANKACNO", dividendReceivableCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Intt. Receivable from " + comp_name + " for FY:" + fy + " of :" + totalShare + " Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "D");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", totalDivAmt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //Dividend Income Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividnedIncomeCode);
                htInsert.Add("BANKACNO_CONTRA", dividnedIncomeCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Intt. Receivable from " + comp_name + " for FY:" + fy + " of Shares:" + totalShare + " @" + diviRate);
                htInsert.Add("TRAN_TYPE", "C");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", net_amt+ Tax_amt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //UPDATE TRANSACTION NUMBER AND CONTROL NUMBER
                contrlNumber++;
                tranNumber++;
                commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");

            }
            else
            {

                //Dividend Receivable Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividendReceivableCode);
                htInsert.Add("BANKACNO", dividendReceivableCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Intt. Receivable from " + comp_name + " for Fy:" + fy + " of :" + totalShare + " Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "D");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", totalDivAmt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //Dividend Income Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividnedIncomeCode);
                htInsert.Add("BANKACNO_CONTRA", dividnedIncomeCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Intt. Receivable from " + comp_name + " for FY:" + fy + " of Shares:" + totalShare + " @" + diviRate);
                htInsert.Add("TRAN_TYPE", "C");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", net_amt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //


                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividendAdvanceTaxCode);
                htInsert.Add("BANKACNO_CONTRA", dividendAdvanceTaxCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Diductable Tax from " + comp_name + " for FY:" + fy + " of :" + totalShare + " Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "C");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", Tax_amt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");
                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");



                //UPDATE TRANSACTION NUMBER AND CONTROL NUMBER
                contrlNumber++;
                tranNumber++;
                commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");



                commonGatewayObj.CommitTransaction();
            }
           
            return VoucherNo;

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }

    }



    public string SaveSaleUniAccountVoucher(string accountSchema, long totalShare, double totalDivAmt, double tax_rate, double Tax_amt, double net_amt, string comp_name, decimal diviRate,string fy,int fund_code,DateTime agmDate, string LoginName)
    {

        try
        {
            long tranNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TRAN_ID");
            long contrlNumber = commonGatewayObj.GetMaxNo(accountSchema + ".GL_BASICINFO", "TO_NUMBER(CTRLNO)") - 1;
            int fundCode = fund_code;
                     
            string dividendReceivableCode = "205010000";
            string dividendAdvanceTaxCode = "207030000";
            string dividnedIncomeCode = "303010000";

            Hashtable htInsert = new Hashtable();
            Hashtable htUpdate = new Hashtable();
            commonGatewayObj.BeginTransaction();
            string VoucherNo = getNexAccountVoucherNo(accountSchema, "10");


            if (fundCode == 1)
            {
                dividendReceivableCode = "209010000";
                //Dividend Receivable Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividendReceivableCode);
                htInsert.Add("BANKACNO", dividendReceivableCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Div. Receivable from " + comp_name + " for Fy:" + fy + " of :" + totalShare + " Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "D");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", totalDivAmt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //Dividend Income Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividnedIncomeCode);
                htInsert.Add("BANKACNO_CONTRA", dividnedIncomeCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Div. Receivable from " + comp_name + " for Fy:" + fy + " of :" + totalShare + "Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "C");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", net_amt+Tax_amt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //UPDATE TRANSACTION NUMBER AND CONTROL NUMBER
                contrlNumber++;
                tranNumber++;
                commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");
                commonGatewayObj.CommitTransaction();


            }
            else
            {

                //Dividend Receivable Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividendReceivableCode);
                htInsert.Add("BANKACNO", dividendReceivableCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Div. Receivable from " + comp_name + " for Fy:" + fy + " of :" + totalShare + " Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "D");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", totalDivAmt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //Dividend Income Debit
                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividnedIncomeCode);
                htInsert.Add("BANKACNO_CONTRA", dividnedIncomeCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Div. Receivable from " + comp_name + " for Fy:" + fy + " of :" + totalShare + "Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "C");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", net_amt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");

                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");

                //


                contrlNumber++;
                htInsert = new Hashtable();
                htInsert.Add("TRAN_ID", tranNumber + 1);
                htInsert.Add("ACCCODE", dividendAdvanceTaxCode);
                htInsert.Add("BANKACNO_CONTRA", dividendAdvanceTaxCode);
                htInsert.Add("TRAN_TIME", DateTime.Now.ToShortTimeString());
                htInsert.Add("TRAN_DATE", agmDate.ToString("dd-MMM-yyyy"));
                htInsert.Add("REMARKS", " Diductable Tax from " + comp_name + " for Fy:" + fy + " of :" + totalShare + "Shares @" + diviRate);
                htInsert.Add("TRAN_TYPE", "C");
                htInsert.Add("VOUCHER_NO", VoucherNo);
                htInsert.Add("TOTAL_AMNT", Tax_amt);
                htInsert.Add("CTRLNO", contrlNumber);
                htInsert.Add("OP_ID", LoginName);
                htInsert.Add("VOUCHER_TYPE", "10");
                htInsert.Add("RECENT", "y");
                htInsert.Add("LATESTDEL", "m");
                htInsert.Add("ISOUT", "N");
                htInsert.Add("ISREV", "N");
                htInsert.Add("OLDDATA", "N");
                commonGatewayObj.Insert(htInsert, accountSchema + ".GL_TRAN");



                //UPDATE TRANSACTION NUMBER AND CONTROL NUMBER
                contrlNumber++;
                tranNumber++;
                commonGatewayObj.ExecuteNonQuery(" UPDATE " + accountSchema + ".GL_BASICINFO SET TRAN_ID=" + tranNumber + " , CTRLNO='" + contrlNumber + "' WHERE 1=1");
                commonGatewayObj.CommitTransaction();
            }    
        
            return VoucherNo;

        }
        catch (Exception ex)
        {
            commonGatewayObj.RollbackTransaction();
            throw ex;
        }

    }
    public string getNexAccountVoucherNo(string accountSchema, string voucherTypeNo)
    {
        int accVoucherNo = 0;
        DataTable dtAccountVoucherNo = commonGatewayObj.Select("SELECT VOUCHER_NO FROM " + accountSchema + ".GL_TRAN WHERE (CTRLNO=(SELECT MAX(TO_NUMBER(CTRLNO))MAX_CTRLNO FROM " + accountSchema + ".GL_TRAN GL_TRAN_1 WHERE (VOUCHER_TYPE='" + voucherTypeNo + "'))) ");
        if (dtAccountVoucherNo.Rows.Count > 0)
        {
            string voucherNo = dtAccountVoucherNo.Rows[0]["VOUCHER_NO"].ToString().ToUpper();          
            accVoucherNo = Convert.ToInt32(voucherNo.ToString()) + 1;
        }
        else
        {
            accVoucherNo = 1;
        }
        return accVoucherNo.ToString();

    }
    public string getFundSchema(int fund_Code)
    {
        string accountSchema = "";
        DataTable dtSaleDiscount = commonGatewayObj.Select(" SELECT * FROM FUND_PARA WHERE F_CD="+fund_Code);
        accountSchema= dtSaleDiscount.Rows[0]["F_ACC_SCHEMA"].ToString();
        return accountSchema;

    }
}