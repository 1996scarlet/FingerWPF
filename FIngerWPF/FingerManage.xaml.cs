using FIngerWPF.Model;
using FIngerWPF.ToolClass;
using Microsoft.Win32;
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
    /// FingerManage.xaml 的交互逻辑
    /// </summary>
    public partial class FingerManage : Window
    {
        FingerWPFModel db = new FingerWPFModel();
        public FingerManage()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource fingerInfoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("fingerInfoViewSource")));
            // 通过设置 CollectionViewSource.Source 属性加载数据: 
            // fingerInfoViewSource.Source = [一般数据源]
            await db.FingerInfoes.ToListAsync();
            fingerInfoViewSource.Source = db.FingerInfoes.Local;
            System.Windows.Data.CollectionViewSource accountInfoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("accountInfoViewSource")));

            // 通过设置 CollectionViewSource.Source 属性加载数据: 
            // accountInfoViewSource.Source = [一般数据源]
            await db.AccountInfoes.ToListAsync();
            accountInfoViewSource.Source = db.AccountInfoes.Local;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            dlg.FilterIndex = 3;

            if (dlg.ShowDialog() == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(dlg.FileName));

                SimilarFinger sf = new SimilarFinger(bitmapImage);

                db.FingerInfoes.Add(new FingerInfo()
                {
                    AccountName = accountNameComboBox.Text,
                    FingerString = sf.GetHash()
                });

                try
                {
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
