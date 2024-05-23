<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="Cash at Bank.aspx.cs" Inherits="UI_CashatBank" Title="Cash at Bank Entry Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">

        function fnClearFields() {
            var Check = confirm("Are You Sure To Clear");
            if (Check) {

                document.getElementById("<%=EntryDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=fundNameDropDownList.ClientID%>").value = "";
                document.getElementById("<%=DropDownListBank.ClientID%>").value = "";
                document.getElementById("<%=TextBox1.ClientID%>").value = "";
                document.getElementById("<%=TextBox_ACC_NUMBER.ClientID%>").value = "";
                document.getElementById("<%=TextBoxRATE_OF_INTEREST.ClientID%>").value = "";
                document.getElementById("<%=TextBox_AVAILABLE_BALANCE.ClientID%>").value = "";
                document.getElementById("<%=TextBox_MARKET_VALUE.ClientID%>").value = "";

                return false;
            }

        }
        function fnCheckSearch()
        {

            if (document.getElementById("<%=fundNameDropDownList.ClientID%>").value == "")
            {
                alert("Please Select a Fund");
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                return false;

            }
            if (document.getElementById("<%=DropDownListBank.ClientID%>").value == "")
            {
                alert("Please Select a Bank");
                document.getElementById("<%=DropDownListBank.ClientID%>").focus();
                return false;

            }
        }
function fncInputNumericValuesOnly() {
    if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
        event.returnValue = false;
    }
}
function fnCheckInput() {
    var checkDate = /^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
    if (document.getElementById("<%=EntryDateTextBox.ClientID%>").value == "") {
        document.getElementById("<%=EntryDateTextBox.ClientID%>").focus();
        alert("Please Insert a  Date.");
        return false;
    }
    if (document.getElementById("<%=EntryDateTextBox.ClientID%>").value != "") {
        if (!checkDate.test(document.getElementById("<%=EntryDateTextBox.ClientID%>").value)) {
                document.getElementById("<%=EntryDateTextBox.ClientID%>").focus();
             alert("Invalid Date Format! Select Date From The Calendar.");
             return false;
         }
     }


 }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
    <%--<style type="text/css">
        .style3
        {
            color: #000066;
            font-size: x-large;
        }
    </style>--%>
    <div id="dvUpdatePanel" runat="server">
        <asp:UpdatePanel ID="BOHolderInfoUpdatePanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Width="800">
                    <br />
                    <table id="Table1" width="600" align="center" cellpadding="0" cellspacing="0" runat="server">
                        <tr>
                            <td align="center" class="style3">
                                <b><u>Cash at Bank Entry Form </u></b>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">

                        <tr>
                            <td align="right">
                                <b>FUND NAME </b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="fundNameDropDownList" runat="server"
                                    OnSelectedIndexChanged="fundNameDropDownList_SelectedIndexChanged"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>BANK  NAME </b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownListBank" runat="server"
                                    OnSelectedIndexChanged="BankNameDropDownList_SelectedIndexChanged"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </tr>
         <tr>
             <td align="right" style="font-weight: 700">ACCOUNT NUMBER
             </td>
             <td align="left">
                 <asp:TextBox ID="TextBox_ACC_NUMBER" runat="server" TabIndex="10" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
             </td>

         </tr>

                        <tr>
                            <td align="right" style="font-weight: 700">NATURE OF ACCOUNT
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBox1" runat="server" Width="63px" TabIndex="11"></asp:TextBox>%
                            </td>
                        </tr>

                        <tr>
                            <td align="right" style="font-weight: 700">RATE_OF_INTEREST
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxRATE_OF_INTEREST" runat="server" Width="63px" TabIndex="11" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td align="right">AVAILABLE_BALANCE
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBox_AVAILABLE_BALANCE" runat="server" onkeypress="fncInputNumericValuesOnly()" TabIndex="12"></asp:TextBox>
                            </td>
                        </tr>
                        </tr>
        <tr>
            <td align="right">MARKET_VALUE
            </td>
            <td align="left">
                <asp:TextBox ID="TextBox_MARKET_VALUE" runat="server" onkeypress="fncInputNumericValuesOnly()" TabIndex="12"></asp:TextBox>
            </td>
        </tr>

                        <tr>
                            <td align="right" style="font-weight: 700">Entry Date
                            </td>
                            <td align="left">
                                <asp:TextBox ID="EntryDateTextBox" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
                                <asp:ImageButton ID="EntryDateImageButton" runat="server"
                                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
                                <ajaxToolkit:CalendarExtender ID="EntryDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="EntryDateImageButton" TargetControlID="EntryDateTextBox"></ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">

                                <asp:Button ID="addNewButton" runat="server" Text="Save"
                                   OnClientClick="return fnCheckSearch();"   OnClick="addNewButton_Click"  TabIndex="19" AccessKey="s" />
                                <asp:Button ID="updateButton" runat="server" Text="Update"
                                    OnClick="updateButton_Click" Visible="False" TabIndex="19" AccessKey="s" />

                            </td>
                            <td align="left">
                                <asp:Button ID="clearButton" runat="server" Text="Clear"
                                    OnClientClick="return fnClearFields();" OnClick="clearButton_Click"
                                    TabIndex="20" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />

                </asp:Panel>

            </ContentTemplate>
            <Triggers>

                <asp:AsyncPostBackTrigger ControlID="addNewButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="updateButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="clearButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

