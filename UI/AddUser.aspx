<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="UI_CompanyInformation" Title="Add User" %>

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
                    <b><u>User Entry</u></b>
                </td>
            </tr>
        </table>

        <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
        </table>


        <table class="table table-hover" id="bootstrap-table">

            <tbody>
                <tr>
                    <td align="left">
                        <b>User Id </b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="userIdTextBox" runat="server" Width="100px"  OnTextChanged="UserIDTextBox_TextChanged"  AutoPostBack="true"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td align="right">
                        <asp:Label ID="LabelUserName" Style="font-weight: 700" runat="server" Text="User Name:"></asp:Label>
                    </td>
                    <td align="left" width="200px">
                        <asp:DropDownList ID="useNameDropDownList" OnSelectedIndexChanged="UserNameDropDownList_SelectedIndexChanged" runat="server" TabIndex="6"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>

                </tr>
                 <tr>
                    <td align="left">
                        <b>Designation </b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="userDesignationTextBox" runat="server" ReadOnly="true" Width="220px" AutoPostBack="true"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="userRoleDropDownListlabel" Style="font-weight: 700" runat="server" Text="User Type:"></asp:Label>
                    </td>
                    <td align="left" width="200px">
                        <asp:DropDownList ID="userRoleDropDownList" runat="server" TabIndex="6"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>Password</b>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="100px"></asp:TextBox>
                    </td>

                    <td align="left">
                        <b>Confirm Password</b>
                    </td>
                    <td align="left">

                        <asp:TextBox ID="txtconfirmPassword" TextMode="Password" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                


                <tr>


                    <td align="center" colspan="6">
                          <asp:Button ID="ButtonSave" Visible="true" Text="Save" CssClass="buttoncommon" runat="server" OnClick="ButtonSave_Click" />
                    </td>

                </tr>


            </tbody>
        </table>


        <script type="text/javascript">
     
            jQuery.validator.addMethod("noSpace", function(value, element) { 
                return value.indexOf(" ") < 0 && value != ""; 
            }, "No space please and don't leave it empty");

            $.validator.addMethod("CheckDropDownList", function (value, element, param) {  
                if (value == '0')  
                    return false;  
                else  
                    return true;  
            },"Please select a  User Name."); 
            $.validator.addMethod("CheckUserTypeDropDownList", function (value, element, param) {  
                if (value == '0')  
                    return false;  
                else  
                    return true;  
            },"Please select a  User Type."); 

            //$.validator.addMethod("PASSWORD",function(value,element){
            //    return this.optional(element) || /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$/i.test(value);
            //},"Passwords are 8-16 characters with uppercase letters, lowercase letters and at least one number.");

            jQuery.validator.addMethod("passwordCheck",
        function(value, element, param) {
            if (this.optional(element)) {
                return true;
            } else if (!/[A-Z]/.test(value)) {
                return false;
            } else if (!/[a-z]/.test(value)) {
                return false;
            } else if (!/[0-9]/.test(value)) {
                return false;
            }

            return true;
        },
        "Passwords are 8-16 characters with uppercase letters, lowercase letters and at least one number.");


            $.validator.addMethod("CheckConfirmUserPassword", function (value, element, param) {  
                
                var txtpass = $('#<%=txtPassword.ClientID %>').val();
                var txtConPass=$('#<%=txtconfirmPassword.ClientID %>').val();
               // alert(txtConPass,txtpass);
 
                if (txtpass != txtConPass)  
                    return false;  
                else  
                    return true;  
            },"* Pasword does not match ");



            $("#aspnetForm").validate({
                submitHandler: function () {
                    test();
                },
                rules: {
                    <%=userIdTextBox.UniqueID %>: {
                        
                    required: true,
                    noSpace: true,
                    minlength: 1,
                    maxlength: 15
                    } 
                    ,<%=useNameDropDownList.UniqueID %>: {
                        CheckDropDownList: true 
                     },<%=userDesignationTextBox.UniqueID %>: {
                        required: true 
                    },<%=txtPassword.UniqueID %>: {
                        required: true,
                        minlength: 8,
                        maxlength: 16,
                        passwordCheck:true
                    },<%=txtconfirmPassword.UniqueID %>: {
                        required:true,
                       CheckConfirmUserPassword:true,
                        minlength: 8,
                        maxlength: 16,
                        passwordCheck:true
                    },<%=userRoleDropDownList.UniqueID %>: {
                        
                        CheckUserTypeDropDownList: true
                    }
                   
                    
            }, messages: {
                <%=userIdTextBox.UniqueID %>:{  
                  required: "*User Id is required*",
                  maxlength: "* Please enter maximum 15 characters *",
                  minlength: "*lease enter minimum 1 characters"
                },<%=txtPassword.UniqueID %>:{  
                        required: "*Password is required*",
                }
                ,
                <%=txtconfirmPassword.UniqueID %>:{  
                            
                    required: "* Confirm Password is required*",
                    equalTo:"* Password Must be Same",
                },
                <%=userRoleDropDownList.UniqueID %>:{  
                        required: "*User Type is required*",
                }
               

                    
          }
        });
      function test() {   
          $.ajax({
              type: "POST",
              url: "AddUser.aspx/InsertandUpdateUser",
              data: '{userId: "' + $("#<%=userIdTextBox.ClientID%>").val() + '",useName: "' + $("#<%=useNameDropDownList.ClientID%> option:selected").text() + '",UserDesignation: "' + $("#<%=userDesignationTextBox.ClientID%>").val() + '",Password: "' + $("#<%=txtPassword.ClientID%>").val() + '",confirmPassword: "' + $("#<%=txtconfirmPassword.ClientID%>").val() + '",userRole: "' + $("#<%=userRoleDropDownList.ClientID%>").val() + '",EMPID: "' + $("#<%=useNameDropDownList.ClientID%>").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        alert('data saved successfully');


                        window.location = 'UserInfo.aspx';
                    }
                
                },
                failure: function (response) {
                  
                }
             });

        }
        </script>
    </div>

</asp:Content>

