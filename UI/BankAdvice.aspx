<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="BankAdvice.aspx.cs" Inherits="UI_BankAdvice" Title="Bank Advice Form" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=monthOfBankAdviceDropDownList.ClientID%>").value ="0";           
            return false;
        }
        else
        {   
            return true;
        }       
    }
    function fnCheckInput()
    {   
        if(document.getElementById("<%=monthOfBankAdviceDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=monthOfBankAdviceDropDownList.ClientID%>").focus();
            alert("Please Select Month of Bank Advice.");
            return false; 
        }
    }
</script>
    
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align="center">
    <tr>
        <td class="FormTitle" align="center">
            Bank Advice Report
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
   
    <tr>
            <td align="right" style="font-weight: 700"><b>Month of Bank Advice:</b></td>
            <td align="left" >
            <asp:DropDownList ID="monthOfBankAdviceDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
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
                CssClass="buttoncommon" TabIndex="10" OnClientClick=" return fnCheckInput();" onclick="showButton_Click" 
                />
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="11" OnClientClick=" return fnReset();" 
                />
            </td>
            
    </tr>
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
</table>      
</asp:Content>

