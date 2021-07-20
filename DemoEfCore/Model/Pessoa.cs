using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEfCore.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Excluido { get; set; }
        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
