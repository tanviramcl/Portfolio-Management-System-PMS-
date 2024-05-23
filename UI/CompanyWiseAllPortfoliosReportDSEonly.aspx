<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CompanyWiseAllPortfoliosReportDSEonly.aspx.cs" Inherits="UI_CompanyWiseAllPortfoliosReportDSEonly" Title="Company Wise All Portfolios Report Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
        function fnReset() {
            var confm = confirm("Are Sure To Reset?");
            if (confm) {
                CheckAllDataGridFundName(this.checked = false)
                document.getElementById("<%=howlaDateDropDownList.ClientID%>").value = "0";
                document.getElementById("<%=percentageTextBox.ClientID%>").value = "";

                return false;
            }
            else {
                return true;
            }
        }
        function fncInputNumericValuesOnly() {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function fncInputCommaAndNumericValuesOnly() {
            if (!(event.keyCode == 44 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }





        function fnConfirm() {
                if (document.getElementById("<%=howlaDateDropDownList.ClientID%>").value == "0") {
                            document.getElementById("<%=howlaDateDropDownList.ClientID%>").focus();
                            alert("Please Select Howla Date.");
                            return false;
                }
              if (document.getElementById("<%=listedCategoryDropDownList.ClientID%>").value == "0") {
                        document.getElementById("<%=listedCategoryDropDownList.ClientID%>").focus();
                         alert("Please Select a Listing Category.");
                        return false;
                    }

             }
    </script>
    <style type="text/css">
        .style4 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 17px;
            color: #08559D;
            FONT-WEIGHT: bold;
            text-align: center;
            background-image: url('../image/titlebg.gif');
            text-decoration: underline;
        }

        .style5 {
            width: 123px;
            text-align: right;
        }

        .style6 {
            color: #FF0066;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

    <table width="600" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" class="style4">Company Wise All Portfolios Report Form</td>
        </tr>
    </table>

    <br />
    <br />
    <br />



    <table align="center">
        <tr>

            <td>

                <div style="height: 300px; width: 400px; overflow: auto;" id="dvGridFund" runat="server">
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
    <table width="600" align="center" cellpadding="0" cellspacing="0">
        <colgroup width="100"></colgroup>
        <tr>
            <td align="right" style="font-weight: 700" class="style5"><b>Howla Date:</b></td>
            <td align="left">
                <asp:DropDownList ID="howlaDateDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
                <span class="style6">*</span>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700" class="style5"><b>Percentage Value:</b></td>
            <td align="left">
                <asp:TextBox ID="percentageTextBox" runat="server"
                    CssClass="textInputStyleDate" TabIndex="3" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox><span>%</span>
            </td>
        </tr>
        <tr>
            <td align="left" class="style5">Company Codes:</td>
            <td align="left">
                <asp:TextBox ID="companyCodeTextBox" runat="server" TextMode="MultiLine" onkeypress="fncInputCommaAndNumericValuesOnly()"
                    Width="450px" TabIndex="5"></asp:TextBox>

                <span>
                    <br />
                    Example: 101 or 101,105,210</span>
                <%--   <asp:DropDownList ID="companyNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>--%>
            </td>
        </tr>

        <tr>
            <td align="left" style="text-align: right">Statement Type:&nbsp; </td>
            <td align="left">
                <asp:RadioButton ID="profitRadioButton" runat="server" Text="Profit" Checked="true"
                    GroupName="statementType" Font-Bold="true" TabIndex="1" />&nbsp;&nbsp;
                 <asp:RadioButton ID="lossRadioButton" runat="server" Text="Loss"
                     GroupName="statementType" Font-Bold="true" TabIndex="2" />
                <asp:RadioButton ID="allRadioButton" runat="server" Text="All"
                    GroupName="statementType" Font-Bold="true" TabIndex="2" />
            </td>
        </tr>
        <tr>
            <td align="left" style="text-align: right">Group Type:&nbsp; </td>
            <td>
                <asp:DropDownList ID="groupDropDownList" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    <asp:ListItem Text="A Group" Value="A"></asp:ListItem>
                    <asp:ListItem Text="B Group" Value="B"></asp:ListItem>
                    <asp:ListItem Text="G Group" Value="G"></asp:ListItem>
                    <asp:ListItem Text="N Group" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Z Group" Value="Z"></asp:ListItem>


                </asp:DropDownList>
            </td>
        </tr>
        <tr>

            <td align="left">
                <b>Market type</b>
            </td>
            <td>
                <asp:DropDownList ID="marketDropDownList" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="Regular" Value="R"></asp:ListItem>
                    <asp:ListItem Text="OTC" Value="O"></asp:ListItem>
                     <asp:ListItem Text="SME" Value="SC"></asp:ListItem>
                 <%--   <asp:ListItem Text="Debt Market" Value="D"></asp:ListItem>--%>

                </asp:DropDownList>
            </td>
        </tr>
           <tr>

            <td align="left">
                <b>Listing Category</b>
            </td>
            <td>
               <asp:DropDownList ID="listedCategoryDropDownList" Width="100px"   runat="server" TabIndex="1" ></asp:DropDownList>
            </td>
        </tr>
        <tr>
         
              <td align="left" ><b>Sector:</b></td>
              <td >
            <asp:DropDownList ID="sectorDropDownList" runat="server" TabIndex="2">
             </asp:DropDownList>
        </td>
        </tr>

        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

        <tr>
            <td align="center" class="style5">&nbsp;</td>
            <td align="left">
                <asp:Button ID="showReportButton" runat="server" CssClass="buttoncommon"
                    OnClientClick="return fnConfirm();" Text="Show Report" Width="78px" OnClick="showReportButton_Click" />&nbsp;&nbsp;<asp:Button ID="resetButton" runat="server" CssClass="buttoncommon"
                        OnClientClick="return fnReset();" Text="Reset" TabIndex="10" />&nbsp;
                   
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

