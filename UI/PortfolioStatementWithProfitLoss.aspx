<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="PortfolioStatementWithProfitLoss.aspx.cs" Inherits="UI_PortfolioStatementWithProfitLoss" Title="Portfolio With Profit/Loss Report Form Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script language="javascript" type="text/javascript"> 
    function fnReset()
    {
        var confrm=confirm("Are You Sure To Reset?");
        if(confrm)
        {
            document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value = "0"; 
             document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=pricePCTTextBox.ClientID%>").value ="0";           
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
        if(document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=portfolioAsOnDropDownList.ClientID%>").focus();
            alert("Please Select Date.");
            return false; 
        }
        if(document.getElementById("<%=pricePCTTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=pricePCTTextBox.ClientID%>").focus();
            alert("Please Enter a MP PCT value.");
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
            Portfolio Statement With Profit/Loss Report Form
        </td>           
        <td>
            &nbsp;</td>
    </tr> 
</table>
<br />
<table width="750" align="center" cellpadding="0" cellspacing="0" border="0">
    <tr>
       <td align="left" style="text-align: right">Statement Type:&nbsp; </td>
        <td align="left">               
           <asp:RadioButton ID="profitRadioButton" runat="server" Text="Profit" Checked="true"
                GroupName="statementType" Font-Bold="true" TabIndex="1"  />&nbsp;&nbsp;
                 <asp:RadioButton ID="lossRadioButton" runat="server" Text="Loss" 
                GroupName="statementType" Font-Bold="true" TabIndex="2"  />
                <asp:RadioButton ID="allRadioButton" runat="server" Text="All" 
                GroupName="statementType" Font-Bold="true" TabIndex="2"  />
                                    </td>
    </tr>
    <tr>
       <td align="left" style="text-align: right">Order By(Decending):&nbsp; </td>
        <td align="left">               
           <asp:RadioButton ID="investmentRadioButton" runat="server" Text="Investment"
                GroupName="orderType" Font-Bold="true" TabIndex="1"  />&nbsp;&nbsp;
                 <asp:RadioButton ID="appriciationRadioButton" runat="server" Text="Appreciation" 
                GroupName="orderType" Font-Bold="true" TabIndex="2"  />
                 <asp:RadioButton ID="ErrosionRadioButton" runat="server" Text="Erosion‎" 
                GroupName="orderType" Font-Bold="true" TabIndex="2"  />
                               
                  <asp:RadioButton ID="RateDiffAscRadioButton" runat="server" Text="Rate Diifer. ASC" 
                GroupName="orderType" Font-Bold="true" TabIndex="2"  />         
                 <asp:RadioButton ID="RateDiffDescRadioButton" runat="server" Text="Rate Diifer. DESC" 
                GroupName="orderType" Font-Bold="true" TabIndex="2"  />          
                 </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: 700"><b>Fund Name:&nbsp; </b></td>
        <td align="left" >
            <asp:DropDownList ID="fundNameDropDownList" runat="server" TabIndex="3"></asp:DropDownList>
        </td>
        
    </tr>
    
    <tr>
        <td align="right" style="font-weight: 700"><b>Portfolio As On:&nbsp; </b></td>
        <td align="left">
            <asp:DropDownList ID="portfolioAsOnDropDownList" runat="server" TabIndex="4"></asp:DropDownList>
            </td>
    </tr>
      <tr>
         
              <td  align="right" style="font-weight: 700" ><b>Sector:</b></td>
              <td align="left">
            <asp:DropDownList ID="sectorDropDownList" runat="server" TabIndex="2">
             </asp:DropDownList>
        </td>
        </tr>
     <tr>
        <td align="right" style="font-weight: 700"><b>Increased&nbsp;&nbsp; Market&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;Price (%):&nbsp; </b></td>
        <td align="left">
                      
                <asp:TextBox ID="pricePCTTextBox" runat="server" Width="71px">0</asp:TextBox>
            
            </td>
    </tr>
    
   
    <tr>
           <td align="center" colspan="2" >
                &nbsp;
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

