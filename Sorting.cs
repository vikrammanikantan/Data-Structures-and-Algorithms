using System;
using System.Runtime.ExceptionServices;
using System.Windows.Forms.VisualStyles;

namespace Assignment5
{
    /// <summary>
    /// Static class implementing the sort algorithms
    /// </summary>
    public static class Sorting
    {
        public static void DepthSort(Particle[] particles, bool useInsertion)
        {
            
            // You can select which sorting algorithm you'll be using by uncommenting one of the two function calls below
            // You can visually test both of your algorithms this way

            PerformanceMonitor.TimeExecution(() =>
                {
                    if (useInsertion)
                        InsertionDepthSort(particles);
                    else
                        QuicksortDepthSort(particles);
                }
            );
        }

        /// <summary>
        /// Sort the particles by decreasing depth
        /// </summary>
        /// <param name="particles">Array of particles to sort</param>
        public static void InsertionDepthSort(Particle[] particles)
        {
            for (int x = 1; x < particles.Length; x++)
            {
                Insert(particles, x, particles[x]);
            }
        }

        private static void Insert(Particle[] p, int pos, Particle value)
        {
            int i = pos - 1;
            for (i = pos - 1; i >= 0 && p[i].DistanceFromCamera < value.DistanceFromCamera; i--)
            {
                p[i + 1] = p[i];
            }

            p[i + 1] = value;
        }

        /// <summary>
        /// Sort the particles by decreasing depth
        /// </summary>
        /// <param name="particles">Array of particles to sort</param>
        public static void QuicksortDepthSort(Particle[] particles)
        {
            QuickSortRecur(particles, 0, particles.Length-1);
        }

        private static void QuickSortRecur(Particle[] particles, int start, int end)
        {            
            if (end > start)
            {
                int pIndex = (start+end)/2;
                int newIndex = Partition(particles, pIndex, start, end);

                QuickSortRecur(particles, start, newIndex-1);
                QuickSortRecur(particles, newIndex+1, end);
            }
        }

        private static int Partition(Particle[] particles, int pIndex, int start, int end)
        {
            float pValue = particles[pIndex].DistanceFromCamera;

            Particle temp = particles[pIndex];
            particles[pIndex] = particles[end];
            particles[end] = temp;

            int nextLeft = start;

            for (int i = start; i < end; i++)
            {
                if (particles[i].DistanceFromCamera >= pValue)
                {
                    temp = particles[i];
                    particles[i] = particles[nextLeft];
                    particles[nextLeft] = temp;
                    nextLeft++;
                }
            }

            temp = particles[nextLeft];
            particles[nextLeft] = particles[end];
            particles[end] = temp;

            return nextLeft;
        }
    }
}
