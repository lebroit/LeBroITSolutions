using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeBroITSolutions.ChatService
{
    public partial class Default : System.Web.UI.Page
    {
// ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
// ReSharper restore InconsistentNaming
        {
            Response.Redirect("LeBroITSolutions.ServiceStack/Metadata");
        }
    }
}
