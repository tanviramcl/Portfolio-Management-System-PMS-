<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="FundTransactionEntry.aspx.cs" Inherits="UI_FundTransactionEntry" Title="Fund Transaction Entry Form" %>
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
            document.getElementById("<%=howlaDateTextBox.ClientID%>").value =""; 
            document.getElementById("<%=stockExchangeDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=fundNameDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=companyNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=transTypeDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=voucherNoTextBox.ClientID%>").value ="";
            document.getElementById("<%=noOfShareTextBox.ClientID%>").value ="";
            document.getElementById("<%=rateTextBox.ClientID%>").value ="";
            document.getElementById("<%=amountTextBox.ClientID%>").value =""; 
            document.getElementById("<%=amountAfterComissionTextBox.ClientID%>").value ="";
            
            return false;
        }
        else
        {   
            return true;
        }
            
    }
    
    function fnCheckInput()
    {   
        if(document.getElementById("<%=howlaDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=howlaDateTextBox.ClientID%>").focus();
            alert("Please Insert Howla Date.");
            return false; 
        }
        if(document.getElementById("<%=howlaDateTextBox.ClientID%>").value!="")
        {
            var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=howlaDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=howlaDateTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=stockExchangeDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=stockExchangeDropDownList.ClientID%>").focus();
            alert("Please Select Stock Exchange.");
            return false; 
        }
        if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
            alert("Please Select Fund Name.");
            return false; 
        }
       
        if(document.getElementById("<%=companyNameDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=companyNameDropDownList.ClientID%>").focus();
            alert("Please Select Company Name.");
            return false; 
        }
        if(document.getElementById("<%=transTypeDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=transTypeDropDownList.ClientID%>").focus();
            alert("Please Select Transaction Type.");
            return false; 
        }
        if(document.getElementById("<%=noOfShareTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=noOfShareTextBox.ClientID%>").focus();
            alert("Please Enter No. of Shares.");
            return false; 
        }
         if(document.getElementById("<%=amountTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=noOfShareTextBox.ClientID%>").focus();
            alert("Please Enter Amount.");
            return false; 
         }
          if(document.getElementById("<%=recordDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=howlaDateTextBox.ClientID%>").focus();
            alert("Please Insert Record Date.");
            return false; 
        }
        if(document.getElementById("<%=recordDateTextBox.ClientID%>").value!="")
        {
            var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=recordDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=recordDateTextBox.ClientID%>").focus();
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
            FUND TRANSACTION ENTRY FORM
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td align="right" style="font-weight: 700"><b>Howla Date:</b></td>
        <td align="left">
            <asp:TextBox ID="howlaDateTextBox" runat="server" style="width:100px;" CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="howlaDateTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>
    </tr>
    <tr>
        <td align="right"><b>Stock Exchange:</b></td>
        <td align="left" ><asp:DropDownList ID="stockExchangeDropDownList" runat="server" 
                TabIndex="3">
            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
            <asp:ListItem Text="ALL" Value="A"></asp:ListItem>
            <asp:ListItem Text="DSE" Value="D"></asp:ListItem>
            <asp:ListItem Text="CSE" Value="C"></asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    
    <tr>
        <td align="right" style="font-weight: 700"><b>Company Name:</b></td>
        <td align="left" >
            <asp:DropDownList ID="companyNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
           
    </tr>
    <tr>
        <td align="right"><b>Trans Type:</b></td>
        <td align="left" >
            <asp:DropDownList ID="transTypeDropDownList" runat="server" 
                TabIndex="5" AutoPostBack="true"
                onselectedindexchanged="transTypeDropDownList_SelectedIndexChanged">
            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Bonus Share" Value="B"></asp:ListItem>
            <asp:ListItem Text="Purchase of Share" Value="C"></asp:ListItem>
            <asp:ListItem Text="Sale of Share" Value="S"></asp:ListItem>
            <asp:ListItem Text="Right Share" Value="R"></asp:ListItem>
            <asp:ListItem Text="IPO Share" Value="I"></asp:ListItem>
            <asp:ListItem Text="Reconciliation of Share" Value="N"></asp:ListItem>
            </asp:DropDownList></td>
        <td align="right" style="font-weight: 700"><b>Voucher No.:</b></td>
        <td align="left" >
            <asp:TextBox ID="voucherNoTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="9"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Fund Name:</b></td>
        <td align="left" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="6" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged"></asp:DropDownList>
            </td>

    <%--      
        <td align="right"  style="font-weight: 700"><b><asp:Label ID="lblrecordDate" runat="server" Text="Record Date:"></asp:Label></b></td>
        <td align="left">
            <asp:TextBox ID="recordDateTextBox" runat="server" style="width:100px;" CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="recordDateTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>--%>
            <td align="right"  style="font-weight: 700"><b><asp:Label ID="lblrecordDate" visible="false" runat="server" Text="Record Date:"></asp:Label></b></td>
            <td align="left"><asp:TextBox ID="recordDateTextBox" runat="server" Visible="false" style="width:100px;" CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderfd" runat="server" TargetControlID="recordDateTextBox"
                PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
             <asp:ImageButton ID="ImageButton1" runat="server"  Visible="false" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" />

            </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>No. of Shares:</b></td>
        <td align="left" >
            <asp:TextBox ID="noOfShareTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="7" AutoPostBack="True" 
                ontextchanged="noOfShareTextBox_TextChanged"></asp:TextBox></td>   
        <td align="right" style="font-weight: 700"><b>Rate:</b></td>
        <td align="left" >
            <asp:TextBox ID="rateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="10"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Amount:</b></td>
        <td align="left" >
            <asp:TextBox ID="amountTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="8" AutoPostBack="True" 
                ontextchanged="amountTextBox_TextChanged"></asp:TextBox></td>   
        <td align="right" style="font-weight: 700"><b>Amount(Comission):</b></td>
        <td align="left" >
            <asp:TextBox ID="amountAfterComissionTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="11"></asp:TextBox></td>
    </tr>
   
    <tr>
           <td align="center" colspan="4" >
                &nbsp;
           </td>
    </tr>
    <tr>
            <td align="center" colspan="4" >
            <asp:Button ID="saveButton" runat="server" Text="Save" 
                CssClass="buttoncommon" TabIndex="12" OnClientClick=" return fnCheckInput();" 
                    onclick="saveButton_Click" AccessKey="s" 
                    />
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="13" OnClientClick=" return fnReset();"
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

