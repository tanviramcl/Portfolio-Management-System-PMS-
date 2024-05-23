<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeFile="DividendReceived.aspx.cs" Inherits="UI_DividendReceived" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
      

        function fncInputNumericValuesOnly() {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function fnCheckInput() {
            var checkDate = /^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;

            if (document.getElementById("<%=companyNameDropDownList.ClientID%>").value == "") {
         alert("Please Select a Company Name ");
         document.getElementById("<%=companyNameDropDownList.ClientID%>").focus();
         return false;

     }
     if (document.getElementById("<%=PaymenttypeDropDownList.ClientID%>").value == "0") {
         alert("Please Enter Payment Type");
         document.getElementById("<%=PaymenttypeDropDownList.ClientID%>").focus();
         return false;
     }
     if (document.getElementById("<%=PaymentDateTextBox.ClientID%>").value == "") {
         document.getElementById("<%=PaymentDateTextBox.ClientID%>").focus();
         alert("Please Insert Payment Date");
         return false;
     }


     if (document.getElementById("<%=TaxRateTextBox.ClientID%>").value == "") {
         document.getElementById("<%=TaxRateTextBox.ClientID%>").focus();
         alert("Please Insert Tax Rate ");
         return false;
     }
 }


    </script>


    <style type="text/css">
        .scrollit {
            overflow: scroll;
            height: 600px;
            width: 1200Px;
        }

        .auto-style1 {
            font-family: "Courier New", Courier, monospace;
            font-size: x-large;
            font-weight: bold;
            color: #990099;
        }

        .auto-style2 {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
        }

        .auto-style3 {
            height: 22px;
        }

        .hiddencol {
            display: none;
        }
    </style>

    <script type="text/javascript">

        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {

                //If checked change color to Aqua

                row.style.backgroundColor = "aqua";

            }

            else {

                //If not checked change back to original color

                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    row.style.backgroundColor = "#C2D69B";

                }

                else {

                    row.style.backgroundColor = "white";

                }

            }



            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;



        }

    </script>
    <script type="text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "#C2D69B";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

    </script>

    <script type="text/javascript">

        function MouseEvents(objRef, evt) {

            var checkbox = objRef.getElementsByTagName("input")[0];

            if (evt.type == "mouseover") {

                objRef.style.backgroundColor = "orange";

            }

            else {

                if (checkbox.checked) {

                    objRef.style.backgroundColor = "aqua";

                }

                else if (evt.type == "mouseout") {

                    if (objRef.rowIndex % 2 == 0) {

                        //Alternating Row Color

                        objRef.style.backgroundColor = "#C2D69B";

                    }

                    else {

                        objRef.style.backgroundColor = "white";

                    }

                }

            }

        }

    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
    <table id="Table1" width="1000" align="left" cellpadding="0" cellspacing="0" runat="server">
        <tr>
            <td align="center" class="auto-style1" colspan="4">
                <u>&nbsp;Dividend Cash&nbsp; Received Entry Form </u>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td align="right" class="auto-style3">
                <b>Fund Name </b>
            </td>
            <td align="left" class="auto-style3">
                <asp:DropDownList ID="fundNameDropDownList" runat="server" AutoPostBack="True"
                    TabIndex="1" OnSelectedIndexChanged="fundNameDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>

            <td align="right" class="auto-style3">
                <b>Company Name:</b>
            </td>
            <td align="left" class="auto-style3">
                <asp:DropDownList ID="companyNameDropDownList" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="companyNameDropDownList_SelectedIndexChanged"
                    TabIndex="2">
                </asp:DropDownList>
            </td>




        </tr>

        <tr>
            <td align="right">Payment Date
            </td>
            <td align="left">
                <asp:TextBox ID="PaymentDateTextBox" runat="server" Width="80px"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonPayment" runat="server"
                    ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderPayment" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButtonPayment" TargetControlID="PaymentDateTextBox"></ajaxToolkit:CalendarExtender>
            </td>
            <td align="right">Tax Rate 
            </td>
            <td align="left">
                <asp:TextBox ID="TaxRateTextBox" runat="server" Width="80px"  onkeypress="fncInputNumericValuesOnly()" AutoPostBack="True" OnTextChanged="TaxRateTextBox_TextChanged">20</asp:TextBox>%
            </td>


        </tr>
        <tr>
            <td align="right" class="auto-style3">
                <b>Payment Type:</b>
            </td>
            <td align="left" class="auto-style3" colspan="3">
                <asp:DropDownList ID="PaymenttypeDropDownList" runat="server" TabIndex="3">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="BFTN" Value="B"></asp:ListItem>
                    <asp:ListItem Text="Warrent" Value="W"></asp:ListItem>
                    <asp:ListItem Text="Cash" Value="C"></asp:ListItem>

                </asp:DropDownList>
            </td>
            
        </tr>

        <tr>
            <td align="center" colspan="4">&nbsp;</td>
        </tr>

        <tr>
            <td align="center" class="style3" colspan="4">
                <asp:Button ID="postedButton" runat="server" CssClass="auto-style2" Text="Save Received Cash "
                    Visible="true" Width="138px" Height="26px" OnClick="postedButton_Click" OnClientClick="return fnCheckInput();" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="4">

                <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="RowDataBound"
                    Width="603px" ShowHeader="true">

                    <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>

                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBoxAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="leftCheckBox" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="f_cd" HeaderText="f_cd" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DIV_RRCIVABLE_ACC_VCH_NO" HeaderText="Acc. VCH. No." ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BOOK_CL_DET_ID" HeaderText="Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>
                        <asp:BoundColumn DataField="f_name" HeaderText="Fund Name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COMP_CD" HeaderText="COMP_CD"></asp:BoundColumn>
                        <asp:BoundColumn DataField="comp_nm" HeaderText="Comp. Name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="tot_nos" HeaderText="No. Share"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TYPE" HeaderText="Decl. Type" />
                        <asp:BoundColumn DataField="RECORD_DT" HeaderText="Record Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundColumn DataField="AGM" HeaderText="AGM Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundColumn DataField="FY" HeaderText="FY" />
                        <asp:BoundColumn DataField="FY_PART" HeaderText="FY Part" />
                        <asp:BoundColumn DataField="DIVIDEND_PER_SHR" HeaderText="@ Share"></asp:BoundColumn>
                        <asp:BoundColumn DataField="GROSS_DIVIDEND" HeaderText="GROSS"></asp:BoundColumn>
                  <asp:BoundColumn DataField="TAX_AMT" HeaderText="TAX"></asp:BoundColumn>
                       <asp:BoundColumn DataField="NET_AMT" HeaderText="NET"></asp:BoundColumn>  
                                                                         
                        <asp:TemplateColumn HeaderText="Frac. Div. Amt.">
                            <ItemTemplate>
                                <asp:TextBox ID="EXCESSSHORTAGE" runat="server" Width="80px" Text='<%#Eval("FRAC_DIV_AMT") %>'></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateColumn>


                        <asp:TemplateColumn HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="RemarksTextBox" runat="server" Width="100px"  Text='<%#Eval("REMARKS") %>'  TabIndex="2"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateColumn>


                                                                                                      
                       
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>


</asp:Content>
