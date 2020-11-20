using EmailApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace EmailApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MailboxInfo _mailboxInfo = new MailboxInfo();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://post-shift.ru/api.php?action=new&type=json");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                _mailboxInfo = JsonConvert.DeserializeObject<MailboxInfo>(stream.ReadToEnd());
            }
            if (_mailboxInfo.Key == null)
            {
                MessageBox.Show("Error: On the server technical work!");
                _mailboxInfo = null;
            }
            else
                AddressLabel.Content += _mailboxInfo.Email;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mailboxInfo != null)
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"https://post-shift.ru/api.php?action=getlist&key={_mailboxInfo.Key}&type=json");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                {
                    LettersGrid.ItemsSource = JsonConvert.DeserializeObject<List<Letter>>(stream.ReadToEnd());
                }
            }
            else
            {
                MessageBox.Show("Создайте почту");
                LettersGrid.Items.Refresh();
            }
        }

        private void ReadLetterButton_Click(object sender, RoutedEventArgs e)
        {
            if (LettersGrid.SelectedItems.Count > 0)
            {
                Letter letter = (Letter)LettersGrid.SelectedItems[0];

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"https://post-shift.ru/api.php?action=getmail&key={_mailboxInfo.Key}&id={letter.ID}&type=json");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                {
                    MessageBox.Show(stream.ReadToEnd());
                }
            }
            else
            {
                MessageBox.Show("Что прочитать?");
            }
        }

        private void LiveTimeUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mailboxInfo != null)
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"https://post-shift.ru/api.php?action=livetime&key={_mailboxInfo.Key}");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                {
                    LiveTimeLabel.Content = $"Почте осталось: {stream.ReadToEnd()} секунд";
                }
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void ProlongEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mailboxInfo != null)
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"https://post-shift.ru/api.php?action=update&key={_mailboxInfo.Key}");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                {
                    LiveTimeLabel.Content = $"Почте осталось: {stream.ReadToEnd()} секунд";
                }
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void DeleteEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mailboxInfo != null)
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"https://post-shift.ru/api.php?action=clear&key={_mailboxInfo.Key}");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using (StreamReader stream = new StreamReader(
                     resp.GetResponseStream(), Encoding.UTF8))
                {
                    MessageBox.Show(stream.ReadToEnd());
                }
                Clear();
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void DeleteAllEmailByIPButton_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"https://post-shift.ru/api.php?action=deleteall");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                MessageBox.Show(stream.ReadToEnd());
            }
            Clear();
        }

        private void Clear()
        {
            AddressLabel.Content = "Адрес почты: ";
            _mailboxInfo = null;
        }
    }
}
