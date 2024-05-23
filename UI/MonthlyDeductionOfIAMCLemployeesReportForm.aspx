<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="MonthlyDeductionOfIAMCLemployeesReportForm.aspx.cs" Inherits="UI_MonthlyDeductionOfIAMCLemployeesReportForm" Title="Monthly Deduction Report Form" %>
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
            document.getElementById("<%=monthOfDeductionDropDownList.ClientID%>").value ="0";           
            return false;
        }
        else
        {   
            return true;
        }       
    }
    function fnCheckInput()
    {   
        if(document.getElementById("<%=monthOfDeductionDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=monthOfDeductionDropDownList.ClientID%>").focus();
            alert("Please Select Month of Deduction.");
            return false; 
        }
    }
</script>
    
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align="center">
    <tr>
        <td class="FormTitle" align="center">
            Monthly Deduction From IAMCL Employees Salary</td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
   <tr>
        <td align="right"><asp:RadioButton ID="welfareFund" runat="server" Text="Benevolent Fund" Checked="true"
                    GroupName="DeductionType" Font-Bold="true"  /></td>
                    
        <td align="left"><asp:RadioButton ID="groupInsurance" runat="server" Text="Group Insurance"  
                    GroupName="DeductionType" Font-Bold="true"  /></td>
    </tr>
    <tr>
            <td align="right" style="font-weight: 700"><b>Month of Deduction:</b></td>
            <td align="left" >
            <asp:DropDownList ID="monthOfDeductionDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
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

