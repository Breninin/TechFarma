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
    //Breno
    internal class EnderecoDAO : IDAO<Endereco>
    {
        private static Conexao conexao;

        public EnderecoDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(Endereco t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = 
                    "call cadastrar_endereco " +
                    "(@estado, " +
                    "@cidade, " +
                    "@bairro, " +
                    "@rua, " +
                    "@numero, " +
                    "@complemento, " +
                    "@cep)";

                query.Parameters.AddWithValue("@estado", t.Estado);
                query.Parameters.AddWithValue("@cidade", t.Cidade);
                query.Parameters.AddWithValue("@bairro", t.Bairro);
                query.Parameters.AddWithValue("@rua", t.Rua);
                query.Parameters.AddWithValue("@numero", t.Numero);
                query.Parameters.AddWithValue("@complemento", t.Complemento);
                query.Parameters.AddWithValue("@cep", t.CEP);

                var result = (string)query.ExecuteScalar();

                return result;

                /*
                if (result == 0)
                {
                    throw new Exception("Erro ao salvar o endereço. Verifique o endereço inserido e tente novamente.");
                }
                */
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

        public void Update(Endereco t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = 
                    "update " +
                    "Endereco " +
                    "set " +
                    "ende_estado = @estado, " +
                    "ende_cidade = @cidade, " +
                    "ende_bairro = @bairro, " +
                    "ende_rua = @rua, " +
                    "ende_numero = @numero, " +
                    "ende_complemento = @complemento, " +
                    "ende_cep = @cep " +
                    "where " +
                    "(ende_id = @id)";

                query.Parameters.AddWithValue("@estado", t.Estado);
                query.Parameters.AddWithValue("@cidade", t.Cidade);
                query.Parameters.AddWithValue("@bairro", t.Bairro);
                query.Parameters.AddWithValue("@rua", t.Rua);
                query.Parameters.AddWithValue("@numero", t.Numero);
                query.Parameters.AddWithValue("@complemento", t.Complemento);
                query.Parameters.AddWithValue("@cep", t.CEP);

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o endereço. Verifique e tente novamente.");
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

        public void Delete(Endereco t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from endereco where (ende_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o endereço. Verifique e tente novamente.");
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

        public Endereco GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Endereco where (ende_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum endereço foi encotrado!");
                }

                var endereco = new Endereco();

                while (reader.Read())
                {
                    endereco.Id = AuxiliarDAO.GetInt(reader, "ende_id");
                    endereco.Estado = AuxiliarDAO.GetString(reader, "ende_estado");
                    endereco.Cidade = AuxiliarDAO.GetString(reader, "ende_cidade");
                    endereco.Bairro = AuxiliarDAO.GetString(reader, "ende_bairro");
                    endereco.Rua = AuxiliarDAO.GetString(reader, "ende_rua");
                    endereco.Numero = AuxiliarDAO.GetInt(reader, "ende_numero");
                    endereco.Complemento = AuxiliarDAO.GetString(reader,"ende_complemento");
                    endereco.CEP = AuxiliarDAO.GetString(reader,"ende_cep");
                }

                return endereco;
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

        public List<Endereco> List()
        {
            try
            {
                List<Endereco> listaEndereco = new List<Endereco>();

                var query = conexao.Query();
                query.CommandText = "select * from Endereco";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum endereço foi encotrado!");
                }

                while (reader.Read())
                {
                    listaEndereco.Add(new Endereco()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "ende_id"),
                        Estado = AuxiliarDAO.GetString(reader, "ende_estado"),
                        Cidade = AuxiliarDAO.GetString(reader, "ende_cidade"),
                        Bairro = AuxiliarDAO.GetString(reader, "ende_bairro"),
                        Rua = AuxiliarDAO.GetString(reader, "ende_rua"),
                        Numero = AuxiliarDAO.GetInt(reader, "ende_numero"),
                        Complemento = AuxiliarDAO.GetString(reader, "ende_complemento"),
                        CEP = AuxiliarDAO.GetString(reader, "ende_cep")
                    });
                }

                return listaEndereco;
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
