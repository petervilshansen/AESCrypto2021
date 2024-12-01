﻿using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Linq;

namespace AESCrypto.Tests
{
    [TestClass()]
    public class AESUtilTests
    {
        [TestMethod()]
        public void EncryptDecryptTestTextInput()
        {
            (string cipherText, string password) = AESUtil.Encrypt(Encoding.UTF8.GetBytes("Hello World"));
            string decrypted = Encoding.UTF8.GetString(AESUtil.Decrypt(cipherText, password));
            Assert.AreEqual("Hello World", decrypted);
        }

        [TestMethod()]
        public void EncryptDecryptTestUTF8Input()
        {
            // https://stackoverflow.com/questions/1319022/really-good-bad-utf-8-example-test-data
            const string input = "ăѣ𝔠ծềſģȟᎥ𝒋ǩľḿꞑȯ𝘱𝑞𝗋𝘴ȶ𝞄𝜈ψ𝒙𝘆𝚣1234567890!@#$%^&*()-_=+[{]};:'\",<.>/?~𝘈Ḇ𝖢𝕯٤ḞԍНǏ𝙅ƘԸⲘ𝙉০Ρ𝗤Ɍ𝓢ȚЦ𝒱Ѡ𝓧ƳȤѧᖯć𝗱ễ𝑓𝙜Ⴙ𝞲𝑗𝒌ļṃŉо𝞎𝒒ᵲꜱ𝙩ừ𝗏ŵ𝒙𝒚ź1234567890!@#$%^&*()-_=+[{]};:'\",<.>/?~АḂⲤ𝗗𝖤𝗙ꞠꓧȊ𝐉𝜥ꓡ𝑀𝑵Ǭ𝙿𝑄Ŗ𝑆𝒯𝖴𝘝𝘞ꓫŸ𝜡ả𝘢ƀ𝖼ḋếᵮℊ𝙝Ꭵ𝕛кιṃդⱺ𝓅𝘲𝕣𝖘ŧ𝑢ṽẉ𝘅ყž1234567890!@#$%^&*()-_=+[{]};:'\",<.>/?~Ѧ𝙱ƇᗞΣℱԍҤ١𝔍К𝓛𝓜ƝȎ𝚸𝑄Ṛ𝓢ṮṺƲᏔꓫ𝚈𝚭𝜶Ꮟçძ𝑒𝖿𝗀ḧ𝗂𝐣ҝɭḿ𝕟𝐨𝝔𝕢ṛ𝓼тú𝔳ẃ⤬𝝲𝗓1234567890!@#$%^&*()-_=+[{]};:'\",<.>/?~𝖠Β𝒞𝘋𝙴𝓕ĢȞỈ𝕵ꓗʟ𝙼ℕ০𝚸𝗤ՀꓢṰǓⅤ𝔚Ⲭ𝑌𝙕𝘢𝕤";
            (string cipherText, string password) = AESUtil.Encrypt(Encoding.UTF8.GetBytes(input));
            string decrypted = Encoding.UTF8.GetString(AESUtil.Decrypt(cipherText, password));
            Assert.AreEqual(input, decrypted);
        }

