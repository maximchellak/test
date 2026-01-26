using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarNet.JWTManager
{
    public class TokenInfo
    {
        public string Address { get; set; }
        public int UserId { get; set; }
        public string TokenIssuerName { get; set; }
        public string SecurityKey { get; set; }
    }
}
