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
        private ListViewItem _listViewItem;
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 20; ++i)
            {
                _listViewItem = new ListViewItem();
                _listViewItem.Content = "Christian" + i;
                _listViewItem.Tag = "Lorilla";
                lvContacts.Items.Add(_listViewItem);
            }
        }
    }
}