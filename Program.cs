using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using carfel_checkpoint_tarde.Interfaces;
using carfel_checkpoint_tarde.Models;
using carfel_checkpoint_tarde.Repositorios;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Senai.Carfel.Checkpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Verificando se o arrquivo existe para criar o adm padrao
            if (!File.Exists("usuarios.dat"))
            {
                UsuarioModel adm = new UsuarioModel();
                adm.Nome = "Administrador do Sistema";
                adm.Administrador = true;
                adm.DataCriacao = DateTime.Now;
                adm.Email = "admin@carfel.com";
                adm.Senha = "admin";

                IUsuarioRepositorio repositorio;
                repositorio = new UsuarioRepositorioSerializacao();

                repositorio.Cadastrar(adm);
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
