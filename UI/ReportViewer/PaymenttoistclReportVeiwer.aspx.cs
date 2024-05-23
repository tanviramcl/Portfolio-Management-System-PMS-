using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_ReportViewer_NonDemateSharesCheckReportViwer : System.Web.UI.Page
{

    CommonGateway commonGatewayObj = new CommonGateway();
    private ReportDocument rdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

        string Fromdate = "";
        string Todate = "";
        string bankcodes = "";


        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../../Default.aspx");
        }
        else {
            Fromdate = (string)Session["Fromdate"];
            Todate = (string)Session["Todate"];
            bankcodes= (string)Session["bankcodes"];



        }

        DataTable dtReprtSource = new DataTable();
        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbfilter = new StringBuilder();
        sbfilter.Append(" ");

       
            sbMst.Append("  select e.F_CD,e.F_NAME,e.Receivable,e.BN_DET_ID,e.BN_CD,e.BANK_ACC_NUMBER,e.BN_NAME,f.BR_CD,f.BR_NAME,f.BR_ADDRESS,f.BR_ROUTING from (select d.F_CD,d.F_NAME,d.Receivable,d.BN_DET_ID,d.BN_CD,d.BANK_ACC_NUMBER, ");
            sbMst.Append("  b.BN_NAME from (select c.F_CD,c.F_NAME,c.Receivable,d.BN_DET_ID,d.BN_CD,d.BANK_ACC_NUMBER  from (select a.F_CD,f.F_NAME,a.Receivable from (select F_CD,sum(Receivable) as Receivable from  (select * from (select t.f_cd,vch_dt,f.f_name fund_name,stock_ex,sum(decode(TRAN_TP,'C',AMT_AFT_COM,0)) buy, ");
            sbMst.Append("     sum(decode(TRAN_TP,'S',AMT_AFT_COM,0)) Sale,sum(decode(TRAN_TP,'C', -AMT_AFT_COM,'S',AMT_AFT_COM,0))  Receivable from  invest.fund_trans_hb t ,invest.fund f  ");
            sbMst.Append("where vch_dt between '" + Fromdate + "' and '" + Todate + "'   and tran_tp<>'B' and no_share>=1 and stock_ex in ('C','D') and f.f_cd=t.f_cd group by t.f_cd,f.f_name,  vch_dt,stock_ex order by  t.vch_dt,f_cd) a where a.Receivable>0) group by F_CD ");
            sbMst.Append("     order by F_CD asc) a inner join invest.fund f on f.F_cd=a.F_CD order by f_cd asc) c inner join invest.BANK_DETAILS d on c.F_CD=d.F_CD ) d inner join invest.bank b on d.BN_CD=b.BN_CD) e inner join invest.BANK_BRANCH f on e.BN_CD=f.BN_CD where e.BN_CD= " + bankcodes + "");

            sbMst.Append(sbfilter.ToString());
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "PaymentToISTCL";
         //   dtReprtSource.WriteXmlSchema(@"D:\AMCLPFMS_Invest_110718\amclpmfs\UI\ReportViewer\Report\CR_PaymentToISTCLReportVeiwerReport.xsd");

          
        
        if (dtReprtSource.Rows.Count > 0 )
        {
            DataTable dtTotalAmount = new DataTable();
            StringBuilder querySring = new StringBuilder();
            querySring.Append("  select sum(Receivable) as TOTAL_AMOUNT from (select e.F_CD,e.F_NAME,e.Receivable,e.BN_DET_ID,e.BN_CD,e.BANK_ACC_NUMBER,e.BN_NAME,f.BR_CD,f.BR_NAME,f.BR_ADDRESS,f.BR_ROUTING from (select d.F_CD,d.F_NAME,d.Receivable,d.BN_DET_ID,d.BN_CD,d.BANK_ACC_NUMBER, ");
            querySring.Append("  b.BN_NAME from (select c.F_CD,c.F_NAME,c.Receivable,d.BN_DET_ID,d.BN_CD,d.BANK_ACC_NUMBER  from (select a.F_CD,f.F_NAME,a.Receivable from (select F_CD,sum(Receivable) as Receivable from  (select * from (select t.f_cd,vch_dt,f.f_name fund_name,stock_ex,sum(decode(TRAN_TP,'C',AMT_AFT_COM,0)) buy, ");
            querySring.Append("     sum(decode(TRAN_TP,'S',AMT_AFT_COM,0)) Sale,sum(decode(TRAN_TP,'C', -AMT_AFT_COM,'S',AMT_AFT_COM,0))  Receivable from  invest.fund_trans_hb t ,invest.fund f  ");
            querySring.Append("where vch_dt between '" + Fromdate + "' and '" + Todate + "'   and tran_tp<>'B' and no_share>=1 and stock_ex in ('C','D') and f.f_cd=t.f_cd group by t.f_cd,f.f_name,  vch_dt,stock_ex order by  t.vch_dt,f_cd) a where a.Receivable>0) group by F_CD ");
            querySring.Append("     order by F_CD asc) a inner join invest.fund f on f.F_cd=a.F_CD order by f_cd asc) c inner join invest.BANK_DETAILS d on c.F_CD=d.F_CD ) d inner join invest.bank b on d.BN_CD=b.BN_CD) e inner join invest.BANK_BRANCH f on e.BN_CD=f.BN_CD where e.BN_CD= " + bankcodes + ")");
            dtTotalAmount = commonGatewayObj.Select(querySring.ToString());
            dtTotalAmount.TableName = "TotalAmount";

            NumberToEnglish numberToEnnglishObj = new NumberToEnglish();
            decimal totalAmount = Convert.ToDecimal(dtTotalAmount.Rows[0]["TOTAL_AMOUNT"]);
            string totalAmountInWords = numberToEnnglishObj.changeNumericToWords(totalAmount);
            string Path = "";
            if (bankcodes == "13")
            {
                Path = Server.MapPath("Report/crtmPaymentToISTCL.rpt");
            }
            else
            {
                Path = Server.MapPath("Report/crtmPaymentToISTCLOTHERS.rpt");
            }
            
            rdoc.Load(Path);
            rdoc.SetDataSource(dtReprtSource);
            CR_PaymentToISTCLeReport.ReportSource = rdoc;
            CR_PaymentToISTCLeReport.DisplayToolbar = true;
            CR_PaymentToISTCLeReport.HasExportButton = true;
            CR_PaymentToISTCLeReport.HasPrintButton = true;
            //rdoc.SetParameterValue("Fromdate", Fromdate);
            //rdoc.SetParameterValue("Todate", Todate);
            rdoc.SetParameterValue("prmtotalAmount", totalAmount);
            rdoc.SetParameterValue("prmtotalAmountInWords", totalAmountInWords);
            rdoc = ReportFactory.GetReport(rdoc.GetType());

        }
        else
        {
            Response.Write("No Data Found");
        }
       
        }



    protected void Page_Unload(object sender, EventArgs e)
    {
        CR_PaymentToISTCLeReport.Dispose();
        CR_PaymentToISTCLeReport = null;
        rdoc.Close();
        rdoc.Dispose();
        rdoc = null;
        GC.Collect();
    }


}