<%@ Page Language="C#"  MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CapitalGainCompanyWise.aspx.cs" Inherits="UI_BalancechekReport" Title="Capital Gain (Company Wise)" %>
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
            <td class="FormTitle" align="center">Capital Gain (Company Wise)</td>
            <td>
                <br />
            </td>
        </tr>

    </table>
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
        //$("#txtFrom").datepicker({
        //    //numberOfMonths: 2,
        //    dateFormat: 'dd-M-yy',
        //    onSelect: function (selected) {
        //        var dt = new Date(selected);
        //        dt.setDate(dt.getDate() + 1);
        //        $("#txtTo").datepicker("option", "minDate", dt);
        //    }
        //});
        //$("#txtTo").datepicker({
        //    // numberOfMonths: 2,
        //    dateFormat: 'dd-M-yy',
        //    onSelect: function (selected) {
        //        var dt = new Date(selected);
        //        dt.setDate(dt.getDate() - 1);
        //        $("#txtFrom").datepicker("option", "maxDate", dt);
        //    }
        //});
       // $('#" + textBox.ClientID + "').datepicker(
       <%-- $('#"<%=RIssuefromTextBox.ClientID %>"').datepicker({
            // numberOfMonths: 2,
            dateFormat: 'dd-M-yy',
            onSelect: function (selected) {
                var dt = new Date(selected);
                dt.setDate(dt.getDate() - 1);
                //$("#txtFrom").datepicker("option", "maxDate", dt);
            }
        });--%>

<%--        $('#<%=RIssuefromTextBox.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
               // yearRange: "2030:-2002",
               // defaultDate: new Date(1980, 01, 01),
                dateFormat: 'dd-M-yy',
                onSelect: function (selected) {
                    var dt = new Date(selected);
                    dt.setDate(dt.getDate() - 1);
                    $('#<%=RIssueToTextBox.ClientID%>').datepicker("option", "minDate", dt);
                }
        });

          $('#<%=RIssueToTextBox.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
               // yearRange: "-100:-17",
               // defaultDate: new Date(1980, 01, 01),
                dateFormat: 'dd-M-yy',
                onSelect: function (selected) {
                    var dt = new Date(selected);
                    dt.setDate(dt.getDate() - 1);
                    $('#<%=RIssuefromTextBox.ClientID%>').datepicker("option", "maxDate", dt);
                }
            });--%>
       
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
                   
                   <%=companyNameDropDownList.UniqueID %>: {
                        
                        //required:true 
                        companyNameDropDownList:true
                        
                    },<%=RIssuefromTextBox.UniqueID %>: {
                        
                        required: true
                       
                    },<%=RIssueToTextBox.UniqueID %>: {
                        
                        required: true
                    }
              
                }, messages: {
                    <%=RIssuefromTextBox.UniqueID %>:{  
                       required: "*From Date  is required*"
                     
                   },<%=RIssueToTextBox.UniqueID %>:{  
                       required: "*To Date  is required*"
                   }
                }
      });

    </script>
</asp:Content>


