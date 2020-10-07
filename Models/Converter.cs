using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConverterApp.Models
{
    public class Converter
    {
        /// <summary>
        /// A more mathmatical approach to ASCII to Binary conversion
        /// https://forums.asp.net/t/1713174.aspx?How+to+convert+ASCII+value+to+binary+value+using+c+net
        /// </summary>
        /// <param name="data">String to convert</param>
        /// <returns>Binary encoded string</returns>
        public static string StringToBinary2(string data)
        {
            string converted = string.Empty;
            // convert string to byte array
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            for (int i = 0; i < bytes.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    converted += (bytes[i] & 0x80) > 0 ? "1" : "0";
                    bytes[i] <<= 1;
                }
            }

            return converted;
        }
        /// <summary>
        /// A less mathmatical approach to ASCII to Binary conversion
        /// https://www.fluxbytes.com/csharp/convert-string-to-binary-and-binary-to-string-in-c/
        /// </summary>
        /// <param name="text">String to convert</param>
        /// <returns>Binary encoded string</returns>
        public static string StringToBinary(string text)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in text.ToCharArray())
            {
                //Convert the char to base 2 and pad the output with 0
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Convert a Binary text string to a Text string
        /// </summary>
        /// <param name="text">Binary encoded string</param>
        /// <returns>Text string</returns>
        public static string BinaryToString(string text)
        {
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < text.Length; i += 8)
            {
                bytes.Add(Convert.ToByte(text.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(bytes.ToArray());
        }
        /// <summary>
        /// An approach to ASCII to Hexadecimal conversion using ToString("X2")
        /// </summary>
        /// <param name="data">String to convert</param>
        /// <returns></returns>
        public static string StringToHex2(string data)
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytearray = Encoding.ASCII.GetBytes(data);

            foreach (byte bytepart in bytearray)
            {
                sb.Append(bytepart.ToString("X2"));
            }

            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// A less mathmatical approach to ASCII to Hexadecimal conversion
        /// </summary>
        /// <param name="data">String to convert</param>
        /// <returns></returns>
        public static string StringToHex(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                //Convert the char to base 16 and pad the output with 0
                sb.Append(Convert.ToString(c, 16).PadLeft(2, '0'));
            }

            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// Converts a Hexadecimal string to ASCII string
        /// </summary>
        /// <param name="hexString">Hexadecimal string</param>
        /// <returns>ASCII string</returns>
        public static string HexToString(string hexString)
        {
            if (hexString == null || (hexString.Length & 1) == 1)
            {
                throw new ArgumentException();
            }
            var sb = new StringBuilder();
            for (var i = 0; i < hexString.Length; i += 2)
            {
                var hexChar = hexString.Substring(i, 2);
                sb.Append((char)Convert.ToByte(hexChar, 16));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Encodes a String to a Base64 String
        /// </summary>
        /// <param name="data">String data</param>
        /// <returns>Base64 Encoded String</returns>
        public static string StringToBase64(string data)
        {
            byte[] bytearray = Encoding.ASCII.GetBytes(data);

            string result = Convert.ToBase64String(bytearray);

            return result;
        }

        /// <summary>
        /// Converts a Base64 string to decoded String
        /// </summary>
        /// <param name="base64String">Base64 encoded string</param>
        /// <returns>Decoded String from Base64</returns>
        public static string Base64ToString(string base64String)
        {
            byte[] bytearray = Convert.FromBase64String(base64String);

            using (var ms = new MemoryStream(bytearray))
            {
                using (StreamReader reader = new StreamReader(ms))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }
        /// <summary>
        /// Encodes a Number to a Base64 String
        /// https://ideone.com/q8wkgt
        /// </summary>
        /// <param name="data">Long number data</param>
        /// <returns>Base64 Encoded String</returns>
        public static string NumberToBase64(long data)
        {
            string result = Convert.ToBase64String(BitConverter.GetBytes(data));
            return result;
        }
        /// <summary>
        /// Encodes a Base64 String back to a Number
        /// https://ideone.com/q8wkgt
        /// </summary>
        /// <param name="data">String data</param>
        /// <returns>Long number</returns>
        public static long Base64ToNumber(string data)
        {
            long number = BitConverter.ToInt64(Convert.FromBase64String(data), 0);

            return number;
        }

        /// <summary>
        /// Provides Simple Encryption of text
        /// </summary>
        public class Encrypter
        {
            public Encrypter(string originalText, int[] encryptionCipher = null, int encryptionDepth = 1)
            {
                OriginalText = originalText;

                if (encryptionCipher != null)
                {
                    if (EncryptionCipher != encryptionCipher)
                    {
                        EncryptionCipher = encryptionCipher;
                    }
                }

                if (EncryptionDepth != encryptionDepth)
                {
                    EncryptionDepth = encryptionDepth;
                }

                ConvertText(OriginalText, EncryptionCipher, EncryptionDepth);
            }

            public void ConvertText(string originalText, int[] encryptionCipher = null, int encryptionDepth = 1)
            {
                //Deep Cipher encrypt
                CipherEncrypted = DeepEncryptWithCipher(OriginalText, EncryptionCipher, EncryptionDepth);

                //Convert Original Text to other base formats
                Base64 = StringToBase64(OriginalText);
                Binary = StringToBinary(OriginalText);
                Hexadecimal = StringToHex(OriginalText);
            }

            public string OriginalText { get; internal set; }
            public int[] EncryptionCipher { get; } = new[] { 1, 1, 2, 3, 5, 8 }; //Fibonacci Sequence
            public int EncryptionDepth { get; } = 1;
            public string CipherEncrypted { get; internal set; }
            public string Base64 { get; internal set; }
            public string Binary { get; internal set; }
            public string Hexadecimal { get; internal set; }

            public static string DeepEncryptWithCipher(string originalText, int[] encryptionCipher, int encryptionDepth)
            {
                string result = originalText;

                //For demonstration
                //string[] encryptedValues = new string[encryptionDepth + 1];
                //encryptedValues[0] = result;

                //Encrypt result encryptionDepth times
                for (int depth = 0; depth < encryptionDepth; depth++)
                {
                    //Apply Encryption Cipher on current value of result
                    result = EncryptWithCipher(result, encryptionCipher);

                    //Add new encrypted result to the encrypted array fro demonstration
                    //encryptedValues[depth + 1] = result;
                }

                return result;
            }

            /// <summary>
            /// Applies a Cipher to a string
            /// </summary>
            /// <param name="text">Text to encrypt</param>
            /// <param name="encryptionCipher">new[] { 1,2,3,4,5,6 }</param>
            /// <returns></returns>
            public static string EncryptWithCipher(string text, int[] encryptionCipher)
            {
                if (encryptionCipher == null || encryptionCipher.Length == 0)
                {
                    return text;
                }

                //Store the original string converted to bytes
                //Convert the text data to Unicode byte in order to handle non ASCII value character
                byte[] bytearray = Encoding.Unicode.GetBytes(text);

                //Build byte array from the original byte array that will receive the encrypted values
                byte[] bytearrayresult = bytearray;

                int encryptionCipherIndex = 0;

                //Apply Encryption Cipher
                for (int i = 0; i < bytearray.Length; i++)
                {
                    //Set the Cipher index
                    encryptionCipherIndex = i;

                    //We reset the current encryption position to 0 to restart at the beginning of the encryptionCipher
                    if (encryptionCipherIndex >= encryptionCipher.Length)
                    {
                        //Reset the cryper postion to zero and restart sequence
                        encryptionCipherIndex = 0;
                    }

                    //These lines are for demonstration to show values
                    //byte bytecharacter = bytearray[i];
                    //int CipherChar = encryptionCipher[encryptionCipherIndex];

                    //Change the value of the current character by the values received from the encryptionCipher array
                    //int newchar = bytearray[i] + encryptionCipher[encryptionCipherIndex];

                    //Change the value of the current character by the values received from the encryptionCipher array
                    if (bytearray[i] != 0)
                    {
                        bytearrayresult[i] = (byte)(bytearray[i] + encryptionCipher[encryptionCipherIndex]);
                    }
                }

                string newresult = Encoding.Unicode.GetString(bytearrayresult);

                return newresult;
            }

            /// <summary>
            /// Decrypts a deep cipher encrypted string
            /// </summary>
            /// <param name="originalText">Cipher encrypted string</param>
            /// <param name="encryptionCipher">Sequence of whole numbers in an array</param>
            /// <param name="encryptionDepth">Depth of the encryption</param>
            /// <returns>Decrypted string</returns>
            public static string DeepDecryptWithCipher(string originalText, int[] encryptionCipher, int encryptionDepth)
            {
                string result = originalText;

                //For demonstration
                string[] encryptedValues = new string[encryptionDepth + 1];
                encryptedValues[0] = result;

                //Encrypt result encryptionDepth times
                for (int depth = 0; depth < encryptionDepth; depth++)
                {
                    //Apply Encryption Cipher on current value of result
                    result = DecryptWithCipher(result, encryptionCipher);

                    //Add new encrypted result to the encrypted array fro demonstration
                    encryptedValues[depth + 1] = result;
                }

                return result;
            }

            /// <summary>
            /// Decrypts a cipher encrypted string
            /// </summary>
            /// <param name="text"></param>
            /// <param name="encryptionCipher"></param>
            /// <returns></returns>
            public static string DecryptWithCipher(string text, int[] encryptionCipher)
            {
                //Convert the text data to Unicode byte in order to handle non ASCII value character
                byte[] bytearray = Encoding.Unicode.GetBytes(text);
                //Build byte array from the original byte array that will receive the encrypted values
                byte[] bytearrayresult = bytearray;

                int encryptionCipherIndex = 0;

                for (int i = 0; i < bytearray.Length; i++)
                {
                    //Set the Cipher index
                    encryptionCipherIndex = i;

                    //We reset the current encryption position to 0 to restart at the beginning of the encryptionCipher
                    if (encryptionCipherIndex >= encryptionCipher.Length)
                    {
                        //Reset the cryper postion to zero and restart sequence
                        encryptionCipherIndex = 0;
                    }

                    if (bytearray[i] != 0)
                    {
                        bytearrayresult[i] = (byte)(bytearray[i] - encryptionCipher[encryptionCipherIndex]);
                    }
                }

                string newresult = Encoding.Unicode.GetString(bytearrayresult);

                return newresult;
            }

            /// <summary>
            /// Converts a Unicode Base64 string to decoded String
            /// </summary>
            /// <param name="base64String">Unicode Base64 encoded string</param>
            /// <returns>Decoded String from Base64</returns>
            public static string Base64ToString(string data)
            {
                if (String.IsNullOrEmpty(data))
                {
                    return String.Empty;
                }

                byte[] bytearray = Convert.FromBase64String(data);

                return Encoding.Unicode.GetString(bytearray);
            }

            /// <summary>
            /// Encodes a String to a Base64 String
            /// </summary>
            /// <param name="data">String data</param>
            /// <returns>Base64 Encoded String</returns>
            public static string StringToBase64(string data)
            {
                byte[] bytearray = Encoding.Unicode.GetBytes(data);

                return Convert.ToBase64String(bytearray);
            }

            /// <summary>
            /// A less mathmatical approach to ASCII to Hexadecimal conversion
            /// </summary>
            /// <param name="data">String to convert</param>
            /// <returns></returns>
            public static string StringToHex(string data)
            {
                StringBuilder sb = new StringBuilder();

                foreach (char c in data.ToCharArray())
                {
                    //Convert the char to base 16 and pad the output with 0
                    sb.Append(Convert.ToString(c, 16).PadLeft(2, '0') + " ");
                }

                return sb.ToString().ToUpper();
            }

            /// <summary>
            /// A less mathmatical approach to ASCII to Binary conversion
            /// https://www.fluxbytes.com/csharp/convert-string-to-binary-and-binary-to-string-in-c/
            /// </summary>
            /// <param name="data">String to convert</param>
            /// <returns></returns>
            public static string StringToBinary(string data)
            {
                StringBuilder sb = new StringBuilder();

                foreach (char c in data.ToCharArray())
                {
                    //Convert the char to base 2 and pad the output with 0
                    sb.Append(Convert.ToString(c, 2).PadLeft(8, '0') + " ");
                }
                return sb.ToString();
            }

        }
    }
}

              

