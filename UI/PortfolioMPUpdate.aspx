<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PortfolioMPUpdate.aspx.cs" Inherits="UI_PORTFOLIO_PortfolioMPUpdate" Title="IAMCL Portfolio Market Price Update  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript"> 
    function fnValidation()
    {
         if(document.getElementById("<%=marketPriceDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=marketPriceDateTextBox.ClientID%>").focus();
            alert("Please Enter Market Date");
            return false;
            
        }
       
    }
    
  
  </script>
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
            padding-right: 5;
            padding-bottom: 3;
            height: 21px;
        }  
       .style11
       {
           height: 18px;
       }
    </style>
 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />  
   

    
        <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      <colgroup width="200"></colgroup>
      <colgroup width="220"></colgroup>
      <colgroup width="150"></colgroup>
        <tr>
            <td align="center" colspan="4" class="style8" >
                Market Price Update from&nbsp; File&nbsp; Form 
            </td>
      </tr>
       
      <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
       <tr>
              <td align="right"><b>Market Price Date :</b></td>  
              <td align="left" colspan="3" >
                  <asp:TextBox ID="marketPriceDateTextBox" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="10" Width="125px" 
                      AutoPostBack="True" ontextchanged="marketPriceDateTextBox_TextChanged"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="AttendenceDatecalendarButtonExtender" 
                      runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                      PopupButtonID="AttendenceDateImageButton" 
                      TargetControlID="marketPriceDateTextBox" />
                  <span class="star">
                  <asp:ImageButton ID="AttendenceDateImageButton" runat="server" 
                      AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                      TabIndex="11" />
                  * </span></td>   
              
                  
                  
                                       
              
      </tr>
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
              <td align="left" colspan="3" >
                  <asp:Button ID="showDseDataButton" runat="server" CssClass="buttoncommon" 
                      
                      Text="Show DSE Price" onclick="showDataButton_Click" Height="21px" OnClientClick="return fnValidation();"
                      Width="110px" />
              &nbsp;<asp:Button ID="SaveDSEButton" runat="server" Text="Save DSE Price " CssClass="buttoncommon" OnClientClick="return fnValidation();"
                AccessKey="S" 
                TabIndex="21" onclick="SaveButton_Click" />
              &nbsp;
                    <asp:Label ID="dsePriceLabel" runat="server" 
                        style="font-size: medium; color: #009933"></asp:Label>
              </td>   
                
      </tr>
          
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
        <tr>
            <td colspan="4" align="left">
            
                <table align="left">
                   
                       <tr>
                        <td>
                         <div id="dvGridDSEMPInfo" runat="server"  visible="false"  
                                style="text-align: center; display: block; overflow: auto;height:200px; width: 656px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:GridView ID="grdShowDSEMP" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" Width="618px" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                <asp:BoundField DataField="ID" HeaderText="SI#" />
                                <asp:BoundField DataField="TRADE_CODE" HeaderText="Trading Code" />
                                 <asp:BoundField DataField="COMP_CD" HeaderText="Company Code" />
                                <asp:BoundField DataField="OPEN" HeaderText="Open Price" />
                                <asp:BoundField DataField="HIGH" HeaderText="High Price" />
                                 <asp:BoundField DataField="LOW" HeaderText="Low Price" />
                                <asp:BoundField DataField="CLOSE" HeaderText="Close Price" />                                                                                               
                                </Columns>
                                </asp:GridView>
                            </div>
                        
                        </td>
                    </tr>
                </table>
            
            </td>   
      </tr>
 <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>    
       <tr>
              <td align="right" >&nbsp;</td>  
              <td align="left" colspan="3" >
                  <asp:Button ID="showCseDataButton" runat="server" CssClass="buttoncommon" OnClientClick="return fnValidation();"
                      
                      Text="Show CSE Price" onclick="showCseDataButton_Click" Width="108px"  />
              &nbsp;<asp:Button ID="SaveCSEButton" runat="server" Text="Save CSE Price" CssClass="buttoncommon" OnClientClick="return fnValidation();"
                AccessKey="S" 
                TabIndex="21" onclick="SaveCSEButton_Click" />
              &nbsp;<asp:Label ID="csePriceLabel" runat="server" 
                        style="font-size: medium; color: #009933"></asp:Label>
              </td>   
                
      </tr>
          
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
        <tr>
            <td colspan="4" align="left">
            
                <table align="left">
                   
                       <tr>
                        <td>
                         <div id="dvGridCSEMPInfo" runat="server"  visible="false"  
                                style="text-align: center; display: block; overflow: auto;height:200px; width: 667px;">
                                &nbsp;&nbsp;&nbsp;
                                <asp:GridView ID="grdShowCSEMP" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" Width="636px" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                <asp:BoundField DataField="ID" HeaderText="SI#" />
                                <asp:BoundField DataField="TRADE_CODE" HeaderText="Trading Code" />
                                 <asp:BoundField DataField="COMP_CD" HeaderText="Company Code" />
                                <asp:BoundField DataField="COMP_NAME" HeaderText="Company Name" />
                               <%-- <asp:BoundField DataField="HIGH" HeaderText="High Price" />
                                 <asp:BoundField DataField="LOW" HeaderText="Low Price" />--%>
                                <asp:BoundField DataField="CLOSE" HeaderText="Close Price" />                                                                                               
                                </Columns>
                                </asp:GridView>
                            </div>
                        
                        </td>
                    </tr>
                </table>
            
            </td>   
      </tr>
       <tr>
            <td colspan="4" align="left" class="style11">
            
             
            
            </td>   
      </tr>
     
       <tr>
              <td align="right" >&nbsp;</td>  
              <td align="left" colspan="3" >
                  <asp:Button ID="avgPriceButton" runat="server" CssClass="buttoncommon" OnClientClick="return fnValidation();"
                      
                      Text="Calculate Average Market Price" 
                      Width="204px" onclick="avgPriceButton_Click"  />
              &nbsp;
                    <asp:Label ID="avegLabel" runat="server" 
                        style="font-size: medium; color: #009933"></asp:Label>
                                </td>   
                
      </tr>
        <tr>
              <td align="right" >&nbsp;</td>  
              <td align="left" >&nbsp;</td>   
              <td align="left" >&nbsp;</td>             
              <td align="left">&nbsp;</td>   
      </tr>
        <tr>
              <td align="right" colspan="4" >&nbsp;</td>  
                
      </tr>
     <tr>   
      
      <td colspan="4">
       <table width="500" align="center" cellpadding="0" cellspacing="0">
     <tr>
        <td align="right">
            &nbsp;</td>
        <td align="left">&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        </td>
       
    </tr>
     
   </table>
   </td>
      
   <tr>
              <td align="right" colspan="4" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="4" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="4" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="4" >&nbsp;</td>  
                                
      </tr>    
</table>
    

   
<br />
<br />
<br />
<br />
</asp:Content>

