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


public partial class UI_ReportViewer_CloseEndNAVLetterReportViewer : System.Web.UI.Page
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

        string letterTypeCr = Request.QueryString["navLetterType"].ToString();
        string letterPrintDateCr = "";
        string signatoryId = "";

            if (Request.QueryString["letterPrintDate"] != "")
            {
                letterPrintDateCr = Request.QueryString["letterPrintDate"].ToString();
            }

            int letterToCr = Convert.ToInt32(Request.QueryString["letterTo"]);
            signatoryId = Convert.ToString(Request.QueryString["signatory"]);
            int fundCodeCr = Convert.ToInt32(Request.QueryString["fundName"]);

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
            //sbMstToPerson.Append("WHERE(1 <> 1) ");

            if (letterToCr >= 1)
            {
                sbfilterToPerson.Append(" WHERE ID =" + letterToCr);
            }
            sbMstToPerson.Append(sbfilterToPerson.ToString());
            sbMstToPerson.Append(" ORDER BY ID");
            dtReportToPerson = commonGatewayObj.Select(sbMstToPerson.ToString());
            dtReportToPerson.TableName = "To_Person";

            int noOfLetterTo = dtReportToPerson.Rows.Count;


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
                sbMst.Append("SELECT NAV.NAV_MASTER.NAVNO, NAV.NAV_MASTER.NAVFUNDID, NAV.NAV_MASTER.NAVDATE, NAV.NAV_MASTER.NAVWORKINGDATE, ");
                sbMst.Append("NAV.NAV_MASTER.NAVISPUBLISHNAV, NAV.NAV_MASTER.NAVLASTEDITBY, NAV.NAV_MASTER.NAVTOTALNOOFUNIT, ");
                sbMst.Append("NAV.NAV_MASTER.NAVLASTVAULTVALUE, NAV.NAV_MASTER.NAVTOTALMARKETPRICE, NAV.NAV_MASTER.NAVTOTALCOSTPRICE, NAV.NAV_MASTER.NOOFDAYSALCAL, ");
                sbMst.Append("NAV.NAV_MASTER.ADD_LESS, ROUND(NAV.NAV_MASTER.NAVTOTALMARKETPRICE / NAV.NAV_MASTER.NAVTOTALNOOFUNIT,2) AS NAV_PER_UNIT_MARKETPRICE, ");
                sbMst.Append("ROUND(NAV.NAV_MASTER.NAVTOTALCOSTPRICE / NAV.NAV_MASTER.NAVTOTALNOOFUNIT,2) AS NAV_PER_UNIT_COST, ");
                sbMst.Append("NAV_LETTER.LETTER_REFERENCE.REF_NO, NAV_LETTER.LETTER_REFERENCE.FACE_VALUE, NAV_LETTER.LETTER_REFERENCE.F_NM  ");
                sbMst.Append("FROM NAV.NAV_MASTER INNER JOIN ");
                sbMst.Append("NAV_LETTER.LETTER_REFERENCE ON NAV.NAV_MASTER.NAVFUNDID = NAV_LETTER.LETTER_REFERENCE.F_CD ");
                sbMst.Append("WHERE (NAV.NAV_MASTER.NAVFUNDID NOT IN (4)) ");

                if (fundCodeCr != 0)
                {
                    sbfilter.Append(" AND (NAV.NAV_MASTER.NAVFUNDID =" + fundCodeCr + ")");
                }
                else
                {
                    sbfilter.Append(" AND (NAV.NAV_MASTER.NAVFUNDID >= 3) AND (NAV.NAV_MASTER.NAVFUNDID < 27) ");
                }

                if (Request.QueryString["navDate"] != "")
                {
                    sbfilter.Append(" AND  (NAV.NAV_MASTER.NAVDATE ='" + Convert.ToDateTime(Request.QueryString["navDate"]).ToString("dd-MMM-yyyy") + "')");
                }

                sbMst.Append(sbfilter.ToString());
                sbMst.Append(" ORDER BY NAV.NAV_MASTER.NAVFUNDID");

                dtReprtSource1 = commonGatewayObj.Select(sbMst.ToString());
                dtReprtSource1.TableName = "CloseEndNAVreport";

            
                
                if (letterTypeCr == "navLetter")//navLetter
                {
                    if (dtReprtSource1.Rows.Count > 0)
                    {
                        string Path = "";
                        //dtReprtSource1.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmCloseEndNAVreport.xsd");
                        ReportDocument rdoc = new ReportDocument();
                        if (letterToCr == 3)
                        {
                            Path = Server.MapPath("Report/CloseEndNAVchairmanAMCLReport.rpt");
                        }
                        else
                        {
                            Path = Server.MapPath("Report/CloseEndNAVReport.rpt");
                        }
                        rdoc.Load(Path);
                        rdoc.SetDataSource(dtReprtSource1);
                        CrystalReportViewerNAVletter.ReportSource = rdoc;
                        
                        //rdoc.SetParameterValue("prmFY", FY);
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

                        rdoc.SetParameterValue("prmNameOfSignatory", nameOfSignatory);
                        rdoc.SetParameterValue("prmDesignationOfSignatory", designationOfSignatory);
                        rdoc.SetParameterValue("prmDesignationShortOfSignatory", designationShortOfSignatory);
                        rdoc = ReportFactory.GetReport(rdoc.GetType());
                        CrystalReportViewerNAVletter.DisplayToolbar = true;
                        CrystalReportViewerNAVletter.HasExportButton = true;
                        CrystalReportViewerNAVletter.HasPrintButton = true;
                    }
                    else
                    {
                        Response.Write("No Data Found");
                    }
                }//End of navLetter
                else//pressRelease
                {
                    if (dtReprtSource1.Rows.Count > 0)
                    {
                        // dtReprtSource1.WriteXmlSchema(@"F:\PortfolioManagementSystem\UI\ReportViewer\Report\crtmCloseEndNAVpressRelease.xsd");

                       
                        string Path = Server.MapPath("Report/CloseEndNAVpressRelease.rpt");
                        rdoc.Load(Path);
                        rdoc.SetDataSource(dtReprtSource1);
                        CrystalReportViewerNAVletter.ReportSource = rdoc;
                        CrystalReportViewerNAVletter.DisplayToolbar = true;
                        CrystalReportViewerNAVletter.HasExportButton = true;
                        CrystalReportViewerNAVletter.HasPrintButton = true;
                        //rdoc.SetParameterValue("prmFY", FY);
                        rdoc.SetParameterValue("prmLetterPrintDate", letterPrintDateCr);
                        //rdoc.SetParameterValue("prmNameOfPerson", nameOfPerson);
                        //rdoc.SetParameterValue("prmDesignationOfPerson", designationOfPerson);
                        //rdoc.SetParameterValue("prmOrganizationOfPerson", organizationOfPerson);
                        //rdoc.SetParameterValue("prmAddress_1OfPerson", address_1OfPerson);
                        //rdoc.SetParameterValue("prmAddress_2OfPerson", address_2OfPerson);
                        //rdoc.SetParameterValue("prmAddress_3OfPerson", address_3OfPerson);
                        //rdoc.SetParameterValue("prmAddress_4OfPerson", address_4OfPerson);
                        //rdoc.SetParameterValue("prmDespatchNoOfPerson", despatchNoOfPerson);
                        //rdoc.SetParameterValue("prmAttentionOfPerson", attentionOfPerson);

                        rdoc.SetParameterValue("prmNameOfSignatory", nameOfSignatory);
                        rdoc.SetParameterValue("prmDesignationOfSignatory", designationOfSignatory);
                        rdoc.SetParameterValue("prmDesignationShortOfSignatory", designationShortOfSignatory);
                    }
                    else
                    {
                        Response.Write("No Data Found");
                    }
                }//End of pressRelease
            //}//end of for loop
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        CrystalReportViewerNAVletter.Dispose();
        CrystalReportViewerNAVletter = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}
