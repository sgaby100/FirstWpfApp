using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace FirstWpf
{

    public class EncodingData
    {
        static System.Security.Cryptography.Aes  Aes()
        {
            Aes aes = new AesManaged();
            return aes;
        }
        private readonly System.Security.Cryptography.Aes aeo = Aes();

        public string Decript(string pass)
        {

            string strDecrypted = (Decrypt(pass));
            return strDecrypted;
        }
        public  static class Global
        {

            // set password
            public const string strPassword = "LetMeIn99$";

            // set permutations
            public const String strPermutation = "ouiveyxaqtd";
            public const Int32 bytePermutation1 = 0x19;
            public const Int32 bytePermutation2 = 0x59;
            public const Int32 bytePermutation3 = 0x17;
            public const Int32 bytePermutation4 = 0x41;
        }

        public  string Encrypt(string strData)
        {

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));
            // reference https://msdn.microsoft.com/en-us/library/ds4kkd55(v=vs.110).aspx

        }


        public  string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData)));
            // reference https://msdn.microsoft.com/en-us/library/system.convert.frombase64string(v=vs.110).aspx

        }

        // encrypt
        private  byte[] Encrypt(byte[] strData)
        {


        PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                         Global.bytePermutation2,
                         Global.bytePermutation3,
                         Global.bytePermutation4
            });
            MemoryStream memstream = new MemoryStream();
            
            aeo.Key = passbytes.GetBytes(aeo.KeySize / 8);
            aeo.IV = passbytes.GetBytes(aeo.BlockSize / 8);


            CryptoStream cryptostream = new CryptoStream(memstream,
            aeo.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        // decrypt
        private  byte[] Decrypt(byte[] strData)
        {
            

            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                         Global.bytePermutation2,
                         Global.bytePermutation3,
                         Global.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            aeo.Key = passbytes.GetBytes(aeo.KeySize / 8);
            aeo.IV = passbytes.GetBytes(aeo.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aeo.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}

