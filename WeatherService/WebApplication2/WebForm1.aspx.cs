using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string inputtext;
            string[] outputText;
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();

            inputtext = TextBox1.Text;

            outputText = client.Weather(inputtext);

           
            Label2.Text = outputText[0];
            Label5.Text = outputText[1];
            Label7.Text = outputText[2];
            Label8.Text = outputText[3];
            Label4.Text = outputText[4];

            Label12.Text = outputText[5];
            Label13.Text = outputText[6];
            Label14.Text = outputText[7];
            Label15.Text = outputText[8];
            Label16.Text = outputText[9];;
            
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}