using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane_Yönetim_Sistemi.PharmacistUC
{
    public partial class UC_P_AddMedicine : UserControl
    {

        function fn = new function();
        String query;


        public UC_P_AddMedicine()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMediId.Text != "" && txtMediName.Text != "" && txtMediNumber.Text != "" && txtQuantity.Text != "" && txtPricePerUnit.Text != "")
            {
                //veritabanı işlemleri


                try
                {
                    String mid = txtMediId.Text;
                    String mname = txtMediName.Text;
                    String mnumber = txtMediNumber.Text;
                    String mdate = txtManufacturingDate.Text;
                    String edate = txtExpireDate.Text;
                    Int64 quantity = Int64.Parse(txtQuantity.Text);
                    Int64 perunit = Int64.Parse(txtPricePerUnit.Text);

                    query = "insert into medic(mid,mname,mnumber,mDate,eDate,quantity,perUnit) values ('" + mid + "', '" + mname + "', '" + mnumber + "', '" + mdate + "', '" + edate + "', " + quantity + ", " + perunit + ")";
                    // bigintler için '' kullanıp kullanmamak arasında kaldım.
                    fn.setData(query, "İlaç veritabanına eklendi.");
                }
                catch (FormatException fe)
                {

                    MessageBox.Show("Sayı girmeniz gereken yere sayı,\nharf girmeniz gereken yere harf giriniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (OverflowException of)
                {
                    MessageBox.Show("Öngörülen sınırlar dışında değer girdiniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Boş kutucuk bıraktınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();            
        }

        public void clearAll()
        {
            txtMediId.Clear();
            txtMediName.Clear();
            txtMediNumber.Clear();
            txtPricePerUnit.Clear();
            txtQuantity.Clear();
            txtExpireDate.ResetText();
            txtManufacturingDate.ResetText();
        }

        private void UC_P_AddMedicine_Load(object sender, EventArgs e)
        {
            // bir başka çözüm : )
            txtExpireDate.Format = DateTimePickerFormat.Custom;
            txtExpireDate.CustomFormat = "MM/dd/yyyy";

            txtManufacturingDate.Format = DateTimePickerFormat.Custom;
            txtManufacturingDate.CustomFormat = "MM/dd/yyyy";
        }
    }
}
