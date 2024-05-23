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
using System.IO;

public partial class UI_AMCLCommon : System.Web.UI.MasterPage
{
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    if (Request.UserAgent.Contains("AppleWebKit"))
    //        Request.Browser.Adapters.Clear();
    //}
    protected string text = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/ui/test.txt");
        TextReader reader = File.OpenText(path);
        text = reader.ReadToEnd();

        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
        string loginId = Session["UserID"].ToString();
        string LoginName = Session["UserName"].ToString();
        string userType = Session["UserType"].ToString();
        //mnuMenu.Items.Clear();
        ////string LoginName = Session["UserName"].ToString();
        //lblLoginName.Text = "Welcome" + "  " + "to" + " " + LoginName.ToString();
        //skmMenu.MenuItem item;
        //skmMenu.MenuItem Subitem;
        ////skmMenu.MenuItem Subitem1;

        //if (userType == "A")
        //{
        //    //Menu for Entry
        //    item = new skmMenu.MenuItem("Entry");
        //    Subitem = new skmMenu.MenuItem("Fund Transaction Entry");
        //    Subitem.Url = "FundTransactionEntry.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Book Closer Entry");
        //    Subitem.Url = "BookCloserEntry.aspx";
        //    item.SubItems.Add(Subitem);
            
        //    Subitem = new skmMenu.MenuItem("Non Listed Securities Entry");
        //    Subitem.Url = "NonListedSecuritiesInvestmentEntryForm.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("DSE Howla Entry");
        //    Subitem.Url = "HowlaDSEentryForm.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("CSE Howla Entry");
        //    Subitem.Url = "HowlaDSEentryForm.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Upload File");
        //    Subitem.Url = "PortfolioFileUpload.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Update Market Price");
        //    Subitem.Url = "PortfolioMPUpdate.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Howla Update");
        //    Subitem.Url = "PortfolioHowlaUpdate.aspx";
        //    item.SubItems.Add(Subitem);


        //    Subitem = new skmMenu.MenuItem("Date Wise Transaction");
        //    Subitem.Url = "DateWiseTransaction.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Balance Update Process");
        //    Subitem.Url = "BalanceUpdateProcess.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Final Processing");
        //    Subitem.Url = "FinalProcessing.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Company Information Entry");
        //    Subitem.Url = "CompanyInformation.aspx";
        //    item.SubItems.Add(Subitem);

        //    //Subitem = new skmMenu.MenuItem("Recent Market Information");
        //    //Subitem.Url = "RecentMarketInformation.aspx";
        //    //item.SubItems.Add(Subitem);
        //    //item.SubItems.Add(Subitem);


        //    //Process Menu
        //    //item = new skmMenu.MenuItem("Process");
        //    //item.Url = "Process.aspx";


        //    mnuMenu.Items.Add(item);
        //}

        ////Report Menu
        //item = new skmMenu.MenuItem("Reports");

        //Subitem = new skmMenu.MenuItem("Sale-Purchase Report");
        //Subitem.Url = "SalePurchaseReportForm.aspx";
        //item.SubItems.Add(Subitem);
        ////mnuMenu.Items.Add(item);

        //Subitem = new skmMenu.MenuItem("Fund Transaction Report");
        //Subitem.Url = "FundTransactionReport.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Book Closer Report");
        //Subitem.Url = "BookCloserReport.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Company Wise Securites Transaction Report");
        //Subitem.Url = "CompanyWiseSecuritiesTransaction.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Company Wise All Portfolios Report(DSE Only)");
        //Subitem.Url = "CompanyWiseAllPortfoliosReportDSEonly.aspx";
        //item.SubItems.Add(Subitem);
        
        //Subitem = new skmMenu.MenuItem("Portfolio With NonListed Securities");
        //Subitem.Url = "PortfolioWithNonListedSecurities.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Portfolio With Profit/Loss");
        //Subitem.Url = "PortfolioStatementWithProfitLoss.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Market Valiation With Profit/Loss");
        //Subitem.Url = "MarketValuationWithProfitLoss.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Market Valiation With Non Listed Securites Company/All Funds");
        //Subitem.Url = "MarketValuationWithNonListedSecuritesCompanyAndAllFunds.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Asset Percentage NAV/Summary/Portfolio");
        //Subitem.Url = "AssetPercentageNAVSummaryAndPortfolio.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Portfolio Summary (Sector Wise)");
        //Subitem.Url = "PortfolioSummaryForm.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Portfolio Summary In Pie Chart (Sector Wise)");
        //Subitem.Url = "PortFolioSummaryInPieChart.aspx";
        //item.SubItems.Add(Subitem);


