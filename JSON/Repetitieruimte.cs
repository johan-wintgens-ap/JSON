using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;

namespace JSON
{
    public class Rootobject
    {
        public Paging paging { get; set; }
        public Datum[] data { get; set; }
    }

    public class Paging
    {
        public int records { get; set; }
        public int pages { get; set; }
        public int pageCurrent { get; set; }
        public object pageNext { get; set; }
        public object pagePrev { get; set; }
        public int pageSize { get; set; }
    }

    public class Datum
    {
        public string objectid { get; set; }
        public string point_lat { get; set; }
        public string point_lng { get; set; }
        public string id { get; set; }
        public string thema { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public string naam { get; set; }
        public string straat { get; set; }
        public string huisnummer { get; set; }
        public string postcode { get; set; }
        public string district { get; set; }
        public object lgst_niv { get; set; }
        public object hgst_niv { get; set; }
        public object grondopp { get; set; }
        public object gemetenopp { get; set; }
        public object gebo { get; set; }
        public object pero { get; set; }
        public string begindatum { get; set; }
        public object capa_zit { get; set; }
        public string capa_staan { get; set; }
        public object groep_0tot5jaar { get; set; }
        public object groep_6tot11jaar { get; set; }
        public object groep_12tot17jaar { get; set; }
        public object groep_18plus { get; set; }
        public Location locatie { get; set; }
    }
}
