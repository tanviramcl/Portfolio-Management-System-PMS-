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
using System.Text;
using CrystalDecisions.CrystalReports.Engine;

public partial class UI_ReportViewer_CloseEndNavPaperCuttingReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    Pf1s1DAO pf1s1DAOObj = new Pf1s1DAO();
    ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }

        string letterPrintDateCr = "";
        string paperPublishedDateCr = "";
        string signatoryId = "";

        if (Request.QueryString["letterPrintDate"] != "")
        {
            letterPrintDateCr = Request.QueryString["letterPrintDate"].ToString();
        }
        if (Request.QueryString["publishedDate"] != "")
        {
            paperPublishedDateCr = Request.QueryString["publishedDate"].ToString();
        }
        int letterToCr = Convert.ToInt32(Request.QueryString["letterTo"]);
        int paperId = Convert.ToInt32(Request.QueryString["paperName"]);
        signatoryId = Convert.ToString(Request.QueryString["signatory"]);
        int fundCodeCr = Convert.ToInt32(Request.QueryString["fundName"]);

        DataTable dtPaperInfo = pf1s1DAOObj.GetPaperInfo(paperId);
        string nameOfPaper = dtPaperInfo.Rows[0][0].ToString();//name of Paper

        DataTable dtSignatoryInfo = pf1s1DAOObj.GetSignatoryInfo(signatoryId);
        string nameOfSignatory = dtSignatoryInfo.Rows[0][0].ToString();//name of Signatory
        string designationOfSignatory = dtSignatoryInfo.Rows[0][1].ToString();//Designation of Signatory
        string designationShortOfSignatory = dtSignatoryInfo.Rows[0][2].ToString();//Short Designation of Signatory

        DataTable dtReportToPerson = new DataTable();
        StringBuilder sbMstToPerson = new StringBuilder();
        StringBuilder sbfilterToPerson = new StringBuilder();
        sbfilterToPerson.Append(" ");
        sbMstToPerson.Append("SELECT NAME, DESIGNATION, ORGANIZATION_NAME, ADDRESS_1, ADDRESS_2, ADDRESS_3, ADDRESS_4, DESPATCH_NO, ATTENTION ");
        sbMstToPerson.Append("FROM NAV_LETTER.TO_PERSON ");
        //sbMstToPerson.Append("WHERE(1 = 1) ");

        if (letterToCr >= 1)
        {
            sbfilterToPerson.Append(" WHERE ID =" + letterToCr);
        }
        sbMstToPerson.Append(sbfilterToPerson.ToString());
        sbMstToPerson.Append(" ORDER BY ID");
        dtReportToPerson = commonGatewayObj.Select(sbMstToPerson.ToString());
        dtReportToPerson.TableName = "To_Person";

        string nameOfPerson = dtReportToPerson.Rows[0]["NAME"].ToString();
        string designationOfPerson = dtReportToPerson.Rows[0]["DESIGNATION"].ToString();
        string organizationOfPerson = dtReportToPerson.Rows[0]["ORGANIZATION_NAME"].ToString();
        string address_1OfPerson = dtReportToPerson.Rows[0][3].ToString();
        string address_2OfPerson = dtReportToPerson.Rows[0][4].ToString();
        string address_3OfPerson = dtReportToPerson.Rows[0][5].ToString();
        string address_4OfPerson = dtReportToPerson.Rows[0][6].ToString();
        string despatchNoOfPerson = dtReportToPerson.Rows[0][7].ToString();
        string attentionOfPerson = dtReportToPerson.Rows[0][8].ToString();

        DataTable dtReprtSource1 = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");
        sbMst.Append("SELECT ID, F_CD, REF_NO, FACE_VALUE, F_NM, LETTER_REF_NO, LETTER_REF_DATE ");
        sbMst.Append("FROM NAV_LETTER.LETTER_REFERENCE ");
        sbMst.Append("WHERE (F_CD NOT IN (4)) ");
        
        if (fundCodeCr != 0)
        {
            sbfilter.Append(" AND (F_CD =" + fundCodeCr + ")");
        }
        else
        {
            sbfilter.Append(" AND (F_CD >= 3) AND (F_CD < 27) ");
        }
        sbMst.Append(sbfilter.ToString());
        sbMst.Append(" ORDER BY F_CD");

        dtReprtSource1 = commonGatewayObj.Select(sbMst.ToString());
        dtReprtSource1.TableName = "CloseEndNAVpaperCutting";

            if (dtReprtSource1.Rows.Count > 0)
                {
                    string Path = "";
                    //dtReprtSource1.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\CloseEndNAVpaperCuttingReport.xsd");
                   
                    Path = Server.MapPath("Report/CloseEndNAVpaperCuttingReport.rpt");
                    rdoc.Load(Path);
                    rdoc.SetDataSource(dtReprtSource1);
                    
                    CrystalReportViewerPaperCuttingNAV.ReportSource = rdoc;
                    rdoc.SetParameterValue("prmLetterPrintDate", letterPrintDateCr);
                    rdoc.SetParameterValue("prmNameOfPerson", nameOfPerson);
                    rdoc.SetParameterValue("prmDesignationOfPerson", designationOfPerson);
                    rdoc.SetParameterValue("prmOrganizationOfPerson", organizationOfPerson);
                    rdoc.SetParameterValue("prmAddress_1OfPerson", address_1OfPerson);
                    rdoc.SetParameterValue("prmAddress_2OfPerson", address_2OfPerson);
                    rdoc.SetParameterValue("prmAddress_3OfPerson", address_3OfPerson);
                    rdoc.SetParameterValue("prmAddress_4OfPerson", address_4OfPerson);
                    rdoc.SetParameterValue("prmDespatchNoOfPerson", despatchNoOfPerson);
                    rdoc.SetParameterValue("prmAttentionOfPerson", attentionOfPerson);
                    rdoc.SetParameterValue("prmPaperName", nameOfPaper);
                    rdoc.SetParameterValue("prmPaperPublishedDate", paperPublishedDateCr);
                    rdoc.SetParameterValue("prmNameOfSignatory", nameOfSignatory);
                    rdoc.SetParameterValue("prmDesignationOfSignatory", designationOfSignatory);
                    rdoc.SetParameterValue("prmDesignationShortOfSignatory", designationShortOfSignatory);
                    rdoc = ReportFactory.GetReport(rdoc.GetType());
                    CrystalReportViewerPaperCuttingNAV.DisplayToolbar = true;
                    CrystalReportViewerPaperCuttingNAV.HasExportButton = true;
                    CrystalReportViewerPaperCuttingNAV.HasPrintButton = true;
                }
                else
                {
                    Response.Write("No Data Found");
                }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CrystalReportViewerPaperCuttingNAV.Dispose();
        CrystalReportViewerPaperCuttingNAV = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
