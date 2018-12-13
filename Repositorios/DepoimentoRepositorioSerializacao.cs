using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using carfel_checkpoint_tarde.Interfaces;
using carfel_checkpoint_tarde.Models;

namespace carfel_checkpoint_tarde.Repositorios
{
    /// <summary>
    /// Classe reponsável pela manipulação dos dados no banco de dados .dat
    /// </summary>
    public class DepoimentoRepositorioSerializacao : IDepoimentoRepositorio
    {
        //Declaração do objeto do tipo List<DepoimentoModel>
        private List<DepoimentoModel> DepoimentosSalvos {get; set;}

        public DepoimentoRepositorioSerializacao()
        {
            //Verifica arquivo depoimentos.dat existe
            if(!File.Exists("depoimentos.dat")){
                //Se não existir instância a lista
                DepoimentosSalvos = new List<DepoimentoModel>();
            } else {
                //Se existir  carrega os dados
                DesserializarLista();
            }
        }

        /// <summary>
        /// Desserializa o arquivo depoimentos.dat
        /// Carrega os dados em Depoimentos Salvos
        /// </summary>
        private void DesserializarLista()
        {
            //le o arquivo depoimento.dat
            byte[] bytesArquivo = File.ReadAllBytes("depoimentos.dat");

            //Armazena na memoria
            MemoryStream memoria = new MemoryStream(bytesArquivo);

            //Cria um objeto BinaryFormatter responsável por Serializar ou Desserializar 
            BinaryFormatter serializador = new BinaryFormatter();

            //Atribui os valores do arquivo a Lista
            DepoimentosSalvos = (List<DepoimentoModel>)serializador.Deserialize(memoria);
        }

        /// <summary>
        /// Serializa a lista
        /// </summary>
        private void SerializarLista(){
            //Cria um objeto MemoryStream
            MemoryStream memoria = new MemoryStream();

            //Cria um objeto BinaryFormatter
            BinaryFormatter serializador = new BinaryFormatter();

            //Serializa a lista
            serializador.Serialize(memoria, DepoimentosSalvos);

            //Grava dos dados no arquivo depoimentos.dat
            File.WriteAllBytes("depoimentos.dat", memoria.ToArray());
        }

        public void Alterar(DepoimentoModel depoimento)
        {
            //Percorre a lista
            for (int i = 0; i < DepoimentosSalvos.Count; i++)
            {
                //Verifica se o id do objeto passado como parametro é o mesmo
                //da lista
                if(DepoimentosSalvos[i].ID == depoimento.ID){
                    //Caso sim, altera o valor da lista
                    DepoimentosSalvos[i] = depoimento;

                    //Grava as informações da lista no arquivo
                    SerializarLista();

                    //Sair do laço for
                    break;
                }
            }
        }

        public DepoimentoModel BuscarPorId(int id)
        {
            //Utilizado Linq para retornar um objeto pelo seu Id
            return DepoimentosSalvos.FirstOrDefault(d => d.ID == id);
            
            // //Percorre a lista
            // foreach (DepoimentoModel depoimento in DepoimentosSalvos)
            // {
            //     //Verifica se o id é igual ao informado
            //     if(depoimento.ID == id){
            //         //retorna o objeto
            //         return depoimento;
            //     }
            // }

            // //Caso não tenha encontrado retorna null
            // return null;
        }

        public void Cadastrar(DepoimentoModel depoimento)
        {
            //Atribui um valor para o id, incrementa 1
            depoimento.ID = DepoimentosSalvos.Count + 1;
            //Adiciona o objeto passado como parametro a lista
            DepoimentosSalvos.Add(depoimento);
            //Grava as informações na lista
            SerializarLista();
        }

        public List<DepoimentoModel> Listar() => DepoimentosSalvos;
    }
}