using LTUDQL2.Models;
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

namespace LTUDQL2.Views
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Page
    {
        public UserProfile()
        {
            InitializeComponent();
        }

        string _email = "";
        public UserProfile(string email) : this()
        {
            _email = email;
            emailText.Text = _email;
            Renderinfo(email);
        }

        void Renderinfo(string email)
        {
            var objData = Models.DataProvider.Ins.DB.Users.Where(u => u.Email == email).SingleOrDefault();

            if (objData != null)
            {
                idText.Text = objData.IDUser.ToString();
                emailText.Text = objData.Email;
                nameText.Text = objData.Name;
                bankText.Text = objData.PurchaseInfo;
            }
            else
                return;
        }


        void OpenText()
        {
            emailText.IsEnabled = true;
            nameText.IsEnabled = true;
            bankText.IsEnabled = true;
        }

        void CloseText()
        {
            emailText.IsEnabled = false;
            nameText.IsEnabled = false;
            bankText.IsEnabled = false;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            OpenText();
            updateBtn.IsEnabled = false;
            submitBtn.IsEnabled = true;
        }

        private void BackHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views/NewRegister.xaml", UriKind.Relative));
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(idText.Text);
            var objData = Models.DataProvider.Ins.DB.Users.Where(u => u.IDUser == id).SingleOrDefault();


            if (objData != null)
            {
                objData.Email = emailText.Text;
                objData.Name = nameText.Text;
                objData.PurchaseInfo = bankText.Text;

                Models.DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Cập Nhật Thành Công!", "Thông Báo!");
                ViewModels.LoginViewModel vm = new ViewModels.LoginViewModel();
                vm.Email = emailText.Text;
                var parentForm = Window.GetWindow(this);
                Views.HomePage hpv = new Views.HomePage();
                hpv.emailUser.Text = emailText.Text;
                hpv.Show();
                parentForm.Close();
                CloseText();
                submitBtn.IsEnabled = false;
                updateBtn.IsEnabled = true;
            }
            else
            {
                CloseText();
                submitBtn.IsEnabled = false;
                updateBtn.IsEnabled = true;
                return;
            }
        }
    }
}
