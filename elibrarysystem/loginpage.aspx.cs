/*
 *   Copyright (c) 2023 
 *   All rights reserved.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elibrarysystem
{
    public partial class loginpage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBox1.Text))
            {
                Response.Write("<script>alert('Fill in the Mmeber id');</script>");
            }
            else if (String.IsNullOrWhiteSpace(TextBox2.Text))
            {
                Response.Write("<script>alert('Fill in the password');</script>");
            }
            else
            {
                try {
                    SqlConnection con = new SqlConnection(strcon);
                    if(con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("select * from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'", 
                        con);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while(dr.Read()){
                            Response.Write("<script>alert('Login successful " + dr.GetValue(8).ToString() + "');</script>");
                            Session["username"] = dr.GetValue(8).ToString();
                            Session["fullname"] = dr.GetValue(0).ToString();
                            Session["role"] = "user";
                            Session["status"] = dr.GetValue(10).ToString();
                        }
                        Response.Redirect("homepage.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Credentials');</script>");
                    }
                
                } 
                catch (Exception ex)
                {
                    Response.Write("<script>alert('"+ex.ToString()+"');</script>");
                }
            }
        }
    }
}