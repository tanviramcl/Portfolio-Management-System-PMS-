<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="HowlaCSEentryForm.aspx.cs" Inherits="UI_HowlaCSEentryForm" Title="Howla CSE Entry Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript"> 
     function fnCheqInput()
     {
            if(document.getElementById("<%=tradingDateTextBox.ClientID%>").value =="0")
            {
                document.getElementById("<%=tradingDateTextBox.ClientID%>").focus();
                alert("Please Enter Trading Date");
                return false;
            }
            if(document.getElementById("<%=clearingDateTextBox.ClientID%>").value =="0")
            {
                document.getElementById("<%=clearingDateTextBox.ClientID%>").focus();
                alert("Please Enter Clearing Date");
                return false;
            }
               
            if(document.getElementById("<%=tradeCusFileUpload.ClientID%>").value =="")
            {
                document.getElementById("<%=tradeCusFileUpload.ClientID%>").focus();
                alert("Please Select btDD-MM-YY.TXT File");
                return false;
            }
     }
     function fnConfirm()
     {
        var Conf=confirm("Are You Sure to Save Data?")
        if(Conf)
        {
          return true
        }
        else
           {
            return false;
           }
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
    

<table width="600" align="center" cellpadding="0" cellspacing="0" >
    <tr>
   
    <td align="center" class="TableHeader">CSE Howla Entry Form</td>  
    </tr>
</table>
<br />
<br />
<br />

<br />

<table width="600" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="100"></colgroup>
    <tr>
        <td align="right" style="font-weight: 700">Trading Date<b>:&nbsp;&nbsp; </b></td>
        <td align="left">
            <asp:TextBox ID="tradingDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="tradingDateTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" />&nbsp;(DD-MMM-YYYY)</td>
        
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Clearing Date:&nbsp;&nbsp; </b></td>
        <td align="left">
            <asp:TextBox ID="clearingDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="clearingDateTextBox"
                PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;(DD-MMM-YYYY)</td>
    </tr>
 
      <tr>
      <td align="left">TradeCus File</td>
        <td align="left">
            <asp:FileUpload ID="tradeCusFileUpload" runat="server" AutoPostBack="True"/>
                &nbsp;<span class="star">btDD-MM-YY.TXT</span></td>
      
    </tr>
   <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:Button ID="showDataButton" 
                runat="server" CssClass="buttoncommon" 
                 OnClientClick="return fnCheqInput();" Text="Show  Data" onclick="showDataButton_Click" 
                />
                &nbsp;&nbsp;<asp:Button ID="SaveButton" 
                runat="server" CssClass="buttoncommon" 
                 OnClientClick="return fnConfirm();" Text="Save  Data" 
                onclick="saveDataButton_Click" Visible="False" 
                />
                </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
         <td align="left" id="tdNoData" runat="server">
                <asp:Label ID="lbNodata" runat="server" Text=""></asp:Label>
            </td>
    </tr>
</table>
    <br />
    
      <table align="center" >
      <tr>
        <td>
            <div style="height:300px; overflow:auto;" id="dvGrid" runat="server" visible="true">
            <asp:DataGrid id="grdShowDetails" runat="server"  style="border: #666666 1px solid;"  AutoGenerateColumns="False" CellPadding="4">                               
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>                                      
                    <asp:BoundColumn HeaderText="SI#" DataField="SI"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Fund" DataField="CUSTOMER"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Trading Date" DataField="SP_DATE"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Howla No." DataField="HOWLA_NO"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Buy or Sale" DataField="IN_OUT"></asp:BoundColumn>  
                    <asp:BoundColumn HeaderText="No. of Shares" DataField="SP_QTY" ></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Rate" DataField="SP_RATE"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Howla Charge" DataField="HOWLA_CHG"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Laga Charge" DataField="LAGA_CHG"></asp:BoundColumn>          
                    </Columns>          
            </asp:DataGrid>
            </div>
        </td>
      </tr>
    </table>  
    
    <br />
    <br />
    <br />
    <br />
</asp:Content>

