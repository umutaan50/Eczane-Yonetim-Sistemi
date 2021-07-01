using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane_Yönetim_Sistemi.AdministratorUC
{
    public partial class UC_AddUser : UserControl
    {
        function fn = new function();
        String query;

        public UC_AddUser()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

            if (txtUserRole.Text != "" && txtName.Text != "" && txtDob.Text != "" && txtMobileNo.Text != "" && txtEmail.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    String role = txtUserRole.Text;
                    String name = txtName.Text;
                    String dob = txtDob.Text;
                    Int64 mobile = Int64.Parse(txtMobileNo.Text);
                    String email = txtEmail.Text;
                    String username = txtUsername.Text;
                    String pass = txtPassword.Text;

                    query = "insert into users (userRole,name,dob,mobile,email,username,pass) values ('" + role + "','" + name + "','" + dob + "','" + mobile + "','" + email + "','" + username + "','" + pass + "')";
                    fn.setData(query, "Kayıt olma işlemi başarıyla tamamlandı.");
                }
                catch (OverflowException of)
                {
                    MessageBox.Show("Öngörülen sınırlar dışında bir değer girdiniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                catch (FormatException fe)
                {
                    MessageBox.Show("Harf girmeniz gereken yere harf, raka girmeniz gereken yere rakam giriniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                                
                catch (Exception)
                {
                    MessageBox.Show("Bu Kullanıcı Adı zaten mevcut.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Eksik giriş yaptınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }


        public void clearAll()
        {
            txtName.Clear();
            txtDob.ResetText();
            txtMobileNo.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtUserRole.SelectedIndex = -1;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            query = "select * from users where username =  '" + txtUsername.Text + "' ";
            DataSet ds = fn.getData(query);

            if (ds.Tables[0].Rows.Count==0)
            {
                pictureBox1.ImageLocation = @"C:\Users\umuta\OneDrive\Masaüstü\Proje Resimler\yes.png";
            }
            else
            {
                pictureBox1.ImageLocation = @"C:\Users\umuta\OneDrive\Masaüstü\Proje Resimler\no.png";
            }

        }
    }
}
