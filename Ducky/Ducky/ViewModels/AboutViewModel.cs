using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Ducky
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Tietoja";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://portville.azurewebsites.net")));
        }

        public ICommand OpenWebCommand { get; }
    }
}