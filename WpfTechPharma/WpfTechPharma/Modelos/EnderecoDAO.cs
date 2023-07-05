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

        public void Insert(Endereco t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = 
                    "insert into " +
                    "Endereco " +
                    "(ende_estado, " +
                    "ende_cidade, " +
                    "ende_bairro, " +
                    "ende_rua, " +
                    "ende_numero, " +
                    "ende_complemento, " +
                    "ende_cep) " +
                    "values " +
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

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar o endereço. Verifique o endereço inserido e tente novamente.");
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
                    endereco.Id = reader.GetInt32("ende_id");
                    endereco.Estado = reader.GetString("ende_estado");
                    endereco.Cidade = reader.GetString("ende_cidade");
                    endereco.Bairro = reader.GetString("ende_bairro");
                    endereco.Rua = reader.GetString("ende_rua");
                    endereco.Numero = reader.GetInt32("ende_numero");
                    endereco.Complemento = reader.GetString("ende_complemento");
                    endereco.CEP = reader.GetString("ende_cep");
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
                        Id = reader.GetInt32("ende_id"),
                        Estado = reader.GetString("ende_estado"),
                        Cidade = reader.GetString("ende_cidade"),
                        Bairro = reader.GetString("ende_bairro"),
                        Rua = reader.GetString("ende_rua"),
                        Numero = reader.GetInt32("ende_numero"),
                        Complemento = reader.GetString("ende_complemento"),
                        CEP = reader.GetString("ende_cep")
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
