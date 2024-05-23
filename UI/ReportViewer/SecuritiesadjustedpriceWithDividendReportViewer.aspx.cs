using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_SecuritiesadjustedpriceWithDividendReportViewer : System.Web.UI.Page
{
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        DataTable dtSecuritiesadjustedpriceWithDividendt = (DataTable)Session["dtCLosePrice"];
        string CompanY_name= (string)Session["comp_name"];


        //calculation of adjusted price
        Double AdjustedPrice = 0;

        //for (int loop = 0; loop < dtSecuritiesadjustedpriceWithDividendt.Rows.Count; loop++)
        //{

        //    if (dtSecuritiesadjustedpriceWithDividendt.Rows[loop]["BONUS"] == DBNull.Value)
        //    {
        //        AdjustedPrice = Convert.ToDouble(dtSecuritiesadjustedpriceWithDividendt.Rows[loop]["Adjustedprice"]) / 1 ;
        //        dtSecuritiesadjustedpriceWithDividendt.Rows[loop]["Adjustedprice"] = AdjustedPrice;
        //    }
        //    else
        //    {
        //        AdjustedPrice = Convert.ToDouble(dtSecuritiesadjustedpriceWithDividendt.Rows[loop]["Adjustedprice"]) / 1 + (Convert.ToDouble(dtSecuritiesadjustedpriceWithDividendt.Rows[loop]["BONUS"]) / 100);
        //        dtSecuritiesadjustedpriceWithDividendt.Rows[loop]["Adjustedprice"] = AdjustedPrice;
        //    }

        //}





            //dtSecuritiesadjustedpriceWithDividendt.WriteXmlSchema(@"D:\Development\RISK Managment\amclinvanalysisandpfms\UI\ReportViewer\Report\xsdSecuritiesadjustedpriceWithDividend.xsd");
            if (dtSecuritiesadjustedpriceWithDividendt.Rows.Count > 0)
        {
            string Path = Server.MapPath("Report/crSecuritiesadjustedpriceWithDividend.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dtSecuritiesadjustedpriceWithDividendt);

            rdoc.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, true, dtSecuritiesadjustedpriceWithDividendt.Rows[0]["comp_cd"].ToString()+"_" + CompanY_name +"_"+ DateTime.Now);

            Response.End();
        }
    }
}