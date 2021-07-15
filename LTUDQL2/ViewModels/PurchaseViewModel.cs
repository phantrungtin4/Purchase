using LTUDQL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LTUDQL2.ViewModels
{
    public class PurchaseViewModel : BaseViewModel
    {

        private int _Cost;
        public int Cost { get => _Cost; set { _Cost = value; OnPropertyChanged(); } }

 

        private int _IDUser;
        public int IDUser { get => _IDUser; set { _IDUser = value; OnPropertyChanged(); } }

        public ICommand PurchaseCommand { get; set; }

        public PurchaseViewModel()
        {
            PurchaseCommand = new RelayCommand<Page>((prams) => { return true; }, (prams) => { Purchase(prams); });
        }

        void Purchase(Page parms)
        {
            if (parms == null)
                return;
            else
            {



                var objData = Models.DataProvider.Ins.DB.Purchases.Add(new Models.Purchase());
                {
                    objData.Cost = Int32.Parse("70000");
                    objData.DatePurchase = DateTime.Now;
                    objData.DateExpire = DateTime.UtcNow.AddDays(30);
                    objData.IDUser = IDUser;

                    Models.DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thanh toán thành công!", "Thông Báo!");

                }


            }
        }
    }
}
