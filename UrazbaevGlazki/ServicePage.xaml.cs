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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var currentServices = Urazbaev_glazkiEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentServices;
            ComboFilter.SelectedIndex = 0;
            ComboType.SelectedIndex = 0;
        }

        public void UpdateAgent()
        {
            var currentAgent = Urazbaev_glazkiEntities.GetContext().Agent.ToList();

            if (ComboType.SelectedIndex == 1)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeString == "ЗАО").ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeString == "МКК").ToList();
            }

            if (ComboType.SelectedIndex == 3)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeString == "МФО").ToList();
            }

            if (ComboType.SelectedIndex == 4)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeString == "ОАО").ToList();
            }

            if (ComboType.SelectedIndex == 5)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeString == "ООО").ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeString == "ПАО").ToList();
            }



            if (ComboFilter.SelectedIndex == 1)
            {
                currentAgent = currentAgent.OrderBy(p => p.Title).ToList();
            }
            if (ComboFilter.SelectedIndex == 2)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Title).ToList();
            }

            if (ComboFilter.SelectedIndex == 3)
            {
                currentAgent = currentAgent.OrderBy(p => p.Title).ToList();
            }
            if (ComboFilter.SelectedIndex == 4)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Title).ToList();
            }

            if (ComboFilter.SelectedIndex == 5)
            {
                currentAgent = currentAgent.OrderBy(p => p.Priority).ToList();
            }
            if (ComboFilter.SelectedIndex == 6)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Priority).ToList();
            }

            currentAgent = currentAgent.Where(p => p.Title.ToLower().Contains(Search.Text.ToLower()) ||
            p.Phone.ToLower().Replace(" ", "").Replace("+", "").Replace("+", "").Replace("7", "").Replace("-", "").Replace("(", "").Replace(")", "").Contains(Search.Text.ToLower().Replace(" ", "").Replace("+", "").Replace("7", "").Replace("-", "").Replace("(", "").Replace(")", "")) ||
            p.Email.ToLower().Contains(Search.Text.ToLower())).ToList();
            AgentListView.ItemsSource = currentAgent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }



        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgent();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgent();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgent();
        }

        private void AgentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }
    }
}
