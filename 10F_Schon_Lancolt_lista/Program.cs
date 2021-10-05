using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10F_Schon_Lancolt_lista
{
    class Program
    {
        class LancoltLista<T>
        {
            private class Elem<R>
            {
                public Elem<R> bal;
                public R ertek;
                public Elem<R> jobb;
                public Elem(Elem<R> b, R e, Elem<R> j)
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
                public Elem(Elem<R> ezele, R e)
                {
                    this.jobb = ezele;
                    this.bal = ezele.bal;
                    ezele.bal.jobb = this;
                    ezele.bal = this;
                    this.ertek = e;
                }
                public static Elem<R> operator ++(Elem<R> a) => a.jobb;
                public static Elem<R> operator --(Elem<R> a) => a.bal;
            }
            Elem<T> fejelem = new Elem<T>();
            private int count=0;
            public int Count { get { return count; } }
            public void Add(T ertek)
            {
                new Elem<T>(fejelem, ertek); // emlékezzünk: a fejelem előtt elem mindig az utolsó elem!
                count++;
            }
            public void Kiir() { Console.WriteLine(this.ToString()); }
            public override string ToString()
            {
                string str = "";
                Elem<T> aktelem = fejelem.jobb;
                while (aktelem != fejelem)
                {
                    str+=$"{aktelem.ertek} ";
                    aktelem = aktelem.jobb;
                }
                return "[ "+str+"]";
            }
            public bool Empty() => fejelem.jobb == fejelem;
            private Elem<T> Helye(T e)
            {
                Elem<T> aktelem = fejelem.jobb; // i=0
                while (aktelem != fejelem && !aktelem.ertek.Equals(e)) // i<lista.count && feltétel
                {
                    aktelem++; //"i++"
                }
                return aktelem;
            }
            /// <summary>
            /// Az első előfordulást kiszedi
            /// </summary>
            /// <param name="ertek">az eltávolítandó elem</param>
            public void Remove(T e)
            {
                if (!Empty())
                {
                    Elem<T> aktelem = Helye(e);
                    aktelem.bal.jobb = aktelem.jobb;
                    aktelem.jobb.bal = aktelem.bal;
                    count--;
                }
            }
            public bool Contains(T e) => Helye(e) != fejelem;
            private Elem<T> GetElemByIndex(int i)
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

                Elem<T> aktelem = fejelem.jobb;
                for (int j = 0; j < i; j++)
                {
                    aktelem = aktelem.jobb;
                }
                return aktelem;
            }
            public T this[int i]
            {
                get => GetElemByIndex(i).ertek;
                set { GetElemByIndex(i).ertek = value; } // lista[i]=... 
            }

            //ti munkáitok:
            /* ez nem fog működni, mert nem tudjuk, hogy T-nek egyáltalán van-e > vagy < operátora! 
             * * /
            public int Min() // 
            {
                if (Empty())
                {
                    Console.WriteLine("Üres listában azért nem kéne optimumot keresni!");
                    throw new Exception();
                }

                T min = fejelem.jobb.ertek;

                Elem<T> aktelem = fejelem.jobb.jobb;
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
            public int Min(Func<int, int> selector)
            {
                if (Empty())
                {
                    Console.WriteLine("Üres listában azért nem kéne optimumot keresni!");
                    throw new Exception();
                }

                int selected_min = selector(fejelem.jobb.ertek);
                int min = fejelem.jobb.ertek;



                Elem aktelem = fejelem.jobb.jobb;
                while (aktelem != fejelem)
                {
                    int sertek = selector(aktelem.ertek);
                    if (sertek < selected_min)
                    {
                        selected_min = sertek;
                        min = aktelem.ertek;
                    }
                    aktelem = aktelem.jobb;
                }
                return min;
            }

            public int Max()
            {
                if (Empty())
                {
                    Console.WriteLine("Üres listában azért nem kéne optimumot keresni!");
                    throw new Exception();
                }
                int max = fejelem.jobb.ertek;
                Elem aktelem = fejelem.jobb.jobb;
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

            /**/
            public System.Collections.Generic.List<T> ToList()
            {
                System.Collections.Generic.List<T> lista = new System.Collections.Generic.List<T>(this.Count);
                Elem<T> aktelem = fejelem.jobb;
                lista.Max();

                while (aktelem != fejelem)
                {
                    lista.Add(aktelem.ertek);
                    aktelem = aktelem.jobb;
                }

                return lista;
            }
            public int IndexOf(int e) // ciklus optimalizálható logikával
            {
                int i = 0;
                Elem<T> aktelem = fejelem.jobb;
                while (!(aktelem == fejelem || aktelem.ertek.Equals(e)))
                {
                    aktelem = aktelem.jobb;
                    i++;
                }

                return i == count ? -1 : i;
            }
            public T Find(Func<T, bool> predicate)
            {
                Elem<T> aktelem = fejelem.jobb;
                while (aktelem != fejelem)
                {
                    if (predicate(aktelem.ertek))
                        return aktelem.ertek;
                    aktelem = aktelem.jobb;
                }


                Console.WriteLine("Nincsen a megadott tulajdonsággal rendelkező elem a listában.");
                throw new Exception();
            }
            public LancoltLista<T> Where(Func<T, bool> predicate)
            {
                LancoltLista<T> lista = new LancoltLista<T>();
                Elem<T> aktelem = fejelem.jobb;
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
            /** /
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
            /**/
            public int FindIndex(Func<T, bool> predicate)
            {
                Elem<T> aktelem = fejelem.jobb;
                int i = 0;
                while (aktelem != fejelem)
                {
                    if (predicate(fejelem.ertek))
                        return i;
                    aktelem = aktelem.jobb;
                    i++;
                }
                return -1;
            }
            /* TO DO LIST
             * 1. RemoveAt()
             * 2. Insert
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
             * Min, Max (overload predikátummal)
             * Min, Max (overload relációval)
             * 
             * 19. MaxIndex
             * 20. MinIndex
             */
        }


        static void Main(string[] args)
        {
            
            LancoltLista<int> lista = new LancoltLista<int>();

            lista.Add(5);
            lista.Add(6);
            lista.Add(7);
            lista.Add(1);
            Console.WriteLine(lista);

            lista.Remove(-1);
            Console.WriteLine(lista);




            /**/
            System.Collections.Generic.List<int> benalista = new System.Collections.Generic.List<int>();
            benalista.Add(5);

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

            Console.ReadKey();
        }
    }
}
