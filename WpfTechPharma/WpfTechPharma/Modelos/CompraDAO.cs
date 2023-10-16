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
    internal class CompraDAO : IDAO<Compra>
    {
        private static Conexao conexao;

        public CompraDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(Compra t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Compra " +
                    "(comp_data, " +
                    "comp_valor, " +
                    "fk_desp_id, " +
                    "fk_forn_id) " +
                    "values " +
                    "(@data, " +
                    "@valor, " +
                    "@despesa," +
                    "@fornecedor)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@despesa", t.Despesa.Id);
                query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar a Compra. Verifique a Compra inserida e tente novamente.");
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

        public void Update(Compra t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Compra " +
                    "set " +
                    "comp_data = @data, " +
                    "comp_valor = @valor, " +
                    "fk_desp_id = @despesa, " +
                    "fk_forn_id = @fornecedor " +
                    "where " +
                    "(comp_id = @id)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@despesa", t.Despesa.Id);
                query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar a Compra. Verifique e tente novamente.");
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

        public void Delete(Compra t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Compra where (comp_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover a Compra. Verifique e tente novamente.");
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

        public Compra GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Compra where (comp_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhuma Compra foi encotrada!");
                }

                var Compra = new Compra();

                while (reader.Read())
                {
                    Compra.Id = AuxiliarDAO.GetInt(reader, "comp_id");
                    Compra.Data = AuxiliarDAO.GetDateTime(reader, "comp_data");
                    Compra.Valor = AuxiliarDAO.GetFloat(reader, "comp_valor");
                    Compra.Despesa = new DespesaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_desp_id"));
                    Compra.Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"));
                }

                return Compra;
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

        public List<Compra> List()
        {
            try
            {
                List<Compra> listaCompra = new List<Compra>();

                var query = conexao.Query();
                query.CommandText = "select * from Compra";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhuma Compra foi encotrada!");
                }

                while (reader.Read())
                {
                    listaCompra.Add(new Compra()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "comp_id"),
                        Data = AuxiliarDAO.GetDateTime(reader, "comp_data"),
                        Valor = AuxiliarDAO.GetFloat(reader, "comp_valor"),
                        Despesa = new DespesaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_desp_id")),
                        Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"))
                    });
                }

                return listaCompra;
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
