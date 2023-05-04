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
    public partial class authormanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["role"].Equals(""))
                {
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                   
                }
            }
            catch(Exception ex)
            {

            }
           

        }

        //add
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBox1.Text))
            {
                Response.Write("<script>alert('Fill in the id of the book');</script>");
            }
            else if (String.IsNullOrWhiteSpace(TextBox2.Text))
            {
                Response.Write("<script>alert('Fill in the Authors name before submitting');</script>");
            }
            else
            {
                if (checkifauthorexist())
                {
                    Response.Write("<script>alert('The author ID exist Use another ');</script>");
                }
                else
                {
                    addnewauthor();

                }
            }
        }
        //update
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkifauthorexist())
            {
                updateauthor();

            }
            else
            {
                Response.Write("<script>alert('Author does not exist');</script>");
            }
        }
        //delete
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkifauthorexist())
            {
                deleteAuthor();

            }
            else
            {
                Response.Write("<script>alert('Author does not exist');</script>");
            }
        }
        //go button
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthorByID();
        }

        bool checkifauthorexist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id ='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        void getAuthorByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Author ID');</script>");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }
        void deleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Deleted Successfully');</script>");
                clearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void updateauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name=@author_name WHERE author_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());



                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Successfully updated an author');</script>");
                Response.Redirect("authormanagement.aspx");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void addnewauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id, author_name) values(@authorid,@authorname)", con);
                cmd.Parameters.AddWithValue("@authorid", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@authorname", TextBox2.Text.Trim());



                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Successfully registered an author');</script>");

                Response.Redirect("authormanagement.aspx");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }


    }
}
