using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeRegister
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Administrador : ContentPage
    {
        public Administrador()
        {
            InitializeComponent();
        }

        private async void clicklogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void clickcurso(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new altaCurso());
        }
    }
}