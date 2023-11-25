
using Aplicacion.Views;
using Xamarin.Forms;

namespace Aplicacion
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Registro", typeof(Registro));
            Routing.RegisterRoute("Cursos", typeof(Cursos));
            Routing.RegisterRoute("Seguimiento", typeof(Seguimiento));
        }
    }
}
