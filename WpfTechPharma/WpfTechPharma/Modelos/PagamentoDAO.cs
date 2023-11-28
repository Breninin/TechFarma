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
    internal class PagamentoDAO : IDAO<Pagamento>
    {
        private static Conexao conexao;

        public PagamentoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(Pagamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_pagamento " +
                    "(@data, " +
                    "@valor, " +
                    "@forma_pagamento, " +
                    "@status, " +
                    "@vencimento, " +
                    "@numero_parcela, " +
                    "@caixa," +
                    "@despesa)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@forma_pagamento", t.FormaPagamento);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@vencimento", t.Vencimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@numero_parcela", t.NumeroParcela);
                query.Parameters.AddWithValue("@caixa", t.Caixa.Id);
                query.Parameters.AddWithValue("@despesa", t.Despesa.Id);

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

        public void Update(Pagamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Pagamento " +
                    "set " +
                    "paga_data = @data, " +
                    "paga_valor = @valor, " +
                    "paga_forma_pagamento = @forma_pagamento, " +
                    "paga_status = @status, " +
                    "paga_vencimento = @vencimento, " +
                    "paga_email = @numero_parcela, " +
                    "fk_desp_id = @despesa, " +
                    "fk_caix_id = @caixa " +
                    "where " +
                    "(paga_id = @id)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", t.Valor);
                query.Parameters.AddWithValue("@forma_pagamento", t.FormaPagamento);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@vencimento", t.Vencimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@numero_parcela", t.NumeroParcela);
                query.Parameters.AddWithValue("@despesa", t.Despesa.Id);
                query.Parameters.AddWithValue("@caixa", t.Caixa.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o Pagamento. Verifique e tente novamente.");
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

        public void Delete(Pagamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Pagamento where (paga_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o Pagamento. Verifique e tente novamente.");
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

        public Pagamento GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Pagamento where (paga_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Pagamento foi encotrado!");
                }

                var Pagamento = new Pagamento();

                while (reader.Read())
                {
                    Pagamento.Id = AuxiliarDAO.GetInt(reader, "paga_id");
                    Pagamento.Data = AuxiliarDAO.GetDateTime(reader, "paga_data");
                    Pagamento.Valor = AuxiliarDAO.GetFloat(reader, "paga_valor");
                    Pagamento.FormaPagamento = AuxiliarDAO.GetString(reader, "paga_forma_pagamento");
                    Pagamento.Status = AuxiliarDAO.GetString(reader, "paga_status");
                    Pagamento.Vencimento = AuxiliarDAO.GetDateTime(reader, "paga_vencimento");
                    Pagamento.NumeroParcela = AuxiliarDAO.GetInt(reader, "paga_numero_parcela");
                    Pagamento.Despesa = new DespesaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_desp_id"));
                    Pagamento.Caixa = new CaixaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_caix_id"));
                }

                return Pagamento;
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

        public List<Pagamento> List()
        {
            try
            {
                List<Pagamento> listaPagamento = new List<Pagamento>();

                var query = conexao.Query();
                query.CommandText = "select * from Pagamento";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Pagamento foi encotrado!");
                }

                while (reader.Read())
                {
                    listaPagamento.Add(new Pagamento()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "paga_id"),
                        Data = AuxiliarDAO.GetDateTime(reader, "paga_data"),
                        Valor = AuxiliarDAO.GetFloat(reader, "paga_valor"),
                        FormaPagamento = AuxiliarDAO.GetString(reader, "paga_forma_pagamento"),
                        Status = AuxiliarDAO.GetString(reader, "paga_status"),
                        Vencimento = AuxiliarDAO.GetDateTime(reader, "paga_vencimento"),
                        NumeroParcela = AuxiliarDAO.GetInt(reader, "paga_numero_parcela"),
                        Despesa = new DespesaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_desp_id")),
                        Caixa = new CaixaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_caix_id"))
                    });
                }

                return listaPagamento;
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

