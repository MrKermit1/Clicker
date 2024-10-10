using Clicker.Models;
using System.ComponentModel;
using Clicker.AppDB;
using System.Diagnostics;
namespace Clicker.Views;

public partial class ClickerView : ContentView
{
    //ustawienie ClickedModel jako obiekt bindowalny(?) bez tego nie mo¿na aktualizowaæ stanu clickera
    public static readonly BindableProperty ClickerModelProperty =
        BindableProperty.Create(
            nameof(ClickerModel),
            typeof(ClickerModel),
            typeof(ClickerView),
            default(ClickerModel),
            BindingMode.Default,
            propertyChanged: OnClickerModelChanged
        );
    Connector connector = new();

    public ClickerModel ClickerModel
    {
        get => (ClickerModel)GetValue(ClickerModelProperty);
        set => SetValue(ClickerModelProperty, value);
    }

    public ClickerView()
    {
        InitializeComponent();
    }

    public ClickerView(ClickerModel model)
    {
        InitializeComponent();
        ClickerModel = model;
        BindingContext = ClickerModel;
    }

    private static void OnClickerModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ClickerView clickerView)
        {
            clickerView.BindingContext = newValue;
        }
    }

    private void OnIncrementClicked(object sender, EventArgs e)
    {
        ClickerModel?.Increment();
        connector?.saveOneClicker(ClickerModel);
    }

    private void OnDecrementClicked(object sender, EventArgs e)
    {
        ClickerModel?.Decrement();
        connector?.saveOneClicker(ClickerModel);
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        await connector.deleteOneClicker(ClickerModel?.Name);

        //odœwie¿anie maina:
        //usun starego maina
        var navigationStack = Navigation.NavigationStack;
        if (navigationStack.Count > 0)
        {
            var oldMainPage = navigationStack.LastOrDefault(p => p is MainPage);
            if (oldMainPage != null)
            {
                Navigation.RemovePage(oldMainPage);
            }
        }

        //nowy main
        var main = new MainPage();
        await Navigation.PushAsync(main);
        await Shell.Current.GoToAsync("//MainPage");

    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        ClickerModel?.Reset();
    }
}
