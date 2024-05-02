using MauiAppPedagio.Models;
using MauiAppPedagio.Views;

namespace MauiAppPedagio
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btn_Calcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                CustoViagem c = new CustoViagem
                {
                    Origem = txt_Origem.Text,
                    Destino = txt_Destino.Text,
                    Distancia = Convert.ToDouble(txt_Distancia.Text),
                    Rendimento = Convert.ToDouble(txt_Rendimento.Text),
                    PrecoCombustivel = Convert.ToDouble(txt_PrecoCombustivel.Text),
            };

                await App.Db2.Insert(c);
                await DisplayAlert("Sucesso!", "Viagem adicionada", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void btn_ListaPedagios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaPedagios());
        }

        private void btn_ListaViagens_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaViagens());
        }
    }

}
