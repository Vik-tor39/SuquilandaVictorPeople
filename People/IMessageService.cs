using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    internal interface IMessageService
    {
        Task ShowAsync(string message);
    }
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Aviso", message, "OK");
        }
    }
}
