<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CompNonListedMArketPriceEntryForm.aspx.cs" Inherits="UI_NonListedSecuritiesInvestmentEntryForm" Title="Non Listed Securites (Investment) Entry Page" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="Stylesheet" type="text/css" href="../Scripts/jquery-ui.css"  />
    <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />
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
     <style type="text/css">
         .Gridview {
             font-family: Verdana;
             font-size: 10pt;
             font-weight: normal;
             color: black;
         }

         .processBtn {
             BORDER-TOP: #CCCCCC 1px solid;
             BORDER-BOTTOM: #000000 1px solid;
             BORDER-LEFT: #CCCCCC 1px solid;
             BORDER-RIGHT: #000000 1px solid;
             COLOR: #FFFFFF;
             FONT-WEIGHT: bold;
             FONT-SIZE: 11px;
             BACKGROUND-COLOR: #547AC6;
             WIDTH: 146px;
             HEIGHT: 35px;
             font-size: 21px;
             border-radius: 25px;
         }
     </style>
     <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

&nbsp;&nbsp;&nbsp;
<table "text-align"="center">
    <tr>
        <td class="FormTitle" align="center">
            NON LISTED SECURITIES MARKET PRICE UPDATE
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>

<table width="750" "text-align"="center" cellpadding="0" cellspacing="0" border="0">
    
    
     <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Company Name:&nbsp; </b></td>
        <td align="left" class="style5" >
            <asp:DropDownList ID="nonlistedCompanyDropDownList"    OnSelectedIndexChanged="nonlistedCompanyDropDownList_SelectedIndexChanged"  AutoPostBack="true"  runat="server" TabIndex="1" ></asp:DropDownList>
            </td>
    </tr>
   

     <tr>
        <td align="right" style="font-weight: 700" class="style5">
            <asp:Label ID="LabelInvestmentDate"  runat="server" Text="Transaction Date:"></asp:Label></td>
        <td align="left" class="style5" >
            <asp:TextBox ID="InvestMentDateTextBox" runat="server" Width="100px"></asp:TextBox>
             <asp:ImageButton ID="InvestMentDateImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
                 <ajaxToolkit:CalendarExtender ID="InvestMentDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="InvestMentDateImageButton" TargetControlID="InvestMentDateTextBox"></ajaxToolkit:CalendarExtender>

            </td>
    </tr>

    
    <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Market Price:&nbsp;</b></td>
        <td align="left" class="style5" >
            <asp:TextBox ID="rateTextBox" runat="server"  style="width:100px;" 
                CssClass="textInputStyleammount" TabIndex="2" 
                ></asp:TextBox></td>   
        
    </tr>
      <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>NAV:&nbsp;</b></td>
        <td align="left" class="style5" >
            <asp:TextBox ID="NavTextBox" runat="server"  style="width:100px;" 
                CssClass="textInputStyleammount" TabIndex="2" 
                ></asp:TextBox></td>   
        
    </tr>
        <tr>
            <td align="center" colspan="2"><asp:Button ID="saveButton" runat="server"  Text="Save" OnClick="saveButton_Click" CssClass="buttoncommon" TabIndex="5" />
                 <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="6" onclick="resetButton_Click"/>
                  <asp:Button ID="ButtonDelete" runat="server" Text="Delete" 
                CssClass="buttoncommon" TabIndex="6" onclick="ButtonDelete_Click"  onclientclick="fnDeleteCloseModal();" />
               

            </td>
            <td align="center" colspan="2" >
            <%--<asp:Button ID="saveButton" runat="server" Text="Save" 
                CssClass="buttoncommon" TabIndex="5" 
                     AccessKey="s" onclick="saveButton_Click"  
                    />--%>
                
            </td>
          <td align="center" colspan="2" >
            <asp:HiddenField ID="HiddenField2" runat="server" />
            </td>
            
    </tr>
</table>

    <script type="text/javascript">

 
        $.validator.addMethod("companycheck", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"* Please select a Company");


        //jQuery.validator.addMethod("Date", function(value, element) { 
        //    return Date.parseExact(value, "dd/mm/yy");
        //});

<%--        $(function () {

            $('#<%=InvestMentDateTextBox.ClientID%>').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                maxDate: 'today',
                onSelect: function(selected) {
                   
                }
            });
      
        });--%>
    
        $("#aspnetForm").validate({
            rules: {
             
                <%=InvestMentDateTextBox.UniqueID %>: {
                        
                   required:true ,
                   Date:true
                        
               }
               ,
                <%=nonlistedCompanyDropDownList.UniqueID %>: {
                        
                   //required:true 
                   companycheck:true
                        
               }
               
              
               
              
            }
        });

    </script>       
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <table class="table table-hover" style=" border: 1px solid black;" id="bootstrap-table">
                <thead>
                    <tr>
                        
                        <th>Company Code</th>
                        <th>Company Name</th>
                         <th>Transaction Date</th>
                        <th>Market Rate</th>
                        <th>NAV</th>
                        <th>Action</th>
                       
                    </tr>
                </thead>
                <tbody>
                    <%

                        System.Data.DataTable dtallNonlistedComapny = (System.Data.DataTable)Session["ALLCompanyInformationNonListed"];
                        if (dtallNonlistedComapny.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtallNonlistedComapny.Rows.Count; i++)
                            {
                    %>
                    <tr>
                        <td><%   Response.Write(dtallNonlistedComapny.Rows[i]["COMP_CD"].ToString());   %> </td>
                        <td><%   Response.Write(dtallNonlistedComapny.Rows[i]["COMP_NM"].ToString());   %> </td>
                         <td><%   Response.Write( Convert.ToDateTime(dtallNonlistedComapny.Rows[i]["TRAN_DATE"].ToString()).ToString("dd-MMM-yyyy"));   %> </td>
                        <td><%   Response.Write(dtallNonlistedComapny.Rows[i]["MARKET_RATE"].ToString());   %> </td>
                         <td><%   Response.Write(dtallNonlistedComapny.Rows[i]["NAV"].ToString()); %> </td>


                        <td><a href="CompNonListedMArketPriceEntryForm.aspx?COMP_CD=<%Response.Write(dtallNonlistedComapny.Rows[i]["COMP_CD"].ToString());   %> &TRAN_DATE=<%Response.Write(dtallNonlistedComapny.Rows[i]["TRAN_DATE"].ToString());%> &MARKET_RATE=<%Response.Write(dtallNonlistedComapny.Rows[i]["MARKET_RATE"].ToString());%>&NAV=<%Response.Write(dtallNonlistedComapny.Rows[i]["NAV"].ToString());%> " 
                            class="custUpdBtn">Select</a></td>
                    </tr>
                    <%

                            }
                        }

                    %>
                 
                </tbody>
            </table>
         <script type="text/javascript">
          $(document).ready(function () {
              $('#bootstrap-table').bdt({

              });
          });
        function fnDeleteCloseModal() {
        
        if (confirm("Are you sure you really want to delete.....?")) {
            //  $("#HiddenField1").val("Yes");
            $("#<%=HiddenField2.ClientID%>").val("Yes");
        } else {
            // $("#HiddenField1").val("No");
            $("#<%=HiddenField2.ClientID%>").val("No");
        }
    }
         
    </script>
               
</asp:Content>




