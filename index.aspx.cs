using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string workstepId = Request["WorkstepID"];
        if (!string.IsNullOrEmpty(workstepId)){
            Distributor distributor = new Distributor();
            distributor.ProcessWorkstep(workstepId);
            Response.Redirect(distributor.GetRedirectURL());
        }
    }
}