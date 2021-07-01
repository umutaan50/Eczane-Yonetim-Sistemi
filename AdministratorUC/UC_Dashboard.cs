﻿using System;
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
    public partial class UC_Dashboard : UserControl
    {
        function fn = new function();
        String query;
        DataSet ds;

        public UC_Dashboard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            //kontrol kısmı

            query = "select count(userRole) from users where userRole = 'Yönetici'";
            ds = fn.getData(query);
            setLabel(ds, AdminLabel);

            query = "select count (userRole) from users where userRole = 'Eczacı'";
            ds = fn.getData(query);
            setLabel(ds, PharLabel);
        }

        private void setLabel(DataSet ds, Label lbl)
        {
            //kullanıcı adının gösterme kısmı
            if (ds.Tables[0].Rows.Count!=0)
            {
                lbl.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                lbl.Text = "0";
            }

        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            //yenileme butonu
            UC_Dashboard_Load(this, null);
        }
    }
}
