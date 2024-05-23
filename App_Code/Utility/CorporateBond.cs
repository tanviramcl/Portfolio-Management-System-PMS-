using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CorporateBond
/// </summary>
public class CorporateBond
{
    public CorporateBond()
    {
        //
        // TODO: Add constructor logic here
        //
    }

     public string BOOK_CL_ID
    {
            get{ return C_BOOK_CL_ID; }
            set { C_BOOK_CL_ID = value;}
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
        public string FY
        {
            get
            {
                return C_FY;
            }

            set
            {
               C_FY = value;
            }
        }        
        public string RECORD_DT
        {
            get
            {
                return C_RECORD_DT;
            }

            set
            {
             C_RECORD_DT = value;
            }
        }       
        public string TYPE
    {
            get
            {
                return C_TYPE;
            }

            set
            {
                C_TYPE = value;
            }
        }        
        public string QUANTITY
    {
            get
            {
                return C_QUANTITY;
            }

            set
            {
                C_QUANTITY = value;
            }
        }
       
        public string AGM
    {
            get
            {
                return C_AGM;
            }

            set
            {
            C_AGM = value;
            }
        }

   
    public string APPR_DT
    {
            get
            {
                return C_APPR_DT;
            }

            set
            {
            C_APPR_DT = value;
            }
        }
        public string REMARKS
        {
            get
            {
                return C_REMARKS;
            }

            set
            {
            C_REMARKS = value;
            }
        }
    

        public string ENTRY_DATE
         {
            get
            {
                return C_ENTRY_DATE;
            }

            set
            {
            C_ENTRY_DATE = value;
            }
        }
        public string FY_PART
         {
            get
            {
                return C_FY_PART;
            }

            set
            {
                C_FY_PART = value;
            }
        }



    private string C_BOOK_CL_ID;
        private string C_COMP_CD;
        private string C_FY;
        private string C_RECORD_DT;
        private string C_TYPE;
        private string C_QUANTITY;
        private string C_AGM;
        private string C_APPR_DT;
        private string C_REMARKS;
        private string C_ENTRY_DATE;
        private string C_FY_PART;

}