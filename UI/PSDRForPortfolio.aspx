<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PSDRForPortfolio.aspx.cs" Inherits="UI_CompanyInformation" Title="PSDR (PortFlio)" %>
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
                    <b><u>PSDR (PortFlio)</u></b>
                </td>
            </tr>
            </table>
    
             <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
             <tr>
                  <td align="left">
                    <b>Fund Code</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="fundcodeTextBox" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="FundCodeTextBox_TextChanged"></asp:TextBox>
                </td>
                 <td align="left">
                        <asp:Label ID="fundLabel" Style="font-size: 10px; color: red; width:100px" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            <tr>
               
                <td align="left">
                    <b>Company code </b>
                </td>
                <td align="left">
                    <asp:TextBox ID="companyCodeTextBox" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="compCodeTextBox_TextChanged"></asp:TextBox>
                </td>
                 <td align="left">
                        <asp:Label ID="companyNameLabe"  Style="font-size: 10px; color: red; width:100px" runat="server" Text=""></asp:Label>
                 </td>
                 <td align="left">
                        <asp:Label ID="MarketLotLabel"  Style="font-size: 15px;  width:100px" runat="server" Text="Market Lot:"></asp:Label>
                    </td>
                     <td align="left">
                         <asp:Label ID="lblLabelMarketLotLabel"  Style="font-size: 15px;   width:100px" runat="server" Text=""></asp:Label>
                    </td>
                    
             </tr>
              <tr>
                
                <td align="left">
                    <b>Certificate No</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="certificateNoTextBox" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="certificateNoTextBox_TextChanged"></asp:TextBox>
                </td>
                  <td align="left">
                    <b>Posted </b>
                </td>
                <td align="left">
                   <%-- <asp:TextBox ID="sectorTextBox" runat="server" Width="100px"></asp:TextBox>--%>
                     <asp:DropDownList ID="postedDropDownList" Width="130px" runat="server" TabIndex="3"></asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                
                <td align="left">
                    <b>PSDR No </b>
                </td>
                <td align="left">
                    <asp:TextBox ID="psdrNoTextBox" runat="server" Width="100px" ></asp:TextBox>
                </td>

                <td align="left">
                    <b>Securities Type </b>
                </td>
                <td align="left">
                     <asp:DropDownList ID="securitiestypeDropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Primary" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Bonus" Value="B"></asp:ListItem>
                        <asp:ListItem Text="Right" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Convertible" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Secondary" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Placement" Value="L"></asp:ListItem>

                    </asp:DropDownList>
                </td>
                 <td align="left">
                    <b>Allotment No. </b>
                </td>
                <td align="left">
                    <asp:TextBox ID="AllotmentNoTextBox" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="allotmentTextBox_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>Market Lot </b>
                </td>
                <td align="left">
                     <asp:DropDownList ID="oddormarketlotDropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Odd" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Market Lot" Value="M"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td align="left">
                    <b>AGM/EGM/Public Issue Date</b>
                </td>
                <td align="left">
                  <asp:TextBox ID="RIssuefromTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                 <td align="left">
                    <b>No Of Shares</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="noofshareTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                
            </tr>
          
         
         
            <tr>
                
                
                 <td align="left">
                    <b>Folio No</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="folioNoTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                 <td align="left">
                    <b>Dictinctive No From</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="DictincttivefromTextBox" runat="server" AutoPostBack="true" Width="100px" OnTextChanged="DictincttivefromTextBox__TextChanged"></asp:TextBox>
                      
                </td>
                <td align="left">
                    <b>Dictinctive No To</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="DictincttivetoTextBox" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                  
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

          $('#<%=RIssuefromTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {
                   
                 }
             });
      
          });


        $.validator.addMethod("securitiestypeDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Securities Type.");  

        $.validator.addMethod("postedDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select");  

        $.validator.addMethod("oddormarketlotDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Odd/Market Lot.");  
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
                    }, <%=psdrNoTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength: 6
                    }, <%=postedDropDownList.UniqueID %>: {
                        
                        postedDropDownList:true
                    },
                    <%=securitiestypeDropDownList.UniqueID %>: {
                        
                        securitiestypeDropDownList:true
                    }
                    , <%=oddormarketlotDropDownList.UniqueID %>: {
                        
                        oddormarketlotDropDownList:true
                    }, <%=RIssuefromTextBox.UniqueID %>: {
                        
                        required: true
                        
                    }, <%=noofshareTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        noofshareTextBox:true,
                        maxlength: 18
                        
                    }, <%=certificateNoTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }, <%=folioNoTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }, <%=DictincttivefromTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }, <%=DictincttivetoTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 12
                    }
              
          }, messages: {
                      <%=fundcodeTextBox.UniqueID %>:{  
                         required: "*Fund Code is required*",
                         maxlength: "* Please enter maximum 2 characters *"
                      },
                     <%=companyCodeTextBox.UniqueID %>:{  
                         required: "*Company Code is required*",
                         maxlength: "* Please enter maximum 3 characters *"
                      }, <%=psdrNoTextBox.UniqueID %>:{  
                         required: "*PHDR No is required*",
                         maxlength: "* Please enter maximum 6 characters *"
                      },<%=RIssuefromTextBox.UniqueID %>:{  
                       required: "*From Date  is required*",
                       
                     }, <%=noofshareTextBox.UniqueID %>:{  
                         required: "*No of Share is required*",
                         maxlength: "* Please enter maximum 18 characters *"
                      },<%=certificateNoTextBox.UniqueID %>:{  
                          required: "*Certificate No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      },<%=folioNoTextBox.UniqueID %>:{  
                          required: "*Folio  No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      },<%=DictincttivefromTextBox.UniqueID %>:{  
                          required: "*Dictincttive From  No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      },<%=DictincttivetoTextBox.UniqueID %>:{  
                          required: "*Dictincttive To  No is required*",
                         maxlength: "* Please enter maximum 12 characters *"
                      }
                
                }
      });
     
    </script>
</asp:Content>


