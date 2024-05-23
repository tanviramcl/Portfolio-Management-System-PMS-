<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="MaxMinClosingPriceOfFundsReport.aspx.cs" Inherits="UI_MaxMinClosingPriceOfFundsReport" Title="Maximum and Minimum Closing Price of Our Mutual Funds Report" %>

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
            document.getElementById("<%=closingPriceDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=closingPriceDateToTextBox.ClientID%>").value ="";
            document.getElementById("<%=closingPriceDateTextBox.ClientID%>").value ="";            
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
        if(document.getElementById("<%=closingPriceDateFromTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=closingPriceDateFromTextBox.ClientID%>").focus();
            alert("Please Insert Closing Price Date From.");
            return false; 
        }
        if(document.getElementById("<%=closingPriceDateFromTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=closingPriceDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=closingPriceDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=closingPriceDateToTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=closingPriceDateToTextBox.ClientID%>").focus();
            alert("Please Insert Closing Price Date To.");
            return false; 
        }
        if(document.getElementById("<%=closingPriceDateToTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=closingPriceDateToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=closingPriceDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=closingPriceDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=closingPriceDateTextBox.ClientID%>").focus();
            alert("Please Insert Closing Price Date.");
            return false; 
        }
        if(document.getElementById("<%=closingPriceDateTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=closingPriceDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=closingPriceDateTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
    }
</script>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align="center">
    <tr>
        <td class="FormTitle" align="center">
            Maximum and Minimum Closing Price of Our Mutual Funds Report
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
    <tr>
            <td align="left" style="font-weight: 700"  >
                Fiscal Year:
            </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Closing Price Date From:</b></td>
        <td align="left">
            <asp:TextBox ID="closingPriceDateFromTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="closingPriceDateFromTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>
        <td align="right" style="font-weight: 700"><b>Closing Price Date To:</b></td>
        <td align="left">
            <asp:TextBox ID="closingPriceDateToTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="closingPriceDateToTextBox"
                PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" /></td>
    </tr>
    <tr>
            <td align="left" style="font-weight: 700"  >
                Latest Date:
            </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Closing Price Date:</b></td>
        <td align="left">
            <asp:TextBox ID="closingPriceDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="5"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="closingPriceDateTextBox"
                PopupButtonID="ImageButton2" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>
    </tr>
    <tr>
           <td align="center" colspan="4" >
                &nbsp;
           </td>
    </tr>
    <tr>
            <td align="center" colspan="4" >
            <asp:Button ID="showButton" runat="server" Text="Show Report" 
                CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnCheckInput();" onclick="showButton_Click"  
                    />
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="7" OnClientClick=" return fnReset();"
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

