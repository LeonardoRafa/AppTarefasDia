using AppTarefasDia.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AppTarefasDia.Helper
{
    public class SqLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _connection;

        public SqLiteDatabaseHelper(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Tarefa>().Wait();
        }

        public Task<List<Tarefa>> GetAll()
        {
            return _connection.Table<Tarefa>().OrderBy(x => x.Data_Vencimento).ToListAsync();
        }

        public Task<int> Insert(Tarefa t)
        {
            return _connection.InsertAsync(t);
        }

        public Task<List<Tarefa>> Update(Tarefa t)
        {
            string sql = "UPDATE Tarefa SET Titulo = ?, Descricao = ?, Data_Vencimento = ?, Status = ? WHERE Id = ?";

            return _connection.QueryAsync<Tarefa>(sql, t.Titulo, t.Descricao, t.Data_Vencimento, t.Status, t.Id);
        }

        public Task<int> Delete(int id)
        {
            return _connection.Table<Tarefa>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Tarefa>> Search(string name)
        {
            string sql = "SELECT * FROM Tarefa WHERE Titulo LIKE '%" + name + "%' ORDER BY Data_Vencimento ASC";

            return _connection.QueryAsync<Tarefa>(sql);
        }
    }
}

