<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="SalePurchaseSummaryReopot.aspx.cs" Inherits="UI_SalePurchaseSummaryReopot" Title="Sale-Purchase Summary Report Page" %>
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
            document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=howlaDateToTextBox.ClientID%>").value ="";       
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
            alert("Please Insert Howla Date From.");
            return false; 
        }
        if(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=howlaDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=howlaDateToTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=howlaDateToTextBox.ClientID%>").focus();
            alert("Please Insert Howla Date To.");
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
        
    }
</script>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align ="center">
    <tr>
        <td class="FormTitle" align="center">
            Sale-Purchase Summary Report Form
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td align="right" style="font-weight: 700"><b>Howla Date From:</b></td>
        <td align="left">
            <asp:TextBox ID="howlaDateFromTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="howlaDateFromTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>
        
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Howla Date To:</b></td>
        <td align="left">
            <asp:TextBox ID="howlaDateToTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="howlaDateToTextBox"
                PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" /></td>
    </tr>
        <tr>
            <td align="right">Statement Type:&nbsp; </td>

            <td align="left">
                      <asp:RadioButton ID="SectorWiseallRadioButton" runat="server" Text="Sector Wise All"
                    GroupName="statementType" Font-Bold="true" AutoPostBack="true" TabIndex="2"  Checked="true"/>
                <asp:RadioButton ID="fundwiseRadioButton" runat="server" Text="Fund Wise"
                    GroupName="statementType" Font-Bold="true" TabIndex="1" AutoPostBack="true" />&nbsp;&nbsp;
          
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
