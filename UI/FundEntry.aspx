<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="FundEntry.aspx.cs" Inherits="UI_CompanyInformation" Title="Fund Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />




</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

    <div align="center">
        <h2>Fund Information</h2>
        <div style="float: left; margin-top: -34px; margin-left: -12px;">
            <asp:Button ID="AddButton" runat="server" Text="Add Fund"
                CssClass="buttoncommon" TabIndex="48"
                OnClick="AddButton_Click" />
        </div>
        <div class="row">

            <table class="table table-hover" id="bootstrap-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Fund Name</th>
                        <th>Fund Type</th>
                        <th>Fund Status</th>
                        <th>Trade Code</th>
                        <th>BO ID</th>
                        <th>Sell Buy Commsion</th>
                        <th>Company Code</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <%

                        System.Data.DataTable dtNoOfFunds = (System.Data.DataTable)Session["funds"];
                        if (dtNoOfFunds.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtNoOfFunds.Rows.Count; i++)
                            {
                    %>
                    <tr>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["F_CD"].ToString());   %> </td>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["F_NAME"].ToString());   %> </td>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["F_TYPE"].ToString());   %> </td>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["IS_F_CLOSE"].ToString());   %> </td>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["CUSTOMER"].ToString());   %> </td>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["BOID"].ToString());   %> </td>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["SL_BUY_COM_PCT"].ToString());   %> </td>
                        <td><%   Response.Write(dtNoOfFunds.Rows[i]["COMP_CD"].ToString());   %> </td>
                        <%--  <td><asp:Button ID="UpdateButton" runat="server" Text="Update"  CssClass="buttoncommon" TabIndex="48" OnClick="UpdateButton_Click" /> </td> --%>
                        <td><a href="AddFund.aspx?ID=<%   Response.Write(dtNoOfFunds.Rows[i]["F_CD"].ToString());   %>" 
                            class="custUpdBtn">Update</a></td>
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

