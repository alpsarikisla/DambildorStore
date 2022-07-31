using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HortumBank.Models;

namespace HortumBank.Controllers.API
{
    public class PayServiceModel
    {
        public string MerchantCode { get; set; }
        public string MerchantPassword { get; set; }
        public string CardNo { get; set; }
        public string ExpM { get; set; }
        public string ExpY { get; set; }
        public string CVV { get; set; }
        public decimal Price { get; set; }
    }
    public class PayServiceController : ApiController
    {
        HortumBankDBEntities db = new HortumBankDBEntities();
        public IHttpActionResult PostPayService(string MerchantCode, string MerchantPassword, string CardNo, string ExpM, string ExpY, string CVV, decimal Price)
        {
            string RequestCode = "";
            int merchantCount = db.SanalPosMusteri.Count(x => x.SaticiNo == MerchantCode && x.SaticiSifre == MerchantPassword);
            if (merchantCount != 0)
            {
                SanalPosMusteri merchant = db.SanalPosMusteri.FirstOrDefault(x => x.SaticiNo == MerchantCode && x.SaticiSifre == MerchantPassword);
                int cardControl = db.MusteriHesaplar.Count(x => x.KartNumarası == CardNo);
                if (cardControl != 0)
                {
                    MusteriHesaplar mh = db.MusteriHesaplar.FirstOrDefault(x => x.KartNumarası == CardNo);
                    if (Convert.ToInt32(mh.CVV) == Convert.ToInt32(CVV))
                    {
                        if (Convert.ToInt32(mh.SonKullanmaAy) == Convert.ToInt32(ExpM))
                        {
                            if (Convert.ToInt32(mh.SonKullanmaYil) == Convert.ToInt32(ExpY))
                            {
                                if (mh.Bakiye >= Price)
                                {
                                    try
                                    {
                                        mh.Bakiye -= Price;
                                        merchant.Bakiye += Price;
                                        db.SaveChanges();
                                        RequestCode = "101";
                                    }
                                    catch
                                    {
                                        RequestCode = "301";
                                    }
                                }
                                else
                                {
                                    RequestCode = "401";
                                }
                            }
                            else
                            {
                                RequestCode = "501";
                            }
                        }
                        else
                        {
                            RequestCode = "601";
                        }
                    }
                    else
                    {
                        RequestCode = "701";
                    }
                }
                else
                {
                    RequestCode = "801";
                }
            }
            else
            {
                RequestCode = "901";
            }

            return Json(RequestCode);
        }

        public IHttpActionResult PostPayService(PayServiceModel model)
        {
            string RequestCode = "";
            int merchantCount = db.SanalPosMusteri.Count(x => x.SaticiNo == model.MerchantCode && x.SaticiSifre == model.MerchantPassword);
            if (merchantCount != 0)
            {
                SanalPosMusteri merchant = db.SanalPosMusteri.FirstOrDefault(x => x.SaticiNo == model.MerchantCode && x.SaticiSifre == model.MerchantPassword);
                int cardControl = db.MusteriHesaplar.Count(x => x.KartNumarası == model.CardNo);
                if (cardControl != 0)
                {
                    MusteriHesaplar mh = db.MusteriHesaplar.FirstOrDefault(x => x.KartNumarası == model.CardNo);
                    if (Convert.ToInt32(mh.CVV) == Convert.ToInt32(model.CVV))
                    {
                        if (Convert.ToInt32(mh.SonKullanmaAy) == Convert.ToInt32(model.ExpM))
                        {
                            if (Convert.ToInt32(mh.SonKullanmaYil) == Convert.ToInt32(model.ExpY))
                            {
                                if (mh.Bakiye >= model.Price)
                                {
                                    try
                                    {
                                        mh.Bakiye -= model.Price;
                                        merchant.Bakiye += model.Price;
                                        db.SaveChanges();
                                        RequestCode = "101";
                                    }
                                    catch
                                    {
                                        RequestCode = "301";
                                    }
                                }
                                else
                                {
                                    RequestCode = "401";
                                }
                            }
                            else
                            {
                                RequestCode = "501";
                            }
                        }
                        else
                        {
                            RequestCode = "601";
                        }
                    }
                    else
                    {
                        RequestCode = "701";
                    }
                }
                else
                {
                    RequestCode = "801";
                }
            }
            else
            {
                RequestCode = "901";
            }

            return Json(RequestCode);
        }
    }
}
