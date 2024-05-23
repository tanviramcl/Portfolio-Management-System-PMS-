<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="WeeklyReportToSEC.aspx.cs" Inherits="UI_WeeklyReportToSEC" Title="Weekly Transaction Report Form Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=weekEndDateTextBox.ClientID%>").value ="";           
            return false;
        }
        else
        {   
            return true;
        }       
    }
    function fnCheckInput()
    {   
        if(document.getElementById("<%=weekEndDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=weekEndDateTextBox.ClientID%>").focus();
            alert("Please Select Week End Date.");
            return false; 
        }
        if(document.getElementById("<%=weekEndDateTextBox.ClientID%>").value!="")
        {
            var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=weekEndDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=weekEndDateTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align="center">
    <tr>
        <td class="FormTitle" align="center">
            Weekly Transaction Report to SEC Form
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
   
    <tr>
            <td align="right" style="font-weight: 700"><b>Week End Date:</b></td>
            <td align="left" >
            <asp:TextBox ID="weekEndDateTextBox" runat="server" Width="80px"></asp:TextBox>
            <asp:ImageButton ID="weekEndDateTextBoxImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" 
                runat="server" Format="dd-MMM-yyyy" PopupButtonID="weekEndDateTextBoxImageButton" 
                TargetControlID="weekEndDateTextBox"></ajaxToolkit:CalendarExtender>
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
                CssClass="buttoncommon" TabIndex="10" OnClientClick=" return fnCheckInput();" 
                    onclick="showButton_Click"/>
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

