<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PortfolioHowlaUpdate.aspx.cs" Inherits="UI_PORTFOLIO_PortfolioHowlaUpdate" Title="IAMCL Portfolio Howla Update  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript"> 
    function fnValidation()
    {
        if(document.getElementById("<%=HowlaDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=HowlaDateTextBox.ClientID%>").focus();
            alert("Please Enter Howla Date");
            return false;
            
        }
        if(document.getElementById("<%=ClearingDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=ClearingDateTextBox.ClientID%>").focus();
            alert("Please Enter Clearing Date");
            return false;
            
        }
         
       
    }
   function fnValidationSave()
    {
        if(document.getElementById("<%=HowlaDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=HowlaDateTextBox.ClientID%>").focus();
            alert("Please Enter Howla Date");
            return false;
            
        }
        if(document.getElementById("<%=ClearingDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=ClearingDateTextBox.ClientID%>").focus();
            alert("Please Enter Clearing Date");
            return false;
            
        }
           //Data Grid Checking
           if(document.getElementById("<%=grdShowDSEMP.ClientID%>"))
           {     
                var grid = document.getElementById("<%= grdShowDSEMP.ClientID %>");
                var cellPivot;

                if (grid.rows.length > 0) 
                {
                   
                        cellPivot = grid.rows[1].cells[2].innerHTML;
                        if(document.getElementById("<%=HowlaDateTextBox.ClientID%>").value !=cellPivot)
                        {
                             alert(" Howla Date and  Trading Date: "+cellPivot+" did not same ");
                             return false;
                      
                        }
                         
                    
                }             
           }
          if(document.getElementById("<%=grdShowCSEMP.ClientID%>"))
           {     
                var grid = document.getElementById("<%= grdShowCSEMP.ClientID %>");
                var cellPivot;

                if (grid.rows.length > 0) 
                {
                   
                        cellPivot = grid.rows[1].cells[2].innerHTML;
                        if(document.getElementById("<%=HowlaDateTextBox.ClientID%>").value !=cellPivot)
                        {
                             alert(" Howla Date and  Trading Date: "+cellPivot+" did not same ");
                             return false;
                      
                        }
                         
                    
                }             
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
       .style12
       {
           height: 13px;
       }
    </style>
 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />  
   

    
        <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      <colgroup width="220"></colgroup>
      <colgroup width="220"></colgroup>
      <colgroup width="150"></colgroup>
        <tr>
            <td align="center" colspan="4" class="style8" >
                Howla Update from&nbsp; File&nbsp; Form 
            </td>
      </tr>
       
      <tr>
         <td colspan="4" align="left" class="style12">&nbsp;&nbsp;</td>   
      </tr>
       <tr>
              <td align="right"><b>&nbsp;Howla Date :</b></td>  
              <td align="left"  >
                  <asp:TextBox ID="HowlaDateTextBox" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="10" Width="125px" 
                      AutoPostBack="True" ontextchanged="HowlaDateTextBox_TextChanged"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="AttendenceDatecalendarButtonExtender" 
                      runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                      PopupButtonID="AttendenceDateImageButton" 
                      TargetControlID="HowlaDateTextBox" />
                  <span class="star">
                  <asp:ImageButton ID="AttendenceDateImageButton" runat="server" 
                      AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                      TabIndex="11" />
                  * </span></td>   
              
                  <td align="right"><b>&nbsp;Clearing Date :</b></td>  
              <td align="left"  >
                  <asp:TextBox ID="ClearingDateTextBox" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="10" Width="125px"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="DSEClearCalendarExtender" 
                      runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                      PopupButtonID="DSEClearDateImageButton" 
                      TargetControlID="ClearingDateTextBox" />
                  <span class="star">
                  <asp:ImageButton ID="DSEClearDateImageButton" runat="server" 
                      AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                      TabIndex="11" />
                  * </span></td>
                  
                                       
              
      </tr>
      <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
 </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
              <td align="left" colspan="3" >
                  <asp:Button ID="showDseDataButton" runat="server" CssClass="buttoncommon" OnClientClick="return fnValidation();"
                      
                      Text="Show DSE Howla" onclick="showDataButton_Click" Width="127px" />
              &nbsp;<asp:Button ID="SaveDSEButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnValidation();"
                AccessKey="S" 
                TabIndex="21" onclick="SaveButton_Click" />
              &nbsp;
                    <asp:Label ID="DSETradeCustLabel" runat="server" 
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
                         <div id="dvGridDSETradeInfo" runat="server"  visible="false"  
                                
                                style="text-align: center; display: block; overflow: auto;height:200px; width: 952px;" 
                                dir="ltr">
                                <asp:GridView ID="grdShowDSEMP" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" Width="920px" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                <asp:BoundField DataField="SI" HeaderText="SI#" />
                                <asp:BoundField DataField="F_CD" HeaderText="Fund Code" />
                                <asp:BoundField DataField="SP_DATE" HeaderText="Trading Date" />
                                 <asp:BoundField DataField="COMP_CD" HeaderText="Company Code" />
                                <asp:BoundField DataField="HOWLA_NO" HeaderText="Howla Number" />
                                <asp:BoundField DataField="IN_OUT" HeaderText="Sale or Buy" />
                                 <asp:BoundField DataField="SP_QTY" HeaderText="No. Of Share" />
                                <asp:BoundField DataField="SP_RATE" HeaderText="Rate" />  
                                <asp:BoundField DataField="HOWLA_CHG" HeaderText="Howla Charge" /> 
                                <asp:BoundField DataField="LAGA_CHG" HeaderText="Laga Charge" />                                                                                              
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
              <td align="right" ></td>  
              <td align="left" colspan="3" >
                  <asp:Button ID="showCseDataButton" runat="server" CssClass="buttoncommon" OnClientClick="return fnValidation();"
                      
                      Text="Show CSE Howla" onclick="showCseDataButton_Click" Width="110px"  />
              &nbsp;<asp:Button ID="SaveCSEButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnValidation();"
                AccessKey="S" 
                TabIndex="21" onclick="SaveCSEButton_Click" />
              &nbsp;
                    <asp:Label ID="CSETradeCustLabel" runat="server" 
                        style="font-size: medium; color: #009933"></asp:Label>
              </td>   
                
      </tr>
          
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
        <tr>
            <td colspan="4" align="left">
            
                <table align="left" style="width: 966px">
                   
                       <tr>
                        <td>
                         <div id="dvGridCSETradeInfo" runat="server"  visible="false"  
                                style="text-align: center; display: block; overflow: auto;height:200px;">
                                <asp:GridView ID="grdShowCSEMP" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" Width="914px" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                <asp:BoundField DataField="SI" HeaderText="SI#" />
                                <asp:BoundField DataField="F_CD" HeaderText="Fund Code" />
                                <asp:BoundField DataField="SP_DATE" HeaderText="Trading Date" />
                                 <asp:BoundField DataField="COMP_CD" HeaderText="Company Code" />
                                <asp:BoundField DataField="HOWLA_NO" HeaderText="Howla Number" />
                                <asp:BoundField DataField="IN_OUT" HeaderText="Sale or Buy" />
                                 <asp:BoundField DataField="SP_QTY" HeaderText="No. Of Share" />
                                <asp:BoundField DataField="SP_RATE" HeaderText="Rate" />  
                                <asp:BoundField DataField="HOWLA_CHG" HeaderText="Howla Charge" /> 
                                <asp:BoundField DataField="LAGA_CHG" HeaderText="Laga Charge" />                                                                                             
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
      <%-- <tr>
              <td align="right"><b>Daily Transaction Update Date :</b></td>  
              <td align="left" colspan="3" >
                  <asp:TextBox ID="dailyTrasactionDateTextBox" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="10" Width="125px"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="avgMPCalendarExtender" 
                      runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                      PopupButtonID="avgMPDateImageButton" 
                      TargetControlID="dailyTrasactionDateTextBox" />
                  <span class="star">
                  <asp:ImageButton ID="avgMPDateImageButton" runat="server" 
                      AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                      TabIndex="11" />
                  * </span></td>   
              
                  
                  
                                       
              
      </tr>--%>
     <%--  <tr>
              <td align="right" >&nbsp;</td>  
              <td align="left" colspan="3" >
                  <asp:Button ID="dailyTransactionButton" runat="server" CssClass="buttoncommon" 
                      
                      Text="Update Daily Transaction" 
                      Width="204px"  />
              &nbsp;</td>   
                
      </tr>--%>
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

