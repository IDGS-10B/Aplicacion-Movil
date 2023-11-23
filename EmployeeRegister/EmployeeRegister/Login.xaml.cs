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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        // C#
        private void OnEmployeeNumberChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 5) // Cambia este valor según tus requisitos
            {
                txtPassword.Focus(); // Cambiar el enfoque al campo de contraseña
            }
        }
        private void OnPasswordCompleted(object sender, EventArgs e)
        {
            btnLogin_Clicked(sender, e); // Llama al evento de clic del botón "Iniciar Sesión"
        }


        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            string employeeNumber = txtEmployeeNumber.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(employeeNumber) && string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor ingresa el número de empleado y la contraseña.", "OK");
                return; // Sale del evento sin continuar
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor ingresa la contraseña.", "OK");
                return; // Sale del evento sin continuar
            }
            else if (string.IsNullOrWhiteSpace(employeeNumber))
            {
                await DisplayAlert("Error", "Por favor ingresa el número de empleado.", "OK");
                return; // Sale del evento sin continuar
            }

            // Verificar las credenciales
            bool isValid = await ValidateCredentials(employeeNumber, password);

            if (isValid)
            {
                // Determinar si es administrador o empleado
                Employee user = await App.SQLiteDB.GetEmployeeByEmployeeNumberAsync(int.Parse(employeeNumber));

                if (user.Role == "Administrador")
                {
                    // Navegar a la página de administrador
                    await Navigation.PushAsync(new Administrador(user));
                }
                else
                {
                    // Navegar a la página de empleado
                    await Navigation.PushAsync(new Empleado(user));  // Pasa el objeto Employee como argumento
                }
            }
            else
            {
                // Mensaje de error personalizado si las credenciales son inválidas
                await DisplayAlert("Error", "Usuario no encontrado o contraseña no encontrado", "OK");
            }
        }

        private async Task<bool> ValidateCredentials(string employeeNumber, string password)
        {
            int employeeNumberInt = int.Parse(employeeNumber);
            Employee user = await App.SQLiteDB.GetEmployeeByEmployeeNumberAsync(employeeNumberInt);

            return (user != null && user.Curp == password);
        }
    }
}