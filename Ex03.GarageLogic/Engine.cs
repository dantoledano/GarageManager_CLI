using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public enum eEngineType
        {
            Battery,
            Fuel
        }

        public class FuelBasedEngine : Engine
        {
            public enum eFuelOctan
            {
                Octan95 = 1,
                Octan96 = 2,
                Octan98 = 3,
                Soler = 4
            }

            private float m_FuelLeft;
            private eFuelOctan m_FuelOctan;
            private float m_MaxAmountOfFuel;

            public FuelBasedEngine(float i_MaxAmountOfFuel, eFuelOctan i_FuelOctan)
            {
                m_MaxAmountOfFuel = i_MaxAmountOfFuel;
                m_FuelOctan = i_FuelOctan;
            }

            /* BEGIN - Getters & Setters */
            public float FuelLeft
            {
                get
                {
                    return m_FuelLeft;
                }
                set
                {
                    m_FuelLeft = value;
                }
            }

            public float MaxAmountOfFuel
            {
                get
                {
                    return m_MaxAmountOfFuel;
                }
                set
                {
                    m_MaxAmountOfFuel = value;
                }
            }

            public eFuelOctan FuelOctan
            {
                get
                {
                    return m_FuelOctan;
                }
                set
                {
                    m_FuelOctan = value;
                }
            }
            /* END - Getters & Setters */

            public void Refuel(float i_AmountToFuel, eFuelOctan i_FuelType)
            {
                try
                {
                    if(i_FuelType != FuelOctan)
                    {
                        throw new ArgumentException("Type Of Fuel Is Not Matching! Try Again.");
                    }

                    m_FuelLeft += i_AmountToFuel;
                    if (i_AmountToFuel < 0)
                    {
                        throw new ValueOutOfRangeException(0, MaxAmountOfFuel, "Fuel Amount Cannot Be Negative");
                    }

                    if (m_FuelLeft > MaxAmountOfFuel)
                    {
                        m_FuelLeft -= i_AmountToFuel;
                        if (m_FuelLeft == MaxAmountOfFuel)
                        {
                            string errorMessageForTankIsFull = string.Format(
                                "Tank Is Full Already! Currently has {0}",
                                MaxAmountOfFuel);
                            throw new ValueOutOfRangeException(0, MaxAmountOfFuel, errorMessageForTankIsFull);
                        }
                        else
                        {
                            float currentAmountOfFuelForErrorMessage = FuelLeft;
                            string errorMessageForUserExceedingAmountOfFuel = string.Format(
                                "Maximum fuel exceeded ! Currently Your Vehicle Has {0} Of Fuel",
                                currentAmountOfFuelForErrorMessage);
                            throw new ValueOutOfRangeException(0, MaxAmountOfFuel, errorMessageForUserExceedingAmountOfFuel);
                        }
                    }

                    string currentAmountOfFuelAfterRefueling = string.Format("Current Amount Of Fuel: {0}", FuelLeft);
                    Console.WriteLine(currentAmountOfFuelAfterRefueling);
                }
                catch (ValueOutOfRangeException exception)
                {
                    string errorMessage = string.Format("Error: {0}", exception.Message);
                    Console.WriteLine(errorMessage);
                    throw;
                }
            }

            public override string ToString()
            {
                StringBuilder information = new StringBuilder();

                information.AppendFormat("Fuel Engine:{0}", Environment.NewLine);
                information.AppendFormat("------------{0}", Environment.NewLine);
                information.AppendFormat("Tank Capacity: {0}{1}", MaxAmountOfFuel, Environment.NewLine);
                information.AppendFormat("Remaining Fuel: {0}{1}", FuelLeft, Environment.NewLine);

                return information.ToString();
            }
        }
        /* END -  FuelBasedEngine Class */

        /* START - ElectricBasedEngine Class */
        public class ElectricBasedEngine : Engine
        {
            private float m_BatteryTimeLeft;
            private float m_BatteryCapacity;

            public ElectricBasedEngine(float i_BatteryCapacity) : base() 
            {
                m_BatteryCapacity = i_BatteryCapacity;
            }
            /* BEGIN - Getters & Setters */

            public float BatteryCapacity
            {
                get
                {
                    return m_BatteryCapacity;
                }
                set
                {
                    m_BatteryCapacity = value;
                }
            }

            public float BatteryTimeLeft
            {
                get
                {
                    return m_BatteryTimeLeft;
                }
                set
                {
                    m_BatteryTimeLeft = value;
                }
            }

            public void ChargeBattery(float i_AmountToCharge)
            {
                try
                {
                    m_BatteryTimeLeft += i_AmountToCharge;
                    if (i_AmountToCharge < 0)
                    {
                        throw new ValueOutOfRangeException(0, BatteryCapacity, "Charge Amount Cannot Be Negative");
                    }

                    if (m_BatteryTimeLeft > m_BatteryCapacity)
                    {
                        m_BatteryTimeLeft -= i_AmountToCharge;
                        if (m_BatteryTimeLeft == m_BatteryCapacity)
                        {
                            string errorMessageForBatteryIsFull = string.Format(
                                "Battery Is Already Full! Currently Has {0}",
                                BatteryCapacity);
                            throw new ValueOutOfRangeException(0, BatteryCapacity, errorMessageForBatteryIsFull);
                        }
                        else
                        {
                            float currentAmountOfBatteryForErrorMessage = BatteryTimeLeft;
                            string errorMessageForUserExceedingCharingMax = string.Format(
                                "Maximum Charge Exceeded ! Currently Your Vehicle Has {0} Of Battery",
                                currentAmountOfBatteryForErrorMessage);
                            throw new ValueOutOfRangeException(0, BatteryCapacity, errorMessageForUserExceedingCharingMax);
                        }
                    }

                    string currentAmountOfBatteryAfterCharging = string.Format("Current Amount Of Battery: {0}", BatteryTimeLeft);
                    Console.WriteLine(currentAmountOfBatteryAfterCharging);
                }
                catch (ValueOutOfRangeException exception)
                {
                    string errorMessage = string.Format("Error: {0}", exception.Message);
                    Console.WriteLine(errorMessage);
                    throw;
                }
            }

            public override string ToString()
            {
                StringBuilder information = new StringBuilder();

                information.AppendFormat("Electric Engine:{0}", Environment.NewLine);
                information.AppendFormat("----------------{0}", Environment.NewLine);
                information.AppendFormat("Capacity Of Battery: {0}{1}", m_BatteryCapacity, Environment.NewLine);
                information.AppendFormat("Battery Time Left: {0}{1}", m_BatteryTimeLeft, Environment.NewLine);

                return information.ToString();
            }
        }
        /* END - ElectricBasedEngine Class */
    }
}
