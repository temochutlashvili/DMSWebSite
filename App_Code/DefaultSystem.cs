using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DefaultSystem : ExternalSystem
{
    const string _prefix = "default_system";

    public DefaultSystem() : base(_prefix)
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
}