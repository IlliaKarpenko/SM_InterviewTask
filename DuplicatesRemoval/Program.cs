using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DuplicatesRemoval
{
    class Packet
    {
        private static Dictionary<int, byte> data;

        public static void ReadPackets()
        {
            string fileName = "dataset.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, @"", fileName);
            
            data = new Dictionary<int, byte>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int timestamp =Int32.Parse(line.Substring(0, 10));
                    byte inputValue = byte.Parse(line.Substring(12, 1));
                    data.Add(timestamp, inputValue);
                }
            }
        }

        public static void RemoveDuplicates()
        { 
            Dictionary<int, byte> pairs = new Dictionary<int, byte>();
            pairs.Add(data.First().Key, data.First().Value);

            //copy timestamp to a new dictionary only when input state changes
            for (int i = 1; i < data.Count(); i++)
                if(data.ElementAt(i).Value!= data.ElementAt(i-1).Value)
                    pairs.Add(data.ElementAt(i).Key, data.ElementAt(i).Value);
            data = pairs;
        }

        public static void PrintPairs()
        {
            foreach (KeyValuePair<int, byte> packet in data)
            {
                Console.Write(packet.Key + ": ");
                Console.WriteLine(packet.Value);
            }
        }

        static void Main(string[] args)
        {
            Packet.ReadPackets();
            Packet.RemoveDuplicates();
            Packet.PrintPairs();

            Console.Read();
        }
    }
}
