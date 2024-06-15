﻿using System;
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
            A,
            A1,
            AA,
            B1
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
            StringBuilder motorcycleAttributes = new StringBuilder();

            motorcycleAttributes.Append(base.ToString());
            motorcycleAttributes.AppendFormat(@"Motorcycle Attributes: 
License Type: {0}
Engine Capacity: {1}", m_LicenseType.ToString(), m_EngineCapacity.ToString());
            motorcycleAttributes.Append(Environment.NewLine);

            return motorcycleAttributes.ToString();
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


        public override void SetResponsesForVehicleQueries(List<string> queriesResponses)
        {
            Exception exception = checkForThrownExceptionsInResponses(queriesResponses, out int o_LicenseType, out int o_EngineCapacity);
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
            else if (OutOfRangeException.IsOutOfRangeValue(o_LicenseType, 1, 4))
            {
                exception = new OutOfRangeException(1, 4, "Out Of Range Choice (License Type). Enter Choice Again: ", exception);
                exception.Source = "1";
            }

            if(!int.TryParse(i_userResponses[1], out o_EngineCapacity))
            {
                exception = new FormatException("Wrong Format. Enter Engine Capacity Again: ", exception);
                exception.Source = "1";
            }

            else if(OutOfRangeException.IsOutOfRangeValue(o_EngineCapacity, 1, 1500))
            {
                exception = new OutOfRangeException(1, 1500, "Out Of Range Choice (Engine Capacity). Enter Choice Again: ", exception);
                exception.Source = "1";
            }
            return exception;
        }
    }
}