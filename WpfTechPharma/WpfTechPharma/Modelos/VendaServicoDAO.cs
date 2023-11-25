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
    internal class VendaServicoDAO : IDAO<VendaServico>
    {
        private static Conexao conexao;

        public VendaServicoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(VendaServico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_venda_servico " +
                    "(@quantidade_item, " +
                    "@valor_item, " +
                    "@Servico," +
                    "@Venda)";

                query.Parameters.AddWithValue("@quantidade_item", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor_item", t.ValorItem);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);
                query.Parameters.AddWithValue("@Servico", t.Servico.Id);

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

        public void Update(VendaServico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Venda_Servico " +
                    "set " +
                    "vaso_quantidade_item = @quantidade_item, " +
                    "vaso_valor_item = @valor_item, " +
                    "fk_vend_id = @Venda, " +
                    "fk_serv_id = @Servico " +
                    "where " +
                    "(vaso_id = @id)";

                query.Parameters.AddWithValue("@data", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor", t.ValorItem);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);
                query.Parameters.AddWithValue("@Servico", t.Servico.Id);
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

        public void Delete(VendaServico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Venda_Servico where (vaso_id = @id)";

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

        public VendaServico GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Venda_Servico where (vaso_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                var VendaServico = new VendaServico();

                while (reader.Read())
                {
                    VendaServico.Id = AuxiliarDAO.GetInt(reader, "vaso_id");
                    VendaServico.QuantidadeItem = AuxiliarDAO.GetInt(reader, "vaso_quantidade_item");
                    VendaServico.ValorItem = AuxiliarDAO.GetFloat(reader, "vaso_valor_item");
                    VendaServico.Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id"));
                    VendaServico.Servico = new ServicoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_serv_id"));
                }

                return VendaServico;
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

        public List<VendaServico> List()
        {
            try
            {
                List<VendaServico> listaVendaServico = new List<VendaServico>();

                var query = conexao.Query();
                query.CommandText = "select * from Venda_Servico";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                while (reader.Read())
                {
                    listaVendaServico.Add(new VendaServico()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "vaso_id"),
                        QuantidadeItem = AuxiliarDAO.GetInt(reader, "vaso_quantidade_item"),
                        ValorItem = AuxiliarDAO.GetFloat(reader, "vaso_valor_item"),
                        Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id")),
                        Servico = new ServicoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_serv_id"))
                    });
                }

                return listaVendaServico;
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
