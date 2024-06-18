using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Vehicle;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private readonly string r_LicenseNumber;
        private readonly List<Wheel> r_Wheels;
        private float m_PercentagePowerLeft;
        private Engine m_Engine;

        public Vehicle(string i_LicenseNumber, int i_NumberOfWheels, float i_MaxAirPressureForWheel, Engine i_Engine)
        {
            m_PercentagePowerLeft = 0;
            m_Engine = i_Engine;
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>();

            for(int i = 0; i < i_NumberOfWheels; ++i)
            {
                r_Wheels.Add(new Wheel(i_MaxAirPressureForWheel));
            }
        }

        /* BEGIN - Getters & Setters */
        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }

        public float PercentagePowerLeft
        {
            get
            {
                return m_PercentagePowerLeft;
            }
            set
            {
                m_PercentagePowerLeft = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }
        /* END - Getters & Setters */

        /* Abstract methods */
        public abstract List<string> SetVehicleQueriesList();
        public abstract void SetResponsesForVehicleQueries(List<string> io_QueriesResponses);

        public void SetPowerLevelLeft(float i_PowerLevel)
        {
            if(Engine is Engine.FuelBasedEngine)
            {
                (Engine as Engine.FuelBasedEngine).FuelLeft = i_PowerLevel;
            }
            else
            {
                (Engine as Engine.ElectricBasedEngine).BatteryTimeLeft = i_PowerLevel;
            }
        }

        public void SetPowerPercentageLeft()
        {
            if (Engine is Engine.FuelBasedEngine fuelEngine)
            {
                PercentagePowerLeft = (fuelEngine.FuelLeft / fuelEngine.MaxAmountOfFuel) * (float)100;
            }
            else if (Engine is Engine.ElectricBasedEngine electricEngine)
            {
                PercentagePowerLeft = (electricEngine.BatteryTimeLeft / electricEngine.BatteryCapacity) * (float)100;
            }
        }

        public void SetVehicleWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            foreach (Wheel currentWheel in Wheels)
            {
                currentWheel.ManufactorName = i_ManufacturerName;
                currentWheel.RemainingAir = i_CurrentAirPressure;
            }
        }

        public void FillTiresToMax()
        {
            float remainingAirToReachMax = Wheels[0].MaxAirPressure - Wheels[0].RemainingAir;

            foreach (Wheel wheel in r_Wheels)
            {
                try
                {
                    if (remainingAirToReachMax > 0)
                    {
                        wheel.FillAir(remainingAirToReachMax);
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, 33, "Maximun Air Pressure Has Reached Already!");
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    throw exception;
                }
            }
        }

        /* - Class Wheel -  */
        public class Wheel
        {
            private readonly float r_MaxAirPressure;
            private float m_RemainingAir;
            private string m_ManufactureName;

            public Wheel(float i_MaxAirPressure)
            {
                r_MaxAirPressure = i_MaxAirPressure;
            }
            /* BEGIN - Getters & Setters */

            public float MaxAirPressure
            {
                get
                {
                    return r_MaxAirPressure;
                }
            }

            public float RemainingAir
            {
                get
                {
                    return m_RemainingAir;
                }
                set
                {
                    m_RemainingAir = value;
                }
            }
            public string ManufactorName
            {
                get
                {
                    return m_ManufactureName;
                }
                set
                {
                    m_ManufactureName = value;
                }
            }

            public void FillAir(float i_AmountOfAirToFill)
            {
                try
                {
                    m_RemainingAir += i_AmountOfAirToFill;
                    if(m_RemainingAir > r_MaxAirPressure)
                    {
                        m_RemainingAir -= i_AmountOfAirToFill;
                        throw new ValueOutOfRangeException(0, MaxAirPressure, "You Exceeded Tire Air Pressure!");
                    }
                }
                catch(ValueOutOfRangeException exception)
                {
                    string errorMessage = string.Format("Error: {0}", exception.Message);
                    Console.WriteLine(errorMessage);
                    throw;
                }
            }

            public override string ToString()
            {
                StringBuilder information = new StringBuilder();

                information.AppendFormat("Wheel Information:{0}", Environment.NewLine);
                information.AppendFormat("=================={0}", Environment.NewLine);
                information.AppendFormat("Maximum Air Pressure: {0}{1}", r_MaxAirPressure, Environment.NewLine);
                information.AppendFormat("Remaining Air Pressure: {0}{1}", m_RemainingAir, Environment.NewLine);
                information.AppendFormat("Manufacturer Name: {0}", m_ManufactureName);

                return information.ToString();
            }
            /* END */
        }

        public override string ToString()
        {
            StringBuilder vehicleInformation = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            vehicleInformation.AppendFormat("Vehicle Information:{0}", Environment.NewLine);
            vehicleInformation.AppendFormat("====================={0}", Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            vehicleInformation.AppendFormat("License Number: {0}{1}", LicenseNumber, Environment.NewLine);
            vehicleInformation.AppendFormat("Model Name: {0}{1}", ModelName, Environment.NewLine);
            vehicleInformation.AppendFormat("{0}", Environment.NewLine);
            vehicleInformation.AppendFormat("{0}{1}", Wheels[0].ToString(), Environment.NewLine);
            vehicleInformation.AppendFormat("{0}", Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            vehicleInformation.AppendFormat("Engine Information:{0}", Environment.NewLine);
            vehicleInformation.AppendFormat("=================={0}", Environment.NewLine);
            vehicleInformation.AppendFormat("Power Percentage: {0:F1}%{1}",PercentagePowerLeft, Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            vehicleInformation.AppendFormat("{0}{1}", Engine.ToString(), Environment.NewLine);

            return vehicleInformation.ToString();
        }
    }
}
