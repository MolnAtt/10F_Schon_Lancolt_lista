using System;
using System.Text;
using System.Threading.Tasks;

namespace _10F_Schon_Lancolt_lista
{
    class Program
    {
        class LancoltLista
        {
            private class Elem
            {
                public Elem bal;
                public int ertek;
                public Elem jobb;

                public Elem(Elem b, int e, Elem j)
                {
                    this.bal = b;
                    this.jobb = j;
                    this.ertek = e;
                }
                public Elem() // hozza létre ez a fejelemet
                {
                    bal = this;
                    jobb = this;
                }

                public Elem(Elem ezele, int e)
                {
                    this.jobb = ezele;
                    this.bal = ezele.bal;
                    ezele.bal.jobb = this;
                    ezele.bal = this;
                    this.ertek = e;
                }
            }

            Elem fejelem = new Elem();
            private int count=0;
            public int Count { get { return count; } }

            public void Add(int szam)
            {
                new Elem(fejelem, szam); // emlékezzünk: a fejelem előtt elem mindig az utolsó elem!
                count++;
            }
            public void Kiir() { Console.WriteLine(this.ToString()); }
            public override string ToString()
            {
                string str = "";
                Elem aktelem = fejelem.jobb;
                while (aktelem != fejelem)
                {
                    str+=$"{aktelem.ertek} ";
                    aktelem = aktelem.jobb;
                }
                return str;
            }


            public bool Empty() => fejelem.jobb == fejelem;

            private Elem Helye(int e)
            {
                Elem aktelem = fejelem.jobb; // i=0
                while (aktelem != fejelem && aktelem.ertek != e) // i<lista.count && feltétel
                {
                    aktelem = aktelem.jobb; //"i++"
                }
                return aktelem;
            }

            /// <summary>
            /// Az első előfordulást kiszedi
            /// </summary>
            /// <param name="ertek">az eltávolítandó elem</param>
            public void Remove(int e)
            {
                if (!Empty())
                {
                    Elem aktelem = Helye(e);
                    aktelem.bal.jobb = aktelem.jobb;
                    aktelem.jobb.bal = aktelem.bal;
                    count--;
                }
            }

            public bool Contains(int e) => Helye(e) != fejelem;

            private Elem GetElemByIndex(int i)
            {
                if (i < 0)
                {
                    Console.WriteLine("pozitív indexet kérek!");
                    throw new IndexOutOfRangeException();
                }
                if (i >= count)
                {
                    Console.WriteLine("túl nagy index!");
                    throw new IndexOutOfRangeException();
                }

                Elem aktelem = fejelem.jobb;
                for (int j = 0; j < i; j++)
                {
                    aktelem = aktelem.jobb;
                }
                return aktelem;
            }

            public int this[int i]
            {
                get => GetElemByIndex(i).ertek;
                set { GetElemByIndex(i).ertek = value; } // lista[i]=... 
            }

            public int Min()// üres/egyelemű listára mit ad?
            {
                Elem aktelem = fejelem.jobb.jobb;
                int min = fejelem.jobb.ertek;
                while (aktelem != fejelem)
                {
                    if (aktelem.ertek < min)
                    {
                        min = aktelem.ertek;
                    }
                    aktelem = aktelem.jobb;
                }
                return min;
            }
            public int Max()
            {
                Elem aktelem = fejelem.jobb.jobb;
                int max = fejelem.jobb.ertek;
                while (aktelem != fejelem)
                {
                    if (aktelem.ertek > max)
                    {
                        max = aktelem.ertek;
                    }
                    aktelem = aktelem.jobb;
                }

                return max;
            }
            public System.Collections.Generic.List<int> ToList()
            {
                System.Collections.Generic.List<int> lista = new System.Collections.Generic.List<int>(this.Count);
                Elem aktelem = fejelem.jobb;

                while (aktelem != fejelem)
                {
                    lista.Add(aktelem.ertek);
                    aktelem = aktelem.jobb;
                }

                return lista;
            }
            public int IndexOf(int e) // ciklus optimalizálható logikával
            {
                int result = 0;
                Elem aktelem = fejelem.jobb;
                while (aktelem != fejelem && aktelem.ertek != e)
                {
                    aktelem = aktelem.jobb;
                    result++;
                }

                return result == count ? -1 : result;
            }

            public int Find(Func<int, bool> predicate)
            {
                Elem aktelem = fejelem.jobb;
                while (aktelem != fejelem)
                {
                    if (predicate(aktelem.ertek))
                        return aktelem.ertek;
                    aktelem = aktelem.jobb;
                }
                Console.WriteLine("Nincsen a megadott tulajdonsággal rendelkező elem a listában.");
                throw new Exception();
            }

