using System;
using System.Collections;
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
        // Hier wird die Schnittstelle zur Datenbank definiert, diese kann man bei den Eigenschaften der Datenbank auslesen
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Sascha\Documents\Pro-Natur Biomarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectedProductKey;

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

            // Der String beinhaltet befehle der SQL Datenbank
            string query = "select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            // Die Datenbankdaten der ersten Tabelle auf dem ScreenproductDGV ausgeben
            productsDGV.DataSource = dataSet.Tables[0];

            // Erste reihe (ID) ausblenden
            productsDGV.Columns[0].Visible = false;

            // Verbindung zur Datenbank schließen
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
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("insert into Products values('{0}','{1}','{2}','{3}')", productName, productBrand, productCategory, productPrice);
            ExecuteQuery(query);

            ClearAllFields();
            ShowProducts();
        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return;
            }
            string productName = textBoxProductName.Text;
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("update Products set Name='{0}', Brand='{1}', Category='{2}', Price='{3}' where ID={4}", productName, productBrand, productCategory, productPrice, lastSelectedProductKey);
            ExecuteQuery(query);

            ShowProducts();
            
        }

        // Die eingaben Textboxen leeren
        private void btnProductClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        // Ein Produkt aus der Liste löschen
        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return;
            }

            string query = string.Format("delete from Products where ID={0};", lastSelectedProductKey);
            ExecuteQuery(query);

            ClearAllFields();
            ShowProducts();
        }

        // Metohde um den query Bearbeiten der Tabelle auszuführen
        private void ExecuteQuery(string query)
        {
            // Der String beinhaltet befehle der SQL Datenbank
            // Wichtig ist um Daten zu schreiben muss erst die Verbindung zur Datenbank geöffnet werden
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
            // Wichtig im Anschluss die Verbindung schließen

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

        private void productsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxProductName.Text = productsDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxProductBrand.Text = productsDGV.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxProductCategory.Text = productsDGV.SelectedRows[0].Cells[3].Value.ToString();
            textBoxProductPrice.Text = productsDGV.SelectedRows[0].Cells[4].Value.ToString();

            lastSelectedProductKey = (int)productsDGV.SelectedRows[0].Cells[0].Value;
        }
    }
}
