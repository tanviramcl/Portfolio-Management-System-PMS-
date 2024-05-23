<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="DividendReceivable.aspx.cs" Inherits="UI_BookCloserEntry" Title="Receivable Cash Dividend Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">

        function fnClearFields() {
            var Check = confirm("Are You Sure To Clear");
            if (Check) {
     <%--           document.getElementById("<%=companyCodeTextBox.ClientID%>").value = "";
                document.getElementById("<%=companyNameDropDownList.ClientID%>").value = "";
                document.getElementById("<%=financialYearTextBox.ClientID%>").value = "";
                document.getElementById("<%=recordDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=bookToTextBox.ClientID%>").value = "";
                document.getElementById("<%=stockTextBox.ClientID%>").value = "";
                document.getElementById("<%=rightTextBox.ClientID%>").value = "";
                document.getElementById("<%=rightApprovalDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=cashTextBox.ClientID%>").value = "";
                document.getElementById("<%=agmDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=remarksTextBox.ClientID%>").value = "";
                document.getElementById("<%=postedDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=TextBoxReceivable.ClientID%>").value = "";
                document.getElementById("<%=postedTextBox.ClientID%>").value = "";--%>
                return false;
            }

        }
        function fnCheckSearch() {
            if (document.getElementById("<%=companyNameDropDownList.ClientID%>").value == "") {
                alert("Please Select a Company Name To Search");
                document.getElementById("<%=companyNameDropDownList.ClientID%>").focus();
        return false;

    }
   <%-- if (document.getElementById("<%=financialYearTextBox.ClientID%>").value == "") {
                alert("Please Enter Financial year To Search");
                document.getElementById("<%=financialYearTextBox.ClientID%>").focus();
        return false;
    }--%>
}
function fncInputNumericValuesOnly() {
    if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
        event.returnValue = false;
    }
}
function fnCheckInput() {
    var checkDate = /^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
    if (document.getElementById("<%=recordDateTextBox.ClientID%>").value == "") {
         document.getElementById("<%=recordDateTextBox.ClientID%>").focus();
         alert("Please Insert Reord Date.");
         return false;
     }
     if (document.getElementById("<%=recordDateTextBox.ClientID%>").value != "") {
         if (!checkDate.test(document.getElementById("<%=recordDateTextBox.ClientID%>").value)) {
            document.getElementById("<%=recordDateTextBox.ClientID%>").focus();
            alert("Invalid Date Format! Select Date From The Calendar.");
            return false;
        }
    }
    <%--if (document.getElementById("<%=bookToTextBox.ClientID%>").value != "") {
         if (!checkDate.test(document.getElementById("<%=bookToTextBox.ClientID%>").value)) {
             document.getElementById("<%=bookToTextBox.ClientID%>").focus();
            alert("Invalid Date Format! Select Date From The Calendar.");
            return false;
        }
    }--%>
    if (document.getElementById("<%=agmDateTextBox.ClientID%>").value == "") {
         document.getElementById("<%=agmDateTextBox.ClientID%>").focus();
         alert("Please Insert AGM Date. If AGM Date is not available then AGM Date will be Same as Record Date.");
         return false;
     }
     if (document.getElementById("<%=agmDateTextBox.ClientID%>").value != "") {
         if (!checkDate.test(document.getElementById("<%=agmDateTextBox.ClientID%>").value)) {
                document.getElementById("<%=agmDateTextBox.ClientID%>").focus();
            alert("Invalid Date Format! Select Date From The Calendar.");
            return false;
        }
    }
    <%--if ((document.getElementById("<%=stockTextBox.ClientID%>").value == "") && (document.getElementById("<%=cashTextBox.ClientID%>").value == "")) {
         document.getElementById("<%=stockTextBox.ClientID%>").focus();
         alert("Please Insert Stock/Cash Dividend Rate.");
         return false;
     }--%>

 }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
 
                    <table id="Table1" width="300" align="center" cellpadding="0" cellspacing="0" runat="server">
                        <tr>
                            <td align="center" class="style3">
                                <b><u>Receivable Cash Dividend Entry</u></b>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
                        <tr>
                            <td align="right">
                                <b>Fund Code</b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="FundCodeTextBox" runat="server"  readOnly="true" Width="27px" ></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td align="right">
                                <b>Fund Name </b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="fundDropDownList"  runat="server" AutoPostBack="True"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>

                      
                        <tr>
                            <td align="right">
                                <b>Company Code</b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="companyCodeTextBox"  runat="server" Width="27px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>Company Name </b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="companyNameDropDownList"  readOnly="true" runat="server" AutoPostBack="True"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        

                        <tr>
                            <td align="right" style="font-weight: 700">Record Date
                            </td>
                            <td align="left">
                                <asp:TextBox ID="recordDateTextBox" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
                                <asp:ImageButton ID="recordDateImageButton" runat="server"
                                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
                                <ajaxToolkit:CalendarExtender ID="recordDateCalendarExtender"  runat="server" Format="dd-MMM-yyyy" PopupButtonID="recordDateImageButton" TargetControlID="recordDateTextBox"></ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>

                        <tr>
                            <td align="right" style="font-weight: 700">AGM Date
                            </td>
                            <td align="left">
                                <asp:TextBox ID="agmDateTextBox" runat="server" Width="80px" TabIndex="8"></asp:TextBox>
                                <asp:ImageButton ID="agmDateImageButton" runat="server"
                                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="9" />
                                <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server"  Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                           <tr>
                            <td align="right">No. of Share
                            </td>
                            <td align="left">
                                <asp:TextBox ID="NoofSahreTextBox"  readOnly="true" runat="server" Width="81px" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">Gross Dividend
                            </td>
                            <td align="left">
                                <asp:TextBox ID="gross_dividend_TextBox" readOnly="true" runat="server" Width="81px" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">Tax
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Tax_TextBox"  readOnly="true" runat="server" Width="81px" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Net Dividend
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Net_dividend_TextBox" readOnly="true" runat="server" Width="81px" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Dividend Received
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBox_divi_receivable" runat="server" OnTextChanged="divi_receivable_TextBox_TextChanged" AutoPostBack="true" Width="81px" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                             <tr>
                            <td align="right">Due/Excess Dividend
                            </td>
                            <td align="left">
                                <asp:TextBox ID="DueExcessTextBox" runat="server" Width="81px" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Dividend Receivable Date</td>


                            <td align="left">
                                <asp:TextBox ID="TextBox_divi_receivable_date" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
                                <asp:ImageButton ID="divi_receivable_date_ImageButton" runat="server"
                                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
                                <ajaxToolkit:CalendarExtender ID="divi_receivable_date_CalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="divi_receivable_date_ImageButton" TargetControlID="TextBox_divi_receivable_date"></ajaxToolkit:CalendarExtender>
                            </td>


                        </tr>
                        <tr>
                            <td align="right">Remarks
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBox_RemarksDividend_Receive" runat="server" TextMode="MultiLine" Width="248px"
                                    TabIndex="15"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center"></td>
                            <td align="left">

                                <asp:Button ID="addNewButton" runat="server" Text="Add New" CssClass="buttoncommon"
                                    OnClick="addNewButton_Click" TabIndex="19" AccessKey="s" />
                                 <asp:Button ID="updateButton" runat="server" Text="Update" CssClass="buttoncommon" 
                                    OnClick="updateButton_Click" Visible="False" TabIndex="19" AccessKey="s" />
                                <asp:Button ID="clearButton" runat="server" Text="Clear" CssClass="buttoncommon"
                                    OnClientClick="return fnClearFields();" OnClick="clearButton_Click"
                                    TabIndex="20" />

                            </td>

                           
                        </tr>
                    </table>
                   
   
</asp:Content>

