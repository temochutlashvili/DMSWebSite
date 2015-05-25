using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public abstract class ExternalSystem
{
    private string _prefix;
    public string id;

    public ExternalSystem()
    {

    }

    public ExternalSystem(string prefix)
    {
        _prefix = prefix;
    }

    public abstract void WriteToDB();

    public void SaveLocally(string name, byte[] document) {

    }

    public abstract void SendToSystem(string name, byte[] document);

    public abstract string GetRedirectURL();

    public bool CheckPrefix(string prefix) { return _prefix == prefix; }
}