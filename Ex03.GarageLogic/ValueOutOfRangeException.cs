using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        
            private float m_MinValue;
            private float m_MaxValue;



            /* BEGIN - Getters & Setters */

            public float MinValue
            {
                get
                {
                    return m_MinValue;
                }
                set
                {
                    m_MinValue = value;
                }
            }

            public float MaxValue
            {
                get
                {
                    return m_MaxValue;
                }
                set
                {
                    m_MaxValue = value;
                }
            }
            /* END - Getters & Setters */


            public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_MessageOfException,
                                            Exception i_Exception = null) : base(i_MessageOfException, i_Exception)
            {
                m_MinValue = i_MaxValue;
                m_MaxValue = i_MaxValue;
            }

            public static bool IsValueOutOfRange(float i_Value, float i_MinValue, float i_MaxValue)
            {
                return i_Value > i_MaxValue || i_Value < i_MinValue;
            }
        }
}
