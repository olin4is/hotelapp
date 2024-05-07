using System.Windows;
using System.Windows.Controls;
using кркр.ViewModels;

namespace кркр.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
            
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AuthorizationViewModel).Password = PasswordBox.Password;
        }
    }
}
