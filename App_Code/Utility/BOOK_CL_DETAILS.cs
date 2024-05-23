using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOOK_CL_DETAILS
/// </summary>
public class BOOK_CL_DETAILS
{
    public BOOK_CL_DETAILS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string BOOK_CL_DET_ID
    {
        get { return C_BOOK_CL_DET_ID; }
        set { C_BOOK_CL_DET_ID = value; }
    }
    public string F_CD
    {
        get { return C_F_CD; }
        set { C_F_CD = value; }
    }
    public string COMP_CD
    {
        get
        {
            return C_COMP_CD;
        }

        set
        {
            C_COMP_CD = value;
        }
    }
    public string TOT_NOS
    {
        get
        {
            return C_TOT_NOS;
        }

        set
        {
            C_TOT_NOS = value;
        }
    }
    public string ALLOTED_SHARE
    {
        get
        {
            return C_ALLOTED_SHARE;
        }

        set
        {
            C_ALLOTED_SHARE = value;
        }
    }
    public string DIVIDEND_PER_SHR
    {
        get
        {
            return C_DIVIDEND_PER_SHR;
        }

        set
        {
            C_DIVIDEND_PER_SHR = value;
        }
    }
    public string GROSS_DIVIDEND
    {
        get
        {
            return C_GROSS_DIVIDEND;
        }

        set
        {
            C_GROSS_DIVIDEND = value;
        }
    }
    public string TAX
    {
        get
        {
            return C_TAX;
        }

        set
        {
            C_TAX = value;

        }
    }
    public string NET_DIVIDEND
    {
        get
        {
            return C_NET_DIVIDEND;
        }

        set
        {
            C_NET_DIVIDEND = value;
        }
    }

    public string RECEIVE_DATE
    {
        get
        {
            return C_RECEIVE_DATE;
        }

        set
        {
            C_RECEIVE_DATE = value;
        }
    }
    public string BOOK_CL_ID
    {
        get
        {
            return C_BOOK_CL_ID;
        }

        set
        {
            C_BOOK_CL_ID = value;
        }
    }
    public string PDATE
    {
        get
        {
            return C_PDATE;
        }

        set
        {
            C_PDATE = value;
        }
    }
    private string C_BOOK_CL_DET_ID;
    private string C_F_CD;
    private string C_COMP_CD;
    private string C_TOT_NOS;
    private string C_ALLOTED_SHARE;
    private string C_DIVIDEND_PER_SHR;
    private string C_GROSS_DIVIDEND;
    private string C_TAX;
    private string C_NET_DIVIDEND;
    private string C_RECEIVE_DATE;
    private string C_BOOK_CL_ID;
    private string C_PDATE;
    


}