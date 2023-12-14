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
    /// Логика взаимодействия для EditPriority.xaml
    /// </summary>
    public partial class EditPriority : Window
    {
        private Agent currentAgent = new Agent();

        public EditPriority(int p)
        {
            InitializeComponent();
            PriorityText.Text = p.ToString();
        }

        private void AddPriority_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PriorityText.Text))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Введите новый приоритет агентов!");
            }

        }

        private void ClosePriority_Click(object sender, RoutedEventArgs e)
        {
            PriorityText.Text = "";
            Close();
        }
    }
}
