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
using CrystalDecisions.CrystalReports.Engine;
using System.Text;
using CrystalDecisions.Shared;

public partial class UI_ReportViewer_ReceivableCashDividendReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbFilter = new StringBuilder();
        string recordDateFrom = "";
        string recordDateTo = "";
        string agmDateFrom = "";
        string agmDateTo = "";
        string fundCode = "";
        string comp_cd = "";
        string statementType = "";
        string rF = "";

        DataTable dtIntimationReport = new DataTable();

        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            fundCode = (string)Session["fundCode"];
            comp_cd = (string)Session["compcd"];
            recordDateFrom = (string)Session["recordDateFrom"];
            recordDateTo = (string)Session["recordDateTo"];
            agmDateFrom = (string)Session["agmDateFrom"];
            agmDateTo = (string)Session["agmDateTo"];
            statementType= (string)Session["statementType"];
            rF = (string)Session["rF"];

        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        sbMst.Append("   select tab2.*, C.FY, C.FY_PART, C.RECORD_DT, C.TYPE,   C.QUANTITY, C.AGM, C.REMARKS, C.APPR_DT, C.POSTED, C.PDATE,  ");
        sbMst.Append("    C.ENTRY_DATE As CD_ENTRY from (select tab1.*,f.f_name from (select a.*,c.comp_nm,c.FC_VAL from  BOOK_CL_DETAILS  a  ");
        sbMst.Append("     inner join  comp C  on  a.comp_cd=c.comp_cd  ) tab1  inner join   fund  f on tab1.f_cd=f.F_cd   ) tab2 inner join  ");
        sbMst.Append("       CORPORATE_DEC c on tab2.BOOK_CL_ID=c.BOOK_CL_ID   Where c.TYPE='C'    ");


        if ((recordDateFrom != "") && (recordDateTo != ""))
        {
            sbMst.Append("  And c.RECORD_DT between '" + recordDateFrom + "' AND '" + recordDateTo + "'");
            
        }

        if ((agmDateFrom != "") && (agmDateTo == ""))
        {
            sbMst.Append(" AND c.AGM >= '" + agmDateFrom + "'");
        }
        else if ((agmDateFrom == "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND c.AGM <= '" + agmDateTo + "'");
        }
        else if ((agmDateFrom != "") && (agmDateTo != ""))
        {
            sbMst.Append(" AND c.AGM BETWEEN '" + agmDateFrom + "' AND '" + agmDateTo + "'");
        }

        if (fundCode != "0")
        {
            sbMst.Append(" AND tab2.F_CD = " + Convert.ToInt16(fundCode.ToString()) + "");
        }


        if (comp_cd != "0")
        {
            sbMst.Append(" AND c.comp_cd = " + Convert.ToInt16(comp_cd.ToString()) + "");
        }


        if (statementType == "Recevied")
        {
            sbMst.Append(" AND PAYMENT_DATE is not null AND POSTED ='Y' ");
        }
        if (statementType == "Pending")
        {
            sbMst.Append(" AND PAYMENT_DATE is  null  ");
        }

        sbMst.Append(" order by AGM ASC  ");
        dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
        if (dtReprtSource.Rows.Count > 0)
        {
            dtReprtSource.TableName = "crtDividendStatus";
          //  dtReprtSource.WriteXmlSchema(@"D:\Development\PFMS_LIVE\PMCS_SERVER\PFMS\amclinvanalysisandpfms\UI\ReportViewer\Report\crtDividendStatus.xsd");
            string Path = "";
            Path = Server.MapPath("Report/crtDividendStatus.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            rdoc.SetParameterValue("recordDateFrom", agmDateFrom);
            rdoc.SetParameterValue("prmRecordDateTo", agmDateTo);
           

            ExportFormatType formatType = ExportFormatType.NoFormat;
            switch (rF)
            {
                case "Word":
                    formatType = ExportFormatType.WordForWindows;
                    break;
                case "PDF":
                    formatType = ExportFormatType.PortableDocFormat;
                    break;
                case "Excel":
                    formatType = ExportFormatType.ExcelWorkbook;
                    break;
                case "CSV":
                    formatType = ExportFormatType.CharacterSeparatedValues;
                    break;
            }

            rdoc.ExportToHttpResponse(formatType, Response, true, "crtDividendStatus_" + DateTime.Now);
        }
        else
        {
            Response.Write("No Data Found");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
