using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface  IHelperService
    {
        public string EncryptString(string key, string plainText);
        public string DecryptString(string key, string cipherText);
    }
}
