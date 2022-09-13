using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades.Twitter
{
    public class Meta
    {
        public int result_count { get; set; }
        public string newest_id { get; set; }
        public string oldest_id { get; set; }
        public string next_token { get; set; }

    }
}
