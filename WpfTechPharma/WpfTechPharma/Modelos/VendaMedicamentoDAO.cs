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
    internal class VendaMedicamentoDAO : IDAO<VendaMedicamento>
    {
        private static Conexao conexao;

        public VendaMedicamentoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(VendaMedicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_venda_medicamento " +
                    "(@quantidade_item, " +
                    "@valor_item, " +
                    "@Medicamento," +
                    "@Venda)";

                query.Parameters.AddWithValue("@quantidade_item", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor_item", t.ValorItem);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);
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

        public void Update(VendaMedicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Venda_Medicamento " +
                    "set " +
                    "vamo_quantidade_item = @quantidade_item, " +
                    "vamo_valor_item = @valor_item, " +
                    "fk_vend_id = @Venda, " +
                    "fk_medi_id = @Medicamento " +
                    "where " +
                    "(vamo_id = @id)";

                query.Parameters.AddWithValue("@data", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor", t.ValorItem);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);
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

        public void Delete(VendaMedicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Venda_Medicamento where (vamo_id = @id)";

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

        public VendaMedicamento GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Venda_Medicamento where (vamo_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                var VendaMedicamento = new VendaMedicamento();

                while (reader.Read())
                {
                    VendaMedicamento.Id = AuxiliarDAO.GetInt(reader, "vamo_id");
                    VendaMedicamento.QuantidadeItem = AuxiliarDAO.GetInt(reader, "vamo_quantidade_item");
                    VendaMedicamento.ValorItem = AuxiliarDAO.GetFloat(reader, "vamo_valor_item");
                    VendaMedicamento.Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id"));
                    VendaMedicamento.Medicamento = new MedicamentoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_medi_id"));
                }

                return VendaMedicamento;
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

        public List<VendaMedicamento> List()
        {
            try
            {
                List<VendaMedicamento> listaVendaMedicamento = new List<VendaMedicamento>();

                var query = conexao.Query();
                query.CommandText = "select * from Venda_Medicamento";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                while (reader.Read())
                {
                    listaVendaMedicamento.Add(new VendaMedicamento()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "vamo_id"),
                        QuantidadeItem = AuxiliarDAO.GetInt(reader, "vamo_quantidade_item"),
                        ValorItem = AuxiliarDAO.GetFloat(reader, "vamo_valor_item"),
                        Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id")),
                        Medicamento = new MedicamentoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_medi_id"))
                    });
                }

                return listaVendaMedicamento;
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