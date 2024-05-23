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
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// Summary description for ConfigReader
/// </summary>
public class ConfigReader
{
	public ConfigReader()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string SecurityAnalysis
    {
        get
        {

            if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_SecurityAnalysis] != null)
            {
                return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING_SecurityAnalysis];
            }
            return "";
        }
    }


    // Added on 18 dec 2016
    public static string _TRADE_FILE_LOCATION
    {
        get
        {

            if (System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING__TRADE_FILE_LOCATION] != null)
            {
                return System.Configuration.ConfigurationManager.AppSettings[AppConstants.CONN_STRING__TRADE_FILE_LOCATION];
            }
            return "";
        }
    }



}
