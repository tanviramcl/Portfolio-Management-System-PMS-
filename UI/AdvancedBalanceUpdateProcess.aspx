                                                  <%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="AdvancedBalanceUpdateProcess.aspx.cs" Inherits="AdvancedBalanceUpdateProcess" Title="IAMCL Advanced Balance Update Process  " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        label.error {
            color: red;
            display: inline-flex;
        }
    </style>
    <style type="text/css">
        .style6 {
            font-family: "Times New Roman";
            font-weight: bold;
        }

        .style8 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 17px;
            color: #08559D;
            text-decoration: none;
            FONT-WEIGHT: bold;
            text-decoration: underline;
            /*padding-right: 5;
            padding-bottom: 3;*/
            height: 21px;
        }

        .style11 {
            height: 18px;
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
            WIDTH: 246px;
            HEIGHT: 65px;
            font-size: 21px;
            border-radius: 25px;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" AsyncPostBackTimeout="4000"
        EnableScriptLocalization="true" ID="ScriptManager1" />

    <table width="1100" cellpadding="0" cellspacing="0" border="0">
    </table>
    <table>

        <tr>
            <td align="center" colspan="4" class="style8">Advanced Balance Update Process  
            </td>
        </tr>

        <tr>

            <td align="center" colspan="4">
                <div id="dvGridDSETradeInfo" runat="server" style="text-align: center; display: block; overflow: auto; height: 200px; width: 952px;"
                    dir="ltr">
                    <asp:GridView ID="grdShowDSEMP" runat="server" AutoGenerateColumns="False"
                        BackColor="#000000"
                        BorderColor="#33D4FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="2" Width="920px">
                        <FooterStyle BackColor="#33D4FF" ForeColor="#000000" />
                        <PagerStyle ForeColor="#33D4FF" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#33D4FF" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#33D4FF" Font-Bold="true" ForeColor="White" />
                        <RowStyle BackColor="#33D4FF" ForeColor="#0000" />
                        <Columns>
                            <asp:BoundField DataField="F_CD" HeaderText="Fund Code" />
                            <asp:BoundField DataField="F_NAME" HeaderText="Fund Name" />
                            <asp:BoundField DataField="BalanceDate" HeaderText="Balance Date" />
                            <asp:BoundField DataField="LastUpadateDate" HeaderText="Last Upadate Date" />

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>

        <tr>
            <td align="center">&nbsp;</td>
            <td align="center">
                <div>

                    <asp:UpdateProgress ID="updProgress"
                        AssociatedUpdatePanelID="UpdatePanel1"
                        runat="server">
                        <ProgressTemplate>
                            <img src="../Image/Processing.gif" alt="processing" style="width: 186px; height: 128px; margin-left: 36px" />

                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 24px; color: green;"></asp:Label>
                            <br />


                            <asp:Button ID="btnProcess" runat="server" Text="Process"
                                CssClass="processBtn" OnClick="btnProcess_Click" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>

        </tr>
    </table>

</asp:Content>

