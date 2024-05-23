<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="ReceivableCashDividendEnrry.aspx.cs" Inherits="UI_ReceivableCashDividend" Title="Receivable Cash Dividend Report Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function fnReset() {
            var confrm = confirm("Are You Sure To Reset?");
            if (confrm) {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").value = "0";
            document.getElementById("<%=recordDateFromTextBox.ClientID%>").value = "";
            document.getElementById("<%=recordDateToTextBox.ClientID%>").value = "";
            document.getElementById("<%=agmDateFromTextBox.ClientID%>").value = "";
            document.getElementById("<%=agmDateToTextBox.ClientID%>").value = "";

            return false;
        }
        else {
            return true;
        }

    }

    function fnCheckInput() {
        if (document.getElementById("<%=fundNameDropDownList.ClientID%>").value == "0") {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
            alert("Please Select Fund Name.");
            return false;
        }
        var checkDate = /^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
        if (document.getElementById("<%=recordDateFromTextBox.ClientID%>").value == "") {
            document.getElementById("<%=recordDateFromTextBox.ClientID%>").focus();
            alert("Please Insert Reord Date From.");
            return false;
        }
        if (document.getElementById("<%=recordDateFromTextBox.ClientID%>").value != "") {
            if (!checkDate.test(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value)) {
                document.getElementById("<%=recordDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if (document.getElementById("<%=recordDateToTextBox.ClientID%>").value == "") {
            document.getElementById("<%=recordDateToTextBox.ClientID%>").focus();
            alert("Please Insert Record Date To.");
            return false;
        }
        if (document.getElementById("<%=recordDateToTextBox.ClientID%>").value != "") {
            if (!checkDate.test(document.getElementById("<%=recordDateToTextBox.ClientID%>").value)) {
                document.getElementById("<%=recordDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }


        if (document.getElementById("<%=agmDateFromTextBox.ClientID%>").value != "") {
            if (!checkDate.test(document.getElementById("<%=agmDateFromTextBox.ClientID%>").value)) {
                document.getElementById("<%=agmDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }

        if (document.getElementById("<%=agmDateToTextBox.ClientID%>").value != "") {
            if (!checkDate.test(document.getElementById("<%=agmDateToTextBox.ClientID%>").value)) {
                document.getElementById("<%=agmDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }

    }
    </script>
    <style type="text/css">
        .style4 {
            width: 297px;
        }

        .style7 {
            font-weight: normal;
            font-size: xx-small;
        }

        .style8 {
            font-weight: normal;
            font-size: xx-small;
            border-left-color: #808080;
            border-right-color: #C0C0C0;
            border-top-color: #808080;
            border-bottom-color: #C0C0C0;
        }

        .style9 {
            width: 145px;
            text-align: right;
        }

        .style10 {
            width: 145px;
            font-weight: bold;
            text-align: right;
        }
    </style>
    <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
    &nbsp;&nbsp;&nbsp;
    <table style="text-align: center">
        <tr>
            <td class="FormTitle" align="center">Receivable Cash Dividend Report Form
            </td>
            <td>
                <br />
            </td>
        </tr>
    </table>
    <br />
    <table width="750" style="text-align: center" cellpadding="0" cellspacing="0" border="0">



        <tr>
            <td style="font-weight: 700" class="style9"><b style="text-align: left">Fund Name:&nbsp;&nbsp; </b></td>
            <td align="left" class="style4">
                <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="font-weight: 700" class="style9">&nbsp;&nbsp;&nbsp; Record<b> Date From:&nbsp;&nbsp;</b></td>
            <td align="left" class="style4">
                <asp:TextBox ID="recordDateFromTextBox" runat="server" Style="width: 100px;"
                    CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="recordDateFromTextBox"
                    PopupButtonID="recordDateFromImageButton" Format="dd-MMM-yyyy" />
                <asp:ImageButton ID="recordDateFromImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /><b>&nbsp;To:&nbsp;&nbsp; </b>
                <asp:TextBox ID="recordDateToTextBox" runat="server" Style="width: 100px;"
                    CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="recordDateToTextBox"
                    PopupButtonID="recordDateToImageButton" Format="dd-MMM-yyyy" />
                <asp:ImageButton ID="recordDateToImageButton" runat="server" AlternateText="Click Here"
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;<br />
                <span class="style8">(</span><span class="style7">DD-MMM-YYYY)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="style8">&nbsp;&nbsp; (</span>DD-MMM-YYYY)</span></td>

        </tr>
        <tr>
            <td style="font-weight: 700" class="style10">AGM Date From:&nbsp;&nbsp;</td>
            <td align="left" class="style4">
                <asp:TextBox ID="agmDateFromTextBox" runat="server" Style="width: 100px;"
                    CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="agmDateFromTextBox"
                    PopupButtonID="agmDateFromImageButton" Format="dd-MMM-yyyy" />
                <asp:ImageButton ID="agmDateFromImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" />&nbsp;<b>To:&nbsp;&nbsp; </b>

                <asp:TextBox ID="agmDateToTextBox" runat="server" Style="width: 100px;"
                    CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="agmDateToTextBox"
                    PopupButtonID="agmDateToImageButton" Format="dd-MMM-yyyy" />
                <asp:ImageButton ID="agmDateToImageButton" runat="server" AlternateText="Click Here"
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;<br />
                <span class="style8">(DD-MMM-YYYY)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (<span class="style7">DD-MMM-YYYY)</span></span></td>

        </tr>

        <tr>
            <td align="center" colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="showButton" runat="server" Text="Show"
                    CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" OnClick="showButton_Click" />
                <asp:Button ID="resetButton" runat="server" Text="Reset"
                    CssClass="buttoncommon" TabIndex="6" OnClick="resetButton_Click" />
            </td>

        </tr>
        <tr>
            <td align="center" colspan="4">&nbsp;
            </td>
        </tr>
        <tr>
            <div class="row">

                <table class="table table-hover" id="bootstrap-table">
                    <thead>
                        <tr>
                            <th>Fund Code</th>
                            <th>Fund Name</th>
                            <th>Company Code</th>
                            <th>Company Name</th>
                            <th>AGM</th>
                            <th>Recod Date</th>
                            <th>Face value</th>
                            <th>Cash</th>
                            <th>Dividend Per Share</th>
                            <th>Total Sahre</th>
                            <th>Gross Dividend</th>
                            <th>Tax</th>
                            <th>Net Dividend</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            if (Session["dtReceivableCashDividend"] != null)
                            {
                                // cast it and use it
                                // The code

                                System.Data.DataTable dtReceivableCashDividend = (System.Data.DataTable)Session["dtReceivableCashDividend"];
                                if (dtReceivableCashDividend.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtReceivableCashDividend.Rows.Count; i++)
                                    {
                        %>
                        <tr>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["F_CD"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["F_NAME"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["COMP_CD"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["COMP_NM"].ToString());   %> </td>
                            <td><%   Response.Write(Convert.ToDateTime(dtReceivableCashDividend.Rows[i]["AGM"].ToString()).ToString("dd/MM/yyyy"));   %> </td>
                            <td><%   Response.Write(Convert.ToDateTime(dtReceivableCashDividend.Rows[i]["RECORD_DT"].ToString()).ToString("dd/MM/yyyy"));   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["FC_VAL"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["CASH"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["DIVIDEND_PER_SHARE"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["TOT_NOS"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["GROSS_DIVIDEND"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["TAX"].ToString());   %> </td>
                            <td><%   Response.Write(dtReceivableCashDividend.Rows[i]["NET_DIVIDEND"].ToString());   %> </td>
                            <%--  <td><asp:Button ID="UpdateButton" runat="server" Text="Update"  CssClass="buttoncommon" TabIndex="48" OnClick="UpdateButton_Click" /> </td> --%>
                            <td><a href="DividendReceivable.aspx?F_CD=<%   Response.Write(dtReceivableCashDividend.Rows[i]["F_CD"].ToString());   %>&COMP_CD=<%   Response.Write(dtReceivableCashDividend.Rows[i]["COMP_CD"].ToString());   %>&AGM_DATE=<%   Response.Write(dtReceivableCashDividend.Rows[i]["AGM"].ToString());   %>&RECORD_DT=<%   Response.Write(dtReceivableCashDividend.Rows[i]["RECORD_DT"].ToString());   %>"
                                class="custUpdBtn">Update</a></td>
                        </tr>
                        <%
                                    }



                                }
                            }

                        %>
                    </tbody>
                </table>
            </div>
        </tr>
    </table>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#bootstrap-table').bdt({

            });
        });
    </script>
</asp:Content>
