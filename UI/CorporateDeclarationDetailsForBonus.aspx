<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/AMCLCommon.master" CodeFile="CorporateDeclarationDetailsForBonus.aspx.cs" Inherits="UI_CorporateDeclarationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
        .scrollit {
            overflow:scroll;
            height:600px;
            width:1000Px;
        }

         .hg_left {
            display: none;
          }
            .hiddencol
          {
            display: none;
          }

         .auto-style1 {
             font-size: large;
             color: #9933FF;
             font-weight: bold;
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
     <table id="Table1" width="1200" align="left" cellpadding="0" cellspacing="0" runat="server">
        <tr>
            <td align="center" class="style3">
                <u><span class="auto-style1">Corporate Declaration Bonus Entry Form </span> </u>
            </td>
        </tr>
          <tr>
            <td align="center" class="style3">
              
                &nbsp;&nbsp;</td>
        </tr>
           <tr>
            
        
            <td align="center" class="style3" >AS on Record Date    
            
                <asp:TextBox ID="TextBoxRecordDate" runat="server" Width="80px" ReadOnly="true" ></asp:TextBox>
            </td>


        </tr>
          <tr>
            <td align="center" class="style3">
              <asp:Button ID="postedButton" runat="server" CssClass="buttoncommon"   Text="Save"
                    OnClick="postedButton_Click" Visible="false" TabIndex="19" AccessKey="s" />
            </td>
        </tr>
           <tr>
            <td align="center" class="style3">
              
                &nbsp;&nbsp;</td>
        </tr>
          <tr>
            <td  align="left">
              
                 <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                   ShowHeader="true" >
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
                    <asp:BoundColumn DataField="f_cd" HeaderText="Fund Code"></asp:BoundColumn>                                                                                              
                    <asp:BoundColumn DataField="f_name" HeaderText="Fund Name"></asp:BoundColumn>
                     <asp:BoundColumn  DataField="comp_cd" HeaderText="Comp Code"></asp:BoundColumn>                                                                                               
                    <asp:BoundColumn  DataField="comp_nm" HeaderText="Company"></asp:BoundColumn> 
                    <asp:BoundColumn  DataField="tot_nos" HeaderText="Total Sahre"></asp:BoundColumn> 
                    <asp:BoundColumn  DataField="TYPE" HeaderText="Type"></asp:BoundColumn> 
                    <asp:BoundColumn  DataField="FY" HeaderText="FY"></asp:BoundColumn> 
                    <asp:BoundColumn  DataField="RECORD_DT" HeaderText="Record Date" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundColumn> 
                    <asp:BoundColumn  DataField="AGM" HeaderText="AGM Date" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundColumn> 
                    <asp:BoundColumn  DataField="APPR_DT" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundColumn>
                    <asp:BoundColumn  DataField="QUANTITY" HeaderText="%"></asp:BoundColumn>
                    <asp:BoundColumn  DataField="share_alloted" HeaderText="Alloted Share"></asp:BoundColumn>
                    <asp:BoundColumn  DataField="BOOK_CL_ID"  HeaderText="Book Closer ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>

                                                             
                </Columns>
            </asp:DataGrid>             
            </td>
        </tr>
    </table>
   
    

 </asp:Content>
