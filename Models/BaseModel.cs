using System;

namespace carfel_checkpoint_tarde.Models {

    [Serializable]
    public class BaseModel {
        /// <summary>
        /// Identificação única para os modelo registrados
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Data de registro do modelo
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}