using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGS.CORE.Entity
{
    public class SistemaExterno : EntidadeBase, IEndereco
    {
        public virtual int CNPJ { get; set; }

        public virtual string Login { get; set; }

        public virtual string Senha { get; set; }

        #region Endereço

        public virtual int CEP { get; set; }

        public virtual TipoLogradrouro TipoLogradouro { get; set; }

        public virtual string Logradouro { get; set; }

        public virtual int Numero { get; set; }

        public virtual string Complemento { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual string Endereco { get; set; }

        #endregion        
    }
}
