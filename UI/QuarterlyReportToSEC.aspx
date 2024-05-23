<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="QuarterlyReportToSEC.aspx.cs" Inherits="UI_QuarterlyReportToSEC" Title="Quarterly Report Page" %>
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
            document.getElementById("<%=quarterStartDateTextBox.ClientID%>").value =""; 
            document.getElementById("<%=quarterEndDateDropDownList.ClientID%>").value ="0";       
            return false;
        }
        else
        {   
            return true;
        }
            
    }
    
    function fnCheckInput()
    {

         if(document.getElementById("<%=DropDownListTrustee.ClientID%>").value =="0")
        {
            document.getElementById("<%=DropDownListTrustee.ClientID%>").focus();
            alert("Please Select a Trustee");
            return false; 
        }
        var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
        if(document.getElementById("<%=quarterStartDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=quarterStartDateTextBox.ClientID%>").focus();
            alert("Please Insert Quarter Start Date.");
            return false; 
        }
        if(document.getElementById("<%=quarterStartDateTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=quarterStartDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=quarterStartDateTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=quarterEndDateDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=quarterEndDateDropDownList.ClientID%>").focus();
            alert("Please Select Howla Date.");
            return false; 
        }
      
    }
</script>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align ="center">
    <tr>
        <td class="FormTitle" align="center">
            Quarterly Report Form
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
<%-- <tr>
        <td align="right" style="font-weight: 700"><b>Report Type:</b></td>
        <td align="left">
            <asp:RadioButton ID="BSECRadioButton" runat="server" Checked="True" 
                GroupName="Type" Text="BESEC" />
            <asp:RadioButton ID="ICBTRusteeRadioButton" runat="server" GroupName="Type" 
                Text="ICB Trustee" />
            <asp:RadioButton ID="ICBCapitalTrusteeRadioButton" runat="server" 
                GroupName="Type" Text="Capital Trustee" />
            </td>
    </tr>--%>

    <tr>
         <td align="right" style="font-weight: 700">Institution/Trustee<b>:</b></td>
        <td align="left">
            <asp:DropDownList ID="DropDownListTrustee" runat="server" TabIndex="8"></asp:DropDownList>
            </td>
     </tr>
    <tr>
        <td align="right" style="font-weight: 700">Quarter S<b>tart Date:</b></td>
        <td align="left">
            <asp:TextBox ID="quarterStartDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="quarterStartDateTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>
        
    </tr>
    <tr>
        <td align="right" style="font-weight: 700">Quarter End Date<b>:</b></td>
        <td align="left">
            <asp:DropDownList ID="quarterEndDateDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
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

