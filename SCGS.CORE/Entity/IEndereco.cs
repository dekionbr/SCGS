using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGS.CORE.Entity
{
    public interface IEndereco
    {
        Cidade Cidade { get; set; }
        int CEP { get; set; }
        TipoLogradrouro TipoLogradouro { get; set; }
        string Logradouro { get; set; }
        int Numero { get; set; }
        string Complemento { get; set; }
        //string Endereco { get; set; }
    }
}
