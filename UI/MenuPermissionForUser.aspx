<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="MenuPermissionForUser.aspx.cs" Inherits="UI_CompanyInformation" Title="Menu Information" %>

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
        <h2>Assign Menu By User</h2>
         <div style="float: left; margin-top: -34px; margin-left: -12px;">
            <asp:Button ID="AddButton" runat="server"   Text="Assign Menu"
                CssClass="buttonAssignMenu" TabIndex="48"
                OnClick="AssignMenu_Click" />
        </div>
        <div class="row">

            <table class="table table-hover" id="bootstrap-table">
                <thead>
                    <tr>
                        <th>User Id</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>Permitted Menu</th>
                         <th></th>


                    </tr>
                </thead>
                <tbody>
                    <%


                        System.Data.DataTable dtmenUList = (System.Data.DataTable)Session["PermittedUser"];

                        if (dtmenUList.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtmenUList.Rows.Count; i++)
                            {
                                if(dtmenUList.Rows[i]["user_id"].ToString() !="admin")
                                {
                    %>
                    <tr>
                        <td><%   Response.Write(dtmenUList.Rows[i]["user_id"].ToString());   %> </td>
                        <td><%   Response.Write(dtmenUList.Rows[i]["name"].ToString());   %> </td>
                         <td><%   Response.Write(dtmenUList.Rows[i]["designation"].ToString());   %> </td>
                         <td><%   Response.Write(dtmenUList.Rows[i]["permittedmenu"].ToString());   %> </td>
                          <td><a href="MenuPermittedbyUser.aspx?ID=<%   Response.Write(dtmenUList.Rows[i]["user_id"].ToString());   %>" 
                            class="custUpdBtn">Details</a></td>
                    </tr>
                    <%
                                
                                }

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

        $('a[href="#sign_up"]').click(function () {
            alert('Sign new href executed.');
        });
    </script>

</asp:Content>

