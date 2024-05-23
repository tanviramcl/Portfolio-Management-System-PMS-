<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="InvestmentByMFasPerSECrulesReportForm.aspx.cs" Inherits="UI_InvestmentByMFasPerSECrulesReportForm" Title="Investment by the Mutual Funds as per SEC Rules Report Form Page" %>
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
            document.getElementById("<%=assetValueTextBox.ClientID%>").value ="";        
            return false;
        }
        else
        {   
            return true;
        }
            
    }
    
    function fnCheck()
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
        if(document.getElementById("<%=assetValueTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=assetValueTextBox.ClientID%>").focus();
            alert("Please Insert Asset Value.");
            return false; 
        }
        var str1=document.getElementById("<%=fundNameDropDownList.ClientID%>").value;
        var str2=document.getElementById("<%=fundCodeTextBox.ClientID%>").value;
        if(str1 != str2)
        {
            alert("Please Press the Show_Total_Asset Button First.");
            return false;
        }
    }
    
    
</script>
    <style type="text/css">
        .style4
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align ="center">
    <tr>
        <td class="FormTitle" align="center">
            Investment By Mutual Fund As Per SEC Rules Report Form
        </td>           
        <td>
            &nbsp;</td>
    </tr> 
</table>
<br />
<div id="dvUpdatePanel" runat="server">
 <asp:UpdatePanel ID="UpdatePanel" runat="server">
 <ContentTemplate>
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td align="right" style="font-weight: 700"><b>Fund Name:&nbsp; </b></td>
        <td align="left" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="1" ></asp:DropDownList>
        </td>
        
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Portfolio As On:&nbsp; </b></td>
        <td align="left">
            <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server" TabIndex="2"></asp:DropDownList>
            </td>
    </tr>
    
   
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="showTotalAssetButton" runat="server" 
                        CssClass="buttoncommon" Text="Show Total Asset" Width="115px" 
                    OnClientClick=" return fnCheck();" onclick="showTotalAssetButton_Click" TabIndex="3" 
                        />
            <asp:Button ID="showButton" runat="server" Text="Show Report" 
                CssClass="buttoncommon" TabIndex="4" OnClientClick=" return fnCheckInput();" onclick="showButton_Click"
                    />
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnReset();"
                />
            </td>
            
    </tr>
    <tr>
           <td align="center" colspan="4" >
                &nbsp;
           </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Asset Value:&nbsp; </b></td>
        <td class="style4" >
            <asp:TextBox ID="assetValueTextBox" runat="server" TabIndex="6"></asp:TextBox>
        <b>Fund Code:&nbsp; </b>
            <asp:TextBox ID="fundCodeTextBox" runat="server" TabIndex="7" ReadOnly="True" 
                Width="30px"></asp:TextBox>
        </td>
        
    </tr>
    
</table>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="showTotalAssetButton" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="showButton" EventName="Click" />
</Triggers>
</asp:UpdatePanel>
</div>    
</asp:Content>

