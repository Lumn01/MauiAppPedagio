using MauiAppPedagio.Models;
using System.Collections.ObjectModel;
namespace MauiAppPedagio.Views;

public partial class ListaViagens : ContentPage
{
    ObservableCollection<CustoViagem> lista_viagens = new ObservableCollection<CustoViagem>();

	public ListaViagens()
	{
		InitializeComponent();
        lst_viagens.ItemsSource = lista_viagens;
	}

    private void ToolbarItem_Somar(object sender, EventArgs e)
    {
        double soma = lista_viagens.Sum(i => i.Custo);
        string msg = $"O total das sua viagens é {soma:C}";
        DisplayAlert("Somatória", msg, "Fechar");
    }

    protected async override void OnAppearing()
    {
        if (lista_viagens.Count == 0)
        {
            List<CustoViagem> tmp = await App.Db2.GetAll();
            foreach (CustoViagem c in tmp)
            {
                lista_viagens.Add(c);
            }
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string s = e.NewTextValue;
        lista_viagens.Clear();

        List<CustoViagem> tmp = await App.Db2.Search(s);
        foreach (CustoViagem c in tmp)
        {
            lista_viagens.Add(c);
        }
    }

    private void ref_carregando_Refreshing(object sender, EventArgs e)
    {
        lista_viagens.Clear();
        Task.Run(async () =>
        {
            List<CustoViagem> tmp = await App.Db2.GetAll();
            foreach (CustoViagem c in tmp)
            {
                lista_viagens.Add(c);
            }
        });
        ref_carregando.IsRefreshing = false;
    }

    private async void MenuItem_Clicked_Remover(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = (MenuItem)sender;
            CustoViagem c = selecionado.BindingContext as CustoViagem;

            bool confirm = await DisplayAlert("Tem certeza?", "Remover Viagem?", "Sim", "Cancelar");

            if (confirm)
            {
                await App.Db2.Delete(c.Id);
                await DisplayAlert("Sucesso!", "Viagem Removida", "OK");
                lista_viagens.Remove(c);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}