using Aplicacion.Models;
using System;
using System.IO;
using Xamarin.Forms;

namespace Aplicacion.Views
{
    public partial class Seguimiento : ContentPage
    {
        //SQLiteConnection db = new SQLiteConnection("Empresa.db3");
        public Seguimiento()
        {
            InitializeComponent();
            CargarDatos();
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            //var empleados = db.Table<Empleados>().FirstOrDefault();

            if (!string.IsNullOrEmpty(txtIdSeguimiento.Text))
            {
                string selectedOption = txtEstatus.SelectedItem as string;
                SegCursoEmp seg = new SegCursoEmp()
                {
                    idEmpleado = int.Parse(txtnoEmpleado.Text),
                    Nombre = txtNombre.Text,
                    Curso = txtCurso.Text,
                    Lugar = txtLugar.Text,
                    Fecha = txtFecha.Text,
                    Hora = txtHora.Text,
                    Estatus = selectedOption,
                    Calificacion = int.Parse(txtCalificacion.Text)
                };
                await App.SQLiteDB.SaveSeguimientoAsync(seg);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa", "OK");

                Limpiar();
                txtnoEmpleado.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnGuardar.IsVisible = true;
                BtnEliminar.IsVisible = false;
                CargarDatos();
            }
        }
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            //var empleados = db.Table<Empleados>().FirstOrDefault();

            if (validaDatos())
            {
                string selectedOption = txtEstatus.SelectedItem as string;
                SegCursoEmp seg = new SegCursoEmp
                {
                    idEmpleado = int.Parse(txtnoEmpleado.Text),
                    Nombre = txtNombre.Text,
                    Curso = txtCurso.Text,
                    Lugar = txtLugar.Text,
                    Fecha = txtFecha.Text,
                    Hora = txtHora.Text,
                    Estatus = selectedOption,
                    Calificacion = int.Parse(txtCalificacion.Text),
                };

                await App.SQLiteDB.SaveSeguimientoAsync(seg);

                Limpiar();
                CargarDatos();
                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var seg = await App.SQLiteDB.GetSeguimientoByIdAsync(int.Parse(txtIdSeguimiento.Text));
            if (seg != null)
            {
                await App.SQLiteDB.DeleteSeguimientoAsync(seg);
                await DisplayAlert("Seguimiento de curso", "Se elimino de manera exitosa", "OK");
                Limpiar();
                CargarDatos();
                txtIdSeguimiento.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                BtnGuardar.IsVisible = true;
            }
        }
        public bool validaDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtnoEmpleado.Text))
            {
                respuesta = false;
            }

            else if (string.IsNullOrEmpty(txtCurso.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtLugar.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtFecha.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtHora.Text))
            {
                respuesta = false;
            }
            else if (txtEstatus.SelectedItem == null)
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCalificacion.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
        private async void CargarDatos()
        {
            var seguimientoList = await App.SQLiteDB.GetSeguimientoAsync();
            if (seguimientoList != null)
            {
                IsSeguimiento.ItemsSource = seguimientoList;
            }
        }
        public void Limpiar()
        {
            txtNombre.Text = "";
            txtCurso.Text = "";
            txtLugar.Text = "";
            txtFecha.Text = "";
            txtHora.Text = "";
            txtEstatus.SelectedItem = null;
            txtCalificacion.Text = "";
            txtnoEmpleado.Text = "";
        }
        private async void IsSeguimiento_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (SegCursoEmp)e.SelectedItem;
            BtnActualizar.IsVisible = true;
            BtnGuardar.IsVisible = false;
            txtnoEmpleado.IsVisible = true;
            BtnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.idSeguimiento.ToString()))
            {
                var seg = await App.SQLiteDB.GetSeguimientoByIdAsync(obj.idSeguimiento);
                if (seg != null)
                {
                    txtIdSeguimiento.Text = seg.idSeguimiento.ToString();
                    txtnoEmpleado.Text = seg.idEmpleado.ToString();
                    txtNombre.Text = seg.Nombre;
                    txtCurso.Text = seg.Curso;
                    txtLugar.Text = seg.Lugar;
                    txtFecha.Text = seg.Fecha.ToString();
                    txtHora.Text = seg.Hora.ToString();
                    txtEstatus.SelectedItem = seg.Estatus;
                    txtCalificacion.Text = seg.Calificacion.ToString();
                }
            }
        }
    }
}