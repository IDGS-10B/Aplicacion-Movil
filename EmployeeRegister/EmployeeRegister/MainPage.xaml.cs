using EmployeeRegister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmployeeRegister
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }
       



        /*private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Employee employee = new Employee
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    MotherLastName = txtMotherLastName.Text,

                };

                await App.SQLiteDB.SaveEmployeeAsync(employee);


              
                CleanControlls();
                llenarDatos();

                await DisplayAlert("Registro", "Se guardo de manera existosa", "OK");
            }
            else
            {
                await DisplayAlert("Aviso", "Ingresa todos los datos", "OK");
            }
        }*/

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Employee employee = new Employee
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    MotherLastName = txtMotherLastName.Text,
                    Role = rolePicker.SelectedItem.ToString(), // Obtener el rol seleccionado del Picker
                    EmployeeNumber = Convert.ToInt32(txtEmployeeNumber.Text), // Convertir a int
                    Password = txtPassword.Text
                };

                await App.SQLiteDB.SaveEmployeeAsync(employee);

                CleanControlls();
                llenarDatos();

                await DisplayAlert("Registro", "Se guardó de manera exitosa", "OK");
            }
            else
            {
                await DisplayAlert("Aviso", "Ingresa todos los datos", "OK");
            }
        }





        public async void llenarDatos()
        {
            var EmployeeList = await App.SQLiteDB.GetEmployeesAsync();
            if (EmployeeList != null)
            {
                lstEmployee.ItemsSource = EmployeeList;
            }
        }




        /*public bool ValidarDatos()
        {
          bool respuesta;
        if (string.IsNullOrEmpty(txtName.Text))
        {
          respuesta = false;
        }
        else if (string.IsNullOrEmpty(txtLastName.Text))
          {
          respuesta = false;
        }
        else if (string.IsNullOrEmpty(txtMotherLastName.Text))
        {
          respuesta = false;
        }
        else
        {
          respuesta = true;
        }
        return respuesta;
        }*/

        public bool ValidarDatos()
        {
            bool respuesta = true;

            // Validar Nombre
            if (string.IsNullOrEmpty(txtName.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "El nombre es requerido", "OK");
            }

            // Validar Apellido Paterno
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "El apellido paterno es requerido", "OK");
            }

            // Validar Apellido Materno
            if (string.IsNullOrEmpty(txtMotherLastName.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "El apellido materno es requerido", "OK");
            }

            // Validar Rol
            if (rolePicker.SelectedItem == null)
            {
                respuesta = false;
                DisplayAlert("Aviso", "Por favor selecciona un rol", "OK");
            }

            // Validar Número de Empleado
            if (string.IsNullOrEmpty(txtEmployeeNumber.Text) || !int.TryParse(txtEmployeeNumber.Text, out int employeeNumber) || employeeNumber.ToString().Length != 8)
            {
                respuesta = false;
                DisplayAlert("Aviso", "El número de empleado debe ser un número de 8 dígitos", "OK");
            }

            // Validar Contraseña
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "La contraseña es requerida", "OK");
            }

            return respuesta;
        }


        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmployee.Text))
            {
                Employee employee = new Employee()
                {
                    IdEmpl = Convert.ToInt32(txtIdEmployee.Text),
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    MotherLastName = txtMotherLastName.Text,
                    Role = rolePicker.SelectedItem.ToString(), // Obtener el rol seleccionado del Picker
                    EmployeeNumber = Convert.ToInt32(txtEmployeeNumber.Text), // Agregar número de empleado
                    Password = txtPassword.Text // Agregar contraseña

                };

                await App.SQLiteDB.SaveEmployeeAsync(employee);
                await DisplayAlert("Registro", "Se actualizo de manera existosa", "OK");

                CleanControlls();
                txtIdEmployee.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegister.IsVisible = true;
                llenarDatos();
            }
        }


        private async void lstEmployee_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Employee)e.SelectedItem;

            if (obj != null) // Verifica que el objeto seleccionado no sea nulo
            {
                btnRegister.IsVisible = false;
                txtIdEmployee.IsVisible = true;
                btnActualizar.IsVisible = true;
                btnEliminar.IsVisible = true;

                txtIdEmployee.Text = obj.IdEmpl.ToString();
                txtName.Text = obj.Name;
                txtLastName.Text = obj.LastName;
                txtMotherLastName.Text = obj.MotherLastName;

                rolePicker.SelectedItem = obj.Role; // Establece el rol seleccionado en el Picker

                txtEmployeeNumber.Text = obj.EmployeeNumber.ToString(); // Establece el número de empleado
                txtPassword.Text = obj.Password; // Establece la contraseña
            }
        }



        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var employee = await App.SQLiteDB.GetEmployeeByIdAsync(Convert.ToInt32(txtIdEmployee.Text));

            if (employee != null)
            {
                await App.SQLiteDB.DeleteEmployee(employee);
                await DisplayAlert("Empleado", "Se elimino de manera existosa", "OK");
                CleanControlls();
                llenarDatos();

                txtIdEmployee.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegister.IsVisible = true;
            }

        }

        private void CleanControlls()
        {
            txtIdEmployee.Text = "";
            txtName.Text = "";
            txtLastName.Text = "";
            txtMotherLastName.Text = "";
            rolePicker.SelectedIndex = -1; // Desseleccionar todos los elementos
            txtEmployeeNumber.Text = ""; // Limpiar campo de número de empleado
            txtPassword.Text = ""; // Limpiar campo de contraseña
        }

        private async void clicklogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

    }
}
