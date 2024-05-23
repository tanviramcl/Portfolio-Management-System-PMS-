<%@ Page Language="C#"  MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="DematComp.aspx.cs" Inherits="UI_BalancechekReport" Title="List of Security (Fund) For demat" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        }  
       .ui-datepicker {
        position: relative !important;
        top: -230px !important;
        left: 100px !important;
        margin-left: 390px;
        margin-top: -15px;
        }
    </style> 
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />--%>
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
    <table style="text-align: center">
        <tr>
            <td class="FormTitle" align="center">List of Security (Fund) For demat</td>
            <td>
                <br />
            </td>
        </tr>

    </table>
    <br />
    <br />
    <table style="text-align: center">
      <%--  <tr>
            <td align="right" style="font-weight: 700"><b>Fund Name:</b></td>
            <td align="left" width="200px">
                <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="6"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
             <asp:Label ID="lblheading"  style="color: red; display:inline-flex ;" Visible="false" runat="server" Text=""></asp:Label>
         </tr>
        <tr>
            <td align="right" style="font-weight: 700"></td>    
            <td align="left">
                 
                <div style="height:300px; width:400px; overflow:auto;" id="dvGridFund" runat="server">
                        <!--- Following code renders the checkboxes and a label control on browser --->
 
                <asp:CheckBox ID="chkAll" Text="Select All" runat="server" />
                <asp:CheckBoxList ID="chkFruits" runat="server">
                        
                </asp:CheckBoxList>

                    </div>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Company Name:&nbsp;&nbsp; </b></td>
            <td align="left">
                <asp:DropDownList ID="companyNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
            </td>
        </tr>
     

           <tr>
            <td align="right">
                <b>From Date:</b>
            </td>
            <td align="left">
               <%-- <input type="text" id="txtFrom" />--%><%--<img alt="d" id="txtFrom1" src="../Image/Calendar_scheduleHS.png" />--%>
                <asp:TextBox ID="RIssuefromTextBox" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td align="right">
                <b>To Date:</b>
            </td>
            <td align="left">
               <%-- <input type="text" id="txtTo" />--%><%--<img alt="d" id="txtFrom2" src="../Image/Calendar_scheduleHS.png" />--%>
                <asp:TextBox ID="RIssueToTextBox" runat="server" Width="100px"></asp:TextBox>
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
        $("[id*=chkAll]").bind("click", function () {
            if ($(this).is(":checked")) {
                $("[id*=chkFruits] input").prop("checked", "checked");
            } else {
                $("[id*=chkFruits] input").removeAttr("checked");
            }
        });
        $("[id*=chkFruits] input").bind("click", function () {
            if ($("[id*=chkFruits] input:checked").length == $("[id*=chkFruits] input").length) {
                $("[id*=chkAll]").prop("checked", "checked");
            } else {
                $("[id*=chkAll]").removeAttr("checked");
            }
        });
    });

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

                     <%--alert(selected);
                     $('#<%=RIssuefromTextBox.ClientID%>').datepicker("option","maxDate", selected)--%>


                 }
             });  



    });


    $.validator.addMethod("companyNameDropDownList", function (value, element, param) {  
        if (value == '0')  
            return false;  
        else  
            return true;  
    },"* Please select a company");

 
    
    $("#aspnetForm").validate({
        rules: {
                    <%=chkFruits.UniqueID%>: {
                        
                          required:true 
                     
                    },<%=companyNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                        companyNameDropDownList:true
                        
                    },<%=RIssuefromTextBox.UniqueID %>: {
                        
                        required: true
                       
                    },<%=RIssueToTextBox.UniqueID %>: {
                        
                        required: true
                    }
              
        }, messages: {
                    <%=chkFruits.UniqueID %>: {
                        
                        required: "*Please fund  is required*"
                    },
                    <%=RIssuefromTextBox.UniqueID %>:{  
                       required: "*From Date  is required*"
                     
                   },<%=RIssueToTextBox.UniqueID %>:{  
                       required: "*To Date  is required*"
                   }
                }
      });

       


    </script>
</asp:Content>


