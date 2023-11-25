using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Aplicacion.DATA;

namespace Aplicacion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SQLiteHelper db;
        public Login()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Empresa.db3");
            db = new SQLiteHelper(dbPath);
        }
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            string email = txtEmailLog.Text;
            string contraseña = txtContraLog.Text;

            var usuario = await db.AuthenticateAsync(email, contraseña);

            if (usuario != null)
            {
                await DisplayAlert("Valido", "Inicio de sesion correcto", "OK");
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Error", "Credenciales inválidas", "OK");
            }
        }

        private void btnRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignIn());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}