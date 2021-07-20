using DemoEfCore.Data;
using DemoEfCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace DemoEfCoreTests
{
    public class TestesInMemory
    {
        [Fact]
        public void Deve_Inserir_Pessoa()
        {
            // Arrange
            var pessoa = new Pessoa
            {
                Nome = "Pessoa Teste"
            };

            // Setup
            var context = CreateContext();
            context.Pessoas.Add(pessoa);

            // Act
            var inseridas = context.SaveChanges();

            // Assert
            Assert.Equal(1, inseridas);
        }

        [Theory]
        [InlineData("Vinicius")]
        [InlineData("Diogo")]
        [InlineData("Leonardo")]
        public void Deve_Inserir_Pessoa_E_Recuperar(string nome)
        {
            // Arrange
            var pessoa = new Pessoa
            {
                Nome = nome
            };

            // Setup
            var context = CreateContext();
            context.Database.EnsureCreated();
            context.Pessoas.Add(pessoa);

            // Act
            var inseridas = context.SaveChanges();
            pessoa = context.Pessoas.FirstOrDefault(p => p.Nome == nome);

            // Assert
            Assert.Equal(1, inseridas);
            Assert.Equal(nome, pessoa.Nome);
        }

        private TestContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .Options;

            return new TestContext(options);
        }
    }
}
