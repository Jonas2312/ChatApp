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

        public ICommand LogInCommand { get; }
        public ICommand RegisterCommand { get; }

        public MainWindowViewModel()
        {
            IsLoggedIn = false;
            LogInCommand = new RelayCommand(LogIn);
            RegisterCommand = new RelayCommand(Register);
        }


        private async void LogIn(object obj)
        {
            string name = (string)obj;

            HttpClient client = new HttpClient();
            var v = await client.GetAsync("https://localhost:44339/api/Users");
            var w = await v.Content.ReadAsStringAsync();

            string loadedUsersJSON = w;
            List<User> users = JsonConvert.DeserializeObject<List<User>>(loadedUsersJSON);

            foreach (User u in users)
            {
                if (name.Equals(u.Name))
                {
                    Console.WriteLine("Logged in as user " + u.Name);
                    ChatModel.CurrentUser = u;
                    IsLoggedIn = true;
                    return;
                }
            }
            Console.WriteLine("Could not find user");
        }

        private async void Register(object obj)
        {
            string name = (string)obj;
            User user = new User(name, "password");

            string s = JsonConvert.SerializeObject(user);
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync("https://localhost:44339/api/Users", stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
