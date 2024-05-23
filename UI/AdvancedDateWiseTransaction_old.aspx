<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="AdvancedDateWiseTransaction.aspx.cs" Inherits="DateWiseTransaction" Title="IAMCL Portfolio Market Price Update  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        }  
    </style>
 
   <style type="text/css">
        .style6
        {
            font-family: "Times New Roman";
            font-weight: bold;
        }
        .style8
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 17px;
            color: #08559D;
            text-decoration: none;
            FONT-WEIGHT: bold;
            text-decoration: underline;
            /*padding-right: 5;
            padding-bottom: 3;*/
            height: 21px;
        }  
       .style11
       {
           height: 18px;
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
            WIDTH: 246px;
            HEIGHT: 65px;
            font-size: 21px;
            border-radius: 25px;
        }
    </style>
 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
   
      <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />    
    
        <table width="1100" cellpadding="0" cellspacing="0" border="0" >
            </table>
    <table>
              <colgroup width="200"></colgroup>
              <colgroup width="220"></colgroup>
              <colgroup width="150"></colgroup>
        <tr>
            <td align="center" colspan="4" class="style8" >
               Advanced Date Wise Transaction&nbsp; (DSE/CSE)&nbsp;  
            </td>
        </tr>
           <tr>

            <td align="center" colspan="4">
                <div id="dvGridDSETradeInfo" visible="false" runat="server" style="text-align: center; display: block; overflow: auto; height: 200px; width: 952px; "
                    dir="ltr">
                    <asp:GridView ID="grdShowDSE" runat="server" AutoGenerateColumns="False"
                        BackColor="#000000"
                        BorderColor="#33D4FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="2" Width="920px">
                        <FooterStyle BackColor="#33D4FF" ForeColor="#000000" />
                        <PagerStyle ForeColor="#33D4FF" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#33D4FF" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#33D4FF" Font-Bold="true" ForeColor="White" />
                        <RowStyle BackColor="#33D4FF" ForeColor="#0000" />
                        <Columns>
                            <asp:BoundField DataField="F_CD" HeaderText="Fund Code" />
                            <asp:BoundField DataField="F_NAME" HeaderText="Fund Name" />
                            <asp:BoundField DataField="Howla_Date_From" HeaderText="Howla Date From" />
                            <asp:BoundField DataField="Howla_LastUpdated_Date" HeaderText="Howla LastUpdated Date" />
                             <asp:BoundField DataField="Howla_Date_To" HeaderText="Howla Date To" />
                             <asp:BoundField DataField="Stock_Exchange" HeaderText="Stock Exchange" />
                  

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
           
        </tr>
        <tr>
              <td align="center" colspan="4">
                <div id="dvGridCSETradeInfo" visible="false" runat="server" style="text-align: center; display: block; overflow: auto; height: 100px; width: 952px; "
                    dir="ltr">
                    <asp:GridView ID="grdShowCSE" runat="server" AutoGenerateColumns="False"
                        BackColor="#000000"
                        BorderColor="#2874A6" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="2" Width="920px">
                        <FooterStyle BackColor="#2874A6" ForeColor="#000000" />
                        <PagerStyle ForeColor="#2874A6" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#2874A6" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#2874A6" Font-Bold="true" ForeColor="White" />
                        <RowStyle BackColor="#2874A6" ForeColor="#0000" />
                        <Columns>
                            <asp:BoundField DataField="F_CD" HeaderText="Fund Code" />
                            <asp:BoundField DataField="F_NAME" HeaderText="Fund Name" />
                            <asp:BoundField DataField="Howla_Date_From" HeaderText="Howla Date From" />
                            <asp:BoundField DataField="Howla_LastUpdated_Date" HeaderText="Howla LastUpdated Date" />
                             <asp:BoundField DataField="Howla_Date_To" HeaderText="Howla Date To" />
                             <asp:BoundField DataField="Stock_Exchange" HeaderText="Stock Exchange" />
                  

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
         </tr>
        <tr>
                <td align="right" style="font-weight: 700"><b>Voucher Number:</b></td>
            <td align="left" width="200px">
                <asp:TextBox ID="txtVoucherNumber" runat="server" style="width:195px;"
            CssClass="textInputStyle" TabIndex="7" AutoPostBack="false" 
            ></asp:TextBox>
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
                            <br />

                          <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:Button ID="btnProcess" runat="server" Text="Process" 
                               CssClass="processBtn" OnClick="btnProcess_Click"  OnClientClick="fnCloseModal();" 
                             />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>

        </tr>
        </table>   
    
   <script type="text/javascript">
        function fnCloseModal() {
        
            if (confirm("Do you want to Proceed......!!!!")) {
              //  $("#HiddenField1").val("Yes");
                $("#<%=HiddenField1.ClientID%>").val("Yes");
            } else {
               // $("#HiddenField1").val("No");
                $("#<%=HiddenField1.ClientID%>").val("No");
            }
        }
    </script>
                  
 </asp:Content>

