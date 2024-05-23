<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="DividendStatusReport.aspx.cs" Inherits="UI_ReceivableCashDividend" Title="Receivable Cash Dividend Report Form" %>
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
    { <%--  
        if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
            alert("Please Select Fund Name.");
            return false; 
        }--%>
        var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
       
        if(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value!="")
        {
            if(!checkDate.test(document.getElementById("<%=recordDateFromTextBox.ClientID%>").value))
            {
                document.getElementById("<%=recordDateFromTextBox.ClientID%>").focus();
                alert("Invalid Date Formate! Select Date From The Calender.");
                return false;
            }
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
        <td style="font-weight: 700" class="style9"><b style="text-align: left">Company Name:&nbsp;&nbsp; </b></td>
        <td align="left" class="style4" >
            <asp:DropDownList ID="companyNameDropDownList"  runat="server" AutoPostBack="True"
                        TabIndex="1">
                    </asp:DropDownList>  
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
               <asp:RadioButton ID="allRadioButton" runat="server" Text="All" Checked="true"
                    GroupName="statementType" Font-Bold="true" AutoPostBack="true" TabIndex="2" OnCheckedChanged="radio_CheckedChanged" />
                <asp:RadioButton ID="ReceviedRadioButton" runat="server" Text="Received"
                    GroupName="statementType" Font-Bold="true" TabIndex="1" AutoPostBack="true" OnCheckedChanged="radio_CheckedChanged" />&nbsp;&nbsp;
               <asp:RadioButton ID="pendingRadioButton" runat="server" Text="Pending"
                   GroupName="statementType" Font-Bold="true" AutoPostBack="true" TabIndex="2" OnCheckedChanged="radio_CheckedChanged" />
                
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
<table id="Table3" width="100" align="center" cellpadding="2" cellspacing="2" runat="server">
     <tr>
             
           <td align="left" class="style4" >
                   <asp:dropdownlist runat="server"  Visible="false" class="form-control" AutoPostBack="true" id="ddlTest">
                         <asp:ListItem Text="PDF" Value="PDF" Selected="True" />
                         <asp:ListItem Text="Excel" Value="Excel"  />
                        <asp:ListItem Text="Word" Value="Word"  />
                        <asp:ListItem Text="CSV" Value="CSV" />
                    </asp:dropdownlist>
                <asp:Button ID="ButtonExport" Visible="false" runat="server" Text="Export" 
                CssClass="buttoncommon" TabIndex="5" OnClientClick=" return fnCheckInput();" onclick="ExportButton_Click"
                    />
           </td>
        
         </tr>
        <tr>
            <td colspan="4" align="center">
              
                 <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                   Width="403px" ShowHeader="true"  >
                   <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>
                     <asp:BoundColumn DataField="comp_cd" HeaderText="Company Code" />                     
                     <asp:BoundColumn DataField="f_cd" HeaderText="Fund Code" />
                     <asp:BoundColumn DataField="f_name" HeaderText="Fund Name" />
                     <asp:BoundColumn DataField="comp_cd" HeaderText="Company Code" />
                     <asp:BoundColumn DataField="comp_nm"  HeaderText="Company Name" />
                      <asp:BoundColumn DataField="tot_nos" HeaderText="Total Sahre" />
                      <asp:BoundColumn DataField="TYPE" HeaderText="Declaration Type" />
                      <asp:BoundColumn DataField="FY" HeaderText="FY" />
                      <asp:BoundColumn DataField="APPR_DT" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy}" />
                      <asp:BoundColumn DataField="QUANTITY" HeaderText="Quantity" />
                      <asp:BoundColumn DataField="DIVIDEND_PER_SHR" HeaderText="Dividend per  Share" />
                      <asp:BoundColumn DataField="GROSS_DIVIDEND" HeaderText="Gross Dividend" />
                      <asp:BoundColumn DataField="TAX" HeaderText="TAX" />
                      <asp:BoundColumn DataField="NET_DIVIDEND" HeaderText="Net Dividend" />
                        <asp:BoundColumn DataField="RECEIVABLE" HeaderText="Receivable" />
                        <asp:BoundColumn DataField="FRACTION_DIV_AMT" HeaderText="Fraction" />
                        <asp:BoundColumn DataField="PAYMENT_TYPE" HeaderText="Payment Type" />
                        <asp:BoundColumn DataField="PAYMENT_DATE" HeaderText="Payment Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                    
                </Columns>
            </asp:DataGrid>             
            </td>
        </tr>
    </table>


</asp:Content>
