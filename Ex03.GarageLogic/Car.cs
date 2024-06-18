using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public enum eCarColor
        {
            Red = 1,
            White = 2,
            Black = 3,
            Yellow = 4
        }

        private const int k_CarNumberOfWheels = 5;
        private const float k_CarMaxAirPressure = 31;
        private const int k_DefaultValueForColorAndDoors = -1;
        private eCarColor m_CarColor;
        private int m_NumberOfDoors;

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

        public int NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
            set
            {
                m_NumberOfDoors = value;
            }
        }

        public Car(Engine i_Engine, string i_LicenceNumber)
            : base(i_LicenceNumber, k_CarNumberOfWheels, k_CarMaxAirPressure, i_Engine)
        {
        }

        public override string ToString()
        {
            StringBuilder vehicleInformation = new StringBuilder();

            vehicleInformation.AppendFormat(base.ToString());
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            vehicleInformation.AppendFormat("Car Attributes:{0}", Environment.NewLine);
            vehicleInformation.AppendFormat("==============={0}", Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            vehicleInformation.AppendFormat("Vehicle Color: {0}{1}", CarColor, Environment.NewLine);
            vehicleInformation.AppendFormat("Number Of Doors: {0}", NumberOfDoors);

            return vehicleInformation.ToString();
        }


        public override List<string> SetVehicleQueriesList()
         {
             List<string> queriesList = new List<string>();

             queriesList.Add(@"Pick The Color Of The Car:
1. Red
2. White
3. Black
4. Yellow
");
             queriesList.Add("Enter How Many Doors The Car Has: ");

             return queriesList;
         }

        public override void SetResponsesForVehicleQueries(List<string> i_UserResponses)
        {
            Exception exception = checkForThrownExceptionsInResponses(i_UserResponses, out int o_ColorPicked, out int o_NumberOfDoors);

            if (exception != null)
            {
                throw exception;
            }
            else
            {
                m_CarColor = (Car.eCarColor)o_ColorPicked;
                m_NumberOfDoors = o_NumberOfDoors;
            }
        }

        private Exception checkForThrownExceptionsInResponses(
            List<string> i_UserResponses,
            out int o_ColorPicked,
            out int o_NumberOfDoors)
        {
            Exception exception = null;
            o_ColorPicked = k_DefaultValueForColorAndDoors;
            o_NumberOfDoors = k_DefaultValueForColorAndDoors;

            if (!int.TryParse(i_UserResponses[1], out o_NumberOfDoors))
            {
                exception = new FormatException("Invalid Input Format of The Number of Doors, Try Again: ");
                exception.Source = "1";
            }
            else if (ValueOutOfRangeException.IsValueOutOfRange(o_NumberOfDoors, 2, 5))
            {
                exception = new ValueOutOfRangeException(2, 5, "Number of Doors For Car Is Out of Range, Try Again (Choose a Number Between 2-5): ", exception);
                exception.Source = "1";
            }
            if (!int.TryParse(i_UserResponses[0], out o_ColorPicked))
            {
                exception = new FormatException("Invalid Input Format of The Car's Color, Try again: ");
                exception.Source = "0";
            }
            else if (ValueOutOfRangeException.IsValueOutOfRange(o_ColorPicked, 1, 4))
            {
                exception = new ValueOutOfRangeException(1, 4, "Color Choice For The Car is Out Of Range, Try Again: ", exception);
                exception.Source = "0";
            }

            return exception;
        }

    }
}
