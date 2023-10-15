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
    internal class RecebimentoDAO : IDAO<Recebimento>
    {
        private static Conexao conexao;

        public RecebimentoDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(Recebimento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Recebimento " +
                    "(rece_data, " +
                    "rece_valor, " +
                    "rece_forma_recebimento, " +
                    "rece_status, " +
                    "rece_vencimento, " +
                    "rece_numero_parcela, " +
                    "fk_caix_id, " +
                    "fk_vend_id) " +
                    "values " +
                    "(@data, " +
                    "@valor, " +
                    "@forma_recebimento, " +
                    "@status, " +
                    "@vencimento, " +
                    "@numero_parcela, " +
                    "@Caixa," +
                    "@Venda)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@forma_recebimento", t.FormaRecebimento);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@vencimento", t.Vencimento);
                query.Parameters.AddWithValue("@numero_parcela", t.NumeroParcela);
                query.Parameters.AddWithValue("@Caixa", t.Caixa.Id);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar o Recebimento. Verifique o Recebimento inserido e tente novamente.");
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

        public void Update(Recebimento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Recebimento " +
                    "set " +
                    "rece_data = @data, " +
                    "rece_valor = @valor, " +
                    "rece_forma_recebimento = @forma_recebimento, " +
                    "rece_status = @status, " +
                    "rece_vencimento = @vencimento, " +
                    "rece_numero_parcela = @numero_parcela, " +
                    "fk_caix_id = @Caixa, " +
                    "fk_vend_id = @Venda " +
                    "where " +
                    "(rece_id = @id)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@forma_recebimento", t.FormaRecebimento);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@vencimento", t.Vencimento);
                query.Parameters.AddWithValue("@numero_parcela", t.NumeroParcela);
                query.Parameters.AddWithValue("@Caixa", t.Caixa.Id);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o Recebimento. Verifique e tente novamente.");
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

        public void Delete(Recebimento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Recebimento where (rece_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o Recebimento. Verifique e tente novamente.");
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

        public Recebimento GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Recebimento where (rece_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Recebimento foi encotrado!");
                }

                var Recebimento = new Recebimento();

                while (reader.Read())
                {
                    Recebimento.Id = AuxiliarDAO.GetInt(reader, "rece_id");
                    Recebimento.Data = AuxiliarDAO.GetDateTime(reader, "rece_data");
                    Recebimento.Valor = AuxiliarDAO.GetFloat(reader, "rece_valor");
                    Recebimento.FormaRecebimento = AuxiliarDAO.GetString(reader, "rece_forma_recebimento");
                    Recebimento.Status = AuxiliarDAO.GetString(reader, "rece_status");
                    Recebimento.Vencimento = AuxiliarDAO.GetDateTime(reader, "rece_vencimento");
                    Recebimento.NumeroParcela = AuxiliarDAO.GetInt(reader, "rece_numero_parcela");
                    Recebimento.Caixa = new CaixaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_caix_id"));
                    Recebimento.Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id"));
                }

                return Recebimento;
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

        public List<Recebimento> List()
        {
            try
            {
                List<Recebimento> listaRecebimento = new List<Recebimento>();

                var query = conexao.Query();
                query.CommandText = "select * from Recebimento";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Recebimento foi encotrado!");
                }

                while (reader.Read())
                {
                    listaRecebimento.Add(new Recebimento()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "rece_id"),
                        Data = AuxiliarDAO.GetDateTime(reader, "rece_data"),
                        Valor = AuxiliarDAO.GetFloat(reader, "rece_valor"),
                        FormaRecebimento = AuxiliarDAO.GetString(reader, "rece_forma_recebimento"),
                        Status = AuxiliarDAO.GetString(reader, "rece_status"),
                        Vencimento = AuxiliarDAO.GetDateTime(reader, "rece_vencimento"),
                        NumeroParcela = AuxiliarDAO.GetInt(reader, "rece_numero_parcela"),
                        Caixa = new CaixaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_caix_id")),
                        Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id"))
                    });
                }

                return listaRecebimento;
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
