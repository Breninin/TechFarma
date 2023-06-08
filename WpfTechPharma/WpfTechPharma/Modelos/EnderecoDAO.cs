using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechPharma.BancoDados;
using WpfTechPharma.Interfaces;

namespace WpfTechPharma.Modelos
{
    //Breno fazer
    internal class EnderecoDAO : IDAO<Endereco>
    {
        private static Conexao conexao = new Conexao();

        public void Insert(Endereco t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "insert into endereco (ende_estado, ende_cidade, ende_bairro, ende_rua, ende_numero, ende_complemento, ende_cep) "
                    + "values (@estado, @cidade, @bairro, @rua, @numero, @complemento, @cep)";

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
        }

        public void Update(Endereco t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Endereco t)
        {
            throw new NotImplementedException();
        }

        public Endereco GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Endereco> List()
        {
            throw new NotImplementedException();
        }
    }
}
