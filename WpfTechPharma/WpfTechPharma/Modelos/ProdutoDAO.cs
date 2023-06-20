using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.BancoDados;
using WpfTechPharma.Interfaces;
using MySql.Data.MySqlClient;

namespace WpfTechPharma.Modelos
{
    internal class ProdutoDAO : IDAO<Produto>
    {
            private static Conexao conexao;
            public ProdutoDAO()
            {
                conexao = new Conexao();
            }
            public void Insert(Produto t)
            {
                try
                {
                    var query = conexao.Query();
                    query.CommandText = "insert into " +
                        "Produto " +
                        "(prod_nome, " +
                        "prod_marca, " +
                        "prod_valor_compra, " +
                        "prod_valor_venda, " +
                        "prod_quantidade" +
                        "prod_codigo_barra, " +
                        "fk_forn_id) " +
                        "values " +
                        "(@nome, " +
                        "@marca, " +
                        "@valor_compra, " +
                        "@valor_venda, " +
                        "@quantidade, " +
                        "@codigo_barra, " +
                        "@fornecedorId)";

                    query.Parameters.AddWithValue("@nome", t.nome);
                    query.Parameters.AddWithValue("@marca", t.marca);
                    query.Parameters.AddWithValue("@valor_compra", t.valor_compra);
                    query.Parameters.AddWithValue("@valor_venda", t.valor_venda);
                    query.Parameters.AddWithValue("@quantidade", t.quantidade);
                    query.Parameters.AddWithValue("@codigo_barra", t.codigo_barra);
                    query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        throw new NotImplementedException("Erro ao salvar o produto.");
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

            public void Update(Produto t)
            {
                try
                {
                    var query = conexao.Query();
                    query.CommandText = "Update" +
                        "Produto " +
                        "set" +
                        "(prod_nome = @nome, " +
                        "prod_marca = @marca, " +
                        "prod_valor_compra = @valor_compra, " +
                        "prod_valor_venda = @valor_venda, " +
                        "prod_quantidade = @quantidade" +
                        "prod_codigo_barra = @codigo_barra, " +
                        "fk_forn_id = @fornecedorId)" +
                        "where " +
                        "prod_id = @id";

                    query.Parameters.AddWithValue("@nome", t.nome);
                    query.Parameters.AddWithValue("@marca", t.marca);
                    query.Parameters.AddWithValue("@valor_compra", t.valor_compra);
                    query.Parameters.AddWithValue("@valor_venda", t.valor_venda);
                    query.Parameters.AddWithValue("@quantidade", t.quantidade);
                    query.Parameters.AddWithValue("@codigo_barra", t.codigo_barra);
                    query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);

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

                    query.Parameters.AddWithValue("@id", t.id);

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
                    Produto.id = reader.GetInt32("prod_id");
                    Produto.nome = AuxiliarDAO.GetString(reader, "prod_nome");
                    Produto.marca = AuxiliarDAO.GetString(reader, "prod_marca");
                    Produto.valor_compra = reader.GetFloat("prod_valor_compra");
                    Produto.valor_venda = reader.GetFloat("prod_valor_venda");
                    Produto.quantidade = AuxiliarDAO.GetInt(reader, "prod_quantidade");
                    Produto.codigo_barra = AuxiliarDAO.GetString(reader, "prod_codigo_barra");
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
                throw new NotImplementedException();
            }


            //fim do MedicamentoDAO
        
    }
}
