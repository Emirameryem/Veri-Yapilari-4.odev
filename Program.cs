using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYaplialriOdev_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList baglantılıListe = new LinkedList();

            string infixIfadesi = "A-(K+T)-M"; // Hesaplanacak infix ifadesi

            string postfix = baglantılıListe.PostfixeDonustur(infixIfadesi);
            Console.WriteLine("Postfix: " + postfix); // Postfix ifadesini yazdırır

            string prefix = baglantılıListe.PrefixDonustur(infixIfadesi);
            Console.WriteLine("Prefix: " + prefix); // Prefix ifadesini yazdırır

            // Yazılan ifadeyi sayılarla ifade et
            string hesaplananPostfix = postfix.Replace("A", "1").Replace("K", "2").Replace("T", "3").Replace("M","4");
            int sonuc = baglantılıListe.PostfixOlarakHesapla(hesaplananPostfix);
            Console.WriteLine("Sonuç: " + sonuc); // Sonucu yazdırma işlemi için

            Console.ReadKey();

        }



        public class Dugum 
        {
            public char data;
            public Dugum sonraki;

            public Dugum(char data)
            {
                this.data = data;
                this.sonraki = null; // İlk düğümde bir sonraki düğümü yokkk
            }
        }

        public class LinkedList
        {
            private Dugum bas;

            public void Ekle(char data)
            {
                Dugum eleman = new Dugum(data); // Yeni düğüm oluşturdum
                if (bas == null)
                {
                    bas = eleman; // Listeye ilk elemanı ekledim
                }
                else
                {
                    Dugum temp = bas; // Geçici düğüm 
                    while (temp.sonraki != null)
                    {
                        temp = temp.sonraki; // Son düğüme git
                    }
                    temp.sonraki = eleman; // Yeni düğümü ekleme
                }
            }

            public string PostfixeDonustur(string infix) // Infix ifadeyi postfix'e dönüştürme metodu
            {
                Stack<char> Yıgın = new Stack<char>();
                string cıktı = "";

                foreach (char n in infix)
                {
                    if (char.IsLetterOrDigit(n)) // Harf veya rakam kontrolü

                    {
                        cıktı += n;
                    }
                    else if (n == '(') // Parantez açma
                    {
                        Yıgın.Push(n); // Yığına ekleme yaparız
                    }
                    else if (n == ')') // Parantez kapama
                    {
                        while (Yıgın.Count > 0 && Yıgın.Peek() != '(')
                        {
                            cıktı += Yıgın.Pop(); // Yığından eleman al
                        }
                        if (Yıgın.Count > 0) // Açık parantez varsa yapıdan çıkarır.
                        {
                            Yıgın.Pop(); // Parantezi çıkart
                        }
                    }
                    else // Operatör
                    {
                        while (Yıgın.Count > 0 && OncelikSırası(n) <= OncelikSırası(Yıgın.Peek()))
                        {
                            cıktı += Yıgın.Pop(); // Yığından eleman al
                        }
                        Yıgın.Push(n); 
                    }
                }
                while (Yıgın.Count > 0)
                {
                    cıktı += Yıgın.Pop(); // Yığındaki tüm operatörleri almasını sağladım.
                }
                return cıktı; // Postfix ifadeyi döndür dedim
            }

            private int OncelikSırası(char x)
            {
                switch (x)
                {
                    case '+':
                    case '-':
                        return 1;
                    case '*':
                    case '/':
                        return 2;
                    case '^':
                        return 3;
                    default:
                        return 0;
                }
            }

            public int PostfixOlarakHesapla(string postfix)
            {
                Stack<int> Yıgın = new Stack<int>(); // Sayıları tutmak için oluşturulan yığın kod satırı

                foreach (char a in postfix)
                {
                    if (char.IsDigit(a)) // Rakam kontrolü yaptım
                    {
                        Yıgın.Push(a - '0'); // Rakamı yığına eklemek içiçn
                    }
                    else  
                    {
                        int y = Yıgın.Pop(); 
                        int x = Yıgın.Pop(); 

                        switch (a)
                        {
                            case '+':
                                Yıgın.Push(x + y); break; // Toplama işlemi
                            case '-':
                                Yıgın.Push(x - y); break; // Çıkarma işlemi
                            case '*':
                                Yıgın.Push(x * y); break; // Çarpma işlemi
                            case '/':
                                Yıgın.Push(x / y); break; // Bölme işlemi
                        }
                    }
                }
                return Yıgın.Pop(); // Sonucu döndür
            }

            public string PrefixDonustur(string infix) // Prefix ifadeye dönüştür
            {
                // Infix ifadeyi ters çevirip işlem yapıyoruz
                char[] karakterDizisi = infix.ToCharArray();
                Array.Reverse(karakterDizisi);
                string tersInfix = new string(karakterDizisi);

                tersInfix = tersInfix.Replace('(', 'X').Replace(')', '(').Replace('X', ')');

                string postfix = PostfixeDonustur(tersInfix); // Ters infix'i postfix'e çevir

                char[] postfixDizisi = postfix.ToCharArray();
                Array.Reverse(postfixDizisi);
                return new string(postfixDizisi); // Prefix ifadeyi döndür

            }
        }


    }
}