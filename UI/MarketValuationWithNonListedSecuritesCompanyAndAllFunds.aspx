<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="MarketValuationWithNonListedSecuritesCompanyAndAllFunds.aspx.cs" Inherits="UI_MarketValuationWithNonListedSecuritesCompanyAndAllFunds" Title="Market Valuation With Non Listed Securites Company And All Funds" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    


          

        <style type="text/css">
            .style4
            {
                font-family: Verdana, Arial, Helvetica, sans-serif;
                font-size: 17px;
                color: #08559D;
                FONT-WEIGHT: bold;
                text-align: center;
                background-image: url('../image/titlebg.gif');
                text-decoration: underline;
            }
            .style5
            {
                width: 123px;
                text-align: right;
            }
            .style6
            {
                color: #FF0066;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />      
        
    <table width="600" align="center" cellpadding="0" cellspacing="0" >
        <tr>
        <td align="center" class="style4">Market Valuation with Profit/Loss</td>  
        </tr>
    </table>
    
    <br />
    <br />
    <br />
    
        <table align="center" >
          <tr>
                
                <td>
                 
                    <div style="height:300px; width:400px; overflow:auto;" id="dvGridFund" runat="server">
                         <!--- Following code renders the checkboxes and a label control on browser --->
 
                    <asp:CheckBox ID="chkAll" Text="Select All" runat="server" />
                    <asp:CheckBoxList ID="chkFunds" runat="server">
                        
                    </asp:CheckBoxList>

                       </div>
                </td>
            </tr>
        </table> 
        <br />
        <br />
        <table width="600" align="center" cellpadding="0" cellspacing="0" >
        <colgroup width="100"></colgroup>
            <tr>
                <td align="right" style="font-weight: 700" class="style5"><b>Portfolio As On:</b></td>
                <td align="left">
                    <asp:DropDownList ID="PortfolioAsOnDropDownList" runat="server" TabIndex="8"></asp:DropDownList>
                    <span class="style6">*</span>
                </td>
            </tr>
           
           
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            
            <tr>
                <td align="center" class="style5">&nbsp;</td>
                <td align="left">
                    <asp:Button ID="showReportButton" runat="server" CssClass="buttoncommon" 
                        OnClientClick="return fnConfirm();" Text="Show Report" Width="78px" onclick="showReportButton_Click" 
                        />&nbsp;&nbsp;<asp:Button ID="resetButton" runat="server" CssClass="buttoncommon" 
                        OnClientClick="return fnReset();" Text="Reset" TabIndex="10" />&nbsp;
                    <asp:Button ID="CloseButton" runat="server" CssClass="buttoncommon" 
                        Text="Close" TabIndex="11" onclick="CloseButton_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
             <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>
       
    <br />
    <br /> 

        <script type="text/javascript">
            $(function () {
                $("[id*=chkAll]").bind("click", function () {
                    if ($(this).is(":checked")) {
                        $("[id*=chkFunds] input").prop("checked", "checked");
                    } else {
                        $("[id*=chkFunds] input").removeAttr("checked");
                    }
                });
                $("[id*=chkFunds] input").bind("click", function () {
                    if ($("[id*=chkFunds] input:checked").length == $("[id*=chkFunds] input").length) {
                        $("[id*=chkAll]").prop("checked", "checked");
                    } else {
                        $("[id*=chkAll]").removeAttr("checked");
                    }
                });
            });
       </script>    
    

  <script type="text/javascript">

    function fnConfirm()
                 {
                       if(document.getElementById("<%=PortfolioAsOnDropDownList.ClientID%>").value =="0")
                            {
                                document.getElementById("<%=PortfolioAsOnDropDownList.ClientID%>").focus();
                                alert("Please Select portfolio as on date.");
                                return false; 
                            }
                            
                          else
                        {
                            
                            return true;
                        }
                }       

    </script> 


</asp:Content>

