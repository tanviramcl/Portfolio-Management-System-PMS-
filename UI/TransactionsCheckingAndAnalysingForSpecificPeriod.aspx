<%@ Page Language="C#"  MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="TransactionsCheckingAndAnalysingForSpecificPeriod.aspx.cs" Inherits="UI_BalancechekReport" Title="Transactions Checking AndAnalysing For Specific Period" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        }
        .ui-datepicker {
        position: relative !important;
        top: -320px !important;
        left: 100px !important;
        margin-left: 390px;
        margin-top: -15px;
        }  
    </style> 
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
    <table style="text-align: center">
        <tr>
            <td class="FormTitle" align="center">Transactions checking and analysing for specific period</td>
            <td>
                <br />
            </td>
            <td>
                 <asp:Label ID="Labelerror" Visible="false" style="color: red; display:inline-flex;" runat="server" Text=""></asp:Label>
            </td>
        </tr>

    </table>
    <table style="text-align: center">
       <tr>
            <td align="right">
                <b>From Date:</b>
            </td>
            <td align="left">
          
                <asp:TextBox ID="RIssuefromTextBox" runat="server" Width="100px"></asp:TextBox>
            </td>
       </tr>
        <tr>
            <td align="right">
                <b>To Date:</b>
            </td>
         
            <td align="left">
            
                <asp:TextBox ID="RIssueToTextBox" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>
    
          
       
        <tr>
          
            <td align="right"> 
                <asp:Label ID="fundNameDropDownListlabel"  style="font-weight: 700" runat="server" Text="Fund Name:"></asp:Label>
            </td>
            <td align="left" width="200px">
                <asp:DropDownList ID="fundNameDropDownList"  runat="server" TabIndex="6"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
         
        <tr>
            <td align="right">
                <asp:Label ID="transTypeDropDownListLabel"  style="font-weight: 700" runat="server" Text="Transaction Type:"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="transTypeDropDownList"  runat="server"
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

    <table width="750" style="text-align: center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center">
                <asp:Button ID="showButton" runat="server" Text="Show Report"
                    CssClass="buttoncommon" TabIndex="5" OnClick="showButton_Click" />
            </td>
        </tr>
    </table>



   <script type="text/javascript">

    $(function () {

          $('#<%=RIssuefromTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {
                     $('#<%=RIssueToTextBox.ClientID%>').datepicker("option","minDate", selected)
                 }
             });
             $('#<%=RIssueToTextBox.ClientID%>').datepicker({ 
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {

                    

                 }
             });  




    });

    $.validator.addMethod("fundDropDownList", function (value, element, param) {  
        if (value == '0')  
            return false;  
        else  
            return true;  
    },"* Please select a Fund");



    $.validator.addMethod("transTypeDropDownList", function (value, element, param) {  
        if (value == '0')  
            return false;  
        else  
            return true;  
    },"* Please select trajection type");

    $.validator.addMethod("assetDate", function(value, element) { 
        return Date.parseExact(value, "dd-M-yy");
    }),"* Please enter a date in the format!";

    
    $("#aspnetForm").validate({
        rules: {
                   
                    <%=RIssuefromTextBox.UniqueID %>: {
                        
                        required: true,
                        
                    },<%=RIssueToTextBox.UniqueID %>: {
                        
                        required: true,
                       
                    },<%=fundNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                        fundDropDownList:true
                        
                    },<%=transTypeDropDownList.UniqueID %>: {
                        
                        //required:true 
                        transTypeDropDownList:true
                        
                    }
              
                }, messages: {
                   <%=RIssuefromTextBox.UniqueID %>:{  
                       required: "*From Date  is required*",
                       
                   },<%=RIssueToTextBox.UniqueID %>:{  
                       required: "*To Date  is required*",
                      
                   }
                    
                    
            
                
                }
      });

    </script>
</asp:Content>


