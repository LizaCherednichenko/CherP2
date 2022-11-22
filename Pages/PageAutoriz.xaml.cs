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
    /// Логика взаимодействия для PageAutoriz.xaml
    /// </summary>
    public partial class PageAutoriz : Page
    {
        public PageAutoriz()
        {
            InitializeComponent();
        }

        private void BtnIn_Click(object sender, RoutedEventArgs e)
        {
            var userObj = DBEntities.GetContext().User.FirstOrDefault(x => x.login == TbLogin.Text && x.password == PbPass.Password);
            if (userObj == null)
            {
                MessageBox.Show("Такого пользователя не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                switch (userObj.role)
                {
                    case "admin":
                        {
                            MessageBox.Show("Здравствуйте, Администратор!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.MainFrame.Navigate(new PageAdmin());
                            break;
                        }
                    case "user":
                        {
                            MessageBox.Show("Здравствуйте, пользователь!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.MainFrame.Navigate(new PageUser());
                            break;
                        }
                }
            }
        }
    }
}
