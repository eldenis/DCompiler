using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CDb.Transversal.Utilitarios
{
    public static class Utilitarios
    {
        public static bool EsNullable<T>(T valor)
        {
            return Nullable.GetUnderlyingType(typeof(T)) != null;
        }

        public static Dupla<MemberInfo, MemberInfo> ParseExpresionBinaria<T1, T2>(Expression<Func<T1, T2, bool>> funcion)
        {
            Dupla<MemberInfo, MemberInfo> retval = null;

            if (funcion.Body is BinaryExpression)
            {
                var binaria = funcion.Body as BinaryExpression;
                var izquierda = ObtenerNombreUltimoMiembro(binaria.Left as MemberExpression); //(binaria.Left as MemberExpression).Member;
                var derecha = ObtenerNombreUltimoMiembro(binaria.Right as MemberExpression);//(binaria.Right as MemberExpression).Member;

                retval = Dupla.Crear(izquierda, derecha);
            }

            return retval;
        }

        public static MemberInfo ObtenerNombreUltimoMiembro(MemberExpression me)
        {
            var me2 = me.Expression as MemberExpression;
            return me2 != null ? ObtenerNombreUltimoMiembro(me2) : me.Member;
        }

        public static MethodInfo ObtenerMethodInfo(Expression<Action> a)
        {
            if (a.Body is MethodCallExpression)
            {
                var res = (a.Body as MethodCallExpression).Method;
                string denis = res.Name;
                return res;
            }


            return null;
        }

        public static string ObtenerNombreMiembro<T>(Expression<Func<T, object>> propiedad)
        {
            return ObtenerNombreMiembro(propiedad.Body);
        }

        public static string ObtenerNombreMiembro(Expression<Func<object>> propiedad)
        {
            return ObtenerNombreMiembro(propiedad.Body);
        }

        public static string ObtenerNombreMiembro(Expression propiedad)
        {
            string nombrePropiedad = null;
            if (propiedad is UnaryExpression)
            {
                var oper = (propiedad as UnaryExpression).Operand;
                if (oper is MethodCallExpression) nombrePropiedad = (oper as MethodCallExpression).Method.Name;
                if (oper is MemberExpression) nombrePropiedad = (oper as MemberExpression).Member.Name;
            }
            else
            {
                var body = propiedad;
                if (body is MethodCallExpression) nombrePropiedad = (body as MethodCallExpression).Method.Name;
                if (body is MemberExpression) nombrePropiedad = (body as MemberExpression).Member.Name;
            }

            if (string.IsNullOrEmpty(nombrePropiedad)) //Este error nunca debería pasar
                throw new Exception("ERROR AL CARGAR NOMBRE DE PROPIEDAD.");

            return nombrePropiedad;
        }

        public static string GetString(this byte[] bytes)
        {
            return bytes != null ? Encoding.ASCII.GetString(bytes) : string.Empty;
        }

        public static Stream GetStream(this byte[] bytes)
        {
            return bytes != null ? new MemoryStream(bytes) : new MemoryStream();
        }

        public static Stream GetStream(this string cadena)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(cadena);
            MemoryStream stream = new MemoryStream(bytes);

            return stream;
        }

        public static XmlDocument GetXmlDocument (this Stream stream)
        {
            XmlDocument documento = new XmlDocument();
            documento.Load(stream);
            return documento;
        }

        public static XmlDocument GetXmlDocument(this byte[] bytes)
        {
            XmlDocument documento = new XmlDocument();
            documento.Load(bytes.GetStream());
            return documento;
        }


        public static XDocument GetXDocument(this string cadena)
        {
            XDocument documento = XDocument.Parse(cadena);
            return documento;
        }

        public static XDocument GetXDocument(this byte[] bytes)
        {
            XDocument documento = XDocument.Parse(bytes.GetString());
            return documento;
        }

        public static byte[] GetBytes(this string cadena)
        {
            return Encoding.ASCII.GetBytes(cadena);
        }

        public static byte[] GetBytes (this XmlDocument documento)
        {
            return Encoding.ASCII.GetBytes(documento.ToString());
        }

        public static byte[] GetBytes(this XDocument documento)
        {
            return Encoding.ASCII.GetBytes(documento.ToString());
        }

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be 
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string Encrypt(string plainText,
                                        string passPhrase,
                                        string saltValue,
                                        string hashAlgorithm,
                                        int passwordIterations,
                                        string initVector,
                                        int keySize)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-formatted ciphertext value.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        /// <remarks>
        /// Most of the logic in this function is similar to the Encrypt
        /// logic. In order for decryption to work, all parameters of this function
        /// - except cipherText value - must match the corresponding parameters of
        /// the Encrypt function which was called to generate the
        /// ciphertext.
        /// </remarks>
        public static string Decrypt(string   cipherText,
                                     string   passPhrase,
                                     string   saltValue,
                                     string   hashAlgorithm,
                                     int      passwordIterations,
                                     string   initVector,
                                     int      keySize)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes  = Encoding.ASCII.GetBytes(saltValue);
        
            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        
            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase, 
                                                            saltValueBytes, 
                                                            hashAlgorithm, 
                                                            passwordIterations);
        
            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);
        
            // Create uninitialized Rijndael encryption object.
            RijndaelManaged    symmetricKey = new RijndaelManaged();
        
            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
        
            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes, 
                                                             initVectorBytes);
        
            // Define memory stream which will be used to hold encrypted data.
            MemoryStream  memoryStream = new MemoryStream(cipherTextBytes);
                
            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream  cryptoStream = new CryptoStream(memoryStream, 
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        
            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 
                                                       0, 
                                                       plainTextBytes.Length);
                
            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();
        
            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 
                                                       0, 
                                                       decryptedByteCount);
        
            // Return decrypted string.   
            return plainText;
        }
    }
}
