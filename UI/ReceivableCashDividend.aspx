<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="ReceivableCashDividend.aspx.cs" Inherits="UI_ReceivableCashDividend" Title="Receivable Cash Dividend Report Form" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").value ="0";    
            document.getElementById("<%=recordDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=recordDateToTextBox.ClientID%>").value ="";
            document.getElementById("<%=agmDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=agmDateToTextBox.ClientID%>").value ="";
            
            return false;
        }
        else
        {   
            return true;
        }
            
    }
    
    function fnCheckInput()
    {   
        if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
            alert("Please Select Fund Name.");
            return false; 
        }
        var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
        if(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=recordDateFromTextBox.ClientID%>").focus();
            alert("Please Insert Reord Date From.");
            return false; 
        }
        if(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=recordDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=recordDateToTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=recordDateToTextBox.ClientID%>").focus();
            alert("Please Insert Record Date To.");
            return false; 
        }
        if(document.getElementById("<%=recordDateToTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=recordDateToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=recordDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        
        
        if(document.getElementById("<%=agmDateFromTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=agmDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=agmDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        
        if(document.getElementById("<%=agmDateToTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=agmDateToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=agmDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        
    }
</script>
    <style type="text/css">
        .style4
        {
            width: 297px;
        }
        .style7
        {
            font-weight: normal;
            font-size: xx-small;
        }
        .style8
        {
            font-weight: normal;
            font-size: xx-small;
            border-left-color: #808080;
            border-right-color: #C0C0C0;
            border-top-color: #808080;
            border-bottom-color: #C0C0C0;
        }
        .style9
        {
            width: 145px;
            text-align: right;
        }
        .style10
        {
            width: 145px;
            font-weight: bold;
            text-align: right;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table style="text-align:center">
    <tr>
        <td class="FormTitle" align="center">
            Receivable Cash Dividend Report Form
        </td>           
        <td>
            <br />
        </td>
    </tr> 
</table>
<br />
<table width="750" style="text-align:center" cellpadding="0" cellspacing="0" border="0">
    
    
    
    <tr>
        <td style="font-weight: 700" class="style9"><b style="text-align: left">Fund Name:&nbsp;&nbsp; </b></td>
        <td align="left" class="style4" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="font-weight: 700" class="style9">&nbsp;&nbsp;&nbsp; Record<b> Date From:&nbsp;&nbsp;</b></td>
        <td align="left" class="style4">
            <asp:TextBox ID="recordDateFromTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="recordDateFromTextBox"
                PopupButtonID="recordDateFromImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="recordDateFromImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /><b>&nbsp;To:&nbsp;&nbsp; </b>
            <asp:TextBox ID="recordDateToTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="recordDateToTextBox"
                PopupButtonID="recordDateToImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="recordDateToImageButton" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;<br />
            <span class="style8">(</span><span class="style7">DD-MMM-YYYY)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="style8">&nbsp;&nbsp; (</span>DD-MMM-YYYY)</span></td>
        
    </tr>
    <tr>
        <td style="font-weight: 700" class="style10">AGM Date From:&nbsp;&nbsp;</td>
        <td align="left" class="style4">
            <asp:TextBox ID="agmDateFromTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="agmDateFromTextBox"
                PopupButtonID="agmDateFromImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="agmDateFromImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" />&nbsp;<b>To:&nbsp;&nbsp; </b>
        
            <asp:TextBox ID="agmDateToTextBox" runat="server" style="width:100px;" 
                CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="agmDateToTextBox"
                PopupButtonID="agmDateToImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="agmDateToImageButton" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" />&nbsp;<br />
            <span class="style8">(DD-MMM-YYYY)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (<span class="style7">DD-MMM-YYYY)</span></span></td>
        
    </tr>
    
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
           </td>
    </tr>
     <tr>
            <td align="right">Statement Type:&nbsp; </td>

            <td align="left">
                <asp:RadioButton ID="allRadioButton" runat="server" Text="All" Checked="true"
                    GroupName="statementType" Font-Bold="true" AutoPostBack="true" TabIndex="2"  />
                <asp:RadioButton ID="SectorWiseallRadioButton" runat="server" Text="Sector Wise All"
                    GroupName="statementType" Font-Bold="true" AutoPostBack="true" TabIndex="2"  />
            </td>
        </tr>

    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="showButton" runat="server" Text="Show Report" 
                CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" onclick="showButton_Click"
                    />
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="6" OnClientClick=" return fnReset();"
                />
            </td>
            
    </tr>
    <tr>
           <td align="center" colspan="4" >
                &nbsp;
           </td>
    </tr>
</table>
</asp:Content>
