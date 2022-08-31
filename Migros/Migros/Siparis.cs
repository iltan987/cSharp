using Newtonsoft.Json;
using System;

namespace Migros
{
    public class Siparis
    {
        public Siparis(ulong id, ulong sipNo, decimal puan, decimal kullanilan, DateTime islemTarihi)
        {
            Id = id;
            SipNo = sipNo;
            Puan = puan;
            Kullanilan = kullanilan;
            IslemTarihi = islemTarihi;
        }

        [JsonIgnore]
        public ulong Id { get; set; }

        public ulong SipNo { get; set; }
        public decimal Puan { get; set; }
        public decimal Kullanilan { get; set; }
        public DateTime IslemTarihi { get; set; }

        [JsonIgnore]
        public decimal TL => Puan * Globals.settings.puanCarpani;
    }
}