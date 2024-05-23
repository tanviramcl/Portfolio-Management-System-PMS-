<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CompanyInformationForCDS.aspx.cs" Inherits="UI_CompanyInformation" Title="Company Information" %>
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
                    <b><u>Company Information For Transaction</u></b>
                </td>
            </tr>
            </table>
            <table "text-align"="center">
                <tr>
                    <td class="" align="center">
                          <asp:Label ID="lblmessage" runat="server" Text="" Style="font-size: 20px; color: greenyellow;"></asp:Label>
                    </td>           
                    <td>
                        <br />
                    </td>
                </tr> 
            </table>
    
             <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
            <tr>
                <td align="left">
                    <b>Company code </b>
                </td>
                <td align="left">
                    <asp:TextBox ID="companyCodeTextBox" runat="server" Width="100px" OnTextChanged="companyCodeTextBox_TextChanged"  AutoPostBack="true"></asp:TextBox>
                </td>
                <td align="left">
                    <b>Group type</b>
                </td>
                <td>
         <%--           <asp:DropDownList ID="groupDropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A Group" Value="N"></asp:ListItem>
                        <asp:ListItem Text="B Group" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Z Group" Value="Z"></asp:ListItem>


                    </asp:DropDownList>--%>

                     <asp:DropDownList ID="GROUPDropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A Group" Value="N"></asp:ListItem>
                        <asp:ListItem Text="B Group" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Z Group" Value="Z"></asp:ListItem>
                       
                    </asp:DropDownList>
                </td>

            </tr>
         
            <tr>
                <td align="left">
                    <b>Market Lot </b>
                </td>
                <td align="left">
                    <asp:TextBox ID="MarketLotTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td align="left">
                    <b>Avarage Market Rate</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="avarageMarketRateTextBox" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>Face value</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="faceValueTextBox" runat="server" Width="100px" ></asp:TextBox>
                </td>

                <td align="left">
                    <b>Last Trading date</b>
                </td>
                <td align="left">
                   <%-- <asp:TextBox ID="lasttradingdateTextBox" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                    <asp:ImageButton ID="lasttradingdateTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                    <ajaxToolkit:CalendarExtender ID="lasttradingdateTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="lasttradingdateTextBoxImageButton" TargetControlID="lasttradingdateTextBox"></ajaxToolkit:CalendarExtender>--%>
                     <asp:TextBox ID="lasttradingdateTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>


                <td align="left">
                    <b>Dse code</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="dsecodeTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>

                <td align="left">
                    <b>CSE code</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="csecodeTextBox" runat="server" Width="100px" ></asp:TextBox>
                </td>


            </tr>
          
         
          
            <tr>
                <td align="left">
                    <b>CSE Script ID</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="cseScriptIdTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>

                <td align="left">
                    <b>IS CDS?</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="IscdsTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>CDS Start Date</b>
                </td>
                <td align="left">
                     <asp:TextBox ID="CDSStartDateTextBox" runat="server" Width="100px"></asp:TextBox>


                </td>
                  <td align="left">
                    <b>ISIN Code</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="isinCode" runat="server" Width="100px"></asp:TextBox>
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

       
           
          $('#<%=CDSStartDateTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today"
               
             });

               $('#<%=lasttradingdateTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today"
               
             });

    });
    
        $.validator.addMethod("CheckDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Group.");  
        
        $.validator.addMethod("CheckCds", function (value, element, param) { 
           // alert(value);
            if (value == 'Y')  
                return true;  
            else if(value == 'N') 
                return true;  
            else
                return false;  
        },"Invalid CDS.");

      $("#aspnetForm").validate({
          rules: {
                    <%=companyCodeTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        minlength: 3,
                        maxlength: 3
                    },<%=GROUPDropDownList.UniqueID %>: {
                        
                        //required:true 
                        CheckDropDownList:true
                        
                    },
                    <%=MarketLotTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength: 5
                    },<%=faceValueTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true
                      
                    },<%=dsecodeTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength:20
                      
                    },<%=csecodeTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength:20
                      
                    }
                    , <%=lasttradingdateTextBox.UniqueID %>: {
                        
                        required: true,
                        
                    }, <%=CDSStartDateTextBox.UniqueID %>: {
                        
                        required: true,
                        
                    }, <%=isinCode.UniqueID %>: {
                        
                        required: true
                       
                    }, <%=IscdsTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength:1,
                        CheckCds:true
                       
                    }
              
                }, messages: {
                     <%=companyCodeTextBox.UniqueID %>:{  
                         required: "*Company Code is required*",
                         maxlength: "* Please enter maximum 3 characters *"
                      },<%=MarketLotTextBox.UniqueID %>: {
                        
                        required: "*Market Lot is required*",
                        number: "Please Enter an Numeric Value",
                        maxlength:"* Please enter maximum 5 characters *"
                    },<%=faceValueTextBox.UniqueID %>: {
                        
                        required: "*Face value is required*",
                        number: "Please Enter an Numeric Value",
                     
                    },<%=dsecodeTextBox.UniqueID %>: {
                        required: "*DSE Code is required*",
                        maxlength:"* Please enter maximum 20 characters *"
                    },<%=csecodeTextBox.UniqueID %>: {
                        required: "*CSE Code is required*",
                        maxlength:"* Please enter maximum 20 characters *"
                    },<%=lasttradingdateTextBox.UniqueID %>:{  
                       required: "* Date  is required*",
                       
                   },<%=CDSStartDateTextBox.UniqueID %>:{  
                       required: "* Date  is required*",
                       
                   },<%=isinCode.UniqueID %>:{  
                       required: "* ISIN Code  is required*",
                       
                   },<%=IscdsTextBox.UniqueID %>:{  
                       required: "* CDS required*",
                       maxlength: "CDS no Invalid"
                       
                   }
                }
      });
     
    </script>
</asp:Content>

