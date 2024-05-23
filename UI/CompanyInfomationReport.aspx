<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CompanyInfomationReport.aspx.cs" Inherits="UI_BalancechekReport" Title="Stock Declaration Before Posted" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        }  
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
    <table style="text-align: center">
        <tr>
            <td class="FormTitle" align="center">Company Information Report</td>
            <td>
                <br />
            </td>
        </tr>

    </table>


    <table width="750" style="text-align: center" cellpadding="0" cellspacing="0" border="0">
        <tr>

            <td >
                <b>Sector </b>
            </td>
            <td align="left">
                <%-- <asp:TextBox ID="sectorTextBox" runat="server" Width="100px"></asp:TextBox>--%>
                <asp:DropDownList ID="sectorDropDownList" runat="server" TabIndex="3"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td >
                <b>Category </b>
            </td>
            <td align="left">
                <asp:DropDownList ID="categoryDropDownList" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Share" Value="SH"></asp:ListItem>
                    <asp:ListItem Text="Mutual Fund" Value="DB"></asp:ListItem>
                    <asp:ListItem Text="S" Value="S"></asp:ListItem>
                    <asp:ListItem Text="L" Value="L"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td >
                <b>Group type</b>
            </td>
            <td align="left">
                <asp:DropDownList ID="groupDropDownList" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="A Group" Value="A"></asp:ListItem>
                    <asp:ListItem Text="B Group" Value="B"></asp:ListItem>
                    <asp:ListItem Text="G Group" Value="G"></asp:ListItem>
                    <asp:ListItem Text="N Group" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Z Group" Value="Z"></asp:ListItem>


                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td >
                <b>IPO type</b>
            </td>
            <td align="left">
                <asp:DropDownList ID="IPODropDownList" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Fixed Price" Value="Fixed price"></asp:ListItem>
                    <asp:ListItem Text="Book building" Value="Book building"></asp:ListItem>
                    <asp:ListItem Text="Direct Listing" Value="Direct Listing"></asp:ListItem>


                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="">
                <b>Market type</b>
            </td>
            <td align="left">
                <asp:DropDownList ID="marketDropDownList" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="Regular" Value="R"></asp:ListItem>
                    <asp:ListItem Text="OTC" Value="O"></asp:ListItem>
                    <asp:ListItem Text="Debt Market" Value="D"></asp:ListItem>


                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            
           <td align="center" colspan="4" >
                &nbsp;
           </td>
         </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="showButton" runat="server" Text="Show Report"
                    CssClass="buttoncommon" TabIndex="5" OnClick="showButton_Click" />
            </td>
        </tr>
    </table>
     <script type="text/javascript">
    

      


<%--        $.validator.addMethod("CheckDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Category."); 

        $.validator.addMethod("CheckSrcorDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select a Sector."); 

        $.validator.addMethod("CheckmarketType", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select Market Type."); 
        $.validator.addMethod("CheckIpoType", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select IPO Type."); 

        $.validator.addMethod("CheckgROUPType", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select Group Type."); 
        
       <%-- $( "#<%=TextBoxAuthorizedcapital.ClientID%>" ).change(function() {
            alert( "Handler for .change() called." );
            $('#<%=TextBoxAuthorizedcapital.ClientID%>').prop('maxLength', 10);
        });--%>
      
   <%--  $("#aspnetForm").validate({
            submitHandler: function () {
                test();
            },
          rules: {
                   <%=sectorDropDownList.UniqueID %>: {
                        required: true,
                        CheckSrcorDropDownList: true
                    },<%=categoryDropDownList.UniqueID %>: {
                        
                        required:true, 
                        CheckDropDownList:true
                        
                    }
                    ,<%=groupDropDownList.UniqueID %>: {
                        
                        required:true, 
                        CheckgROUPType:true
                        
                    },

                  <%=IPODropDownList.UniqueID %>: {
                        required: true,
                        CheckIpoType:true
                    },<%=marketDropDownList.UniqueID %>: {
                        required: true,
                        CheckmarketType:true
                    }
              
                }
      });--%>
       
    </script>
</asp:Content>
