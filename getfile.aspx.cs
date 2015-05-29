using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class getfile : System.Web.UI.Page
{

    public bool success;
    public string errorMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
        success = true;

        string fileName = Request.Params["file"];

        if(fileName == null)
        {
            success = false;
            errorMessage = "File name not provided";
            return;
        }

        Response.Clear();

        //if (Request.Browser != null && Request.Browser.Browser == "IE")
        //string sFileName = HttpUtility.UrlPathEncode(sFileName);

        string filePath = Path.Combine("f:\\www\\DMSWebService\\telasi\\dms\\Files\\Default", fileName);
        // Response.Cache.SetCacheability(HttpCacheability.Public); // that's upon you

        if (!File.Exists(filePath))
        {
            success = false;
            errorMessage = "File does not exists";
            return;
        }

        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        // ^                   ^
        // Response.AddHeader("Content-Length", new FileInfo(sFilePath).Length.ToString()); // upon you 

        try
        {
            Response.WriteFile(filePath);
            
        } catch (Exception ex)
        {
            success = false;
            errorMessage = "Error occured";
        }
        finally
        {
            Response.Flush();
            Response.End();
        }
        
    }
}