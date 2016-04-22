// (c) Rishikesh Parkhe 2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.RishikeshParkhe.PersonalStorage.Services
{
    public class SecureStorageService
    {
        public SecureStorageService()
        {
            var rsa = Windows.Security.Cryptography.Core.AsymmetricKeyAlgorithmProvider.OpenAlgorithm("RSA");
        }
    }
}