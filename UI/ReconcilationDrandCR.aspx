<%@ Page Language="C#"  MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="ReconcilationDrandCR.aspx.cs" Inherits="UI_BalancechekReport" Title="Reconcilation DR/CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        } 
        .ui-datepicker {
        position: relative !important;
        top: -250px !important;
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
            <td class="FormTitle" align="center">Reconcilation DR/CR</td>
            <td>
                <br />
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
            <asp:Label ID="fundNameDropDownListlabe"  style="font-weight: 700" runat="server" Text="Fund Name:"></asp:Label>
        </td>
        <td align="left" width="200px">
            <asp:DropDownList ID="fundNameDropDownList"  runat="server" TabIndex="6"
                AutoPostBack="True">
            </asp:DropDownList>
        </td>
       </tr>
    </table>
    <table width="750" style="text-align: center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center" colspan="2">
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
    
    $("#aspnetForm").validate({
        rules: {
             <%=RIssuefromTextBox.UniqueID %>: {
                        
                        required: true
                      
                    },<%=RIssueToTextBox.UniqueID %>: {
                        
                        required: true,
                        greaterThan: '#<%=RIssueToTextBox.ClientID%>'                      
                    },<%=fundNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                        fundDropDownList:true
                        
                    }
              
                }, messages: {
                   <%=RIssuefromTextBox.UniqueID %>:{  
                       required: "*From Date  is required*"
                     
                   },<%=RIssueToTextBox.UniqueID %>:{  
                       required: "*To Date  is required*",
                      
                   }
                    
                    
                        
                }
      });

    </script>
</asp:Content>
