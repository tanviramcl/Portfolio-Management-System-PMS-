<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="NonListedSecuritiesInvestmentEntryForm.aspx.cs" Inherits="UI_NonListedSecuritiesInvestmentEntryForm" Title="Non Listed Securites (Investment) Entry Page" EnableEventValidation="false" %>
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
           .processBtn {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
            WIDTH: 146px;
            HEIGHT: 35px;
            font-size: 21px;
            border-radius: 25px;
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />

&nbsp;&nbsp;&nbsp;
<table "text-align"="center">
    <tr>
        <td class="FormTitle" align="center">
            NON LISTED SECURITIES (Investment) Details
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
&nbsp;&nbsp;&nbsp;
<table "text-align"="center">
    <tr>
        <td class="" align="center">
              <asp:Label ID="lblerror" runat="server" Text="" Style="font-size: 20px; color: red;"></asp:Label>
           
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>


                       
<br />
<table width="750" "text-align"="center" cellpadding="0" cellspacing="0" border="0">
    
    <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Fund Name:&nbsp; </b></td>
        <td align="left" class="style5" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="1" OnSelectedIndexChanged="fundNameDropDownList_SelectedIndexChanged"
                AutoPostBack="true"
                 ></asp:DropDownList>
            </td>
    </tr>
     <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Company Name:&nbsp; </b></td>
        <td align="left" class="style5" >
            <asp:DropDownList ID="nonlistedCompanyDropDownList" OnSelectedIndexChanged="nonlistedCompanyDropDownList_SelectedIndexChanged"  AutoPostBack="true" runat="server" TabIndex="1" ></asp:DropDownList>
            </td>
    </tr>
    <tr>

       
       <%-- <td align="right" style="font-weight: 700" class="style5"><b>Nonlisted Category:</b></td>--%>
         <td align="right" style="font-weight: 700" class="style5"><asp:Label ID="Label2" runat="server" Text="Nonlisted Category:"></asp:Label></td>
        <td align="left">
            <asp:DropDownList ID="nonlistedCategoryDropDownList"  readonly="true" runat="server" TabIndex="8"></asp:DropDownList>

        </td>
    </tr>
     <tr>
        <td align="right" style="font-weight: 700" class="style5">Investment Date:</td>
        <td align="left" class="style5" >
            <asp:TextBox ID="InvestMentDateTextBox" runat="server" Width="100px"></asp:TextBox>
            </td>
    </tr>
   
    
    <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Amount(BDT):&nbsp;</b></td>
        <td align="left" class="style5" >
            <asp:TextBox ID="amountTextBox" runat="server"  style="width:100px;" 
                CssClass="textInputStyleammount" TabIndex="2" 
                ></asp:TextBox></td>   
        
    </tr>
     <tr>

        
            
        <td align="right" style="font-weight: 700" class="style5"><b>Rate(BDT):&nbsp;</b></td>
        <td align="left" class="style5" >
           <div> 
                <asp:UpdatePanel ID ="updt1" runat ="server" >
              <ContentTemplate >
            <asp:TextBox ID="rateTextBox" runat="server"  AutoPostBack="true"  style="width:100px" OnTextChanged=" rateChange_TextChanged"
                CssClass="textInputStyle" TabIndex="2" 
                ></asp:TextBox>
                   <asp:TextBox ID="noOfShareTextBox" runat="server"  style="width:100px;" AutoPostBack="true"
                CssClass="textInputStyle" TabIndex="2" 
                ></asp:TextBox>
                   </ContentTemplate >
                  
                  
                  </asp:UpdatePanel>
            </div>
                  </td>   
        
    </tr>
    
    <tr>
           <td align="left" class="style6" ></td>
    </tr>
    <tr>
        <td align="center" colspan="2" class="style5" >&nbsp;</td>
    </tr>
    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="saveButton" runat="server" Text="Save" 
                CssClass="buttoncommon" TabIndex="5" 
                     AccessKey="s" onclick="saveButton_Click"  OnClientClick="return Confirm();"
                    />
          <%-- <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnReset();"
                />--%>
            </td>
            
    </tr>
    
</table>



    <script type="text/javascript">


       
   
       $.validator.addMethod("fundDropDownList", function (value, element, param) {  
           if (value == '0')  
               return false;  
           else  
               return true;  
       },"* Please select a Fund");




       $.validator.addMethod("pfoliodatecheck", function (value, element, param) {  
           if (value == '0')  
               return false;  
           else  
               return true;  
       },"* Please select a Date");
 
       $.validator.addMethod("companycheck", function (value, element, param) {  
           if (value == '0')  
               return false;  
           else  
               return true;  
       },"* Please select a Company");

       $.validator.addMethod("CategoryCheck", function (value, element, param) {  
           if (value == '0')  
               return false;  
           else  
               return true;  
       },"* Please select a Category");

       jQuery.validator.addMethod("Date", function(value, element) { 
           return Date.parseExact(value, "dd/mm/yy");
       });

       $(function () {

           $('#<%=InvestMentDateTextBox.ClientID%>').datepicker({
                  changeMonth: true,
                  changeYear: true,
                  dateFormat: "dd/mm/yy",
                  maxDate: 'today',
                  onSelect: function(selected) {
                   
                  }
              });
      
          });
    
       $("#aspnetForm").validate({
           rules: {
               <%=fundNameDropDownList.UniqueID %>: {
                        
                   //required:true 
                   fundDropDownList:true
                        
               }
               ,
                 <%=InvestMentDateTextBox.UniqueID %>: {
                        
                   //required:true 
                     Date:true
                        
               }
               ,
                <%=nonlistedCompanyDropDownList.UniqueID %>: {
                        
                   //required:true 
                    companycheck:true
                        
                }
               ,
                <%=InvestMentDateTextBox.UniqueID %>: {
                        
                   required:true 
                   
                        
                }
               , <%=amountTextBox.UniqueID %>: {
                        
                   required:true ,
                   number:true              
                        
               }
               ,<%=nonlistedCategoryDropDownList.UniqueID %>: {
                        
                   CategoryCheck:true 
                        
               }, <%=rateTextBox.UniqueID %>: {
                        
                     required:true ,
                     number:true              
                        
                 }, <%=noOfShareTextBox.UniqueID %>: {
                        
                   required:true ,
                   number:true              
                        
               }
              
           }
       });

    </script>
    
               
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<table width="750" "text-align"="center" cellpadding="0" cellspacing="0" border="0">
    
        <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
    <tr>
           <td align="center" colspan="2" >
                <div id="dvGridDSETradeInfo" runat="server" 
                    dir="ltr">
               <asp:GridView ID="GridViewNonListedSecurities" runat="server" AutoGenerateColumns="False"
                   AllowPaging="True" onselectedindexchanged="GridViewNonListedSecurities_SelectedIndexChanged" 
                OnPageIndexChanging="GridViewNonListedSecurities_PageIndexChanging"   OnRowDeleting="OnRowDeleting"
                onrowdatabound ="GridViewNonListedSecurities_RowDataBound" BackColor="White" 
                        BorderColor="#33D4FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                         Width="800px"
                        CellSpacing="2">

                    <Columns>
                            <asp:BoundField DataField="F_CD" HeaderText="Fund Code" />
                            <asp:BoundField DataField="COMP_CD" HeaderText="Company Code" />
                            <asp:BoundField DataField="COMP_NM" HeaderText="Company Name" />
                            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                            <asp:BoundField DataField="RATE" HeaderText="RATE" />
                          <asp:BoundField DataField="NO_SHARES" HeaderText="No Shares" />
                          <asp:BoundField DataField="INV_DATE" HeaderText="Investment Date" />
                        <asp:BoundField Visible="false" DataField="CAT_ID" HeaderText="Category Id" />
                        <asp:BoundField DataField="CAT_NM" HeaderText="Category Name" />
                      <%--  <asp:CommandField ShowDeleteButton="True"  ButtonType="Button" />--%>

                          <asp:templatefield>
                 <itemtemplate>
                     <asp:button id="Button1" runat="server" text="Delete" CssClass="buttoncommon" Style="  width: 95px; height: 30px; font-size: 16px;"  commandname="Delete" onclientclick="fnDeleteCloseModal();" />
                 </itemtemplate>
                              </asp:templatefield>

                    </Columns>

                <FooterStyle BackColor="#2874A6" ForeColor="#000000" />
                        <PagerStyle ForeColor="#2874A6" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#2874A6" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#2874A6" Font-Bold="true" ForeColor="White" />
                        <RowStyle BackColor="#2874A6" ForeColor="#0000" />
               </asp:GridView>
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    </div>

          
                
           </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700" class="style5"><asp:Label ID="LabelINVDate" Visible="false" runat="server" Text="Date:"></asp:Label></td>
        <td align="left" class="style5" >
          <b> <asp:Label Visible="false" ID="lblInvDate" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
        <tr>
         
        <td align="right" style="font-weight: 700" class="style5"><asp:Label ID="lblTotalAmmont" Visible="false" runat="server" Text="Total Ammount:"></asp:Label></td>
        <td align="left" class="style5" >
          <b> <asp:Label Visible="false" ID="lblTotalAmmount" runat="server" Text=""></asp:Label></b>
            </td>
    </tr>
    <tr>
        <td align="right">&nbsp;</td>
        <td align="left">
            <div>

                <asp:UpdateProgress ID="updProgress"
                    AssociatedUpdatePanelID="UpdatePanel1"
                    runat="server">
                    <ProgressTemplate>
                        <img src="../Image/Processing.gif" alt="processing" style="width: 186px; height: 128px; margin-left: 36px" />

                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 24px; color: green;"></asp:Label>
                         <asp:Label ID="Label1" runat="server" Text="" Style="font-size: 20px; color: red;"></asp:Label>
                        <br />

                         <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:Button ID="btnProcess" runat="server" Text="Process" Visible="false" OnClientClick="fnCloseModal();"
                            CssClass="processBtn" OnClick="btnProcess_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </td>

    </tr>
</table>

         <script type="text/javascript">
             function fnCloseModal() {
                
                
                 var amount = $('#<%=lblTotalAmmount.ClientID%>').html();
                 var InvDate= $('#<%=lblInvDate.ClientID%>').html();
                // alert(g);
                 var str="Amount: "+ amount +", as on :"+InvDate+". Do you want to proceed ?";       
              if (confirm(str)) {
              //  $("#HiddenField1").val("Yes");
                $("#<%=HiddenField1.ClientID%>").val("Yes");
            } else {
               // $("#HiddenField1").val("No");
                $("#<%=HiddenField1.ClientID%>").val("No");
            }
        }
        function fnDeleteCloseModal() {
        
          if (confirm("Are you sure you really want to delete.....?")) {
              //  $("#HiddenField1").val("Yes");
                $("#<%=HiddenField2.ClientID%>").val("Yes");
            } else {
               // $("#HiddenField1").val("No");
                $("#<%=HiddenField2.ClientID%>").val("No");
            }
        }
    </script>
               
</asp:Content>


