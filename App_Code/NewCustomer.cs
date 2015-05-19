using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class NewCustomer : ExternalSystem
{
    const string _prefix = "NewCustomer";

    public NewCustomer() : base(_prefix)
    {

    }

    public override void WriteToDB()
    {
        throw new NotImplementedException();
    }

    public override void SendToSystem(string name, byte[] document)
    {


    }
}