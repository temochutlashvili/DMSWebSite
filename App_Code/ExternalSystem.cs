using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public abstract class ExternalSystem
{
    private string _prefix;

    public ExternalSystem()
    {

    }

    public ExternalSystem(string prefix)
    {
        _prefix = prefix;
    }

    public abstract void WriteToDB();

    public abstract void SendToSystem(string name, byte[] document);

    public bool CheckPrefix(string prefix) { return _prefix == prefix; }
}