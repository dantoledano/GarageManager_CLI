using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_TruckNumberOfWheels = 16;
        private const float k_TruckMaxWheelAirPressure = 28;
        private const char k_HasToxinsAsChar = ' ';
        private const int k_DefaultCargoInitilize = -1;
        private bool m_IsTransportingToxins;
        private float m_CargoCapacity;

        public bool IsTransportingToxins
        {
            get
            {
                return m_IsTransportingToxins;
            }
            set
            {
                m_IsTransportingToxins = value;
            }
        }

        public float CargoCapacity
        {
            get
            {
                return m_CargoCapacity;
            }
            set
            {
                m_CargoCapacity = value;
            }
        }

        public Truck(Engine i_Engine, string i_LicenseNumber)
            : base(i_LicenseNumber, k_TruckNumberOfWheels, k_TruckMaxWheelAirPressure, i_Engine)
        {
        }

        public override string ToString()
        {
            StringBuilder vehicleInformation = new StringBuilder();

            vehicleInformation.Append(base.ToString());
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            vehicleInformation.AppendFormat("Truck Attributes:\n");
            vehicleInformation.AppendFormat("=================\n");
            Console.ForegroundColor = ConsoleColor.White;
            vehicleInformation.AppendFormat("Transporting Toxins: {0}\n", IsTransportingToxins);
            vehicleInformation.AppendFormat("Cargo Capacity: {0}", CargoCapacity);

            return vehicleInformation.ToString();
        }

        public override List<string> SetVehicleQueriesList()
        {
            List<string> queriesList = new List<string>();

            queriesList.Add("Are There Any Toxins The Truck Transport ? (Y/N) : ");
            queriesList.Add("Please Enter The Truck's Cargo Capacity: ");
            return queriesList;
        }

        public override void SetResponsesForVehicleQueries(List<string> i_QueriesResponses)
        {
            Exception exception = checkForThrownExceptionsInResponses(
                i_QueriesResponses,
                out char o_TruckHasToxins,
                out int o_CargoCapacity);

            if(exception != null)
            {
                throw exception;
            }
            else
            {
                m_CargoCapacity = o_CargoCapacity;
                if(o_TruckHasToxins == 'Y' || o_CargoCapacity == 'y')
                {
                    m_IsTransportingToxins = true;
                }
                else
                {
                    m_IsTransportingToxins = false;
                }
            }
        }


        private Exception checkForThrownExceptionsInResponses(List<string> i_QueriesResponses, out char o_TruckHasToxins, out int o_CargoCapacity)
        {
            Exception exception = null;
            o_TruckHasToxins = k_HasToxinsAsChar;
            o_CargoCapacity = k_DefaultCargoInitilize;
            o_TruckHasToxins = Char.ToUpper(o_TruckHasToxins);

            if(!char.TryParse(i_QueriesResponses[0], out o_TruckHasToxins))
            {
                exception = new FormatException("Format Of Toxic Materials In Truck Is Wrong. Enter Again: ");
                exception.Source = "0";
            }
            else if(!o_TruckHasToxins.Equals('N') && !o_TruckHasToxins.Equals('Y'))
            {
                exception = new ArgumentException("Enter Y or N - Does Truck Carrying Toxins. Enter Again: ");
                exception.Source = "0";
            }

            if(!int.TryParse(i_QueriesResponses[1], out o_CargoCapacity))
            {
                exception = new FormatException("Format OF Truck Cargo Is Wrong. Enter Again: ");
                exception.Source = "1";
            }

            else if(o_CargoCapacity < 0 || o_CargoCapacity > 9999999)// No maximum of cargo capacity were mentioned
            {
                exception = new ValueOutOfRangeException(1, 9999999, "Out Of Range Cargo Capacity");
                exception.Source = "1";
            }

            return exception;
        }
    }
}
