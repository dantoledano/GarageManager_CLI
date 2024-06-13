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
            private eFuelOctan e_FuelOctan;
            private float m_MaxAmountOfFuel;

            public FuelBasedEngine(float i_MaxAmountOfFuel, eFuelOctan i_FuelOctan)
            {
                m_MaxAmountOfFuel = i_MaxAmountOfFuel;
                e_FuelOctan = i_FuelOctan;
            }

            /* BEGIN - Getters & Setters */
            public float FuelLeft
            {
                get { return m_FuelLeft; }
                set { m_FuelLeft = value; }
            }

            public float MaxAmountOfFuel
            {
                get { return m_MaxAmountOfFuel; }
                set { m_MaxAmountOfFuel = value; }
            }

            public eFuelOctan FuelOctan
            {
                get { return e_FuelOctan; }
                set { e_FuelOctan = value; }
            }
            /* END - Getters & Setters */

            public void Refuel(float i_AmountToFuel)
            {
                try
                {
                    m_FuelLeft += i_AmountToFuel;

                    if (i_AmountToFuel < 0)
                    {
                        throw new OutOfRangeException(0, MaxAmountOfFuel, "Fuel amount cannot be negative");
                    }

                    if (m_FuelLeft > MaxAmountOfFuel)
                    {
                        m_FuelLeft -= i_AmountToFuel;
                        if (m_FuelLeft == MaxAmountOfFuel)
                        {
                            throw new OutOfRangeException(0, MaxAmountOfFuel, "Tank Is Full Already !");
                        }
                        else
                        {
                            throw new OutOfRangeException(0, MaxAmountOfFuel, "Maximum fuel exceeded !");
                        }
                    }
                }

                catch (OutOfRangeException ex)
                {
                    string errorMessage = string.Format("Error: {0}", ex.Message);
                    Console.WriteLine(errorMessage);
                    throw;
                }
            }

            // check if the getters and setter don't mess things
            public override string ToString()
            {
                StringBuilder information = new StringBuilder();
                information.AppendFormat(@"Electric Engine:
    Capacity Of Battery: {0}
    Battery Time Left: {1}",
                    MaxAmountOfFuel.ToString(),
                    FuelLeft.ToString());
                information.Append(Environment.NewLine);

                return information.ToString();
            }
        }

        /* END -  FuelBasedEngine Class */

        /* START - ElectricBasedEngine Class */
        public class ElectricBasedEngine : Engine
        {
            private float m_BatteryTimeLeft;
            private float m_BatteryCapacity;

            public ElectricBasedEngine(float i_BatteryCapacity) : base() // is base is relevant ?
            {
                m_BatteryCapacity = i_BatteryCapacity;
            }

            /* BEGIN - Getters & Setters */

            public float BatteryCapacity
            {
                get { return m_BatteryCapacity; }
                set { m_BatteryCapacity = value; }
            }

            public float BatteryTimeLeft
            {
                get { return m_BatteryTimeLeft; }
                set { m_BatteryTimeLeft = value; }
            }


            public void ChargeBattery(float i_AmountToCharge)
            {
                try
                {
                    m_BatteryTimeLeft += i_AmountToCharge;

                    if (i_AmountToCharge < 0)
                    {
                        throw new OutOfRangeException(0, BatteryCapacity, "Charge amount cannot be negative");
                    }

                    if (m_BatteryTimeLeft > m_BatteryCapacity)
                    {
                        m_BatteryTimeLeft -= i_AmountToCharge;
                        if (m_BatteryTimeLeft == m_BatteryCapacity)
                        {
                            throw new OutOfRangeException(0, BatteryCapacity, "Battery Is Full Already !");
                        }
                        else
                        {
                            throw new OutOfRangeException(0, BatteryCapacity, "Maximum charge exceeded !");
                        }
                    }
                }

                catch (OutOfRangeException ex)
                {
                    string errorMessage = string.Format("Error: {0}", ex.Message);
                    Console.WriteLine(errorMessage);
                    throw;
                }
            }

            public override string ToString()
            {
                StringBuilder information = new StringBuilder();
                information.AppendFormat(@"Electric Engine:
    Capacity Of Battery: {0}
    Battery Time Left: {1}",
                    m_BatteryTimeLeft.ToString(),
                    m_BatteryTimeLeft.ToString());
                information.Append(Environment.NewLine);

                return information.ToString();
            }
        }
        /* END - ElectricBasedEngine Class */

    }
}
