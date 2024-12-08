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
using System.Windows;

namespace ElfeledettVarosokWPF
{
    public partial class RoomSelectionDialog : Window
    {
        public string SelectedRoom { get; private set; }

        public RoomSelectionDialog(IEnumerable<string> roomNames)
        {
            InitializeComponent();
            RoomComboBox.ItemsSource = roomNames;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedRoom = RoomComboBox.SelectedItem as string;
            DialogResult = true;
            Close();
        }
    }
}
