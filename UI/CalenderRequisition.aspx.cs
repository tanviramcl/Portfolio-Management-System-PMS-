using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UI_RiskVARCaluation : System.Web.UI.Page
{
    CommonGateway commonGatewayObj = new CommonGateway();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList dropDownListObj = new DropDownList();
        if (Session["UserID"] == null)
        {
            Session.RemoveAll();
            Response.Redirect("../Default.aspx");
        }

        DataTable dtCompanyNameDropDownList = dropDownListObj.FillCompanyNameDropDownList();
        DataTable dtHowlaDateDropDownList = dropDownListObj.BalanceDateDropDownList();
        DataTable dtEmpwithdesNameDropDownList = dropDownListObj.UserNameDropDownList();
        if (!IsPostBack)
        {
            companyNameDropDownList.DataSource = dtCompanyNameDropDownList;
            companyNameDropDownList.DataTextField = "COMP_NM";
            companyNameDropDownList.DataValueField = "COMP_CD";
            companyNameDropDownList.DataBind();

            portfolioAsOnDropDownList.DataSource = dtHowlaDateDropDownList;
            portfolioAsOnDropDownList.DataTextField = "Howla_Date";
            portfolioAsOnDropDownList.DataValueField = "BAL_DT_CTRL";
            portfolioAsOnDropDownList.DataBind();

            useNameDropDownList.DataSource = dtEmpwithdesNameDropDownList;
            useNameDropDownList.DataTextField = "Name";
            useNameDropDownList.DataValueField = "ID";
            useNameDropDownList.DataBind();
        }
    }
    protected void ExportButton_Click(object sender, EventArgs e)
    {
        string comp_cd = companyNameDropDownList.SelectedValue.ToString();

        string comp_nm = companyNameDropDownList.SelectedItem.Text.ToString();
        string emp_name = useNameDropDownList.SelectedItem.Text.ToString();
        string bal_date = Convert.ToDateTime(portfolioAsOnDropDownList.SelectedValue.ToString()).ToString("dd-MMM-yyyy");
        string year = Convert.ToDateTime(portfolioAsOnDropDownList.SelectedValue.ToString()).Year.ToString();
        string rF = ddlTest.SelectedItem.Value.ToString();
        ReportDocument rdoc = new ReportDocument();
        ExportFormatType formatType = ExportFormatType.NoFormat;
        DataTable dt = new DataTable();
        StringBuilder sbMst = new StringBuilder();

        sbMst.Append(" select * from (select * from (select * from pfolio_bk where bal_dt_ctrl='"+bal_date+"' and comp_cd="+comp_cd+" ) a join   comp c  on a.comp_cd=c.comp_cd join fund f on a.f_cd=f.f_cd order by f.f_cd) Where  IS_F_CLOSE IS NULL AND BOID IS NOT NULL ");

        dt = commonGatewayObj.Select(sbMst.ToString());


        if (dt.Rows.Count > 0)
        {
            dt.TableName = "crtdtCalenderRequsition";
            // dt.WriteXmlSchema(@"D:\Development\PFMS_LIVE\Main_LIVE_SERVER\amclinvanalysisandpfms\UI\ReportViewer\Report\crtmRequisitionCalender_final.xsd");
            string Path = "";
            Path = Server.MapPath("ReportViewer/Report/crtmRequisitionCalender.rpt");
            rdoc.Load(Path);
            rdoc.SetDataSource(dt);
            rdoc.SetParameterValue("emp_name", emp_name);
            rdoc.SetParameterValue("bal_date", bal_date);
            rdoc.SetParameterValue("year", year);



            switch (rF)
            {
                case "Word":
                    formatType = ExportFormatType.WordForWindows;
                    break;
                case "PDF":
                    formatType = ExportFormatType.PortableDocFormat;
                    break;
                case "Excel":
                    formatType = ExportFormatType.ExcelWorkbook;
                    break;
                case "CSV":
                    formatType = ExportFormatType.CharacterSeparatedValues;
                    break;
            }
            rdoc.ExportToHttpResponse(formatType, Response, true, "RequisitionCalender_" + DateTime.Now);

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
        }




    }

    }
