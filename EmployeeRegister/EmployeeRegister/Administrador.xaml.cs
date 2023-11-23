using EmployeeRegister.Models;
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
        private bool isLoggedOut = false;
        private Employee employee;  // Declarar la variable employee

        public Administrador(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;  // Asignar el valor recibido al campo de clase

            // Muestra el mensaje de bienvenida con el nombre del empleado
            lblWelcome.Text = $"Bienvenido {this.employee.FullName}";

            // Deshabilitar el botón de retroceso en la barra de navegación
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void clicklogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void clickcurso(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new altaCurso());
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            bool confirmLogout = await DisplayAlert("Cerrar Sesión", "¿Estás seguro de que deseas cerrar sesión?", "Sí", "No");

            if (confirmLogout)
            {
                // Eliminar cualquier información de sesión actual
                App.Current.Properties.Remove("EmployeeNumber");
                isLoggedOut = true;

                // Cambiar la página principal de la aplicación a la página de inicio de sesión
                Application.Current.MainPage = new NavigationPage(new Login());
            }
        }
        //OIPA021105HNLRRLA6
        protected override bool OnBackButtonPressed()
        {
            // Siempre devuelve true para evitar que el botón de retroceso del dispositivo tenga efecto
            return true;
        }

    }
}