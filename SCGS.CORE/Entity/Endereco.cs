using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public class Endereco : EntidadeBase, IEndereco
    {
        public virtual int CEP { get; set; }
        public virtual TipoLogradrouro TipoLogradouro { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual int Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual MicroArea MicroArea { get; set; }
        public virtual string Bairro { get; set; }

        //public virtual string Endereco
        //{
        //    get
        //    {
        //        //R. Severino Filho, 630 - Sen. Camará, Rio de Janeiro - RJ, 21831-380
        //        return string.Concat(TipoLogradouro.ToString(), Logradouro, Numero, " - ", Cidade.Nome, " - ", Cidade.Estado.UF, CEP.ToString("00000-000"));
        //    }
        //}

    }
}
