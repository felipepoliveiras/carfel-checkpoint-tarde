using System;

namespace carfel_checkpoint_tarde.Models
{
    /// <summary>
    /// Classe responsável pelo model dos depoimentos
    /// </summary>
    [Serializable]
    public class DepoimentoModel : BaseModel
    {
        /// <summary>
        /// Mensagem de depoimento
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Status do depoimento(Em Espera, Aprovado, Reprovado)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Usuario que efetuou o cadastro do depoimento
        /// </summary>
        public UsuarioModel Usuario { get; set; }
    }
}