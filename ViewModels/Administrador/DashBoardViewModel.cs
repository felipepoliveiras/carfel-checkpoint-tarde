using System.Collections.Generic;
using carfel_checkpoint_tarde.Models;

namespace carfel_checkpoint_tarde.ViewModels.Administrador
{
    public class DashBoardViewModel
    {
        public int QuantUsuarios { get; set; }
        public int QuantDepoimentos { get; set; }
        public int QuantDepoimentosAprovados { get; set; }
        public int QuantDepoimentosReprovados { get; set; }

        public List<UsuarioModel> ListaUsuarios { get; set; }
        public List<DepoimentoModel> ListaDepoimentos { get; set; }
    }
}