using Newtonsoft.Json;
using System;

namespace Migros
{
    public class Siparis
    {
        public Siparis(ulong id, ulong sipNo, long puan, long kullanilan, DateTime islemTarihi)
        {
            Id = id;
            SipNo = sipNo;
            Puan = puan;
            Kullanilan = kullanilan;
            IslemTarihi = islemTarihi;
        }

        [JsonIgnore]
        internal ulong Id { get; set; }

        internal ulong SipNo { get; set; }
        internal long Puan { get; set; }

        [JsonIgnore]
        internal decimal TL => Puan * Globals.settings.puanCarpani;

        internal long Kullanilan { get; set; }
        internal DateTime IslemTarihi { get; set; }
    }
}