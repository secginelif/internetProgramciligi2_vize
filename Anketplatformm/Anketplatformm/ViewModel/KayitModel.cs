using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketplatformm.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitAnketId { get; set; }
        public string kayitKatId { get; set; }
        public string kayitKulId { get; set; }

        public KullaniciModel kulBilgi { get; set; }

        public AnketModel anketBilgi { get; set; }

        public KategoriModel katBilgi { get; set; }



    }
}