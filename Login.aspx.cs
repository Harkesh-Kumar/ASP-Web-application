using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;


namespace WebApplication3
{
    public partial class Login : System.Web.UI.Page
    {
       
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (authenticate(TextBox1.Text, TextBox2.Text))
            {
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                Label1.Text = "Invalid Username and Password";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }
        private bool authenticate(string Username, string Passsword)
        {

            SqlConnection con = new SqlConnection(@"Data Source=Nilesh;Initial Catalog=test_db;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("sp_select", con);
            cmd.CommandType = CommandType.StoredProcedure;

            string encryp = FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox2.Text, "SHA1");

            cmd.Parameters.AddWithValue("@Username", TextBox1.Text);
            cmd.Parameters.AddWithValue("@Password",encryp);
            con.Open();
            int codereturn = (int)cmd.ExecuteScalar();
            return codereturn == 1;
                
        }

    }

}