using EmployeeRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using System.IO;



namespace EmployeeRegister
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empleado : ContentPage
    {
        private Curso cursoSeleccionado;
        private Employee employee;  // Declarar la variable employee
        private bool isLoggedOut = false;

        public Empleado(Employee employee)
        {
            InitializeComponent();
            LlenarListaCursos();
            this.employee = employee;  // Asignar el valor recibido al campo de clase

            // Muestra el mensaje de bienvenida con el nombre del empleado
            lblWelcome.Text = $"Bienvenido {this.employee.FullName}";

            // Deshabilitar el botón de retroceso en la barra de navegación
            NavigationPage.SetHasBackButton(this, false);
        }


        private async void lstCursos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null && e.SelectedItem is Curso curso)
            {
                cursoSeleccionado = curso;
            }
        }

        private void LlenarListaCursos()
        {
            // Obtener la lista de cursos disponibles desde tu base de datos
            List<Curso> cursos = ObtenerCursosDesdeBD(); // Implementa esta función

            // Asignar la lista de cursos al ListView
            lstCursos.ItemsSource = cursos;
        }

        private List<Curso> ObtenerCursosDesdeBD()
        {
            // Usar tu instancia de SQLiteHelper para obtener los cursos
            List<Curso> cursos = App.SQLiteDB.GetCursosAsync().Result;

            return cursos;
        }

        


        private async void OnReproducirClicked(object sender, EventArgs e)
        {
            Curso cursoSeleccionado = (Curso)lstCursos.SelectedItem;

            if (cursoSeleccionado != null)
            {
                // Aquí deberías implementar la lógica para reproducir el video
                await ReproducirVideo(cursoSeleccionado.FilePath); // Implementa esta función

            }
            else
            {
                await DisplayAlert("Aviso", "Selecciona un curso para reproducir.", "OK");
            }
        }

        private async Task ReproducirVideo(string filePath)
        {
            // Implementa aquí la lógica para reproducir el video.
            // Puedes usar alguna de las opciones mencionadas anteriormente.
            // Por ejemplo, si usaste Xamarin.Essentials, puedes usar Launcher.OpenAsync().

            // Ejemplo ficticio usando Xamarin.Essentials:
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
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