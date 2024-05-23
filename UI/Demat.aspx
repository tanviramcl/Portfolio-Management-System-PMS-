<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="Demat.aspx.cs" Inherits="UI_CompanyInformation" Title="Single Certificate Dematerialization" %>
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
                    <b><u>Single Certificate Dematerialization</u></b>
                </td>
            </tr>
            
            </table>

            <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
               
                <tr>
                    <td align="left">
                        <b>Company code 
                       
                        </b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="companyCodeTextBox" runat="server" Width="100px" AutoPostBack="true" OnChange="companycodeTextBox__TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>


                    <td align="left">
                        <b>Allotment No. </b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="AllotmentNoTextBox" runat="server" Width="100px" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td align="left">
                        <b>Certificate No</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="certificateNoTextBox" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="certificateNoTextBox_TextChanged"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td align="left">
                        <b>Howla Date</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="HowladateTextBox" runat="server"  Width="100px"></asp:TextBox>
                    </td>
                    <td align="left">
                        <b>No Of Securities</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="SecuritiesTextBox" runat="server"  Width="100px"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td align="left">
                        <b>Dictincttive No From</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="DictincttivefromTextBox" runat="server" AutoPostBack="true" Width="100px"></asp:TextBox>

                    </td>
                    <td align="left">
                        <b>Dictincttive No To</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="DictincttivetoTextBox" runat="server" Width="100px"></asp:TextBox>

                    </td>
                </tr>
                <tr>

                    <td align="left">
                        <b>Fund Code</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="fundcodeTextBox" runat="server"  Width="100px"></asp:TextBox>
                    </td>
                    <td align="left">
                        <b>Folio No</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="folioNoTextBox" ReadOnly="true" runat="server" Width="100px"></asp:TextBox>
                    </td>

                </tr>

                <tr>


                    <td align="left">
                        <b>Shares type</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="shareTypeTextBox" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                    </td>
                    <td align="left">
                        <b>Purchase Rate</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="PurchaseRateTextBox" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td align="left">
                        <b>Demat sending Date</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="DematsendingDateTextBox" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td align="left">
                        <b>Demat sending No</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="DematsendingNo" runat="server" Width="100px"></asp:TextBox>
                    </td>

                </tr>

                <tr>

                    <td align="center" colspan="6">
                        <asp:Button ID="saveButton" runat="server" Text="Save"
                            CssClass="buttoncommon" TabIndex="48"
                            OnClick="saveButton_Click" />


                    </td>

                </tr>
        </table>
        </div>
  
         
    <script type="text/javascript">
            $(function () {

          $('#<%=HowladateTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {
                   
                 }
          });

            
          $('#<%=DematsendingDateTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {
                   
                 }
             });
      
          });

        $.validator.addMethod("noofshareTextBox", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"No of Share Cannot be 0.");  
       
    

      $("#aspnetForm").validate({
          rules: {
                    <%=fundcodeTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength: 2
                    },
                    <%=companyCodeTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        minlength: 3,
                        maxlength: 3
                    }, <%=HowladateTextBox.UniqueID %>: {
                        
                        required: true
                        
                    }, <%=AllotmentNoTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }, <%=certificateNoTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }<%--, <%=folioNoTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }--%>, <%=DictincttivefromTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }, <%=DictincttivetoTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }, <%=SecuritiesTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        noofshareTextBox:true,
                        maxlength: 18
                        
                    }, <%=DematsendingDateTextBox.UniqueID %>: {
                        
                        required: true
                        
                    }
              
          }, messages: {
                      <%=fundcodeTextBox.UniqueID %>:{  
                         required: "*Fund Code is required*",
                         maxlength: "* Please enter maximum 2 characters *"
                      },
                     <%=companyCodeTextBox.UniqueID %>:{  
                         required: "*Company Code is required*",
                         maxlength: "* Please enter maximum 3 characters *"
                      },<%=AllotmentNoTextBox.UniqueID %>:{  
                         required: "*Allotment No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      },<%=certificateNoTextBox.UniqueID %>:{  
                          required: "*Certificate No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      }<%--,<%=folioNoTextBox.UniqueID %>:{  
                          required: "*Folio  No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      }--%>,<%=DictincttivefromTextBox.UniqueID %>:{  
                          required: "*Dictincttive From  No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      },<%=DictincttivetoTextBox.UniqueID %>:{  
                          required: "*Dictincttive To  No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      }, <%=SecuritiesTextBox.UniqueID %>:{  
                         required: "*No of Share is required*",
                         maxlength: "* Please enter maximum 18 characters *"
                      }
                
                }
      });
     
    </script>
</asp:Content>

