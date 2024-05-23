<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CalenderRequisition.aspx.cs" Inherits="UI_RiskVARCaluation" Title="Company Wise Securities Tansaction Report Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .auto-style1 {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
    &nbsp;&nbsp;&nbsp;
    <table style="text-align: center">
        <tr>
            <td class="FormTitle" align="center">&nbsp;Calendar Requisition
            </td>
            <td>
                <br />
            </td>
        </tr>
    </table>
    <br />
    <table width="750" style="text-align: center" cellpadding="0" cellspacing="0" border="0">







        <tr>
            <td align="right" style="font-weight: 700"><b>Company Name:&nbsp;&nbsp; </b></td>
            <td align="left">
                <asp:DropDownList ID="companyNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Portfolio As On:&nbsp; </b></td>
            <td align="left">
                <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="LabelUserName" Style="font-weight: 700" runat="server" Text="Assign to :"></asp:Label>
            </td>
            <td align="left" width="200px">
                <asp:DropDownList ID="useNameDropDownList" runat="server" TabIndex="6">
                </asp:DropDownList>
            </td>

        </tr>
        <tr>

             <td align="right">
                <asp:Label ID="Label1" Style="font-weight: 700" runat="server" Text="Type:"></asp:Label>
            </td>
            <td align="left" class="style4">
                <asp:DropDownList runat="server" Visible="true" AutoPostBack="true" ID="ddlTest">
                    <asp:ListItem Text="PDF" Value="PDF" Selected="True" />
                    <asp:ListItem Text="Excel" Value="Excel" />
                    <asp:ListItem Text="Word" Value="Word" />
                    <asp:ListItem Text="CSV" Value="CSV" />
                </asp:DropDownList>
                


        </tr>

        <tr>
            <td align="center" colspan="4">&nbsp;
                <asp:Button ID="ButtonExport" Visible="true" runat="server" Text="Export"
                    CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" OnClick="ExportButton_Click" />
            </td>
            </td>
        </tr>
    </table>
</asp:Content>

