using MauiAppPedagio.Models;

namespace MauiAppPedagio.Views;

public partial class EditarPedagio : ContentPage
{
	public EditarPedagio()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Pedagio pedagio_anexado = BindingContext as Pedagio;

            Pedagio p = new Pedagio
            {
                Id = pedagio_anexado.Id,
                Local = txt_local.Text,
                Valor = Convert.ToDouble(txt_valor.Text),
            };

            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", "Pedagio Editado!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Fechar");
        }
    }
}