using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Model;
using System.Windows.Forms;
using Client.Others;
using Client.Model.SignalR;

namespace Client.ViewModel
{
    class ChatViewModel : ViewModelBase
    {
        private DispatcherTimer dispatcherTimer;
        
        private ObservableCollection<ChatMessage> messages;
        public ObservableCollection<ChatMessage> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                OnPropertyChanged("Messages");
            }
        }

        private string messageDraft;
        public string MessageDraft
        {
            get
            {
                return messageDraft;
            }
            set
            {
                messageDraft = value;
                OnPropertyChanged("MessageDraft");
            }
        }


        public ICommand LoadMessagesCommand { get; }
        public ICommand WriteMessageCommand { get; }
        public ICommand UploadFileCommand { get; }
        public ICommand DownloadFileCommand { get; }


        public IDataTransferModel DataTransferModel;
        public IFileTransferModel FileTransferModel;


        public ChatViewModel() : this(Globals.dataTransferModel, Globals.fileTransferModel)
        {
        }

        public ChatViewModel(IDataTransferModel dataTransferModel, IFileTransferModel fileTransferModel)
        {
            DataTransferModel = dataTransferModel;
            FileTransferModel = fileTransferModel;

            Messages = new ObservableCollection<ChatMessage>();

            LoadMessagesCommand = new RelayCommand(LoadMessages);
            WriteMessageCommand = new RelayCommand(WriteMessage);
            UploadFileCommand = new RelayCommand(UploadFile);
            DownloadFileCommand = new RelayCommand(DownloadFile);

            // For periodic events
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // REST API has to ask server periodically if new messages have arrived
            // Other solutions like using SignalR don't
            if (DataTransferModel is RESTTransferModel)
            {
                LoadMessages(null);
            }
            else if (DataTransferModel is SignalRTransferModel)
            {
                
            }
        }

        private async void LoadMessages(object obj)
        {
            List<ChatMessage> MessageList = await DataTransferModel.LoadData<List<ChatMessage>>(Globals.Url + "/api/Messages");
            if (MessageList == null)
                return;

            // Currently we assume two lists of messages are the same if they contain the same amount of objects
            // TODO: Check if lists are really the same 
            if(MessageList.Count != Messages.Count)
                Messages = new ObservableCollection<ChatMessage>(MessageList);            
        }

        private async void WriteMessage(object obj)
        {
            string content = MessageDraft;
            MessageDraft = String.Empty;

            ChatMessage message = new ServerSide.Models.ChatMessage(Globals.CurrentUser, content, false, " ");
            DataTransferModel.SendData<ChatMessage>(message, Globals.Url + "/api/Messages");

            LoadMessages(null);
        }

        private async void UploadFile(object obj)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    // OpenFileDialog.SafeFileName gets the file name and extension for the file selected in
                    // the dialog box. The file name does not include the path.

                    filePath = openFileDialog.FileName;                   
                    FileTransferModel.UploadFile(filePath, openFileDialog.SafeFileName, Globals.Url);
                }
            }
        }

        private async void DownloadFile(object obj)
        {
            ChatMessage message = (ChatMessage)obj;

            Stream myStream;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                 FileTransferModel.DownloadFile(saveFileDialog1.FileName, message.FileID, Globals.Url);
            }
        }
    }
}
