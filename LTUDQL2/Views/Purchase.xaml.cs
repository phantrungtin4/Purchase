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
    /// Interaction logic for Purchase.xaml
    /// </summary>
    public partial class Purchase : Page
    {
        public Purchase()
        {
            InitializeComponent();
        }

        string _email = "";
        public Purchase(string email) : this()
        {
            _email = email;
            Renderinfo(email);
        }

        void Renderinfo(string email)
        {
            var objData = Models.DataProvider.Ins.DB.Users.Where(u => u.Email == email).SingleOrDefault();

            if (objData != null)
            {
                idText.Text = objData.IDUser.ToString();
                nameText.Text = objData.Name;
                bankText.Text = objData.PurchaseInfo;
                costText.Text = "70000";
            }
            else
                return;
        }

        private void Purchasebtn_Click(object sender, RoutedEventArgs e)
        {

            var objData = Models.DataProvider.Ins.DB.Purchases.Add(new Models.Purchase());
            {
                objData.Cost = Int32.Parse(costText.Text);
                objData.DatePurchase = DateTime.Now;
                objData.DateExpire = DateTime.UtcNow.AddDays(30);
                objData.IDUser = Int32.Parse(idText.Text);

                Console.Write(objData.DatePurchase);

                Models.DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thanh toán thành công!", "Thông Báo!");

            }
        }
    }
}
