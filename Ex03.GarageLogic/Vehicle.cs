using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        throw new OutOfRangeException(0, MaxAirPressure, "You Exceeded Tire Air Pressure!");
                    }
                }
                catch(OutOfRangeException exception)
                {
                    string errorMessage = string.Format("Error: {0}", exception.Message);
                    Console.WriteLine(errorMessage);
                    throw;
                }
            }

            public override string ToString()
            {
                StringBuilder information = new StringBuilder();
                information.AppendFormat(@"
    Maximum Air Presure: {0}
    Remaining Air Presure: {1}
    Manufactor Name: {2}",
                    r_MaxAirPressure.ToString(),
                    m_RemainingAir.ToString(),
                    m_ManufactureName.ToString());
                information.Append(Environment.NewLine);

                return information.ToString();
            }

            /* END */


        }

    }
}
