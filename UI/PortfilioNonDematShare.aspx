<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PortfilioNonDematShare.aspx.cs" Inherits="UI_BalancechekReport" Title="Portfolio Non Demat Shares" %>
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
                    <b><u>Portfolio Non Demat Shares</u></b>
                </td>
            </tr>
            </table>
    
             <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
                 <tr>
                     <td align="right" style="font-weight: 700"><b>Fund Name:</b></td>
                     <td align="left" width="200px">
                         <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="6"
                             AutoPostBack="True">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td align="right" style="font-weight: 700" class="style5"><b>Portfolio As On:</b></td>
                     <td align="left">
                         <asp:DropDownList ID="PortfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
                         <span class="style6">*</span>
                     </td>
           </tr>
                 <tr>

                     <td align="center" colspan="2">
                         <asp:Button ID="showButton" runat="server" Text="Show Report"
                             CssClass="buttoncommon" TabIndex="5" OnClick="showButton_Click" />

                     </td>

                 </tr>
        </table>
        </div>
  
         
    <script type="text/javascript">
        $.validator.addMethod("fundNameDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Fund");   
        $.validator.addMethod("PortfolioAsOnDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Balance Date"); 

      $("#aspnetForm").validate({
          rules: {
                     <%=fundNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                     fundNameDropDownList:true
                        
                    },<%=PortfolioAsOnDropDownList.UniqueID %>: {
                        
                        PortfolioAsOnDropDownList: true
                    }
              
                }
              
              
      });
     
    </script>
</asp:Content>

