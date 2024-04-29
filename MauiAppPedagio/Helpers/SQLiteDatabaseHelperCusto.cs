using MauiAppPedagio.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppPedagio.Helpers
{
    public class SQLiteDatabaseHelperCusto
    {
        readonly SQLiteAsyncConnection _connn;

        public SQLiteDatabaseHelperCusto(string path)
        {
            _connn = new SQLiteAsyncConnection(path);
            _connn.CreateTableAsync<CustoViagem>().Wait();
        }

        public Task<int> Insert(CustoViagem c)
        {
            return _connn.InsertAsync(c);
        }

        public Task<List<CustoViagem>> Update(CustoViagem c)
        {
            string sql = "UPDATE CustoViagem SET Origem=?, Destino=?, Distancia=?, " +
                "Rendimento=?, PrecoCombustivel=?, Custo=? WHERE Id+?";
            return _connn.QueryAsync<CustoViagem>(
                sql,
                c.Origem, c.Destino, c.Distancia, c.Rendimento, c.PrecoCombustivel, c.Custo,
                c.Id
                );
        }

        public Task<List<CustoViagem>> GetAll()
        {
            return _connn.Table<CustoViagem>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _connn.Table<CustoViagem>().DeleteAsync(i => i.Id == id);
        }
    }
}
