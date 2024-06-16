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
             StringBuilder carAttributes = new StringBuilder();

             //carAttributes.Append(base.ToString());
             carAttributes.AppendFormat(
                 @"Car Attributes:
                   Color: {0}
                   Number of Doors: {1}", m_CarColor.ToString(), m_NumberOfDoors.ToString());
             carAttributes.Append(Environment.NewLine);
             Console.WriteLine(carAttributes.ToString());
             

             return carAttributes.ToString();
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

        public override void SetResponsesForVehicleQueries(List<string> i_userResponses)
        {
            Exception exception = checkForThrownExceptionsInResponses(i_userResponses, out int o_ColorPicked, out int o_NumberOfDoors);

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
            List<string> i_userResponses,
            out int o_ColorPicked,
            out int o_NumberOfDoors)
        {
            Exception exception = null;
            o_ColorPicked = k_DefaultValueForColorAndDoors;
            o_NumberOfDoors = k_DefaultValueForColorAndDoors;

            if (!int.TryParse(i_userResponses[1], out o_NumberOfDoors))
            {
                exception = new FormatException("Invalid format of input of the number of doors, please try again: ");
                exception.Source = "1";
            }
            else if (ValueOutOfRangeException.IsValueOutOfRange(o_NumberOfDoors, 2, 5))
            {
                exception = new ValueOutOfRangeException(2, 5, "Number of doors for car is out of range, please try again (Choose a number between 2-5): ", exception);
                exception.Source = "1";
            }
            if (!int.TryParse(i_userResponses[0], out o_ColorPicked))
            {
                exception = new FormatException("Format of input of the color isn't valid, please try again: ");
                exception.Source = "0";
            }
            else if (ValueOutOfRangeException.IsValueOutOfRange(o_ColorPicked, 1, 4))
            {
                exception = new ValueOutOfRangeException(1, 4, "Color choice for the car is out of range, please try again: ", exception);
                exception.Source = "0";
            }

            return exception;
        }

    }
}
