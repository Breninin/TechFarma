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
    internal class InsumoServicoDAO : IDAO<InsumoServico>
    {
        private static Conexao conexao;

        public InsumoServicoDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(InsumoServico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Insumo_Servico " +
                    "(ioso_quantidade_insumo, " +
                    "fk_insu_id, " +
                    "fk_serv_id) " +
                    "values " +
                    "(@quantidade_insumo, " +
                    "@Insumo," +
                    "@Servico)";

                query.Parameters.AddWithValue("@quantidade_insumo", t.QuantidadeInsumo);
                query.Parameters.AddWithValue("@Insumo", t.Insumo.Id);
                query.Parameters.AddWithValue("@Servico", t.Servico.Id);

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

        public void Update(InsumoServico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Insumo_Servico " +
                    "set " +
                    "ioso_quantidade_insumo = @quantidade_insumo, " +
                    "fk_insu_id = @Insumo, " +
                    "fk_serv_id = @Servico " +
                    "where " +
                    "(ioso_id = @id)";

                query.Parameters.AddWithValue("@data", t.QuantidadeInsumo);
                query.Parameters.AddWithValue("@Insumo", t.Insumo.Id);
                query.Parameters.AddWithValue("@Servico", t.Servico.Id);
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

        public void Delete(InsumoServico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Insumo_Servico where (ioso_id = @id)";

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

        public InsumoServico GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Insumo_Servico where (ioso_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                var InsumoServico = new InsumoServico();

                while (reader.Read())
                {
                    InsumoServico.Id = AuxiliarDAO.GetInt(reader, "ioso_id");
                    InsumoServico.QuantidadeInsumo = AuxiliarDAO.GetInt(reader, "ioso_quantidade_insumo");
                    InsumoServico.Insumo = new InsumoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_insu_id"));
                    InsumoServico.Servico = new ServicoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_serv_id"));
                }

                return InsumoServico;
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

        public List<InsumoServico> List()
        {
            try
            {
                List<InsumoServico> listaInsumoServico = new List<InsumoServico>();

                var query = conexao.Query();
                query.CommandText = "select * from Insumo_Servico";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                while (reader.Read())
                {
                    listaInsumoServico.Add(new InsumoServico()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "ioso_id"),
                        QuantidadeInsumo = AuxiliarDAO.GetInt(reader, "ioso_quantidade_insumo"),
                        Insumo = new InsumoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_insu_id")),
                        Servico = new ServicoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_serv_id"))
                    });
                }

                return listaInsumoServico;
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
