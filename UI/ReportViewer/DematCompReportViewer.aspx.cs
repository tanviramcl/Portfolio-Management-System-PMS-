using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_StockDeclarationBeforePostedReportViewer : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    string strSQL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }

        
        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");


        string Fromdate = "";
        string Todate = "";
        string fundCodes = "";
        string companycode = "";
        string CompanyName ="";



        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            Fromdate = (string)Session["Fromdate"];
            Todate = (string)Session["Todate"];
            fundCodes = (string)Session["fundCodes"];
            companycode = (string)Session["companycode"];
            CompanyName = (string)Session["CompanyName"];


        }
        strSQL = "select  a.f_cd, b.f_name, a.folio_no, a.cert_no, a.dmat_no, a.dmat_dt, a.allot_no, a.dis_no_fm,a.dis_no_to, a.no_shares, a.sp_date, substr(a.sh_type,1,1) sh_tp,  a.posted" +
                " from shr_dmat_fi  a, fund b where a.comp_cd = '"+companycode+"'and a.f_cd =b.f_cd and a.posted is null and a.dmat_dt between '"+Fromdate+"' and '"+Todate+"' and a.f_cd IN(" + fundCodes + ") and a.f_cd not in(3,5,18)   " +
                " order by  a.dmat_dt, a.dmat_no, c_dt, cert_no";

      
        dtReprtSource = commonGatewayObj.Select(strSQL);
        dtReprtSource.TableName = "DematCompReport";
      //  dtReprtSource.WriteXmlSchema(@"D:\officialProject\4-5-2017\amclpmfs\UI\ReportViewer\Report\CR_DematComp.xsd");
        if (dtReprtSource.Rows.Count > 0)
        {
            string Path = Server.MapPath("Report/crpdematCompReport.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_DematComp.ReportSource = rdoc;
            rdoc.SetParameterValue("prmCompanyName", CompanyName);
            rdoc.SetParameterValue("Fromdate", Fromdate);
            rdoc.SetParameterValue("Todate", Todate);
            rdoc = ReportFactory.GetReport(rdoc.GetType());
        }
        else
        {
            Response.Write("No Data Found");
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_DematComp.Dispose();
        CR_DematComp = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }
}