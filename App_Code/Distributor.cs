using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

public class Distributor
{
    public List<ExternalSystem> systemlist;

    public Distributor()
    {
        systemlist = new List<ExternalSystem>();
        systemlist.Add(new NewCustomer());
        systemlist.Add(new DefaultSystem());
    }

    public void ProcessWorkstep(string workstepId)
    {
        string documentId = null;

        WorkstepWebServiceReference.ManagementWorkstepControllerSoapClient soapClient = new WorkstepWebServiceReference.ManagementWorkstepControllerSoapClient();
        //string workstepInfo = soapClient.GetWorkstepInformation_v1(workstepId);
        string workstepStatus = soapClient.GetWorkstepStatus_v1(workstepId);

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(workstepStatus);

        string result = null;
        XmlNodeList elList = xml.GetElementsByTagName("baseResult");
        if(elList.Count > 0)
        {
            result = elList[0].InnerXml;
        }

        if(result != "ok") { return; }

        XmlNodeList documentList = xml.GetElementsByTagName("documentId");
        if(documentList.Count > 0)
        {
            documentId = documentList[0].InnerXml;
        }

        string fileData = soapClient.GetFileWithId_v1(documentId);

        result = null;

        xml.LoadXml(fileData);

        elList = xml.GetElementsByTagName("baseResult");
        if (elList.Count > 0)
        {
            result = elList[0].InnerXml;
        }

        if (result != "ok") { return; }

        string documentName = xml.GetElementsByTagName("SourceFileName")[0].InnerXml;
        string documentDataString = xml.GetElementsByTagName("SourceFileContent")[0].InnerXml;
        byte[] documentData = Convert.FromBase64String(documentDataString);

        string prefix = GetPrefix(documentName);
        ExternalSystem es = GetSystemByPrefix(prefix);

        es.SendToSystem(documentName, documentData);

        //File.WriteAllBytes(@"f:\a.pdf", documentData);

        return;
    }

    public string GetRedirectURL()
    {
        return null;
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
}