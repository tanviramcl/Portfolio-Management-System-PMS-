<%@ Page Language="C#"  MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="StatementOfQuaterlyReportShowingReazlizedProfiltLoss.aspx.cs" Inherits="UI_BalancechekReport" Title="Statement Of Profit On Sale Of Investment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        } 
        .ui-datepicker {
        position: relative !important;
        top: -250px !important;
        left: 100px !important;
        margin-left: 390px;
        margin-top: -15px;
        } 
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=howlaDateToTextBox.ClientID%>").value ="";
                 
            return false;
        }
        else
        {   
            return true;
        }
            
    }
    
    function fnCheckInput()
    {   
        var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
        if(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=howlaDateFromTextBox.ClientID%>").focus();
            alert("Please Insert Howla Date From.");
            return false; 
        }
        if(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=howlaDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=howlaDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        if(document.getElementById("<%=howlaDateToTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=howlaDateToTextBox.ClientID%>").focus();
            alert("Please Insert Howla Date To.");
            return false; 
        }
        if(document.getElementById("<%=howlaDateToTextBox.ClientID%>").value!="")
        {
            //var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
            if(!checkDate.test(document.getElementById("<%=howlaDateToTextBox.ClientID%>").value))
            {
                document.getElementById("<%=howlaDateToTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
        }
        
    }
</script>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />
&nbsp;&nbsp;&nbsp;
    
    <table style="text-align: center">
        <tr>
            <td class="FormTitle" align="center">Statement Of Quaterly Report Showing Reazlized Profilt Loss</td>
            <td>
                <br />
            </td>
        </tr>

    </table>
      <table style="text-align: center">
                <tr>
            <td align="right" style="font-weight: 700"><b>Howla Date From:</b></td>
            <td align="left">
                <asp:TextBox ID="howlaDateFromTextBox" runat="server" style="width:100px;" 
                    CssClass="textInputStyle" TabIndex="1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="howlaDateFromTextBox"
                    PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="2" /></td>
        
        </tr>
        <tr>
            <td align="right" style="font-weight: 700"><b>Howla Date To:</b></td>
            <td align="left">
                <asp:TextBox ID="howlaDateToTextBox" runat="server" style="width:100px;" 
                    CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="howlaDateToTextBox"
                    PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Click Here" 
                    ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="4" /></td>
        </tr>
        <tr>
          
        <td align="right"> 
            <asp:Label ID="fundNameDropDownListlabel"  style="font-weight: 700"  runat="server" Text="Fund Name:"></asp:Label>
        </td>
        <td align="left" width="200px">
            <asp:DropDownList ID="fundNameDropDownList"  runat="server" TabIndex="6"
                AutoPostBack="True">
            </asp:DropDownList>
        </td>
        </tr>
       
   
    </table>
    <table width="750" style="text-align: center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="showButton" runat="server" Text="Show Report"
                    CssClass="buttoncommon"   OnClientClick=" return fnCheckInput();" TabIndex="5" OnClick="showButton_Click" />
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
 
    
    $("#aspnetForm").validate({
        rules: {
             <%=fundNameDropDownList.UniqueID %>: {
                        
                        required:true ,
                        fundDropDownList:true
                        
                    }
                    
                        
                }
      });

    </script>
</asp:Content>
