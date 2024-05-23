<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="Process.aspx.cs" Inherits="UI_Process" Title="Process Execute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            color: #3333CC;
        }
        .style5
        {
            color: #3333CC;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
    function fnCheckInput()
    {   
        if(document.getElementById("<%=closingPriceDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=closingPriceDateTextBox.ClientID%>").focus();
            alert("Please Select Closing Price Date.");
            return false;   
        }
        if(document.getElementById("<%=closingPriceDateTextBox.ClientID%>").value!="")
        {
            var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=closingPriceDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=closingPriceDateTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=closingPriceDateTextBox.ClientID%>").value ==document.getElementById("<%=updateTillTextBox.ClientID%>").value)
        {
            alert("Closing Price Already Updated!!");
            return false;   
        }
    }
    function fnRefresh()
    {
       document.getElementById("<%=updatePriceButton.ClientID%>").click();
    }
</script>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
        &nbsp;&nbsp;&nbsp;
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           EXECUTE PROCESS FORM
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      <br />
      <table width="900" align="center", cellpadding="0", cellspacing="0", border="0">
        <colgroup width="450"></colgroup>
        <colgroup width="450"></colgroup>
        <tr>
            <td align="right" style="font-weight: 700" class="style5">Closing Price Update Till:</td>
            <td align="left" class="style4" >
                <asp:TextBox ID="updateTillTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" Enabled="False" ForeColor="Blue" ></asp:TextBox>
            </td>
        </tr>
        <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Closing Price Date:</b></td>
            <td align="left" >
                <asp:TextBox ID="closingPriceDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" >
                </asp:TextBox>
                <asp:ImageButton ID="ImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="closingPriceDateTextBox" 
                 PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
                <asp:Button ID="updatePriceButton" runat="server" Text="Update Price" 
                CssClass="buttoncommon" OnClientClick=" return fnCheckInput();" onclick="updatePriceButton_Click"/>
            </td>
        </tr>
        <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
        </tr>
      </table>
      
</asp:Content>

