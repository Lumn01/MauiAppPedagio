using SQLite;

namespace MauiAppPedagio.Models
{
    public class CustoViagem
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set;}
        public double Distancia { get; set; }
        public double Rendimento { get; set; }
        public double PrecoCombustivel {get; set;}
        public double Custo { get => (PrecoCombustivel / Rendimento) * Distancia; }
    }
}
