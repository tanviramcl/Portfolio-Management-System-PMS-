<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" Title="IAMCL INVESTMENT ANALYSIS AND PFMS" %>


<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <script language="javascript" type="text/javascript"> 
    function fnValidation()
    {
         if(document.getElementById("<%=loginIDTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=loginIDTextBox.ClientID%>").focus();
            alert("Please Enter LoginID");
            return false;
            
        }
        if(document.getElementById("<%=loginPasswardTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=loginPasswardTextBox.ClientID%>").focus();
            alert("Please Enter Login Password");
            return false;   
        }
        
   <%--   //  if(document.getElementById("<%=txtCaptcha.ClientID%>").value =="")
      //  {
      //    document.getElementById("<%=txtCaptcha.ClientID%>").focus();
      //     alert("Type Captcha Code");
      //      return false;
      //  }  --%>
    }
  </script>
<link rel="Stylesheet" type="text/css" href="CSS/amcl.css"/>
    <style type="text/css">
        .style3
        {
            font-size: small;
            font-family: "Courier New";
            font-weight: 700;
        }
        .style4
        {
            font-family: "Courier New", Courier, monospace;
            font-weight: bold;
        }
        .style5
        {
            font-family: "Courier New";
            font-size: small;
            color:Red;
        }
        .style6
        {
        	font-size: medium;
        	font-family: "Courier New", Courier, monospace;
            font-weight: bold;
            color:Maroon;
        }
        .style7
        {
            font-size: small;
            font-family: "Courier New";
            font-weight: 700;
            width: 129px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" >
    <div>
    <br />
    <br />
    <br />
    <br />
    <table width="400" align="center" cellpadding="0" cellspacing="0">
    <tr>
            <td class="style6">
              IAMCL INVESTMENT ANALYSIS AND PFMS
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table width="400" align="center" cellpadding="0" cellspacing="0">    
        <tr>
            <td class="style3">
             User ID:
            </td>
            <td class="style3">
            <asp:TextBox ID="loginIDTextBox" runat="server" CssClass="textInputStyle" TabIndex="1" ></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="style4">
                <span class="style3">Password</span>:
            </td>
            <td>
            <asp:TextBox ID="loginPasswardTextBox" runat="server" TextMode="Password" CssClass="textInputStyle"  TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        </table>
    <%-- <div align ="center">
            <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaLength="5"

             CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5"

             CaptchaMaxTimeout="240" FontColor = "#529E00" Width="197px" 
                Font-Italic="True" CaptchaChars="ACDEFGHJKLNPQRTUVXYZ2346789" />
    </div>--%>
        <table width="400" align="center" cellpadding="0" cellspacing="0">    
        <%--<tr>
           <td class="style7">
               Type the<br />
               code shown:</td>
             <td class="style3">
            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="textInputStyle" TabIndex="3" 
                     Width="101px" ></asp:TextBox>
            </td>
         
        </tr>--%>

        <br />
        
         <tr>
            <td align="center" colspan="2">
                <asp:Button ID="loginButton" runat="server" Text="Login" 
                    CssClass="buttoncommon" OnClientClick="return fnValidation();" 
                    onclick="loginButton_Click" TabIndex="4" />
            </td>
         </tr>
         <tr>
            <td align="center" colspan="2">
            <asp:Label runat="server" ID="loginErrorLabel" Visible="false" Text="" class="style5"></asp:Label>
            <asp:Label runat="server" ID="lblMessage" class="style5"></asp:Label>
            </td>
          </tr>
        </table>
    </div>
    
    </form>
</body>
</html>
