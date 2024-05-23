<%@ Page Language="C#"  MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="InvestmentAnalysis.aspx.cs" Inherits="UI_BalancechekReport" Title="Investment Analysis" %>
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
            <td class="FormTitle" align="center">Investment Analysis</td>
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
                <asp:Label ID="fundNameDropDownListlabel" style="font-weight: 700" runat="server" Text="Fund Name:"></asp:Label>
            </td>
            <td align="left" width="200px">
                <asp:DropDownList ID="fundNameDropDownList"  runat="server" TabIndex="6"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td align="right">
                 <asp:Label ID="companyNameDropDownListlabel"  runat="server" Text="Company Name:"></asp:Label>
            </td>
            
            <td align="left">
                <asp:DropDownList ID="companyNameDropDownList"  runat="server" TabIndex="4"></asp:DropDownList>
            </td>
        </tr>
        <tr>
<%--             <td align="right">
                 <asp:Label ID="Label1"  runat="server" Text="Format:"></asp:Label>
            </td>--%>
            
           <%-- <td align="left">
                 <asp:dropdownlist runat="server"  class="form-control" AutoPostBack="true" id="ddlTest">
                         <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                        <asp:ListItem Text="Word" Value="Word"  />
                        <asp:ListItem Text="PDF" Value="PDF" />
                        <asp:ListItem Text="CSV" Value="CSV" />
                    </asp:dropdownlist>
            </td>--%>
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





    });

    $.validator.addMethod("fundDropDownList", function (value, element, param) {  
        if (value == '0')  
            return false;  
        else  
            return true;  
    },"* Please select a Fund");

    $.validator.addMethod("companyNameDropDownList", function (value, element, param) {  
        if (value == '0')  
            return false;  
        else  
            return true;  
    },"* Please select a company");

    
    $("#aspnetForm").validate({
        rules: {
                   <%=fundNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                        fundDropDownList:true
                        
                    }, <%=companyNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                        companyNameDropDownList:true
                        
                    }
              
                }, messages: {
                  
            
                
                }
      });

    </script>
</asp:Content>


