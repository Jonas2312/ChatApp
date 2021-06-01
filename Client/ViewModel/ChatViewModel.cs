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

        public ChatViewModel()
        {
            Messages = new ObservableCollection<ChatMessage>();

            LoadMessagesCommand = new RelayCommand(LoadMessages);
            WriteMessageCommand = new RelayCommand(WriteMessage);
            UploadFileCommand = new RelayCommand(UploadFile);
            DownloadFileCommand = new RelayCommand(DownloadFile);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LoadMessages(null);
        }

        private async void LoadMessages(object obj)
        {
            string loadedMessagesJSON = await ChatModel.LoadMessages();
            Messages = new ObservableCollection<ChatMessage>(JsonConvert.DeserializeObject<List<ChatMessage>>(loadedMessagesJSON));

        }

        private async void WriteMessage(object obj)
        {
            string content = MessageDraft;
            MessageDraft = String.Empty;

            ChatMessage message = new ServerSide.Models.ChatMessage(ChatModel.CurrentUser, content, false, " ");
            ChatModel.WriteMessage(message);

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
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);
                        ChatModel.SetFile(ms.ToArray(), openFileDialog.FileName);
                    }
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
                 ChatModel.LoadFile(saveFileDialog1.FileName, message);
            }
        }
    }
}
