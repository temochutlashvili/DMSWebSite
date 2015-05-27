using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public abstract class ExternalSystem
{
    private string _systemid;
    private string _prefix;
    public string id;

    public ExternalSystem()
    {

    }

    public ExternalSystem(string prefix, string sysid)
    {
        _prefix = prefix;
        _systemid = sysid;
    }

    public abstract void WriteToDB();

    public abstract void SaveFile(string name, byte[] document);

    public abstract void SendToSystem(string name, byte[] document);

    public abstract string GetRedirectURL();

    public bool CheckPrefix(string prefix) { return _prefix == prefix; }

    public bool CheckSystemId(string id) { return _systemid == id; }

    protected void SaveToURL(string name, byte[] document, string URL)
    {
        string filename = Util.GenerateUniqueFileName(Path.Combine(URL, name));
        File.WriteAllBytes(filename, document);
    }

}