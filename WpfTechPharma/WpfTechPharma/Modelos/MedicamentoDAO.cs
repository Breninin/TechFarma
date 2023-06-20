using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.BancoDados;
using WpfTechPharma.Interfaces;

namespace WpfTechPharma.Modelos
{
    internal class MedicamentoDAO : IDAO<Medicamento>
    {
        private static Conexao conexao;
        public MedicamentoDAO()
        {
            conexao = new Conexao();
        }
        public void Insert(Medicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "insert into " +
                    "Medicamento " +
                    "(medi_nome, " +
                    "medi_marca, " +
                    "medi_valor_compra, " +
                    "medi_valor_venda, " +
                    "medi_quantidade," +
                    "medi_tarja, " +
                    "medi_codigo_barra, " +
                    "fk_forn_id) " +
                    "values " +
                    "(@nome, " +
                    "@marca, " +
                    "@valor_compra, " +
                    "@valor_venda, " +
                    "@quantidade, " +
                    "@tarja, " +
                    "@codigo_barra, " +
                    "@fornecedorId)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valor_compra", t.ValorCompra);
                query.Parameters.AddWithValue("@valor_venda", t.ValorVenda);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@tarja", t.Tarja);
                query.Parameters.AddWithValue("@codigo_barra", t.CodigoBarra);
                query.Parameters.AddWithValue("@fornecedorId", t.Fornecedor.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new NotImplementedException("Erro ao salvar o medicamento.");
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

        public void Update(Medicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "Update" +
                    "Medicamento " +
                    "set" +
                    "(medi_nome = @nome, " +
                    "medi_marca = @marca, " +
                    "medi_valor_compra = @valor_compra, " +
                    "medi_valor_venda = @valor_venda, " +
                    "medi_quantidade = @quantidade," +
                    "medi_tarja = @tarja, " +
                    "medi_codigo_barra = @codigo_barra, " +
                    "fk_forn_id = @fornecedorId)" +
                    "where " +
                    "medi_id = @id";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@valor_compra", t.ValorCompra);
                query.Parameters.AddWithValue("@valor_venda", t.ValorVenda);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@tarja", t.Tarja);
                query.Parameters.AddWithValue("@codigo_barra", t.CodigoBarra);
                query.Parameters.AddWithValue("@fornecedorId", t.Fornecedor.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new NotImplementedException("Erro ao atualizar o medicamento.");
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
        public void Delete(Medicamento t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Medicamento where (medi_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o medicamento. Verifique e tente novamente.");
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

        public Medicamento GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Medicamento where (medi_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum medicamento foi encontrado!");
                }

                var Medicamento = new Medicamento();

                while (reader.Read())
                {
                    Medicamento.Id = AuxiliarDAO.GetInt(reader, "medi_id");
                    Medicamento.Nome = AuxiliarDAO.GetString(reader, "medi_nome");
                    Medicamento.Marca = AuxiliarDAO.GetString(reader, "medi_marca");
                    Medicamento.ValorCompra = AuxiliarDAO.GetFloat(reader, "medi_valor_compra");
                    Medicamento.ValorVenda = AuxiliarDAO.GetFloat(reader,"medi_valor_venda");
                    Medicamento.Quantidade = AuxiliarDAO.GetInt(reader, "medi_quantidade");
                    Medicamento.Tarja = AuxiliarDAO.GetString(reader, "medi_tarja");
                    Medicamento.CodigoBarra = AuxiliarDAO.GetString(reader, "medi_codigo_barra");
                    Medicamento.Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"));
                }

                return Medicamento;
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


        public List<Medicamento> List()
        {

            try
            {
                List<Medicamento> listaMedicamento = new List<Medicamento>();

                var query = conexao.Query();
                query.CommandText = "select * from medicamento";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum medicamento foi encotrado!");
                }

                while (reader.Read())
                {
                    listaMedicamento.Add(new Medicamento()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "medi_id"),
                        Nome = AuxiliarDAO.GetString(reader, "medi_nome"),
                        Marca = AuxiliarDAO.GetString(reader, "medi_marca"),
                        ValorCompra = AuxiliarDAO.GetFloat(reader, "medi_valor_compra"),
                        ValorVenda = AuxiliarDAO.GetFloat(reader, "medi_valor_venda"),
                        Quantidade = AuxiliarDAO.GetInt(reader, "medi_quantidade"),
                        Tarja = AuxiliarDAO.GetString(reader, "medi_tarja"),
                        CodigoBarra = AuxiliarDAO.GetString(reader, "medi_codigo_barra"),
                        Fornecedor = new FornecedorDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_forn_id"))
                    });
                }

                return listaMedicamento;

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
        //fim do MedicamentoDAO
    }
}