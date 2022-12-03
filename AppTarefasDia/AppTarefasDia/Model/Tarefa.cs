using System;
using SQLite;

namespace AppTarefasDia.Model
{
    public class Tarefa
    {
        string _titulo;
        string _descricao;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Data_Vencimento { get; set; }
        public string Titulo { 
            get=> _titulo; 
            set 
            {
                if(value == null)
                {
                    throw new Exception("Título inválido");
                }

                 _titulo = value;
            }
        }

        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Descrição inválida");
                }

                _descricao = value;
            }
        }
        public bool Status { get; set; }

        public bool Atrasado
        {
            get
            {
                return Data_Vencimento < DateTime.Today;
            }
        }
    }
}
