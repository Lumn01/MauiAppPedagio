using MauiAppPedagio.Models;
using System.Collections.ObjectModel;

namespace MauiAppPedagio.Views;

public partial class ListaPedagios : ContentPage
{
    ObservableCollection<Pedagio> lista_pedagios = new ObservableCollection<Pedagio>();
    public ListaPedagios()
	{
		InitializeComponent();
        lst_pedagios.ItemsSource = lista_pedagios;
    }

    private void ToolbarItem_Somar(object sender, EventArgs e)
    {
        double soma = lista_pedagios.Sum(i => i.Valor);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Somatória", msg, "Fechar");
    }

    protected async override void OnAppearing()
    {
        if (lista_pedagios.Count == 0)
        {
            List<Pedagio> tmp = await App.Db.GetAll();
            foreach (Pedagio p in tmp)
            {
                lista_pedagios.Add(p);
            }
        }
    }

    private async void ToolbarItem_Add(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.NovoPedagio());
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        lista_pedagios.Clear();

        List<Pedagio> tmp = await App.Db.Search(q);
        foreach (Pedagio p in tmp)
        {
            lista_pedagios.Add(p);
        }
    }

    private void ref_carregando_Refreshing(object sender, EventArgs e)
    {
        lista_pedagios.Clear();
        Task.Run(async () =>
        {
            List<Pedagio> tmp = await App.Db.GetAll();
            foreach (Pedagio p in tmp)
            {
                lista_pedagios.Add(p);
            }
        });
        ref_carregando.IsRefreshing = false;
    }

    private void lst_pedagios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Pedagio? p = e.SelectedItem as Pedagio;

        Navigation.PushAsync(new Views.EditarPedagio
        {
            BindingContext = p
        });
    }

    private async void MenuItem_Clicked_Remover(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = (MenuItem)sender;
            Pedagio p = selecionado.BindingContext as Pedagio;

            bool confirm = await DisplayAlert("Tem certeza?", "Remover Pedágio?", "Sim", "Cancelar");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                await DisplayAlert("Sucesso!", "Pedágio Removido", "OK");
                lista_pedagios.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}