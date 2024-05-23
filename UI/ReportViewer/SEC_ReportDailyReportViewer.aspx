<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SEC_ReportDailyReportViewer.aspx.cs" Inherits="UI_ReportViewer_SEC_ReportDailyReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Report to SEC Page</title>
</head>
<body>
    <form id="dailyReportSEC" runat="server">
    <div>
            <CR:CrystalReportViewer ID="CRV_SEC_Daily" runat="server" 
            AutoDataBind="True" Height="50px" 
            Width="350px" />
    </div>
    </form>
</body>
</html>
