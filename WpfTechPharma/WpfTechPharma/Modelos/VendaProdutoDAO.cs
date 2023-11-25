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
    internal class VendaProdutoDAO : IDAO<VendaProduto>
    {
        private static Conexao conexao;

        public VendaProdutoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(VendaProduto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "cadastrar_venda_produto " +
                    "(@quantidade_item, " +
                    "@valor_item, " +
                    "@produto," +
                    "@Venda)";

                query.Parameters.AddWithValue("@quantidade_item", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor_item", t.ValorItem);
                query.Parameters.AddWithValue("@produto", t.Produto.Id);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);

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

        public void Update(VendaProduto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Venda_Produto " +
                    "set " +
                    "vapo_quantidade_item = @quantidade_item, " +
                    "vapo_valor_item = @valor_item, " +
                    "fk_vend_id = @Venda, " +
                    "fk_prod_id = @produto " +
                    "where " +
                    "(vapo_id = @id)";

                query.Parameters.AddWithValue("@data", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor", t.ValorItem);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);
                query.Parameters.AddWithValue("@produto", t.Produto.Id);
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

        public void Delete(VendaProduto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Venda_Produto where (vapo_id = @id)";

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

        public VendaProduto GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Venda_Produto where (vapo_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                var VendaProduto = new VendaProduto();

                while (reader.Read())
                {
                    VendaProduto.Id = AuxiliarDAO.GetInt(reader, "vapo_id");
                    VendaProduto.QuantidadeItem = AuxiliarDAO.GetInt(reader, "vapo_quantidade_item");
                    VendaProduto.ValorItem = AuxiliarDAO.GetFloat(reader, "vapo_valor_item");
                    VendaProduto.Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id"));
                    VendaProduto.Produto = new ProdutoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_prod_id"));
                }

                return VendaProduto;
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

        public List<VendaProduto> List()
        {
            try
            {
                List<VendaProduto> listaVendaProduto = new List<VendaProduto>();

                var query = conexao.Query();
                query.CommandText = "select * from Venda_Produto";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                while (reader.Read())
                {
                    listaVendaProduto.Add(new VendaProduto()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "vapo_id"),
                        QuantidadeItem = AuxiliarDAO.GetInt(reader, "vapo_quantidade_item"),
                        ValorItem = AuxiliarDAO.GetFloat(reader, "vapo_valor_item"),
                        Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id")),
                        Produto = new ProdutoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_prod_id"))
                    });
                }

                return listaVendaProduto;
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
