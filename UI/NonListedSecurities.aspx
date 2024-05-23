<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="NonListedSecurities.aspx.cs" Inherits="UI_NonListedSecuritiesInvestmentEntryForm" Title="Non Listed Securites (Investment) Entry Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="../Scripts/jquery-ui.css"  />
    <link href="../CSS/vendor/bootstrap.min.css" rel="stylesheet" />
    <script  type="text/javascript" src="../Scripts/jquery-ui.js"></script>
    <style type="text/css">
        label.error {
            color: red;
            display: inline-flex;
        }

        .style5 {
            height: 24px;
        }

        .style6 {
            height: 14px;
        }
    </style>
     <style type="text/css">
         .Gridview {
             font-family: Verdana;
             font-size: 10pt;
             font-weight: normal;
             color: black;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

&nbsp;&nbsp;&nbsp;
<table "text-align"="center">
    <tr>
        <td class="FormTitle" align="center">
            NON LISTED SECURITIES (Investment)
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />

    <table class="table table-hover" id="bootstrap-table">
        <thead>
            <tr>
                <%--<th></th>--%>
                <th>Fund Code</th>
                <th>Comp Code</th>
                <th>Ammount</th>
                <th>Rate</th>
                <th>Investment Date</th>


                <%

                    System.Data.DataTable dtNonlistedSecurities = (System.Data.DataTable)Session["NonlistedDetails"];
                    if (dtNonlistedSecurities.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtNonlistedSecurities.Rows.Count; i++)
                        {
                %>
            </tr>
        </thead>
        <tbody>

            <tr>

                <td><%   Response.Write(dtNonlistedSecurities.Rows[i]["F_CD"].ToString());   %> </td>
                <td><%   Response.Write(dtNonlistedSecurities.Rows[i]["COMP_CD"].ToString());   %> </td>
                <td><%   Response.Write(dtNonlistedSecurities.Rows[i]["AMOUNT"].ToString());   %> </td>
                <td><%   Response.Write(dtNonlistedSecurities.Rows[i]["RATE"].ToString());   %> </td>
                <td><%   Response.Write(dtNonlistedSecurities.Rows[i]["INV_DATE"].ToString());   %> </td>
           
                 <td><a href="NonListedSecuritiesInvestmentEntryForm.aspx?ID=<%   Response.Write(dtNonlistedSecurities.Rows[i]["COMP_CD"].ToString());   %>" 
                            class="custUpdBtn">Update</a></td>

            </tr>
            <%

                    }
                }

            %>
        </tbody>
    </table>


    <script type="text/javascript">


       
       $(document).ready(function () {
           $('#bootstrap-table').bdt({

           });
       });
     
    </script>
    
               
</asp:Content>

