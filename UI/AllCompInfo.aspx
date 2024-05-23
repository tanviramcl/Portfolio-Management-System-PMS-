<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="AllCompInfo.aspx.cs" Inherits="UI_CompanyInformation" Title="Company Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <style type="text/css">
        .buttonadditionalCharge {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
            WIDTH: 100%;
            HEIGHT: 24px;
        }

        .buttonaddOrupdateCompany {
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
    }



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

    <div align="center">
        <h2>Company Information</h2>
        <div style="float: left; margin-top: -34px; margin-left: -12px;">
            <%
                string userType = Session["UserType"].ToString();

                if (userType == "A" )
                {

            %>
            <asp:Button ID="AddButton" runat="server" Text="Add/Update Company"
                CssClass="buttonaddOrupdateCompany" TabIndex="48"
                OnClick="AddButton_Click" />
            <%
                }
            %>
        </div>
        <%--<div style="float: right;margin-top: -2%;">
            <asp:Button ID="Button1" runat="server" Text="Additional B/S Charge"
                CssClass="buttonadditionalCharge" TabIndex="48"
                  />--%>
    </div>
    <div class="row">
        <div id="dialog" style="display: none">
            This is a simple popup
        </div>
        <table class="table table-hover" id="bootstrap-table">
            <thead>
                <tr>
                    <%--<th></th>--%>
                    <th>Company Code</th>
                    <th>Company Name</th>
                    <th>Sector Code</th>
                    <th>Sector Name</th>
                    <th>Trade Code</th>
                     <th>Group</th>
                    <th>Authorize Capital</th>
                    <th>Paid of Capital</th>
                    <th>Face value</th>
                     <th>Listing Category</th>
                     <th>Market Type</th>
                    
                    <%


                        if (userType == "A")
                        {

                    %>
                    <th>Is Add. buy/sell charge applicable?</th>
                    <th>Additional buy/sell charge amount</th>

                    <%
                        }
                    %>
                    <%-- <th>Action</th>--%>
                </tr>
            </thead>
            <tbody>
                <%

                    System.Data.DataTable dtCompInfo = (System.Data.DataTable)Session["CompInfo"];
                    if (dtCompInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCompInfo.Rows.Count; i++)
                        {
                %>
                <tr>
                    <%-- <td><input type="checkbox" /> </td>--%>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["COMP_CD"].ToString());   %> </td>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["COMP_NM"].ToString());   %> </td>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["SECT_MAJ_CD"].ToString());   %> </td>
                      <td><%   Response.Write(dtCompInfo.Rows[i]["SECT_MAJ_NM"].ToString());   %> </td>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["INSTR_CD"].ToString());   %> </td>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["TRADE_METH"].ToString());   %> </td>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["ATHO_CAP"].ToString());   %> </td>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["PAID_CAP"].ToString());   %> </td>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["FC_VAL"].ToString());   %> </td>
                      <td><%   Response.Write(dtCompInfo.Rows[i]["flag"].ToString());   %> </td>
                     <td><%   Response.Write(dtCompInfo.Rows[i]["MARKETTYPE"].ToString());   %> </td>
                    <%


                        if (userType == "A")
                        {

                    %>
                    <td><%   Response.Write(dtCompInfo.Rows[i]["ISADD_HOWLACHARGE_DSE"].ToString());   %> </td>
                    <td><%  

                            if (dtCompInfo.Rows[i]["ADD_HOWLACHARGE_AMTDSE"].ToString() == "0")
                            {
                                Response.Write("");
                            }
                            else
                            {
                                Response.Write(dtCompInfo.Rows[i]["ADD_HOWLACHARGE_AMTDSE"].ToString());
                            }

                    %> </td>
                    <%
                        }
                    %>

                    <%--<td><asp:Button ID="UpdateButton" runat="server" Text="Update"  CssClass="buttoncommon" TabIndex="48" OnClick="UpdateButton_Click" /> </td>--%>
                    <%--<td align="center"><a  href="CompanyInformation.aspx?ID=<% Response.Write(dtCompInfo.Rows[i]["COMP_CD"].ToString());%>" 
                            class="custUpdBtn">Update</a></td>--%>
                </tr>
                <%

                        }
                    }

                %>
            </tbody>
        </table>
    </div>





    <script type="text/javascript">
        function fnConfirm() {
            var selected = [];
            $('input:checked').each(function () {
                selected.push($(this).attr('type'));
            });
            if (selected.length > 0) {
                $("#dialog").dialog({
                    title: "jQuery Dialog Popup",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });
            }
            else {
                alert(" Please Select at least one  Row ");
            }

        }


        $(document).ready(function () {
            $('#bootstrap-table').bdt({

            });
        });
    </script>

</asp:Content>

