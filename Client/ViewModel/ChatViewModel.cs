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
        
        public ObservableCollection<ChatMessage> Messages;
        public ICommand LoadMessagesCommand;
        public ICommand WriteMessageCommand;
        public ICommand UploadFileCommand;
        public ICommand DownloadFileCommand;

        public User CurrentUser;

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

        public ChatViewModel()
        {
            LoadMessagesCommand = new RelayCommand(loadMessages);
            WriteMessageCommand = new RelayCommand(writeMessage);
            UploadFileCommand = new RelayCommand(uploadFile);
            DownloadFileCommand = new RelayCommand(downloadFile);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            loadMessages(null);
        }

        private void loadMessages(object obj)
        {
            string loadedMessagesJSON = ChatModel.LoadMessages().Result;
            Messages = new ObservableCollection<ChatMessage>(JsonConvert.DeserializeObject<List<ChatMessage>>(loadedMessagesJSON));
        }

        private void writeMessage(object obj)
        {
            string content = MessageDraft;
            MessageDraft = String.Empty;

            ChatMessage message = new ServerSide.Models.ChatMessage(CurrentUser, content);
            ChatModel.WriteMessage(message);

            loadMessages(null);
        }

        private void uploadFile(object obj)
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

        private void downloadFile(object obj)
        {
            Stream myStream;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                 ChatModel.LoadFile(saveFileDialog1.FileName);
            }
        }
    }
}
