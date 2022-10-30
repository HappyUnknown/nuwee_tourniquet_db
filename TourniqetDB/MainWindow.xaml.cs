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

namespace TourniqetDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            liAccounts.Items.Add("1 - danchenkov_ak20@gmail.com - X2");
            liAccounts.Items.Add("2 - demchuk_ak20@gmail.com - X2");
            liAccounts.Items.Add("3 - zhuranskyi_ak20@gmail.com - X2");
        }

        private void liAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] args = liAccounts.Items[liAccounts.SelectedIndex].ToString().Split('-');
            for (int i = 0; i < args.Length; i++)
                args[i] = args[i].Trim(' ');
            tbSId.Text = args[0];
            tbSEmail.Text = args[1];
            tbSEmailSoil.Text = args[2];
            lblHash.Content = Classes.MD5Hasher.Encrypt(tbSEmail.Text, tbSEmailSoil.Text);
        }
    }
}
