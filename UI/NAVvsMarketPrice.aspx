<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="NAVvsMarketPrice.aspx.cs" Inherits="UI_NAVvsMarketPrice" Title="NAV Vs Market Price" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            height: 7px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript"> 

    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=marketPriceTextBox.ClientID%>").value =""; 
            document.getElementById("<%=navDateTextBox.ClientID%>").value ="";         
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

        if(document.getElementById("<%=navDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=navDateTextBox.ClientID%>").focus();
            alert("Please Insert NAV Date.");
            return false; 
        }
        if(document.getElementById("<%=navDateTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=navDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=navDateTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=marketPriceTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=marketPriceTextBox.ClientID%>").focus();
            alert("Please Insert Market Price Date.");
            return false; 
        }
        if(document.getElementById("<%=marketPriceTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=marketPriceTextBox.ClientID%>").value))
            {
                document.getElementById("<%=marketPriceTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=diffBetnSellRepurchaseUnitTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=diffBetnSellRepurchaseUnitTextBox.ClientID%>").focus();
            alert("Please Enter Amount of Difference between Sell & Repurchase.");
            return false;
            
        }
        if(document.getElementById("<%=diffBetnSellRepurchaseUnitTextBox.ClientID%>").value !="")
        {
            var digitCheck = /\d+\.?\d*/;
            if(!digitCheck.test(document.getElementById("<%=diffBetnSellRepurchaseUnitTextBox.ClientID%>").value))
            {
                document.getElementById("<%=diffBetnSellRepurchaseUnitTextBox.ClientID%>").focus();
                alert("Please Enter Valid Number of Diff. between Sell & Repurchase TextBox.");
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
            NAV Vs Market Price
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
   
    <tr>
            <td align="right" style="font-weight: 700" class="style4"><b>NAV Date:</b></td>
            <td align="left" class="style4">
                <asp:TextBox ID="navDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="navDateTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" 
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" />
                <asp:Label ID="Label1" runat="server" style="font-weight: 700" 
                    Text="(dd-mmm-yyyy)"></asp:Label>
            </td>
    </tr>
    <tr>
            <td align="right" style="font-weight: 700"><b>Market Price Date:</b></td>
            <td align="left">
                <asp:TextBox ID="marketPriceTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="marketPriceTextBox"
                PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />
                <asp:Label ID="Label2" runat="server" style="font-weight: 700" 
                    Text="(dd-mmm-yyyy)"></asp:Label>
            </td>
    </tr>
    <tr>
            <td align="right" style="font-weight: 700"><b>Diff. of Sell & Repurchase Unit Cert:</b></td>
            <td align="left">
                <asp:TextBox ID="diffBetnSellRepurchaseUnitTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="5" ReadOnly="True" >5</asp:TextBox>
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
                CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnCheckInput();" onclick="showButton_Click" 
                />
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="7" OnClientClick=" return fnReset();" 
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



