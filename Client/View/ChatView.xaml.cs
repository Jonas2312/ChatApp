using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class ChatView : System.Windows.Controls.UserControl
    {
        ChatViewModel _viewModel;

        public ChatView()
        {
            _viewModel = new ChatViewModel();
            this.DataContext = _viewModel;

            InitializeComponent();

        }

    }
}
