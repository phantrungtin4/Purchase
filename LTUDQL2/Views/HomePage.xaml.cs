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

namespace LTUDQL2.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri("pack://application:,,,/Images/bg-project.jpg", UriKind.Absolute));
            this.Background = myBrush;
            mainHomepage.Content = new Views.NewRegister();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ColorZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnLogOutPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ms = new MainWindow();
            ms.Show();
            this.Close();
        }

        private void CircularBorder_Click(object sender, RoutedEventArgs e)
        {
            mainHomepage.Content = new Views.UserProfile(emailUser.Text);
        }

        private void BtnPurchase_Click(object sender, RoutedEventArgs e)
        {

            mainHomepage.Content = new Views.Purchase(emailUser.Text);
        }
    }
}
