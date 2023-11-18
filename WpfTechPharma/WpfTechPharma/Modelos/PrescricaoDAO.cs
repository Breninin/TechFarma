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
    internal class PrescricaoDAO : IDAO<Prescricao>
    {
        private static Conexao conexao;

        public PrescricaoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(Prescricao t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_prescricao " +
                    "(@data, " +
                    "@patologia, " +
                    "@vencimento, " +
                    "@nome_emissor, " +
                    "@clinica_emissora, " +
                    "@scan_documento, " +
                    "@Cliente," +
                    "@Medicamento," +
                    "@Funcionario," +
                    "@Venda)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@patologia", t.Patologia);
                query.Parameters.AddWithValue("@vencimento", t.Vencimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@nome_emissor", t.NomeEmissor);
                query.Parameters.AddWithValue("@clinica_emissora", t.ClinicaEmissora);
                query.Parameters.AddWithValue("@scan_documento", t.ScanDocumento);
                query.Parameters.AddWithValue("@Cliente", t.Cliente.Id);
                query.Parameters.AddWithValue("@Medicamento", t.Medicamento.Id);
                query.Parameters.AddWithValue("@Funcionario", t.Funcionario.Id);
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

        public void Update(Prescricao t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Prescricao " +
                    "set " +
                    "pres_data = @data, " +
                    "pres_patologia = @patologia, " +
                    "pres_vencimento = @vencimento, " +
                    "pres_nome_emissor = @nome_emissor, " +
                    "pres_clinica_emissora = @clinica_emissora, " +
                    "pres_scan_documento = @scan_documento, " +
                    "fk_clie_id = @Cliente, " +
                    "fk_medi_id = @Medicamento " +
                    "fk_func_id = @Funcionario, " +
                    "fk_vend_id = @Venda " +
                    "where " +
                    "(pres_id = @id)";

                query.Parameters.AddWithValue("@data", t.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@patologia", t.Patologia);
                query.Parameters.AddWithValue("@vencimento", t.Vencimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@nome_emissor", t.NomeEmissor);
                query.Parameters.AddWithValue("@clinica_emissora", t.ClinicaEmissora);
                query.Parameters.AddWithValue("@scan_documento", t.ScanDocumento);
                query.Parameters.AddWithValue("@Cliente", t.Cliente.Id);
                query.Parameters.AddWithValue("@Medicamento", t.Medicamento.Id);
                query.Parameters.AddWithValue("@Funcionario", t.Funcionario.Id);
                query.Parameters.AddWithValue("@Venda", t.Venda.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar a Prescricao. Verifique e tente novamente.");
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

        public void Delete(Prescricao t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Prescricao where (pres_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover a Prescricao. Verifique e tente novamente.");
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

        public Prescricao GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Prescricao where (pres_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Prescricao foi encotrado!");
                }

                var Prescricao = new Prescricao();

                while (reader.Read())
                {
                    Prescricao.Id = AuxiliarDAO.GetInt(reader, "pres_id");
                    Prescricao.Data = AuxiliarDAO.GetDateTime(reader, "pres_data");
                    Prescricao.Patologia = AuxiliarDAO.GetString(reader, "pres_patologia");
                    Prescricao.Vencimento = AuxiliarDAO.GetDateTime(reader, "pres_vencimento");
                    Prescricao.NomeEmissor = AuxiliarDAO.GetString(reader, "pres_nome_emissor");
                    Prescricao.ClinicaEmissora = AuxiliarDAO.GetString(reader, "pres_clinica_emissora");
                    Prescricao.ScanDocumento = AuxiliarDAO.GetString(reader, "pres_scan_documento");
                    Prescricao.Cliente = new ClienteDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_clie_id"));
                    Prescricao.Medicamento = new MedicamentoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_medi_id"));
                    Prescricao.Funcionario = new FuncionarioDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_func_id"));
                    Prescricao.Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id"));
                }

                return Prescricao;
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

        public List<Prescricao> List()
        {
            try
            {
                List<Prescricao> listaPrescricao = new List<Prescricao>();

                var query = conexao.Query();
                query.CommandText = "select * from Prescricao";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Prescricao foi encotrado!");
                }

                while (reader.Read())
                {
                    listaPrescricao.Add(new Prescricao()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "pres_id"),
                        Data = AuxiliarDAO.GetDateTime(reader, "pres_data"),
                        Patologia = AuxiliarDAO.GetString(reader, "pres_patologia"),
                        Vencimento = AuxiliarDAO.GetDateTime(reader, "pres_vencimento"),
                        NomeEmissor = AuxiliarDAO.GetString(reader, "pres_nome_emissor"),
                        ClinicaEmissora = AuxiliarDAO.GetString(reader, "pres_clinica_emissora"),
                        ScanDocumento = AuxiliarDAO.GetString(reader, "pres_scan_documento"),
                        Cliente = new ClienteDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_clie_id")),
                        Medicamento = new MedicamentoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_medi_id")),
                        Funcionario = new FuncionarioDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_func_id")),
                        Venda = new VendaDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_vend_id"))
                    });
                }

                return listaPrescricao;
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