            public LancoltLista Where(Func<int, bool> predicate)
            {
                LancoltLista lista = new LancoltLista();
                Elem aktelem = fejelem.jobb;
                while (aktelem != fejelem)
                {
                    if (predicate(aktelem.ertek))
                    {
                        lista.Add(aktelem.ertek);
                    }
                    aktelem = aktelem.jobb;
                }
                return lista;
            }
            public int MaxIndex()
            {
                Elem aktelem = fejelem.jobb.jobb;

                int maximum = fejelem.jobb.ertek;
                int i = 0;
                int maxix = 0;

                while (aktelem != fejelem)
                {
                    if (aktelem.ertek > maximum)
                    {
                        maximum = aktelem.ertek;
                        maxix = i;
                    }

                    aktelem = aktelem.jobb;
                    i++;
                }

                return maxix;
            }

            public int FindIndex(Func<int, bool> predicate)
            {
                Elem aktelem = fejelem.jobb;
                int ertek;
                int i = 0;
                while (aktelem != fejelem)
                {
                    if (predicate(fejelem.ertek))
                    {
                        ertek = aktelem.ertek;
                        return i;
                    }
                    aktelem = aktelem.jobb;
                    i++;
                }
                return -1;
            }

            /* TO DO LIST
             * 1. RemoveAt()
             * 2. Insert
             * 3. FindIndex()
             * 4. AddRange()
             * 5. InsertRange
             * 8. FindAll()
             * 9. FindLast()
             * 10. FindLastIndex()
             * 12. LastIndexOf
             * 13. RemoveAll
             * 14. Reverse
             * BubbleSort (implementáljunk egy privát csere függvényt!)
             * 15. Where
             * 16. Select
             * 17. Max
             * Min, Max (overload szelektorral)
             * Min, Max (overload predikátummal)
             * Min, Max (overload relációval)
             * 
             * 19. MaxIndex
             * 20. MinIndex
             */
        }


        static void Main(string[] args)
        {
            
            LancoltLista lista = new LancoltLista();

            lista.Add(5);
            lista.Add(6);
            lista.Add(7);
            lista.Add(1);

            lista.Kiir();

            Console.WriteLine("[ "+lista+"]");

            // lista.Count = 100;
            int valtozo = lista.Count;

            Console.WriteLine(valtozo);

            lista.Remove(5);
            Console.WriteLine($"[{lista}] -> {lista.Count} db elem");
            lista.Remove(6);
            Console.WriteLine($"[{lista}] -> {lista.Count} db elem");

            int ez = 1;
            Console.WriteLine($"A(z) {ez} benne van? {lista.Contains(ez)}");

            int i = 0;
            Console.WriteLine($"A lista {i}. eleme {lista[i]}");
            lista[i] = 13;
            Console.WriteLine($"A lista {i}. eleme {lista[i]}");

            

            /*
             lista.GetByIndex(2) ===== lista[2]
            int x = lista.GetByIndex(2)
            int x = lista[2]
            lista[2] = 7
            lista.GetByIndex(2) = 7
             */


            /**/
            System.Collections.Generic.List<int> benalista = new System.Collections.Generic.List<int>();
            benalista.Add(5);

            Console.WriteLine(benalista.Count);
            benalista.find
            /**/
            /** /
            Elem f = new Elem();
            Elem e1 = new Elem();
            Elem e2 = new Elem();
            Elem e3 = new Elem();

            f.bal = e3;
            f.jobb = e1;

            e1.bal = f;
            e1.ertek = 2;
            e1.jobb = e2;

            e2.bal = e1;
            e2.ertek = 3;
            e2.jobb = e3;

            e3.bal = e2;
            e3.ertek = 5;
            e3.jobb = f;

            //-----------------

            // új elemet adjunk hozzá!
            Elem e4 = new Elem(e3, 8, f);
            e3.jobb = e4;
            f.bal = e4;

            Elem e5 = new Elem(e4, 13); // az e4 elé szúrjuk be a 13 tartalmú dolgot!

            Console.WriteLine(f.ertek);
            Console.WriteLine(f.jobb.ertek);
            Console.WriteLine(f.jobb.jobb.ertek);
            Console.WriteLine(f.jobb.jobb.jobb.ertek);
            Console.WriteLine(f.jobb.jobb.jobb.jobb.ertek);
            Console.WriteLine(f.jobb.jobb.jobb.jobb.jobb.ertek);

            /**/
        }
    }
}
