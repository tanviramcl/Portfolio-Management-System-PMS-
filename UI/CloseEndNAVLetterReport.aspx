<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CloseEndNAVLetterReport.aspx.cs" Inherits="UI_CloseEndNAVLetterReport" Title="NAV Letter Report of Close End Mutual Funds" %>
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
            document.getElementById("<%=letterPrintDateTextBox.ClientID%>").value =""; 
            document.getElementById("<%=navDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=letterToDropDownList.ClientID%>").value ="0";   
            document.getElementById("<%=fundNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=signatoryDropDownList.ClientID%>").value ="IAMCL411";         
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
        if(document.getElementById("<%=letterPrintDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=letterPrintDateTextBox.ClientID%>").focus();
            alert("Please Insert Letter Print Date.");
            return false; 
        }
        if(document.getElementById("<%=letterPrintDateTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=letterPrintDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=letterPrintDateTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=navDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=navDateTextBox.ClientID%>").focus();
            alert("Please Insert NAV Date.");
            return false; 
        }
        if(document.getElementById("<%=navDateTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=navDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=navDateTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calender.");
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
                NAV Letter Report of Close End Mutual Funds
            </td>           
            <td>
                <br />
            </td>
        </tr> 
    </table>
    <br />
    <table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center" colspan="2" >
                <asp:RadioButton ID="navLetterRadioButton" runat="server" Checked="True" 
                    GroupName="letterType" Text="NAV Letter" TabIndex="1" />
                <asp:RadioButton ID="pressReleaseRadioButton" runat="server" 
                    GroupName="letterType" Text="Press Release" TabIndex="2" />
           </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Letter Print Date:</b></td>
            <td align="left">
                <asp:TextBox ID="letterPrintDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="letterPrintDateTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" 
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>NAV Date:</b></td>
            <td align="left">
                <asp:TextBox ID="navDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="5"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="navDateTextBox"
                PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="6" />
            </td>   
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Letter To:</b></td>
            <td align="left" >
            <asp:DropDownList ID="letterToDropDownList" runat="server" TabIndex="7">
            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Chairman of SEC" Value="1"></asp:ListItem>
            <asp:ListItem Text="MD of ICB" Value="2"></asp:ListItem>
            <asp:ListItem Text="Chairman of ICB AMCL" Value="3"></asp:ListItem>
            <asp:ListItem Text="CEO of DSE" Value="4"></asp:ListItem>
            <asp:ListItem Text="CEO of CSE" Value="5"></asp:ListItem>
            </asp:DropDownList></td>   
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Fund Name:</b></td>
            <td align="left" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Authorized Signatory:</b></td>
            <td align="left" >
            <asp:DropDownList ID="signatoryDropDownList" runat="server" TabIndex="9"></asp:DropDownList>
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

