<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="AssetPercentageCheck.aspx.cs" Inherits="UI_AssetPercentageCheck" Title="Total Asset Percentage Check Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
        <script language="javascript" type="text/javascript"> 
             function fnReset()
                {
                    var confm=confirm("Are Sure To Reset?");
                    if(confm)
                    {         
                        document.getElementById("<%=transactionDateTextBox.ClientID%>").value ="";
                        document.getElementById("<%=percentageTextBox.ClientID%>").value ="";
                        CheckAllDataGridFundName(false);
                        return false;
                    }
                    else
                    {
                        return true; 
                    }
                }
             function fnCheckInput()
             {
                    if(document.getElementById("<%=transactionDateTextBox.ClientID%>").value =="")
                    {
                        document.getElementById("<%=transactionDateTextBox.ClientID%>").focus();
                        alert("Please Enter  Transaction Date");
                        return false;
                    }  
             }
             function fncInputNumericValuesOnly()
	            {
		            if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		            {
			            event.returnValue=false;
		            }
	            }
             
              function CheckAllDataGridFundName(checkVal)
                 {
                        if(document.getElementById("<%=grdShowFund.ClientID%>"))
                        {  
                            
                            var datagrid=document.getElementById("<%=grdShowFund.ClientID%>")
                               
                            var check = 0;                
                            
                            for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                            {
                              var tr = datagrid.rows[rowCount];
                              var td= tr.childNodes[0]; 
                              var item = td.firstChild; 
                              var strType=item.type;
                              if(strType=="checkbox")
                              {
                                    item.checked = checkVal; 
                              }
                            }
                        }
                 }
              
                 function CalculateNoOfCheck(datagrid)
                 {
                    var noOfcheck = 0;                
                            
                        for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                        {
                          var tr = datagrid.rows[rowCount];
                          var td= tr.childNodes[0]; 
                          var item = td.firstChild; 
                          var strType=item.type;
                          if(strType=="checkbox")
                          {
                            if(item.checked)
                            {
                             noOfcheck = noOfcheck + 1; 
                            }
                          }
                        }
                        return noOfcheck;
                 }
                 
//                 function TextBoxValueCheck(GridVw)
//                    {
//                         var TextBoxcell;
//                          
//                             if (GridVw.rows.length > 0)
//                            {
//                                for (i=1; i<GridVw.rows.length; i++)
//                                {
//                                    TextBoxcell = GridVw.rows[i].cells[4];//Change the cell number based on the one you have
//                                    
//                                    for (j=0; j<TextBoxcell.childNodes.length; j++)
//                                    {           
//                                        if (TextBoxcell.childNodes[j].type =="text")
//                                        {
//                                             alert(TextBoxcell.childNodes[0].value);
//                                        }
//                                     }
//                                }
//                            }
//                    }
                 
              
              function fnConfirm()
                 {
                        if(document.getElementById("<%=grdShowFund.ClientID%>"))
                        {
                            var datagridFund = document.getElementById("<%=grdShowFund.ClientID%>")
                            var noOfcheckFund =   CalculateNoOfCheck(datagridFund);
                            //TextBoxValueCheck(datagridFund)
                        }
                        
                        if( noOfcheckFund > 0)
                        {
                            var msg="Are You Sure to Save The Selected Funds Asset Value?";
                            var  conformMsg=confirm(msg);       
                            if(conformMsg)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            alert("Please Check Mark at least One Fund's Asset Value.");
                            return false;
                        }
                }
                
                function showDate(sender,args)
                {
                    if(sender._textbox.get_element().value == "")
                    {
                        var todayDate = new Date();
                        sender._selectedDate = todayDate;
                    }
                }  
    </script>
        <style type="text/css">
            .style4
            {
                width: 119px;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />      
        
    <table width="600" align="center" cellpadding="0" cellspacing="0" >
        <tr>
        <td align="center" class="TableHeader">Asset Percentage Check Report</td>  
        </tr>
    </table>
    
    <br />
    <br />
    <br />
    
        <table width="600" align="center" cellpadding="0" cellspacing="0" >
        <colgroup width="100"></colgroup>
            <tr>
                <td align="left" class="style4">Transaction Date:</td>
                <td align="left">
                    <asp:TextBox ID="transactionDateTextBox" runat="server" 
                        CssClass="textInputStyleDate" TabIndex="3"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="transactionDatecalendarButtonExtender" runat="server" OnClientShowing="showDate"
                        Format="dd-MMM-yyyy" PopupButtonID="transactionDateImageButton" 
                        TargetControlID="transactionDateTextBox" />
                    <asp:ImageButton ID="transactionDateImageButton" runat="server" 
                        AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                        TabIndex="3" />
                    <span class="star">* </span>
                </td>
            </tr>
            <tr>
                <td align="left" class="style4">Percentage Value:</td>
                <td align="left">
                    <asp:TextBox ID="percentageTextBox" runat="server" 
                        CssClass="textInputStyleDate" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            
            <tr>
                <td align="center" class="style4" >
                    &nbsp;
                </td>
                <td align="left">&nbsp;<asp:Button ID="showTotalAssetButton" runat="server" 
                        CssClass="buttoncommon" Text="Show Total Asset" Width="115px" OnClientClick=" return fnCheckInput();" onclick="showTotalAssetButton_Click" 
                        />&nbsp;
                    <asp:Button ID="showReportButton" runat="server" CssClass="buttoncommon" 
                        OnClientClick="return fnConfirm();" Text="Show Report" Width="78px" onclick="showReportButton_Click" 
                        />&nbsp;&nbsp;<asp:Button ID="resetButton" runat="server" CssClass="buttoncommon" 
                        OnClientClick="return fnReset();" Text="Reset" TabIndex="10" />&nbsp;
                    <asp:Button ID="CloseButton" runat="server" CssClass="buttoncommon" 
                        Text="Close" TabIndex="11" />
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
        
        <table align="center" >
          <tr>
                
                <td>
                    <div style="height:310px; overflow:auto;" id="dvGridFund" runat="server" visible="false">
                        <asp:DataGrid id="grdShowFund" runat="server"  style="border: #666666 1px solid;"  AutoGenerateColumns="False" CellPadding="4">                               
                            <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                            <ItemStyle CssClass="TableText"></ItemStyle>
                            <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                            <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                   
                            <Columns>    
                                 <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <input id="chkAllFund" type="checkbox" onclick="CheckAllDataGridFundName(this.checked)"> 
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                         <asp:CheckBox ID="chkFund" runat="server"></asp:CheckBox> 
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                </asp:TemplateColumn> 
                                     
                                <asp:BoundColumn HeaderText="ID" DataField="FUND_CODE" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="SI#" DataField="SI"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Fund Name" DataField="FUND_NAME"></asp:BoundColumn>   
                                
                                <asp:TemplateColumn HeaderText="Asset Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="assetValueTextBox" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem,"ASSET_VALUE") %>' Width="120px"
                                            
                                            meta:resourcekey="assetValueTextBoxResource1"></asp:TextBox>&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>          
                           </asp:DataGrid>
                       </div>
                </td>
            </tr>
    </table> 
    
    <br />
    <br /> 
</asp:Content>

