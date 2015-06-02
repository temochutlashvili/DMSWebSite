using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

public class Distributor
{
    public List<ExternalSystem> systemlist;
    public ExternalSystem _defaultsystem;
    private string _documentName;
    private byte[] _documentData;
    private ExternalSystem _es;

    public Distributor()
    {
        _defaultsystem = new DefaultSystem();

        systemlist = new List<ExternalSystem>();
        systemlist.Add(new NewCustomer());
    }

    public Boolean ProcessWorkstep(NameValueCollection _params)
    {
        string documentId = null;
        string workstepId = null;
        string id = null;
        string sysid = null;

        workstepId = _params["WorkstepID"];
        id = _params["id"];
        sysid = _params["sysid"];

        _es = GetSystemById(sysid);
        if (_es == null) { Util.WriteToLog("system not found"); return false; };

        WorkstepWebServiceReference.ManagementWorkstepControllerSoapClient soapClient = new WorkstepWebServiceReference.ManagementWorkstepControllerSoapClient();
        string workstepStatus = soapClient.GetWorkstepStatus_v1(workstepId);

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(workstepStatus);

        if (getElementByName("baseResult", xml) != "ok") { Util.WriteToLog("workstep error"); return false; }
        if (getElementByName("workstepFinished", xml) != "1") { Util.WriteToLog("workstep rejected"); return false; }
        documentId = getElementByName("documentId", xml);

        string fileData = soapClient.GetFileWithId_v1(documentId);

        xml.LoadXml(fileData);

        if (getElementByName("baseResult", xml) != "ok") { Util.WriteToLog("document error"); return false; }

        _documentName = xml.GetElementsByTagName("SourceFileName")[0].InnerXml;
        string documentDataString = xml.GetElementsByTagName("SourceFileContent")[0].InnerXml;
        _documentData = Convert.FromBase64String(documentDataString);

        _es.id = id; 
        _es.WriteToDB();
        _es.SaveFile(_documentName, _documentData);
        _es.SendToSystem(_documentName, _documentData);

        return true;
    }

    public string GetRedirectURL()
    {
        if(_es != null) { return _es.GetRedirectURL();  } else return null ;
    }

    public ExternalSystem GetSystemById(string id)
    {
        foreach (ExternalSystem es in systemlist)
        {
            if (es.CheckSystemId(id)) { return es; }
        }

        return _defaultsystem;
    }

    public ExternalSystem GetSystemByPrefix(string prefix)
    {
        ExternalSystem extsys = null;

        if (string.IsNullOrEmpty(prefix)) { return null; }

        foreach (ExternalSystem es in systemlist)
        {
            if (es.CheckPrefix(prefix))
            {
                extsys = es;
                break;
            }
        }

        return extsys;
    }

    private string GetPrefix(string str)
    {
        var i = str.IndexOf('_');
        return (i > 0) ? str.Substring(0, i) : "default_system";
    }

    private string getElementByName(string name, XmlDocument xml)
    {
        XmlNodeList elList = xml.GetElementsByTagName(name);
        if (elList.Count > 0)
        {
            return elList[0].InnerXml;
        }

        return null;
    }

}