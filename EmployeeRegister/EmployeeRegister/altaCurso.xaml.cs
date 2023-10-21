﻿using System;
using System.IO;
using EmployeeRegister.Models;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeRegister
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class altaCurso : ContentPage
	{
        private string selectedVideoPath; // Agrega esta línea para declarar la variable

        public altaCurso ()
		{
			InitializeComponent ();
		}

        private async void OnSelectVideoButtonClicked(object sender, EventArgs e)
        {
            try
            {
                FileResult fileResult = await FilePicker.PickAsync(new PickOptions());

                if (fileResult != null)
                {
                    // Verificar si el archivo seleccionado es un video
                    if (fileResult.ContentType.StartsWith("video/"))
                    {
                        selectedVideoPath = fileResult.FullPath;
                        lblSelectedVideo.Text = Path.GetFileName(selectedVideoPath);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Por favor selecciona un archivo de video.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
            }
        }


        private async void OnUploadButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(selectedVideoPath))
                {
                    // Guardar el video en la base de datos SQLite
                    Video video = new Video
                    {
                        FilePath = selectedVideoPath,
                        FileName = Path.GetFileName(selectedVideoPath)
                    };

                    await App.SQLiteDB.SaveVideoAsync(video);

                    // Limpiar selección
                    selectedVideoPath = null;
                    lblSelectedVideo.Text = "Ningún video seleccionado";

                    await DisplayAlert("Carga exitosa", "El video se cargó exitosamente.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Selecciona un video antes de cargarlo.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
            }
        }

        private async void lstVideos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var videos = await App.SQLiteDB.GetVideosAsync(); // Obtener la lista de videos desde la base de datos
            lstVideos.ItemsSource = videos; // Asignar la lista de videos al ListView
        }
    }
}