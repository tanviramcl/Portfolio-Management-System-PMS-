<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="MenuPermission.aspx.cs" Inherits="UI_CompanyInformation" Title="Menu Information" %>

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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

    <div align="center">
        <h2>Menu Information</h2>
         <div style="float: left; margin-top: -34px; margin-left: -12px;">
            <asp:Button ID="AddButton" runat="server"   Text="Assign Menu"
                CssClass="buttonAssignMenu" TabIndex="48"
                OnClick="AssignMenu_Click" />
        </div>
        <div class="row">

            <table class="table table-hover" id="bootstrap-table">
                <thead>
                    <tr>
                       <%-- <th>Menu Id</th>--%>
                        <th>Menu Name</th>
                        <th>SUBMENU NAME</th>
                        <th>CHILD OF SUBMENU NAME</th>
                        <th>URL</th>


                    </tr>
                </thead>
                <tbody>
                    <%


                        System.Data.DataTable dtmenUList = (System.Data.DataTable)Session["menus"];
                        if (dtmenUList.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtmenUList.Rows.Count; i++)
                            {
                    %>
                    <tr>
                     <%--   <td><%   Response.Write(dtmenUList.Rows[i]["MENU_ID"].ToString());   %> </td>--%>
                        <td><%   Response.Write(dtmenUList.Rows[i]["MENU_NAME"].ToString());   %> </td>
                         <td><%   Response.Write(dtmenUList.Rows[i]["SUBMENU_NAME"].ToString());   %> </td>
                         <td><%   Response.Write(dtmenUList.Rows[i]["CHILD_OF_SUBMENU_NAME"].ToString());   %> </td>
                         <td><%   Response.Write(dtmenUList.Rows[i]["URL"].ToString());   %> </td>


                    </tr>
                    <%

                            }
                        }

                    %>
                </tbody>
            </table>
        </div>


    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#bootstrap-table').bdt({

            });
        });
    </script>

</asp:Content>

