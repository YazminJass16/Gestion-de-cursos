using System;
using Xamarin.Forms;
using Aplicacion.Models;

namespace Aplicacion.Views
{
    public partial class Cursos : ContentPage
    {
        public Cursos()
        {
            InitializeComponent();
            CargarDatos();
        }
        private async void IsCursos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Cursodb)e.SelectedItem;
            BtnGuardar.IsVisible = false;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;
            txtId.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.idC.ToString()))
            {
                var curso = await App.SQLiteDB.GetCursoByIdAsync(obj.idC);
                if (curso != null)
                {
                    txtId.Text = curso.idC.ToString();
                    txtNombre.Text = curso.Nombre;
                    txtTipo.SelectedItem = curso.Tipo;
                    txtDescripcion.Text = curso.Descripcion;
                    txtHoras.Text = curso.Horas.ToString();
                }
            }
        }
        public bool validaDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (txtTipo.SelectedItem == null)
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtHoras.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
        public void Limpiar()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtTipo.SelectedItem = null;
            txtDescripcion.Text = "";
            txtHoras.Text = "";
        }
        private async void CargarDatos()
        {
            var cursoList = await App.SQLiteDB.GetCursoAsync();
            if (cursoList != null)
            {
                IsCursos.ItemsSource = cursoList;
            }
        }
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                string selectedOption = txtTipo.SelectedItem as string;
                Cursodb curso = new Cursodb();
                {
                    curso.Nombre = txtNombre.Text;
                    curso.Tipo = selectedOption;
                    curso.Descripcion = txtDescripcion.Text;
                    curso.Horas = int.Parse(txtHoras.Text);
                };

                await App.SQLiteDB.SaveCursoAsync(curso);

                await DisplayAlert("Cursos", "Se guardo exitosamente", "OK");
                Limpiar();
                CargarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }
        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                string selectedOption = txtTipo.SelectedItem as string;
                Cursodb curso = new Cursodb()
                {
                    idC = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text,
                    Tipo = selectedOption,
                    Descripcion = txtDescripcion.Text,
                    Horas = int.Parse(txtHoras.Text)
                };
                await App.SQLiteDB.SaveCursoAsync(curso);
                await DisplayAlert("Cursos", "Se guardo exitosamente", "OK");

                Limpiar();

                txtId.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                BtnGuardar.IsVisible = true;
                CargarDatos();
            }
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var curso = await App.SQLiteDB.GetCursoByIdAsync(int.Parse(txtId.Text));
            if (curso != null)
            {
                await App.SQLiteDB.DeleteCursosAsync(curso);
                await DisplayAlert("Cursos", "Se elimino de manera exitosa", "OK");
                Limpiar();
                CargarDatos();
                txtId.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                BtnGuardar.IsVisible = true;
            }
        }
    }
}