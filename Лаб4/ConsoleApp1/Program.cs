using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using miller_rabin;

namespace @base
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger a, b, p, q;
            a = 7;
            b = 516;
            p = BigInteger.Parse("547");
            q = BigInteger.Parse("546");
            base1(a, b, p, q);
            //gelford(a, b, p, q);
            pollard(a, b, p, q);

            // a = 2
            // b = 3
            // p = 87659432019823474919
            // q = 43829716009911737459
        }
        static void base1(BigInteger a, BigInteger b, BigInteger p, BigInteger r)
        {
        beg:
            BigInteger p_h, prime_num, u, powmod, f = 0, len = 0;
            int i, i1 = 0;

            

            p_h = (BigInteger)Math.Exp(Math.Sqrt((BigInteger.Log(p)) * Math.Log(BigInteger.Log(p)))) + 1;
            BigInteger[] base1 = new BigInteger[(int)p_h];
            base1[0] = 2;
            i = 1;
            Console.WriteLine("************");
           for (prime_num = 5; prime_num <= p_h; prime_num += 2)
            {
                if (Program_MillerRabin.is_prime(prime_num) == true)
                {
                    base1[i] = prime_num;
                    if (i > 21) break;
                    Console.WriteLine(base1[i]);
                    i++;
                }
               
            }
           
            Console.WriteLine("************");
            Random rnd = new Random();
            len = i;
            BigInteger[] pows = new BigInteger[(int)len];

            double[,] AX = new double[(int)len, (int)len];
            double[] B = new double[(int)len];
            double[] result = new double[(int)len];

            int info;
            alglib.densesolverreport rep;

            int iii = 0;
            while (true)
            {
                while (true)
                {
                    Array.Clear(pows, 0, pows.Length);
                    u = Program_MillerRabin.RandomBigInteger(p, rnd);
                    powmod = BigInteger.ModPow(a, u, p);
                    for (i = 0; i < len; i++)
                    {
                        if (base1[i] == 0) break;
                        while (powmod % base1[i] == 0)
                        {
                            powmod = powmod / base1[i];
                            pows[i] += 1;
                        }
                    }
                    //foreach (double value in pows) Console.Write("{0} ", value);
                    //Console.WriteLine();
                    ////Console.ReadLine();
                    
                    if (powmod == 1)
                    {
                        iii++;
                        if (iii > 50000) goto beg;
                        f++;
                        for (i = 0; i < len; i++)
                        {
                            AX[i1, i] = (double)pows[i];
                        }

                        B[i1] = (double)u;//приписали к концу массива u
                        i1++;

                        if (f >= len)
                        {
                            i1 = 0;
                            break;
                        }
                    }
                }

                alglib.rmatrixsolve(AX, (int)len, B, out info, out rep, out result);
                if (info == -3)
                {
                    f = 0;
                    Array.Clear(B, 0, B.Length);
                    Array.Clear(AX, 0, AX.Length);
                }
                else break;

            }
            

            Array.Clear(pows, 0, pows.Length);
            iii = 0;
            double x = 0;
            double[] lst = new double[(int)len];
            BigInteger ax = 0;

            while (true)
            {
                u = Program_MillerRabin.RandomBigInteger(p, rnd);
                powmod = (BigInteger.ModPow(a, u, p) * b) % p;
                Array.Clear(pows, 0, pows.Length);
                for (i = 0; i < len; i++)
                {
                    if (base1[i] == 0) break;
                    while (powmod % base1[i] == 0)
                    {
                        powmod = powmod / base1[i];
                        pows[i] += 1;
                    }
                }
              
                x = 0;
                Array.Clear(lst, 0, lst.Length);
                if (powmod == 1)
                {
                    for (i = 0; i < len; i++)
                        lst[i] = result[i] * (double)pows[i];

                    for (i = 0; i < len; i++)
                        x = x + lst[i];

                    x = x - (double)u;
                    x = Math.Round(x, MidpointRounding.ToEven);
                    if (x >= 0)
                    {
                        iii++;
                        if (iii > 10000) goto beg;
                        ax = BigInteger.ModPow(a, (BigInteger)x, p);
                        if (ax == b)
                        {
                            Console.WriteLine("{0}^{1}={2}(mod {3})", a,(BigInteger)x % r,b,p);
                            break;
                        }
                    }
                }
            }
           

        }
        static void gelford(BigInteger a, BigInteger b, BigInteger p, BigInteger r)
        {
            BigInteger s, revers, i, a_k, result = 0;
            bool state = false;
            Tuple<BigInteger, BigInteger, BigInteger> retval;


            s = (BigInteger)Math.Sqrt((double)r) + 1; // наш H
            Console.WriteLine("s={0}", s);
            retval = euclid(BigInteger.ModPow(a, s, p), p);
            Console.WriteLine("nod={0}", retval.Item1 + p);
            revers = retval.Item1 + p;
            SortedDictionary<BigInteger, BigInteger> base1 = new SortedDictionary<BigInteger, BigInteger>();

            for (i = 0; i <= s - 1; i++)
            {
                base1.Add((b * BigInteger.ModPow(revers, i, p)) % p, i);
                //if (i <= 5 || i > 10000000)
                    Console.WriteLine("{0}  {1}", (b * BigInteger.ModPow(revers, i, p)) % p, i);
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape) break;
            }


            SortedDictionary<BigInteger, BigInteger>.ValueCollection i_num = base1.Values;
            SortedDictionary<BigInteger, BigInteger>.KeyCollection element = base1.Keys;
            for (i = 0; i <= s - 1; i++)
            {
                a_k = BigInteger.ModPow(a, i, p) % p;
                foreach (BigInteger j in element)
                {
                    if (j > a_k) break;
                    if (j == a_k)
                    {
                        result = (base1[j] * s + i) % r;
                        state = true;
                        break;
                    }
                }
                if (state == true) break;
            }

            if (state == true) Console.WriteLine("{0}^{1}={2}(mod {3})", a, result, b, p);
            else Console.WriteLine("Error!");


        }
        static void pollard(BigInteger a, BigInteger b, BigInteger p, BigInteger r)
        {
            BigInteger u, v, c, d, k, m, vm, ku, x, iter = 0;
            Tuple<BigInteger, BigInteger, BigInteger> retval;
            Random rnd = new Random();
            u = RandomBigInteger(p, rnd);
            v = RandomBigInteger(p, rnd);
            Console.WriteLine("{0} {1}", u, v);
            k = u;
            m = v;
            c = (BigInteger.ModPow(a, u, p) * BigInteger.ModPow(b, v, p)) % p;
            d = c;
            Console.Write("i={0}  c = {1},  u={2}  v={3}     ", iter, c, u, v);
            Console.Write("d = {0},  k={1}  m={2}\r\n", d, k, m);
            do
            {
                iter++;
                retval = func(c, p, a, b, u, v, r);
                c = retval.Item1;
                u = retval.Item2;
                v = retval.Item3;
                    Console.Write("i={0}  c = {1},  u={2}  v={3}     ", iter, c, u, v);

                retval = func(d, p, a, b, k, m, r);
                d = retval.Item1;
                k = retval.Item2;
                m = retval.Item3;

                retval = func(d, p, a, b, k, m, r);
                d = retval.Item1;
                k = retval.Item2;
                m = retval.Item3;
                    Console.Write("d = {0},  k={1}  m={2}\r\n", d, k, m);

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape) break;

            } while (c != d);
            Console.WriteLine("{0} {1}", v, m);
            vm = v - m;
            ku = k - u;
            if (ku % r == 0)
            {
                Console.WriteLine("False1");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("{0} {1}", vm, r);
            retval = euclid(vm, r);
            Console.WriteLine("{0} {1} {2}", retval.Item1, retval.Item2, retval.Item3);
            x = retval.Item1;
            d = retval.Item3;
            if (d != 1)
            {
                Console.WriteLine("False2");
                Console.ReadLine();
                return;
            }

            x *= ku;

            if (x < 0)
                while (x < 0) x += r;
            else
                while (x >= r) x -= r;

            Console.WriteLine("{0}^{1}={2}(mod {3})", a, x, b, p);
        }
        public static BigInteger RandomBigInteger(BigInteger n, Random rnd)
        {
            byte[] bytes = n.ToByteArray();
            BigInteger a;
            do
            {
                rnd.NextBytes(bytes);
                a = new BigInteger(bytes);
            } while (a > n - 2 || a < 2);

            return a;
        }
        static public Tuple<BigInteger, BigInteger, BigInteger> func(BigInteger c, BigInteger p, BigInteger a,
            BigInteger b, BigInteger u, BigInteger v, BigInteger r)
        {
            if (c < p / 2)
            {
                c = (a * c) % p;
                u = (u + 1) % r;
                return Tuple.Create(c, u, v);
            }
            else
            {
                c = (b * c) % p;
                v = (v + 1) % r;
                return Tuple.Create(c, u, v);
            }
        }
        static public Tuple<BigInteger, BigInteger, BigInteger> euclid(BigInteger a, BigInteger b)
        {
            BigInteger r0, r1, r2, q, x0, x1, x2, y0, y1, y2;
            int i = 0;

            r0 = a;
            r1 = b;
            x0 = 1;
            x1 = 0;
            y0 = 0;
            y1 = 1;

            do
            {
                i++;
                if (r1 < 0)
                {
                    r0 = -r0;
                    r1 = -r1;
                }

                q = r0 / r1;

                r2 = r0 - r1 * q;
                while (r2 < 0)
                {
                    q -= 1;
                    r2 += r1;
                }

                if (r2 > BigInteger.Abs(r1) / 2)
                {
                    r2 -= r1;
                    x0 -= x1;
                    y0 -= y1;
                }

                x2 = x0 - q * x1;
                y2 = y0 - q * y1;
                r0 = r1;
                r1 = r2;
                x0 = x1;
                x1 = x2;
                y0 = y1;
                y1 = y2;

            } while (r2 != 0);

            if (a * x0 + b * y0 < 0)
            {
                if (x0 < 0)
                {
                    x0 = -x0;
                    y0 = -y0;
                }
                else
                {
                    x0 = -x0;
                    y0 = -y0;
                }
            }

            return Tuple.Create(x0, y0, r0);

        }
    }
}
