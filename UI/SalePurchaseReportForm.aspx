<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="SalePurchaseReportForm.aspx.cs" Inherits="UI_SalePurchaseReportForm" Title="Sale Purchase Report Form Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=howlaDateToTextBox.ClientID%>").value ="";  
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
        var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
        if(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=howlaDateFromTextBox.ClientID%>").focus();
            alert("Please Insert Date From.");
            return false; 
        }
        if(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=howlaDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=howlaDateToTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=howlaDateToTextBox.ClientID%>").focus();
            alert("Please Insert Date To.");
            return false; 
        }
        if(document.getElementById("<%=howlaDateToTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=howlaDateToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=howlaDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
            alert("Please Select Fund Name.");
            return false; 
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table style="text-align:center">
    <tr>
        <td class="FormTitle" align="center">
            Sale Purchase Report Form
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" style="text-align:center" cellpadding="0" cellspacing="0" border="0">
    
    <tr>
        <td align="right" style="font-weight: 700"><b>From Date:&nbsp;&nbsp; </b></td>
        <td align="left">
            <asp:TextBox ID="howlaDateFromTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="howlaDateFromTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" />&nbsp;(DD-MMM-YYYY)</td>
        
    </tr>
    <tr>
        <td align="right" style="font-weight: 700">To <b>Date:&nbsp;&nbsp; </b></td>
        <td align="left">
            <asp:TextBox ID="howlaDateToTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="howlaDateToTextBox"
                PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;(DD-MMM-YYYY)</td>
    </tr>
    
    <tr>
        <td align="right" style="font-weight: 700"><b>Fund Name:&nbsp;&nbsp; </b></td>
        <td align="left" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
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

