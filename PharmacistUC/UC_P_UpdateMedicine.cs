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
    public partial class UC_P_UpdateMedicine : UserControl
    {

        function fn = new function();
        String query;
        public UC_P_UpdateMedicine()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtMediId.Text!="")
            {
                query = "select * from medic where mid = '" + txtMediId.Text + "'";
                DataSet ds = fn.getData(query);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    

                    txtMediName.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtMediNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                    DateTime manDate = DateTime.Parse(ds.Tables[0].Rows[0][4].ToString(),System.Globalization.CultureInfo.InvariantCulture);
                    txtMDate.Text = manDate.ToString();
                    DateTime exDate = DateTime.Parse(ds.Tables[0].Rows[0][5].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    txtEDate.Text = exDate.ToString();
                    //txtEDate.Text = ; 
                    txtAvailableQuantity.Text = ds.Tables[0].Rows[0][6].ToString();
                    txtPricePerUnit.Text = ds.Tables[0].Rows[0][7].ToString();
                    
                    
                

                }
                else
                {
                    MessageBox.Show(txtMediId.Text + " kimliğine sahip bir ilaç mevcut değil.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                clearAll();
            }
        }

        private void clearAll()
        {
            txtAddQuantity.Clear();
            txtAvailableQuantity.Clear();
            txtEDate.ResetText();
            txtMDate.ResetText();
            txtMediId.Clear();
            txtMediName.Clear();
            txtMediNumber.Clear();
            txtPricePerUnit.Clear();
            if (txtAddQuantity.Text != "0")
            {
                txtAddQuantity.Text = "0";
            }
            else
            {
                txtAddQuantity.Text = "0";
            }

        }

        private void UC_P_UpdateMedicine_Load(object sender, EventArgs e)
        {
            txtEDate.Format = DateTimePickerFormat.Custom;
            txtEDate.CustomFormat = "MM/dd/yyyy";

            txtMDate.Format = DateTimePickerFormat.Custom;
            txtMDate.CustomFormat = "MM/dd/yyyy";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        Int64 totalQuantity;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String mname = txtMediName.Text;
            String mnumber = txtMediNumber.Text;
            String mdate = txtMDate.Text;
            String edate = txtEDate.Text;
            Int64 quantity = Int64.Parse(txtAvailableQuantity.Text);
            Int64 addquantity = Int64.Parse(txtAddQuantity.Text);
            Int64 unitprice = Int64.Parse(txtPricePerUnit.Text);
            totalQuantity = quantity + addquantity;

            query = "update medic set mname = '" + mname + "',mnumber= '" + mnumber + "',mDate= '" + mdate + "',eDate='" + edate + "', quantity='" + totalQuantity + "', perUnit='" + unitprice + "' where mid ='" + txtMediId.Text + "'   ";
            fn.setData(query, "İlaç Detayları Güncellendi.");


        }
    }
}
