using Client.Model;
using Client.Others;
using Newtonsoft.Json;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get
            {
                return isLoggedIn;
            }
            set
            {
                isLoggedIn = value;
                OnPropertyChanged("IsLoggedIn");
                OnPropertyChanged("IsLoggedOff");
            }
        }

        public bool IsLoggedOff
        {
            get
            {
                return !isLoggedIn;
            }
        }


        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string statusMessage;
        public string StatusMessage
        {
            get
            {
                return statusMessage;
            }
            set
            {
                statusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }


        public ICommand LogInCommand { get; }
        public ICommand RegisterCommand { get; }


        public IDataTransferModel DataTransferModel;

        
        public MainWindowViewModel(bool isLoggedIn) : this(Globals.dataTransferModel, isLoggedIn)
        {
        }

        public MainWindowViewModel(IDataTransferModel dataTransferModel, bool isLoggedIn)
        {
            DataTransferModel = dataTransferModel;
            IsLoggedIn = isLoggedIn;

            LogInCommand = new RelayCommand(LogIn);
            RegisterCommand = new RelayCommand(Register);
        }


        private async void LogIn(object obj)
        {
            string logInName = (string)obj;
            List<User> users = await DataTransferModel.LoadData<List<User>>(Globals.Url + "/api/Users");

            foreach (User u in users)
            {
                if (logInName.Equals(u.Name))
                {
                    Console.WriteLine("Logged in as user " + u.Name);
                    Globals.CurrentUser = u;
                    IsLoggedIn = true;
                    return;
                }
            }
            StatusMessage = "Could not find user";
            Console.WriteLine("Could not find user");
        }

        private async void Register(object obj)
        {
            string logInName = (string)obj;
            User user = new User(logInName, "password");

            string serverResponse = await DataTransferModel.SendData<User>(user, Globals.Url + "/api/Users");
            StatusMessage = serverResponse;
        }
    }
}
