using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views
{
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Usuarios user = new Usuarios
                {

                    Email = txtEmailReg.Text,
                    Password = txtContraReg.Text,
                    Nombre = txtNombreReg.Text,
                };

                Limpiar();
                await App.SQLiteDB.SaveUserAsync(user);
                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");

            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }
        public void Limpiar()
        {
            txtEmailReg.Text = "";
            txtContraReg.Text = "";
            txtNombreReg.Text = "";
        }
        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtEmailReg.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtContraReg.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtNombreReg.Text))
            {
                respuesta = false;

            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

    }
}