using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            UIManagar garageUI = new UIManagar();
            garageUI.InitilizeUIManager();
        }
    }
}
