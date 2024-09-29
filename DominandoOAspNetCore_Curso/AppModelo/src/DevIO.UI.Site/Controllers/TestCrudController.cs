using DevIO.UI.Site.Data;
using DevIO.UI.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DevIO.UI.Site.Controllers
{
    public class TestCrudController : Controller
    {
        private readonly MeuDbContext _contexto;

        public TestCrudController(MeuDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var aluno = new Aluno 
            { 
                Nome = "Mateus",
                DataNascimento = DateTime.Now,
                Email = "akira.dev.math@gmail.com"
            };
            ///neste momento, ainda nao salvamos o aluno na DB
            ///aqui só adicionamos o aluno dentro da memory
            ///do DbContext
            _contexto.Alunos.Add(aluno);
            //Para salvar o aluno na Db:
            _contexto.SaveChanges();
            ///O método SaveChanges retorna um int

            //Se quiser obter o aluno através do id/PK
            var aluno2 = _contexto.Alunos.Find(aluno.Id);
            //Se quiser obter o aluno através do email
            //ou qualquer registro/campo da tabela, caso queira
            //pegar somente 1 aluno.
            var aluno3 = _contexto.Alunos.FirstOrDefault(a => a.Email == "akira.dev.math@gmail.com");
            //Para trazer tudo que achar de acordo com o que se quer
            var aluno4 = _contexto.Alunos.Where(a => a.Nome == "Eduardo");

            //supondo que mudamos a instância de aluno
            aluno.Nome = "Joao";
            _contexto.Alunos.Update(aluno);
            _contexto.SaveChanges();

            ///O método remove é feito da entidade/registro
            ///nao é feito pelo Id.
            _contexto.Alunos.Remove(aluno);
            ///Se nao tiver o aluno, somente o Id;
            ///Primeiro terá que achar ele através do Find
            ///ter ele como uma entidade completa ex.: aluno2
            ///e depois passar o método remove


            return View("_Layout");
        }
    }
}
