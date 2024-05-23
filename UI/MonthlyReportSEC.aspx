<%@ Page Language="C#"  MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="MonthlyReportSEC.aspx.cs" Inherits="UI_BalancechekReport" Title="Monthly" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        }
        .ui-datepicker {
       
        }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />      
        
    <table width="600" align="center" cellpadding="0" cellspacing="0" >
        <tr>
        <td align="center" class="style4">Monthly Transaction Report to SEC Form</td>  
        </tr>
    </table>
    
    <br />
    <br />
    <br />
    
    <table align="center" >
        <tr>
                
           <td align="right">Statement Type:&nbsp; </td>

            <td align="left">
                <asp:RadioButton ID="tbl2"  runat="server" Text="Break-up of Investment" Checked="true"
                    GroupName="statementType" Font-Bold="true" AutoPostBack="true"  TabIndex="2" OnCheckedChanged="radio_CheckedChanged" />
                <asp:RadioButton ID="tbl3" runat="server" Text="Scheme wise monthly turnover" 
                    GroupName="statementType" Font-Bold="true" TabIndex="1"  AutoPostBack="true"  OnCheckedChanged="radio_CheckedChanged"/>&nbsp;&nbsp;
               <asp:RadioButton ID="tbl6" runat="server" Text="Investment in other MF during this Month"
                     GroupName="statementType" Font-Bold="true"  AutoPostBack="true" TabIndex="2" OnCheckedChanged="radio_CheckedChanged" />
                
           </td>
        </tr>
    </table> 
    <br />
    <br />
    <table width="600" align="center" cellpadding="0" cellspacing="0" >
    <colgroup width="100"></colgroup>


       
            <tr>
            <td align="right">
                <b> <asp:Label ID="LabelFromDate" Visible="false" style="font-weight: 700" runat="server" Text="From Date:"></asp:Label></b>
            </td>
            <td align="left">
          
                <asp:TextBox ID="RIssuefromTextBox" Visible="false" runat="server" Width="100px"></asp:TextBox>
            </td>
       </tr>
        <tr>
            <td align="right">
                <b><asp:Label ID="LabelToDte" Visible="false" style="font-weight: 700" runat="server" Text="To Date:"></asp:Label></b>
            </td>
         
            <td align="left">
            
                <asp:TextBox ID="RIssueToTextBox" Visible="false" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>       
           <tr>
        <td align="right" style="font-weight: 700"><b><asp:Label ID="LabelPfolioAsOn" Visible="true" style="font-weight: 700" runat="server" Text="Portfolio As On:"></asp:Label>&nbsp; </b></td>
        <td align="left">
            <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server"  TabIndex="8"></asp:DropDownList>
            </td>
    </tr>
         <tr>
        <td align="right" style="font-weight: 700"><b><asp:Label ID="LabelPreviousMonth" Visible="false" style="font-weight: 700" runat="server" Text="Previous Month-End Date:"></asp:Label>&nbsp; </b></td>
        <td align="left">
            <asp:DropDownList ID="portfolioPreviousMonthDropDownList"  Visible="false"  runat="server" TabIndex="8"></asp:DropDownList>
            </td>
    </tr>
      
       
            
        <tr>
            <td align="center" class="style5">&nbsp;</td>
            <td align="left">
                <asp:Button ID="showButton" runat="server" Text="Show Report"
                    CssClass="buttoncommon" TabIndex="5" OnClick="showButton_Click" />&nbsp;&nbsp;&nbsp;
                                </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
            <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
    </table>
       
    <br />
    <br /> 


      <script type="text/javascript">

    $(function () {

          $('#<%=RIssuefromTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
              //  dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {
                     $('#<%=RIssueToTextBox.ClientID%>').datepicker("option","minDate", selected)
                 }
             });
             $('#<%=RIssueToTextBox.ClientID%>').datepicker({ 
                 changeMonth: true,
                 changeYear: true,
               //  dateFormat: "dd/mm/yy",
                 maxDate:"today",
                 onSelect: function(selected) {

                    

                 }
             });  




    });

          $.validator.addMethod("portfolioasonDate", function (value, element, param) {
              if (value == '0')
                  return false;
              else
                  return true;
          }, "* Please Enter a Date");

          $.validator.addMethod("pfolioPreviousDate", function (value, element, param) {
              if (value == '0')
                  return false;
              else
                  return true;
          }, "* Previous Month-End Date");

              $("#aspnetForm").validate({
        rules: {
                   
                    <%=RIssuefromTextBox.UniqueID %>: {
                        
                        required: true,
                        
                    },<%=RIssueToTextBox.UniqueID %>: {
                        
                        required: true,
                       
                    },<%=portfolioAsOnDropDownList.UniqueID %>: {
                        
                        //required:true 
                        portfolioasonDate:true
                        
                    }, <%=portfolioPreviousMonthDropDownList.UniqueID %>: {
                        
                        //required:true 
                        pfolioPreviousDate:true
                        
                    }
              
                }, messages: {
                   <%=RIssuefromTextBox.UniqueID %>:{  
                       required: "*From Date  is required*",
                       
                   },<%=RIssueToTextBox.UniqueID %>:{  
                       required: "*To Date  is required*",
                      
                   }
                    
                    
            
                
                }
      });
    </script>
  
</asp:Content>



