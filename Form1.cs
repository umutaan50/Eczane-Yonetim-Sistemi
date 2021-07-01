using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane_Yönetim_Sistemi
{
    public partial class Form1 : Form
    {
        function fn = new function();
        String query;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtUsername.Clear();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            query = "select * from users";
            ds = fn.getData(query);
            if (ds.Tables[0].Rows.Count==0)
            {
                if (txtUsername.Text=="root" && txtPassword.Text=="root")
                {
                    Administrator admin = new Administrator();
                    admin.Show();
                    this.Hide();
                }
            }

            else
            {
                query = "select *from users where username='" + txtUsername.Text + "' and pass='" + txtPassword.Text + "'";
                ds = fn.getData(query);
                if (ds.Tables[0].Rows.Count!=0)
                {
                    String role = ds.Tables[0].Rows[0][1].ToString();
                    if (role== "Yönetici")
                    {
                        Administrator admin = new Administrator(txtUsername.Text);
                        admin.Show();
                        this.Hide();
                    }
                    else if (role == "Eczacı")
                    {
                        Pharmacist pharm = new Pharmacist();
                        pharm.Show();
                        this.Hide();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Yanlış kullanıcı adı ya da şifre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  
                }
            }








            /*if (txtUsername.Text=="ukö" && txtPassword.Text == "123")
            {
                Administrator am = new Administrator();
                am.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Yanlık kullanıcı adı ya da şifre girdiniz", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Şifrenin gözükmesi
            txtPassword.PasswordChar = '\0';
            btnClosedEye.Visible = false;
            btnOpenedEye.Visible = true;
        }

        private void btnOpenedEye_Click(object sender, EventArgs e)
        {
            // Şifrenin gizlenmesi
            txtPassword.PasswordChar = '*';
            btnOpenedEye.Visible = false;
            btnClosedEye.Visible = true;
        }
    }
}
