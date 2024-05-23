<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CompanyInformation.aspx.cs" Inherits="UI_CompanyInformation" Title="Company Information" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
  <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        }  
    </style> 
  

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
 
       <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
   
        <div align="center">
            <table id="Table1" width = "600" align = "center" cellpadding ="0" cellspacing ="0" runat="server">
            <tr>
                <td align="center" class="style3">
                    <b><u>Company Information</u></b>
                </td>
            </tr>
            </table>
             <table id="Table2" width="600" align="center" cellpadding="2" cellspacing="2" runat="server">
            <tr>
                <td align="left">
                    <b>Company code </b>
                </td>
                <td align="left">
                    <asp:TextBox ID="companyCodeTextBox" runat="server" Width="100px" OnTextChanged="companyCodeTextBox_TextChanged"  AutoPostBack="true"></asp:TextBox>
                </td>

                <td align="left">
                    <b>Company Name </b>
                </td> 
                <td align="left">
                   
                    <asp:TextBox ID="companyNameTextBox" runat="server" Width="100px"></asp:TextBox>

                </td>
            </tr>
            <tr>
               
                <td align="left">
                    <b>Sector </b>
                </td>
                <td align="left">
                   <%-- <asp:TextBox ID="sectorTextBox" runat="server" Width="100px"></asp:TextBox>--%>
                     <asp:DropDownList ID="sectorDropDownList" runat="server" TabIndex="3"></asp:DropDownList>
                </td>

                <td align="left">
                    <b>Category </b>
                </td>
                <td align="left">
                     <asp:DropDownList ID="categoryDropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Share" Value="SH"></asp:ListItem>
                        <asp:ListItem Text="Mutual Fund" Value="DB"></asp:ListItem>
                         <asp:ListItem Text="Debenture" Value="D"></asp:ListItem>
                         <asp:ListItem Text="Bond" Value="B"></asp:ListItem>
                        <asp:ListItem Text="S" Value="S"></asp:ListItem>
                        <asp:ListItem Text="L" Value="L"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td align="left">
                    <b>Prospectus Publishing Date</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtProspectusPublishDate" runat="server" Width="100px"></asp:TextBox>
                  <%--  <asp:ImageButton ID="openingdateTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                    <ajaxToolkit:CalendarExtender ID="openingdateTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="openingdateTextBoxImageButton" TargetControlID="openingdateTextBox"></ajaxToolkit:CalendarExtender>--%>
                </td>

                
                <td align="left">
                    <b>Avarage Market Rate</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="avarageMarketRateTextBox" runat="server" Width="100px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>Face value</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="faceValueTextBox" runat="server" Width="100px" ></asp:TextBox>
                </td>

                <td align="left">
                    <b>Last Trading date</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="lasttradingdateTextBox" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                    <%--<asp:ImageButton ID="lasttradingdateTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                    <ajaxToolkit:CalendarExtender ID="lasttradingdateTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="lasttradingdateTextBoxImageButton" TargetControlID="lasttradingdateTextBox"></ajaxToolkit:CalendarExtender>
                    <asp:TextBox ID="lastTradindDate" runat="server" Width="100px"></asp:TextBox>--%>
                </td>
            </tr>
            <tr>


                <td align="left">
                    <b>Dse code</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="dsecodeTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>

                <td align="left">
                    <b>CSE code</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="csecodeTextBox" runat="server" Width="100px" ></asp:TextBox>
                </td>


            </tr>
            <tr>
                <td align="left">
                    <b>Group type</b>
                </td>
                <td>
                    <asp:DropDownList ID="groupDropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A Group" Value="A"></asp:ListItem>
                        <asp:ListItem Text="B Group" Value="B"></asp:ListItem>
                        <asp:ListItem Text="G Group" Value="G"></asp:ListItem>
                        <asp:ListItem Text="N Group" Value="N"></asp:ListItem>
                        <asp:ListItem Text="Z Group" Value="Z"></asp:ListItem>


                    </asp:DropDownList>
                </td>
                 <td align="left">
                    <b>Listing Category</b>
                </td>
                <td align="left">
                   <%-- <asp:TextBox ID="flugTextBox" runat="server" Width="100px"></asp:TextBox>--%>
                       <asp:DropDownList ID="listedCategoryDropDownList" Width="100px"  AutoPostBack="true" runat="server" TabIndex="1" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>Address1</b>
                </td>
                <td align="left">