        //Subitem = new skmMenu.MenuItem("Total Asset Percetage Check");
        //Subitem.Url = "AssetPercentageCheck.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Share Reconciliation Report");
        //Subitem.Url = "CompanyWiseShareReconciliationReport.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Receivable Cash Dividend Report");
        //Subitem.Url = "ReceivableCashDividend.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Max and Min Closing Price of ALL Funds");
        //Subitem.Url = "MaxMinClosingPriceOfFundsReport.aspx";
        //item.SubItems.Add(Subitem);
        ////mnuMenu.Items.Add(item);

        //Subitem = new skmMenu.MenuItem("Balance Check");
        //Subitem.Url = "BalanceCheckReport.aspx";
        //item.SubItems.Add(Subitem);
        ////mnuMenu.Items.Add(item);

      
        ////Subitem = new skmMenu.MenuItem("Test Report");
        ////Subitem.Url = "testFundTransactionHBReport.aspx";
        ////item.SubItems.Add(Subitem);

        //mnuMenu.Items.Add(item);

        //item = new skmMenu.MenuItem("More Reports");

        //Subitem = new skmMenu.MenuItem("Non Demate Shares Check");
        //Subitem.Url = "NonDemateSharesCheckReport.aspx";
        //item.SubItems.Add(Subitem);
        ////mnuMenu.Items.Add(item);

        //Subitem = new skmMenu.MenuItem("Negative Balance Check");
        //Subitem.Url = "NegativeBalanceCheckReport.aspx";
        //item.SubItems.Add(Subitem);
        ////mnuMenu.Items.Add(item);
        //Subitem = new skmMenu.MenuItem("Stock Declaration Before Posted");
        //Subitem.Url = "StockDeclarationBeforePostedReport.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Stock Declaration After Posted");
        //Subitem.Url = "StockDeclarationAfterPostedReport.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Capital Gain of Funds ");
        //Subitem.Url = "CapitalGainAllFundsReport.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("PortFolio CategoryWise");
        //Subitem.Url = "PortFolioCategoryWise.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Portfilio Demat Share");
        //Subitem.Url = "PortfilioDematShare.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Portfilio NonDemat Share");
        //Subitem.Url = "PortfilioNonDematShare.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Capital Gain Summary DateWise");
        //Subitem.Url = "CapitalGainSummeryDateWise.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Capital Gain Individual Fund (Company Wise)");
        //Subitem.Url = "CapitalGainCompanyWiseNew.aspx";
        //item.SubItems.Add(Subitem);



        //Subitem = new skmMenu.MenuItem("Capital Gain (Company Wise)");
        //Subitem.Url = "CapitalGainCompanyWise.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Company Wise Capital Gain Summary Descending");
        //Subitem.Url = "CapitalGainSummeryCompanyWiseDsc.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Sell Buy Check");
        //Subitem.Url = "SellBuyCheck.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Portfolio Quarterwise Report");
        //Subitem.Url = "PortFolioQuaterWise.aspx";
        //item.SubItems.Add(Subitem);


        //mnuMenu.Items.Add(item);
        /////////////////////

        ////Report Menu
        ////item = new skmMenu.MenuItem("Report");
        ////Subitem = new skmMenu.MenuItem("General Report");
        ////Subitem.Url = "GeneralReport.aspx";
        ////item.SubItems.Add(Subitem);
        ////mnuMenu.Items.Add(item);


        ////reports Accounce
        //// C
        //item = new skmMenu.MenuItem("Reports(Accounts)");

        //Subitem = new skmMenu.MenuItem("Receivable/Payable(DSE and CSE Separate)");
        //Subitem.Url = "ReceivablePayableDSEandCSESeparate.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Sell Buy Fundwise Reconcilation");
        //Subitem.Url = "ReceivablePayableFundWise.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Reconcilation DR/CR");
        //Subitem.Url = "ReconcilationDrandCR.aspx";
        //item.SubItems.Add(Subitem);


