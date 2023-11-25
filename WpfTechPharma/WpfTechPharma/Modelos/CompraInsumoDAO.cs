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
    internal class CompraInsumoDAO : IDAO<CompraInsumo>
    {
        private static Conexao conexao;

        public CompraInsumoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(CompraInsumo t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar compra_insumo " +
                    "(@quantidade_item, " +
                    "@valor_item, " +
                    "@compra," +
                    "@Insumo)";

                query.Parameters.AddWithValue("@quantidade_item", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor_item", t.ValorItem);
                query.Parameters.AddWithValue("@compra", t.Compra.Id);
                query.Parameters.AddWithValue("@Insumo", t.Insumo.Id);

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

        public void Update(CompraInsumo t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Compra_Insumo " +
                    "set " +
                    "caio_quantidade_item = @quantidade_item, " +
                    "caio_valor_item = @valor_item, " +
                    "fk_comp_id = @compra, " +
                    "fk_insu_id = @Insumo " +
                    "where " +
                    "(caio_id = @id)";

                query.Parameters.AddWithValue("@data", t.QuantidadeItem);
                query.Parameters.AddWithValue("@valor", t.ValorItem);
                query.Parameters.AddWithValue("@compra", t.Compra.Id);
                query.Parameters.AddWithValue("@Insumo", t.Insumo.Id);
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

        public void Delete(CompraInsumo t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Compra_Insumo where (caio_id = @id)";

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

        public CompraInsumo GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Compra_Insumo where (caio_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                var CompraInsumo = new CompraInsumo();

                while (reader.Read())
                {
                    CompraInsumo.Id = AuxiliarDAO.GetInt(reader, "caio_id");
                    CompraInsumo.QuantidadeItem = AuxiliarDAO.GetInt(reader, "caio_quantidade_item");
                    CompraInsumo.ValorItem = AuxiliarDAO.GetFloat(reader, "caio_valor_item");
                    CompraInsumo.Compra = new CompraDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_comp_id"));
                    CompraInsumo.Insumo = new InsumoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_insu_id"));
                }

                return CompraInsumo;
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

        public List<CompraInsumo> List()
        {
            try
            {
                List<CompraInsumo> listaCompraInsumo = new List<CompraInsumo>();

                var query = conexao.Query();
                query.CommandText = "select * from Compra_Insumo";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum registro foi encotrado!");
                }

                while (reader.Read())
                {
                    listaCompraInsumo.Add(new CompraInsumo()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "caio_id"),
                        QuantidadeItem = AuxiliarDAO.GetInt(reader, "caio_quantidade_item"),
                        ValorItem = AuxiliarDAO.GetFloat(reader, "caio_valor_item"),
                        Compra = new CompraDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_comp_id")),
                        Insumo = new InsumoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_insu_id"))
                    });
                }

                return listaCompraInsumo;
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
