using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProcesadorTextoMSP
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ProcButton_Click(object sender, EventArgs e)
        {
            TextoProcesadoLabel.Text = Texto.Text + " Cada vez estas mas cerca";
        }
    }
}