using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Auxiliares
{
    public class TipoObjeto
    {
        public object Objeto { get; set; }
        public string ObterTipo()
        {
            return Objeto?.GetType().Name;
        }
    }

    public class CarrinhoItem
    {
        public TipoObjeto TipoObjeto { get; set; }
        public int Quantidade { get; set; }
        public bool IsSelected { get; set; }
        public CarrinhoItem()
        {
            IsSelected = false;
        }
    }
}
