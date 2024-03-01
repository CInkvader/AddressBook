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
        private TextBox[] _tbxEntries = new TextBox[] { };

        private bool _editMode = false;
        private bool _addMode = false;

        public MainWindow()
        {
            InitializeComponent();

            _tbxEntries = new TextBox[] { tbxFirstName, tbxLastName, tbxPhone, tbxEmail, tbxAddress, tbxNotes };

            SortContacts();
            FindSearchResult();
        }

        private void ShowEditFields(string banner)
        {
            stpProfileDetails.Visibility = Visibility.Collapsed;
            tbNoneSelectedMessage.Visibility = Visibility.Collapsed;
            tbProfileDisplayName.Visibility = Visibility.Collapsed;
            btnProfileEdit.Visibility = Visibility.Hidden;
            btnAddContact.Visibility = Visibility.Collapsed;

            tbProfileEditBanner.Text = banner;
            stpProfileTools.Visibility = Visibility.Visible;
            stpProfileIcon.Visibility = Visibility.Visible;
            stpProfileEdit.Visibility = Visibility.Visible;
            tbProfileEditBanner.Visibility = Visibility.Visible;
            btnProfileEditBack.Visibility = Visibility.Visible;
            LeftControlBlock.Visibility = Visibility.Visible;

            CheckEntriesComplete();
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
        }
        private void SetContactEditMode()
        {
            ShowEditFields("Edit Contact");

            _editMode = true;
            tbSearchBar.IsEnabled = false;
            lvContacts.IsEnabled = false;
        }
        private void SetContactAddMode()
        {
            ShowEditFields("Add Contact");

            _addMode = true;
            tbSearchBar.IsEnabled = false;
            lvContacts.IsEnabled = false;
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
        }
        private void SetContactNoResult()
        {
            lvContacts.Items.Clear();
            tbNoResultMessage.Visibility = Visibility.Visible;
        }
        
        private void ShowBackScreen(bool show)
        {
            if (show)
            {
                stpConfirmScreen.Visibility = Visibility.Visible;

                tbConfirmScreen.Text = "You have unsaved changes! Discard changes and continue?";
                btnConfirm.Content = "Discard Changes";

                SetEditBtnEnabled(false);
                return;
            }
            stpConfirmScreen.Visibility = Visibility.Collapsed;
            SetEditBtnEnabled(true);
        }
        private void ShowConfirmScreen(bool show)
        {
            if (show)
            {
                stpConfirmScreen.Visibility = Visibility.Visible;

                tbConfirmScreen.Text = "Are you sure the contact information is correct?";
                btnConfirm.Content = "Yes";

                SetEditBtnEnabled(false);
                return;
            }
            stpConfirmScreen.Visibility = Visibility.Collapsed;
            SetEditBtnEnabled(true);
        }
        private void SetEditBtnEnabled(bool enabled)
        {
            foreach (TextBox entry in _tbxEntries)
            {
                entry.IsEnabled = enabled;
            }
            btnProfileEditBack.IsEnabled = enabled;
            CheckEntriesComplete();
        }

        private void FindSearchResult()
        {
            int resultCount = 0;
            lvContacts.Items.Clear();
            
            foreach (ContactInformation contact in _contacts)
            {
                string fullname = contact.FirstName + " " + contact.LastName;

                if (fullname.Contains(tbSearchBar.Text, StringComparison.OrdinalIgnoreCase))
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
        private void SortContacts()
        {
            _contacts = _contacts.OrderBy(contact => contact.FirstName).ToList();
        }
        private void tbSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindSearchResult();
        }
        private void StopProfileManage()
        {
            tbSearchBar.IsEnabled = true;
            lvContacts.IsEnabled = true;
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
            ShowBackScreen(true);
        }
        private void btnProfileEditSave_Click(object sender, RoutedEventArgs e)
        {
            ShowConfirmScreen(true);
        }
        #endregion

        private void CheckEntriesComplete()
        {
            int entryCount = 0;
            for (int i = 0; i < _tbxEntries.Count() - 1; ++i)
            {
                if (_tbxEntries[i].Text.Length != 0)
                {
                    ++entryCount;
                }
            }
            if (entryCount == 5)
            {
                btnProfileEditSave.IsEnabled = true;
                return;
            }
            btnProfileEditSave.IsEnabled = false;
        }
        private void ClearEntries()
        {
            foreach (TextBox entry in _tbxEntries)
            {
                entry.Clear();
            }
        }
        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            tbSearchBar.Clear();
            SetContactAddMode();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ShowConfirmScreen(false);
        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ShowConfirmScreen(false);
            StopProfileManage();

            if (_addMode)
            {
                lvContacts.UnselectAll();
                SetContactNoneSelected();
            }
            _editMode = false;
            _addMode = false;
        }

        #region TextEntries
        private void tbxFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEntriesComplete();
        }
        private void tbxLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEntriesComplete();
        }
        private void tbxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEntriesComplete();
        }
        private void tbxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEntriesComplete();
        }
        private void tbxAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEntriesComplete();
        }
        private void tbxNotes_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEntriesComplete();
        } 
        #endregion

        private class ContactInformation
        {
            private string _identification = string.Empty;
            private string _firstName = string.Empty;
            private string _lastName = string.Empty;
            private string _phone = string.Empty;
            private string _email = string.Empty;
            private string _address = string.Empty;
            private string _notes = string.Empty;

            public string Identification { get { return _identification; } set { _identification = value; } }
            public string FirstName { get { return _firstName; } set { _firstName = value; } }
            public string LastName { get { return _lastName; } set { _lastName = value; } }
            public string Phone { get { return _phone; } set { _phone = value; } }
            public string Email { get { return _email; } set { _email = value; } }
            public string Address { get { return _address; } set { _address = value; } }
            public string Notes { get { return _notes; } set { _notes = value; } }
        }
    }
}