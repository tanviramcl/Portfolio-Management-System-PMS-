<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="UI_CompanyInformation" Title="User Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

    <div align="center">
        <h2>User Information</h2>
        <div style="float: left; margin-top: -34px;  margin-left: -12px;">
            <asp:Button ID="AddButton" runat="server" width="200px" Text="Add/Update User"
                CssClass="buttoncommon" TabIndex="48"
                OnClick="AddButton_Click" />
        </div>
        <div style="float: right;  margin-top: -34px; margin-left: -12px;">
            <asp:Button ID="ManageRoleButton" width="200px" runat="server" Text="Manage User Type"
                CssClass="buttoncommon" TabIndex="48"
                OnClick="ManageRole_Click" />
        </div>

        <div>
            <asp:Panel ID="Panel1" runat="server" style="float: right;"  Visible="false">
                
              <table class="table table-hover" id="roleTable">

            <tbody>
                <tr>
                    <td align="left">
                         <asp:Label ID="LabelUserRole" runat="server" Visible="false" Text="User Type"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="UserRoleTextBox" Visible="false" runat="server" Width="100px" ></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td align="left">
                         <asp:Button ID="ButtonRoleAddTextBox" Visible="false" Text="Add" CssClass="buttoncommon" runat="server" OnClick="ButtonRoleAddTextBox_Click" />
                    </td>

                 </tr>
           
            </tbody>
        </table>
            </asp:Panel>

    
   </div>

        <div class="row">
             <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 20px; color: green; width:300px"></asp:Label>
            <table class="table table-hover" id="bootstrap-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>USER ID</th>
                        <th>NAME</th>
                        <th>DESIGNATION</th>
                        <th>ROLE NAME</th>
                     <%--   
                        <th>Action</th>--%>
                    </tr>
                </thead>
                <tbody>
                    <%

                        System.Data.DataTable dtuserlist = (System.Data.DataTable)Session["user_list"];
                        if (dtuserlist.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtuserlist.Rows.Count; i++)
                            {
                    %>
                    <tr>
                        <td><%   Response.Write(dtuserlist.Rows[i]["ID"].ToString());   %> </td>
                        <td><%   Response.Write(dtuserlist.Rows[i]["USER_ID"].ToString());   %> </td>
                        <td><%   Response.Write(dtuserlist.Rows[i]["NAME"].ToString());   %> </td>
                        <td><%   Response.Write(dtuserlist.Rows[i]["DESIGNATION"].ToString());   %> </td>
                        <td><%   Response.Write(dtuserlist.Rows[i]["ROLE_NAME"].ToString());   %> </td>
                       
                
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
         $("#aspnetForm").validate({
                submitHandler: function () {
                  //  test();
                },
                rules: {
                    <%=UserRoleTextBox.UniqueID %>: {
                        
                    required: true,
                    number:true,
                    minlength: 1,
                    maxlength: 3
                }
                    
            }, messages: {
                <%=UserRoleTextBox.UniqueID %>:{  
                  required: "*Fund Code is required*",
                  maxlength: "* Please enter maximum 3 characters *"
              }
                    
          }
        });
   

      

        $(document).ready(function () {
            $('#bootstrap-table').bdt({

            });
        });
    </script>

</asp:Content>

