using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Distributor
{
    public List<ExternalSystem> systemlist;

    public Distributor()
    {
        systemlist.Add(new NewCustomer());
    }

    public void ProcessWorkstep(string worksteId)
    {

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
}