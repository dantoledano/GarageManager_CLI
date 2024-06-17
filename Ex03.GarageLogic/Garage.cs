using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Garage;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleStatusAndOwnerDetails> r_VehiclesOfGarage;

        public Dictionary<string, VehicleStatusAndOwnerDetails> VehiclesOfGarage
        {
            get
            {
                return r_VehiclesOfGarage;
            }
        }

        public Garage()
        {
            r_VehiclesOfGarage = new Dictionary<string, VehicleStatusAndOwnerDetails>();

        }

        public void SetVehicleGarageStatus(string i_LicenseNumber, VehicleStatusAndOwnerDetails.eVehicleGarageStatus i_NewGarageStatus)
        {
            try
            {
                VehicleStatusAndOwnerDetails vehicleDetails = GetVehicleByLicenceNumber(i_LicenseNumber);
                vehicleDetails.VehicleStatus = i_NewGarageStatus;
            }
            catch(ArgumentException exception)
            {
                throw exception;
            }
        }

        public void FuelOrChargeVehicle(
            Engine.eEngineType i_EngineType,
            string i_LicenseNumber,
            float i_QuantityToFill,
            Engine.FuelBasedEngine.eFuelOctan i_FuelType, Garage.VehicleStatusAndOwnerDetails i_VehicleToFillEngine)
        {
            try
            {
                //VehicleStatusAndOwnerDetails vehicleToFillEngine = GetVehicleByLicenceNumber(i_LicenseNumber);

                if(i_VehicleToFillEngine != null)
                {
                    if(i_EngineType == Engine.eEngineType.Fuel && i_VehicleToFillEngine.OwnerVehicle.Engine is Engine.FuelBasedEngine)
                    {
                        (i_VehicleToFillEngine.OwnerVehicle.Engine as Engine.FuelBasedEngine).Refuel(i_QuantityToFill, i_FuelType); 
                    }
                    else if(i_EngineType == Engine.eEngineType.Battery
                            && i_VehicleToFillEngine.OwnerVehicle.Engine is Engine.ElectricBasedEngine)
                    {
                        (i_VehicleToFillEngine.OwnerVehicle.Engine as Engine.ElectricBasedEngine).ChargeBattery(i_QuantityToFill);
                    }
                    else
                    {
                        if(i_EngineType == Engine.eEngineType.Fuel)
                        {
                            throw new ArgumentException("You Have Requested To Refuel The Vehicle, But The Garage Has Identified It As An Electric Car.");
                        }
                        else
                        {
                            throw new ArgumentException("You Have Requested To Charge The Vehicle, But The Garage Has Identified It As A Fuel Powered Car");
                        }
                    }
                }
            }
            catch(ValueOutOfRangeException exception)
            {
                throw exception;
            }
            catch(ArgumentException exception)
            {
                throw exception;
            }
        }

        public VehicleStatusAndOwnerDetails GetVehicleByLicenceNumber(string i_LicenseNumber)
        {
            bool isVehicleFound = r_VehiclesOfGarage.TryGetValue(
                i_LicenseNumber,
                out VehicleStatusAndOwnerDetails o_VehicleDetails);

            if(isVehicleFound)
            {
                return o_VehicleDetails;
            }
            else
            {
                ArgumentException exception =
                    new ArgumentException("Sorry, There Is No a Vehicle With This License Number In Our Garage");
                throw exception;
            }
        }

        public string GetListOfLicenseNumberByStatus(VehicleStatusAndOwnerDetails.eVehicleGarageStatus i_DesiredStatusToFilter, bool i_IsFilter)
        {
            int countOfDesiredVehicles = 1;
            StringBuilder listOfDesiredVehiclesLicenseNumbers = new StringBuilder();

            foreach(KeyValuePair<string, VehicleStatusAndOwnerDetails> currentPair in r_VehiclesOfGarage)
            {
                if(i_IsFilter == true)
                {
                    if (currentPair.Value.VehicleStatus == i_DesiredStatusToFilter)
                    {
                        listOfDesiredVehiclesLicenseNumbers.Append(countOfDesiredVehicles.ToString() + ") ");
                        listOfDesiredVehiclesLicenseNumbers.AppendLine(currentPair.Key);
                        countOfDesiredVehicles++;
                    }
                }
                else
                {
                    listOfDesiredVehiclesLicenseNumbers.Append(countOfDesiredVehicles.ToString() + ") ");
                    listOfDesiredVehiclesLicenseNumbers.AppendLine(currentPair.Key);
                    countOfDesiredVehicles++;
                }

            }

            return listOfDesiredVehiclesLicenseNumbers.ToString();
        }

        public void FillWheelsToMaxAirPressureByLicenseNumber(string i_LicenseNumber)
        {
            try
            {
                VehicleStatusAndOwnerDetails vehicleDetails = GetVehicleByLicenceNumber(i_LicenseNumber); 
                vehicleDetails.OwnerVehicle.FillTiresToMax();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public class VehicleStatusAndOwnerDetails
        {
            private string m_OwnerName;
            private string m_OwnerPhoneNumber;
            private Vehicle m_Vehicle;
            private eVehicleGarageStatus m_VehicleStatus;

            public enum eVehicleGarageStatus
            {
                Default,
                UnderRepair,
                Repaired,
                Paid,
                
            }

            public VehicleStatusAndOwnerDetails(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhoneNumber = i_OwnerPhoneNumber;
                m_Vehicle = i_Vehicle;
                m_VehicleStatus = eVehicleGarageStatus.UnderRepair;
            }

            public string OwnerName
            {
                get
                {
                    return m_OwnerName;
                }
                set
                {
                    m_OwnerName = value;
                }
            }

            public string OwnerPhoneNumber
            {
                get
                {
                    return m_OwnerPhoneNumber;
                }
                set
                {
                    m_OwnerPhoneNumber = value;
                }
            }

            public Vehicle OwnerVehicle
            {
                get
                {
                    return m_Vehicle;
                }
                set
                {
                    m_Vehicle = value;
                }
            }

            public eVehicleGarageStatus VehicleStatus
            {
                get
                {
                    return this.m_VehicleStatus;
                }
                set
                {
                    m_VehicleStatus = value;
                }
            }

            //            public override string ToString()
            //            {
            //                StringBuilder vehicleInGarageDetails = new StringBuilder();
            //                vehicleInGarageDetails.AppendFormat(@"Owner information:
            //    Owner Name: {0}
            //    Owner Phone Number: {1}
            //",
            //m_OwnerName,
            //m_OwnerPhoneNumber);
            //                vehicleInGarageDetails.AppendFormat(this.m_Vehicle.ToString());
            //                vehicleInGarageDetails.AppendFormat("The Status Of The Vehicle: {0}" + Environment.NewLine, m_VehicleStatus.ToString());


            //                return vehicleInGarageDetails.ToString();
            //            }

            public override string ToString()
            {
                StringBuilder vehicleInGarageDetails = new StringBuilder();

                vehicleInGarageDetails.AppendFormat("Owner Information:\n");
                vehicleInGarageDetails.AppendFormat("===================\n");
                vehicleInGarageDetails.AppendFormat("Owner Name: {0}\n", m_OwnerName);
                vehicleInGarageDetails.AppendFormat("Owner Phone Number: {0}\n\n", m_OwnerPhoneNumber);

                vehicleInGarageDetails.AppendFormat("----------------\n");
                vehicleInGarageDetails.AppendFormat("{0}\n\n", m_Vehicle.ToString());

                vehicleInGarageDetails.AppendFormat("Vehicle Status:\n");
                vehicleInGarageDetails.AppendFormat("---------------\n");
                vehicleInGarageDetails.AppendFormat("The Status Of The Vehicle: {0}\n", m_VehicleStatus);

                return vehicleInGarageDetails.ToString();
            }



        }

    }
}
