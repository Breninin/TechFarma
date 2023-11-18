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
    internal class CaixaDAO : IDAO<Caixa>
    {
        private static Conexao conexao;

        public string Insert(Caixa t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_caixa " +
                    "(@numero, " +
                    "@data, " +
                    "@horario_inicial, " +
                    "@horario_final, " +
                    "@status, " +
                    "@saldo_inicial, " +
                    "@saldo_final, " +
                    "@total_entrada," +
                    "@total_saida)";

                query.Parameters.AddWithValue("@numero", t.Numero);
                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@horario_inicial", t.HorarioInicial);
                query.Parameters.AddWithValue("@horario_final", t.HorarioFinal);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@saldo_inicial", t.SaldoInicial);
                query.Parameters.AddWithValue("@saldo_final", t.SaldoFinal);
                query.Parameters.AddWithValue("@total_entrada", t.TotalEntrada);
                query.Parameters.AddWithValue("@total_saida", t.TotalSaida);

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

        public void Update(Caixa t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Caixa " +
                    "set " +
                    "caix_numero = @numero, " +
                    "caix_data = @data, " +
                    "caix_horario_inicial = @horario_inicial, " +
                    "caix_horario_final = @horario_final, " +
                    "caix_status = @status, " +
                    "caix_saldo_inicial = @saldo_inicial, " +
                    "caix_saldo_final = @saldo_final, " +
                    "caix_total_entrada = @total_entrada, " +
                    "caix_total_saida = @total_saida " +
                    "where " +
                    "(caix_id = @id)";

                query.Parameters.AddWithValue("@numero", t.Numero);
                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@horario_inicial", t.HorarioInicial);
                query.Parameters.AddWithValue("@horario_final", t.HorarioFinal);
                query.Parameters.AddWithValue("@status", t.Status);
                query.Parameters.AddWithValue("@saldo_inicial", t.SaldoInicial);
                query.Parameters.AddWithValue("@saldo_final", t.SaldoFinal);
                query.Parameters.AddWithValue("@total_entrada", t.TotalEntrada);
                query.Parameters.AddWithValue("@total_saida", t.TotalSaida);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o Caixa. Verifique e tente novamente.");
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

        public void Delete(Caixa t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Caixa where (caix_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o Caixa. Verifique e tente novamente.");
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

        public Caixa GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Caixa where (caix_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Caixa foi encotrado!");
                }

                var Caixa = new Caixa();

                while (reader.Read())
                {
                    Caixa.Id = AuxiliarDAO.GetInt(reader, "caix_id");
                    Caixa.Numero = AuxiliarDAO.GetInt(reader, "caix_numero");
                    Caixa.Data = AuxiliarDAO.GetDateTime(reader, "caix_data");
                    Caixa.HorarioInicial = AuxiliarDAO.GetString(reader, "caix_horario_incial");
                    Caixa.HorarioFinal = AuxiliarDAO.GetString(reader, "caix_horario_final");
                    Caixa.Status = AuxiliarDAO.GetString(reader, "caix_status");
                    Caixa.SaldoInicial = AuxiliarDAO.GetFloat(reader, "caix_saldo_inicial");
                    Caixa.SaldoFinal = AuxiliarDAO.GetFloat(reader, "caix_saldo_final");
                    Caixa.TotalEntrada = AuxiliarDAO.GetFloat(reader, "caix_total_entrada");
                    Caixa.TotalSaida = AuxiliarDAO.GetFloat(reader, "caix_total_saida");
                }

                return Caixa;
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

        public List<Caixa> List()
        {
            try
            {
                List<Caixa> listaCaixa = new List<Caixa>();

                var query = conexao.Query();
                query.CommandText = "select * from Caixa";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Caixa foi encotrado!");
                }

                while (reader.Read())
                {
                    listaCaixa.Add(new Caixa()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "caix_id"),
                        Numero = AuxiliarDAO.GetInt(reader, "caix_numero"),
                        Data = AuxiliarDAO.GetDateTime(reader, "caix_data"),
                        HorarioInicial = AuxiliarDAO.GetString(reader, "caix_horario_incial"),
                        HorarioFinal = AuxiliarDAO.GetString(reader, "caix_horario_final"),
                        Status = AuxiliarDAO.GetString(reader, "caix_status"),
                        SaldoInicial = AuxiliarDAO.GetFloat(reader, "caix_saldo_inicial"),
                        SaldoFinal = AuxiliarDAO.GetFloat(reader, "caix_saldo_final"),
                        TotalEntrada = AuxiliarDAO.GetFloat(reader, "caix_total_entrada"),
                        TotalSaida = AuxiliarDAO.GetFloat(reader, "caix_total_saida")
                    });
                }

                return listaCaixa;
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
