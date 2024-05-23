<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon_old.master" AutoEventWireup="true" CodeFile="testFundTransactionHBReport.aspx.cs" Inherits="UI_FundTransactionReport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="text-align:center">
    <tr>
        <td class="FormTitle" align="center">
            Fund Transaction HB Table Report         </td>           
        <td>
            <br />
        </td>
    </tr> 

</table>
<br />
<table width="750" style="text-align:center" cellpadding="0" cellspacing="0" border="0">
   
 <tr>
    <td align="right"><b>Transaction Type:</b></td>
    <td align="left" >
        <asp:DropDownList ID="Fund_transTypeDropDownList" runat="server" 
            TabIndex="5">
        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
        <asp:ListItem Text="Bonus Share" Value="B"></asp:ListItem>
        <asp:ListItem Text="Purchase of Share" Value="C"></asp:ListItem>
        <asp:ListItem Text="Sale of Share" Value="S"></asp:ListItem>
        <asp:ListItem Text="Right Share" Value="R"></asp:ListItem>
        <asp:ListItem Text="Pre IPO Share" Value="I"></asp:ListItem>
        </asp:DropDownList></td>
    </tr>
   
    
</table>
<table width="750" style="text-align:center" cellpadding="0" cellspacing="0" border="0">
   
 

    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="showButton" runat="server" Text="Show Report" 
                CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" onclick="showButton_Click" 
                    />
          
            </td>
            
    </tr>
    
</table>
</asp:Content>

