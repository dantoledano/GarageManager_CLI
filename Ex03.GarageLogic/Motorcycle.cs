using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A=1,
            A1=2,
            AA=3,
            B1=4
        }

        private const int k_CarNumberOfWheels = 2;
        private const float k_CarMaxAirPressure = 33;
        private const int k_DefaultValueForLicenseAndCapacity = -1;
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
            set
            {
                m_EngineCapacity = value;
            }
        }

        public Motorcycle(Engine i_Engine, string i_LicenseNumber)
            : base(i_LicenseNumber, k_CarNumberOfWheels, k_CarMaxAirPressure, i_Engine)
        {
        }

        public override string ToString()
        {
            StringBuilder vehicleInformation = new StringBuilder();

            vehicleInformation.AppendFormat(base.ToString());
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            vehicleInformation.AppendFormat("Motorcycle Attributes:\n");
            vehicleInformation.AppendFormat("======================\n");
            Console.ForegroundColor = ConsoleColor.White;
            vehicleInformation.AppendFormat("EngineCapacity {0}\n", EngineCapacity);
            vehicleInformation.AppendFormat("License Type: {0}", LicenseType.ToString());

            return vehicleInformation.ToString();
        }

    

    public override List<string> SetVehicleQueriesList()
        {
            List<string> queriesList = new List<string>();

            queriesList.Add(@"Choose Motorcycle's License Type:
1. A
2. A1
3. AA
4. B1
");
            queriesList.Add("Enter Motorcycle's Engine Capacity: ");
            
            return queriesList;
        }

        public override void SetResponsesForVehicleQueries(List<string> i_QueriesResponses)
        {
            Exception exception = checkForThrownExceptionsInResponses(i_QueriesResponses, out int o_LicenseType, out int o_EngineCapacity);
            if(exception != null)
            {
                throw exception;
            }

            else
            {
                m_LicenseType = (Motorcycle.eLicenseType)o_LicenseType;
                m_EngineCapacity = o_EngineCapacity;
            }
        }

        private Exception checkForThrownExceptionsInResponses(
            List<string> i_userResponses,
            out int o_LicenseType,
            out int o_EngineCapacity)
        {
            Exception exception = null;
            o_EngineCapacity = k_DefaultValueForLicenseAndCapacity;
            o_LicenseType = k_DefaultValueForLicenseAndCapacity;

            if(!int.TryParse(i_userResponses[0], out o_LicenseType))
            {
                exception = new FormatException("Wrong Format. Enter License Type Again: ");
                exception.Source = "1";
            }
            else if (ValueOutOfRangeException.IsValueOutOfRange(o_LicenseType, 1, 4))
            {
                exception = new ValueOutOfRangeException(1, 4, "Out Of Range Choice (License Type). Enter Choice Again: ", exception);
                exception.Source = "1";
            }

            if(!int.TryParse(i_userResponses[1], out o_EngineCapacity))
            {
                exception = new FormatException("Wrong Format. Enter Engine Capacity Again: ", exception);
                exception.Source = "1";
            }
            else if(ValueOutOfRangeException.IsValueOutOfRange(o_EngineCapacity, 1, 2147)) // 2147 Because Harley Davidson has the biggest engine.
            {
                exception = new ValueOutOfRangeException(1, 2147, "Out Of Range Choice (Engine Capacity). Enter Choice Again: ", exception);
                exception.Source = "1";
            }

            return exception;
        }
    }
}