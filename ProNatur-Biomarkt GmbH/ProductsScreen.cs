using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNatur_Biomarkt_GmbH
{
    public partial class ProductsScreen : Form
    {
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Sascha\Documents\Pro-Natur Biomarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");

        public ProductsScreen()
        {
            InitializeComponent();

            ShowProducts();
        }

        // Methode zum Auslesen der Datenbank
        private void ShowProducts()
        {
            // Start Verbindung zur Datenbank
            databaseConnection.Open();

            string query = "select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            // Die Datenbankdaten der ersten Tabelle auf dem ScreenproductDGV ausgeben
            productsDGV.DataSource = dataSet.Tables[0];

            // Erste reihe (ID) ausblenden
            productsDGV.Columns[0].Visible = false;

            // Schnittstelle zur Datenbank schließen
            databaseConnection.Close();
        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            // Wenn ein Feld leer ist dann
            if (textBoxProductName.Text == null || textBoxProductBrand.Text == null || textBoxProductPrice.Text == null || comboBoxProductCategory.Text == null)
            {
                MessageBox.Show("Bitte alle Werte ausfüllen!");
                return;
            }

            // Save products to database
            string productName = textBoxProductName.Text;
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            float productPrice = float.Parse(textBoxProductPrice.Text);

            ClearAllFields();
            ShowProducts();
        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {

            ShowProducts();
        }

        // Die eingaben Textboxen leeren
        private void btnProductClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {

            ShowProducts();
        }

        // Methode zum löschen aller Felder der EingabeBoxen
        private void ClearAllFields()
        {
            textBoxProductName.Text = string.Empty;
            textBoxProductBrand.Text = string.Empty;
            textBoxProductPrice.Text = string.Empty;
            comboBoxProductCategory.Text = string.Empty;
            comboBoxProductCategory.SelectedItem = null;
        }
    }
}
