using System;

namespace carfel_checkpoint_tarde.Models
{
    [Serializable]
    public class UsuarioModel : BaseModel
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome {get; set;}

        /// <summary>
        /// E-mail do usuário. Utilizado como credencial de acesso ao sistema
        /// </summary>
        public string Email {get; set;}

        /// <summary>
        /// Senha de acesso ao sistema
        /// </summary>
        public string Senha {get; set;}

        /// <summary>
        /// Flag que indica se o usuário possui privilégios de administrador
        /// </summary>
        public bool Administrador {get; set;}
    }
}