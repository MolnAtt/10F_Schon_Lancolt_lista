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

            /// <summary>
            /// Az első előfordulást kiszedi
            /// </summary>
            /// <param name="ertek">az eltávolítandó elem</param>
            public void Remove(int e)
            {
                if (!Empty())
                {
                    Elem aktelem = fejelem.jobb; // i=0
                    while (aktelem != fejelem && aktelem.ertek != e) // i<lista.count && feltétel
                    {
                        aktelem = aktelem.jobb; //"i++"
                    }
                    aktelem.bal.jobb = aktelem.jobb;
                    aktelem.jobb.bal = aktelem.bal;
                    count--;
                }
            }
            /* TO DO LIST
             * 
             * lista[3]
             * RemoveAt()
             * FindIndex()
             * Contains()
             * AddRange()
             * ToList()
             * Find()
             * FindAll()
             * FindLast()
             * FindLastIndex()
             * IndexOf
             * LastIndexOf
             * Insert
             * InsertRange
             * RemoveAll
             * Reverse
             * Sort
             * Where
             * Select
             * Max
             * Min
             * MaxIndex
             * MinIndex
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
            Console.WriteLine(lista);
            Console.WriteLine(lista.Count);

            lista.Remove(6);
            Console.WriteLine(lista);
            Console.WriteLine(lista.Count);

            lista.Remove(7);
            Console.WriteLine(lista);
            Console.WriteLine(lista.Count);

            lista.Remove(1);
            Console.WriteLine(lista);
            Console.WriteLine(lista.Count);

            lista.Remove(5);
            Console.WriteLine(lista);
            Console.WriteLine(lista.Count);




            /**/
            System.Collections.Generic.List<int> benalista = new System.Collections.Generic.List<int>();
            benalista.Add(5);

            Console.WriteLine(benalista.Count);
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
