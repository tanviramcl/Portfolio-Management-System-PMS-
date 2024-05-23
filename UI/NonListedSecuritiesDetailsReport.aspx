<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="NonListedSecuritiesDetailsReport.aspx.cs" Inherits="UI_PortfolioWithNonListedSecurities" Title="Individual Portfolio Statement Report Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type ="text/css" >  
        label.error {             
            color: red;   
            display:inline-flex ;                 
        } 
      
    </style> 
  <script language="javascript" type="text/javascript"> 
      
      function fnCheckInput()
    {   
       
        if(document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").focus();
            alert("Please Select Date.");
            return false; 
        }
    }
  
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
&nbsp;&nbsp;&nbsp;
<table align ="center">
    <tr>
        <td class="FormTitle" align="center">
             Statement of Investment in Non-Listed Securities Details  Report 
        </td>           
        <td>
            &nbsp;</td>
    </tr> 
</table>
<br />
<table align="center" cellpadding="0" cellspacing="0" border="0">

    <tr>
        <td align="right" style="font-weight: 700" ><b>Fund Name:&nbsp; </b></td>
        <td align="left" width="200px">
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
        </td>
        
    </tr>
    <tr>
        <td align="right" style="font-weight: 700" class="style5"><b>Company Name:&nbsp; </b></td>
        <td align="left" class="style5" >
            <asp:DropDownList ID="nonlistedCompanyDropDownList"   AutoPostBack="true" runat="server" TabIndex="1" ></asp:DropDownList>
            </td>
    </tr>

    <tr>
        <td align="right" style="font-weight: 700"><b>Portfolio As On:&nbsp; </b></td>
        <td align="left">
            <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
            </td>
    </tr>

    <tr>
            <td align="center" colspan="2" >
            <asp:Button ID="showButton" runat="server" Text="Show Report" OnClientClick=" return fnCheckInput();"
                CssClass="buttoncommon" TabIndex="5"  onclick="showButton_Click" 
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

