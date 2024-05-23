<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="BookCloserEntry.aspx.cs" Inherits="UI_BookCloserEntry" Title="Book Closer Entry Form" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">

 function fnClearFields()
 {
    var Check=confirm("Are You Sure To Clear");
    if(Check)    
    { 
      document.getElementById("<%=companyCodeTextBox.ClientID%>").value ="" ; 
      document.getElementById("<%=companyNameDropDownList.ClientID%>").value="";      
      document.getElementById("<%=financialYearTextBox.ClientID%>").value ="";
      document.getElementById("<%=recordDateTextBox.ClientID%>").value ="" ; 
      document.getElementById("<%=bookToTextBox.ClientID%>").value ="";
      document.getElementById("<%=stockTextBox.ClientID%>").value ="" ; 
      document.getElementById("<%=rightTextBox.ClientID%>").value ="";
      document.getElementById("<%=rightApprovalDateTextBox.ClientID%>").value ="" ; 
      document.getElementById("<%=cashTextBox.ClientID%>").value ="";
      document.getElementById("<%=agmDateTextBox.ClientID%>").value ="" ; 
      document.getElementById("<%=remarksTextBox.ClientID%>").value ="";
      document.getElementById("<%=postedDateTextBox.ClientID%>").value ="" ;    
       document.getElementById("<%=TextBoxReceivable.ClientID%>").value = "";  
       document.getElementById("<%=postedTextBox.ClientID%>").value ="" ;  
      return false;         
    }
   
 }
 function fnCheckSearch()
 {
    if(document.getElementById("<%=companyNameDropDownList.ClientID%>").value=="")
    {
        alert("Please Select a Company Name To Search");
        document.getElementById("<%=companyNameDropDownList.ClientID%>").focus();
        return false;
        
    }
    if(document.getElementById("<%=financialYearTextBox.ClientID%>").value=="")
    {
        alert("Please Enter Financial year To Search");
        document.getElementById("<%=financialYearTextBox.ClientID%>").focus();
        return false;
    }
 }
 function fncInputNumericValuesOnly()
    {
        if(!(event.keyCode==46||event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
        {
            event.returnValue=false;
        }
    }
 function fnCheckInput()
    { 
        var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
        if(document.getElementById("<%=recordDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=recordDateTextBox.ClientID%>").focus();
            alert("Please Insert Reord Date.");
            return false; 
        }
        if(document.getElementById("<%=recordDateTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=recordDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=recordDateTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calendar.");
                return false;
            }
        }        
        if(document.getElementById("<%=bookToTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=bookToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=bookToTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calendar.");
                return false;
            }
        }
        if(document.getElementById("<%=agmDateTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=agmDateTextBox.ClientID%>").focus();
            alert("Please Insert AGM Date. If AGM Date is not available then AGM Date will be Same as Record Date.");
            return false; 
        }        
        if(document.getElementById("<%=agmDateTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=agmDateTextBox.ClientID%>").value))
            {
                document.getElementById("<%=agmDateTextBox.ClientID%>").focus();
                alert("Invalid Date Format! Select Date From The Calendar.");
                return false;
            }
        }
        if((document.getElementById("<%=stockTextBox.ClientID%>").value =="")&&(document.getElementById("<%=cashTextBox.ClientID%>").value ==""))
        {
            document.getElementById("<%=stockTextBox.ClientID%>").focus();
            alert("Please Insert Stock/Cash Dividend Rate.");
            return false;
        }
    
    }

</script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
    <%--<style type="text/css">
        .style3
        {
            color: #000066;
            font-size: x-large;
        }
    </style>--%>
