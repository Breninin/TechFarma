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
    internal class FornecedorDAO : IDAO<Fornecedor>
    {
        private static Conexao conexao;

        public FornecedorDAO()
        {
            conexao = new Conexao();
        }

        public void Insert(Fornecedor t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "insert into " +
                    "Fornecedor " +
                    "(forn_razao_social, " +
                    "forn_nome_fantasia, " +
                    "forn_contato, " +
                    "forn_cnpj, " +
                    "forn_email, " +
                    "fk_ende_id, " +
                    "forn_inscricao_estadual) " +
                    "values " +
                    "(@razaoSocial, " +
                    "@nomeFantasia, " +
                    "@contato, " +
                    "@cnpj, " +
                    "@email, " +
                    "@endereco, " +
                    "@inscricaoEstadual)";

                query.Parameters.AddWithValue("@razaoSocial", t.RazaoSocial);
                query.Parameters.AddWithValue("@nomeFantasia", t.NomeFantasia);
                query.Parameters.AddWithValue("@contato", t.Contato);
                query.Parameters.AddWithValue("@cnpj", t.CNPJ);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@endereco", t.Endereco.Id);
                query.Parameters.AddWithValue("@inscricaoEstadual", t.InscrcaoEstadual);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao salvar o fornecedor. Verifique o fornecedor inserido e tente novamente.");
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

        public void Update(Fornecedor t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Fornecedor " +
                    "set " +
                    "forn_razao_social = @razaoSocial, " +
                    "forn_nome_fantasia = @nomeFantasia, " +
                    "forn_contato = @contato, " +
                    "forn_cnpj = @cnpj, " +
                    "forn_email = @email, " +
                    "fk_ende_id = @endereco, " +
                    "forn_inscricao_estadual = @inscricaoEstadual " +
                    "where " +
                    "(forn_id = @id)";

                query.Parameters.AddWithValue("@razaoSocial", t.RazaoSocial);
                query.Parameters.AddWithValue("@nomeFantasia", t.NomeFantasia);
                query.Parameters.AddWithValue("@contato", t.Contato);
                query.Parameters.AddWithValue("@cnpj", t.CNPJ);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@endereco", t.Endereco.Id);
                query.Parameters.AddWithValue("@inscricaoEstadual", t.InscrcaoEstadual);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o fornecedor. Verifique e tente novamente.");
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

        public void Delete(Fornecedor t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Fornecedor where (forn_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o fornecedor. Verifique e tente novamente.");
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

        public Fornecedor GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Fornecedor where (forn_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum fornecedor foi encotrado!");
                }

                var fornecedor = new Fornecedor();

                while (reader.Read())
                {
                    fornecedor.Id = AuxiliarDAO.GetInt(reader, "forn_id");
                    fornecedor.RazaoSocial = AuxiliarDAO.GetString(reader, "forn_razao_social");
                    fornecedor.NomeFantasia = AuxiliarDAO.GetString(reader, "forn_nome_fantasia");
                    fornecedor.Contato = AuxiliarDAO.GetString(reader, "forn_contato");
                    fornecedor.CNPJ = AuxiliarDAO.GetString(reader, "forn_cnpj");
                    fornecedor.Email = AuxiliarDAO.GetString(reader, "forn_email");
                    fornecedor.Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id"));
                    fornecedor.InscrcaoEstadual = AuxiliarDAO.GetString(reader, "forn_inscricao_estadual");
                }

                return fornecedor;
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

        public List<Fornecedor> List()
        {
            try
            {
                List<Fornecedor> listaFornecedor = new List<Fornecedor>();

                var query = conexao.Query();
                query.CommandText = "select * from Fornecedor";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum fornecedor foi encotrado!");
                }

                while (reader.Read())
                {
                    listaFornecedor.Add(new Fornecedor()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "forn_id"),
                        RazaoSocial = AuxiliarDAO.GetString(reader, "forn_razao_social"),
                        NomeFantasia = AuxiliarDAO.GetString(reader, "forn_nome_fantasia"),
                        Contato = AuxiliarDAO.GetString(reader, "forn_contato"),
                        CNPJ = AuxiliarDAO.GetString(reader, "forn_cnpj"),
                        Email = AuxiliarDAO.GetString(reader, "forn_email"),
                        Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id")),
                        InscrcaoEstadual = AuxiliarDAO.GetString(reader, "forn_inscricao_estadual")
                    });
                }

                return listaFornecedor;
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
