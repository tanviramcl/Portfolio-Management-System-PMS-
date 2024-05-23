<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortfolioPriceAssumptionReportReportViewer.aspx.cs" Inherits="ReportViewer_PortfolioPriceAssumptionReportReportViewer" Title="Price Assumption Report Viewer For ALL(Design and Developed by Sakhawat)" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unit Holder Registration Information (Design and Developed by Sakhawat)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </div>
       
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Report\PortfolioPriceAssumption.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
       
    </form>
</body>
</html>
