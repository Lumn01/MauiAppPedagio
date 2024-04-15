using SQLite;

namespace MauiAppPedagio.Models
{
    public class Pedagio
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Local { get; set; }
        public double Valor { get; set; }
    }
}
