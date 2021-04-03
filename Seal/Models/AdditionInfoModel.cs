using System;
using System.Collections.Generic;

namespace Seal.Models
{
    public class AdditionalInfo
    {
        public byte[] MainImage { get; set; }
        public List<string> Cusines { get; set; }
        public AdditionalInfo()
        {
        }
    }
}
