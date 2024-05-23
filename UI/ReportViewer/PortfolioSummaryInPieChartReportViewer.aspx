<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortfolioSummaryInPieChartReportViewer.aspx.cs" Inherits="UI_ReportViewer_PortfolioSummaryReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Portfolio Summary Report Viewer Page</title>
</head>
<body>
    <form id="formPortfolioSummary" runat="server">
    <div>
            <CR:CrystalReportViewer ID="CRV_PortfolioSummary" runat="server" 
            AutoDataBind="true" 
             ToolbarImagesFolderUrl="\aspnet_client\system_web\2_0_50727\CrystalReportWebFormViewer4\images\toolbar\"  />
    </div>
    </form>
</body>
</html>
