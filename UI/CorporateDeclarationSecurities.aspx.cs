using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_CorporateDeclarationSecurities : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    DropDownList dropDownListObj = new DropDownList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

      
        if (!IsPostBack)
        {
            DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
            DataTable bookCloser = GrirdViewCorporateDeclaration();
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();
           

            if (bookCloser.Rows.Count > 0)
            { 
                dvGridBoolCloser.Visible = true;
                grdShowBookCloser.DataSource = bookCloser;
                grdShowBookCloser.DataBind();
                updateButton.Visible = true;
                ButtonDelete.Visible = true;

               
                Session["dtBoolCloser"] = bookCloser;

            }
            else
            {
                Session["dtBoolCloser"] = null;

            }

         
        }
    }

    public DataTable GrirdViewCorporateDeclaration()
    {
        DataTable bookCloser = commonGatewayObj.Select(" select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd  order by RECORD_DT DESC) order by AGM DESC  ");

        return bookCloser;
    }

    protected void grdShowBookCloser_SelectedIndexChanged(object sender, EventArgs e)
    {
        financialYearTextBox.Text = grdShowBookCloser.SelectedRow.Cells[3].Text;
        remarksTextBox.Text = grdShowBookCloser.SelectedRow.Cells[9].Text;
    }

    protected void addNewButton_Click(object sender, EventArgs e)
    {
        Hashtable httable = new Hashtable();
        string strQuery = "select max(BOOK_CL_ID)+1 AS BOOK_CL_ID From CORPORATE_DEC ";
        string LoginName = Session["UserID"].ToString();
        DataTable dt = commonGatewayObj.Select(strQuery);

        httable.Add("BOOK_CL_ID", Convert.ToInt16(dt.Rows[0]["BOOK_CL_ID"]));
        httable.Add("COMP_CD", Convert.ToInt16(companyCodeTextBox.Text));
        httable.Add("FY", financialYearTextBox.Text.ToString());
        if (!DropDownListFYPART.SelectedValue.Equals(""))
        {
            httable.Add("FY_PART", DropDownListFYPART.SelectedValue.ToString());
        }

        if (!recordDateTextBox.Text.Equals(""))
        {
            httable.Add("RECORD_DT", Convert.ToDateTime(recordDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!typeDropDownList.SelectedValue.Equals(""))
        {
            httable.Add("TYPE", typeDropDownList.SelectedValue.ToString());
        }
        if (!QuantityTextBox.Text.Equals(""))
        {
            httable.Add("QUANTITY", Convert.ToDecimal(QuantityTextBox.Text));
        }

        if (!agmDateTextBox.Text.Equals(""))
        {
            httable.Add("AGM", Convert.ToDateTime(agmDateTextBox.Text.ToString()).ToString("dd-MMM-yyyy"));
        }
        if (!remarksTextBox.Text.Equals(""))
        {
            httable.Add("REMARKS", remarksTextBox.Text.ToString());
        }
        if (!ApprovalDateTextBox.Text.Equals(""))
        {
            httable.Add("APPR_DT", ApprovalDateTextBox.Text.ToString());
        }
        httable.Add("ENTRY_DATE", DateTime.Today.ToString("dd-MMM-yyyy"));
        httable.Add("UserName", LoginName);
        commonGatewayObj.Insert(httable, "CORPORATE_DEC");
        ClearFields();

        DataTable bookCloser = GrirdViewCorporateDeclaration();
        if (bookCloser.Rows.Count > 0)
        {
            dvGridBoolCloser.Visible = true;
            grdShowBookCloser.DataSource = bookCloser;
            grdShowBookCloser.DataBind();
            updateButton.Visible = true;
            ButtonDelete.Visible = true;

            Session["dtBoolCloser"] = bookCloser;

        }
        else
        {
            Session["dtBoolCloser"] = null;

        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Saved Successfully.');", true);

      //  Response.Redirect("CorporateDeclarationSecurities.aspx");

    }



    private void ClearFields()
    {
        companyCodeTextBox.Text = "";
        companyNameDropDownList.SelectedValue = "0";
        typeDropDownList.SelectedValue = "0";
        DropDownListFYPART.SelectedValue = "0";
        ApprovalDateTextBox.Text = "";
        financialYearTextBox.Text = "";
        QuantityTextBox.Text = "";
        recordDateTextBox.Text = "";
        agmDateTextBox.Text = "";
        remarksTextBox.Text = "";
       

    }

    private void searchClearFields()
    {

        financialYearTextBox.Text = "";
        recordDateTextBox.Text = "";
        agmDateTextBox.Text = "";
        remarksTextBox.Text = "";
       

    }

    protected void companyNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();

        if (comp_cd != "")
        {
            companyCodeTextBox.Text = companyNameDropDownList.SelectedValue.ToString();

            DataTable bookCloser = commonGatewayObj.Select(" select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd  order by RECORD_DT DESC) Where comp_cd=" + companyNameDropDownList.SelectedValue.ToString() + "    order by AGM DESC ");

            dvGridBoolCloser.Visible = true;
            grdShowBookCloser.DataSource = bookCloser;
            grdShowBookCloser.DataBind();


        }
        else
        {
            companyCodeTextBox.Text = "";
        }
    }
    protected void typeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        updateButton.Visible = true;
        addNewButton.Visible = true;
        
        searchClearFields();
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();
        string type = typeDropDownList.SelectedValue.ToString();

        if (comp_cd != "0" && type != "0")
        {
           

            DataTable bookCloser = commonGatewayObj.Select(" select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd  order by RECORD_DT DESC) Where comp_cd=" + companyNameDropDownList.SelectedValue.ToString() + "  and type='" + type + "' order by AGM DESC  ");

            dvGridBoolCloser.Visible = true;
            grdShowBookCloser.DataSource = bookCloser;
            grdShowBookCloser.DataBind();


        }
        else if (comp_cd == "0" && type != "0")
        {
            DataTable bookCloser = commonGatewayObj.Select(" select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd  order by RECORD_DT DESC) Where  type='" + type + "' order by AGM DESC  ");

            dvGridBoolCloser.Visible = true;
            grdShowBookCloser.DataSource = bookCloser;
            grdShowBookCloser.DataBind();
        }
        else
        {
            companyCodeTextBox.Text = "";
        }

    }

    protected void clearButton_Click(object sender, EventArgs e)
    {

    }
    protected void ShowButton_Click(object sender, EventArgs e)
    {
        string recordDateFrom = recordDateFromTextBox.Text.ToString();
        string recordDateTo = recordDateToTextBox.Text.ToString();
        string agmDateFrom = agmDateFromTextBox.Text.ToString();
        string agmDateTo  = agmDateToTextBox.Text.ToString();
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();
        string type = typeDropDownList.SelectedValue.ToString();

        StringBuilder sbMst = new StringBuilder();

        sbMst.Append("select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd  order by RECORD_DT DESC) where (QUANTITY IS NOT NULL)  ");

        if ((recordDateFrom != "") && (recordDateTo != ""))
        {
            sbMst.Append(" AND (RECORD_DT BETWEEN '" + recordDateFrom + "' AND '" + recordDateTo + "')");
        }

        if ((agmDateFrom != "") && (agmDateTo == ""))
        {
            sbMst.Append(" AND (AGM >= '" + agmDateFrom + "')");
        }
        else if ((agmDateFrom == "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (AGM <= '" + agmDateTo + "')");
        }
        else if ((agmDateFrom != "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (AGM BETWEEN '" + agmDateFrom + "' AND '" + agmDateTo + "')");
        }

        if (comp_cd != "0" && type == "0")
        {
            sbMst.Append(" AND (COMP_CD= " + comp_cd + ") ");


        }
        else if (comp_cd == "0" && type != "0")
        {
            sbMst.Append(" AND (TYPE= '" + type + "')");
        }
        else if (comp_cd != "0" && type != "0")
        {
            sbMst.Append(" AND (COMP_CD= " + comp_cd + ") AND (TYPE= '" + type + "' ) ");
        }

        sbMst.Append(" order by AGM DESC ");

        DataTable bookCloser = commonGatewayObj.Select(sbMst.ToString());

        if (bookCloser.Rows.Count > 0)
        {
            dvGridBoolCloser.Visible = true;
            grdShowBookCloser.DataSource = bookCloser;
            grdShowBookCloser.DataBind();
            updateButton.Visible = true;
            addNewButton.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('No Data Found');", true);
        }
     
    }
    protected void closeButton_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;

    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

            TableCell companyCode = e.Row.Cells[2];
            TableCell recordDate = e.Row.Cells[5];
            TableCell posted = e.Row.Cells[12];
            string comp_cd = companyCode.Text;
            string record_date = recordDate.Text;

            if (posted.Text == "Y")
            {
                Button button = e.Row.Cells[0].FindControl("btnDettCollez") as Button;
                button.Text = "Posted";
                button.BackColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                DataTable bookCloser = commonGatewayObj.Select("  select * from (Select * from pfolio_bk where comp_cd=" + comp_cd + " and bal_dt_ctrl='" + record_date + "') a inner join fund f on a.f_cd=f.f_cd and f.IS_F_CLOSE is null  ");

                if (bookCloser.Rows.Count > 0)
                {
                    Button button = e.Row.Cells[0].FindControl("btnDettCollez") as Button;
                    button.Text = "Show";
                    button.BackColor = System.Drawing.Color.Green;


                }
                else
                {
                    Button button = e.Row.Cells[0].FindControl("btnDettCollez") as Button;
                    button.Text = "Not Found";
                    

                }

            }

        }

    }


    protected void updateButton_Click(object sender, EventArgs e)
    {
        int countCheck = 0;
        foreach (GridViewRow Drv in grdShowBookCloser.Rows)
        {

            CheckBox leftCheckBox = (CheckBox)grdShowBookCloser.Rows[countCheck].FindControl("leftCheckBox");



            if (leftCheckBox.Checked)
            {
                TextBox Fy = (TextBox)grdShowBookCloser.Rows[countCheck].FindControl("financialYearTextBox");
                TextBox recordDate = (TextBox)grdShowBookCloser.Rows[countCheck].FindControl("recordDateTextBox");              
                TextBox Quantity = (TextBox)grdShowBookCloser.Rows[countCheck].FindControl("QuantityTextBox");
                TextBox AgmDate = (TextBox)grdShowBookCloser.Rows[countCheck].FindControl("agmDateTextBox");
                TextBox ApprovalDate = (TextBox)grdShowBookCloser.Rows[countCheck].FindControl("ApprovalDateTextBox");
                TextBox REMARKS = (TextBox)grdShowBookCloser.Rows[countCheck].FindControl("remarksTextBox");
                TextBox Fy_part = (TextBox)grdShowBookCloser.Rows[countCheck].FindControl("FY_PARTTextBox");

                CorporateBond corbond = new CorporateBond();

               
                 corbond.BOOK_CL_ID = Drv.Cells[1].Text.ToString();
                 corbond.COMP_CD = Drv.Cells[2].Text.ToString();
                 corbond.FY = Drv.Cells[4].Text.ToString();
                 corbond.FY_PART = Fy_part.Text.ToString();
                 corbond.RECORD_DT = recordDate.Text.ToString();
                 corbond.TYPE = Drv.Cells[7].Text.ToString();
                 corbond.QUANTITY = Quantity.Text.ToString();
                 corbond.AGM = AgmDate.Text.ToString();
                 corbond.APPR_DT = ApprovalDate.Text.ToString();
                 corbond.REMARKS = REMARKS.Text.ToString();
                 corbond.ENTRY_DATE = Drv.Cells[12].Text.ToString();
                 EditObject(corbond);

                DataTable bookCloser = GrirdViewCorporateDeclaration();
                if (bookCloser.Rows.Count > 0)
                {
                    dvGridBoolCloser.Visible = true;
                    grdShowBookCloser.DataSource = bookCloser;
                    grdShowBookCloser.DataBind();
                    updateButton.Visible = true;
                    ButtonDelete.Visible = true;

                    Session["dtBoolCloser"] = bookCloser;

                }
                else
                {
                    Session["dtBoolCloser"] = null;

                }
                         

                countCheck++;

            }
        }
         

           
        if (countCheck > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Updated Successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please Check  checkbox');", true);
        }




    }
    protected void btnDettCollez_Click(object sender, EventArgs e)
    {
        string comp_cd = "";
        string type = "";
        string FY = "";
        int countCheck = 0;
        int count = 0;
        int book_cl_id = 0;




        foreach (GridViewRow Drv in grdShowBookCloser.Rows)
        {
            CheckBox leftCheckBox = (CheckBox)grdShowBookCloser.Rows[countCheck].FindControl("leftCheckBox");
           


            if (leftCheckBox.Checked)
            {

                comp_cd = Drv.Cells[2].Text.ToString();
                type = Drv.Cells[7].Text.ToString();
                FY = Drv.Cells[4].Text.ToString();
                book_cl_id=Convert.ToInt32( Drv.Cells[1].Text.ToString());
               
                Session["comp_cd"] = comp_cd;
                Session["type"] = type;
                Session["FY"] = FY;
                Session["book_cl_id"] = book_cl_id;
                count++;
            }          
            countCheck++;           
        }

        if (count == 1)
        {
            if (type == "C")
            {
                Response.Redirect("CorporateDeclarationDetailsCash.aspx");
            }
            else if (type == "B")
            {
                Response.Redirect("CorporateDeclarationDetailsForBonus.aspx");
            }


        }
        else if (count > 1)
        {
            Session["comp_cd"] = null;
            Session["type"] = null;
            Session["FY"] = null;
            Session["book_cl_id"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please Check only one checkbox');", true);
        }
        else
        {
            Session["comp_cd"] = null;
            Session["type"] = null;
            Session["FY"] = null;
            Session["book_cl_id"] = null;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please Check  checkbox');", true);
        }

      
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string loginId = Session["UserID"].ToString();
        if (loginId == "admin")
        {
            int countCheck = 0;
            foreach (GridViewRow Drv in grdShowBookCloser.Rows)
            {
                CheckBox leftCheckBox = (CheckBox)grdShowBookCloser.Rows[countCheck].FindControl("leftCheckBox");



                if (leftCheckBox.Checked)
                {

                    countCheck++;

                    CorporateBond corbond = new CorporateBond();
                    corbond.BOOK_CL_ID = Drv.Cells[1].Text.ToString();
                    corbond.COMP_CD = Drv.Cells[2].Text.ToString();
                    corbond.FY = Drv.Cells[3].Text.ToString();
                    corbond.RECORD_DT = Drv.Cells[4].Text.ToString();
                    corbond.TYPE = Drv.Cells[5].Text.ToString();
                    corbond.QUANTITY = Drv.Cells[6].Text.ToString();
                    corbond.AGM = Drv.Cells[7].Text.ToString();
                    corbond.APPR_DT = Drv.Cells[8].Text.ToString();
                    corbond.REMARKS = Drv.Cells[9].Text.ToString();
                    corbond.ENTRY_DATE = Drv.Cells[10].Text.ToString();
                    DeleteReceivedItem(corbond);

                    DataTable bookCloser = GrirdViewCorporateDeclaration();
                    if (bookCloser.Rows.Count > 0)
                    {
                        dvGridBoolCloser.Visible = true;
                        grdShowBookCloser.DataSource = bookCloser;
                        grdShowBookCloser.DataBind();
                        updateButton.Visible = true;
                        ButtonDelete.Visible = true;

                        Session["dtBoolCloser"] = bookCloser;

                    }
                    else
                    {
                        Session["dtBoolCloser"] = null;

                    }
                }
                countCheck++;
            }

            if (countCheck > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Data Deleted Successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please Check  checkbox');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "alert('Please log in  Admin user');", true);
        }
        

    }



    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string recordDateFrom = recordDateFromTextBox.Text.ToString();
        string recordDateTo = recordDateToTextBox.Text.ToString();
        string agmDateFrom = agmDateFromTextBox.Text.ToString();
        string agmDateTo = agmDateToTextBox.Text.ToString();
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();
        string type = typeDropDownList.SelectedValue.ToString();

        StringBuilder sbMst = new StringBuilder();

        sbMst.Append("select * from (SELECT b.*,a.comp_nm FROM CORPORATE_DEC b inner join comp a on  a.comp_cd=b.comp_cd  order by RECORD_DT DESC) where (QUANTITY IS NOT NULL)  ");

        if ((recordDateFrom != "") && (recordDateTo != ""))
        {
            sbMst.Append(" AND (RECORD_DT BETWEEN '" + recordDateFrom + "' AND '" + recordDateTo + "')");
        }

        if ((agmDateFrom != "") && (agmDateTo == ""))
        {
            sbMst.Append(" AND (AGM >= '" + agmDateFrom + "')");
        }
        else if ((agmDateFrom == "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (AGM <= '" + agmDateTo + "')");
        }
        else if ((agmDateFrom != "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (AGM BETWEEN '" + agmDateFrom + "' AND '" + agmDateTo + "')");
        }

        if (comp_cd != "0" && type == "0")
        {
            sbMst.Append(" AND (COMP_CD= " + comp_cd + ") ");


        }
        else if (comp_cd == "0" && type != "0")
        {
            sbMst.Append(" AND (TYPE= '" + type + "')");
        }
        else if (comp_cd != "0" && type != "0")
        {
            sbMst.Append(" AND (COMP_CD= " + comp_cd + ") AND (TYPE= '" + type + "') ");
        }
        sbMst.Append(" order by AGM DESC ");

        DataTable bookCloser = commonGatewayObj.Select(sbMst.ToString());
        grdShowBookCloser.DataSource = bookCloser;
        grdShowBookCloser.PageIndex = e.NewPageIndex;
        grdShowBookCloser.DataBind();
    } 
    public void DeleteReceivedItem(CorporateBond itemObj)
    {
        commonGatewayObj.DeleteByCommand("CORPORATE_DEC", "BOOK_CL_ID=" + Convert.ToInt32(itemObj.BOOK_CL_ID) + "");
        commonGatewayObj.CommitTransaction();

    }

    public void EditObject(CorporateBond itemObj)
    {

      
        Hashtable htItem = new Hashtable();

        htItem.Add("BOOK_CL_ID", Convert.ToInt32(itemObj.BOOK_CL_ID));
        htItem.Add("COMP_CD", Convert.ToInt32(itemObj.COMP_CD));
        if(itemObj.FY!="")
             htItem.Add("FY", itemObj.FY);
        else
            htItem.Add("FY", DBNull.Value);
        if (itemObj.FY_PART != "")
            htItem.Add("FY_PART", itemObj.FY_PART);
        else
            htItem.Add("FY_PART", DBNull.Value);
        if (itemObj.RECORD_DT != "")
            htItem.Add("RECORD_DT", itemObj.RECORD_DT);
        else
            htItem.Add("RECORD_DT", DBNull.Value);

        if (itemObj.QUANTITY != "")
            htItem.Add("QUANTITY", itemObj.QUANTITY);
        else
            htItem.Add("QUANTITY", DBNull.Value);

        if (itemObj.AGM != "")
            htItem.Add("AGM", itemObj.AGM);
        else
            htItem.Add("AGM", DBNull.Value);

        if (itemObj.APPR_DT != "")
            htItem.Add("APPR_DT", itemObj.APPR_DT);
        else
            htItem.Add("APPR_DT", DBNull.Value);

        if (itemObj.REMARKS != "")
            htItem.Add("REMARKS", itemObj.REMARKS);
        else
            htItem.Add("REMARKS", DBNull.Value);

        commonGatewayObj.Update(htItem, "CORPORATE_DEC", "BOOK_CL_ID=" + Convert.ToInt32(itemObj.BOOK_CL_ID) + "");
        commonGatewayObj.CommitTransaction();


    }

}