﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace BVaccine
{
    public partial class loginform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["User"] = null;
            Login1.Focus();
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Boolean blnresult = false;

            blnresult = Authentication(Login1.UserName, Login1.Password);

            if (blnresult == true)
            {
                e.Authenticated = true;
                Session["User"] = Login1.UserName;
                Response.Redirect("Home.aspx");
            }
            else
                e.Authenticated = false;
        }

        protected static Boolean Authentication(string username, string password)
        {
            string selectcmd = "Select username, password from [userRegistration] where username='" + username + "' and password ='" + password + "'";
            
            SqlConnection conn = new SqlConnection("Data Source=F48604;Initial Catalog=BaselineVaccine; User ID=sa; password=123; Integrated Security=False");

            SqlCommand cmd = new SqlCommand(selectcmd, conn);
            SqlDataReader reader;
            conn.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
                return true;
            else
                return false;
        }
    }
}