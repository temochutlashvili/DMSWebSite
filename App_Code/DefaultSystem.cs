using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DefaultSystem : ExternalSystem
{
    const string _prefix = "default_system";
    const string _systemid = "default_system";
    const string _URLToSave = "/";

    public DefaultSystem() : base(_prefix, _systemid)
    {

    }

    public override void WriteToDB()
    {
        // empty
    }

    public override void SendToSystem(string name, byte[] document)
    {
        // empty
    }

    public override string GetRedirectURL()
    {
        return null;
    }

    public override void SaveFile(string name, byte[] document)
    {
        base.SaveToURL(name, document, _URLToSave);
    }
}