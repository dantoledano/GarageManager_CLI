using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class VehicleAllocator
    {
        public enum eVehicleType
        {
            FueledMotorCycle = 1,
            ElectricMotorcycle = 2,
            FueledCar = 3,
            ElectricCar = 4,
            Truck = 5
        }
   
        public static Vehicle AllocateVehicle(eVehicleType i_TypeOfVehicle, string i_LicenseNumber)
        {
            Vehicle newVehicle;
            Engine newEngine;

            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FueledMotorCycle:
                    newEngine = new Engine.FuelBasedEngine((float)5.5, Engine.FuelBasedEngine.eFuelOctan.Octan98);
                    newVehicle = new Motorcycle(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newEngine = new Engine.ElectricBasedEngine((float)2.5);
                    newVehicle = new Motorcycle(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.FueledCar:
                    newEngine = new Engine.FuelBasedEngine((float)45, Engine.FuelBasedEngine.eFuelOctan.Octan95);
                    newVehicle = new Car(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.ElectricCar:
                    newEngine = new Engine.ElectricBasedEngine((float)3.5);
                    newVehicle = new Car(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.Truck:
                    newEngine = new Engine.FuelBasedEngine((float)120, Engine.FuelBasedEngine.eFuelOctan.Soler);
                    newVehicle = new Truck(newEngine, i_LicenseNumber);
                    break;
                default:
                    newEngine = null;
                    newVehicle = null;
                    break;
            }

            return newVehicle;
        }
       
        public static List<string> GetAndSetVehicleQueries(VehicleAllocator.eVehicleType i_TypeOfVehicle, Vehicle i_Vehicle)
        {
            List<string> queriesList = null;

            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FueledCar:
                case eVehicleType.ElectricCar:
                    queriesList = (i_Vehicle as Car).SetVehicleQueriesList();
                    break;

                case eVehicleType.FueledMotorCycle:
                case eVehicleType.ElectricMotorcycle:
                    queriesList = (i_Vehicle as Motorcycle).SetVehicleQueriesList();
                    break;

                case eVehicleType.Truck:
                    queriesList = (i_Vehicle as Truck).SetVehicleQueriesList();
                    break;
            }

            return queriesList;
        }

        public static void SetResponsesForVehicle(VehicleAllocator.eVehicleType i_VehicleType, Vehicle i_Vehicle, List<string> i_UserResponses)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FueledCar:
                    (i_Vehicle as Car).SetResponsesForVehicleQueries(i_UserResponses);
                    break;
                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FueledMotorCycle:
                    (i_Vehicle as Motorcycle).SetResponsesForVehicleQueries(i_UserResponses);
                    break;
                case eVehicleType.Truck:
                    (i_Vehicle as Truck).SetResponsesForVehicleQueries(i_UserResponses);
                    break;
            }
        }
    }
}
