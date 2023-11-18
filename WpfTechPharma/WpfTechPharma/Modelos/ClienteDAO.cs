using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechPharma.BancoDados;
using WpfTechPharma.Interfaces;
using WpfTechPharma.Auxiliares;
using MySql.Data.MySqlClient;
using System.Windows;

namespace WpfTechPharma.Modelos
{
    internal class ClienteDAO : IDAO<Cliente>
    {
        private static Conexao conexao;

        public ClienteDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(Cliente t)
        {
            try
            {
                var query = conexao.Query();

                query.CommandText =
                    "call cadastrar_cliente " +
                    "(@nome, " +
                    "@sexo, " +
                    "@nascimento, " +
                    "@rg, " +
                    "@cpf, " +
                    "@email, " +
                    "@contato," +
                    "@endereco)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@sexo", t.Sexo);
                query.Parameters.AddWithValue("@nascimento", t.Nascimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@contato", t.Contato);
                query.Parameters.AddWithValue("@endereco", t.Endereco.Id);

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

        public void Update(Cliente t)
        {
            try
            {
                var query = conexao.Query();

                query.CommandText =
                    "update " +
                    "Cliente " +
                    "set " +
                    "clie_nome = @nome, " +
                    "clie_sexo = @sexo, " +
                    "clie_nascimento = @nascimento, " +
                    "clie_rg = @rg, " +
                    "clie_cpf = @cpf, " +
                    "clie_email = @email, " +
                    "clie_contato = @contato, " +
                    "fk_ende_id = @endereco " +
                    "where " +
                    "(clie_id = @id)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@sexo", t.Sexo);
                query.Parameters.AddWithValue("@nascimento", t.Nascimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@contato", t.Contato);
                query.Parameters.AddWithValue("@endereco", t.Endereco.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o cliente. Verifique e tente novamente.");
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

        public void Delete(Cliente t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Cliente where (clie_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o cliente. Verifique e tente novamente.");
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

        public Cliente GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Cliente where (clie_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum cliente foi encotrado!");
                }

                var Cliente = new Cliente();

                while (reader.Read())
                {
                    Cliente.Id = AuxiliarDAO.GetInt(reader, "clie_id");
                    Cliente.Nome = AuxiliarDAO.GetString(reader, "clie_nome");
                    Cliente.Sexo = AuxiliarDAO.GetString(reader, "clie_sexo");
                    Cliente.Nascimento = AuxiliarDAO.GetDateTime(reader, "clie_nascimento");
                    Cliente.RG = AuxiliarDAO.GetString(reader, "clie_rg");
                    Cliente.CPF = AuxiliarDAO.GetString(reader, "clie_cpf");
                    Cliente.Email = AuxiliarDAO.GetString(reader, "clie_email");
                    Cliente.Contato = AuxiliarDAO.GetString(reader, "clie_contato");
                    Cliente.Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id"));
                }

                return Cliente;
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

        public List<Cliente> List()
        {
            try
            {
                List<Cliente> listaCliente = new List<Cliente>();

                var query = conexao.Query();
                query.CommandText = "select * from Cliente";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum cliente foi encotrado!");
                }

                while (reader.Read())
                {
                    listaCliente.Add(new Cliente()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "clie_id"),
                        Nome = AuxiliarDAO.GetString(reader, "clie_nome"),
                        Sexo = AuxiliarDAO.GetString(reader, "clie_sexo"),
                        Nascimento = AuxiliarDAO.GetDateTime(reader, "clie_nascimento"),
                        RG = AuxiliarDAO.GetString(reader, "clie_rg"),
                        CPF = AuxiliarDAO.GetString(reader, "clie_cpf"),
                        Email = AuxiliarDAO.GetString(reader, "clie_email"),
                        Contato = AuxiliarDAO.GetString(reader, "clie_contato"),
                        Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id"))
                    });
                }

                return listaCliente;
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