        //Subitem = new skmMenu.MenuItem("Capital Gain Summary Stock Wise");
        //Subitem.Url = "CapitalGainSummaryStockWise.aspx";
        //item.SubItems.Add(Subitem);

        //Subitem = new skmMenu.MenuItem("Statement Of Profit On Sale Of Investment");
        //Subitem.Url = "StatementOfProfitOnSaleOfInvestment.aspx";
        //item.SubItems.Add(Subitem);

        //mnuMenu.Items.Add(item);

        ////Report to SEC Menue
        //item = new skmMenu.MenuItem("SEC Report");
        //    Subitem = new skmMenu.MenuItem("Daily");
        //    Subitem.Url = "DailyReportToSEC.aspx";
        //    item.SubItems.Add(Subitem);
        //    Subitem = new skmMenu.MenuItem("Weekly");
        //    Subitem.Url = "WeeklyReportToSEC.aspx";
        //    item.SubItems.Add(Subitem);
        //    Subitem = new skmMenu.MenuItem("Quarterly");
        //    Subitem.Url = "QuarterlyReportToSEC.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Investment by the MF as per SEC Rules");
        //    Subitem.Url = "InvestmentByMFasPerSECrulesReportForm.aspx";
        //    item.SubItems.Add(Subitem);
        
        //    Subitem = new skmMenu.MenuItem("Sec Info Summary");
        //    Subitem.Url = "SecInfosummary.aspx";
        //    item.SubItems.Add(Subitem);
           
        //    Subitem = new skmMenu.MenuItem("Sec Invesment Sector Wise Details");
        //    Subitem.Url = "SecInvesmentSectorwiseDetails.aspx";
        //    item.SubItems.Add(Subitem);
        //    mnuMenu.Items.Add(item);
            
        ////NAV Menue
        //    item = new skmMenu.MenuItem("NAV");
        //    Subitem = new skmMenu.MenuItem("Close End NAV Letter");
        //    Subitem.Url = "CloseEndNAVLetterReport.aspx";
        //    item.SubItems.Add(Subitem);
        //    Subitem = new skmMenu.MenuItem("Paper Cutting of Close End MF");
        //    Subitem.Url = "CloseEndNAVpaperCutting.aspx";
        //    item.SubItems.Add(Subitem);
        //    mnuMenu.Items.Add(item);

        //    //Salary Menue
        //    item = new skmMenu.MenuItem("Salary");
        //    Subitem = new skmMenu.MenuItem("Monthly Bank Advice(Salary)");
        //    Subitem.Url = "BankAdvice.aspx";
        //    item.SubItems.Add(Subitem);
        //    Subitem = new skmMenu.MenuItem("Monthly Deduction of IAMCL Employees Report");
        //    Subitem.Url = "MonthlyDeductionOfIAMCLemployeesReportForm.aspx";
        //    item.SubItems.Add(Subitem);
        //    Subitem = new skmMenu.MenuItem("Selection Scale 2009-2010");
        //    Subitem.Url = "ReportViewer/SelectionScaleCalculation.aspx";
        //    item.SubItems.Add(Subitem);
        //    mnuMenu.Items.Add(item);
        //    //psdr menu
        //    item = new skmMenu.MenuItem("MOMS");
        //    Subitem = new skmMenu.MenuItem("Monthly Bank Advice(Salary)");
        //    Subitem.Url = "BankAdvice.aspx";
        //    item.SubItems.Add(Subitem);
        //    Subitem = new skmMenu.MenuItem("Company Information For CDS");
        //    Subitem.Url = "CompanyInformationForCDS.aspx";
        //    item.SubItems.Add(Subitem);
        
        //    Subitem = new skmMenu.MenuItem("Demat Comp");
        //    Subitem.Url = "DematComp.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Demat List");
        //    Subitem.Url = "DematList.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Single Demat(Fund)");
        //    Subitem.Url = "SingleDemat.aspx";
        //    item.SubItems.Add(Subitem);

        //    Subitem = new skmMenu.MenuItem("Group Demat(Fund)");
        //    Subitem.Url = "GroupDemat.aspx";
        //    item.SubItems.Add(Subitem);

        //mnuMenu.Items.Add(item);


        ////Logout
        //   item = new skmMenu.MenuItem("Logout");
        //    item.Url = "../Default.aspx";
        //    mnuMenu.Items.Add(item);
    }
}
