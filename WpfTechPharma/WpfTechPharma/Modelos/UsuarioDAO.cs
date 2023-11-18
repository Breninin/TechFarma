using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Esf;
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
    internal class UsuarioDAO : IDAO<Usuario>
    {
        private static Conexao conexao;

        public UsuarioDAO()
        {
            conexao = new Conexao();
        }

        public string Insert(Usuario t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_usuario " +
                    "(@usuario, " +
                    "@senha, " +
                    "@funcionarioId)";

                query.Parameters.AddWithValue("@usuario", t.NomeUsuario);
                query.Parameters.AddWithValue("@senha", t.Senha);
                query.Parameters.AddWithValue("@funcionarioId", t.Funcionario.Id);

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

        public void Update(Usuario t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Usuario " +
                    "set " +
                    "usua_login = @usuario, " +
                    "usua_senha = @senha, " +
                    "fk_func_id = @funcionarioId " +
                    "where " +
                    "(usua_id = @id)";

                query.Parameters.AddWithValue("@usuario", t.NomeUsuario);
                query.Parameters.AddWithValue("@senha", t.Senha);
                query.Parameters.AddWithValue("@funcionarioId", t.Funcionario.Id);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o usuário. Verifique e tente novamente.");
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

        public void Delete(Usuario t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Usuario where (usua_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o usuario. Verifique e tente novamente.");
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

        public Usuario GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Usuario where (usua_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum usuario foi encotrado!");
                }

                var Usuario = new Usuario();

                while (reader.Read())
                {
                    Usuario.Id = AuxiliarDAO.GetInt(reader, "usua_id");
                    Usuario.NomeUsuario = AuxiliarDAO.GetString(reader, "usua_login");
                    Usuario.Senha = AuxiliarDAO.GetString(reader, "usua_senha");
                    
                    var id_func = AuxiliarDAO.GetInt(reader, "fk_func_id");
                    Usuario.Funcionario = new FuncionarioDAO().GetById(id_func);
                }

                return Usuario;
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

        public List<Usuario> List()
        {
            try
            {
                List<Usuario> listaUsuario = new List<Usuario>();

                var query = conexao.Query();
                query.CommandText = "select * from Usuario";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum usuario foi encotrado!");
                }

                while (reader.Read())
                {
                   var id_func = AuxiliarDAO.GetInt(reader, "fk_func_id");

                   listaUsuario.Add(new Usuario()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "usua_id"),
                        NomeUsuario = AuxiliarDAO.GetString(reader, "usua_login"),
                        Senha = AuxiliarDAO.GetString(reader, "usua_senha"),

                        Funcionario = new FuncionarioDAO().GetById(id_func)
                    });
                }

                return listaUsuario;
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
