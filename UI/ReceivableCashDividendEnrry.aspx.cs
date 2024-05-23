using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UI_ReceivableCashDividend : System.Web.UI.Page
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

        DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
        if (!IsPostBack)
        {
            fundNameDropDownList.DataSource = dtFundNameDropDownList;
            fundNameDropDownList.DataTextField = "F_NAME";
            fundNameDropDownList.DataValueField = "F_CD";
            fundNameDropDownList.DataBind();


        }
        Session["dtReceivableCashDividend"] = null;

        DataTable dtReceivableCashDividend = (DataTable)Session["dtReceivableCashDividend"];
    }
    protected void showButton_Click(object sender, EventArgs e)
    {
        string recordDateFrom = recordDateFromTextBox.Text.ToString();
        string recordDateTo = recordDateToTextBox.Text.ToString();
        string agmDateFrom = agmDateFromTextBox.Text.ToString();
        string agmDateTo = agmDateToTextBox.Text.ToString();
        string fundCode = fundNameDropDownList.SelectedValue.ToString();

        DataTable dtReceivableCashDividend = new DataTable();


        StringBuilder sbMst = new StringBuilder();

        sbMst.Append(" SELECT     FUND.F_CD,FUND.F_NAME,COMP.COMP_CD, COMP.COMP_NM, BOOK_CL.AGM, BOOK_CL.RECORD_DT, COMP.FC_VAL, BOOK_CL.CASH, ");
        sbMst.Append(" COMP.FC_VAL * BOOK_CL.CASH / 100 AS DIVIDEND_PER_SHARE, PFOLIO_BK.TOT_NOS,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH AS GROSS_DIVIDEND, decode(FUND.F_CD, 1, ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .2, 0) AS TAX,  decode(FUND.F_CD, 1,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH * .8,  ");
        sbMst.Append(" PFOLIO_BK.TOT_NOS * COMP.FC_VAL / 100 * BOOK_CL.CASH) AS NET_DIVIDEND ");
        sbMst.Append(" FROM         BOOK_CL INNER JOIN ");
        sbMst.Append(" COMP ON BOOK_CL.COMP_CD = COMP.COMP_CD INNER JOIN ");
        sbMst.Append(" FUND INNER JOIN ");
        sbMst.Append(" PFOLIO_BK ON FUND.F_CD = PFOLIO_BK.F_CD ON BOOK_CL.COMP_CD = PFOLIO_BK.COMP_CD AND ");
        sbMst.Append(" BOOK_CL.RECORD_DT = PFOLIO_BK.BAL_DT_CTRL ");
        sbMst.Append(" WHERE       (BOOK_CL.CASH IS NOT NULL) ");

        if ((recordDateFrom != "") && (recordDateTo != ""))
        {
            sbMst.Append(" AND (BOOK_CL.RECORD_DT BETWEEN '" + recordDateFrom + "' AND '" + recordDateTo + "')");
        }

        if ((agmDateFrom != "") && (agmDateTo == ""))
        {
            sbMst.Append(" AND (BOOK_CL.AGM >= '" + agmDateFrom + "')");
        }
        else if ((agmDateFrom == "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (BOOK_CL.AGM <= '" + agmDateTo + "')");
        }
        else if ((agmDateFrom != "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND (BOOK_CL.AGM BETWEEN '" + agmDateFrom + "' AND '" + agmDateTo + "')");
        }

        if (fundCode != "0")
        {
            sbMst.Append(" AND (FUND.F_CD = " + Convert.ToInt16(fundCode.ToString()) + ")");
        }
        sbMst.Append(" ORDER BY 2 ");

        dtReceivableCashDividend = commonGatewayObj.Select(sbMst.ToString());


        dtReceivableCashDividend = commonGatewayObj.Select(sbMst.ToString());

        Session["dtReceivableCashDividend"] = dtReceivableCashDividend;
    }
    protected void resetButton_Click(object sender, EventArgs e)
    {

        Response.Redirect("ReceivableCashDividendEnrry.aspx");
    }
}
