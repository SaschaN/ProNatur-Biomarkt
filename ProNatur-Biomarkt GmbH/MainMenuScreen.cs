﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNatur_Biomarkt_GmbH
{
    public partial class MainMenuScreen : Form
    {
        public MainMenuScreen()
        {
            InitializeComponent();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductsScreen productsScreen= new ProductsScreen();
            productsScreen.Show();

            // this deshalb weil wir uns im MainMenuScreen befinden
            // this bezieht sich immer auf der oben genannten Klasse
            this.Hide();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            BillScreen billScreen = new BillScreen();
            billScreen.Show();

            // Siehe oben
            this.Hide();
        }
    }
}
