using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechPharma.BancoDados;
using WpfTechPharma.Interfaces;
using WpfTechPharma.Auxiliares;
using MySql.Data.MySqlClient;

namespace WpfTechPharma.Modelos
{
    internal class FuncionarioDAO : IDAO<Funcionario>
    {
        private static Conexao conexao;

        public FuncionarioDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(Funcionario t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Funcionario " +
                    "(func_nome, " +
                    "func_sexo, " +
                    "func_nascimento, " +
                    "func_rg, " +
                    "func_cpf, " +
                    "func_email, " +
                    "func_contato," +
                    "func_funcao, " +
                    "func_salario, " +
                    "fk_ende_id) " +
                    "values " +
                    "(@nome, " +
                    "@sexo, " +
                    "@nascimento, " +
                    "@rg, " +
                    "@cpf, " +
                    "@email, " +
                    "@contato," +
                    "@funcao, " +
                    "@salario," +
                    "@endereco)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@sexo", t.Sexo);
                query.Parameters.AddWithValue("@nascimento", t.Nascimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@contato", t.Contato);
                query.Parameters.AddWithValue("@funcao", t.Funcao);
                query.Parameters.AddWithValue("@salario", t.Salario);
                query.Parameters.AddWithValue("@endereco", t.Endereco.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar o Funcionario. Verifique o Funcionario inserido e tente novamente.");
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

        public void Update(Funcionario t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Funcionario " +
                    "set " +
                    "func_nome = @nome, " +
                    "func_sexo = @sexo, " +
                    "func_nascimento = @nascimento, " +
                    "func_rg = @rg, " +
                    "func_cpf = @cpf, " +
                    "func_email = @email, " +
                    "func_contato = @contato, " +
                    "func_funcao = @funcao, " +
                    "func_salario = @salario, " +
                    "fk_ende_id = @endereco " +
                    "where " +
                    "(func_id = @id)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@sexo", t.Sexo);
                query.Parameters.AddWithValue("@nascimento", t.Nascimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@contato", t.Contato);
                query.Parameters.AddWithValue("@funcao", t.Funcao);
                query.Parameters.AddWithValue("@salario", t.Salario);
                query.Parameters.AddWithValue("@endereco", t.Endereco.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o Funcionario. Verifique e tente novamente.");
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

        public void Delete(Funcionario t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Funcionario where (func_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o Funcionario. Verifique e tente novamente.");
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

        public Funcionario GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Funcionario where (func_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Funcionario foi encotrado!");
                }

                var Funcionario = new Funcionario();

                while (reader.Read())
                {
                    Funcionario.Id = reader.GetInt32("func_id");
                    Funcionario.Nome = AuxiliarDAO.GetString(reader, "func_nome");
                    Funcionario.Sexo = AuxiliarDAO.GetString(reader, "func_sexo");
                    Funcionario.Nascimento = AuxiliarDAO.GetDateTime(reader, "func_nascimento");
                    Funcionario.RG = AuxiliarDAO.GetString(reader, "func_rg");
                    Funcionario.CPF = AuxiliarDAO.GetString(reader, "func_cpf");
                    Funcionario.Email = AuxiliarDAO.GetString(reader, "func_email");
                    Funcionario.Contato = AuxiliarDAO.GetString(reader, "func_contato");
                    Funcionario.Funcao = AuxiliarDAO.GetString(reader, "func_funcao");
                    Funcionario.Salario = reader.GetFloat("func_salario");
                    Funcionario.Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id"));
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

        public List<Funcionario> List()
        {
            try
            {
                List<Funcionario> listaFuncionario = new List<Funcionario>();

                var query = conexao.Query();
                query.CommandText = "select * from Funcionario";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Funcionario foi encotrado!");
                }

                while (reader.Read())
                {
                    listaFuncionario.Add(new Funcionario()
                    {
                        Id = reader.GetInt32("func_id"),
                        Nome = AuxiliarDAO.GetString(reader, "func_nome"),
                        Sexo = AuxiliarDAO.GetString(reader, "func_sexo"),
                        Nascimento = AuxiliarDAO.GetDateTime(reader, "func_nascimento"),
                        RG = AuxiliarDAO.GetString(reader, "func_rg"),
                        CPF = AuxiliarDAO.GetString(reader, "func_cpf"),
                        Email = AuxiliarDAO.GetString(reader, "func_email"),
                        Contato = AuxiliarDAO.GetString(reader, "func_contato"),
                        Funcao = AuxiliarDAO.GetString(reader, "func_funcao"),
                        Salario = reader.GetFloat("func_salario"),
                        Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id"))
                });
                }

                return listaFuncionario;
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
