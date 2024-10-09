using Clicker.Views;

namespace Clicker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddNewClickerPage), typeof(AddNewClickerPage));
        }
    }
}
