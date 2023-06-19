using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public void Update(Fornecedor t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Fornecedor t)
        {
            throw new NotImplementedException();
        }

        public Fornecedor GetById(int id)
        {
            throw new NotImplementedException();
        } 

        public List<Fornecedor> List()
        {
            throw new NotImplementedException();
        }
    }
}
