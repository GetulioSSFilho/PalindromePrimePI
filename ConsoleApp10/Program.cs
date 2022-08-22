using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static System.Math;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace PalindromePrimePI
{
    internal class Program
    {
        static bool stop = false;
        static int end = 1000;
        static Int64 initial = 0;
        static int digitsFound = 9;
        static void Main(string[] args)
        {
            NovaThread();
        }
        static void NovaThread()
        {
            while (!stop)
            {
                try
                {
                    Console.WriteLine(initial);
                    var jsonData = JObject.Parse(GetPI(initial, end));
                    Find_Palindromic_Prime_NDigits((string)jsonData["content"], digitsFound);
                }
                catch { }
                initial += 979;
            }
        }
        public static void Find_Palindromic_Prime_NDigits(string n, int Digits)
        {
            List<string> vetor1, vetor2;
            string nCheck;
            bool palindromic;
            vetor1 = new List<string>();
            vetor2 = new List<string>();
            int numLength = n.Length;
            try
            {
                for (int j = 0; j < numLength; j++)
                {
                    nCheck = n.Substring(j, Digits);
                    vetor1.Clear();
                    vetor2.Clear();
                    vetor1.Add(nCheck);
                    vetor2.Add(new string(nCheck.Reverse().ToArray()));
                    palindromic = vetor1[0] == vetor2[0];
                    if (palindromic && IsPrime(int.Parse(nCheck)))
                    {
                        MessageBox.Show("The number {0} is palíndrome and Prime.", nCheck);
                        Console.ReadKey();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        static string GetPI(Int64 initial, int end)
        {
                var web = new WebClient();
                var url = $"https://api.pi.delivery/v1/pi?start={initial}&numberOfDigits={end}&radix=10";
                return web.DownloadString(url);
        }
        public static bool IsPrime(int numero)
        {
            if (numero <= 1) return false;
            if (numero == 2) return true;
            if (numero % 2 == 0) return false;
            var limite = (int)Floor(Sqrt(numero));
            for (int i = 3; i <= limite; i += 2) if (numero % i == 0) return false;
            return true;
        }
    }
}

