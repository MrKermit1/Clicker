using Clicker.Models;
using Clicker.AppDB;
namespace Clicker.Views;

public partial class AddNewClickerPage : ContentPage
{

    public ClickerModel clickerModel { get; set; }
    public Connector connector  = new();

    public AddNewClickerPage()
	{
		InitializeComponent();

        //init nowego obiektu z bazowymi wartoœciami
        clickerModel = new ClickerModel
        {
            Name = "",
            Value = 0,
            InitValue = 0,
            Color = "Red"
        };

        //zbindowanie na nowo utworzony obiekt
        BindingContext = clickerModel;
    }

    //ustawienie koloru
    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int index = picker.SelectedIndex;

        if (index != -1)
        {
            clickerModel.Color = (string)picker.SelectedItem;
        }
    }

    private async void OnAddClickerClicked(object sender, EventArgs e)
    {
        if (!clickerModel.isNameEmpty())
        {
            clickerModel.InitValue = clickerModel.Value;
            //dodanie clickera do bazy
            await connector.insertClicker(clickerModel);
            if (connector.connectorState)
            {
                await DisplayAlert("Clicker Add Raport", $"Name: {clickerModel.Name}, Value: {clickerModel.Value}, InitValue: {clickerModel.InitValue}, Color: {clickerModel.Color}", "OK");


                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await DisplayAlert("Clicker Add Raport", "Database Error", "OK");
            }
            
        }
        else
        {
            await DisplayAlert("Clicker Add Raport", "Wpisano nie prawid³owe dane", "OK");
        }
        
    }

}