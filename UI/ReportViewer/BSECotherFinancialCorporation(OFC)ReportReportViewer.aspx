<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BSECotherFinancialCorporation(OFC)ReportReportViewer.aspx.cs" Inherits="UI_ReportViewer_CapitalGainAllFundsReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="CapitalGainAllFundsReportViewer" runat="server">
    
    <div>
            <CR:CrystalReportViewer ID="CR_BSECotherFinancialCorporation" runat="server" 
            AutoDataBind="true" 
             ToolbarImagesFolderUrl="\aspnet_client\system_web\2_0_50727\CrystalReportWebFormViewer4\images\toolbar\"  />
    </div>
      
   
    </form>
</body>
</html>
