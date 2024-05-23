<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CapitalGainAllFundsReport.aspx.cs" Inherits="UI_MarketValuationWithProfitLoss" Title="Capital Gain All Funds" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
            .style4
            {
                font-family: Verdana, Arial, Helvetica, sans-serif;
                font-size: 17px;
                color: #08559D;
                FONT-WEIGHT: bold;
                text-align: center;
                background-image: url('../image/titlebg.gif');
                text-decoration: underline;
            }
            .style5
            {
                width: 123px;
                text-align: right;
            }
            .style6
            {
                color: #FF0066;
            }
        </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />      
        
    <table width="600" align="center" cellpadding="0" cellspacing="0" >
        <tr>
        <td align="center" class="style4">Capital Gain All Funds</td>  
        </tr>
    </table>
    
    <br />
    <br />
    <br />
    
    <table align="center" >
        <tr>
                
            <td>
                 
                <div style="height:300px; width:400px; overflow:auto;" id="dvGridFund" runat="server">
                        <!--- Following code renders the checkboxes and a label control on browser --->
 
                <asp:CheckBox ID="chkAll" Text="Select All" runat="server" />
                <asp:CheckBoxList ID="chkFruits" runat="server">
                        
                </asp:CheckBoxList>

                    </div>
            </td>
        </tr>
    </table> 
    <br />
    <br />
    <table width="600" align="center" cellpadding="0" cellspacing="0" >
    <colgroup width="100"></colgroup>
       <%-- <tr>
            <td align="right" style="font-weight: 700" class="style5"><b>Portfolio As On:</b></td>
            <td align="left">
                <asp:DropDownList ID="PortfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
                <span class="style6">*</span>
            </td>
        </tr>--%>
        <tr>
        <td align="right" style="font-weight: 700"><b>From Date:</b></td>
        <td align="left">
            <asp:TextBox ID="FromDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="FromDateTextBox"
                PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>
        
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>To Date</b></td>
            <td align="left">
                <asp:TextBox ID="ToDateTextBox" runat="server" style="width:100px;" 
                    CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="ToDateTextBox"
                    PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" /></td>



        </tr>
           
           
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
            
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
            
        <tr>
            <td align="center" class="style5">&nbsp;</td>
            <td align="left">
                <asp:Button ID="showReportButton" runat="server" CssClass="buttoncommon" 
                    OnClientClick="return fnConfirm();" Text="Show Report" Width="78px" onclick="showReportButton_Click" 
                    />&nbsp;&nbsp;&nbsp;
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
</script>    
  
</asp:Content>

