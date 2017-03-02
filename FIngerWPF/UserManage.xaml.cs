using FIngerWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace FIngerWPF
{
    /// <summary>
    /// UserManage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManage : Window
    {
        FingerWPFModel db = new FingerWPFModel();
        public UserManage()
        {
            InitializeComponent();
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            string name = TBusername.Text.Trim();
            if (String.IsNullOrEmpty(name))
                return;

            db.AccountInfoes.Add(new AccountInfo()
            {
                AccountName = name
            });

            try
            {
                await db.SaveChangesAsync();
                MessageBox.Show("OK");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource accountInfoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("accountInfoViewSource")));
            // 通过设置 CollectionViewSource.Source 属性加载数据: 
            // accountInfoViewSource.Source = [一般数据源]
            await db.AccountInfoes.ToListAsync();
            accountInfoViewSource.Source = db.AccountInfoes.Local;
        }

        private void GotoFinger_Click(object sender, RoutedEventArgs e)
        {
            FingerManage fm = new FingerManage();
            fm.ShowDialog();
        }
    }
}
