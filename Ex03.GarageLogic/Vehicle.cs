using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public abstract void SetResponsesForVehicleQueries(List<string> queriesResponses);

        public void SetPowerLevelLeft(float i_PowerLevel)
        {
            if(this.Engine is Engine.FuelBasedEngine)
            {
                (this.Engine as Engine.FuelBasedEngine).FuelLeft = i_PowerLevel;
            }
            else
            {
                (this.Engine as Engine.ElectricBasedEngine).BatteryTimeLeft = i_PowerLevel;
            }
        }

        public void SetPowerPercentageLeft()
        {
            if(this.Engine is Engine.FuelBasedEngine)
            {
                this.PercentagePowerLeft = ((this.Engine as Engine.FuelBasedEngine).FuelLeft / (this.Engine as Engine.FuelBasedEngine).MaxAmountOfFuel) *100f;
            }
            else
            {

                this.PercentagePowerLeft = ((this.Engine as Engine.ElectricBasedEngine).BatteryTimeLeft / (this.Engine as Engine.ElectricBasedEngine).BatteryCapacity) * 100f;

            }
        }
        public void SetVehicleWheels(string i_ManufactorName, float i_CurrentAirPressure)
        {
            foreach (Wheel currentWheel in Wheels)
            {
                currentWheel.ManufactorName = i_ManufactorName;
                currentWheel.RemainingAir = i_CurrentAirPressure;
            }
        }

        public void FillTiresToMax()
        {
            float remaningAirToReachMax = Wheels[0].MaxAirPressure - Wheels[0].RemainingAir;

            foreach (Wheel wheel in this.r_Wheels)
            {
                try
                {
                    if (remaningAirToReachMax > 0)
                    {
                        wheel.FillAir(remaningAirToReachMax);
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

        /* ------------------------------------------------------------ */
        /* - Class Wheel -  */
        /* BEGINING */
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
                information.AppendFormat("Manufacturer Name: {0}{1}", m_ManufactureName, Environment.NewLine);

                return information.ToString();
            }


            /* END */


        }


        public override string ToString()
        {
            Console.WriteLine("TEST");
            StringBuilder vehicleInformation = new StringBuilder();
            //vehicleInformation.AppendLine("Vehicle Information:");
            //vehicleInformation.AppendLine("=====================");
            //vehicleInformation.AppendLine($"License Number: {r_LicenseNumber}");
            //vehicleInformation.AppendLine($"Model Name: {m_ModelName}");
            //vehicleInformation.AppendLine();
            //vehicleInformation.AppendLine("Wheels:");
            //vehicleInformation.AppendLine("-------");
            //vehicleInformation.AppendLine($"{Wheels[0]}");
            //vehicleInformation.AppendLine();
            //vehicleInformation.AppendLine("Engine Information:");
            //vehicleInformation.AppendLine("-------------------");
            //vehicleInformation.AppendLine(Engine.ToString());

            return vehicleInformation.ToString();
        }

    }
}
