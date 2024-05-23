<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CorporateDeclarationSecurities.aspx.cs" Inherits="UI_CorporateDeclarationSecurities" Title="Corporate Declaration Entry Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function fnClearFields() {
            var Check = confirm("Are You Sure To Clear");
            if (Check) {
                document.getElementById("<%=companyCodeTextBox.ClientID%>").value = "";
                document.getElementById("<%=companyNameDropDownList.ClientID%>").value = "";
                document.getElementById("<%=typeDropDownList.ClientID%>").value = "";
                document.getElementById("<%=ApprovalDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=financialYearTextBox.ClientID%>").value = "";
                document.getElementById("<%=recordDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=agmDateTextBox.ClientID%>").value = "";
                document.getElementById("<%=remarksTextBox.ClientID%>").value = "";
                document.getElementById("<%=QuantityTextBox.ClientID%>").value = "";
                document.getElementById("<%=recordDateFromTextBox.ClientID%>").value =""; 
                document.getElementById("<%=recordDateToTextBox.ClientID%>").value ="";
                document.getElementById("<%=agmDateFromTextBox.ClientID%>").value =""; 
                document.getElementById("<%=agmDateToTextBox.ClientID%>").value ="";
                return false;
            }

        }
    function fnCheckSearch() {
     <%--       if (document.getElementById("<%=companyNameDropDownList.ClientID%>").value == "") {
        alert("Please Select a Company Name ");
        document.getElementById("<%=companyNameDropDownList.ClientID%>").focus();
         return false;

     }--%>
    var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
        if(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=recordDateFromTextBox.ClientID%>").focus();
            alert("Please Insert Reord Date From.");
            return false; 
        }
        if(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=recordDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=recordDateToTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=recordDateToTextBox.ClientID%>").focus();
            alert("Please Insert Record Date To.");
            return false; 
        }
        if(document.getElementById("<%=recordDateToTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=recordDateToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=recordDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        
        
        if(document.getElementById("<%=agmDateFromTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=agmDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=agmDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        
        if(document.getElementById("<%=agmDateToTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=agmDateToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=agmDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
 }
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
     if (document.getElementById("<%=financialYearTextBox.ClientID%>").value == "") {
        alert("Please Enter Financial year To Search");
        document.getElementById("<%=financialYearTextBox.ClientID%>").focus();
         return false;
     }
     if (document.getElementById("<%=recordDateTextBox.ClientID%>").value == "") {
        document.getElementById("<%=recordDateTextBox.ClientID%>").focus();
        alert("Please Insert Reord Date");
        return false;
    }
    if (document.getElementById("<%=agmDateTextBox.ClientID%>").value == "") {
        document.getElementById("<%=agmDateTextBox.ClientID%>").focus();
         alert("Please Insert AGM Date. If AGM Date is not available then AGM Date will be Same as Record Date.");
         return false;
     }

     if (document.getElementById("<%=QuantityTextBox.ClientID%>").value == "") {
        document.getElementById("<%=QuantityTextBox.ClientID%>").focus();
           alert("Please Insert Quantity ");
           return false;
       }

        if (document.getElementById("<%=DropDownListFYPART.ClientID%>").value == "0") {
        alert("Please Select a  FY ");
        document.getElementById("<%=DropDownListFYPART.ClientID%>").focus();
                 return false;

             }


         }

    </script>
    <style type="text/css">
         .scrollit {
            overflow:scroll;
            height:600px;
            width:1000Px;
        }

        .auto-style4 {
            font-size: large;
        }

        .auto-style5 {
            font-size: large;
            color: #9900CC;
        }

        .auto-style6 {
            overflow: auto;
            height: 600px;
            width: 1590px;
        }
           .hiddencol
          {
            display: none;
          }
        .ClassA {
            background-color: #008CBA;
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

    <div id="dvUpdatePanel" runat="server">

        <table id="Table1" width="1200px" align="left" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td align="center" class="auto-style5" colspan="4">
                  <strong><u>Corporate Declaration Entry Form </u></strong>  
                </td>
            </tr>
             <tr>
                <td align="center" class="auto-style4" colspan="4">
                 
                    &nbsp;</td>
            </tr>
           <tr>
                <td align="right" >
                    <b>Company Name </b>
                </td>
                <td align="left" >
                    <asp:DropDownList ID="companyNameDropDownList"  runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="companyNameDropDownList_SelectedIndexChanged"
                        TabIndex="1">
                    </asp:DropDownList>                   
                </td>

                <td align="right" >
                    <b>Company Code:</b>
                </td>
                <td align="left" >
                    <asp:TextBox ID="companyCodeTextBox" runat="server" Width="27px" ReadOnly="True"></asp:TextBox>
                </td>


            </tr>
           <tr>
                <td align="right" >Financial Year
                </td>
                <td align="left">
                    <asp:TextBox ID="financialYearTextBox" runat="server"  TabIndex="2"></asp:TextBox>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorfinancialYearTextBox" runat="server" ErrorMessage="Financial Year cannot be blank" ControlToValidate="financialYearTextBox" ForeColor="Red"></asp:RequiredFieldValidator> --%>
                    
                </td>
                <td align="right">
                    <b>Type</b>
                </td>
                <td align="left">
                    <asp:DropDownList ID="typeDropDownList" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="typeDropDownList_SelectedIndexChanged" TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="BONUS" Value="B"></asp:ListItem>
                        <asp:ListItem Text="CASH" Value="C"></asp:ListItem>
                        <asp:ListItem Text="RIGHT" Value="R"></asp:ListItem>

                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="typeDropDownList" InitialValue="0" runat="server" ForeColor="Red" />--%>
                </td>


            </tr>

            <tr>
                <td align="right" >Record Date 
                </td>
                <td align="left">
                    <asp:TextBox ID="recordDateTextBox" runat="server" TabIndex="4"></asp:TextBox>
                    <asp:ImageButton ID="recordDateImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
                    <ajaxToolkit:CalendarExtender ID="recordDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="recordDateImageButton" TargetControlID="recordDateTextBox"></ajaxToolkit:CalendarExtender>
                    <%--<asp:RequiredFieldValidator ID="recordDateDate" runat="server" ControlToValidate="recordDateTextBox" ErrorMessage="Please select Record Date" ForeColor="Red"  SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                </td>

                <td align="right" style="font-weight: 700">AGM Date
                </td>
                <td align="left">
                    <asp:TextBox ID="agmDateTextBox" runat="server" TabIndex="8"></asp:TextBox>
                    <asp:ImageButton ID="agmDateImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="9" />
                    <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
                    <%-- <asp:RequiredFieldValidator ID="agmDateRequiredFieldValidator" runat="server" ControlToValidate="agmDateTextBox" ErrorMessage="Please select AGM Date" ForeColor="Red"  SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                </td>

            </tr>

            <tr>
                <td align="right">Remarks
                </td>
                <td align="left">
                    <asp:TextBox ID="remarksTextBox" runat="server" TextMode="MultiLine"
                        TabIndex="15"></asp:TextBox>
                </td>
                <td align="right" style="font-weight: 700">Stock/Cash/Right
                </td>
                <td align="left">
                    <asp:TextBox ID="QuantityTextBox" runat="server" TabIndex="10" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>%                    
                </td>

            </tr>
            <tr>
                <td align="right">BSEC/CDBL Approval Date
                </td>
                <td align="left">
                    <asp:TextBox ID="ApprovalDateTextBox" runat="server" 
                        TabIndex="13"></asp:TextBox>
                    <asp:ImageButton ID="ApprovalDateImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="14" />
                    <ajaxToolkit:CalendarExtender ID="ApprovalCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ApprovalDateImageButton" TargetControlID="ApprovalDateTextBox"></ajaxToolkit:CalendarExtender>
                </td>
                <td align="right">FY  Part
                </td>
                <td align="left">
                  
                    <asp:DropDownList ID="DropDownListFYPART" runat="server" AutoPostBack="True"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="FINAL" Value="FINAL"></asp:ListItem>
                        <asp:ListItem Text="INRRIM-1" Value="INRRIM-1"></asp:ListItem>
                        <asp:ListItem Text="INRRIM-2" Value="INRRIM-2"></asp:ListItem>
                        <asp:ListItem Text="INRRIM-3" Value="INRRIM-3"></asp:ListItem>
                        <asp:ListItem Text="INRRIM-4" Value="INRRIM-4"></asp:ListItem>

                    </asp:DropDownList>
                </td>

            </tr>
             <tr>
                <td align="center" class="auto-style4" colspan="4">
                 
                    &nbsp;</td>
            </tr>
              <tr>
                <td align="center" colspan="4">

                   
                     <asp:Button ID="addNewButton" runat="server" CssClass="buttoncommon" Text="Add New"
                        OnClick="addNewButton_Click" OnClientClick="return fnCheckInput();" TabIndex="19" AccessKey="s" />
                     <asp:Button ID="updateButton" Visible="false" runat="server" CssClass="buttoncommon" Text="Update"
                                    OnClick="updateButton_Click" TabIndex="19" AccessKey="s" />
                    <asp:Button ID="ButtonDelete" runat="server"  CssClass="buttoncommon" Text="Delete"
                                    OnClick="btnDelete_Click" Visible="false" TabIndex="19" AccessKey="s" />
                    <asp:Button ID="clearButton" runat="server" CssClass="buttoncommon" Text="Clear"
                        OnClientClick="return fnClearFields();" OnClick="clearButton_Click"
                        TabIndex="20" />
                    <asp:Button ID="ButtonSearch" runat="server" CssClass="buttoncommon" Text="Search"
                        OnClick="SearchButton_Click"
                        TabIndex="20" />

                </td>
            </tr>
              <tr>
                <td colspan="4" align="center">

                    <asp:Panel ID="Panel1" Visible="false" runat="server" BorderColor="#990000" BorderStyle="Solid"
                        Height="180px" ScrollBars="Both" Style="width: 578px">
                        <table align="center">
                            <tr>
                                <td style="font-weight: 700" class="style9">&nbsp;&nbsp;&nbsp; Record<b> Date From:&nbsp;&nbsp;</b></td>
                                <td align="left" class="style4">
                                    <asp:TextBox ID="recordDateFromTextBox" runat="server" Style="width: 100px;"
                                        CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="recordDateFromTextBox"
                                        PopupButtonID="recordDateFromImageButton" Format="dd-MMM-yyyy" />
                                    <asp:ImageButton ID="recordDateFromImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /><b>&nbsp;To:&nbsp;&nbsp; </b>
                                    <asp:TextBox ID="recordDateToTextBox" runat="server" Style="width: 100px;"
                                        CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="recordDateToTextBox"
                                        PopupButtonID="recordDateToImageButton" Format="dd-MMM-yyyy" />
                                    <asp:ImageButton ID="recordDateToImageButton" runat="server" AlternateText="Click Here"
                                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;<br />
                                    <span class="style8">(</span><span class="style7">DD-MMM-YYYY)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <span class="style8">&nbsp;&nbsp; (</span>DD-MMM-YYYY)</span></td>

                            </tr>
                            <tr>
                                <td style="font-weight: 700" class="style10">AGM Date From:&nbsp;&nbsp;</td>
                                <td align="left" class="style4">
                                    <asp:TextBox ID="agmDateFromTextBox" runat="server" Style="width: 100px;"
                                        CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="agmDateFromTextBox"
                                        PopupButtonID="agmDateFromImageButton" Format="dd-MMM-yyyy" />
                                    <asp:ImageButton ID="agmDateFromImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" />&nbsp;<b>To:&nbsp;&nbsp; </b>

                                    <asp:TextBox ID="agmDateToTextBox" runat="server" Style="width: 100px;"
                                        CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="agmDateToTextBox"
                                        PopupButtonID="agmDateToImageButton" Format="dd-MMM-yyyy" />
                                    <asp:ImageButton ID="agmDateToImageButton" runat="server" AlternateText="Click Here"
                                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;<br />
                                    <span class="style8">(DD-MMM-YYYY)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (<span class="style7">DD-MMM-YYYY)</span></span></td>

                            </tr>

                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="showButton" runat="server" Text="Show" OnClick="ShowButton_Click"
                                        OnClientClick="return fnCheckSearch();" CssClass="buttoncommon" TabIndex="5" />
                                    <asp:Button ID="closeButton" runat="server" Text="Close"
                                        CssClass="buttoncommon" TabIndex="6" OnClick="closeButton_Click" />
                                </td>

                            </tr>

                        </table>
                    </asp:Panel>

                </td>
            </tr>
               <tr>
                <td colspan="4" align="left">
                    <table align="left" >
                        <tr>
                            <td>
                                <div id="dvGridBoolCloser"  runat="server" visible="true"
                                    style="text-align: center; display: block; ">
                                 
                                <asp:GridView ID="grdShowBookCloser" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="grdShowBookCloser_SelectedIndexChanged"
                                    BackColor="#DEBA84" AllowPaging="true" OnRowDataBound="RowDataBound"
                                    OnPageIndexChanging="OnPageIndexChanging" PageSize="30"
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="2" >
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
                                        <asp:BoundField DataField="BOOK_CL_ID" HeaderText="SI#"  ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>
                                        <asp:BoundField DataField="COMP_CD" HeaderText="Company Code" />
                                        <asp:BoundField DataField="COMP_NM" HeaderText="Company Name" />
                                       
                                        <asp:BoundField DataField="FY" HeaderText="Finacial Year" />
                                        <asp:BoundField DataField="RECORD_DT" HeaderText="Record Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <%--<asp:TemplateField HeaderText="Record Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="recordDateTextBox" runat="server" Width="80px" Text='<%# Eval("RECORD_DT", "{0: dd-MMM-yyyy}") %>' TabIndex="4"></asp:TextBox>
                                                <asp:ImageButton ID="recordDateImageButton" runat="server"
                                                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
                                                <ajaxToolkit:CalendarExtender ID="recordDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="recordDateImageButton" TargetControlID="recordDateTextBox"></ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>  --%>                                     
                                        <asp:TemplateField HeaderText="FY Part">
                                            <ItemTemplate>
                                             
                                                <asp:TextBox runat="server" ID="FY_PARTTextBox" Width="80px" Style="text-transform: uppercase" Text='<%#Eval("FY_PART") %>'></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TYPE" HeaderText="Type" />
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="QuantityTextBox" runat="server" Width="40px" Text='<%#Eval("QUANTITY") %>' TabIndex="2"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="AGM Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="agmDateTextBox" runat="server" Width="80px" Text='<%# Eval("AGM", "{0: dd-MMM-yyyy}") %>' TabIndex="8"></asp:TextBox>
                                                <asp:ImageButton ID="agmDateImageButton" runat="server"
                                                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="9" />
                                                <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      

                                        <asp:TemplateField HeaderText="Approve Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ApprovalDateTextBox" runat="server" Width="80px" Text='<%# Eval("APPR_DT", "{0: dd-MMM-yyyy}") %>' TabIndex="13"></asp:TextBox>
                                                <asp:ImageButton ID="ApprovalDateImageButton" runat="server"
                                                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="14" />
                                                <ajaxToolkit:CalendarExtender ID="ApprovalCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ApprovalDateImageButton" TargetControlID="ApprovalDateTextBox"></ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="REMARKS">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="REMARKSTextBox" TextMode="MultiLine" Text='<%#Eval("REMARKS") %>' Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="POSTED" HeaderText="POSTED" />
                                       
                                        <asp:BoundField DataField="ENTRY_DATE" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                                        <asp:TemplateField HeaderText="As on Record Date">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDettCollez" runat="server" CausesValidation="false"
                                                    CommandArgument='<%# Container.DataItemIndex %>' CommandName="Select" Text="Entry" OnClick="btnDettCollez_Click" />
                                            </ItemTemplate>
                                            <ControlStyle CssClass="btn btn-info" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </div>

                             
                               
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>       
       
    </div>





</asp:Content>


