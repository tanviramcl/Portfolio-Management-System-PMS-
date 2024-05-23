using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_CompanyInformation : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
    List<PSDR_FI> psdrfilist = new List<PSDR_FI>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        SaveNoOfSharecertificateNo();

        SaveNoOfShareAllotmentNo();

        clearField();




    }


    public void clearField()
    {
        companyCodeTextBox.Text = "";
        fundcodeTextBox.Text = "";
        atalphaTextBox.Text = "";
        AllotmentNoTextBox.Text ="";
        atalphaTextBox1.Text = "";
        AllotmentNoTextBox2.Text = "";
        CatalphaTextBox.Text = "";
        certificateNoTextBox.Text = "";
        CatalphaTextBox1.Text = "";
        certificateNoTextBox2.Text = "";
        LatterNoTextBox.Text = "";
        SecuritiesTextBox.Text = "";
        NoofTextBox.Text = "";
        LatterDateTextBox.Text = "";
    }




    protected void atalphaTextBox_onchange(object sender, EventArgs e)
    {
        atalphaTextBox1.Text=atalphaTextBox.Text.ToString();

    }
    protected void CatalphaTextBox_onchange(object sender, EventArgs e)
    {
        CatalphaTextBox1.Text = CatalphaTextBox.Text.ToString();

    }
    protected void AllotmentNoTextBox_onchange(object sender, EventArgs e)
    {
        AllotmentNoTextBox2.Text = AllotmentNoTextBox.Text.ToString();

        string allotmentNo1, allotmentNo2;
        allotmentNo1 = AllotmentNoTextBox.Text;
        allotmentNo2 = AllotmentNoTextBox2.Text;
        if (allotmentNo1 != "" && allotmentNo2 != "")
        {
            checkNoOfShareAllotmentNoTextBox();
        }

    }

    protected void certificateNoTextBox_onchange(object sender, EventArgs e)
    {
        certificateNoTextBox2.Text = certificateNoTextBox.Text.ToString();

        string certificateNO1, certificateNO2;
        certificateNO1 = certificateNoTextBox.Text;
        certificateNO2 = certificateNoTextBox2.Text;
        if (certificateNO1 != "" && certificateNO2 != "")
        {
            checkNoOfSharecertificateNo();
        }

    }



    public void checkNoOfShareAllotmentNoTextBox()
    {
        string Query="";
        string NOZero = "";
        int allot_noSize, allot_noSize2;
        int totalshare=0;
        int nofoshare=0;
        int allowtmentNO = 0;


        DataTable dtsource = new DataTable();

        allot_noSize = AllotmentNoTextBox.Text.Length;
        allot_noSize2 = AllotmentNoTextBox2.Text.Length;
        int allot_no = Convert.ToInt32(AllotmentNoTextBox.Text);
        int allot_no2 = Convert.ToInt32(AllotmentNoTextBox2.Text); 

        if (AllotmentNoTextBox.Text.ToString() != "" && AllotmentNoTextBox2.Text.ToString() != "")
        {
            if (allot_noSize != allot_noSize2)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('AllotmentNo are not equal');", true);
            }
            for (int i = allot_no; i <= allot_no2; i++)
            {
                int cert = i;
                
                int allot_nolength = allot_noSize - i.ToString().Length;
                if (allot_nolength == 0)
                {
                    allowtmentNO = Convert.ToInt32(atalphaTextBox.Text);
                    if (allowtmentNO.ToString().Length == 0)
                    {
                        allowtmentNO = cert;
                    }
                }
                else
                {
                    allowtmentNO = Convert.ToInt32(atalphaTextBox.Text);
                   
                    if (allot_nolength == 0)
                    {
                        for (int j = 0; j <= allot_nolength; j++)
                        {

                            NOZero = "0000000";


                        }

                        allowtmentNO = Convert.ToInt32(NOZero);


                    }
                }
            }
            //allowtmentNO;

            Query = "select no_shares from psdr_fi where comp_cd = "+companyCodeTextBox.Text.ToString()+" and f_cd = "+fundcodeTextBox.Text.ToString()+" and allot_no = '"+ allowtmentNO + "' and sh_type<> 'T' and posted = 'A'";
            dtsource = commonGatewayObj.Select(Query.ToString());
            if (dtsource.Rows.Count > 0)
            {
                psdrfilist = (from DataRow dr in dtsource.Rows
                              select new PSDR_FI()
                              {

                                  no_shares = dr["no_shares"].ToString()

                              }).ToList();
                foreach (PSDR_FI psdrfi in psdrfilist)
                {
                    nofoshare = Convert.ToInt32(psdrfi.no_shares);

                }
            }
            else
            {
                nofoshare = 0;
            }
            if (SecuritiesTextBox.Text.ToString() == "")
            {
                totalshare = nofoshare + 0;
            }
            else
            {
                totalshare = nofoshare + Convert.ToInt32(SecuritiesTextBox.Text.ToString());
            }

          
        }


        SecuritiesTextBox.Text = totalshare.ToString();
    }

    public void checkNoOfSharecertificateNo()
    {
        string Query = "";
        int certificateno1size, certificate2size;
        string NOZero = "";
        certificateno1size = certificateNoTextBox.Text.Length;
        certificate2size = certificateNoTextBox2.Text.Length;

        int certificateNo = Convert.ToInt32(certificateNoTextBox.Text);
        int certificate2No = Convert.ToInt32(certificateNoTextBox2.Text);
        int certificateNO = 0;
        int totalshare = 0;
        int nofoshare = 0;
        DataTable dtsource = new DataTable();

        if (certificateNoTextBox.Text.ToString() != "" && certificateNoTextBox2.Text.ToString() != "")
        {
            if (certificateno1size != certificate2size)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Certificate are not equal');", true);
            }
       
        for (int i = certificateNo; i <= certificate2No; i++)
        {
            int cert = i;

            int certificate_nolength = certificateno1size - i.ToString().Length;


            if (certificate_nolength == 0)
            {
                certificateNO = Convert.ToInt32(CatalphaTextBox.Text);
                if (certificateNo.ToString().Length == 0)
                {
                    certificateNo = cert;
                }
            }
            else
            {
                certificateNO = Convert.ToInt32(CatalphaTextBox.Text);
                if (certificate_nolength == 0)
                {
                    for (int j = 0; j <= certificate_nolength; j++)
                    {
                        NOZero = "0000000";
                    }

                    certificateNO = Convert.ToInt32(NOZero);
                }
            }

        }
        Query = "select no_shares from psdr_fi where comp_cd = " + companyCodeTextBox.Text.ToString() + " and f_cd = " + fundcodeTextBox.Text.ToString() + " and cert_no = '" + certificateNO + "' and sh_type<> 'T' and posted = 'A'";
        dtsource = commonGatewayObj.Select(Query.ToString());
        if (dtsource.Rows.Count > 0)
        {
            psdrfilist = (from DataRow dr in dtsource.Rows
                          select new PSDR_FI()
                          {

                              no_shares = dr["no_shares"].ToString()

                          }).ToList();
            foreach (PSDR_FI psdrfi in psdrfilist)
            {
                nofoshare = Convert.ToInt32(psdrfi.no_shares);

            }
        }
        else
        {
            nofoshare = 0;
        }
        if (SecuritiesTextBox.Text.ToString() == "")
        {
            totalshare = nofoshare + 0;
        }
        else
        {
            totalshare = nofoshare + Convert.ToInt32(SecuritiesTextBox.Text.ToString());
        }
        }
        SecuritiesTextBox.Text = totalshare.ToString();

    }

    public void SaveNoOfSharecertificateNo()
    {

        string Query,Query2 = "";
        int certificateno1size, certificate2size;
        string NOZero = "";
        int nofoshare = 0;
        string LoginName = Session["UserName"].ToString();
        certificateno1size = certificateNoTextBox.Text.Length;
        certificate2size = certificateNoTextBox2.Text.Length;

        int certificateNo = Convert.ToInt32(certificateNoTextBox.Text);
        int certificate2No = Convert.ToInt32(certificateNoTextBox2.Text);
        string certificateNO ="";
        string companycode = "";

        DateTime currentdatetime = DateTime.Today;

        DateTime latterdate = DateTime.ParseExact(LatterDateTextBox.Text, "dd/MM/yyyy", null);
        string strlatterdate = Convert.ToDateTime(latterdate).ToString("dd-MMM-yyyy");

        string currentdate = currentdatetime.ToString("dd-MMM-yyyy");

        DataTable dtsource = new DataTable();
        DataTable dtsourcepsdrfi = new DataTable();

        if (certificateNoTextBox.Text.ToString() != "" && certificateNoTextBox2.Text.ToString() != "")
        {
            for (int i = certificateNo; i <= certificate2No; i++)
            {
                int cert = i;

                int certificate_nolength = certificateno1size - i.ToString().Length;


                if (certificate_nolength == 0)
                {
                    certificateNO =CatalphaTextBox.Text.ToString();
                    if (certificateNo.ToString().Length == 0)
                    {
                        certificateNo = cert;
                    }
                }
                else
                {
                    certificateNO = CatalphaTextBox.Text.ToString();
                    if (certificate_nolength == 0)
                    {
                        for (int j = 0; j <= certificate_nolength; j++)
                        {
                            NOZero = "0000000";
                        }

                        certificateNO = NOZero;
                    }
                }
                Query = "select comp_cd  from shr_dmat_fi where comp_cd = "+companyCodeTextBox.Text.ToString()+" and cert_no = '"+ certificateNO + "'";
                dtsource = commonGatewayObj.Select(Query.ToString());
                if (dtsource.Rows.Count > 0)
                {
                    psdrfilist = (from DataRow dr in dtsource.Rows
                                  select new PSDR_FI()
                                  {

                                      comp_cd = dr["comp_cd"].ToString()

                                  }).ToList();
                    foreach (PSDR_FI psdrfi in psdrfilist)
                    {
                        companycode =psdrfi.comp_cd;

                    }
                }
                else
                {
                    companycode = companyCodeTextBox.Text;
                }


                Query2 = "select f_cd,psdr_no,sh_type,om_lot,sp_date,no_shares,folio_no,cert_no,dis_no_fm,dis_no_to,allot_no,howla_no,sp_rate,mv_date, ref_no,bk_cd,op_name from  psdr_fi where comp_cd="+companycode+" and f_cd = "+fundcodeTextBox.Text+" and cert_no = '"+ certificateNO + "' and sh_type<>'T' and posted ='A'";
                dtsourcepsdrfi = commonGatewayObj.Select(Query2.ToString());
                if (dtsourcepsdrfi.Rows.Count > 0)
                {
                    psdrfilist = (from DataRow dr in dtsourcepsdrfi.Rows
                                  select new PSDR_FI()
                                  {
                                      f_cd = dr["f_cd"].ToString(),
                                      psdr_no = dr["psdr_no"].ToString(),
                                      no_shares = dr["no_shares"].ToString(),
                                      dis_no_fm = dr["dis_no_fm"].ToString(),
                                      dis_no_to = dr["dis_no_to"].ToString(),
                                      sh_type = dr["sh_type"].ToString(),
                                      sp_date = dr["sp_date"].ToString(),
                                      sp_rate = dr["sp_rate"].ToString(),
                                      folio_no = dr["folio_no"].ToString(),
                                      om_lot = dr["om_lot"].ToString(),
                                      cert_no = dr["cert_no"].ToString(),
                                      allot_no = dr["allot_no"].ToString(),
                                      howla_no = dr["howla_no"].ToString(),
                                      mv_date = dr["mv_date"].ToString(),
                                      ref_no = dr["ref_no"].ToString(),
                                      bk_cd = dr["bk_cd"].ToString(),
                                      op_name = dr["op_name"].ToString()
                                       }).ToList();
                    foreach (PSDR_FI psdrfi in psdrfilist)
                    {
                        string strInsQuery = "insert into shr_dmat_fi(f_cd, comp_cd, sh_type, sp_date, no_shares, folio_no, cert_no, dis_no_fm, dis_no_to, allot_no, sp_rate, dmat_no, dmat_dt, posted, op_name, c_dt)values('"+psdrfi.f_cd+"','"+companycode+"', '"+psdrfi.sh_type+"', '"+Convert.ToDateTime(psdrfi.sp_date).ToString("dd-MMM-yyyy") +"','"+psdrfi.no_shares+"','"+psdrfi.folio_no+"','"+psdrfi.cert_no+"','"+psdrfi.dis_no_fm +"','"+psdrfi.dis_no_to +"','"+psdrfi.allot_no+"', '"+psdrfi.sp_rate+"',  '"+LatterNoTextBox.Text.ToString()+"', '"+ strlatterdate + "','D','"+ LoginName + "','"+ currentdate + "')";

                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);

                        string strUPQuery = "update psdr_fi set posted='D' where comp_cd = "+companyCodeTextBox.Text.ToString()+" and f_cd ="+fundcodeTextBox.Text.ToString()+"  and cert_no = '"+psdrfi.cert_no+"'";

                        int NumOfRowsupdate = commonGatewayObj.ExecuteNonQuery(strUPQuery);
                        nofoshare = nofoshare + Convert.ToInt32(psdrfi.no_shares);
                    }
                }


            }
        }

   }

    public void SaveNoOfShareAllotmentNo()
    {
        string Query, Query2 = "";
        string NOZero = "";
        int allot_noSize, allot_noSize2;
       
        int nofoshare = 0;
        string  allowtmentNO = "";
        string companycode = "";
        string LoginName = Session["UserName"].ToString();

        DateTime currentdatetime = DateTime.Today;

        DateTime latterdate = DateTime.ParseExact(LatterDateTextBox.Text, "dd/MM/yyyy", null);
        string strlatterdate = Convert.ToDateTime(latterdate).ToString("dd-MMM-yyyy");

        string currentdate = currentdatetime.ToString("dd-MMM-yyyy");

        DataTable dtsource = new DataTable();

        allot_noSize = AllotmentNoTextBox.Text.Length;
        allot_noSize2 = AllotmentNoTextBox2.Text.Length;
        int allot_no = Convert.ToInt32(AllotmentNoTextBox.Text);
        int allot_no2 = Convert.ToInt32(AllotmentNoTextBox2.Text);
        DataTable dtsourcepsdrfi = new DataTable();
        if (AllotmentNoTextBox.Text.ToString() != "" && AllotmentNoTextBox2.Text.ToString() != "")
        {

            for (int i = allot_no; i <= allot_no2; i++)
            {
                int cert = i;

                int allot_nolength = allot_noSize - i.ToString().Length;
                if (allot_nolength == 0)
                {
                    allowtmentNO = atalphaTextBox.Text.ToString();
                    if (allowtmentNO.ToString().Length == 0)
                    {
                        allowtmentNO = cert.ToString();
                    }
                }
                else
                {
                    allowtmentNO = atalphaTextBox.Text.ToString();

                    if (allot_nolength == 0)
                    {
                        for (int j = 0; j <= allot_nolength; j++)
                        {

                            NOZero = "0";


                        }

                        allowtmentNO = NOZero;


                    }
                }
                Query = "select comp_cd  from shr_dmat_fi where comp_cd = " + companyCodeTextBox.Text.ToString() + " and allot_no = '" + allowtmentNO + "'";
                dtsource = commonGatewayObj.Select(Query.ToString());
                if (dtsource.Rows.Count > 0)
                {
                    psdrfilist = (from DataRow dr in dtsource.Rows
                                  select new PSDR_FI()
                                  {

                                      comp_cd = dr["comp_cd"].ToString()

                                  }).ToList();
                    foreach (PSDR_FI psdrfi in psdrfilist)
                    {
                        companycode = psdrfi.comp_cd;

                    }
                }
                else
                {
                    companycode = companyCodeTextBox.Text;
                }
                Query2 = "select f_cd,psdr_no,sh_type,om_lot,sp_date,no_shares,folio_no,cert_no,dis_no_fm,dis_no_to,allot_no,howla_no,sp_rate,mv_date, ref_no,bk_cd,op_name from  psdr_fi where comp_cd=" + companycode + " and f_cd = " + fundcodeTextBox.Text + " and allot_no = '" + allowtmentNO + "' and sh_type<>'T' and posted ='A'";
                dtsourcepsdrfi = commonGatewayObj.Select(Query2.ToString());
                if (dtsourcepsdrfi.Rows.Count > 0)
                {
                    psdrfilist = (from DataRow dr in dtsourcepsdrfi.Rows
                                  select new PSDR_FI()
                                  {
                                      f_cd = dr["f_cd"].ToString(),
                                      psdr_no = dr["psdr_no"].ToString(),
                                      no_shares = dr["no_shares"].ToString(),
                                      dis_no_fm = dr["dis_no_fm"].ToString(),
                                      dis_no_to = dr["dis_no_to"].ToString(),
                                      sh_type = dr["sh_type"].ToString(),
                                      sp_date = dr["sp_date"].ToString(),
                                      sp_rate = dr["sp_rate"].ToString(),
                                      folio_no = dr["folio_no"].ToString(),
                                      om_lot = dr["om_lot"].ToString(),
                                      cert_no = dr["cert_no"].ToString(),
                                      allot_no = dr["allot_no"].ToString(),
                                      howla_no = dr["howla_no"].ToString(),
                                      mv_date = dr["mv_date"].ToString(),
                                      ref_no = dr["ref_no"].ToString(),
                                      bk_cd = dr["bk_cd"].ToString(),
                                      op_name = dr["op_name"].ToString()
                                  }).ToList();
                    foreach (PSDR_FI psdrfi in psdrfilist)
                    {
                        string strInsQuery = "insert into shr_dmat_fi(f_cd, comp_cd, sh_type, sp_date, no_shares, folio_no, cert_no, dis_no_fm, dis_no_to, allot_no, sp_rate, dmat_no, dmat_dt, posted, op_name, c_dt)values('" + psdrfi.f_cd + "','" + companycode + "', '" + psdrfi.sh_type + "', '" + Convert.ToDateTime(psdrfi.sp_date).ToString("dd-MMM-yyyy") + "','" + psdrfi.no_shares + "','" + psdrfi.folio_no + "','" + psdrfi.cert_no + "','" + psdrfi.dis_no_fm + "','" + psdrfi.dis_no_to + "','" + psdrfi.allot_no + "', '" + psdrfi.sp_rate + "',  '" + LatterNoTextBox.Text.ToString() + "', '" + strlatterdate + "','D','" + LoginName + "','" + currentdate + "')";

                        int NumOfRows = commonGatewayObj.ExecuteNonQuery(strInsQuery);

                        string strUPQuery = "update psdr_fi set posted='D' where comp_cd = " + companyCodeTextBox.Text.ToString() + " and f_cd =" + fundcodeTextBox.Text.ToString() + "  and allot_no = '" + psdrfi.allot_no + "'";

                        int NumOfRowsupdate = commonGatewayObj.ExecuteNonQuery(strUPQuery);
                        nofoshare = nofoshare + Convert.ToInt32(psdrfi.no_shares);
                    }
                }


            }
        }
           
    }

    public class Shr_dmat_fi
    {
        public string comp_cd { get; set; }
        public string dmat_no { get; set; }
        public string dmat_dt { get; set; }
        public string f_cd { get; set; }
        public string no_shares { get; set; }
        public string cert_no { get; set; }
        public string allot_no { get; set; }
        public string folio_no { get; set; }
        public string op_name { get; set; }
        public string dis_no_fm { get; set; }
        public string dis_no_to { get; set; }
        public string sh_type { get; set; }
        public string sp_date { get; set; }
        public string sp_rate { get; set; }


    }
    public class PSDR_FI {

        public string f_cd { get; set; }
        public string psdr_no { get; set; }
        public string comp_cd { get; set; }
        public string no_shares { get; set; }
        public string dis_no_fm { get; set; }
        public string dis_no_to { get; set; }
        public string sh_type { get; set; }
        public string sp_date { get; set; }
        public string sp_rate { get; set; }
        public string folio_no { get; set; }
        public string om_lot { get; set; }
        public string cert_no { get; set; }
        public string allot_no { get; set; }
        public string howla_no { get; set; }
        public string mv_date { get; set; }
        public string ref_no { get; set; }
        public string bk_cd { get; set; }
        public string op_name { get; set; }
    }

}





