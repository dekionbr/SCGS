using System;

namespace SCGS.WEB.Models
{
    [Serializable]
    public class RelatorioModel
    {
        public String[] labels { get; set; }
        public DataSetModel[] datasets { get; set;}

    }
}