using System;

namespace L05
{

    class Program
    {
        // private static MetricsRepository _metricsRepository = ;
        static void Main(string[] args)
        {
            new MetricsRepository().GetStatistics().GetAwaiter().GetResult();
        }
    }
}
