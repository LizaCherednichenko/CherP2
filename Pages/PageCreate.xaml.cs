using CherP2.ApplicationData;
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

namespace CherP2.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageCreate.xaml
    /// </summary>
    public partial class PageCreate : Page
    {


        private Table1 _currentTovar = new Table1();

        public PageCreate(Table1 _selectedData)
        {
            InitializeComponent();

            if (_selectedData != null)
            {
                _currentTovar = _selectedData;
            }

            DataContext = _currentTovar;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentTovar.name))
            {
                errors.AppendLine("Укажите наименование");
            }

            if (_currentTovar.kolich < 0)
            {
                errors.AppendLine("Количество не может быть меньше 0");
            }

            if (_currentTovar.cena <= 0)
            {
                errors.AppendLine("Цена не может быть меньше или равана 0");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            if (_currentTovar.id == 0)
                DBEntities.GetContext().Table1.Add(_currentTovar);

            try
            {
                DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }
    }
}
