using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace cmd.libs
{
    public class encryptDes
    {
        
        //默认密钥向量
        //private static byte[] _key1 = { 0x22, 0x33, 0x35, 0x81, 0x16, 0x38, 0x12, 0x22, 0x22, 0x33, 0x35, 0x81, 0x16, 0x38, 0x12, 0x22 };
        //private static string _key = "seanmoo@";

        private static string _key8 = "dolphin@";
        private static string _key8_IV = "seanmoo@";






        /// <summary>
        /// C# DES解密方法
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string encryptedValue)
        {
            using (DESCryptoServiceProvider sa =
                new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(_key8), IV = Encoding.UTF8.GetBytes(_key8_IV) })
            {
                using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(encryptedValue);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }


        /// <summary>
        /// C# DES加密方法
        /// </summary>
        /// <param name="encryptedValue">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string originalValue)
        {
            using (DESCryptoServiceProvider sa
                = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(_key8), IV = Encoding.UTF8.GetBytes(_key8_IV) })
            {
                using (ICryptoTransform ct = sa.CreateEncryptor())
                {
                    byte[] by = Encoding.UTF8.GetBytes(originalValue);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct,
                                                         CryptoStreamMode.Write))
                        {
                            cs.Write(by, 0, by.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }












        /******************************
/// <summary>
/// AES加密算法
/// </summary>
/// <param name="plainText">明文字符串</param>
/// <param name="strKey">密钥</param>
/// <returns>返回加密后的密文字节数组</returns>
public static byte[] AESEncrypt(string plainText)
{

    //分组加密算法
    SymmetricAlgorithm des = Rijndael.Create();
    //des.BlockSize = 64;
    //des.KeySize = 64;

    byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组
    //设置密钥及密钥向量
    des.Key = Encoding.UTF8.GetBytes(_key);
    des.IV = _key1;

    MemoryStream ms = new MemoryStream();
    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
    cs.Write(inputByteArray, 0, inputByteArray.Length);
    cs.FlushFinalBlock();
    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组
    cs.Close();
    ms.Close();
    return cipherBytes;
}

/// <summary>
/// AES解密
/// </summary>
/// <param name="cipherText">密文字节数组</param>
/// <param name="strKey">密钥</param>
/// <returns>返回解密后的字符串</returns>
public static byte[] AESDecrypt(byte[] cipherText)
{
    SymmetricAlgorithm des = Rijndael.Create();
    des.Key = Encoding.UTF8.GetBytes(_key);
    des.IV = _key1;
    byte[] decryptBytes = new byte[cipherText.Length];
    MemoryStream ms = new MemoryStream(cipherText);
    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
    cs.Read(decryptBytes, 0, decryptBytes.Length);
    cs.Close();
    ms.Close();
    return decryptBytes;
}

*********************/




        /******************************
        //方法  
        //加密方法  
        public static string Encrypt(string pToEncrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中  
            //原来使用的UTF8编码，我改成Unicode编码了，不行  
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);  

            //建立加密对象的密钥和偏移量  
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
            //使得输入密码必须输入英文文本  
            des.Key = ASCIIEncoding.ASCII.GetBytes(_key8);
            des.IV = ASCIIEncoding.ASCII.GetBytes(_key8_vector);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write  the  byte  array  into  the  crypto  stream  
            //(It  will  end  up  in  the  memory  stream)  
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Get  the  data  back  from  the  memory  stream,  and  into  a  string  
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                //Format  as  hex  
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }


        //解密方法  
        public static string Decrypt(string pToDecrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Put  the  input  string  into  the  byte  array  
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改  
            des.Key = ASCIIEncoding.ASCII.GetBytes(_key8);
            des.IV = ASCIIEncoding.ASCII.GetBytes(_key8_vector);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream  
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

    *****************************/



    }
}