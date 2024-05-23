<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="ComparisonBetweenNavAndDseIndex.aspx.cs" Inherits="UI_BalancechekReport" Title="Comparison  Between Nav And Dse Index " %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
  <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        }  
    </style> 
  

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
 
       <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
   
        <div align="center">
            <table id="Table1" width = "600" align = "center" cellpadding ="0" cellspacing ="0" runat="server">
            <tr>
                <td align="center" class="style3">
                    <b><u>Comparison  Between Nav And Dse Index</u></b>
                </td>
            </tr>
            </table>
    
             <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
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
                     <td align="right" style="font-weight: 700"><b>Fund Name:</b></td>
                     <td align="left" width="200px">
                         <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="6"
                             AutoPostBack="True">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 
                 <%--<tr>
                     <td align="right" style="font-weight: 700" class="style5"><b>Portfolio As On:</b></td>
                     <td align="left">
                         <asp:DropDownList ID="PortfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
                         <span class="style6">*</span>
                     </td>
                 </tr>--%>
                 <tr>

                     <td align="center" colspan="2">
                         <asp:Button ID="showButton" runat="server" Text="Show Report"
                             CssClass="buttoncommon" TabIndex="5" OnClick="showButton_Click" />

                     </td>

                 </tr>
        </table>
        </div>
  
         
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
        $.validator.addMethod("fundNameDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Fund");   
        //$.validator.addMethod("PortfolioAsOnDropDownList", function (value, element, param) {  
        //    if (value == '0')  
        //        return false;  
        //    else  
        //        return true;  
        //},"Please select a Balance Date"); 

      $("#aspnetForm").validate({
          rules: {
                      <%=RIssuefromTextBox.UniqueID %>: {
                        
                        required: true
                      
                    },<%=RIssueToTextBox.UniqueID %>: {
                        
                        required: true,
                        greaterThan: '#<%=RIssueToTextBox.ClientID%>'                      
                    },
                     <%=fundNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                     fundNameDropDownList:true
                        
                    }<%--,<%=PortfolioAsOnDropDownList.UniqueID %>: {
                        
                        PortfolioAsOnDropDownList: true
                    }--%>
              
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

