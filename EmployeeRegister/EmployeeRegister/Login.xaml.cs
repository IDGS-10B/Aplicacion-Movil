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

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            string employeeNumber = txtEmployeeNumber.Text;
            string password = txtPassword.Text;

            // Verificar las credenciales
            bool isValid = await ValidateCredentials(employeeNumber, password);

            if (isValid)
            {
                // Determinar si es administrador o empleado
                Employee user = await App.SQLiteDB.GetEmployeeByEmployeeNumberAsync(int.Parse(employeeNumber));

                if (user.Role == "Administrador")
                {
                    // Navegar a la página de administrador
                    await Navigation.PushAsync(new Administrador());
                }
                else
                {
                    // Navegar a la página de empleado
                    await Navigation.PushAsync(new Empleado());
                }
            }
            else
            {
                await DisplayAlert("Error", "Credenciales inválidas", "OK");
            }
        }


        private async Task<bool> ValidateCredentials(string employeeNumber, string password)
        {
            int employeeNumberInt = int.Parse(employeeNumber);
            Employee user = await App.SQLiteDB.GetEmployeeByEmployeeNumberAsync(employeeNumberInt);

            return (user != null && user.Password == password);
        }

    }
}