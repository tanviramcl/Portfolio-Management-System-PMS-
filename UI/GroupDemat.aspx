<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="GroupDemat.aspx.cs" Inherits="UI_CompanyInformation" Title="Group Certificate Dematerialization" %>
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
                    <b><u>Group Certificate Dematerialization</u></b>
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
                    <td align="left">
                        <b></b>
                    </td>
                     <td align="left">
                        <b>Fund Code</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="fundcodeTextBox" runat="server"  Width="100px"></asp:TextBox>
                    </td>
                </tr>


                <tr>


                    <td align="left">
                        <b>Allotment No. From </b>
                    </td>
                    <td align="left">
                         <asp:TextBox ID="atalphaTextBox" runat="server" Width="100px"  OnTextChanged="atalphaTextBox_onchange" AutoPostBack="true"></asp:TextBox>
                     </td>
                    <td align="left">
                        <asp:TextBox ID="AllotmentNoTextBox" runat="server" Width="100px"  OnTextChanged="AllotmentNoTextBox_onchange" AutoPostBack="true"></asp:TextBox>
                    </td>

                     <td align="left">
                        <b>Allotment No. To </b>
                    </td>
                    <td align="left">
                         <asp:TextBox ID="atalphaTextBox1" runat="server" Width="100px" AutoPostBack="true"></asp:TextBox>
                     </td>
                    <td align="left">
                        <asp:TextBox ID="AllotmentNoTextBox2" runat="server" Width="100px" AutoPostBack="true"></asp:TextBox>
                    </td>
                    

                </tr>

                <tr>
                    <td align="left">
                        <b>Certificate No From</b>
                    </td>
                    
                    <td align="left">
                         <asp:TextBox ID="CatalphaTextBox"  OnTextChanged="CatalphaTextBox_onchange" runat="server" Width="100px" AutoPostBack="true"></asp:TextBox>
                     </td>
                    <td align="left">
                        <asp:TextBox ID="certificateNoTextBox" runat="server"  OnTextChanged="certificateNoTextBox_onchange" Width="100px" AutoPostBack="true" ></asp:TextBox>
                    </td>
                    <td align="left">
                        <b>Certificate No To</b>
                    </td>
                    
                    <td align="left">
                         <asp:TextBox ID="CatalphaTextBox1" runat="server" Width="100px" AutoPostBack="true"></asp:TextBox>
                     </td>
                    <td align="left">
                        <asp:TextBox ID="certificateNoTextBox2" runat="server" Width="100px" AutoPostBack="true" ></asp:TextBox>
                    </td>
                 </tr>



                <tr>
                    <td align="left">
                        <b>Letter No</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="LatterNoTextBox" runat="server"  Width="100px"></asp:TextBox>
                    </td>
                     <td align="left">
                        <b></b>
                    </td>
                    <td align="left">
                        <b>Total Shares</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="SecuritiesTextBox" ReadOnly="true" runat="server"  Width="100px"></asp:TextBox>
                    </td>

                </tr>
                
                <tr>

                   
                    <td align="left">
                        <b>Letter Date</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="LatterDateTextBox"  runat="server" Width="100px"></asp:TextBox>
                    </td>
                     <td align="left">
                        <b></b>
                    </td>
                    <td align="left">
                        <b>No of Script</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="NoofTextBox" ReadOnly="true" runat="server" Width="100px"></asp:TextBox>
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

          $('#<%=LatterDateTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {
                   
                 }
          });

            
         
          });
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
                    },<%=atalphaTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true
                    },<%=atalphaTextBox1.UniqueID %>: {
                        
                        required: true,
                        number:true
                    },<%=AllotmentNoTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true
                    },<%=AllotmentNoTextBox2.UniqueID %>: {
                        
                        required: true,
                        number:true
                    },<%=CatalphaTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true
                    },<%=certificateNoTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true
                    },<%=CatalphaTextBox1.UniqueID %>: {
                        
                        required: true,
                        number:true
                    },<%=certificateNoTextBox2.UniqueID %>: {
                        
                        required: true,
                        number:true
                    }, <%=LatterDateTextBox.UniqueID %>: {
                        
                        required: true
                        
                    }, <%=LatterNoTextBox.UniqueID %>: {
                        
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
                      }, <%=atalphaTextBox.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=atalphaTextBox1.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=AllotmentNoTextBox.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=AllotmentNoTextBox2.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=CatalphaTextBox.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=certificateNoTextBox.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=CatalphaTextBox1.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=certificateNoTextBox.UniqueID %>:{  
                         required: "*required*",
                         number: "* please enter a number *"
                      }, <%=LatterNoTextBox.UniqueID %>:{  
                         required: "*required*"
                      }
                
                }
      });
     
    </script>
</asp:Content>

