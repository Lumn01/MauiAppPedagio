using MauiAppPedagio.Models;

namespace MauiAppPedagio.Views;

public partial class NovoPedagio : ContentPage
{
	public NovoPedagio()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Pedagio p = new Pedagio
            {
                Local = txt_local.Text,
                Valor = Convert.ToDouble(txt_valor.Text),
            };

            await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Pedagio adicionado", "OK");
            await Navigation.PushAsync(new MainPage());

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}