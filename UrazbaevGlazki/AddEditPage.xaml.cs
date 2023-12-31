﻿using Microsoft.Win32;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Agent currentAgent = new Agent();
        public bool check = false;
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();
            if (SelectedAgent != null)
            {
                currentAgent = SelectedAgent;
                check = true;
            }
                
            DataContext = currentAgent;
        }

        private void CHangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                currentAgent.Logo = myOpenFileDialog.FileName;

                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");
            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if (currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = currentAgent.Phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("+", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Email))
                errors.AppendLine("Укажите почту агента");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            /*MessageBox.Show(ComboType.SelectedIndex.ToString());*/
            currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;

            var allAgent = Urazbaev_glazkiEntities.GetContext().Agent.ToList();
            allAgent = allAgent.Where(p => p.Title == currentAgent.Title).ToList();

            if (allAgent.Count == 0 || check == true)
            {
                if (currentAgent.ID == 0)
                    Urazbaev_glazkiEntities.GetContext().Agent.Add(currentAgent);
                try
                {
                    Urazbaev_glazkiEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show("Уже существует такая услуга");
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentProductSale = Urazbaev_glazkiEntities.GetContext().ProductSale.ToList();
            var currentService = (sender as Button).DataContext as Agent;
            currentProductSale = currentProductSale.Where(p => p.AgentID == currentService.ID).ToList();
            if (currentProductSale.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существуют записи на эту услугу");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Urazbaev_glazkiEntities.GetContext().Agent.Remove(currentService);
                        Urazbaev_glazkiEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
        private void ProdBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProdPage((sender as Button).DataContext as Agent));
        }

    }

}
