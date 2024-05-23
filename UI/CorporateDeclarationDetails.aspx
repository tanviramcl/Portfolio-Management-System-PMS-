<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeFile="CorporateDeclarationDetails.aspx.cs" Inherits="UI_CorporateDeclarationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 31px;
        }
        .scrollit {
            overflow:scroll;
            height:600px;
            width:1200Px;
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
     <table id="Table1" width="1000" align="center" cellpadding="0" cellspacing="0" runat="server">
        <tr>
            <td align="center" class="style3">
                <b><u>Corporate Declaration Details </u></b>
            </td>
        </tr>
    </table>
    <table id="Table3" width="100" align="center" cellpadding="2" cellspacing="2" runat="server">
        <tr>
            <td colspan="4" align="center">
              
                 <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                   Width="403px" ShowHeader="true"  >
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
                         <asp:CheckBox ID="leftCheckBox" runat="server"   ></asp:CheckBox> 
                    </ItemTemplate>                    
                    </asp:TemplateColumn>                    

                     <asp:BoundColumn DataField="f_cd" HeaderText="Fund Code" />
                     <asp:BoundColumn DataField="f_name" HeaderText="Fund Name" />
                        <asp:BoundColumn DataField="BOOK_CL_ID" HeaderText="Book Closer ID" />
                        <asp:BoundColumn DataField="comp_cd" HeaderText="Company Code" />
                        <asp:BoundColumn DataField="comp_nm"  HeaderText="Company Name" />
                        <asp:BoundColumn DataField="tot_nos" HeaderText="Total Sahre" />
                        <asp:BoundColumn DataField="TYPE" HeaderText="Declaration Type" />
     
                        <asp:BoundColumn DataField="FY" HeaderText="FY" />
                     <%--   <asp:TemplateColumn HeaderText="Record Date">
                            <ItemTemplate>
                                <asp:TextBox ID="recordDateTextBox" runat="server" Enabled="false" Width="100px" Text='<%# Eval("RECORD_DT", "{0: dd-MMM-yyyy}") %>' TabIndex="4"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateColumn>
                                       
                        <asp:TemplateColumn HeaderText="AGM Date">
                            <ItemTemplate>
                                <asp:TextBox ID="agmDateTextBox" runat="server" Enabled="false" Width="100px" Text='<%# Eval("AGM", "{0: dd-MMM-yyyy}") %>' TabIndex="8"></asp:TextBox>

                                <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateColumn>--%>
                        <asp:BoundColumn DataField="APPR_DT" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundColumn DataField="QUANTITY" HeaderText="Quantity" />
                        <asp:BoundColumn DataField="DIVIDEND_PER_SHARE" HeaderText="Dividend per  Share" />
                        <asp:BoundColumn DataField="GROSS_DIVIDEND" HeaderText="Gross Dividend" />
                        <asp:BoundColumn DataField="TAX" HeaderText="TAX" />
                        <asp:BoundColumn DataField="NET_DIVIDEND" HeaderText="Net Dividend" />
                                  

                                                             
                </Columns>
            </asp:DataGrid>             
            </td>
        </tr>
    </table>
     <table id="Table5" width="1000" align="center" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td align="center" class="style3">
                    <asp:Button ID="postedButton" runat="server" CssClass="buttoncommon" Text="Posted"
                    OnClick="postedButton_Click" Visible="true" TabIndex="19" AccessKey="s" />
                </td>
            </tr>
        </table>

 </asp:Content>
