using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using carfel_checkpoint_tarde.Interfaces;
using carfel_checkpoint_tarde.Models;

namespace carfel_checkpoint_tarde.Repositorios
{
    /// <summary>
    /// Classe responsável pela manipulação dos dados do usuário
    /// </summary>
    public class UsuarioRepositorioSerializacao : IUsuarioRepositorio
    {

        /// <summary>
        /// Armazena todos os usuários que serão persistidos na
        /// aplicação utilizando serialiização
        /// </summary>
        public List<UsuarioModel> UsuariosSalvos {get; set;}

        //CONSTRUTOR
        public UsuarioRepositorioSerializacao()
        {
            if (File.Exists("usuarios.dat"))
            {
                DesserializarLista();
            }
            else
            {
                UsuariosSalvos = new List<UsuarioModel>();
            }
        }

        
        public void Cadastrar(UsuarioModel usuario)
        {
            usuario.ID = UsuariosSalvos.Count + 1;
            UsuariosSalvos.Add(usuario);
            SerializarLista();
        }

        /// <summary>
        /// Desserializa a lista dos usuários salvos
        /// </summary>
        private void DesserializarLista()
        {
            //Ler os bytes do arquivo serializado
            byte[] bytesSerializados = File.ReadAllBytes("usuarios.dat");

            //Passar os bytes lidos e bufferizar na memória para desserializar
            MemoryStream memoria = new MemoryStream(bytesSerializados);

            //Criando o objeto que irá desserializar a lista
            BinaryFormatter desserializador = new BinaryFormatter();

            UsuariosSalvos = desserializador.Deserialize(memoria) as List<UsuarioModel>;
        }

        /// <summary>
        /// Serializa a lista dos usuários salvos
        /// </summary>
        private void SerializarLista()
        {
            //Objetos para serialização
            MemoryStream memoria = new MemoryStream();
            BinaryFormatter serializador = new BinaryFormatter();

            //Serializando a lista UsuariosSalvos e colocando na memoria
            serializador.Serialize(memoria, UsuariosSalvos);

            //Pegando os bytes que foram serializados e guardando no array
            byte[] bytesSerializados = memoria.ToArray();

            //Pegando o array com os bytes e escrevendo no arquuivo usuarios.dat
            File.WriteAllBytes("usuarios.dat", bytesSerializados);
        }

        public List<UsuarioModel> Listar() => UsuariosSalvos;

        public UsuarioModel Login(string email, string senha)
        {
            foreach (var usuario in UsuariosSalvos)
            {
                
                if (usuario.Email == email && usuario.Senha == senha)
                {
                    return usuario;
                }
            }

            return null;
        }
    }
}