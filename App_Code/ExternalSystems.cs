using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class ExternalSystems
{
    static List<ExternalSystem> systemlist;

    static ExternalSystems()
    {
        systemlist.Add(new NewCustomer());
    }
}