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

public partial class UI_RiskVARCaluation : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();
        }
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        DataTable dtCLosePrice = new DataTable();
        DataTable Bc =new DataTable();
        dtCLosePrice = commonGatewayObj.Select("select b.INSTR_CD,b.trade_meth,a.* from  (SELECT   comp_cd,    TRAN_DATE,AVG_RT, DSE_RT, CSE_RT " +
        " FROM     MARKET_PRICE  WHERE   TRAN_DATE IN(SELECT MAX(TRAN_DATE) FROM MARKET_PRICE  WHERE(COMP_CD = "+companyNameDropDownList.SelectedValue.ToString()+") AND " +
         "TRAN_DATE BETWEEN '01-jul-2016' AND '30-jun-2021'    GROUP BY TO_CHAR(TRAN_DATE, 'MM-YYYY')) " +
        " AND(COMP_CD = "+companyNameDropDownList.SelectedValue.ToString()+ ") and  TRAN_DATE BETWEEN  '01-Sep-2016' AND '30-Sep-2021'  ) a inner join comp b on a.comp_cd=b.comp_cd  where a.AVG_RT is not null ");

        DataColumn newColRecord_DT = new DataColumn("Record_DT", typeof(string));
         newColRecord_DT.AllowDBNull = true;
        dtCLosePrice.Columns.Add(newColRecord_DT);

        DataColumn newColBONUS = new DataColumn("BONUS", typeof(string));
        newColBONUS.AllowDBNull = true;
        dtCLosePrice.Columns.Add(newColBONUS);

        DataColumn newColCASH = new DataColumn("CASH", typeof(string));
        newColCASH.AllowDBNull = true;
        dtCLosePrice.Columns.Add(newColCASH);


        DataColumn newColAdjustedprice = new DataColumn("Adjustedprice", typeof(string));
        newColAdjustedprice.AllowDBNull = true;
        dtCLosePrice.Columns.Add(newColAdjustedprice);
        double  Adjustedprice = 0;
   
        if (dtCLosePrice.Rows.Count > 0)
        {
            
            for (int loop = 0; loop < dtCLosePrice.Rows.Count; loop++)
            {


                int count = 1;

                DateTime TRAN_DATE = Convert.ToDateTime(dtCLosePrice.Rows[loop]["TRAN_DATE"].ToString());
                string TRAN_DT = TRAN_DATE.ToString("dd-MMM-yyyy");
                string[] TRAN_DT_ARR = TRAN_DT.Split('-');
                Adjustedprice = Convert.ToDouble(dtCLosePrice.Rows[loop]["AVG_RT"]);


                //Get the number of days in the current month
                int daysInMonth = DateTime.DaysInMonth(TRAN_DATE.Year, TRAN_DATE.Month);

                //First day of the month is always 1
                var firstDay = new DateTime(TRAN_DATE.Year, TRAN_DATE.Month, 1).Day;

                //Last day will be similar to the number of days calculated above
                var lastDay = new DateTime(TRAN_DATE.Year, TRAN_DATE.Month, daysInMonth).Day;



                string DATEFrom = " ";
                string DATETO = " ";

                DATEFrom = firstDay + "-" + TRAN_DT_ARR[1].ToString() + "-" + TRAN_DT_ARR[2].ToString();
                DATETO = lastDay + "-" + TRAN_DT_ARR[1].ToString() + "-" + TRAN_DT_ARR[2].ToString();

                Bc = commonGatewayObj.Select("select * from BOOK_CL where RECORD_DT  BETWEEN '" + DATEFrom + "' AND '" + DATETO + "' and comp_cd=115 ");


                if (Bc.Rows.Count > 0)
                {

                    DateTime Record_DT = Convert.ToDateTime(Bc.Rows[0]["RECORD_DT"].ToString());
                    string Record_Date = Record_DT.ToString("dd-MMM-yyyy"); ;
                    string BONUS = Bc.Rows[0]["BONUS"].ToString();
                    string CASH = Bc.Rows[0]["CASH"].ToString();



                    dtCLosePrice.Rows[loop]["Record_DT"] = Record_Date;
                    dtCLosePrice.Rows[loop]["BONUS"] = BONUS;
                    dtCLosePrice.Rows[loop]["CASH"] = CASH;

                    




                }
               
                dtCLosePrice.Rows[loop]["Adjustedprice"] = Adjustedprice;

            }
        }

        Session["dtCLosePrice"] = dtCLosePrice;
        Session["comp_name"] = companyNameDropDownList.SelectedItem.Text.ToString();

        Response.Redirect("ReportViewer/SecuritiesadjustedpriceWithDividendReportViewer.aspx");


    }

    }
