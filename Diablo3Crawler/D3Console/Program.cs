using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D3Core;
using D3Core.Models;

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
