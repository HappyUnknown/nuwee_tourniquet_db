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
using TourniqetDB.Classes;
using TourniqetDB.Classes.Contexts;

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
            try
            {
                //List<StudentAccount> accounts = new List<StudentAccount>()
                //{
                //    new StudentAccount(){Id=1,Email="danchenkov_ak20@gmail.com",Encoding="X2" },
                //    new StudentAccount(){Id=2,Email="demchuk_ak20@gmail.com",Encoding="X2" },
                //    new StudentAccount(){Id=3,Email="zhuranskyi_ak20@gmail.com",Encoding="X2" }
                //};
                var db = new StudentAccountContext();
                //foreach (var acc in accounts)
                //{
                //    db.StudentAccounts.Add(acc);
                //}
                db.SaveChanges();
                foreach (var sa in new StudentAccountContext().StudentAccounts)
                {
                    liAccounts.Items.Add(sa.ToString());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void liAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (liAccounts.SelectedIndex >= 0)//If list clearence hasn't been launched
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
        List<StudentAccount> GetUIAccounts()
        {
            List<StudentAccount> uiStudents = new List<StudentAccount>();
            string[] liUI = new string[liAccounts.Items.Count];
            for (int i = 0; i < liUI.Length; i++)
            {
                string[] args = liAccounts.Items[i].ToString().Split('-');
                for (int ai = 0; ai < args.Length; ai++)
                    args[ai] = args[ai].Trim(' ');
                int sid;
                int.TryParse(args[0], out sid);
                StudentAccount sa = new StudentAccount() { Id = sid, Email = args[1], Soil = args[2] };
                uiStudents.Add(sa);
            }
            return uiStudents;
        }
        bool SIDUnique(int id)
        {
            var uiAccounts = GetUIAccounts();
            for (int i = 0; i < uiAccounts.Count; i++)
                if (uiAccounts[i].Id == id)
                    return false;
            return true;
        }
        void ClearForm()
        {
            tbSEmail.Text = string.Empty;
            tbSEmailSoil.Text = string.Empty;
            tbSId.Text = string.Empty;
            lblHash.Content = string.Empty;
        }
        void RefreshStudAccList()
        {
            liAccounts.Items.Clear();
            foreach (var sa in new StudentAccountContext().StudentAccounts)
                liAccounts.Items.Add(sa.ToString());
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EmailValid(tbSEmail.Text))
                {
                    if (LocalEmail(tbSEmail.Text))
                    {

                        var db = new StudentAccountContext();
                        //int sid;
                        //int.TryParse(tbSId.Text, out sid);
                        //if (SIDUnique(sid))
                        {
                            var acc = new StudentAccount() { /*Id = sid, */Email = tbSEmail.Text, Soil = tbSEmailSoil.Text };
                            db.StudentAccounts.Add(acc);
                            db.SaveChanges();
                        }
                        //else MessageBox.Show($"User with StudentID-{sid} already exists");
                        ClearForm();
                    }
                    else MessageBox.Show("You are using foreign email");
                }
                else MessageBox.Show("Email is not formated correctly");
                RefreshStudAccList();
            }
            catch (Exception ex) { MessageBox.Show($"btnCreate_Click global error: {ex.Message}"); }
        }
        bool EmailValid(string email)
        {
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email);
            }
            catch { return false; }
            bool formatInvalid = !email.Contains('.') || email.StartsWith(".") || email.StartsWith("@") || email.EndsWith(".") || email.EndsWith("@");
            if (formatInvalid)
                return false;
            return true;
        }
        bool LocalEmail(string email)
        {
            if (email.EndsWith("@nuwm.edu.ua"))
                return true;
            return false;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EmailValid(tbSEmail.Text))
                {
                    if (LocalEmail(tbSEmail.Text))
                    {
                        if (MessageBox.Show($"Are you sure to update student data on SID-{GetUIAccounts()[liAccounts.SelectedIndex].Id}?", "UPDATE REQUESTED", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            var db = new StudentAccountContext();
                            var acc = db.StudentAccounts.ToList()[liAccounts.SelectedIndex];
                            acc.Email = tbSEmail.Text;
                            acc.Soil = tbSEmailSoil.Text;
                            db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        ClearForm();
                    }
                    else MessageBox.Show("You are using foreign email");
                }
                else MessageBox.Show("Email is not formated correctly");
                RefreshStudAccList();
            }
            catch (Exception ex) { MessageBox.Show($"btnUpdate_Click global error: {ex.Message}"); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Are you sure to delete student on SID-{GetUIAccounts()[liAccounts.SelectedIndex].Id}?", "DELETE REQUESTED", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var db = new StudentAccountContext();
                    db.StudentAccounts.Remove(db.StudentAccounts.ToList()[liAccounts.SelectedIndex]);
                    //db.StudentAccounts.Remove(db.StudentAccounts.Single(x => x.Id == 1));
                    db.SaveChanges();
                    ClearForm();
                }
                RefreshStudAccList();
            }
            catch (Exception ex) { MessageBox.Show($"btnDelete_Click global error: {ex.Message}"); }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                liAccounts.SelectedIndex = -1;
                ClearForm();
            }
            catch (Exception ex) { MessageBox.Show($"btnReset_Click global error: {ex.Message}"); }
        }
    }
}
