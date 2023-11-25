using System;
using System.ComponentModel;
using Xamarin.Forms;
using Aplicacion.Models;

namespace Aplicacion.Views
{
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
            CargarDatos();
        }
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                string selectedOption = txtTipo.SelectedItem as string;
                Empleados emp = new Empleados
                {
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    CURP = txtCURP.Text,
                    TipoEmp = selectedOption,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = long.Parse(txtTelefono.Text)
                };

                await App.SQLiteDB.SaveEmpleadoAsync(emp);

                Limpiar();
                CargarDatos();
                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }
        public bool validaDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCURP.Text))
            {
                respuesta = false;
            }
            else if (txtTipo.SelectedItem == null)
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmp.Text))
            {
                string selectedOption = txtTipo.SelectedItem as string;
                Empleados empleados = new Empleados()
                {
                    idEmp = int.Parse(txtIdEmp.Text),
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    CURP = txtCURP.Text,
                    TipoEmp = selectedOption,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = long.Parse(txtTelefono.Text)
                };
                await App.SQLiteDB.SaveEmpleadoAsync(empleados);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa", "OK");

                Limpiar();

                txtIdEmp.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                BtnGuardar.IsVisible = true;
                CargarDatos();
            }
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(int.Parse(txtIdEmp.Text));
            if (empleado != null)
            {
                await App.SQLiteDB.DeleteEmpleadoAsync(empleado);
                await DisplayAlert("Empleado", "Se elimino de manera exitosa", "OK");
                Limpiar();
                CargarDatos();
                txtIdEmp.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                BtnGuardar.IsVisible = true;
            }
        }
        private async void CargarDatos()
        {
            var empleadoList = await App.SQLiteDB.GetEmpleadosAsync();
            if (empleadoList != null)
            {
                IsEmpleados.ItemsSource = empleadoList;
            }
        }
        public void Limpiar()
        {
            txtIdEmp.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtCURP.Text = "";
            txtTipo.SelectedItem = null;
            txtEdad.Text = "";
            txtTelefono.Text = "";
        }
        private async void IsEmpleados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleados)e.SelectedItem;
            BtnGuardar.IsVisible = false;
            txtIdEmp.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.idEmp.ToString()))
            {
                var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(obj.idEmp);
                if (empleado != null)
                {
                    txtIdEmp.Text = empleado.idEmp.ToString();
                    txtNombre.Text = empleado.Nombre;
                    txtDireccion.Text = empleado.Direccion;
                    txtCURP.Text = empleado.CURP;
                    txtTipo.SelectedItem = empleado.TipoEmp;
                    txtEdad.Text = empleado.Edad.ToString();
                    txtTelefono.Text = empleado.Telefono.ToString();
                }
            }
        }
    }
}