<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeFile="BookCloserDetails.aspx.cs" Inherits="UI_BookCloserDetails" Title="Book Closer Delatls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 31px;
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

    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" AsyncPostBackTimeout="4000"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table id="Table1" width="1000" align="center" cellpadding="0" cellspacing="0" runat="server">
        <tr>
            <td align="center" class="style3">
                <b><u>Corporate Declaration Details </u></b>
            </td>
        </tr>
    </table>
    <table id="Table2" width="1000" align="center" cellpadding="2" cellspacing="2" runat="server">
        <tr>
            <td align="left">
                <b>Company Name </b>
            </td>
            <td align="left">
                <asp:DropDownList ID="companyNameDropDownList" Enabled="false" Width="300px" runat="server" AutoPostBack="True"
                    TabIndex="1">
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="companyNameDropDownList" InitialValue="0" runat="server" ForeColor="Red" />--%>
            </td>

        
            <td align="left">
                <b>Type</b>
            </td>
            <td align="left">
                <asp:DropDownList ID="typeDropDownList"  Enabled="false" Width="100px" runat="server"
                    TabIndex="3">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="BONUS" Value="B"></asp:ListItem>
                    <asp:ListItem Text="CASH" Value="C"></asp:ListItem>
                    <asp:ListItem Text="RIGHT" Value="R"></asp:ListItem>

                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="typeDropDownList" InitialValue="0" runat="server" ForeColor="Red" />--%>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-weight: 700">Financial Year
            </td>
            <td align="left">
                <asp:DropDownList ID="FYDropDownList" Enabled="false" Width="300px" runat="server" OnSelectedIndexChanged="FYDropDownList_SelectedIndexChanged" AutoPostBack="True" TabIndex="1">
                </asp:DropDownList>


            </td>
        
            <td align="left" style="font-weight: 700">Record Date 
            </td>
            <td align="left">
                <asp:TextBox ID="recordDateTextBox" Enabled="false" runat="server" Width="100px" TabIndex="4"></asp:TextBox>
                <asp:ImageButton ID="recordDateImageButton" runat="server"
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
                <ajaxToolkit:CalendarExtender ID="recordDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="recordDateImageButton" TargetControlID="recordDateTextBox"></ajaxToolkit:CalendarExtender>
                <%--<asp:RequiredFieldValidator ID="recordDateDate" runat="server" ControlToValidate="recordDateTextBox" ErrorMessage="Please select Record Date" ForeColor="Red"  SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-weight: 700">AGM Date
            </td>
            <td align="left">
                <asp:TextBox ID="agmDateTextBox"  Enabled="false" runat="server" Width="100px" TabIndex="8"></asp:TextBox>
                <asp:ImageButton ID="agmDateImageButton" runat="server"
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="9" />
                <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
                <%-- <asp:RequiredFieldValidator ID="agmDateRequiredFieldValidator" runat="server" ControlToValidate="agmDateTextBox" ErrorMessage="Please select AGM Date" ForeColor="Red"  SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            </td>

        </tr>
    <%--    <tr>
            <td align="center" colspan="6">
                <asp:Button ID="searchButton" runat="server" CssClass="buttoncommon" Text="GO"
                    OnClick="searchButton_Click" Visible="true" TabIndex="19" AccessKey="s" />


            </td>
        </tr>--%>

    </table>
    <table id="Table3" width="1000" align="center" cellpadding="2" cellspacing="2" runat="server">
        <tr>
            <td colspan="4" align="center">

                <div id="dvGridCASH" runat="server" visible="true"
                    style="text-align: center; display: block; overflow: auto;">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:GridView ID="grdGridCAsh" align="center" runat="server" AutoGenerateColumns="False"
                                    BackColor="#DEBA84" AllowPaging="true" OnRowDataBound="RowDataBound" PageSize="15"  OnPageIndexChanging="OnPageIndexChanging" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="2" Width="618px">
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                    <Columns>
                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBoxAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="leftCheckBox" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="f_cd" HeaderText="Fund Code" />
                                        <asp:BoundField DataField="f_name" HeaderText="Fund Name" />
                                         <asp:BoundField DataField="BOOK_CL_ID" HeaderText="Book Closer ID" />
                                        <asp:BoundField DataField="comp_cd" HeaderText="Company Code" />
                                         <asp:BoundField DataField="comp_nm" HeaderText="Company Name" />
                                        <asp:BoundField DataField="tot_nos" HeaderText="Total Sahre" />
                                        <asp:BoundField DataField="TYPE" HeaderText="Declaration Type" />
                                      <%--  <asp:BoundField DataField="FY" HeaderText="FY" />--%>
                                        <asp:BoundField DataField="FY" HeaderText="FY" />
                                        <asp:TemplateField HeaderText="Record Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="recordDateTextBox" runat="server" Enabled="false" Width="100px" Text='<%# Eval("RECORD_DT", "{0: dd-MMM-yyyy}") %>' TabIndex="4"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="RECORD_DT"  HeaderText="Record Date" />--%>
                                        <%--<asp:BoundField DataField="AGM" HeaderText="AGM Date" />--%>
                                        <asp:TemplateField HeaderText="AGM Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="agmDateTextBox" runat="server" Enabled="false" Width="100px" Text='<%# Eval("AGM", "{0: dd-MMM-yyyy}") %>' TabIndex="8"></asp:TextBox>

                                                <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="APPR_DT" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" />
                                        <asp:BoundField DataField="DIVIDEND_PER_SHARE" HeaderText="Dividend per  Share" />
                                        <asp:BoundField DataField="GROSS_DIVIDEND" HeaderText="Gross Dividend" />
                                        <asp:BoundField DataField="TAX" HeaderText="TAX" />
                                        <asp:BoundField DataField="NET_DIVIDEND" HeaderText="Net Dividend" />
                                       <%-- <asp:BoundField DataField="share_alloted" HeaderText="Alloted Share" />--%>
                                        <asp:TemplateField HeaderText="Posting Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="postingDateTextBox" runat="server" Enabled="true" Width="100px" Text='<%# Eval("pdate", "{0: dd-MMM-yyyy}") %>' TabIndex="8"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="pdate" HeaderText="Posting Date" />--%>
                                    </Columns>
                                </asp:GridView>
                </div>



            </td>
        </tr>
    </table>

    <table id="Table4" width="1000" align="center" cellpadding="2" cellspacing="2" runat="server">
        <tr>
            <td colspan="4" align="center">

                <div id="dvGridBonus" runat="server" visible="true"
                    style="text-align: center; display: block; overflow: auto;">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:GridView ID="grdGridBonus" align="center" runat="server" AutoGenerateColumns="False"
                                    BackColor="#DEBA84" AllowPaging="true" OnRowDataBound="RowDataBound" PageSize="15"  OnPageIndexChanging="OnPageIndexChanging" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="2" Width="618px">
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                    <Columns>
                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBoxAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="leftCheckBox" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="f_cd" HeaderText="Fund Code" />
                                        <asp:BoundField DataField="f_name" HeaderText="Fund Name" />
                                         <asp:BoundField DataField="BOOK_CL_ID" HeaderText="Book Closer ID" />
                                        <asp:BoundField DataField="comp_cd" HeaderText="Company Code" />
                                         <asp:BoundField DataField="comp_nm" HeaderText="Company Name" />
                                        <asp:BoundField DataField="tot_nos" HeaderText="Total Sahre" />
                                        <asp:BoundField DataField="TYPE" HeaderText="Declaration Type" />
                                      <%--  <asp:BoundField DataField="FY" HeaderText="FY" />--%>
                                        <asp:BoundField DataField="FY" HeaderText="FY" />
                                        <asp:TemplateField HeaderText="Record Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="recordDateTextBox" runat="server" Enabled="false" Width="100px" Text='<%# Eval("RECORD_DT", "{0: dd-MMM-yyyy}") %>' TabIndex="4"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="RECORD_DT"  HeaderText="Record Date" />--%>
                                        <%--<asp:BoundField DataField="AGM" HeaderText="AGM Date" />--%>
                                        <asp:TemplateField HeaderText="AGM Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="agmDateTextBox" runat="server" Enabled="false" Width="100px" Text='<%# Eval("AGM", "{0: dd-MMM-yyyy}") %>' TabIndex="8"></asp:TextBox>

                                                <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="APPR_DT" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" />
                                        <asp:BoundField DataField="share_alloted" HeaderText="Alloted Share" />
                                        <asp:TemplateField HeaderText="Posting Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="postingDateTextBox" runat="server" Enabled="true" Width="100px" Text='<%# Eval("pdate", "{0: dd-MMM-yyyy}") %>' TabIndex="8"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="pdate" HeaderText="Posting Date" />--%>
                                    </Columns>
                                </asp:GridView>
                </div>



            </td>
        </tr>
    </table>
      <table id="Table5" width="1000" align="center" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td align="center" class="style3">
                    <asp:Button ID="postedButton" runat="server" CssClass="buttoncommon" Text="Posted"
                    OnClick="postedButton_Click" Visible="False" TabIndex="19" AccessKey="s" />
                </td>
            </tr>
        </table>

</asp:Content>


