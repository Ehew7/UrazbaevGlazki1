﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.Win32;

namespace UrazbaevGlazki
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;

        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        int con = Urazbaev_glazkiEntities.GetContext().Agent.ToList().Count;
        public ServicePage()
        {
            InitializeComponent();
            var currentAgent = Urazbaev_glazkiEntities.GetContext().Agent.ToList();
            ComboFilter.SelectedIndex = 0;
            ComboType.SelectedIndex = 0;
        }


        public void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;
            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }
            Boolean Ifupdate = true;
            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords; ;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;
                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();
                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }

        }

        private void AgentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AgentListView.SelectedItems.Count > 1)
            {
                BtnEditPriority.Visibility = Visibility.Visible;
            }
            else
            {
                BtnEditPriority.Visibility = Visibility.Hidden;
            }
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgent();
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
                currentAgent = currentAgent.OrderBy(p => p.Shop).ToList();
            }
            if (ComboFilter.SelectedIndex == 4)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Shop).ToList();
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
            TableList = currentAgent;
            ChangePage(0, 0);
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgent();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgent();
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));

            UpdateAgent();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
            UpdateAgent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateAgent();
        }
        private void BtnEditPriority_Click(object sender, RoutedEventArgs e)
        {
            int p = 0;
            foreach (Agent AgentLV in AgentListView.SelectedItems)
            {
                if (p < AgentLV.Priority) p = AgentLV.Priority;
            }
            EditPriority window = new EditPriority(p);
            window.ShowDialog();

            if (string.IsNullOrEmpty(window.PriorityText.Text))
            {
                return;
            }
            foreach (Agent AgentLV in AgentListView.SelectedItems)
            {
                AgentLV.Priority = Convert.ToInt32(window.PriorityText.Text);
            }
            try
            {
                Urazbaev_glazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("информвция сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateAgent();
        }

        private void ProdBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProdPage((sender as Button).DataContext as Agent));
        }
        
        


    }
}
