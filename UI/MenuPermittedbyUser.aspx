<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="MenuPermittedbyUser.aspx.cs" Inherits="UI_CompanyInformation" Title="Menu Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
       

         .buttonAssignMenu {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
            WIDTH: 144px;
            HEIGHT: 24px;
        }
    </style>
    <style type="text/css">  
            .Gridview {  
                font-family: Verdana;  
                font-size: 10pt;  
                font-weight: normal;  
                color: black;  
            }  

             .GridPager a, .GridPager span
    {
        display: block;
        height: 15px;
        width: 15px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
        color: #969696;
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
        color: #000;
        border: 1px solid #3AC0F2;
    }

    .UsersGridViewButton
{
    border: thin solid #FF0000;
    font-family: 'Arial Unicode MS';
    
    color: #FF0000;
    font-style: normal;
    text-align: center;
    border-radius: 10px;
    padding-right: 20px;
    padding-left: 20px;
}
    tbody tr {
	height: 30px !important;
}
        </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

    <div align="center">
        <h2>Assign Menu By User</h2>
         <div style="float: left; margin-top: -34px; margin-left: -12px;">
            <asp:Button ID="AddButton" runat="server"   Text="Assign Menu"
                CssClass="buttonAssignMenu" TabIndex="48"
                OnClick="AssignMenu_Click" />
        </div>
        <div>
            <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 20px; color: green; width:300px"></asp:Label>
         </div>
        <div class="row">

            <table class="table table-hover" id="bootstrap-table">
                
                    <tr>
                            <td align="center" width="624px" colspan="2" > 

                                <div>
                                    <asp:GridView ID="grdShowDSEMP" runat="server" AutoGenerateColumns="false" DataKeyNames="MENU_ID" OnRowDeleting="grdShowDSEMP_RowDeleting"
                                        OnPageIndexChanging="grdShowDSEMP_PageIndexChanging"
                                        BackColor="White" AllowPaging="True"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        Font-Names="Verdana" PageSize="5" Font-Size="Small">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" VerticalAlign="Middle" ForeColor="White" HorizontalAlign="center" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="center" />
                                        <RowStyle ForeColor="#000066" BackColor="White" HorizontalAlign="center" Height="10px" />
                                        <SelectedRowStyle BackColor="#669999" Height="10px" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        <Columns>
                                            <asp:BoundField  DataField="USER_ID" HeaderText="User Id" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                                FooterStyle-Width="5%" />
                                            <asp:BoundField DataField="MENU_ID" HeaderStyle-HorizontalAlign="Center" HeaderText="Menu Id" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                                FooterStyle-Width="5%" />
                                            <asp:BoundField DataField="MENU_NAME" HeaderStyle-HorizontalAlign="Center" HeaderText="Menu Name" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                                FooterStyle-Width="5%" />
                                            <asp:BoundField DataField="SUBMENU_NAME" HeaderStyle-HorizontalAlign="Center" HeaderText="SUBMENU NAME" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                FooterStyle-Width="10%" />
                                            <asp:BoundField DataField="CHILD_OF_SUBMENU_ID"  HeaderText ="CHILD OF SUBMENU ID" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                                FooterStyle-Width="5%" />
                                            <asp:BoundField DataField="CHILD_OF_SUBMENU_NAME" HeaderStyle-HorizontalAlign="Center" HeaderText="CHILD OF SUBMENU NAME" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                FooterStyle-Width="20%" />
                                            <asp:BoundField DataField="URL" HeaderText="URL" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                FooterStyle-Width="20%" />

                                            <asp:CommandField ShowDeleteButton="true" HeaderStyle-Width="10%" ItemStyle-Width="10%" DeleteText="Revoke"
                                                FooterStyle-Width="10%" ControlStyle-CssClass="UsersGridViewButton" ControlStyle-BackColor="Red" ControlStyle-ForeColor="White" ControlStyle-Width="80px" ControlStyle-Height="22px"
                                                />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>

             
            </table>
        </div>


    </div>

</asp:Content>

