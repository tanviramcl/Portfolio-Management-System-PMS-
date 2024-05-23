<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="GeneralReport.aspx.cs" Inherits="UI_GeneralReport" Title="Genral Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            font-weight: bold;
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript"> 
 function fnReset()
    {
        var Confrm=confirm("Are Sure To Reset?");
        if(Confrm)
        {
            document.getElementById("<%=fromDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=toDateTextBox.ClientID%>").value =""; 
            document.getElementById("<%=fromYearEndTextBox.ClientID%>").value ="";
            document.getElementById("<%=toYearEndTextBox.ClientID%>").value =""; 
            document.getElementById("<%=companyNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=sectorDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=marketCategoryDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=OrderByDropDownList.ClientID%>").value ="0";
            return false;
        }
        else
        {
            return true;
            
        }
    }
 </script>
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
      GENERAL REPORT FORM
            </td>           
        </tr> 
      </table>
     <br />
     <table width="980" align="center" cellpadding="0" cellspacing="0" border="0">
        <colgroup width = "490"></colgroup>
        <colgroup width = "490"></colgroup>
        
        <tr>
            <td align="left" class="style4" >
                                Closing Price Date</td>
            <td align="left" class="style4" >
                Year End</td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
         <td align="left" >
              <b>From Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b>
              <asp:TextBox ID="fromDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" ></asp:TextBox>
                &nbsp;<asp:ImageButton ID="ImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="fromDateTextBox" 
                 PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
                 &nbsp;
                 <b>To Date:</b> 
                 <asp:TextBox ID="toDateTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" ></asp:TextBox>
                &nbsp;<b><asp:ImageButton ID="ToDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
                <ajaxToolkit:CalendarExtender ID="calendarButtonExtender1" runat="server" TargetControlID="toDateTextBox" 
                 PopupButtonID="ToDateImageButton" Format="dd-MMM-yyyy"/>
            </td>
            <td align="left" style="font-weight: 700" >
                From Year End: <b>
              &nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="fromYearEndTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" ></asp:TextBox>
                 </b>&nbsp; To Year End:
              <b>
              <asp:TextBox ID="toYearEndTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" ></asp:TextBox>
                </b>
            </td>
        </tr>
         <tr>
            <td align="left"  >
                &nbsp;
            </td>
        </tr>
        
        <tr>
         <td align="left" <b><b>CompanyName:</b></b>&nbsp; <b>
             <asp:DropDownList ID="companyNameDropDownList" runat="server" TabIndex="1">
             </asp:DropDownList>
             </b>
             <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server"
                TargetControlID="companyNameDropDownList" 
                QueryPattern="Contains" QueryTimeout="2000">
            </ajaxToolkit:ListSearchExtender>
             &nbsp;&nbsp;&nbsp;
              
            </td>
              <td align="left" >
                  <b>Market Category:&nbsp;
              <asp:DropDownList ID="marketCategoryDropDownList" runat="server">
             </asp:DropDownList>
                  </b>
            </td>
        </tr>

        
         <tr>
            <td align="left" colspan="2" >
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" style="font-weight: 700"  >
                Sector:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;             
                          
             <asp:DropDownList ID="sectorDropDownList" runat="server">
             </asp:DropDownList>
             </td> 
             <td align="left" style="font-weight: 700" >Order By:&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="OrderByDropDownList" runat="server">
                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                <asp:ListItem Text="NAV" Value="NAV"></asp:ListItem>
                <asp:ListItem Text="PE" Value="PE"></asp:ListItem>
                <asp:ListItem Text="Expected PE" Value="EXPECTED_PE"></asp:ListItem>
             </asp:DropDownList>
             </td>
        </tr>
        <tr>
            <td align="left" colspan="2" >
                &nbsp;
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" >
                &nbsp;
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" >
                &nbsp;
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" >
                &nbsp;
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right"  >
            
                &nbsp;&nbsp;&nbsp;<asp:Button ID="ShowDataButton" runat="server" Text="Show Data" 
                CssClass="buttoncommon" />
                &nbsp;<asp:Button ID="ShowButton" runat="server" Text="Print Preview" 
                CssClass="buttoncommon" onclick="ShowButton_Click" />
            
            </td>
            <td align="left">
                &nbsp;<asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" OnClientClick=" return fnReset();" />   
                &nbsp;<asp:Button ID="CloseButton" runat="server" Text="Close" CssClass="buttoncommon"/>  
            </td>
         </tr>
          <tr>
            <td align="left" colspan="2" >
                &nbsp;
            </td>
        </tr>
         
     </table>
     <br />
</asp:Content>

