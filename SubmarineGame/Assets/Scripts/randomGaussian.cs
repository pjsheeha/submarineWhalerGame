/*
 * found in some random github
 * i have to test if it actually works.
 */


using UnityEngine;

public static class GaussianRandom
{
    /*
     * simulador: https://www.desmos.com/calculator/3iioyvma2l
     */
    public static float generateNormalRandom(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }

    public static void testGauss(float mu, float sigma)
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(i + ": " + generateNormalRandom(mu, sigma));
        }
    }
}



/* this shit makes sense in C
double gaussian(void)
{
   static double v, fac;
   static int phase = 0;
   double S, Z, U1, U2, u;

   if (phase)
      Z = v * fac;
   else
   {
      do
      {
         U1 = (double)rand() / RAND_MAX;
         U2 = (double)rand() / RAND_MAX;

         u = 2. * U1 - 1.;
         v = 2. * U2 - 1.;
         S = u * u + v * v;
      } while (S >= 1);

      fac = sqrt (-2. * log(S) / S);
      Z = u * fac;
   }

   phase = 1 - phase;

   return Z;
}
*/
