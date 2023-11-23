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
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Employee employee = new Employee
                {
                    FullName = txtFullName.Text,
                    Role = rolePicker.SelectedItem.ToString(), // Obtener el rol seleccionado del Picker
                    EmployeeNumber = Convert.ToInt32(txtEmployeeNumber.Text), // Convertir a int

                    Address = txtDirección.Text, // Nuevo campo
                    PhoneNumber = txtTeléfono.Text, // Nuevo campo
                    BirthDate = txtfechanacimineto.Date, // Nuevo campo
                    Curp = txtCurp.Text // Nuevo campo
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
        public bool ValidarDatos()
        {
            bool respuesta = true;
            // Validar Nombre
            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "El nombre es requerido", "OK");
            }
            // Validar Rol
            if (rolePicker.SelectedItem == null)
            {
                respuesta = false;
                DisplayAlert("Aviso", "Por favor selecciona un rol", "OK");
            }
            // Validar Número de Empleado
            if (string.IsNullOrEmpty(txtEmployeeNumber.Text) || !int.TryParse(txtEmployeeNumber.Text, out int employeeNumber) || employeeNumber.ToString().Length != 5)
            {
                respuesta = false;
                DisplayAlert("Aviso", "El número de empleado debe ser un número de 5 dígitos", "OK");
            }
            
           

            if (string.IsNullOrEmpty(txtDirección.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "La direccion es requerida", "OK");
            }
            if (string.IsNullOrEmpty(txtTeléfono.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "El telefono es requerida", "OK");
            }
            if (txtfechanacimineto.Date == null || txtfechanacimineto.Date == DateTime.MinValue)
            {
                respuesta = false;
                DisplayAlert("Aviso", "La fecha de nacimiento es requerida", "OK");
            }

            if (string.IsNullOrEmpty(txtCurp.Text))
            {
                respuesta = false;
                DisplayAlert("Aviso", "El CURP es requerida", "OK");
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
                    FullName = txtFullName.Text,
                    Role = rolePicker.SelectedItem.ToString(), // Obtener el rol seleccionado del Picker
                    EmployeeNumber = Convert.ToInt32(txtEmployeeNumber.Text), // Agregar número de empleado

                    Address = txtDirección.Text, // Nuevo campo
                    PhoneNumber = txtTeléfono.Text, // Nuevo campo
                    BirthDate = txtfechanacimineto.Date, // Nuevo campo
                    Curp = txtCurp.Text // Nuevo campo
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
                txtFullName.Text = obj.FullName;
                rolePicker.SelectedItem = obj.Role; // Establece el rol seleccionado en el Picker
                txtEmployeeNumber.Text = obj.EmployeeNumber.ToString(); // Establece el número de empleado

                txtDirección.Text = obj.Address;
                txtTeléfono.Text = obj.PhoneNumber;
                txtfechanacimineto.Date = obj.BirthDate;
                txtCurp.Text = obj.Curp;
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
            txtFullName.Text = "";
            rolePicker.SelectedIndex = -1; // Desseleccionar todos los elementos
            txtEmployeeNumber.Text = ""; // Limpiar campo de número de empleado

            txtDirección.Text = "";
            txtTeléfono.Text = "";
            txtfechanacimineto.Date = DateTime.Now; // Asignar un valor por defecto o nulo a la fecha de nacimiento
            txtCurp.Text = "";
        }
        private async void clicklogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}
