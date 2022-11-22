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
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            InitializeComponent();
            DGTable1.ItemsSource = DBEntities.GetContext().Table1.ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageCreate(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGTable1.ItemsSource = DBEntities.GetContext().Table1.ToList();
            }
        }

        private void BtnRedact_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageCreate((sender as Button).DataContext as Table1));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var _dateForDel = DGTable1.SelectedItems.Cast<Table1>().ToList();

            if (MessageBox.Show("Вы действительно хотите удалить выбранные данные?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DBEntities.GetContext().Table1.RemoveRange(_dateForDel);

                try
                {
                    DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                DGTable1.ItemsSource = DBEntities.GetContext().Table1.ToList();
            }
        }
    }
}
