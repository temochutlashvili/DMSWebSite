using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

class NewCustomer : ExternalSystem
{
    const string _prefix = "NewCustomer";
    const string _systemid = "newcustomer";
    //const string _postURL = "http://my.telasi.ge/signature/callback";
    //const string _redirectURL = "http://my.telasi.ge/network/new_customer/";
    const string _postURL = "http://localhost:3000/signature/callback";
    const string _redirectURL = "http://localhost:3000/network/new_customer/";
    const string _URLToSave = "/";

    public NewCustomer() : base(_prefix, _systemid)
    {

    }

    public override void WriteToDB()
    {
        
    }

    public override void SendToSystem(string name, byte[] document)
    {
        try
        {
            // Generate post objects
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("filename", name);
            postParameters.Add("fileformat", "pdf");
            postParameters.Add("id", id);
            postParameters.Add("sys_file[file]", new FormUpload.FileParameter(document, name, "application/pdf"));

            // Create request and receive response
            string userAgent = "DMSWebClient";
            HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(_postURL, userAgent, postParameters);

            // Process response
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            webResponse.Close();
        } catch(Exception ex)
        {
            Console.Write(ex.Message);
        }
        
    }

    public override string GetRedirectURL()
    {
        return _redirectURL + id;
    }

    public override void SaveFile(string name, byte[] document)
    {
        base.SaveToURL(name, document, _URLToSave);
    }
}