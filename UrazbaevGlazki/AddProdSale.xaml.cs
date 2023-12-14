using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UrazbaevGlazki
{
    /// <summary>
    /// Логика взаимодействия для AddProdSale.xaml
    /// </summary>
    public partial class AddProdSale : Window
    {
        private ProductSale currentProductSale = new ProductSale();
        private Product currentProduct = new Product();
        private Agent currentAgent = new Agent();
        public AddProdSale(ProductSale productsale, Agent agent)
        {
            InitializeComponent();
            currentAgent = agent;
            var currentProduct = Urazbaev_glazkiEntities.GetContext().Product.ToList();
            ComboProduct.ItemsSource = currentProduct;
            //currentProductSale = productsale;
            DataContext = currentProductSale;

        }

        public void UpdateAgent()
        {
            var currentProduct = Urazbaev_glazkiEntities.GetContext().ProductSale.ToList();
            ComboProduct.ItemsSource = currentProduct.Where(p => p.AgentID == currentAgent.ID);
        }
        private void AddProdHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (ProdCount.Text == "")
                errors.AppendLine("Укажите количество");
            if (StartDate.Text == "")
                errors.AppendLine("Укажите дату");
            if (ComboProduct.SelectedItem == null)
                errors.AppendLine("Укажите наименование продукта");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            currentProductSale.ProductID = ComboProduct.SelectedIndex + 1;
            currentProductSale.AgentID = currentAgent.ID;
            currentProductSale.ProductCount = Convert.ToInt32(ProdCount.Text);
            currentProductSale.SaleDate = Convert.ToDateTime(StartDate.Text);


            if (currentProductSale.ID == 0)
            {
                Urazbaev_glazkiEntities.GetContext().ProductSale.Add(currentProductSale);

                try
                {
                    Urazbaev_glazkiEntities.GetContext().SaveChanges();
                    MessageBox.Show("информация сохранена");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

    }
}
