using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using People.Models;
using System.Runtime.CompilerServices;

namespace People
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _people;
        private string _inputName;
        private readonly IMessageService _messageService;
        public ObservableCollection<Person> PeopleList
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        public string InputName
        {
            get => _inputName;
            set
            {
                _inputName = value;
                OnPropertyChanged();
            }
        }
        public string StatusMessage => App.PersonRepo.StatusMessage;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainPageViewModel()
        {
            _messageService = new MessageService();
            _people = new ObservableCollection<Person>(App.PersonRepo.GetAllPeople());
            AddCommand = new Command(AddPerson);
            DeleteCommand = new Command<Person>(DeletePerson);
        }
        private void AddPerson()
        {
            if (!string.IsNullOrWhiteSpace(InputName))
            {
                App.PersonRepo.AddNewPerson(InputName);
                InputName = string.Empty;
                RefreshPeople();
            }
        }
        private async void DeletePerson(Person person)
        {
            if (person != null)
            {
                await _messageService.ShowAsync($"Victor Suquilanda acaba de eliminar un registro.");
                App.PersonRepo.DeletePerson(person.Id);
                PeopleList.Remove(person);
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        private void RefreshPeople()
        {
            PeopleList.Clear();
            foreach (var person in App.PersonRepo.GetAllPeople())
            {
                PeopleList.Add(person);
            }
            OnPropertyChanged(nameof(StatusMessage));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
