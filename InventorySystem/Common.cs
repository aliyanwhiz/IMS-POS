using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO.Compression;
using System.Web.UI.WebControls;

using System.Web.UI;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{

    public Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //public static string Serialize(object obj)
    //{
    //    string result = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    //    return result;
    //}

    //public static object Deserialize(string json, Type type)
    //{
    //    return JsonConvert.DeserializeObject(json, type);
    //}
    public static string GetHash(string value)
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5");
    }
    public static DataTable GetDllValue(string Cmd)
    {
        SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminManagementSystem"].ToString());
        DataTable t1 = new DataTable();

        SqlCommand myCommand = new SqlCommand(Cmd, myConn);
        string ErrorMsg = "";

        try
        {
            myCommand.Connection.Open();
            SqlDataReader dr = myCommand.ExecuteReader();

            // Check if SqlDataReader has rows before reading
            if (dr.HasRows)
            {
                t1.Load(dr);
            }

            dr.Close(); // Close the SqlDataReader after use
        }
        catch (Exception ex)
        {
            // Handle any exceptions here
            ErrorMsg = ex.Message.ToString();
        }
        finally
        {
            // Close the connection in the finally block to ensure it's always closed
            if (myCommand.Connection.State == ConnectionState.Open)
            {
                myCommand.Connection.Close();
            }
        }

        return t1;
    }


    public static int Insert(String Cmd)
    {
        SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminManagementSystem"].ToString());

        SqlCommand myCommand;
        myCommand = new SqlCommand(Cmd, myConn);
        try
        {
            myCommand.Connection.Open();
            myCommand.ExecuteNonQuery();
            return 1;
        }
        catch (Exception ex)
        {
            myCommand.Connection.Close();
            // throw new System.Exception(ex.Message);
            return 0;
        }
        myCommand.Connection.Close();
        return 1;
    }

    public static void BindDropDown(DropDownList ddlGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    {
        ddlGeneral.Items.Clear();
        ddlGeneral.DataSource = dataSource;
        ddlGeneral.DataTextField = dataTextField;
        ddlGeneral.DataValueField = dataValueField;
        ddlGeneral.DataBind();

        if (hasSelectItem == true)
        {
            ddlGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        if (hasOtherItem == true)
        {
            ddlGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
        }
    }

    //public static void BindMultiselectdropdown( My.UsrCtrl_multiSelectDropDwnLst ddlGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    //{


    //    ddlGeneral.DataSource = dataSource;
    //    ddlGeneral.DataTextField = dataTextField;
    //    ddlGeneral.DataValueField = dataValueField;
    //    ddlGeneral.DataBind();

    //    if (hasSelectItem == true)
    //    {
    //        ddlGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
    //    }

    //    if (hasOtherItem == true)
    //    {
    //        ddlGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
    //    }
    //}

    public static void BindCheckBoxList(CheckBoxList cbl, Object dataSource, string dataTextField, string dataValueField, bool hasAllItem = false, bool hasAllItemsSelected = false)
    {
        cbl.Items.Clear();
        cbl.DataSource = dataSource;
        cbl.DataTextField = dataTextField;
        cbl.DataValueField = dataValueField;
        cbl.DataBind();

        if (hasAllItem == true)
        {
            cbl.Items.Insert(0, new ListItem("All", "0"));
            //cbl.Items[0].Selected = true;
        }
        if (hasAllItemsSelected == true)
        {
            foreach (ListItem li in cbl.Items)
            {
                li.Selected = true;
            }
        }
    }

    public static void BindRadioButtonList(RadioButtonList cbl, Object dataSource, string dataTextField, string dataValueField)
    {
        cbl.DataSource = dataSource;
        cbl.DataTextField = dataTextField;
        cbl.DataValueField = dataValueField;
        cbl.DataBind();

        if (cbl.Items.Count > 0)
        {
            cbl.Items[0].Selected = true;
        }
    }

    public static string getSubstring(string content, int length)
    {
        string substr = content;
        if (content.Length > length)
        {
            substr = content.Substring(0, length) + "...";
        }

        return substr;
    }

    public static string GetCommaSeparatedCBLValues(CheckBoxList cbl, bool breakOnAllSelected = false)
    {
        bool isAllItemSelected = false;
        string value = "";
        foreach (ListItem li in cbl.Items)
        {
            if (li.Selected || isAllItemSelected)
            {
                if (li.Value == "0")
                {
                    isAllItemSelected = true;
                    if (breakOnAllSelected)
                    {
                        break;
                    }
                }
                value += li.Value + ",";
            }
        }
        return value.Length > 1 ? value : null;
    }

    public static string ExportToExcel(Repeater rpt, string FileName, HttpResponse Response)
    {
        try
        {
            //TextWriter tw;// = new TextWriter();
            //HttpResponse Response = new HttpResponse();// = new HttpResponse();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=\"" + FileName + "\".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            rpt.RenderControl(htw);
            string style = @"<style> TD.exceltext { mso-number-format:\@; } </style> ";
            Response.Write(style);
            //Response.Write(CreateExcelCSS());

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    private static string CreateExcelCSS()
    {
        string css = @"<style> 
            table {
                /*background: none repeat scroll 0 0 #fff;*/
                border: 1px #585858 solid;
                /*margin: 5px 0 0 5px;*/
                width: 100%;
            }

            .gridline a {
                color: #ffffff;
                font-family: Calibri;
                font-size: 13px;
                text-decoration: none;
                margin: 10px auto;
            }

            th {
                color: #eee;
                font-weight: normal;
                padding: 5px 5px 10px;
                font: 15px;
                background-color: Red;
                text-align: center;
            }

            .gridline td {
                padding: 2px 2px 2px 2px;
                text-transform: capitalize;
                border-bottom: 1px #585858 solid;
                border-right: 1px #585858 solid;
                text-align: center;
            }

            tr:nth-child(even) {
                background: #E0EDF3;
            }

            tr:nth-child(odd) {
                background: #FFF;
            }
        </style>";
        return css;
    }


    public static void SetRepeaterFooter(Repeater rpt)
    {
        RepeaterItem item = (RepeaterItem)rpt.Controls[rpt.Controls.Count - 1];
        if (item.ItemType == ListItemType.Footer)
        {
            for (int i = 1; i <= item.Controls.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlInputHidden hfFooter = (System.Web.UI.HtmlControls.HtmlInputHidden)item.FindControl("hf" + i);
                if (hfFooter != null)
                {
                    string strClass = Convert.ToString(hfFooter.Attributes["class"]);
                    Label lbl = (Label)item.FindControl(strClass);
                    if (lbl != null)
                    {
                        lbl.Text = hfFooter.Value;
                    }
                }
            }
        }
    }
    //    private static string JavascriptFunction()
    //    {
    //        string str = @"<script type='text/javascript'>
    //
    //            function pageLoad() {
    //                SetFooter();
    //            }
    //            function SetFooter() {
    //            //alert($('.ShowFooter').size());
    //            $('.ShowFooter').each(function () {
    //                var total = 0;
    //                var className = "";
    //                $(this).removeClass('ShowFooter');
    //                if ($(this).hasClass('intFooter')) {
    //                    $(this).removeClass('intFooter');
    //                    className = $(this).attr('class');
    //                    $('.' + className).each(function () {
    //                        total += parseInt($(this).html());
    //                    });
    //                    $('.Footer' + className).html(total);
    //                }
    //                else if ($(this).hasClass('timeFooter')) {
    //                    $(this).removeClass('timeFooter');
    //                    className = $(this).attr('class');
    //                    $('.' + className).each(function () {
    //                        total += toSeconds($(this).html());
    //                    });
    //                    $('.Footer' + className).html(toHHMMSS(total));
    //                }
    //            });
    //        }
    //        function toSeconds(time) {
    //            var parts = time.split(':');
    //            return (+parts[0]) * 60 * 60 + (+parts[1]) * 60 + (+parts[2]);
    //        }
    //
    //        function toHHMMSS(sec) {
    //            var sec_num = parseInt(sec, 10); // don't forget the second parm
    //            var hours = Math.floor(sec_num / 3600);
    //            var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
    //            var seconds = sec_num - (hours * 3600) - (minutes * 60);
    //
    //            if (hours < 10) { hours = '0' + hours; }
    //            if (minutes < 10) { minutes = '0' + minutes; }
    //            if (seconds < 10) { seconds = '0' + seconds; }
    //            var time = hours + ':' + minutes + ':' + seconds;
    //            return time;
    //        }
    //
    //    </script>";

    //        return str;
    //    }



    // calculate off days and holiday

    public static DataTable get_DataTable_WithPara(string sp_name, SqlParameter[] param)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminManagementSystem"].ToString());

        DataTable dt = new DataTable();

        var connectionState = conn.State;
        try
        {
            {
                if (connectionState != ConnectionState.Open)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sp_name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (param != null)
                        foreach (var item in param)
                        {
                            cmd.Parameters.Add(item);
                        }

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (connectionState != ConnectionState.Open)
                conn.Close();
        }
        return dt;
    }

}