<div id="dvUpdatePanel" runat="server">
    <asp:UpdatePanel ID="BOHolderInfoUpdatePanel" runat="server">
       <ContentTemplate>  
            <asp:Panel ID="Panel1" runat="server" Width = "800">
    <br />
    <table id="Table1" width = "600" align = "center" cellpadding ="0" cellspacing ="0" runat="server">
    <tr>
    <td align= "center" class="style3">
        <b><u>Book Closer Entry Form </u></b>
    </td>
    </tr>
    </table>
    <br />
    <table id="Table2" width = "600" align= "center" cellpadding ="2" cellspacing ="2" runat="server">
    <tr>
        <td align="right">
            <b>Company Code</b>
        </td>
        <td align="left" >
        <asp:TextBox ID="companyCodeTextBox" runat="server" Width="27px" ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td colspan="2"> &nbsp;</td>
    </tr>
     <tr>
        <td align="right">
            <b>Company Name </b>
        </td>
        <td align="left" >
        <asp:DropDownList ID="companyNameDropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="companyNameDropDownList_SelectedIndexChanged" 
                TabIndex="1"></asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td align="right" style="font-weight: 700">
        Financial Year
        </td>
        <td align="left" >
        <asp:TextBox ID="financialYearTextBox" runat="server" Width="81px" TabIndex="2"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server"  
                OnClientClick="return fnCheckSearch();" onclick="searchButton_Click" 
                Text="Search" TabIndex="3" />
        </td>
    </tr>
     <tr>
        <td align="right" style="font-weight: 700">
        Record Date
        </td>
        <td align="left" >
        <asp:TextBox ID="recordDateTextBox" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
        <asp:ImageButton ID="recordDateImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="5" />
        <ajaxToolkit:CalendarExtender ID="recordDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="recordDateImageButton" TargetControlID="recordDateTextBox"></ajaxToolkit:CalendarExtender>
        </td>
    </tr>
     <tr>
        <td align="right">
        Book To
        </td>
        <td align="left" >
        <asp:TextBox ID="bookToTextBox" runat="server" Width="80px" TabIndex="6"></asp:TextBox>
        <asp:ImageButton ID="bookToImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="7" />
        <ajaxToolkit:CalendarExtender ID="bookToCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="bookToImageButton" TargetControlID="bookToTextBox"></ajaxToolkit:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700">
        AGM Date
        </td>
        <td align="left" >
        <asp:TextBox ID="agmDateTextBox" runat="server" Width="80px" TabIndex="8"></asp:TextBox>
        <asp:ImageButton ID="agmDateImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="9"/>
        <ajaxToolkit:CalendarExtender ID="agmDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="agmDateImageButton" TargetControlID="agmDateTextBox"></ajaxToolkit:CalendarExtender>
        </td>
    </tr>
     <tr>
        <td align="right" style="font-weight: 700">
        Stock
        </td>
        <td align="left" >
        <asp:TextBox ID="stockTextBox" runat="server" Width="54px" TabIndex="10" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>%
        </td>
        
    </tr>
    <tr>
        <td align="right" style="font-weight: 700">
        Cash
        </td>
        <td align="left" >
        <asp:TextBox ID="cashTextBox" runat="server" Width="63px" TabIndex="11" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>%
        </td>
    </tr>
     <tr>
        <td align="right">
        Right
        </td>
        <td align="left" >
        <asp:TextBox ID="rightTextBox" runat="server" TabIndex="12"></asp:TextBox>
        </td>
    </tr>
     <tr>
        <td align="right">
        Right Approval Date
        </td>
        <td align="left" >
        <asp:TextBox ID="rightApprovalDateTextBox" runat="server" Width="80px" 
                TabIndex="13"></asp:TextBox>
        <asp:ImageButton ID="rightApprovalDateImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="14"/>
        <ajaxToolkit:CalendarExtender ID="rightApprovalCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="rightApprovalDateImageButton" TargetControlID="rightApprovalDateTextBox"></ajaxToolkit:CalendarExtender>
        </td>
    </tr>
     <tr>
        <td align="right">
        Remarks
        </td>
        <td align="left" >
        <asp:TextBox ID="remarksTextBox" runat="server" TextMode="MultiLine" Width="248px" 
                TabIndex="15"></asp:TextBox>
        </td>
    </tr>
     <tr>
        <td align="right">
            Is Posted?
        </td>
        <td align="left" >
        <asp:TextBox ID="postedTextBox" runat="server" Width="57px" TabIndex="16"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td align="right">
       Is Receive?
    </td>
    <td align="left" >
    <asp:TextBox ID="TextBoxReceivable" runat="server" Width="57px" TabIndex="16"></asp:TextBox>
    </td>
</tr>
     <tr>
        <td align="right">
        Posted Date
        </td>
        <td align="left" >
        <asp:TextBox ID="postedDateTextBox" runat="server" Width="80px" TabIndex="17"></asp:TextBox>
        <asp:ImageButton ID="postedDateImageButton" runat="server" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="18"/>
        <ajaxToolkit:CalendarExtender ID="postedDateCalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="postedDateImageButton" TargetControlID="postedDateTextBox"></ajaxToolkit:CalendarExtender>
        </td>
    </tr>
    <tr>
    <td colspan="2"> &nbsp;</td>
    </tr>
    <tr>
    <td  align="right">
    
        <asp:Button ID="addNewButton" runat="server" Text="Add New" 
            onclick="addNewButton_Click" TabIndex="19" AccessKey="s" /> 
        <asp:Button ID="updateButton" runat="server" Text="Update" 
            onclick="updateButton_Click" Visible="False" TabIndex="19" AccessKey="s" /> 
       
    </td>
     <td align="left">  <asp:Button ID="clearButton" runat="server" Text="Clear" 
            OnClientClick="return fnClearFields();" onclick="clearButton_Click" 
             TabIndex="20" />
    </td>
    </tr>
    </table>
    <br />
    <br />
        
    </asp:Panel>

       </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="searchButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="addNewButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="updateButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="clearButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>    
</asp:Content>

