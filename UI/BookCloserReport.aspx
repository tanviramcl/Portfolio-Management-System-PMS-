<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="BookCloserReport.aspx.cs" Inherits="UI_BookCloserReport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />


    
    <table id="Table1" width = "600" align = "center" cellpadding ="0" cellspacing ="0" runat="server">
    <tr>
    <td align= "center" class="style3">
        <b><u>Book Closer Report Form </u></b>
    </td>
    </tr>
    </table>
    <br />
    <br />
  <table id="Table2" width = "600" align= "center" cellpadding ="0" cellspacing ="0" runat="server">
   <tr>
      <td align="center">
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Company Name &nbsp;
      
        <asp:DropDownList ID="companyNameDropDownList" runat="server" 
               ></asp:DropDownList>
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server"
                TargetControlID="companyNameDropDownList" 
                QueryPattern="Contains" QueryTimeout="2000">
            </ajaxToolkit:ListSearchExtender>
      </td>
    </tr>
  </table>
  <br />
  <table id="Table3" width = "600" align= "center" cellpadding ="0" cellspacing ="0" runat="server">
   <tr>
      <td align="right">
       Entry Date From &nbsp; 
       <asp:TextBox ID="entryDateTextBox" runat="server" Width="80px"></asp:TextBox>
        <asp:ImageButton ID="entryDateTextBoxImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" 
        runat="server" Format="dd-MMM-yyyy" PopupButtonID="entryDateTextBoxImageButton" 
        TargetControlID="entryDateTextBox"></ajaxToolkit:CalendarExtender>
       </td>
       <td align="left" >
        &nbsp;To&nbsp;
        <asp:TextBox ID="toEntryDateTextBox" runat="server" Width="80px"></asp:TextBox>
        <asp:ImageButton ID="toEntryDateTextBoxImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" 
        runat="server" Format="dd-MMM-yyyy" PopupButtonID="toEntryDateTextBoxImageButton" 
        TargetControlID="toEntryDateTextBox"></ajaxToolkit:CalendarExtender>
       </td>
    </tr>
   <tr>
    <td colspan="2"> &nbsp;</td>
    </tr>
           <tr>
            <td align="right">Statement Type:&nbsp; </td>

            <td align="left">
                <asp:RadioButton ID="NewRadioButton" runat="server" Text="New" Checked="true"
                    GroupName="statementType" Font-Bold="true" AutoPostBack="true" TabIndex="2"  />
                <asp:RadioButton ID="oldRadioButton" runat="server" Text="old"
                    GroupName="statementType" Font-Bold="true" TabIndex="1" AutoPostBack="true"  />&nbsp;&nbsp;
              
            </td>
        </tr>
   
   <tr>
    <td  align="right">
    
        <asp:Button ID="viewReportButton" runat="server" 
                Text="View Report" onclick="viewReportButton_Click" />
       
    </td>
     
    </tr>
                 
 </table>  
 <br />            
</asp:Content>

