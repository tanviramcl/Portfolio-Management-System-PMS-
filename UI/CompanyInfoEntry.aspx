<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="CompanyInfoEntry.aspx.cs" Inherits="UI_CompanyInfoEntry" Title="Company Info Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript"> 
 function fnReset()
    {
        var Confrm=confirm("Are Sure To Reset?");
        if(Confrm)
        {
            document.getElementById("<%=companyNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=authorizedCapitalTextBox.ClientID%>").value ="";
            document.getElementById("<%=paidupCapitalTextBox.ClientID%>").value ="";
            document.getElementById("<%=reserveAndSurplusTextBox.ClientID%>").value ="";
            document.getElementById("<%=faceValueTextBox.ClientID%>").value =""; 
            document.getElementById("<%=totalNoOfSecuritiesTextBox.ClientID%>").value ="";
            document.getElementById("<%=marketLotTextBox.ClientID%>").value =""; 
            document.getElementById("<%=lastYearEndTextBox.ClientID%>").value =""; 
            document.getElementById("<%=firstQuarterEpsTextBox.ClientID%>").value ="";
            document.getElementById("<%=secondQuarterTextBox.ClientID%>").value ="";
            document.getElementById("<%=thirdQuarterTextBox.ClientID%>").value ="";
            document.getElementById("<%=fourthQuarterTextBox.ClientID%>").value ="";
            document.getElementById("<%=navTextBox.ClientID%>").value =""; 
            document.getElementById("<%=stockDividendTextBox.ClientID%>").value ="";
            document.getElementById("<%=cashDividendTextBox.ClientID%>").value =""; 
            document.getElementById("<%=recordDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=recordDateToTextBox.ClientID%>").value ="";
            document.getElementById("<%=agmDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=rightShareTextBox.ClientID%>").value ="";
            document.getElementById("<%=rightApprovalDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=rightRecordDateFromTextBox.ClientID%>").value =""; 
            document.getElementById("<%=rightRecordDateToTextBox.ClientID%>").value ="";
            document.getElementById("<%=cashEpsTextBox.ClientID%>").value =""; 
            document.getElementById("<%=interimStockDividendTextBox.ClientID%>").value =""; 
            document.getElementById("<%=interimCashDividendTextBox.ClientID%>").value ="";
            document.getElementById("<%=interimRecordDateFromTextBox.ClientID%>").value ="";
            document.getElementById("<%=interimRecordDateToTextBox.ClientID%>").value ="";
            document.getElementById("<%=shareOfDirectorTextBox.ClientID%>").value ="";
            document.getElementById("<%=shareOfPublicTextBox.ClientID%>").value =""; 
            document.getElementById("<%=shareOfGovtTextBox.ClientID%>").value ="";
            document.getElementById("<%=shareOfForeignTextBox.ClientID%>").value =""; 
            document.getElementById("<%=shareOfInstitutionTextBox.ClientID%>").value =""; 
            document.getElementById("<%=percentageGrothEpsTextBox.ClientID%>").value ="";
            document.getElementById("<%=percentageGrothCashEpsTextBox.ClientID%>").value ="";
           
            document.getElementById("<%=marketCategoryDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=electronicShareDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=sectorDropDownList.ClientID%>").value ="0"; 
            document.getElementById("<%=netProfitTextBox.ClientID%>").value =""; 
            document.getElementById("<%=totalEquityTextBox.ClientID%>").value =""; 
            document.getElementById("<%=longTermDebtTextBox.ClientID%>").value =""; 
            return false;
        }
        else
        {   
            return true;
        }
            
    }
