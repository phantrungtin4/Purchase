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

namespace LTUDQL2
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void OpenRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm frm = new RegisterForm();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using (var data = new Models.QuanLyTrangPhimEntities1())
            {

                var listUser = data.Users.Where(u => u.Email == txttendangnhap.Text).SingleOrDefault();

                if (listUser != null)
                {
                    if (listUser.Password == txtmatkhau.Password)
                    {
                        MainWindow mw = new MainWindow();
                        this.Close();
                        mw.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mk sai!");
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mk sai!");
                }
            }
        }

        private void OpenChangePass_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordForm frm = new ChangePasswordForm();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
