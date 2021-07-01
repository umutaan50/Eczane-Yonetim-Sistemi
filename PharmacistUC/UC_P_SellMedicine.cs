using DGVPrinterHelper;
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

    public partial class UC_P_SellMedicine : UserControl
    {
        function fn = new function();
        String query;
        DataSet ds;


        public UC_P_SellMedicine()
        {
            InitializeComponent();
        }

        private void UC_P_SellMedicine_Load(object sender, EventArgs e)
        {
            txtExpireDate.Format = DateTimePickerFormat.Custom;
            txtExpireDate.CustomFormat = "MM/dd/yyyy";

            


            listBoxMedicines.Items.Clear();
            query = "select mname from medic where eDate >= getDate() and quantity > '0' ";
            ds = fn.getData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count;i++)
            {
                listBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());


            }        
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_SellMedicine_Load(this, null);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listBoxMedicines.Items.Clear();
            query = "select mname from medic where mname like '" + txtSearch.Text + "%' and eDate >= getdate() and quantity > '0' ";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        
        }

        private void listBoxMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumberOfUnits.Clear();

            String name = listBoxMedicines.GetItemText(listBoxMedicines.SelectedItem);

            txtMediName.Text = name;
            query = "select mid,eDate,perUnit from medic where mname = '" + name + "'";
            ds = fn.getData(query);
            txtMediId.Text = ds.Tables[0].Rows[0][0].ToString();
            DateTime manDate = DateTime.Parse(ds.Tables[0].Rows[0][1].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            txtExpireDate.Text = manDate.ToString();
            txtPricePerUnit.Text = ds.Tables[0].Rows[0][2].ToString();



        }

        private void txtNumberOfUnits_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberOfUnits.Text!="")
            {
                try
                {
                    Int64 unitPrice = Int64.Parse(txtPricePerUnit.Text);
                    Int64 noOfUnit = Int64.Parse(txtNumberOfUnits.Text);
                    Int64 totalAmount = unitPrice * noOfUnit;
                    txtTotalPrice.Text = totalAmount.ToString();
                }
                catch (OverflowException of)
                {
                    MessageBox.Show("Öngörülen sınırlar dışında bir değer girdiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
                catch (FormatException fe)
                {
                    
                    MessageBox.Show("Sayı girmeniz gereken yere sayı girmelisiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
            else
            {
                txtTotalPrice.Clear();
            }
        }

        protected int n, totalAmount = 0;
        protected Int64 quantity, newQuantity;

        

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtMediId.Text!="")
            {
                try
                {
                    query = "select quantity from medic where mid = '" + txtMediId.Text + "'";
                    ds = fn.getData(query);

                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity - Int64.Parse(txtNumberOfUnits.Text);





                    if (newQuantity >= 0)
                    {
                        n = guna2DataGridView1.Rows.Add();
                        guna2DataGridView1.Rows[n].Cells[0].Value = txtMediId.Text;
                        guna2DataGridView1.Rows[n].Cells[1].Value = txtMediName.Text;
                        guna2DataGridView1.Rows[n].Cells[2].Value = txtExpireDate.Text;
                        guna2DataGridView1.Rows[n].Cells[3].Value = txtPricePerUnit.Text;
                        guna2DataGridView1.Rows[n].Cells[4].Value = txtNumberOfUnits.Text;
                        guna2DataGridView1.Rows[n].Cells[5].Value = txtTotalPrice.Text;

                        totalAmount = totalAmount + int.Parse(txtTotalPrice.Text);
                        totalLabel.Text = "Toplam " + totalAmount.ToString();

                        query = "update medic set quantity = '" + newQuantity + "' where mid = '" + txtMediId.Text + "'";
                        fn.setData(query, "İlaç eklendi.");
                    }

                    else
                    {
                        MessageBox.Show("O kadar ilaç mevcut değil. \n  Yalnızca " + quantity + " adet ilaç mevcut.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    clearAll();
                    UC_P_SellMedicine_Load(this, null);
                }

                catch (FormatException fe)
                {

                    MessageBox.Show("Harf girmeniz gereken yere harf, rakam girmeniz gerken yere rakam giriniz", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (OverflowException of)
                {

                }

            }
            else
            {
                MessageBox.Show("Önce ilacı seçin.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                

        }

        int valueAmount;
        String valueId;
        protected Int64 noOfunit;

        

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valueAmount = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                valueId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                noOfunit = Int64.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            
            
            }
            catch 
            {

                
            }
        }

        private void btnPurchasePrint_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "İlaç Faturası";
            print.SubTitle = String.Format("Tarih:- {0}", DateTime.Now.Date);
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            print.Footer = "Total Ödenebilir Miktar: " + totalLabel.Text;
            print.FooterSpacing = 15;
            print.PrintDataGridView(guna2DataGridView1);

            totalAmount = 0;
            totalLabel.Text = "Toplam 00";
            guna2DataGridView1.DataSource = 0;
        
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (valueId != "")
            {
                try
                {
                    guna2DataGridView1.Rows.RemoveAt(this.guna2DataGridView1.SelectedRows[0].Index);
                }
                catch (Exception)
                {

                    MessageBox.Show("Eksik giriş yapmayınız.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    query = "select quantity from medic where mid = '" + valueId + "'  ";
                    ds = fn.getData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity + noOfunit;

                    query = "update medic set quantity = '" + newQuantity + "' where mid = '" + valueId + "' ";
                    fn.setData(query, "İlaç sepetten çıkarıldı.");
                    totalAmount = totalAmount - valueAmount;
                    totalLabel.Text = "Toplam: " + totalAmount.ToString();
                
                }
                UC_P_SellMedicine_Load(this, null);


            }
        }

        private void clearAll()
        {
            txtMediId.Clear();
            txtMediName.Clear();
            txtExpireDate.ResetText();
            txtPricePerUnit.Clear();
            txtNumberOfUnits.Clear();
        }
        
    }
}
