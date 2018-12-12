using System.Collections.Generic;
using carfel_checkpoint_tarde.Models;

namespace carfel_checkpoint_tarde.Interfaces
{
    /// <summary>
    /// Interface DDD para o repositório de usuário
    /// </summary>
    public interface IUsuarioRepositorio
    {
        /// <summary>
        /// Persiste um novo registro de usuário no banco de dados
        /// </summary>
        /// <param name="usuario">Objeto que será persistido</param>
         void Cadastrar(UsuarioModel usuario);

        /// <summary>
        /// Busca um usuário através da combinação de e-mail de senha
        /// </summary>
        /// <param name="email">E-mail para busca</param>
        /// <param name="senha">Senha para busca</param>
        /// <returns>
        /// Retorna um objeto caso a combinação exista, caso a
        /// combinação não exista, retorna nulo.
        /// </returns>
         UsuarioModel Login(string email, string senha);

        /// <summary>
        /// Busca todos os usuários cadastrados no banco de dados
        /// </summary>
         List<UsuarioModel> Listar();
    }
}