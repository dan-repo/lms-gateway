using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Medias
{
    public class Media : BaseEntity
    {
        public byte[] File { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string MimeType { get; set; }
        public string Url { get; set; }

    }


}
