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
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Windows.Navigation;
using LTUDQL2.Models;

namespace LTUDQL2
{
    /// <summary>
    /// Interaction logic for ChangePasswordForm.xaml
    /// </summary>
    public partial class ChangePasswordForm : Window
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        string activeEmail = RandomString(10);
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var smtpServerName = ConfigurationManager.AppSettings["SmtpServer"];
            var port = ConfigurationManager.AppSettings["Port"];
            var senderEmailId = ConfigurationManager.AppSettings["SenderEmailId"];
            var senderPassword = ConfigurationManager.AppSettings["SenderPassword"];

            using (var data = new QuanLyTrangPhimEntities1())
            {
                var user = data.Users.Where(u => u.Email == txtemail.Text).SingleOrDefault();

                if (user != null)
                {
                    try
                    {
                        var smptClient = new SmtpClient(smtpServerName, Convert.ToInt32(port))
                        {
                            Credentials = new NetworkCredential(senderEmailId, senderPassword),
                            EnableSsl = true,
                        };

                        smptClient.Send(senderEmailId, txtemail.Text.Trim(), "Mã xác nhận mật khẩu", activeEmail);
                        labelTitle.Visibility = Visibility;
                        txtActivecode.Visibility = Visibility;
                        submitBtn.Visibility = Visibility;
                        sendmailBtn.Visibility = Visibility.Hidden;
                    }
                    catch
                    {
                        MessageBox.Show("Send Mail Failed!");
                    }
                }
                else
                {
                    MessageBox.Show("email không tồn tại hoặc chưa đăng ký");
                }
            }
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtActivecode.Text == activeEmail)
            {
                using (var data = new QuanLyTrangPhimEntities1())
                {
                    var user = data.Users.Where(u => u.Email == txtemail.Text).SingleOrDefault();

                    user.Password = txtmatkhau.Password;
                    data.SaveChanges();

                    MessageBox.Show("Update Successfully!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Mã xác nhận không chính xác!", "Thông báo!");
            }
        }
    }
}
