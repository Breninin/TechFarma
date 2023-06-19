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
    internal class InsumoDAO : IDAO<Insumo>
    {
        private static Conexao conexao;

        public InsumoDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(Insumo t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Insumo " +
                    "(insu_nome, " +
                    "insu_marca, " +
                    "insu_valor_compra, " +
                    "insu_quantidade, " +
                    "insu_tipo, " +
                    "insu_codigo_barra) " +
                    "values " +
                    "(@nome, " +
                    "@marca, " +
                    "@valorCompra, " +
                    "@quantidade, " +
                    "@tipo, " +
                    "@codigoBarra)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valorCompra", t.ValorCompra);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@tipo", t.Tipo);
                query.Parameters.AddWithValue("@codigoBarra", t.CodigoBarra);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar o insumo. Verifique o insumo inserido e tente novamente.");
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

        public void Update(Insumo t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Insumo " +
                    "set " +
                    "insu_nome = @nome, " +
                    "insu_marca = @marca, " +
                    "insu_valor_compra = @valorCompra, " +
                    "insu_quantidade = @quantidade, " +
                    "insu_tipo = @tipo, " +
                    "insu_codigo_barra = @codigoBarra, " +
                    "where " +
                    "(insu_id = @id)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valorCompra", t.ValorCompra);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@tipo", t.Tipo);
                query.Parameters.AddWithValue("@codigoBarra", t.CodigoBarra);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o insumo. Verifique e tente novamente.");
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

        public void Delete(Insumo t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Insumo where (insu_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o insumo. Verifique e tente novamente.");
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

        public Insumo GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Insumo where (insu_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum insumo foi encotrado!");
                }

                var insumo = new Insumo();

                while (reader.Read())
                {
                    insumo.Id = AuxiliarDAO.GetInt(reader, "insu_id");
                    insumo.Nome = AuxiliarDAO.GetString(reader, "clie_nome");
                    insumo.Marca = AuxiliarDAO.GetString(reader, "insu_marca");
                    insumo.ValorCompra = AuxiliarDAO.GetFloat(reader, "insu_valor_compra");
                    insumo.Quantidade = AuxiliarDAO.GetInt(reader, "insu_quantidade");
                    insumo.Tipo = AuxiliarDAO.GetString(reader, "insu_tipo");
                    insumo.CodigoBarra = AuxiliarDAO.GetString(reader, "insu_codigo_barra");
                }

                return insumo;
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

        public List<Insumo> List()
        {
            try
            {
                List<Insumo> listaInsumo = new List<Insumo>();

                var query = conexao.Query();
                query.CommandText = "select * from Insumo";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum insumo foi encotrado!");
                }

                while (reader.Read())
                {
                    listaInsumo.Add(new Insumo()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "insu_id"),
                        Nome = AuxiliarDAO.GetString(reader, "clie_nome"),
                        Marca = AuxiliarDAO.GetString(reader, "insu_marca"),
                        ValorCompra = AuxiliarDAO.GetFloat(reader, "insu_valor_compra"),
                        Quantidade = AuxiliarDAO.GetInt(reader, "insu_quantidade"),
                        Tipo = AuxiliarDAO.GetString(reader, "insu_tipo"),
                        CodigoBarra = AuxiliarDAO.GetString(reader, "insu_codigo_barra")
                    });
                }

                return listaInsumo;
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
