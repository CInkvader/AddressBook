using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddressBook
{
    public partial class MainWindow : Window
    {
        
        private List<ContactInformation> _contacts = new List<ContactInformation>();

        private bool _editMode = false;
        private bool _addMode = false;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 20; ++i)
            {
                ContactInformation contactInformation = new ContactInformation();
                contactInformation.FirstName = "Christian Arthur";
                contactInformation.LastName = "Lorilla";

                _contacts.Add(contactInformation);
            }
        }

        private void SetContactViewMode()
        {
            stpProfileEdit.Visibility = Visibility.Collapsed;
            tbNoneSelectedMessage.Visibility = Visibility.Collapsed;
            btnProfileEditBack.Visibility = Visibility.Hidden;
            tbProfileEditBanner.Visibility = Visibility.Hidden;
            LeftControlBlock.Visibility = Visibility.Collapsed;

            stpProfileTools.Visibility = Visibility.Visible;
            stpProfileDetails.Visibility = Visibility.Visible;
            stpProfileIcon.Visibility = Visibility.Visible;
            tbProfileDisplayName.Visibility = Visibility.Visible;
            btnProfileEdit.Visibility = Visibility.Visible;
            btnAddContact.Visibility = Visibility.Visible;

            _editMode = false;
            _addMode = false;
        }
        private void SetContactEditMode()
        {
            stpProfileDetails.Visibility = Visibility.Collapsed;
            tbNoneSelectedMessage.Visibility = Visibility.Collapsed;
            tbProfileDisplayName.Visibility = Visibility.Collapsed;
            btnProfileEdit.Visibility = Visibility.Hidden;
            btnAddContact.Visibility = Visibility.Collapsed;

            stpProfileTools.Visibility = Visibility.Visible;
            stpProfileIcon.Visibility = Visibility.Visible;
            stpProfileEdit.Visibility = Visibility.Visible;
            tbProfileEditBanner.Text = "Edit Contact";
            tbProfileEditBanner.Visibility = Visibility.Visible;
            btnProfileEditBack.Visibility = Visibility.Visible;
            LeftControlBlock.Visibility = Visibility.Visible;

            _editMode = true;
            tbSearchBar.IsEnabled = false;
        }
        private void SetContactAddMode()
        {
            tbSearchBar.Clear();
            SetContactNoResult();
            tbNoneSelectedMessage.Visibility = Visibility.Collapsed;
            btnProfileEdit.Visibility = Visibility.Hidden;
            tbProfileDisplayName.Visibility = Visibility.Collapsed;
            stpProfileDetails.Visibility = Visibility.Collapsed;
            btnAddContact.Visibility = Visibility.Collapsed;

            tbProfileEditBanner.Text = "Add Contact";
            tbProfileEditBanner.Visibility = Visibility.Visible;
            stpProfileEdit.Visibility = Visibility.Visible;
            btnProfileEditBack.Visibility = Visibility.Visible;
            stpProfileTools.Visibility = Visibility.Visible;
            stpProfileIcon.Visibility = Visibility.Visible;
            LeftControlBlock.Visibility = Visibility.Visible;

            _addMode = true;
            tbSearchBar.IsEnabled = false;
        }
        private void SetContactNoneSelected()
        {
            stpProfileIcon.Visibility = Visibility.Collapsed;
            stpProfileDetails.Visibility = Visibility.Collapsed;
            stpProfileTools.Visibility = Visibility.Collapsed;
            stpProfileEdit.Visibility = Visibility.Collapsed;
            tbProfileDisplayName.Visibility = Visibility.Collapsed;
            LeftControlBlock.Visibility = Visibility.Collapsed;

            btnAddContact.Visibility = Visibility.Visible;
            tbNoneSelectedMessage.Visibility = Visibility.Visible;

            _addMode = false;
            _editMode = false;
        }
        private void SetContactNoResult()
        {
            lvContacts.Items.Clear();
            tbNoResultMessage.Visibility = Visibility.Visible;
        }
        private void tbSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            int resultCount = 0;
            foreach (ContactInformation contact in _contacts)
            {
                string fullname = contact.FirstName + " " + contact.LastName;
                if (fullname.Contains(tbSearchBar.Text))
                {
                    ListViewItem listContact = new ListViewItem();

                    listContact = new ListViewItem();
                    listContact.Content = fullname;
                    listContact.Tag = fullname;
                    lvContacts.Items.Add(listContact);

                    resultCount++;
                }
            }

            if (resultCount == 0)
            {
                SetContactNoResult();
            }
            else
            {
                tbNoResultMessage.Visibility = Visibility.Hidden;
            }
        }
        private void StopProfileManage()
        {
            tbSearchBar.IsEnabled = true;
            ClearEntries();
            if (lvContacts.SelectedIndex == -1)
            {
                SetContactNoneSelected();
                return;
            }
            SetContactViewMode();
        }

        #region ProfileTools
        private void lvContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetContactViewMode();
        }
        private void btnProfileEdit_Click(object sender, RoutedEventArgs e)
        {
            SetContactEditMode();
        }
        private void btnProfileEditBack_Click(object sender, RoutedEventArgs e)
        {
            StopProfileManage();
        }
        private void btnProfileEditSave_Click(object sender, RoutedEventArgs e)
        {
            StopProfileManage();
        }
        #endregion

        private void ClearEntries()
        {
            tbxFirstName.Clear();
            tbxLastName.Clear();
            tbxPhone.Clear();
            tbxEmail.Clear();
            tbxAddress.Clear();
            tbxNotes.Clear();
        }
        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            SetContactAddMode();
        }

        private class ContactInformation
        {
            string _firstName = string.Empty;
            string _lastName = string.Empty;
            string _phone = string.Empty;
            string _email = string.Empty;
            string _address = string.Empty;
            string _notes = string.Empty;

            public string FirstName { get { return _firstName; } set { _firstName = value; } }
            public string LastName { get { return _lastName; } set { _lastName = value; } }
            public string Phone { get { return _phone; } set { _phone = value; } }
            public string Email { get { return _email;} set { _email = value; } }
            public string Address { get { return _address; } set { _address = value; } }
            public string Notes { get { return _notes; } set { _notes = value; } }
        }
    }
}