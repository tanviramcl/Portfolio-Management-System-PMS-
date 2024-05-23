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
using CrystalDecisions.Shared;
using System.Text;
using System.IO;

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

public partial class ReportViewer_PortfolioPriceAssumptionReportReportViewer : System.Web.UI.Page
{
     
    Message msgObj = new Message();   
    CommonGateway commonGatewayObj = new CommonGateway();
    BaseClass bcContent = new BaseClass();

    private ReportDocument rdoc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DataTable PortfolioPriceAssumption = (DataTable)Session["PortfolioPriceAssumption"];
            string balDate = (string)Session["balDate"];
            string incMarketPricePCT = (string)Session["incMarketPricePCT"];
            string amountInPM = (string)Session["amountInPM"];



            if (PortfolioPriceAssumption.Rows.Count > 0)
            {
                PortfolioPriceAssumption.TableName = "PortfolioPriceAssumption";
                //PortfolioPriceAssumption.WriteXmlSchema(@"F:\GITHUB_PROJECT\DOTNET2015\amclinvanalysisandpfms\UI\ReportViewer\Report\rdlcPortfolioAssumptionReport.xsd");
                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report/PortfolioPriceAssumption.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource _rsource = new ReportDataSource("PortfolioPriceAssumption", PortfolioPriceAssumption);
                ReportViewer1.LocalReport.DataSources.Add(_rsource);
                ReportViewer1.LocalReport.Refresh();

                ReportParameter[] parms = new ReportParameter[3];
                parms[0] = new ReportParameter("asOneDate", balDate);
                parms[1] = new ReportParameter("incMarketPricePCT", incMarketPricePCT);
                parms[2] = new ReportParameter("AMT", amountInPM);
                this.ReportViewer1.LocalReport.SetParameters(parms);
                this.ReportViewer1.LocalReport.Refresh();

                //List<ReportParameter> ListParameters = new List<ReportParameter>();
                //ReportParameter parameter = new ReportParameter();
                //parameter.Name = "asOneDate";
                //parameter.Values.Add(balDate);
                //ReportViewer1.LocalReport.SetParameters(ListParameters);

            }
            else
            {
                Response.Write("No Data Found");
            }


           

        }
    }


    
    protected void Page_Unload(object sender, EventArgs e)
    {
      
    }
}
