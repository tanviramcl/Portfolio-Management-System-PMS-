<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PortfolioStatementWithtAssumption.aspx.cs" Inherits="UI_PortfolioStatementWithtAssumption" Title="Portfolio With  Assumption Report Form Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value = "0"; 
             document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=pricePCTTextBox.ClientID%>").value ="0";           
            return false;
        }
        else
        {   
            return true;
        }
            
    }
    
    function fnCheckInput()
    {   
        if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
            alert("Please Select Fund Name.");
            return false; 
        }
        if(document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").focus();
            alert("Please Select Date.");
            return false; 
        }
        if(document.getElementById("<%=pricePCTTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=pricePCTTextBox.ClientID%>").focus();
            alert("Please Enter a MP PCT value.");
            return false; 
        }
    }
</script>
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
        .auto-style2 {
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align ="center">
    <tr>
        <td class="FormTitle" align="center">
            Portfolio Statement With Assumption Report Form
        </td>           
        <td>
            &nbsp;</td>
    </tr> 
</table>
<br />
<table width="1000" align="center" cellpadding="0" cellspacing="0" border="0">
  
    <tr>
        <td align="right" style="font-weight: 700"><b>Fund Name:&nbsp; </b></td>
        <td align="left" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="3"></asp:DropDownList>
        </td>
        
    </tr>
    
    <tr>
        <td align="right" style="font-weight: 700" class="auto-style1"><b>Portfolio As On:&nbsp; </b></td>
        <td align="left" class="auto-style1">
            <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
            </td>
    </tr>
     <tr>
        <td align="right" style="font-weight: 700"><b>Increased&nbsp;&nbsp; Market
&nbsp;Price (%):&nbsp; </b></td>
        <td align="left">
                      
                <asp:TextBox ID="pricePCTTextBox" runat="server" Width="71px">5</asp:TextBox>
            
            </td>
    </tr>
    <tr>
       <td align="left" style="text-align: right"><span class="auto-style2"><strong>Fund Size at:</strong></span>&nbsp; </td>
        <td align="left">               
           <asp:RadioButton ID="costPriceRadioButton" runat="server" Text="Cost Price" Checked="true"
                GroupName="fundSizeType" Font-Bold="true" TabIndex="1"  />&nbsp;&nbsp;
                 <asp:RadioButton ID="marketPriceRadioButton" runat="server" Text=" Market Price" 
                GroupName="fundSizeType" Font-Bold="true" TabIndex="2"  />
    </td>
    </tr>
    <tr>
       <td align="left" style="text-align: right"><span class="auto-style2"><strong>&nbsp;Amount in:</strong></span>&nbsp; </td>
        <td align="left">               
           <asp:RadioButton ID="corereRadioButton" runat="server" Text="Crore" Checked="true"
                GroupName="amountType" Font-Bold="true" TabIndex="1"  />&nbsp;&nbsp;
                 <asp:RadioButton ID="lacRadioButton" runat="server" Text="Lac" 
                GroupName="amountType" Font-Bold="true" TabIndex="2"  />
    </td>
    </tr>
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="showButton" runat="server" Text="Print Report" 
                CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" onclick="showButton_Click" 
                    />
            &nbsp;
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnReset();"
                />
            </td>
            
    </tr>
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
     
</table>
</asp:Content>

