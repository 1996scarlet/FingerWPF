using FIngerWPF.Model;
using FIngerWPF.ToolClass;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

namespace FIngerWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        FingerWPFModel db = new FingerWPFModel();
        SimilarFinger sf;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void userLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            dlg.FilterIndex = 3;

            if (dlg.ShowDialog() == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(dlg.FileName));

                sf = new SimilarFinger(bitmapImage);
                var fingerInfo = await CompareFinger(sf.GetHash());

                if (fingerInfo != null)
                {

                    //TODO:
                    MessageBox.Show("已成功识别用户：" + fingerInfo.AccountName + "，指纹ID：" + fingerInfo.FingerId );
                }
                else
                {
                    MessageBox.Show("未找到匹配的用户");
                }

                image.Source = bitmapImage;    
            }
        }

        private async Task<FingerInfo> CompareFinger(string FingerRes)
        {
            var find = db.FingerInfoes.Where(f => f.FingerString == FingerRes);

            return await find.FirstOrDefaultAsync();
        }

        private void adminLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TBusername.Text.Trim();
            string password = TBpassword.Text.Trim();
            if (username == "admin" && password == "123456")
            {
                UserManage um = new UserManage();
                um.ShowDialog();
            }
        }
    }
}
