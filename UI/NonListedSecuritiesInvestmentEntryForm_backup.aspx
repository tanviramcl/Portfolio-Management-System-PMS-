<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="NonListedSecuritiesInvestmentEntryForm_backup.aspx.cs" Inherits="UI_NonListedSecuritiesInvestmentEntryForm" Title="Non Listed Securites (Investment) Entry Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="../Scripts/jquery-ui.css"  />
    <script  type="text/javascript" src="../Scripts/jquery-ui.js"></script>
    <style type="text/css">
        label.error {
            color: red;
            display: inline-flex;
        }

        .style5 {
            height: 24px;
        }

        .style6 {
            height: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

&nbsp;&nbsp;&nbsp;
<table "text-align"="center">
    <tr>
        <td class="FormTitle" align="center">
            NON LISTED SECURITIES (Investment) ENTRY FORM
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" "text-align"="center" cellpadding="0" cellspacing="0" border="0">
    
    <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Fund Name:&nbsp; </b></td>
        <td align="left" class="style5" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="1" ></asp:DropDownList>
            </td>
    </tr>
    <tr>
       
        <td align="right" style="font-weight: 700" class="style5"><b>Investment Date:</b></td>
        <td align="left">
            <asp:DropDownList ID="PortfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
            <span class="style6">*</span>
        </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Amount(BDT):&nbsp;</b></td>
        <td align="left" class="style5" >
            <asp:TextBox ID="amountTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="2" 
                ></asp:TextBox></td>   
        
    </tr>
    
    <tr>
           <td align="left" class="style6" ></td>
    </tr>
    <tr>
        <td align="center" colspan="2" class="style5" ></td>
    </tr>
    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="saveButton" runat="server" Text="Save" 
                CssClass="buttoncommon" TabIndex="5" 
                     AccessKey="s" onclick="saveButton_Click"  OnClientClick="return Confirm();"
                    />
          <%-- <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnReset();"
                />--%>
            </td>
            
    </tr>
    <tr>
           <td align="center" colspan="4" >
                &nbsp;
           </td>
    </tr>
</table> 
<!-- for Diaglogbox-->    
<table style="text-align: center ; display:none">
<tr>
            
    <td>
        <div id="dialog-confirm" title="" >
            <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 5px 0;"></span>Do you want to Update. Are you sure?</p>
        </div>
      
    </td>
</tr>

</table>  


   <script type="text/javascript">
       $.validator.addMethod("fundDropDownList", function (value, element, param) {  
           if (value == '0')  
               return false;  
           else  
               return true;  
       },"* Please select a Fund");


       $.validator.addMethod("pfoliodatecheck", function (value, element, param) {  
           if (value == '0')  
               return false;  
           else  
               return true;  
       },"* Please select a Date");
 
    
       $("#aspnetForm").validate({
           rules: {
               <%=fundNameDropDownList.UniqueID %>: {
                        
                   //required:true 
                   fundDropDownList:true
                        
               }, <%=amountTextBox.UniqueID %>: {
                        
                   required:true ,
                   number:true              
                        
               },<%=PortfolioAsOnDropDownList.UniqueID %>: {
                        
                     pfoliodatecheck:true 
                        
                 }
              
           }
       });

    </script>
    
               
</asp:Content>

