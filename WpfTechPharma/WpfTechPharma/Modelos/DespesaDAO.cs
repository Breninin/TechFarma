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
    internal class DespesaDAO : IDAO<Despesa>
    {
        private static Conexao conexao;

        public DespesaDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(Despesa t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_despesa " +
                    "(@data, " +
                    "@valor, " +
                    "@descricao, " +
                    "@tipo, " +
                    "@quantidade_parcelas)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@descricao", t.Descricao);
                query.Parameters.AddWithValue("@tipo", t.Tipo);
                query.Parameters.AddWithValue("@quantidade_parcelas", t.QuantidadeParcelas);

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

        public void Update(Despesa t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Despesa " +
                    "set " +
                    "desp_data = @data, " +
                    "desp_valor = @valor, " +
                    "desp_desc = @descricao, " +
                    "desp_tipo = @tipo, " +
                    "desp_quantidade_parcelas = @quantidade_parcelas, " +
                    "where " +
                    "(desp_id = @id)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@descricao", t.Descricao);
                query.Parameters.AddWithValue("@tipo", t.Tipo);
                query.Parameters.AddWithValue("@quantidade_parcelas", t.QuantidadeParcelas);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar a Despesa. Verifique e tente novamente.");
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

        public void Delete(Despesa t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Despesa where (desp_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover a Despesa. Verifique e tente novamente.");
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

        public Despesa GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Despesa where (desp_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhuma Despesa foi encotrado!");
                }

                var Despesa = new Despesa();

                while (reader.Read())
                {
                    Despesa.Id = AuxiliarDAO.GetInt(reader, "desp_id");
                    Despesa.Data = AuxiliarDAO.GetDateTime(reader, "desp_data");
                    Despesa.Valor = AuxiliarDAO.GetFloat(reader, "desp_valor");
                    Despesa.Descricao = AuxiliarDAO.GetString(reader, "desp_desc");
                    Despesa.Tipo = AuxiliarDAO.GetString(reader, "desp_tipo");
                    Despesa.QuantidadeParcelas = AuxiliarDAO.GetInt(reader, "desp_quantidade_parcelas");
                }

                return Despesa;
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

        public int GetLastInsertID()
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "SELECT * FROM Despesa WHERE ((SELECT MAX(desp_id) FROM Despesa) = desp_id)";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhuma Despesa foi encontrada!");
                }

                int lastInsertID = 0;

                while (reader.Read())
                {
                    lastInsertID = AuxiliarDAO.GetInt(reader, "desp_id");
                }

                return lastInsertID;
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

        public List<Despesa> List()
        {
            try
            {
                List<Despesa> listaDespesa = new List<Despesa>();

                var query = conexao.Query();
                query.CommandText = "select * from Despesa";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhuma Despesa foi encotrado!");
                }

                while (reader.Read())
                {
                    listaDespesa.Add(new Despesa()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "desp_id"),
                        Data = AuxiliarDAO.GetDateTime(reader, "desp_data"),
                        Valor = AuxiliarDAO.GetFloat(reader, "desp_valor"),
                        Descricao = AuxiliarDAO.GetString(reader, "desp_desc"),
                        Tipo = AuxiliarDAO.GetString(reader, "desp_tipo"),
                        QuantidadeParcelas = AuxiliarDAO.GetInt(reader, "desp_quantidade_parcelas")
                    });
                }

                return listaDespesa;
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
