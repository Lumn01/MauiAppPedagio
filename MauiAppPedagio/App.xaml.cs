using MauiAppPedagio.Helpers;

namespace MauiAppPedagio
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;
        static SQLiteDatabaseHelperCusto _db2;

        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData
                        ), "banco_sqlite_pedagio.db3"
                    );

                    _db = new SQLiteDatabaseHelper( path );
                }

                return _db;
            }
        }

        public static SQLiteDatabaseHelperCusto Db2
        {
            get
            {
                if (_db2 == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData
                        ), "banco_sqlite_custoviagem.db3"
                    );

                    _db2 = new SQLiteDatabaseHelperCusto( path );
                }

                return _db2;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
