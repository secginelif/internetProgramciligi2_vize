using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketplatformm.ViewModel
{
    public class AnketModel
    {
        public string anketId { get; set; }
        public string anketKodu { get; set; }
        public string anketAdi { get; set; }
        public string anketKatId { get; set; }
        public int anketSoruSayisi { get; set; }
    }
}