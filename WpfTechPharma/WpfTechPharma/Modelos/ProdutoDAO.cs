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
    internal class ProdutoDAO : IDAO<Produto>
    {
        private static Conexao conexao;
        public ProdutoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(Produto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = 
                    "call cadastrar_produto " +
                    "(@nome, " +
                    "@marca, " +
                    "@valor_compra, " +
                    "@valor_venda, " +
                    "@quantidade, " +
                    "@tipo, " +
                    "@codigo_barra, " +
                    "@fornecedorId)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valor_compra", t.ValorCompra);
                query.Parameters.AddWithValue("@valor_venda", t.ValorVenda);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@tipo", t.Tipo);
                query.Parameters.AddWithValue("@codigo_barra", t.CodigoBarra);
                query.Parameters.AddWithValue("@fornecedorId", t.Fornecedor.Id);

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

        public void Update(Produto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "Update " +
                    "Produto " +
                    "set " +
                    "prod_nome = @nome, " +
                    "prod_marca = @marca, " +
                    "prod_valor_compra = @valor_compra, " +
                    "prod_valor_venda = @valor_venda, " +
                    "prod_tipo = @tipo, " +
                    "prod_quantidade = @quantidade," +
                    "prod_codigo_barra = @codigo_barra, " +
                    "fk_forn_id = @fornecedorId " +
                    "where " +
                    "(prod_id = @id)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valor_compra", t.ValorCompra);
                query.Parameters.AddWithValue("@valor_venda", t.ValorVenda);
                query.Parameters.AddWithValue("@tipo", t.Tipo);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@codigo_barra", t.CodigoBarra);
                query.Parameters.AddWithValue("@fornecedorId", t.Fornecedor.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new NotImplementedException("Erro ao atualizar o produto.");
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

        public void Delete(Produto t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Produto where (prod_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o produto. Verifique e tente novamente.");
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

        public Produto GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Produto where (prod_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum produto foi encontrado!");
                }

                var Produto = new Produto();

                while (reader.Read())
                {
                    Produto.Id = AuxiliarDAO.GetInt(reader, "prod_id");
                    Produto.Nome = AuxiliarDAO.GetString(reader, "prod_nome");
                    Produto.Marca = AuxiliarDAO.GetString(reader, "prod_marca");
                    Produto.ValorCompra = AuxiliarDAO.GetFloat(reader, "prod_valor_compra");
                    Produto.ValorVenda = AuxiliarDAO.GetFloat(reader, "prod_valor_venda");
                    Produto.Tipo = AuxiliarDAO.GetString(reader, "prod_tipo");
                    Produto.Quantidade = AuxiliarDAO.GetInt(reader, "prod_quantidade");
                    Produto.CodigoBarra = AuxiliarDAO.GetString(reader, "prod_codigo_barra");
                    Produto.Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"));
                }

                return Produto;
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


        public List<Produto> List()
        {
            try
            {
                List<Produto> listaProduto = new List<Produto>();

                var query = conexao.Query();
                query.CommandText = "select * from Produto";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum produto foi encotrado!");
                }

                while (reader.Read())
                {
                    listaProduto.Add(new Produto()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "prod_id"),
                        Nome = AuxiliarDAO.GetString(reader, "prod_nome"),
                        Marca = AuxiliarDAO.GetString(reader, "prod_marca"),
                        ValorCompra = AuxiliarDAO.GetFloat(reader, "prod_valor_compra"),
                        ValorVenda = AuxiliarDAO.GetFloat(reader, "prod_valor_venda"),
                        Tipo = AuxiliarDAO.GetString(reader, "prod_tipo"),
                        Quantidade = AuxiliarDAO.GetInt(reader, "prod_quantidade"),
                        CodigoBarra = AuxiliarDAO.GetString(reader, "prod_codigo_barra"),
                        Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"))
                    });
                }

                return listaProduto;

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
        //fim do MedicamentoDAO

    }
}