using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Message
/// </summary>
public class Message
{
	public Message()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Success()
    {
        return "Save Successfully";
    }
    public string Error()
    {
        return "Save Failed:";
    }
    public string Duplicate()
    {
        return "Save Failed Duplicate";
    }
}
