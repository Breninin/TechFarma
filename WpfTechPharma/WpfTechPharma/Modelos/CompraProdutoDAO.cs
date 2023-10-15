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
    internal class CompraProdutoDAO : IDAO<CompraProduto>
    {
        private static Conexao conexao;

        public CompraProdutoDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(CompraProduto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Compra_Produto " +
                    "(capo_quantidade_item, " +
                    "capo_valor_item, " +
                    "fk_comp_id, " +
                    "fk_prod_id) " +
                    "values " +
                    "(@quantidade_item, " +
                    "@valor_item, " +
                    "@compra," +
                    "@produto)";

                query.Parameters.AddWithValue("@quantidade_item", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor_item", t.ValorItem);
                query.Parameters.AddWithValue("@compra", t.Compra.Id);
                query.Parameters.AddWithValue("@produto", t.Produto.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar o registro. Verifique o registro inserido e tente novamente.");
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

        public void Update(CompraProduto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Compra_Produto " +
                    "set " +
                    "capo_quantidade_item = @quantidade_item, " +
                    "capo_valor_item = @valor_item, " +
                    "fk_comp_id = @compra, " +
                    "fk_prod_id = @produto " +
                    "where " +
                    "(capo_id = @id)";

                query.Parameters.AddWithValue("@data", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor", t.ValorItem);
                query.Parameters.AddWithValue("@compra", t.Compra.Id);
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

        public void Delete(CompraProduto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Compra_Produto where (capo_id = @id)";

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

        public CompraProduto GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Compra_Produto where (capo_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                var CompraProduto = new CompraProduto();

                while (reader.Read())
                {
                    CompraProduto.Id = AuxiliarDAO.GetInt(reader, "capo_id");
                    CompraProduto.QuantidadeItem = AuxiliarDAO.GetInt(reader, "capo_quantidade_item");
                    CompraProduto.ValorItem = AuxiliarDAO.GetFloat(reader, "capo_valor_item");
                    CompraProduto.Compra = new CompraDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_comp_id"));
                    CompraProduto.Produto = new ProdutoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_prod_id"));
                }

                return CompraProduto;
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

        public List<CompraProduto> List()
        {
            try
            {
                List<CompraProduto> listaCompraProduto = new List<CompraProduto>();

                var query = conexao.Query();
                query.CommandText = "select * from Compra_Produto";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                while (reader.Read())
                {
                    listaCompraProduto.Add(new CompraProduto()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "capo_id"),
                        QuantidadeItem = AuxiliarDAO.GetInt(reader, "capo_quantidade_item"),
                        ValorItem = AuxiliarDAO.GetFloat(reader, "capo_valor_item"),
                        Compra = new CompraDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_comp_id")),
                        Produto = new ProdutoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_prod_id"))
                    });
                }

                return listaCompraProduto;
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
