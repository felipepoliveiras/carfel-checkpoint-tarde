using System.Collections.Generic;
using carfel_checkpoint_tarde.Models;

namespace carfel_checkpoint_tarde.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório depoimento
    /// </summary>
    public interface IDepoimentoRepositorio
    {
        /// <summary>
        /// Cadastrar um novo depoimento
        /// </summary>
        /// <param name="depoimento">DepoimentoModel</param>
        void Cadastrar(DepoimentoModel depoimento);
        
        /// <summary>
        /// Altera um depoimento
        /// </summary>
        /// <param name="depoimento">DepoimentoModel</param>
        void Alterar(DepoimentoModel depoimento);

        /// <summary>
        /// Busca um depoimento por Id
        /// </summary>
        /// <param name="id">Id do depoimento</param>
        /// <returns>Retorna um DepoimentoModel caso encontre ou nulo</returns>
        DepoimentoModel BuscarPorId(int id);

        /// <summary>
        /// Lista todos os depoimentos
        /// </summary>
        /// <returns>Retorna uma lista de depoimentos</returns>
        List<DepoimentoModel> Listar();
    }
}