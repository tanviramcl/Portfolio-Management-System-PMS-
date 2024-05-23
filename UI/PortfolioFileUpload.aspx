<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PortfolioFileUpload.aspx.cs" Inherits="UI_PORTFOLIO_PortfolioFileUpload" Title="IAMCL Portfolio File Upload  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script language="javascript" type="text/javascript"> 
    function fnValidation()
    {
         if(document.getElementById("<%=FileUploadDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=FileUploadDateTextBox.ClientID%>").focus();
            alert("Please Enter Transaction Date");
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
       .style9
       {
           text-decoration: underline;
           font-size: medium;
       }
       .style10
       {
           height: 13px;
       }
       .style11
       {
           height: 18px;
       }
       .style12
       {
           height: 22px;
       }
    </style>
 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />  
   

    
        <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      <colgroup width="300"></colgroup>
      <colgroup width="220"></colgroup>
      <colgroup width="150"></colgroup>
        <tr>
            <td align="center" colspan="4" class="style8" >
                Upload File to Server Form 
            </td>
      </tr>
       
      <tr>
      <td colspan="4" align="left" class="style9"><strong>File Upload :</strong></td>   
      </tr>   
         <tr>
              <td align="right"><b>File Upload Date :</b></td>  
              <td align="left" colspan="3" >
                  <asp:TextBox ID="FileUploadDateTextBox" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="10" Width="125px" 
                      ontextchanged="FileUploadDateTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="AttendenceDatecalendarButtonExtender" 
                      runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                      PopupButtonID="AttendenceDateImageButton" 
                      TargetControlID="FileUploadDateTextBox" />
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
              <td align="right" class="style10"><b>Upload DSE Market Price File :</b></td>  
              <td align="left"class="style10" >
                  <asp:FileUpload ID="dseMPFileUpload" runat="server" />
              </td>   
              <td align="left"> 
                  <asp:Button ID="SaveDSEMPButton" runat="server" Text="Save Dse  MP File" OnClientClick="return fnValidation();" 
                      CssClass="buttoncommon" onclick="SaveDSEMPButton_Click" Width="147px"  />
              </td>     
              <td align="left">
                  &nbsp;
                  <asp:Label ID="DSEMPLabel" runat="server" 
                      style="font-size: medium; color: #009933"></asp:Label>
              </td>                                                                                                  
      </tr>
        <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
         <tr>
              <td align="right" class="style10"><b>Upload CSE Market Price File :</b></td>  
              <td align="left"class="style10" >
                  <asp:FileUpload ID="cseMPFileUpload" runat="server" />
              </td>   
              <td align="left"> 
                  <asp:Button ID="SaveCSEMPButton" runat="server" Text="Save CSE MP File" 
                      CssClass="buttoncommon" onclick="SaveCSEMPButton_Click" Height="18px" OnClientClick="return fnValidation();" 
                      Width="145px"  />
              </td>     
              <td align="left">
                  &nbsp;
                  <asp:Label ID="CSEMPLabel" runat="server" 
                      style="font-size: medium; color: #009933"></asp:Label>
              </td>                                                                                                  
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
              <td align="left" colspan="3" >
              &nbsp;</td>   
                
      </tr>
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
    
        <tr>
              <td align="right" class="style10"><b>Upload DSE Trade Cust File :</b></td>  
              <td align="left"class="style10" >
                  <asp:FileUpload ID="dseFileUpload" runat="server" />
              </td>   
              <td align="left"> <asp:Button ID="SaveDSEButton" runat="server" 
                      Text="Save DSE Trade Cust File" CssClass="buttoncommon" OnClientClick="return fnValidation();" 
                      onclick="SaveDSEButton_Click" Width="147px"  />
              </td>
              
                <td align="left">
                    &nbsp;
                    <asp:Label ID="DSETradeCustLabel" runat="server" 
                        style="font-size: medium; color: #009933"></asp:Label>
                                </td>       
                  
                                       
              
      </tr>
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
       <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>

      <tr>
              <td align="right" class="style12"><b>Upload CSE Trade Cust File :</b></td>  
              <td align="left"class="style12" >
                  <asp:FileUpload ID="cseFileUpload" runat="server" />
              </td>   
              <td align="left" class="style12"> 
                  <asp:Button ID="SaveCSEButton" runat="server" Text="Save CSE Trade Cust File" OnClientClick="return fnValidation();" 
                      CssClass="buttoncommon" onclick="SaveCSEButton_Click" Width="147px"  />
              </td>     
              <td align="left" class="style12">
                  &nbsp;
                  <asp:Label ID="CSETradeCustLabel" runat="server" 
                      style="font-size: medium; color: #009933"></asp:Label>
              </td>                                                                                                  
      </tr>
      <tr>
         <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
      
       <tr>
             <td colspan="4" align="left">&nbsp;&nbsp;</td>   
      </tr>
       <tr>
            <td colspan="4" align="left" class="style11">
            
             
            
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

