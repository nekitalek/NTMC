using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using System.IO;

namespace miller_rabin
{
    class Program_MillerRabin
    {
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

        public static bool is_prime(BigInteger n)
        {

            BigInteger a, r, y;
            int i = 0, s = 0, j = 0;
            
            bool isprime = true;
            
            Random rnd = new Random();

            

            while (i < 100)
            {
                r = n - 1;
                do
                {
                    r = r / 2;
                    s++;

                } while (r % 2 == 0);
                //a = RandomBigInteger(n, rnd);
                a = rnd.Next(1, (int)n-2);
                y = BigInteger.ModPow(a, r, n);
                
                if (y != 1 && y != n - 1)
                {
                    j = 1;
                    while (j <= s - 1 && y != n - 1)
                    {
                        y = BigInteger.ModPow(y, 2, n);
                        
                        if (y == 1)
                        {
                            isprime = false;
                            break;
                        }
                        j++;
                    }
                   
                    if (y != n - 1)
                    {
                        isprime = false;
                        break;
                    }
                }
                
                i++;
            }
            return isprime;
        }
    }
}
