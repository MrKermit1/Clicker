using System.Collections.ObjectModel;
using Clicker.Models;
using Clicker.AppDB;
using System.Diagnostics;

namespace Clicker.Views
{
    public partial class MainPage : ContentPage
    {
        //lista wszystkich klikerów
        public ObservableCollection<ClickerModel> Clickers { get; set; } = new ObservableCollection<ClickerModel>();
        
        public Connector connector = new ();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            fillClickers(); 
        }

        //wyjęcie wszystkich clickerow z bazy
        private async void fillClickers()
        {
            await connector.getAllClickers();
            Clickers.Clear();
            foreach (var clicker in connector.Clickers)
            {
                Clickers.Add(clicker);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            fillClickers();
        }

        private async void OnSaveAllClicked(object sender, EventArgs e)
        {
           await connector.saveAllClickers(Clickers);
        }

        private void OnAddNewClickerG(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("AddNewClickerPage");
        }
    }
}
