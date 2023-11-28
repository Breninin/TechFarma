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

        public string Insert(Insumo t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_insumo " +
                    "(@nome, " +
                    "@marca, " +
                    "@valorCompra, " +
                    "@quantidade, " +
                    "@codigoBarra, " +
                    "@fornecedor)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valorCompra", t.ValorCompra);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@codigoBarra", t.CodigoBarra);
                query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);

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
                    "insu_codigo_barra = @codigoBarra, " +
                    "fk_forn_id = @fornecedor " +
                    "where " +
                    "(insu_id = @id)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valorCompra", t.ValorCompra);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@codigoBarra", t.CodigoBarra);
                query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);
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
                    insumo.Nome = AuxiliarDAO.GetString(reader, "insu_nome");
                    insumo.Marca = AuxiliarDAO.GetString(reader, "insu_marca");
                    insumo.ValorCompra = AuxiliarDAO.GetFloat(reader, "insu_valor_compra");
                    insumo.Quantidade = AuxiliarDAO.GetInt(reader, "insu_quantidade");
                    insumo.CodigoBarra = AuxiliarDAO.GetString(reader, "insu_codigo_barra");
                    insumo.Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"));
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
                        Nome = AuxiliarDAO.GetString(reader, "insu_nome"),
                        Marca = AuxiliarDAO.GetString(reader, "insu_marca"),
                        ValorCompra = AuxiliarDAO.GetFloat(reader, "insu_valor_compra"),
                        Quantidade = AuxiliarDAO.GetInt(reader, "insu_quantidade"),
                        CodigoBarra = AuxiliarDAO.GetString(reader, "insu_codigo_barra"),
                        Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"))
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

