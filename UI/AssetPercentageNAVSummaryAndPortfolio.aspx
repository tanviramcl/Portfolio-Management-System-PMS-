<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="AssetPercentageNAVSummaryAndPortfolio.aspx.cs" Inherits="UI_AssetPercentageNAVSummaryAndPortfolio" Title="Asset Percentage NAV/Summary/Portfolio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=fundNameDropDownList.ClientID%>").value ="0";           
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
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align ="center">
    <tr>
        <td class="FormTitle" align="center">
            Asset Percentage (NAV)/ Summary (Total Asset Value)/ Portfolio (Total Asset Value)
        </td>           
        <td>
            &nbsp;</td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
    <tr>
       <td align="left" style="text-align: right">Statement Type:&nbsp; </td>
        <td align="left">               
           <asp:RadioButton ID="NAVRadioButton" runat="server" Text="Net Asset Value" Checked="true"
                GroupName="statementType" Font-Bold="true" TabIndex="1"  />&nbsp;&nbsp;
                 <asp:RadioButton ID="summTotAsValRadioButton" runat="server" Text="Summary (Total Asset Value)" 
                GroupName="statementType" Font-Bold="true" TabIndex="2"  />
                <asp:RadioButton ID="portTotAsValRadioButton" runat="server" Text="Portfolio (Total Asset Value)" 
                GroupName="statementType" Font-Bold="true" TabIndex="2"  />
                                    </td>
    </tr>
    
    <tr>
        <td align="right" style="font-weight: 700"><b>Fund Name:&nbsp; </b></td>
        <td align="left" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="3"></asp:DropDownList>
        </td>
        
    </tr>
    
    <tr>
        <td align="right" style="font-weight: 700"><b>Portfolio As On:&nbsp; </b></td>
        <td align="left">
            <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
            </td>
    </tr>
    
   
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="showButton" runat="server" Text="Show Report" 
                CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" onclick="showButton_Click" 
                    />
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnReset();"
                />
            </td>
            
    </tr>
    <tr>
           <td align="center" colspan="4" >
                &nbsp;
           </td>
    </tr>
</table>
</asp:Content>

