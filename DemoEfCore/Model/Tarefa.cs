using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEfCore.Model
{
    public class Tarefa
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public string Descricao { get; set; }
        public bool Concluida { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
