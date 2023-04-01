using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SallivanExamples
{
    internal class Program
    {
        const int IE = 60;
        const int JE = 60;
        static void Main(string[] args)
        {
            float[,] ga = new float[IE, JE];
            float[,] dz = new float[IE, JE];
            float[,] ez = new float[IE, JE];
            float[,] hx = new float[IE, JE];
            float[,] hy = new float[IE, JE];
            int l, n, i, j, ic, jc, nsteps;
            float ddx, dt, epsz, T, pi, epsilon, sigma, eaf;
            float t0, spread, pulse;

            ic = IE / 2;
            jc = JE / 2;
            ddx = .01f; //Cell size
            dt = ddx / 6e8f; //Time steps
            epsz = 8.8e1f - 12;
            pi = 3.14159f;

            for (j = 0; j < JE; j++)
            {
                Console.Write(j);
                for (i = 0; i < IE; i++)
                {
                    dz[i, j] = 0.0f;
                    ez[i, j] = 0.0f;
                    hx[i, j] = 0.0f;
                    hy[i, j] = 0.0f;
                    ga[i, j] = 1.0f;
                    Console.Write(ga[i, j]);
                }
                Console.WriteLine();
            }

            t0 = 20.0f;
            spread = 6.0f;
            T = 0;
            nsteps = 1;

            while (nsteps > 0)
            {
                Console.Write("nsteps --> ");
                nsteps = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(nsteps);

                for (n = 1; n <= nsteps; n++)
                {
                    T += 1;

                    //Start of the Main FDTD loop

                    //Calculate the Ex field
                    for (j = 1; j < JE; j++)
                    {
                        for (i = 1; i < IE; i++)
                        {
                            dz[i, j] += .5f * (hy[i, j] - hy[i - 1, j] - hx[i, j] + hx[i, j - 1]);

                        }
                    }
                    //Put a Gaussian pulse in the middle
                    pulse = (float)Math.Exp(-.5 * (Math.Pow((t0 - T) / spread, 2.0)));
                    dz[ic, jc] = pulse;

                    // Calculate the Ex field
                    for (j = 1; j < JE; j++)
                    {
                        for (i = 1; i < IE; i++)
                        {
                            ez[i, j] = ga[i, j] * dz[i, j];
                        }
                    }

                    //Calculate the Hx field
                    for (j = 0; j < JE - 1; j++)
                    {
                        for (i = 0; i < IE - 1; i++)
                        {
                            hx[i, j] += .5f * (ez[i, j] - ez[i, j + 1]);
                        }
                        /* Calculate the Hy field */
                        for (j = 0; j < JE - 1; j++)
                        {
                            for (i = 0; i <= IE - 1; i++)
                            {
                                hy[i, j] += .5f * (ez[i, j] - ez[i, j]);
                            }
                        }
                    }
                    //End of the main FDTD loop
                    for (j = 1; j < jc; j++)
                    {
                        Console.Write(j);
                        Console.Write(" ");
                        for (i = 1; i < ic; i++)
                        {
                            Console.Write(ez[2 * i, 2 * j]);
                            Console.Write(" ");
                        }
                        Console.WriteLine();
                    }
                    Console.Write("T = ", T);

                    //Write the E field out to a file "Ez
                    string path = @"D:\ALEK\diploma\Rodionov\Repository\Diplom-VirtualLaboratoryWorkshop-\SallivanExamples\Пример 1.Text.txt";
                    try
                    {
                        StreamWriter sw = new StreamWriter(path);
                        for (j = 0; j < JE; j++)
                        {
                            for (i = 0; i < IE; i++)
                            {
                                sw.Write(ez[i, j]);
                                sw.Write(" ");
                            }
                            sw.WriteLine("\n");
                        }
                        sw.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                    }
                }
            }
        }
    }
}
