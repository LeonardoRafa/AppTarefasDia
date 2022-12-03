using AppTarefasDia.View;
using Xamarin.Forms;
using AppTarefasDia.Helper;
using System.IO;
using System;
using System.Threading;
using System.Globalization;


namespace AppTarefasDia
{
    public partial class App : Application
    {
        static SqLiteDatabaseHelper _db;

        public static SqLiteDatabaseHelper Database
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                                Environment.SpecialFolder.LocalApplicationData
                            ),
                        "banco_sqllite_tarefas.db3"
                        );

                    _db = new SqLiteDatabaseHelper(path);
                }

                return _db;
            }
        }
        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            MainPage = new NavigationPage(new Listagem());
        }
    }
}
