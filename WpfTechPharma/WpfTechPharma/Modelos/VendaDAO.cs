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
    internal class VendaDAO : IDAO<Venda>
    {
        private static Conexao conexao;

        public VendaDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(Venda t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Venda " +
                    "(vend_data, " +
                    "vend_valor, " +
                    "vend_desconto, " +
                    "vend_quantidade_parcelas, " +
                    "fk_clie_id, " +
                    "fk_func_id) " +
                    "values " +
                    "(@data, " +
                    "@valor, " +
                    "@desconto, " +
                    "@quantidade_parcelas, " +
                    "@Cliente," +
                    "@Funcionario)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@desconto", t.Desconto);
                query.Parameters.AddWithValue("@quantidade_parcelas", t.QuantidadeParcelas);
                query.Parameters.AddWithValue("@Cliente", t.Cliente.Id);
                query.Parameters.AddWithValue("@Funcionario", t.Funcionario.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar a Venda. Verifique a Venda inserida e tente novamente.");
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

        public void Update(Venda t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Venda " +
                    "set " +
                    "vend_data = @data, " +
                    "vend_valor = @valor, " +
                    "vend_desconto = @desconto, " +
                    "vend_quantidade_parcelas = @quantidade_parcelas, " +
                    "fk_clie_id = @Cliente, " +
                    "fk_func_id = @Funcionario " +
                    "where " +
                    "(vend_id = @id)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@desconto", t.Desconto);
                query.Parameters.AddWithValue("@quantidade_parcelas", t.QuantidadeParcelas);
                query.Parameters.AddWithValue("@Cliente", t.Cliente.Id);
                query.Parameters.AddWithValue("@Funcionario", t.Funcionario.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar a Venda. Verifique e tente novamente.");
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

        public void Delete(Venda t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Venda where (vend_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover a Venda. Verifique e tente novamente.");
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

        public Venda GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Venda where (vend_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhuma Venda foi encotrada!");
                }

                var Venda = new Venda();

                while (reader.Read())
                {
                    Venda.Id = AuxiliarDAO.GetInt(reader, "vend_id");
                    Venda.Data = AuxiliarDAO.GetDateTime(reader, "vend_data");
                    Venda.Valor = AuxiliarDAO.GetFloat(reader, "vend_valor");
                    Venda.Desconto = AuxiliarDAO.GetFloat(reader, "vend_desconto");
                    Venda.QuantidadeParcelas = AuxiliarDAO.GetInt(reader, "vend_quantidade_parcelas");
                    Venda.Cliente = new ClienteDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_clie_id"));
                    Venda.Funcionario = new FuncionarioDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_func_id"));
                }

                return Venda;
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

        public List<Venda> List()
        {
            try
            {
                List<Venda> listaVenda = new List<Venda>();

                var query = conexao.Query();
                query.CommandText = "select * from Venda";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhuma Venda foi encotrada!");
                }

                while (reader.Read())
                {
                    listaVenda.Add(new Venda()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "vend_id"),
                        Data = AuxiliarDAO.GetDateTime(reader, "vend_data"),
                        Valor = AuxiliarDAO.GetFloat(reader, "vend_valor"),
                        Desconto = AuxiliarDAO.GetFloat(reader, "vend_desconto"),
                        QuantidadeParcelas = AuxiliarDAO.GetInt(reader, "vend_quantidade_parcelas"),
                        Cliente = new ClienteDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_clie_id")),
                        Funcionario = new FuncionarioDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_func_id"))
                    });
                }

                return listaVenda;
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
