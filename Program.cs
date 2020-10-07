using System;
using System.Text;
using ConverterApp.Models;
using System.Linq;
using static ConverterApp.Models.Converter;

namespace ConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Shivansh Desai";
            
             string binaryValue2 = Converter.StringToBinary2(text);

             Console.WriteLine($"Text: {text}\nBinary: {binaryValue2}");

             string binaryValue = Converter.StringToBinary2(text);

             Console.WriteLine($"Text: {text}\nBinary: {binaryValue}");
            
             string textFromBinary = Converter.BinaryToString(binaryValue);

             Console.WriteLine($"Binary: {binaryValue}\nText: {textFromBinary}");

            //Store some ASCII text in a string

            //Convert the text to a byte array

            // byte[] textbytes = Encoding.ASCII.GetBytes(text);

            //Convert the first item in the byte array
            // var hex = textbytes[0].ToString("X2");

            //Output the Hexadecimal conversion
            // Console.WriteLine(hex);
            
            string hexadecimalValue2 = Converter.StringToHex2(text);

            Console.WriteLine($"Text: {text}\nHEX: {hexadecimalValue2}");

            string hexadecimalValue = Converter.StringToHex(text);

            Console.WriteLine($"Text: {text}\nHEX: {hexadecimalValue}");

            string textFromHex = Converter.HexToString(hexadecimalValue);

            Console.WriteLine($"HEX: {hexadecimalValue}\nText: {textFromHex}");

            // string nameBase64Encoded = Converter.StringToBase64(text);
            
            // Console.WriteLine(nameBase64Encoded);
           
            //Output the decoded Base64 string
            // string nameBase64Decoded = Converter.Base64ToString(nameBase64Encoded);
            
            // Console.WriteLine(nameBase64Decoded);

            // long bignumber = 1234567890987654321L;

            // string base64FromNumber = Converter.NumberToBase64(bignumber);

            // Console.WriteLine($"Long: {bignumber}\nBase64: {base64FromNumber}");

            // long bignumber2 = Converter.Base64ToNumber(base64FromNumber);

            // Console.WriteLine($"Base64: {base64FromNumber}\nLong: {bignumber2}");

            // Random random = new System.Random();
            // int value = random.Next(0, 100); //returns integer of 0-100
         
            // for (int i = 0; i < 10; i++)
            {
                // double value2 = random.NextDouble(); //returns floating point 0.0-1.0
                // Console.WriteLine(value2);
            }
            
            // var byteArray = new byte[256];
            // random.NextBytes(byteArray); //fill with random bytes

            string unicodeString = "This string contains the unicode character Pi (\u03a0)";
            int[] cipher = new[] { 1, 1, 2, 3, 5, 8, 13 }; //Fibonacci Sequence
            string cipherasString = String.Join(",", cipher.Select(x => x.ToString())); //FOr display

            int encryptionDepth = 20;

            Encrypter encrypter = new Encrypter(unicodeString, cipher, encryptionDepth);

            //Single Level Encrytion
            string nameEncryptWithCipher = Encrypter.EncryptWithCipher(unicodeString, cipher);
            Console.WriteLine($"Encrypted once using the cipher {{{cipherasString}}} {nameEncryptWithCipher}");

            string nameDecryptWithCipher = Encrypter.DecryptWithCipher(nameEncryptWithCipher, cipher);
            Console.WriteLine($"Decrypted once using the cipher {{{cipherasString}}} {nameDecryptWithCipher}");

            //Deep Encrytion
            string nameDeepEncryptWithCipher = Encrypter.DeepEncryptWithCipher(unicodeString, cipher, encryptionDepth);
            Console.WriteLine($"Deep Encrypted {encryptionDepth} times using the cipher {{{cipherasString}}} {nameDeepEncryptWithCipher}");

            string nameDeepDecryptWithCipher = Encrypter.DeepDecryptWithCipher(nameDeepEncryptWithCipher, cipher, encryptionDepth);
            Console.WriteLine($"Deep Decrypted {encryptionDepth} times using the cipher {{{cipherasString}}} {nameDeepDecryptWithCipher}");

            //Base64 Encoded
            Console.WriteLine($"Base64 encoded {unicodeString} {encrypter.Base64}");

            string base64toPlainText = Encrypter.Base64ToString(encrypter.Base64);
            Console.WriteLine($"Base64 decoded {encrypter.Base64} {base64toPlainText}");
        }
    }
}
