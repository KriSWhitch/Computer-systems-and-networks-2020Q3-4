using System;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Threading;

namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Ping needs a host or IP Address.");

            string who = args[0];
            int number_of_packages = 4;
            int total_packages_lost = 0;
            try
            {
                Ping myPing = new Ping();
                Random rnd = new Random();

                for (int i = 0; i < number_of_packages; i++)
                {
                    int value = rnd.Next(1, 100);
                    string data = "";
                    for (int j = 0; j < value; j++)
                    {
                        data += "a";
                    }

                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    PingReply reply = myPing.Send(who, 1000, buffer);

                    if (reply != null && reply.Status.ToString() != "TimedOut")
                    {
                        Console.WriteLine("\nPackage: " + (i + 1));
                        Console.WriteLine(
                            $"Status : { reply.Status} \n" +
                            $"Time : {reply.RoundtripTime.ToString()} \n" +
                            $"Address : { reply.Address} \n" +
                            $"Buffer: {reply.Buffer.Length}"
                        );
                    }
                    else
                    {
                        total_packages_lost++;
                        Console.WriteLine("\nPackage " + (i + 1) + " lost");
                        continue;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine($"\nTotal Packages Lost: {total_packages_lost} / {number_of_packages}");
            }
            Console.ReadKey();
        }
    }
}
