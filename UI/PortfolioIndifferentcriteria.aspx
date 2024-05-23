<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PortfolioIndifferentcriteria.aspx.cs" Inherits="UI_PortfolioWithNonListedSecurities" Title="Individual Portfolio Statement Report Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function fnReset() {
            var confrm = confirm("Are You Sure To Reset?");
            if (confrm) {
                document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value = "0";
            document.getElementById("<%=fundNameDropDownList.ClientID%>").value = "0";
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
        if (document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value == "0") {
            document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").focus();
            alert("Please Select Date.");
            return false;
        }
        if (document.getElementById("<%=listedCategoryDropDownList.ClientID%>").value == "0") {
            document.getElementById("<%=listedCategoryDropDownList.ClientID%>").focus();
                alert("Please Select a Listing Category.");
            return false;
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
    &nbsp;&nbsp;&nbsp;
    <table align="center">
        <tr>
            <td class="FormTitle" align="center">Portfolio Statement Report Form
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="right" style="font-weight: 700"><b>Fund Name:&nbsp; </b></td>
            <td align="left">
                <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Portfolio As On:&nbsp; </b></td>
            <td align="left">
                <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
            </td>
        </tr>
        <tr>

            <td align="right" style="font-weight: 700">
                <b>Sector </b>
            </td>
            <td align="left">
                <%-- <asp:TextBox ID="sectorTextBox" runat="server" Width="100px"></asp:TextBox>--%>
                <asp:DropDownList ID="sectorDropDownList" runat="server" TabIndex="3"></asp:DropDownList>
            </td>
        </tr>
        <tr>
             <td align="right" style="font-weight: 700">
                <b>Listed category </b>
            </td>
            <td align="left">
                <%-- <asp:TextBox ID="flugTextBox" runat="server" Width="100px"></asp:TextBox>--%>
                <asp:DropDownList ID="listedCategoryDropDownList" Width="100px" AutoPostBack="true" runat="server" TabIndex="1"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700">
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
            <td align="right" style="font-weight: 700">
                <b>Group type</b>
            </td>
            <td align="left">
                <asp:DropDownList ID="groupDropDownList" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="A Group" Value="N"></asp:ListItem>
                    <asp:ListItem Text="B Group" Value="R"></asp:ListItem>
                    <asp:ListItem Text="G Group" Value="G"></asp:ListItem>
                    <asp:ListItem Text="N Group" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Z Group" Value="Z"></asp:ListItem>


                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700">
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
            <td align="right" style="font-weight: 700">
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
            <td align="center" colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="showButton" runat="server" Text="Show Report"
                    CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" OnClick="showButton_Click" />
                <asp:Button ID="resetButton" runat="server" Text="Reset"
                    CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnReset();" />
            </td>

        </tr>
        <tr>
            <td align="center" colspan="4">&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

