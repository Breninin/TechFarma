using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
                    "medi_peso_volume, " +
                    "medi_valor_compra, " +
                    "medi_valor_venda, " +
                    "medi_quantidade" +
                    "medi_tarja, " +
                    "medi_codigo_barra, " +
                    "fk_forn_id) " +
                    "values " +
                    "(@nome, " +
                    "@marca, " +
                    "@peso_volume, " +
                    "@valor_compra, " +
                    "@valor_venda, " +
                    "@quantidade, " +
                    "@tarja, " +
                    "@codigo_barra, " +
                    "@fornecedorId)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@peso_volume", t.Peso_Volume);
                query.Parameters.AddWithValue("@valor_compra", t.Valor_Compra);
                query.Parameters.AddWithValue("@valor_venda", t.Valor_Venda);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@tarja", t.Tarja);
                query.Parameters.AddWithValue("@codigo_barra", t.Codigo_Barra);
                query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new NotImplementedException("Erro ao salvar o medicamento.");
                }

            } catch (Exception e) {
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
                    "medi_peso_volume = @peso_volume, " +
                    "medi_valor_compra = @valor_compra, " +
                    "medi_valor_venda = @valor_venda, " +
                    "medi_quantidade = @quantidade" +
                    "medi_tarja = @tarja, " +
                    "medi_codigo_barra = @codigo_barra, " +
                    "fk_forn_id = @fornecedorId)" +
                    "where " +
                    "medi_id = @id" ;

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@peso_volume", t.Peso_Volume);
                query.Parameters.AddWithValue("@valor_compra", t.Valor_Compra);
                query.Parameters.AddWithValue("@valor_venda", t.Valor_Venda);
                query.Parameters.AddWithValue("@quantidade", t.Quantidade);
                query.Parameters.AddWithValue("@tarja", t.Tarja);
                query.Parameters.AddWithValue("@codigo_barra", t.Codigo_Barra);
                query.Parameters.AddWithValue("@fornecedor", t.Fornecedor.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new NotImplementedException("Erro ao atualizar o Medicamento.");
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
                    throw new Exception("Erro ao remover o Medicamento. Verifique e tente novamente.");
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
                    throw new Exception("Nenhum Medicamento foi encotrado!");
                }

                var Medicamento = new Medicamento();

                while (reader.Read())
                {
                    Medicamento.Id = reader.GetInt32("func_id");
                    Medicamento.Nome = AuxiliarDAO.GetString(reader, "func_nome");
                    Medicamento.Marca = AuxiliarDAO.GetString(reader, "func_sexo");
                    Medicamento.Peso_Volume = AuxiliarDAO.GetDateTime(reader, "func_nascimento");
                    Medicamento.Valor_Compra = AuxiliarDAO.GetString(reader, "func_rg");
                    Medicamento.Valor_Venda = AuxiliarDAO.GetString(reader, "func_cpf");
                    Funcionario.Quantidade = AuxiliarDAO.GetString(reader, "func_email");
                    Funcionario.Tarja = AuxiliarDAO.GetString(reader, "func_contato");
                    Funcionario.Codigo_Barra = AuxiliarDAO.GetString(reader, "func_funcao");
                    Funcionario.Fornecedor = reader.GetFloat("func_salario");
                }

                return Funcionario;
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
            throw new NotImplementedException();
        }


        //fim do MedicamentoDAO
    }
}