<%--                    <asp:TextBox ID="addressTextBox1" runat="server" Width="170px" Height="80px"></asp:TextBox>--%>
                    <textarea id="addressTextBox1" cols="20" rows="2" runat="server"></textarea>
                </td>
                   <td align="left">
                    <b>Address2</b>
                </td>
                <td align="left">
                  <%--  <asp:TextBox ID="addressTextBox2" runat="server" Width="170px" Height="80px"></asp:TextBox>--%>
                       <textarea id="addressTextBox2" cols="20" rows="2" runat="server"></textarea>
                </td>
            </tr>
            <tr>
             
                  <td align="left">
                    <b>Subscription Opening Date</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="openingdateTextBox" runat="server" Width="100px"></asp:TextBox>
                  <%--  <asp:ImageButton ID="openingdateTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                    <ajaxToolkit:CalendarExtender ID="openingdateTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="openingdateTextBoxImageButton" TargetControlID="openingdateTextBox"></ajaxToolkit:CalendarExtender>--%>
                </td>
                <td align="left">
                    <b>Phone No</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="phnNoTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>

               
            </tr>
            <tr>
                
                <td align="left">
                    <b>Total Share</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="totalshareTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                 <td align="left">
                    <b>Reg. Office</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="regofficeTextBox2" runat="server" Width="100px"></asp:TextBox>
                </td>
                

            </tr>
            <tr>
                <td align="left">
                    <b>Base Rate</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="baserateTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>

                <td align="left">
                    <b> Major Product</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="productTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>Base Update Date</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="baseupdateDateTextBox" runat="server" Width="100px"></asp:TextBox>
                    <%--   <asp:ImageButton ID="baseupdateDateImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                     <ajaxToolkit:CalendarExtender ID="baseupdateDateTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="baseupdateDateImageButton" TargetControlID="baseupdateDateTextBox"></ajaxToolkit:CalendarExtender>--%>

                </td>
                  <td align="left">
                    <b>Margin</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="merginTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                 <td align="left">
                    <b>Float Date from</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="floatdatefromTextBox" runat="server" Width="100px"></asp:TextBox>
                      <%--<asp:ImageButton ID="floatdatefromTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                     <ajaxToolkit:CalendarExtender ID="floatdatefromTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="floatdatefromTextBoxImageButton" TargetControlID="floatdatefromTextBox"></ajaxToolkit:CalendarExtender>--%>
                </td>
                <td align="left">
                    <b>Float Date To</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="floatdatetoTextBox" runat="server" Width="100px"></asp:TextBox>
                    <%--<asp:ImageButton ID="floatdatetoTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                     <ajaxToolkit:CalendarExtender ID="floatdatetoTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="floatdatetoTextBoxImageButton" TargetControlID="floatdatetoTextBox"></ajaxToolkit:CalendarExtender>--%>
                </td>

              
            </tr>
              <tr>
                 <td align="left">
                    <b>R Issue Date from</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="RIssuefromTextBox" runat="server" Width="100px"></asp:TextBox>
                    <%--  <asp:ImageButton ID="RIssuefromTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                     <ajaxToolkit:CalendarExtender ID="RIssuefromCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="RIssuefromTextBoxImageButton" TargetControlID="RIssuefromTextBox"></ajaxToolkit:CalendarExtender>--%>

                </td>
                <td align="left">
                    <b>R Issue Date from To</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="RIssuetoTextBox" runat="server" Width="100px"></asp:TextBox>
                   <%-- <asp:ImageButton ID="RIssuetoTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                     <ajaxToolkit:CalendarExtender ID="RIssuetoTextBoxImageButtonCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="RIssuetoTextBoxImageButton" TargetControlID="RIssuetoTextBox"></ajaxToolkit:CalendarExtender>--%>
                </td>

              
            </tr>
            <tr>
                <td align="left">
                    <b>Premium</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="premiumTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td align="left">
                    <b>Paid Up capital</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextBoxPaidUpCapital" runat="server"  Width="100px"></asp:TextBox>
                </td>
                
                 
            </tr>
             <tr>
                <td align="left">
                    <b>Authorized capital</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextBoxAuthorizedcapital" runat="server" type="number" Width="100px"></asp:TextBox>
                </td>
                
                <td align="left">
                     <% 
                         string userType = Session["UserType"].ToString();

                         if (userType == "A")
                         {

                         %>
                    <b>Is Buy Sell Charge Applicable ?</b>
                    <% } %>
                </td>
                <td align="left">
                    <% 
                         string userType = Session["UserType"].ToString();

                         if (userType == "A")
                         {

                         %>
                    <%--<asp:TextBox ID="txtIsBuySellChargeApplicable" runat="server" Width="100px" OnTextChanged="txtIsBuySellChargeApplicable_TextChanged"  AutoPostBack="true"></asp:TextBox>--%>
                     <asp:DropDownList ID="ddltxtIsBuySellChargeApplicable" Width="100px" runat="server"  AutoPostBack="true"  OnSelectedIndexChanged="ddlSellBuyCharge_SelectedIndexChanged"
                        TabIndex="3">
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    </asp:DropDownList>
                     <% } %>
                </td>
                 
            </tr>

            <tr>

                <td align="left">
                      <% 
                         string userType = Session["UserType"].ToString();

                         if (userType == "A")
                         {

                         %>
                    <b></b>
                    <asp:Label ID="lblTexAdditionalbuysellcharge" runat="server" Visible="false" Text="Additional buy/sell charge "></asp:Label>
                     <% } %>
                </td>
                <td align="left">
                    <% 
                         string userType = Session["UserType"].ToString();

                         if (userType == "A")
                         {

                         %>
                    <asp:TextBox ID="txtTexAdditionalbuysellcharge" runat="server" Visible="false" Width="100px"></asp:TextBox>
                    <% } %>
                </td>
                <td align="left">
                      <% 
                         string userType = Session["UserType"].ToString();

                         if (userType == "A")
                         {

                         %>
                     <asp:Label ID="lblEXCEP_BUYSL_COMPCT_APPLDSE" runat="server" Visible="false" Text="Additional buy/sell Commision "></asp:Label>
                     <% } %>
                </td>
                <td align="left">
                    <% 
                         string userType = Session["UserType"].ToString();

                         if (userType == "A")
                         {

                         %>
                    <asp:TextBox ID="txtEXCEP_BUYSL_COMPCT_APPLDSE" Visible="false" runat="server" Width="100px"></asp:TextBox>
                    <% } %>
                </td>
            
            </tr>


            <tr>
                <td align="left">
                    <b>Market Lot </b>
                </td>
                <td align="left">
                    <asp:TextBox ID="MarketLotTextBox" runat="server" Width="100px"></asp:TextBox>
                </td>
                  <td align="left">
                    <b>IPO type</b>
                </td>
                <td>
                    <asp:DropDownList ID="IPODropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Fixed Price" Value="Fixed Price"></asp:ListItem>
                        <asp:ListItem Text="Book building" Value="Book building"></asp:ListItem>
                        <asp:ListItem Text="Direct Listing" Value="Direct Listing"></asp:ListItem>


                    </asp:DropDownList>
                </td>

            </tr>
                 <tr>
             
                  <td align="left">
                    <b>Market type</b>
                </td>
                <td>
                    <asp:DropDownList ID="marketDropDownList" Width="100px" runat="server"
                        TabIndex="3">
                        <asp:ListItem Text="Regular" Value="R"></asp:ListItem>
                        <asp:ListItem Text="OTC" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Debt Market" Value="D"></asp:ListItem>
                        <asp:ListItem Text="SME" Value="SC"></asp:ListItem>


                    </asp:DropDownList>
                </td>
                   <td align="left">
                    <b>Issue Manager</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtissueTextBox" runat="server" Width="100px"></asp:TextBox>
                  <%--  <asp:ImageButton ID="openingdateTextBoxImageButton" runat="server"
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
                    <ajaxToolkit:CalendarExtender ID="openingdateTextBoxCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="openingdateTextBoxImageButton" TargetControlID="openingdateTextBox"></ajaxToolkit:CalendarExtender>--%>
                </td>

            </tr>
          
            <tr>
               
            <td align="center" colspan="6">
                <%--<asp:Button ID="saveButton" runat="server" Text="Save"
                    CssClass="buttoncommon" TabIndex="48" 
                    OnClick="saveButton_Click" />--%>

                    <asp:Button ID="Button1"  Text="Save" CssClass="buttoncommon" runat="server" OnClick="saveButton_Click" />
                  

            </td>

        </tr>
        </table>
        </div>
  
         
    <script type="text/javascript">
    

        $(function () {

              $('#<%=txtProspectusPublishDate.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                 //maxDate:"today",
                 onSelect: function(selected) {
                    
                 }
              });

             $('#<%=lasttradingdateTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                 //maxDate:"today",
                 onSelect: function(selected) {
                    
                 }
             });
              $('#<%=openingdateTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                 //maxDate:"today",
                 onSelect: function(selected) {
                    
                 }
             });


          $('#<%=RIssuefromTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                // maxDate:"today",
                 onSelect: function(selected) {
                    
                 }
             });
             $('#<%=RIssuetoTextBox.ClientID%>').datepicker({ 
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                 //maxDate:"today",
                 onSelect: function(selected) {

                    

                 }
             });  
               $('#<%=floatdatefromTextBox.ClientID%>').datepicker({
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                 //maxDate:"today",
                 onSelect: function(selected) {
                    
                 }
             });
             $('#<%=floatdatetoTextBox.ClientID%>').datepicker({ 
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                 //maxDate:"today",
                 onSelect: function(selected) {

                    

                 }
             });  
               $('#<%=baseupdateDateTextBox.ClientID%>').datepicker({ 
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: "dd-M-yy",
                 //maxDate:"today",
                 onSelect: function(selected) {

                    

                 }
             }); 


    });


        $.validator.addMethod("CheckDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Category."); 

        $.validator.addMethod("listedCategoryDropDownListCHeck", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"Please select a Type."); 

        $.validator.addMethod("CheckSrcorDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select a Sector."); 

        $.validator.addMethod("groupDropDownList", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select a Group."); 

        $.validator.addMethod("CheckmarketType", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select Market Type."); 
        $.validator.addMethod("CheckIpoType", function (value, element, param) {  
            if (value == '0')  
                return false;  
            else  
                return true;  
        },"*Please select IPO Type."); 
        
       <%-- $( "#<%=TextBoxAuthorizedcapital.ClientID%>" ).change(function() {
            alert( "Handler for .change() called." );
            $('#<%=TextBoxAuthorizedcapital.ClientID%>').prop('maxLength', 10);
        });--%>
      
  

        $("#aspnetForm").validate({
            submitHandler: function () {
                test();
            },
          rules: {
                    <%=companyCodeTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        minlength: 2,
                        maxlength: 3
                    }, <%=companyNameTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength: 100
                    },
                     <%=avarageMarketRateTextBox.UniqueID %>: {
                        required: true,
                        number: true
                    }, <%=sectorDropDownList.UniqueID %>: {
                        required: true,
                        CheckSrcorDropDownList: true
                    },
                   <%=groupDropDownList.UniqueID %>: {
                        required: true,
                        groupDropDownList: true
                    }
              
                    ,<%=categoryDropDownList.UniqueID %>: {
                        
                        required:true, 
                        CheckDropDownList:true
                        
                    }
                    ,<%=TextBoxAuthorizedcapital.UniqueID %>: {
                       //new 
                        required: true,
                        number:true,
                        maxlength: 17
                    }
                   ,<%=MarketLotTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength: 5
                    },<%=faceValueTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength:7
                      
                    },<%=dsecodeTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength:20
                      
                    },<%=csecodeTextBox.UniqueID %>: {
                        
                        required: true,
                        maxlength:20
                      
                    }
                     ,<%=premiumTextBox.UniqueID %>: {
                        //new
                         number: true,
                        required: true,
                        maxlength:6
                      
                     }
                     ,<%=merginTextBox.UniqueID %>: {
                        
                        number: true,
                        maxlength:3
                      
                     }
                    ,<%=productTextBox.UniqueID %>: {
                        
                        
                        maxlength:50
                      
                    }
                  ,<%=TextBoxPaidUpCapital.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength:17
                       
                      
                    },<%=totalshareTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength:17
                      
                      
                    }
                    ,<%=listedCategoryDropDownList.UniqueID %>: {
                        
                        required: true,
                        listedCategoryDropDownListCHeck:true
                      
                    } 
                     ,<%=lasttradingdateTextBox.UniqueID %>: {
                        
                         required: true
                      
                     }
                     ,<%=openingdateTextBox.UniqueID %>: {
                        
                        required: true
                      
                    }
                    ,<%=baserateTextBox.UniqueID %>: {
                        
                        required: true,
                        number:true,
                        maxlength:12
                        
                      
                    },<%=ddltxtIsBuySellChargeApplicable.UniqueID %>: {
                        required: true
                    },<%=txtTexAdditionalbuysellcharge.UniqueID %>: {
                        required: true,
                        number:true,
                        maxlength: 4
                    },<%=txtEXCEP_BUYSL_COMPCT_APPLDSE.UniqueID %>: {
                        required: true,
                        number:true
                    },<%=IPODropDownList.UniqueID %>: {
                        required: true,
                        CheckIpoType:true
                    },<%=marketDropDownList.UniqueID %>: {
                        required: true,
                        CheckmarketType:true
                    }
              
                }, messages: {
                     <%=companyCodeTextBox.UniqueID %>:{  
                         required: "*Company Code is required*",
                         maxlength: "* Please enter maximum 3 characters *"
                      }, <%=companyNameTextBox.UniqueID %>: {
                        
                        required: "*Company name is required*",
                        maxlength:"* Please enter maximum 40 characters *"
                    },<%=MarketLotTextBox.UniqueID %>: {
                        
                        required: "*Market Lot is required*",
                        number: "Please Enter an Numeric Value",
                        maxlength:"* Please enter maximum 5 characters *"
                    },<%=faceValueTextBox.UniqueID %>: {
                        
                        required: "*Face value is required*",
                        number: "Please Enter an Numeric Value",
                     
                    }
                    ,<%=premiumTextBox.UniqueID %>: {
                        required: "*Premium is required*"
                    }
                    ,<%=TextBoxAuthorizedcapital.UniqueID %>: {
                        required: "*Authorized capital is required*"
                    }
                    
                    ,<%=dsecodeTextBox.UniqueID %>: {
                        required: "*Dse Code is required*",
                        maxlength:"* Please enter maximum 20 characters *"
                    },<%=csecodeTextBox.UniqueID %>: {
                        required: "*Cse Code is required*",
                        maxlength:"* Please enter maximum 20 characters *"
                    }
                    ,<%=TextBoxPaidUpCapital.UniqueID %>: {
                        required: "*Paid Up Capital is required*",
                        number: "Please Enter an Numeric Value",
                       
                    },<%=totalshareTextBox.UniqueID %>: {
                        required: "*Total Share is required*",
                        number: "Please Enter an Numeric Value"
                      
                    },
                     <%=lasttradingdateTextBox.UniqueID %>:{  
                       required: "*Last Trading Date  is required*",
                       
                   },<%=openingdateTextBox.UniqueID %>:{  
                       required: "*Opening Date  is required*",
                      
                   }
                    ,<%=baserateTextBox.UniqueID %>: {
                        required: "*Base rate is required*",
                        number: "Please Enter an Numeric Value"
                       
                    },<%=ddltxtIsBuySellChargeApplicable.UniqueID %>: {
                        required: "* Is required*"
                       
                    },<%=txtEXCEP_BUYSL_COMPCT_APPLDSE.UniqueID %>: {
                        required: "* Is required*"
                   
                    }
              
                
                }
      });

      
          function test() {   
         

               $.ajax({
              type: "POST",
              url: "CompanyInformation.aspx/InsertandUpdateCompany",
              
          
              data: '{CompanyCode: "' + $("#<%=companyCodeTextBox.ClientID%>").val() + '" ,companyName: "' + $("#<%=companyNameTextBox.ClientID%>").val() + '",dsecode: "' + $("#<%=dsecodeTextBox.ClientID%>").val() + '",PaidUpCapital: "' + $("#<%=TextBoxPaidUpCapital.ClientID%>").val() + '",atho_cap: "' + $("#<%=TextBoxAuthorizedcapital.ClientID%>").val() + '",totalshare: "' + $("#<%=totalshareTextBox.ClientID%>").val() + '" ,faceValue: "' + $("#<%=faceValueTextBox.ClientID%>").val() + '",MarketLot: "' + $("#<%=MarketLotTextBox.ClientID%>").val() + '",sector: "' + $("#<%=sectorDropDownList.ClientID%>").val() + '",product: "' + $("#<%=productTextBox.ClientID%>").val() + '",category: "' + $("#<%=categoryDropDownList.ClientID%>").val() + '",avarageMarketRate: "' + $("#<%=avarageMarketRateTextBox.ClientID%>").val() + '",baserate: "' + $("#<%=baserateTextBox.ClientID%>").val() + '",baseupdateDate: "' + $("#<%=baseupdateDateTextBox.ClientID%>").val() + '",lasttradingdate: "' + $("#<%=lasttradingdateTextBox.ClientID%>").val() + '",sector: "' + $("#<%=sectorDropDownList.ClientID%>").val() + '",category: "' + $("#<%=categoryDropDownList.ClientID%>").val() + '" ,flug: "' + $("#<%=listedCategoryDropDownList.ClientID%>").val() + '",group: "' + $("#<%=groupDropDownList.ClientID%>").val() + '",floatdatefrom: "' + $("#<%=floatdatefromTextBox.ClientID%>").val() + '",floatdateto: "' + $("#<%=floatdatetoTextBox.ClientID%>").val() + '",csecode: "' + $("#<%=csecodeTextBox.ClientID%>").val() + '",address1: "' + $("#<%=addressTextBox1.ClientID%>").val() + '",address2: "' + $("#<%=addressTextBox2.ClientID%>").val() + '",regoffice: "' + $("#<%=regofficeTextBox2.ClientID%>").val() + '",phnNo: "' + $("#<%=phnNoTextBox.ClientID%>").val() + '",openingdate: "' + $("#<%=openingdateTextBox.ClientID%>").val() + '",premium: "' + $("#<%=premiumTextBox.ClientID%>").val() + '",RIssuefrom: "' + $("#<%=RIssuefromTextBox.ClientID%>").val() + '",RIssueto: "' + $("#<%=RIssuetoTextBox.ClientID%>").val() + '",mergin: "' + $("#<%=merginTextBox.ClientID%>").val() + '",IsBuySellChargeApplicable: "' + $("#<%=ddltxtIsBuySellChargeApplicable.ClientID%>").val() + '" ,Additionalbuysellcharge: "' + $("#<%=txtTexAdditionalbuysellcharge.ClientID%>").val() + '",AdditionalbuysellCommision: "' + $("#<%=txtEXCEP_BUYSL_COMPCT_APPLDSE.ClientID%>").val() + '",propectusPublishDate: "' + $("#<%=txtProspectusPublishDate.ClientID%>").val() + '",IpoType: "' + $("#<%=IPODropDownList.ClientID%>").val() + '",marketType: "' + $("#<%=marketDropDownList.ClientID%>").val() + '",ISSUE_MNG: "' + $("#<%=txtissueTextBox.ClientID%>").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        alert('data saved successfully');

                       window.location = 'AllCompInfo.aspx';
                    }
                
                },
                failure: function (response) {
                    alert(response);
                }
             });

        }

       
        
    </script>
</asp:Content>

