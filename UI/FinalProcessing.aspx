<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="FinalProcessing.aspx.cs" Inherits="BalanceUpdateProcess" Title="IAMCL Portfolio Market Price Update  (Design and Developed by Sakhawat)" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style6 {
            font-family: "Times New Roman";
            font-weight: bold;
        }

        .style8 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 17px;
            color: #08559D;
            text-decoration: none;
            FONT-WEIGHT: bold;
            text-decoration: underline;
            height: 21px;
        }

        .style11 {
            height: 18px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table width="1100" cellpadding="0" cellspacing="0" border="0">
    </table>
    <table>
        <colgroup width="200"></colgroup>
        <colgroup width="220"></colgroup>
        <colgroup width="150"></colgroup>
        <tr>
            <td align="center" colspan="3" class="style8">Final Processing
            </td>
        </tr>
        <tr>
            <td colspan="3" align="left">&nbsp;&nbsp;</td>
        </tr>

        <tr>
            <td align="right" style="font-weight: 700">Balance Date</td>
            <td align="left" width="300px">
               <asp:TextBox ID="txtbalanceDate1" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="10" Width="125px" 
                      AutoPostBack="True" ></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="balanceDate1calendarButtonExtender" 
                      runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                      PopupButtonID="balanceDate1ImageButton" 
                      TargetControlID="txtbalanceDate1" />
                  <span class="star">
                  <asp:ImageButton ID="balanceDate1ImageButton" runat="server" 
                      AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                      TabIndex="11" />
           </td>
            <td align="left" width="300px">
                <asp:TextBox ID="txtbalanceDate2" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="10" Width="125px" 
                      AutoPostBack="True" ></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="balanceDate2CalendarExtender1" 
                      runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                      PopupButtonID="balanceDate2ImageButton" 
                      TargetControlID="txtbalanceDate2" />
                  <span class="star">
                  <asp:ImageButton ID="balanceDate2ImageButton" runat="server" 
                      AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                      TabIndex="11" />
           </td>
        </tr>
        <tr>

            <td align="right" style="font-weight: 700"><b>Total Row Count:</b></td>
            <td align="left" width="200px">
                <asp:TextBox ID="txttotalRowCount" runat="server" Style="width: 195px;"
                    CssClass="textInputStyle" ReadOnly="true" TabIndex="7" AutoPostBack="True"
                   ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700">&nbsp;</td>
            <td align="left" width="200px">
                <asp:UpdateProgress ID="updProgress"
                    AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                    <ProgressTemplate>
                        <img src="../Image/Processing.gif" alt="processing" style="width: 186px; height: 128px; margin-left: 36px" />

                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700">&nbsp;</td>
            <td align="left" width="600px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblProcessing" runat="server" Text="" Style="font-size: 20px; color: green; width:300px"></asp:Label>
                        <br />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:Button ID="btnProcessingforBackup" runat="server"  CssClass="buttoncommon" Style="width: 195px; height: 30px; font-size: 16px; margin-bottom: 20px;" Text="Processing for Backup" OnClick="btnProcessingforBackup_Click" OnClientClick="fnCloseModal();" />
                        <br />
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <asp:Button ID="btnDelete" runat="server" CssClass="buttoncommon" Style="width: 195px; height: 30px; font-size: 16px;" Text="Delete" OnClick="btnDelete_Click" OnClientClick="fnDeleteCloseModal();" /></td>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"></td>
            <td align="left" width="200px"></td>
            <td></td>
        </tr>
    </table>
      <script type="text/javascript">



      
    $("#aspnetForm").validate({
        rules: {
                   
                    <%=txtbalanceDate1.UniqueID %>: {
                        
                        required: true,
                        
                    },<%=txtbalanceDate2.UniqueID %>: {
                        
                        required: true,  
                    }
              
                }, messages: {
                   <%=txtbalanceDate1.UniqueID %>:{  
                       required: "*Balance Date  is required*",
                       
                   },<%=txtbalanceDate2.UniqueID %>:{  
                       required: "*Balance Date  is required*",
                      
                   }
                    
                    
            
                
                }
      });

    </script>
         <script type="text/javascript">
        function fnCloseModal() {
        
            if (confirm("Do you want to proceed....?")) {
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

