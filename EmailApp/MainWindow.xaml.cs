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
        private EmailInfo _mailboxInfo;
        PostShiftQueryHelper postShiftQueryHelper = new PostShiftQueryHelper();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            _mailboxInfo = postShiftQueryHelper.GetNewMailInfo();
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
                LettersGrid.ItemsSource = postShiftQueryHelper.GetListLetters(_mailboxInfo.Key);
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
                MessageBox.Show(postShiftQueryHelper.GetLetterText(_mailboxInfo.Key, letter.Id));
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
                LiveTimeLabel.Content = $"Почте осталось: {postShiftQueryHelper.GetEmailLiveTime(_mailboxInfo.Key)} секунд";
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
                LiveTimeLabel.Content = $"Почте осталось: {postShiftQueryHelper.ProlongEmailLiveTime(_mailboxInfo.Key)} секунд";
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
                MessageBox.Show(postShiftQueryHelper.DeleteEmail(_mailboxInfo.Key));
                Clear();
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void DeleteAllEmailByIPButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(postShiftQueryHelper.DeleteAllEmailByIP());
            Clear();
        }

        private void Clear()
        {
            AddressLabel.Content = "Адрес почты: ";
            _mailboxInfo = null;
        }
    }
}
