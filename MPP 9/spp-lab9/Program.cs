using System;

namespace spp_lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            var dynamicList = new DynamicList<int>();
            
            Console.WriteLine(dynamicList.Count);
            
            for (int i = 1; i <= 10; i++)
            {
                dynamicList.Add(i);
            }
            foreach (int element in dynamicList)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine("\n");
            Console.WriteLine(dynamicList.Count);
            
            dynamicList.Remove(4);
            foreach (int i in dynamicList)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(dynamicList.Count);
            
            Console.WriteLine(dynamicList[1]);
            
            dynamicList.Clear();
            
            Console.WriteLine(dynamicList.Count);
        }
    }
}