        [TestMethod()]
        public void EncryptDecryptTestUTF8InputLoremIpsumHomoglyphs()
        {
            // https://jeff.cis.cabrillo.edu/tools/homoglyphs
            const string input = "𐐛𝛔гĕᵯ Ꭵ𝟈ṥ𝗎ṃ 𝜎δȱ𝐫 ậḿӭṯ, 𐐽ợ𝒏ʂễ𝙘ƭȇ𝘁ṻḕŗ ẫᏧȋ𝟈ị𝙨ɕįṇǥ ȩł𝐢ẗ. 𝔑𝒊𝙨ӏ 𝓯ṟìńցĭľḽӓ м𝐨гᖯȉ 𝐛ᶅ𝞪𝐧𝙙ıτ ḷ𝜶őŕеḝ𝘵 ẚᴦсս 𝐬ȁ𝐠Ꭵҭṱǐṣ. Ựƚŧṛ𝐢ḉꙇȩș ẝ𝚞ş𝔠𝔢 ᵵ𝞲𝗇ᴄḭ𝕕𝝊ṋ𝓽 ļ𝛔ŕẹḿ ḟḗա𝒈⍳ᶏ𝘁 ᴄǚɼṥṻᶊ 𝖋ȇ𝓾ģᶖⱥŧ ếṻ 𝒽𝖾ṋԁɾ℮𝕣𝘪𝞃. 𝑇ủ𝖗𝒑𝚒𝘀 ꞧĭ𝒹ḭ𐐽𝑢ɭҵ𝕤 ɗ𝘪𝓬𝚝ủм 𝑠ö𝗰ⅰ𝝈ŝ𝐪𝞾 𝑓𝕦š𝓬ė 𝒸ṓɱɱ𝞼𝑑о ṁâ×𝜄ṃաᶊ 𝗽èņ𝖆𝙩Ꭵḇ𝛖𝘀. 𝛮𝛖ḽȴαṃ ѵứᶅ𝓅𝞾𝘁ẚ𝔱ế е𝐟𝒇ɪҫḭ𝞽ứ𝘳 𝖋äč𝒊ꝉɪᶊỉ ƫ𝓪ꞓ𝙞ţ𝔦 ৮ⱡ𝛼𝝅ⅆĭ𝘵 ćŭЬ⍳ⱡȉạ. 𝞠𝐡åřê𝖙ɍȧ 𝐪𝓊įṩ𝗾ů𝚎 ծці ѧŧ 𝑓ěų𝘨ℹầᵵ 𝝉ō𝒓ʠμéṇ𝞃 ⱥḷ𝔦𝒒ʉėҭ 𝗽һ𝖆𝑟ḝᴛṙɑ. Ꞑůɲƈ 𝔱ȭ𝐫𝖖𝞄ḙ𝝿𝔱 ɍӏ𝙨𝒖𝙨 ɬḯɡùꞎ𝐚 𝔥𝕖ṉძŕ𝓮𝐫ᶖ𝓽 ℊ𝖗á𝗏𝚤ḍ𝗮 ⲣɽ𝜶ℯ𝚜ℯᵰť Գǘḯ𝐬.";
            (string cipherText, string password) = AESUtil.Encrypt(Encoding.UTF8.GetBytes(input));
            string decrypted = Encoding.UTF8.GetString(AESUtil.Decrypt(cipherText, password));
            Assert.AreEqual(input, decrypted);
        }

        [TestMethod()]
        public void EncryptDecryptTestBinaryInput()
        {
            byte[] random = new byte[1024];
            RandomNumberGenerator.Fill(random);
            (string cipherText, string password) = AESUtil.Encrypt(random);
            byte[] decrypted = AESUtil.Decrypt(cipherText, password);
            Assert.AreEqual(System.Convert.ToBase64String(random), System.Convert.ToBase64String(decrypted));
        }

        [TestMethod()]
        public void EncryptTestFail()
        {
            (string cipherText, string password) = AESUtil.Encrypt(Encoding.UTF8.GetBytes("Hello World"));
            try
            {
                string decrypted = Encoding.UTF8.GetString(AESUtil.Decrypt(cipherText, "wrong"));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Error during decryption:"));
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void EncryptTestUsingBouncyCastleAesGcm()
        {
            // This input (plaintext, key, and nonce) must be generated by AESUtil.cs. If this test method is able
            // to encrypt the plaintext successfully using these values, and the output matches the output from 
            // AESUtil.cs, the test is a success. It proves that AESUtil.cs and BouncyCastle produce identical
            // output from AES-256 GCM encryption. It is currently a manual process to verify that the values match.

            string plaintext = "Hello, World!";
            byte[] key = Convert.FromBase64String("aOZEKPOjHOI6UOR09ogIsaHEMX6F+DOQ6koa0Rkd1yU="); // 256-bit key
            byte[] nonce = Convert.FromBase64String("LuzLRsn8mdxglJiN");  // 96-bit IV

            // Encrypt

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, nonce);
            cipher.Init(true, parameters);

            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] ciphertextBytes = new byte[cipher.GetOutputSize(plaintextBytes.Length)];
            int len = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, ciphertextBytes, 0);
            cipher.DoFinal(ciphertextBytes, len);

            byte[] ciphertext = ciphertextBytes.Take(ciphertextBytes.Length - 16).ToArray();
            byte[] tag = ciphertextBytes.Skip(ciphertextBytes.Length - 16).Take(16).ToArray();
            Console.WriteLine($"Ciphertext: {Convert.ToBase64String(ciphertext)}");
            Console.WriteLine($"Tag: {Convert.ToBase64String(tag)}");

            // Decrypt
            
            cipher = new GcmBlockCipher(new AesEngine());
            parameters = new AeadParameters(new KeyParameter(key), 128, nonce);
            cipher.Init(false, parameters);

            plaintextBytes = new byte[cipher.GetOutputSize(ciphertextBytes.Length)];
            len = cipher.ProcessBytes(ciphertextBytes, 0, ciphertextBytes.Length, plaintextBytes, 0);
            cipher.DoFinal(plaintextBytes, len);

            string decryptedText = Encoding.UTF8.GetString(plaintextBytes).TrimEnd('\0');

            Console.WriteLine($"Decrypted Text: {decryptedText}");
        }
    }
}
