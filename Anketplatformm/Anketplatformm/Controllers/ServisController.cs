using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Anketplatformm.Models;
using Anketplatformm.ViewModel;


namespace Anketplatformm.Controllers
{
    public class ServisController : ApiController
    {
        DB03Entities db = new DB03Entities();
        SonucModel sonuc = new SonucModel();

        #region Anket

        [HttpGet]
        [Route("api/anketliste")]

        public List<AnketModel> AnketListe()
        {
            List<AnketModel> liste = db.Anket.Select(x => new AnketModel()
            {
                anketId = x.anketId,
                anketKodu = x.anketKodu,
                anketAdi = x.anketAdi,
                anketSoruSayisi = x.anketSoruSayisi
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/anketbyid/{anketId}")]

        public AnketModel AnketById(string anketId)
        {
            AnketModel kayit = db.Anket.Where(s => s.anketAdi == anketId).Select(x => new
            AnketModel()
            {
                anketId = x.anketId,
                anketKodu = x.anketKodu,
                anketAdi = x.anketAdi,
                anketSoruSayisi = x.anketSoruSayisi
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/anketekle")]

        public SonucModel AnketEkle(AnketModel model)
        {
            if (db.Anket.Count(s => s.anketKodu == model.anketKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Anket Kodu Sistemde Kayıtlıdır.";
                return sonuc;
            }

            Anket yeni = new Anket() { };
            yeni.anketId = Guid.NewGuid().ToString();
            yeni.anketKodu = model.anketKodu;
            yeni.anketAdi = model.anketAdi;
            yeni.anketSoruSayisi = model.anketSoruSayisi;
            db.Anket.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Anket Eklendi";
            return sonuc;

        }

        [HttpPut]
        [Route("api/anketduzenle")]

        public SonucModel AnketDuzenle(AnketModel model)
        {
            Anket kayit = db.Anket.Where(s => s.anketId == model.anketId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı.";
                return sonuc;
            }

            kayit.anketKodu = model.anketKodu;
            kayit.anketAdi = model.anketAdi;
            kayit.anketSoruSayisi = model.anketSoruSayisi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kayıt Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/anketsil/{anketId}")]

        public SonucModel AnketSil(string anketId)
        {
            Anket kayit = db.Anket.Where(s => s.anketId == anketId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sistemde Kayıt Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitAnketId == anketId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıtlı Kullanıcı Olduğu İçin Silinemez";
                return sonuc;
            }

            db.Anket.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sistemden Kayıt Silindi";
            return sonuc;
        }
        #endregion

        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]

        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]

        public KategoriModel KategoriById(string katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.katAdi == katId).Select(x => new 
            KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi,
                
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]

        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.katId == model.katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Id Sistemde Kayıtlıdır.";
                return sonuc;
            }

            Kategori yeni = new Kategori() { };
            yeni.katId = Guid.NewGuid().ToString();
            yeni.katAdi = model.katAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;

        }

        [HttpPut]
        [Route("api/anketduzenle")]

        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == model.katId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı.";
                return sonuc;
            }

            kayit.katId = model.katId;
            kayit.katAdi = model.katAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/kategorisil/{katId}")]

        public SonucModel KategoriSil(string katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == katId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sistemde Kategori Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitKatId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıtlı Anket Olduğu İçin Silinemez";
                return sonuc;
            }

            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sistemden Kategori Silindi";
            return sonuc;
        }





        #endregion

        #region Kullanici

        [HttpGet]
        [Route("api/kullaniciliste")]

        public List<KullaniciModel> KullaniciListe()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()
            {
                kulId = x.kulId,
                kulNo = x.kulNo,
                kulAdsoyad = x.kulAdsoyad,
                kulTarih = x.kulTarih,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kullanicibyid/{kulId}")]

        public KullaniciModel KullaniciById(string kulId)
        {
            KullaniciModel kayit = db.Kullanici.Where(s => s.kulId == kulId).Select(x => new KullaniciModel()
            {
                kulId = x.kulId,
                kulNo = x.kulNo,
                kulAdsoyad = x.kulAdsoyad,
                kulTarih = x.kulTarih,
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kullaniciekle")]

        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.kulNo == model.kulNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Numarası Kayıtlıdır.";
            }

            Kullanici yeni = new Kullanici() { };
            yeni.kulId = Guid.NewGuid().ToString();
            yeni.kulNo = model.kulNo;
            yeni.kulAdsoyad = model.kulAdsoyad;
            yeni.kulTarih = model.kulTarih;
            db.Kullanici.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sisteme Kullanıcı Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/kullaniciduzenle")]

        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.kulId == model.kulId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı.";
                return sonuc;
            }

            kayit.kulNo = model.kulNo;
            kayit.kulAdsoyad = model.kulAdsoyad;
            kayit.kulTarih = model.kulTarih;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kayıt Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kullanisil")]

        public SonucModel KullaniciSil(string kulId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.kulId == kulId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sistemde Kayıt Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitKulId == kulId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Üzerinde Anket Kayıtlı Olduğundan Silinemez";
                return sonuc;
            }

            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sistemden Kullanıcı Silindi";
            return sonuc;
        }

        #endregion

        #region Kayit

        [HttpGet]
        [Route("api/kullanicianketliste/{kulId}")]

        public List<KayitModel> KullaniciAnketListe(string kulId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitKulId == kulId).Select(x => new
                KayitModel()
            {
                kayitId = x.kayitId,
                kayitAnketId = x.kayitAnketId,
                kayitKatId = x.kayitKatId,
                kayitKulId = x.kayitKulId

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.kulBilgi = KullaniciById(kayit.kayitKulId);
                kayit.anketBilgi = AnketById(kayit.kayitAnketId);
                kayit.katBilgi = KategoriById(kayit.kayitKatId);
            }

            return liste;
        }

        [HttpGet]
        [Route("api/anketkullaniciliste/{anketId}")]

        public List<KayitModel> AnketKullaniciListe(string kulId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitKulId == kulId).Select(x => new
                KayitModel()
            {
                kayitId = x.kayitId,
                kayitAnketId = x.kayitAnketId,
                kayitKulId = x.kayitKulId

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.kulBilgi = KullaniciById(kayit.kayitKulId);
                kayit.anketBilgi = AnketById(kayit.kayitAnketId);
            }

            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]

        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitAnketId == model.kayitAnketId && s.kayitKulId ==
           model.kayitKulId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Tarafından Anket Önceden Oluşturmuştur";
                return sonuc;
            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitKulId = model.kayitKulId;
            yeni.kayitAnketId = model.kayitAnketId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Anket Kaydı Oluşturulmuştur";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]

        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Anket Kaydı Silinmiştir";
            return sonuc;
        }

        #endregion


    }


}
