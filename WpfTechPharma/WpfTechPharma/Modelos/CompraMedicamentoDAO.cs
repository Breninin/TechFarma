using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.BancoDados;
using WpfTechPharma.Interfaces;

namespace WpfTechPharma.Modelos
{
    internal class CompraMedicamentoDAO : IDAO<CompraMedicamento>
    {
        private static Conexao conexao;

        public CompraMedicamentoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(CompraMedicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_medicamento " +
                    "(@quantidade_item, " +
                    "@valor_item, " +
                    "@compra," +
                    "@Medicamento)";

                query.Parameters.AddWithValue("@quantidade_item", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor_item", t.ValorItem);
                query.Parameters.AddWithValue("@compra", t.Compra.Id);
                query.Parameters.AddWithValue("@Medicamento", t.Medicamento.Id);

                var result = (string)query.ExecuteScalar();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Update(CompraMedicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Compra_Medicamento " +
                    "set " +
                    "camo_quantidade_item = @quantidade_item, " +
                    "camo_valor_item = @valor_item, " +
                    "fk_comp_id = @compra, " +
                    "fk_medi_id = @Medicamento " +
                    "where " +
                    "(camo_id = @id)";

                query.Parameters.AddWithValue("@data", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor", t.ValorItem);
                query.Parameters.AddWithValue("@compra", t.Compra.Id);
                query.Parameters.AddWithValue("@Medicamento", t.Medicamento.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o registro. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Delete(CompraMedicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Compra_Medicamento where (camo_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o registro. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public CompraMedicamento GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Compra_Medicamento where (camo_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                var CompraMedicamento = new CompraMedicamento();

                while (reader.Read())
                {
                    CompraMedicamento.Id = AuxiliarDAO.GetInt(reader, "camo_id");
                    CompraMedicamento.QuantidadeItem = AuxiliarDAO.GetInt(reader, "camo_quantidade_item");
                    CompraMedicamento.ValorItem = AuxiliarDAO.GetFloat(reader, "camo_valor_item");
                    CompraMedicamento.Compra = new CompraDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_comp_id"));
                    CompraMedicamento.Medicamento = new MedicamentoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_medi_id"));
                }

                return CompraMedicamento;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public List<CompraMedicamento> List()
        {
            try
            {
                List<CompraMedicamento> listaCompraMedicamento = new List<CompraMedicamento>();

                var query = conexao.Query();
                query.CommandText = "select * from Compra_Medicamento";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                while (reader.Read())
                {
                    listaCompraMedicamento.Add(new CompraMedicamento()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "camo_id"),
                        QuantidadeItem = AuxiliarDAO.GetInt(reader, "camo_quantidade_item"),
                        ValorItem = AuxiliarDAO.GetFloat(reader, "camo_valor_item"),
                        Compra = new CompraDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_comp_id")),
                        Medicamento = new MedicamentoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_medi_id"))
                    });
                }

                return listaCompraMedicamento;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}