function fnCheckInput()
{   
    if(document.getElementById("<%=companyNameDropDownList.ClientID%>").value =="0")
        {
            document.getElementById("<%=companyNameDropDownList.ClientID%>").focus();
            alert("Please Select Company Name.");
            return false;
            
        }
}

 </script>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
        &nbsp;&nbsp;&nbsp;
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           COMPANY INFORMATION ENTRY FORM
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
<table width="1040" align="center" cellpadding="0" cellspacing="0" border="0">
<colgroup width = "150"></colgroup>
<colgroup width = "300"></colgroup>
<colgroup width = "145"></colgroup>
<colgroup width = "140"></colgroup>
<colgroup width = "180"></colgroup>


    <tr>
        <td align="left" style="font-weight: 700"><b>CompanyName:</b></td>
        <td align="left" >
            <asp:DropDownList ID="companyNameDropDownList" runat="server" TabIndex="1">
             </asp:DropDownList>
            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server"
                TargetControlID="companyNameDropDownList" 
                QueryPattern="Contains" QueryTimeout="2000">
            </ajaxToolkit:ListSearchExtender>
        </td>
        <td align="left" style="font-weight: 700"><b>Fourth Quarter EPS:</b></td>
        <td align="left" >
            <asp:TextBox ID="fourthQuarterTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="15"></asp:TextBox>
                                </td>
        <td align="left" style="font-weight: 700">Interim Record Date From:</td>
        <td align="left">
            <asp:TextBox ID="interimRecordDateFromTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="35"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="interimRecordDateFromTextBox" 
                 PopupButtonID="ImageButton6" Format="dd-MMM-yyyy"/>
        <asp:ImageButton ID="ImageButton6" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="36" />
                 </td>
    </tr>
    <tr>
        <td align="left" style="font-weight: 700"><b>Sector:</b></td>
        <td align="left" >
            <asp:DropDownList ID="sectorDropDownList" runat="server" TabIndex="2">
             </asp:DropDownList>
        </td>
        <td align="left" style="font-weight: 700">NAV<b>:</b></td>
        <td align="left" >
            <asp:TextBox ID="navTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="16"></asp:TextBox>
        </td>
        <td align="left" style="font-weight: 700"><b>Interim Record Date To:</b></td>
        <td align="left" >
            <asp:TextBox ID="interimRecordDateToTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="37"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="interimRecordDateToTextBox" 
                 PopupButtonID="ImageButton7" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton7" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="38" />
        </td>
        
    </tr>
    <tr>
        <td align="left" style="font-weight: 700" >Last Year End:</td>
        <td align="left" >
            <asp:TextBox ID="lastYearEndTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="3"></asp:TextBox>
        <span class="star">*</span></td>
        
        <td align="left" style="font-weight: 700">Stock Dividend:</td>
        <td align="left" >
            <asp:TextBox ID="stockDividendTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="17"></asp:TextBox>
        </td>
        <td align="left" style="font-weight: 700" >Share of Director:</td>
        <td align="left" >
            <asp:TextBox ID="shareOfDirectorTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="39"></asp:TextBox>
                 </td>
        
        
    </tr>
    <tr>
        <td align="left" style="font-weight: 700" >Authorized Capital:</td>
        <td align="left" >
            <asp:TextBox ID="authorizedCapitalTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="4"></asp:TextBox>
        <span class="star">*</span></td>     
        <td align="left" style="font-weight: 700" ><b>Cash Dividend:</b></td>
        <td align="left" >
            <asp:TextBox ID="cashDividendTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="18"></asp:TextBox>
        </td>
        <td align="left" style="font-weight: 700">Share of Public:</td>
        <td align="left">
            <asp:TextBox ID="shareOfPublicTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="40"></asp:TextBox>
        </td>
        
    </tr>
    <tr>
        <td align="left" style="font-weight: 700" >Paid-up Capital:</td>
        <td align="left" >
            <asp:TextBox ID="paidupCapitalTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="5"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left" style="font-weight: 700">Record Date From:</td>
        <td align="left" >
            <asp:TextBox ID="recordDateFromTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="19"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="recordDateFromTextBox_CalendarExtender" 
                runat="server" TargetControlID="recordDateFromTextBox" 
                 PopupButtonID="ImageButton" Format="dd-MMM-yyyy"/>
        <asp:ImageButton ID="ImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="20" />
                 </td>
        <td align="left" style="font-weight: 700" >Share of Government:</td>
        <td align="left" >
            <asp:TextBox ID="shareOfGovtTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="41"></asp:TextBox>
        </td>
        
    </tr>
    <tr>
        <td align="left" style="font-weight: 700">Reserve and Surplus:</td>
        <td align="left" >
            <asp:TextBox ID="reserveAndSurplusTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="6"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left" style="font-weight: 700" ><b>Record Date To:</b></td>
        <td align="left" >
            <asp:TextBox ID="recordDateToTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="21"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="recordDateToTextBox_CalendarExtender" 
                runat="server" TargetControlID="recordDateToTextBox" 
                 PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton1" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="22" />
                 </td>
        <td align="left" style="font-weight: 700" >Share of Foreign:</td>
        <td align="left" >
            <asp:TextBox ID="shareOfForeignTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="42"></asp:TextBox>
        </td>
        
        
    </tr>
    <tr>
        <td align="left" style="font-weight: 700" >Face Value:</td>
        <td align="left" >
            <asp:TextBox ID="faceValueTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="7"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left"><b>AGM Date:</b></td>
        <td align="left" >
            <asp:TextBox ID="agmDateTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="23"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="agmDateTextBox_CalendarExtender" 
                runat="server" TargetControlID="agmDateTextBox" 
                 PopupButtonID="ImageButton2" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton2" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="24" />
        </td>
        <td align="left" style="font-weight: 700" >Share of Institution:</td>
        <td align="left">
            <asp:TextBox ID="shareOfInstitutionTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="43"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left" style="font-weight: 700">Total no. of Securities:</td>
        <td align="left" >
            <asp:TextBox ID="totalNoOfSecuritiesTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="8"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left"><b>Right Share:</b></td>
        <td align="left" >
            <asp:TextBox ID="rightShareTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="25"></asp:TextBox>
        </td>
        <td align="left" style="font-weight: 700" >Percentage Growth EPS:</td>
        <td align="left" >
            <asp:TextBox ID="percentageGrothEpsTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="44"></asp:TextBox>
                                </td>
        
        
    </tr>
    <tr>
        <td align="left" style="font-weight: 700" >Market Lot:</td>
        <td align="left" >
            <asp:TextBox ID="marketLotTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="9"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left" style="font-weight: 700">Right Approval Date:</td>
        <td align="left" >
            <asp:TextBox ID="rightApprovalDateTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="26"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="rightApprovalDateTextBox" 
                 PopupButtonID="ImageButton3" Format="dd-MMM-yyyy"/>
        <asp:ImageButton ID="ImageButton3" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="27" />
                 </td>
        <td align="left" style="font-weight: 700" >Percentage Growth Cash EPS:</td>
        <td align="left" >
            <asp:TextBox ID="percentageGrothCashEpsTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="45"></asp:TextBox>
                                </td>
        
    </tr>
    <tr>
        <td align="left"><b>Market Category:</b></td>
        <td align="left" ><asp:DropDownList ID="marketCategoryDropDownList" 
                runat="server" TabIndex="10">
                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                <asp:ListItem Text="A" Value="A"></asp:ListItem>
                <asp:ListItem Text="B" Value="A"></asp:ListItem>
                <asp:ListItem Text="G" Value="G"></asp:ListItem>
                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
             </asp:DropDownList>
        </td>
        <td align="left" style="font-weight: 700" ><b>Right Record Date From:</b></td>
        <td align="left" >
            <asp:TextBox ID="rightRecordDateFromTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="28"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="rightRecordDateFromTextBox" 
                 PopupButtonID="ImageButton4" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton4" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="29" />
                 </td>
        <td align="left" style="font-weight: 700" >Net Profit:</td>
        <td align="left">
            <asp:TextBox ID="netProfitTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="46"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left"><b>Electronic Share:</b></td>
        <td align="left" >
            <asp:DropDownList ID="electronicShareDropDownList" runat="server" 
                TabIndex="11">
                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                <asp:ListItem Text="N" Value="N"></asp:ListItem>
             </asp:DropDownList>
        </td>
        <td align="left"><b>Right Record Date To:</b></td>
        <td align="left" >
            <asp:TextBox ID="rightRecordDateToTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="30"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="rightRecordDateToTextBox" 
                 PopupButtonID="ImageButton5" Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="ImageButton5" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="31" />
        </td>
        <td align="left" style="font-weight: 700" >Total Equity:</td>
        <td align="left" >
            <asp:TextBox ID="totalEquityTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="47"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left" style="font-weight: 700"><b>First Quarter EPS:</b></td>
        <td align="left" >
            <asp:TextBox ID="firstQuarterEpsTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="12"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left"><b>Cash EPS:</b></td>
        <td align="left" >
            <asp:TextBox ID="cashEpsTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="32"></asp:TextBox>
        </td>
        <td align="left" style="font-weight: 700" >Long Term Debt:</td>
        <td align="left" >
            <asp:TextBox ID="longTermDebtTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="47"></asp:TextBox>
                                </td>
        
    </tr>
    <tr>
        <td align="left" style="font-weight: 700"><b>Second Quarter EPS:</b></td>
        <td align="left" >
            <asp:TextBox ID="secondQuarterTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="13"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left"><b>Interim Stock Dividend:</b></td>
        <td align="left" >
            <asp:TextBox ID="interimStockDividendTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="33"></asp:TextBox>
        </td>
        
       <td align="left" style="font-weight: 700" >&nbsp;</td>
        <td align="left" >
            &nbsp;</td> 
    </tr>
    <tr>
        <td align="left" style="font-weight: 700"><b>Third Quarter EPS:</b></td>
        <td align="left" >
            <asp:TextBox ID="thirdQuarterTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="14"></asp:TextBox>
        <span class="star">*</span></td>
        <td align="left"><b>Interim Cash Dividend:</b></td>
        <td align="left" >
            <asp:TextBox ID="interimCashDividendTextBox" runat="server" 
                style="width:100px;" CssClass="textInputStyle" TabIndex="34"></asp:TextBox>
        </td>
        
       <td align="left" style="font-weight: 700" >&nbsp;</td>
        <td align="left" >
            &nbsp;</td> 
    </tr>
    <tr>
           <td align="center" colspan="6" >
                &nbsp;
           </td>
    </tr>
    <tr>
            <td align="center" colspan="6" >
            <asp:Button ID="saveButton" runat="server" Text="Save" 
                CssClass="buttoncommon" TabIndex="48" OnClientClick=" return fnCheckInput();" 
                    onclick="saveButton_Click"/>
            <asp:Button ID="resetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon" TabIndex="49" OnClientClick=" return fnReset();" onclick="resetButton_Click"
                />
            </td>
            
    </tr>
    <tr>
           <td align="center" colspan="6" >
                &nbsp;
           </td>
    </tr>
          
 </table>
      <br />
</asp:Content>

