using System;
using System.Linq;
using System.Reflection;
using Test;

namespace spp_lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Assembly.GetExecutingAssembly().Location;
           

            Assembly.LoadFrom(path).GetTypes()
                .Where(type => type.IsPublic && Attribute.GetCustomAttribute(type, typeof(ExportClass)) != null)
                .OrderBy(type => type.FullName)
                .ToList()
                .ForEach(type => Console.WriteLine(type.FullName));
        }
    }
}

namespace A
{
    [ExportClass]
    public class Hello
    {
    }

    public class A
    {
    }

    [ExportClass]
    public class B
    {
        
    }
}