using DemoEfCore.Data;
using DemoEfCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoEfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var db = new DemoContext();

            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            //db.Database.CanConnect();

            //InserirEmMassa();

            //BuscarAdiantado();
            //BuscarExplicito();
            //BuscarLazyLoad();

            //Update1();
            //Update2();
            //UpdateSemSelect();

            //CriarCidade();
            //DeleteComSelect();
            //DeleteSemSelect();

            //CriarCidade();
            //SqlRaw();
            //SqlRawInjection();
            //SqlInterpolated();

            //using var db = new DemoContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //InserirMuitos();

            //SelectTracking();
            //SelectAsNoTracking();
            //SelectAsNoTrackingWithIdentityResolution();

            //SelectSingleQuery();
            //SelectSpllitQuery();

            //SelectLike();
            //SelectNaoMapeado();

            //SelectToListWhere();
            //SelectWhereToList();

            //SelectWhereAtributo();

            //SelectSpllitQuery();
            //_count = 0;
            //GerenciarConexoes(false);
            //_count = 0;
            //GerenciarConexoes(true);
            //pooling

            FiltroGlobal();

            //SelectComTag();

            Console.ReadKey();
        }

        public static void InserirEmMassa()
        {
            using var db = new DemoContext();

            var pessoa = new Pessoa
            {
                Nome = "Diogo Silva",
                Tarefas = new List<Tarefa>
                {
                    new Tarefa
                    {
                        Descricao = "Passear com o cachorro"
                    },
                    new Tarefa
                    {
                        Descricao = "Ir ao mar"
                    }
                }
            };

            var pessoa1 = new Pessoa
            {
                Nome = "Vinicius Mussak",
                Tarefas = new List<Tarefa>
                {
                    new Tarefa
                    {
                        Descricao = "Colocar roupa no varal"
                    }
                }
            };

            var cidade = new Cidade
            {
                Nome = "Franca"
            };

            db.AddRange(pessoa, cidade, pessoa1);

            db.SaveChanges();
        }

        public static void BuscarAdiantado()
        {
            using var db = new DemoContext();

            var pessoas = db.Pessoas
                .Include(x => x.Tarefas)
                .ToList();
               
            foreach(var pessoa in pessoas)
            {
                Console.WriteLine($"Pessoa: {pessoa.Nome}");

                if(pessoa.Tarefas?.Any() ?? false)
                {
                    Console.WriteLine("Tarefas:");
                    foreach(var tarefa in pessoa.Tarefas)
                    {
                        Console.WriteLine($"\t{tarefa.Descricao} {(tarefa.Concluida ? "[x]" : "[ ]")}");
                    }
                }
            }
        }

        public static void BuscarExplicito()
        {
            using var db = new DemoContext();

            var pessoas = db.Pessoas
                .ToList();

            foreach (var pessoa in pessoas)
            {
                Console.WriteLine($"Pessoa: {pessoa.Nome}");

                if(pessoa.Id == 1)
                {
                    db.Entry(pessoa).Collection(p => p.Tarefas).Load();
                }

                if (pessoa.Tarefas?.Any() ?? false)
                {
                    Console.WriteLine("Tarefas:");
                    foreach (var tarefa in pessoa.Tarefas)
                    {
                        Console.WriteLine($"\t{tarefa.Descricao} {(tarefa.Concluida ? "[x]" : "[ ]")}");
                    }
                }
            }
        }

        public static void BuscarLazyLoad()
        {
            using var db = new DemoContext();

            //db.ChangeTracker.LazyLoadingEnabled = false;

            var pessoas = db.Pessoas.ToList();

            foreach (var pessoa in pessoas)
            {
                Console.WriteLine($"Pessoa: {pessoa.Nome}");

                if (pessoa.Tarefas?.Any() ?? false)
                {
                    Console.WriteLine("Tarefas:");
                    foreach (var tarefa in pessoa.Tarefas)
                    {
                        Console.WriteLine($"\t{tarefa.Descricao} {(tarefa.Concluida ? "[x]" : "[ ]")}");
                    }
                }
            }
        }

        public static void Update1()
        {
            using var db = new DemoContext();

            var pessoa = db.Pessoas.Find(1);

            pessoa.Nome = "Outra pessoa";

            db.Pessoas.Update(pessoa);

            db.SaveChanges();
        }

        public static void Update2()
        {
            using var db = new DemoContext();

            var pessoa = db.Pessoas.Find(1);

            pessoa.Nome = "Outra";

            db.SaveChanges();
        }

        public static void UpdateSemSelect()
        {
            using var db = new DemoContext();

            var id = 1;

            var pessoa = new Pessoa
            {
                Id = id
            };

            var pessoaObject = new
            {
                Nome = "Pessoa Update sem Select",
            };

            db.Attach(pessoa);
            db.Entry(pessoa).CurrentValues.SetValues(pessoaObject);

            db.SaveChanges();
        }

        public static void DeleteComSelect()
        {
            using var db = new DemoContext();

            var id = 1;
            var cidade = db.Cidades.Find(id);

            db.Cidades.Remove(cidade);

            db.SaveChanges();
        }

        public static void DeleteSemSelect()
        {
            using var db = new DemoContext();

            var id = 2;
            var cidade = new Cidade
            {
                Id = id
            };

            db.Entry(cidade).State = EntityState.Deleted;

            db.SaveChanges();
        }

        public static void CriarCidade()
        {
            using var db = new DemoContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Cidades.Add(new Cidade { Nome = "Minha cidade" });
            db.Cidades.Add(new Cidade { Nome = "Minha cidade 2" });
            db.Cidades.Add(new Cidade { Nome = "CIDADE" });

            db.SaveChanges();

            foreach (var cidade in db.Cidades.AsNoTracking())
            {
                Console.WriteLine($"Id: {cidade.Id} Nome: {cidade.Nome}");
            }
        }

        public static void SqlRaw()
        {
            using var db = new DemoContext();

            var nome = "Minha cidade";
            db.Database.ExecuteSqlRaw("UPDATE Cidade SET Nome = 'Cidade 01' WHERE Nome = {0}", nome);

            foreach (var cidade in db.Cidades.AsNoTracking())
            {
                Console.WriteLine($"Id: {cidade.Id} Nome: {cidade.Nome}");
            }
        }

        public static void SqlRawInjection()
        {
            using var db = new DemoContext();

            var descricao = "Teste ' OR 1='1";
            db.Database.ExecuteSqlRaw($"UPDATE Cidade SET Nome = 'Cidade Injetada' WHERE Nome='{descricao}'");

            foreach(var cidade in db.Cidades.AsNoTracking())
            {
                Console.WriteLine($"Id: {cidade.Id} Nome: {cidade.Nome}");
            }
        }

        public static void SqlInterpolated()
        {
            using var db = new DemoContext();

            var descricao = "Cidadezinha";
            db.Database.ExecuteSqlInterpolated($"UPDATE Cidade SET Nome = {descricao} WHERE Id=1");

            foreach (var cidade in db.Cidades.AsNoTracking())
            {
                Console.WriteLine($"Id: {cidade.Id} Nome: {cidade.Nome}");
            }
        }

        public static void InserirMuitos()
        {
            using var db = new DemoContext();

            for (var i = 0; i < 3; i++)
            {
                var pessoa = new Pessoa
                {
                    Nome = $"Pessoa {i}",
                    Tarefas = Enumerable.Range(1, 100).Select(x => new Tarefa
                    {
                        Descricao = $"Tarefa {x}",
                    }).ToList()
                };

                db.Pessoas.Add(pessoa);
            }

            db.SaveChanges();
        }

        public static void SelectTracking()
        {
            using var db = new DemoContext();

            var tarefas = db.Tarefas
                .Include(x => x.Pessoa)
                .ToList();

            var tarefa10 = db.Tarefas.Find(10);

            var teste = 10;
        }

        public static void SelectAsNoTracking()
        {
            using var db = new DemoContext();

            var tarefas = db.Tarefas
                .Include(x => x.Pessoa)
                .AsNoTracking()
                .ToList();

            var tarefa10 = db.Tarefas.Find(10);

            var teste = 10;
        }

        public static void SelectAsNoTrackingWithIdentityResolution()
        {
            using var db = new DemoContext();

            var tarefas = db.Tarefas
                .Include(x => x.Pessoa)
                .AsNoTrackingWithIdentityResolution()
                .ToList();

            var teste = 10;
        }

        public static void SelectSingleQuery()
        {
            using var db = new DemoContext();

            var _ = db.Pessoas
                .Include(x => x.Tarefas)
                .ToList();
        }

        public static void SelectSpllitQuery()
        {
            using var db = new DemoContext();

            var _ = db.Pessoas
                .Include(x => x.Tarefas)
                .AsSplitQuery()
                .ToList();
        }

        public static void SelectLike()
        {
            using var db = new DemoContext();

            var _ = db.Pessoas
                .Where(x => EF.Functions.Like(x.Nome, "v%"))
                .ToList();
        }

        public static void SelectNaoMapeado()
        {
            using var db = new DemoContext();

            var cidades = db.Cidades
                .Where(x => EF.Property<string>(x, "NaoMapeada") == "Teste")
                .ToList();

            foreach(var cidade in cidades)
            {
                Console.WriteLine($"Cidade: {cidade.Nome}");
            }
        }

        public static void SelectToListWhere()
        {
            using var db = new DemoContext();

            var _ = db.Tarefas
                .ToList()
                .Where(x => x.Id > 50);
        }

        public static void SelectWhereToList()
        {
            using var db = new DemoContext();

            var _ = db.Tarefas
                .Where(x => x.Id > 50)
                .ToList();
        }

        static int _count;
        public static void GerenciarConexoes(bool gerenciar)
        {
            using var db = new DemoContext();
            var time = System.Diagnostics.Stopwatch.StartNew();

            var connection = db.Database.GetDbConnection();

            connection.StateChange += (_, __) => ++_count;

            if (gerenciar)
            {
                connection.Open();
            }

            for (var i = 0; i < 200; i++)
            {
                db.Pessoas.AsNoTracking().Any();
            }

            time.Stop();
            var mensagem = $"Tempo: {time.Elapsed}, {gerenciar}, Contador: {_count}";

            Console.WriteLine(mensagem);
        }

        public static void FiltroGlobal()
        {
            using var db = new DemoContext();
            var _ = db.Pessoas.ToList();
        }

        public static void SelectWhereAtributo()
        {
            using var db = new DemoContext();
            var _ = db.Cidades.Where(x => x.Uf == "SP").ToList();
        }

        public static void SelectComTag()
        {
            using var db = new DemoContext();
            var _ = db.Cidades
                .Where(x => x.Uf == "SP")
                .TagWith("NOLOCK")
                .ToList();
        }
    }
}
