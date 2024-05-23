<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="AssignMenuByUser.aspx.cs" Inherits="UI_CompanyInformation" Title="Assign Menu By User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        label.error {
            color: red;
            display: inline-flex;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

    <div align="center">
        <table id="Table1" width="600" align="center" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td align="center" class="style3">
                    <b><u>Assign Menu By User</u></b>
                </td>
            </tr>
        </table>

        <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
        </table>


        <table class="table table-hover" id="bootstrap-table">

            <tbody>


                <tr>

                    <td >

                        <div style="height: 300px; width: 400px; overflow: auto;" id="dvGridFund" runat="server">
                            <!--- Following code renders the checkboxes and a label control on browser --->

                            <asp:CheckBox ID="chkAll" Text="Select All" runat="server" />
                            <asp:CheckBoxList ID="chkFunds" runat="server">
                            </asp:CheckBoxList>

                        </div>
                    </td>


                </tr>
              
            </tbody>
        </table>
        <table width="600" align="center" cellpadding="0" cellspacing="0" >
              <tr>
                    <td align="left">
                        <b>User Name </b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="userDropDownList" Style="" runat="server" TabIndex="4"></asp:DropDownList>
                    </td>

                </tr>
                <tr>
                   
                        <td align="center" colspan="6">
                        <asp:Button ID="saveButton" runat="server" Text="Save"
                            CssClass="buttoncommon" TabIndex="48"
                            OnClick="saveButton_Click" />
                    </td>
                </tr>

         </table>

        <br />
        <br />

        <script type="text/javascript">
            $(function () {
                $("[id*=chkAll]").bind("click", function () {
                    if ($(this).is(":checked")) {
                        $("[id*=chkFunds] input").prop("checked", "checked");
                    } else {
                        $("[id*=chkFunds] input").removeAttr("checked");
                    }
                });
                $("[id*=chkFunds] input").bind("click", function () {
                    if ($("[id*=chkFunds] input:checked").length == $("[id*=chkFunds] input").length) {
                        $("[id*=chkAll]").prop("checked", "checked");
                    } else {
                        $("[id*=chkAll]").removeAttr("checked");
                    }
                });
            });
        </script>
        <script type="text/javascript">
     


            $.validator.addMethod("CheckDropDownList", function (value, element, param) {  
                if (value == '0')  
                    return false;  
                else  
                    return true;  
            },"Please select a  user"); 

       

            

            $("#aspnetForm").validate({
                
                rules: {
                    <%=userDropDownList.UniqueID %>: {
                        
                        CheckDropDownList: true
                    }
                    
                }
            });
            function test() {   
                alert("s");

            }
        </script>


    </div>

</asp:Content>

