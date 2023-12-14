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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UrazbaevGlazki
{
    /// <summary>
    /// Логика взаимодействия для ProdPage.xaml
    /// </summary>
    public partial class ProdPage : Page
    {
        private Agent currentAgend = new Agent();
        public ProdPage(Agent agent)
        {
            InitializeComponent();
            currentAgend = agent;
            var currentProduct = Urazbaev_glazkiEntities.GetContext().ProductSale.ToList();
            ProdHistory.ItemsSource = currentProduct.Where(p => p.AgentID == currentAgend.ID);
        }
        public void UpdateAgent()
        {
            var currentProduct = Urazbaev_glazkiEntities.GetContext().ProductSale.ToList();
            ProdHistory.ItemsSource = currentProduct.Where(p => p.AgentID == currentAgend.ID);
        }

        private void ProdHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DeleteProdHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentProductSale = Urazbaev_glazkiEntities.GetContext().ProductSale.ToList();
            var currentService = (sender as Button).DataContext as ProductSale;
            currentProductSale = currentProductSale.Where(p => p.AgentID == currentService.ID).ToList();
            if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Urazbaev_glazkiEntities.GetContext().ProductSale.Remove(currentService);
                    Urazbaev_glazkiEntities.GetContext().SaveChanges();
                    UpdateAgent();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddProdHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProdSale windows = new AddProdSale((sender as Button).DataContext as ProductSale, currentAgend as Agent);
            //UpdateAgent();
            windows.ShowDialog();
            //Manager.MainFrame.Navigate(new Page1((sender as Button).DataContext as ProductSale, currentAgend as Agent));
            UpdateAgent();
        }
    }
}
