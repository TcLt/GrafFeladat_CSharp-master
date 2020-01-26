using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        public void Szelesseg(int a)
        {
            HashSet<int> eddig = new HashSet<int>();
            Queue<int> kovetkezo = new Queue<int>();
            kovetkezo.Enqueue(a);
            eddig.Add(a);

            while (kovetkezo.Count > 0)
            {
                a = kovetkezo.Dequeue();
            }

            foreach (var item in this.elek)
            {
                if (item.Csucs1 == a && !eddig.Contains(item.Csucs2))
                {
                    kovetkezo.Enqueue(item.Csucs2);
                    eddig.Add(item.Csucs2);
                }
            }

            Console.WriteLine(this.csucsok[a]);
        }

            
        

        public void Melyseg(int a)
        {
            HashSet<int> eddig = new HashSet<int>();
            Stack<int> kovetkezo = new Stack<int>();
            kovetkezo.Push(a);
            eddig.Add(a);

            while (kovetkezo.Count > 0)
            {
                a = kovetkezo.Pop();
            }

            foreach (var item in elek)
            {
                if (item.Csucs1 == a && !eddig.Contains(item.Csucs2))
                {
                    kovetkezo.Push(item.Csucs2);
                    eddig.Add(item.Csucs2);
                }
            }

            Console.WriteLine(csucsok[a]);
        }

        public Graf Feszitofa(int a)
        {
            Graf g = new Graf(csucsokSzama);
            HashSet<int> eddig = new HashSet<int>();
            Queue<int> kovetkezo = new Queue<int>();
            kovetkezo.Enqueue(0);
            eddig.Add(0);

            while (kovetkezo.Count > 0)
            {
                a = kovetkezo.Dequeue();
            }

            foreach (var item in elek)
            {
                if (item.Csucs1 == a)
                {
                    if (!eddig.Contains(item.Csucs2))
                    {
                        kovetkezo.Enqueue(item.Csucs2);
                        eddig.Add(item.Csucs2);
                        g.Hozzaad(item.Csucs1, item.Csucs2);
                    }
                }
            }
            return g;
        }

        public bool Osszefuggo(int a)
        {
            HashSet<int> eddig = new HashSet<int>();
            Queue<int> kovetkezo = new Queue<int>();
            kovetkezo.Enqueue(0);
            eddig.Add(0);

            while (kovetkezo.Count > 0)
            {
                a = kovetkezo.Dequeue();
            }

            foreach (var item in elek)
            {
                if (item.Csucs1 == a && !eddig.Contains(item.Csucs2))
                {
                    kovetkezo.Enqueue(item.Csucs2);
                    eddig.Add(item.Csucs2);
                }
            }

            if (eddig.Equals(csucsokSzama))
            {
                Console.WriteLine("igen");
                return true;
            }
            else
            {
                Console.WriteLine("nem");
                return false;
            }
        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}