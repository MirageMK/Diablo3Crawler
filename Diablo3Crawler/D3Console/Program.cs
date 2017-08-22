using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using D3Core;
using D3Core.Models;
using D3Core.Readers;

namespace D3Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new IcyVeinsReader();
            List<Item> items = reader.GetAll();
        }
    }
}
