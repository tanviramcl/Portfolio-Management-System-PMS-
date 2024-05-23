using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        Session["CompInfo"] = GetAllCompInfo();

        DataTable dtCompInfo = (DataTable)Session["CompInfo"];



        //  companyNameTextBox.Text = "sss";
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {



    }

    private DataTable GetAllCompInfo()
    {
        DataTable dtCompInfo = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" SELECT a.COMP_CD, a.COMP_NM, a.SECT_MAJ_CD,a.INSTR_CD, a.CAT_TP,a. ATHO_CAP, a.PAID_CAP, a.NO_SHRS, ");
        sbMst.Append(" a.FC_VAL,a.TRADE_METH,a.flag,a.MARKETTYPE,a.AVG_RT,a.RT_UPD_DT,a.ADC_RT,a.DSE_HIGH,a.DSE_OPEN,a.DSE_LOW, a.ISADD_HOWLACHARGE_DSE,a.ADD_HOWLACHARGE_AMTDSE,a.EXCEP_BUYSL_COMPCT_DSE,B.SECT_MAJ_NM FROM COMP a ");
        sbOrderBy.Append(" inner join SECT_MAJ b on A.SECT_MAJ_CD =B.SECT_MAJ_CD ORDER BY COMP_CD ");

        sbMst.Append(sbOrderBy.ToString());

        dtCompInfo = commonGatewayObj.Select(sbMst.ToString());

        Session["CompInfo"] = dtCompInfo;
        return dtCompInfo;
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyInformation.aspx");
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddFund.aspx");
    }
}