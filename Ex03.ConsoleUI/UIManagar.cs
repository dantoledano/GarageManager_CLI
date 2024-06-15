
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    public class UIManagar
    {
        public void InitilizeUIManager()
        {
            startGarage();
        }
        private void startGarage()
        {
            bool isGarageOpen = false;
            int userMenuChoice;

            /*
            while (!isGarageOpen)
            {
                showMenu();
               // userMenuChoice = getUserInput();
               // execute the option the user chose.

            }
            */
            showMenu();

        }
        private void showMenu()
        {
            Console.Write(@"Pick An Option From The Menu:

1. Put your vehicle in the garage.
2. Display all the license plates of vehicles that are in the garage.
3. Update the status of a vehicle in the garage.
4. Inflate tires of a chosen vehicle to the maximum capacity.
5. Fuel a vehicle.
6. Charge a vehicle.
7. Show all the information of a vehicle chosen using license plate number
8. Exit garage.

Enter your choice here: ");
        }

        private void showTypeOfVehicleMenu()
        {
            Console.Write(@"Please choose the vehicle type:

1. Electric Car.
2. Fuel Car.
3. Electric Motorcycle.
4. Fuel Motorcycle
5. Truck
Enter Your Choice: ");
        }

        private int showMenuOfOptionToFilterCarsInGarage()
        {
            int userChoice = 0 ;
            Console.WriteLine("Displaying license plates using the following filter: \n");
            Console.Write(@"Pick A Filter:

1. Filter by current state in the garage.
2. Don't use filtering.
Enter Your Choice: ");
            userChoice = getAndValidateInputFromUserInRange(1, 2); 
           return userChoice;

        }

        private void displayFuelMenu()
        {
            Console.Write(@"Choose A Fuel:
1. Octan95.
2. Octan96.
3. Octan98.
4. Soler.
Enter Your Choice: ");
        }

        // See later if the type int is suitable.
        private int getAndValidateInputFromUserInRange(int i_MinValue, int i_MaxValue)
        {
            string userInputChoice = Console.ReadLine();
            int userInputNumerical = 0;

            while (!int.TryParse(userInputChoice, out userInputNumerical) ||
                   OutOfRangeException.IsOutOfRangeValue(userInputNumerical, i_MaxValue, i_MinValue))
            {
                string messageOutOfRange =
                    string.Format("Your Choice is out of range ! Enter input again in the range {0} - {1}:",
                        i_MinValue, i_MaxValue);
                Console.WriteLine(messageOutOfRange);
                userInputChoice = Console.ReadLine();
            }
            return userInputNumerical;
        }

        private void executeUserChoice(int i_UserChoice, ref bool io_IsGarageOpen, Garage i_Garage)
        {
            io_IsGarageOpen = false;
            Console.Clear();

            switch (i_UserChoice)
            {
                case 1:
                    getVehicleStatusAndOwnerDetails(i_Garage);
                    break;
                case 2:
                    showLicensePlatesInGarage((int)showMenuOfOptionToFilterCarsInGarage(), i_Garage);
                    break;
                case 3:
                    updateStatusOfVehicle(i_Garage);
                    break;
                case 4:
                    inflateVehicleTires(i_Garage);
                    break;
                case 5:
                    Console.WriteLine("Procedding To Fueling Station...\n");
                    powerUpVehicle(i_Garage, Engine.eEngineType.Fuel);
                    break;
                case 6:
                    Console.WriteLine("Procedding To Charing Station...\n");
                    powerUpVehicle(i_Garage, Engine.eEngineType.Battery);
                    break;
                case 7:
                    displayVehicleInformation(i_Garage);
                    break;
                case 8:
                    Console.Clear();
                    exitingScreen(out io_IsGarageOpen);
                    break;
                
            }
        }

        
        private void displayVehicleInformation(Garage i_Garage)
        {
            Console.WriteLine("Printing vehicle information from the garage by license number:" + Environment.NewLine);
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = this.getVehicleLicenseNumberFromUser();
                    Garage.VehicleStatusAndOwnerDetails userVehicle = i_Garage.GetVehicleByLicenceNumber(licenseNumber);
                    Console.WriteLine(userVehicle.ToString());
                    displayBackToMenuOption();
                    userChoice = 2;
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = this.showBadInputMessageTryAgainAndGetMenu();
                }
            }
        }

        private void getVehicleStatusAndOwnerDetails(Garage i_Garage)
        {
            Console.WriteLine("Vehicle Is Being Added To The Garage: \n");
            Garage.VehicleStatusAndOwnerDetails VehicleStatusAndOwnerDetails = null;
            string licenseNumber = getVehicleLicenseNumberFromUser();

            if (i_Garage.VehiclesOfGarage.TryGetValue(licenseNumber, out VehicleStatusAndOwnerDetails))
            {
                Console.WriteLine("The Entered License Number Already Exist. Status Changed To - In Repair.");
                VehicleStatusAndOwnerDetails.VehicleStatus = Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus.UnderRepair;
            }
            else
            {
                showTypeOfVehicleMenu();
                VehicleAllocator.eVehicleType vehicleType = (VehicleAllocator.eVehicleType)getAndValidateInputFromUserInRange(1, 5);
                Vehicle newVehicle = VehicleAllocator.AllocateVehicle(vehicleType, licenseNumber);
                VehicleStatusAndOwnerDetails = setInformationForVehicle(newVehicle, vehicleType);
                i_Garage.VehiclesOfGarage.Add(licenseNumber, VehicleStatusAndOwnerDetails);
                Console.WriteLine("Vehicle Added Successfully.");
            }
            
            displayBackToMenuOption();
        }

        private void askForFuelOctanAndFuelAmountToFill(
            out int o_FuelChoice,
            out float o_FuelAmountToAdd,
            Engine.eEngineType i_EngineType)
        {
            o_FuelChoice = 0;
            if(i_EngineType == Engine.eEngineType.Fuel)
            {
                Console.WriteLine("Please Enter Which Octan You'd Like:");
                displayFuelMenu();
                o_FuelChoice = getAndValidateInputFromUserInRange(1, 4);
            }
            Console.WriteLine("Enter how much you'd like to add to you vehicle: ");
            o_FuelAmountToAdd = getAndValidateInputFromUserInRange(0, 120);
        }

        private string getAndValidateStringInputOfDigitsAndLetters()
        {
            string userInput = Console.ReadLine();
            bool isInvalidString = true;

            while(isInvalidString)
            {
                isInvalidString = false;
                foreach (char currentChar in userInput)
                {
                    if(!char.IsDigit(currentChar) && !char.IsLetter(currentChar))
                    {
                        Console.WriteLine("Your input must be letters and numbers only. Try Again: ");
                        userInput = Console.ReadLine();
                        isInvalidString = true;
                        break;
                    }
                }
            }

            return userInput;
        }


        private string getAndValidateStringOfChars(string i_Message)
        {
            bool isStringValid = false;
            Console.Write(i_Message);
            string userString = Console.ReadLine();

            while (!isStringValid)
            {
                isStringValid = true;

                foreach (char currentChar in userString)
                {
                    if (!(currentChar == ' ') && !char.IsLetter(currentChar))
                    {
                        isStringValid = false;
                        Console.Write("Enter Letter & Spaces Only. Enter again: ");
                        userString = Console.ReadLine();
                        break;
                    }
                }
            }

            return userString;
        }

        private void displayBackToMenuOption()
        {
            Console.WriteLine("Press Any Key To Get Back To Menu");
            Console.ReadKey();
        }

        private int getAndValidatePositiveNumberInput(string i_Message)
        {
            Console.Write(i_Message);
            string userInput = Console.ReadLine();
            int numberUserInput;

            while (!int.TryParse(userInput, out numberUserInput) || numberUserInput < 1)
            {
                Console.WriteLine("Enter Positive Number. Try Again: ");
                userInput = Console.ReadLine();
            }

            return numberUserInput;
        }

        private string getVehicleLicenseNumberFromUser()
        {
            Console.WriteLine("Enter The License Number Of The Vehicle: ");
            string licenseNumber = getAndValidateStringInputOfDigitsAndLetters();

            return licenseNumber;
        }

        private Garage.VehicleStatusAndOwnerDetails setInformationForVehicle(
            Vehicle i_Vehicle,
            VehicleAllocator.eVehicleType i_VehicleType)
        {
            string manufactorName;
            float currentAirPressure, currentEnergy;

            Console.Write("Enter The Model Name: ");
            i_Vehicle.ModelName = getAndValidateStringInputOfDigitsAndLetters();
            Console.Write("Enter Wheels Manufactor Name: ");
            manufactorName = getAndValidateStringInputOfDigitsAndLetters();
            Console.Write("Enter Wheels Remaining Air Pressure: ");
            currentAirPressure = getAndValidateInputFromUserInRange(0, (int)i_Vehicle.Wheels.First().MaxAirPressure);
            Console.Write("Enter Remaining Power Left In Vehicle: ");

            if(i_Vehicle.Engine is Engine.FuelBasedEngine)
            {
                currentEnergy = getAndValidateInputFromUserInRange(
                    0,
                    (int)(i_Vehicle.Engine as Engine.FuelBasedEngine).MaxAmountOfFuel);
            }
            else
            {
                currentEnergy = getAndValidateInputFromUserInRange(
                    0,
                    (int)(i_Vehicle.Engine as Engine.ElectricBasedEngine).BatteryCapacity);
            }
            i_Vehicle.SetPowerLevelLeft(currentEnergy);
            i_Vehicle.SetPowerPercentageLeft();
            getVehicleOwnerData(out string o_OwnerName, out string o_OwnerPhoneNumber);
            i_Vehicle.SetVehicleWheels(manufactorName, currentAirPressure);
            correctAndSetVehicleInformation(ref i_Vehicle, i_VehicleType);
            return new Garage.VehicleStatusAndOwnerDetails(o_OwnerName, o_OwnerPhoneNumber, i_Vehicle);

        }
        private void getVehicleOwnerData(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            o_OwnerName = this.getAndValidateStringOfChars("Enter Your name: ");
            o_OwnerPhoneNumber = this.getAndValidatePositiveNumberInput("Enter your phone number: ").ToString();
        }

        private void correctAndSetVehicleInformation(ref Vehicle io_Vehicle, VehicleAllocator.eVehicleType i_TypeOfVehicle)
        {
            List<string> queries = VehicleAllocator.GetAndSetVehicleQueries(i_TypeOfVehicle, io_Vehicle);
            List<string> userResponses = getResponsesAboutVehicle(queries);
            bool isWrongAnswer = true;

            while (isWrongAnswer)
            {
                try
                {
                    VehicleAllocator.SetResponsesForVehicle(i_TypeOfVehicle, io_Vehicle, userResponses);
                    isWrongAnswer = false;
                }
                catch (Exception exception)
                {
                    if (exception is FormatException || exception is OutOfRangeException || exception is ArgumentException)
                    {
                        while (exception != null)
                        {
                            Console.Write(exception.Message);
                            userResponses[int.Parse(exception.Source)] = Console.ReadLine();
                            exception = exception.InnerException;
                        }
                    }
                    else
                    {
                        throw exception;
                    }
                }
            }
        }


        private void inflateVehicleTires(Garage i_Garage)
        {
            Console.WriteLine("Inflating Air Pressure To Maximum" + Environment.NewLine);
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = getVehicleLicenseNumberFromUser();
                    i_Garage.FillWheelsToMaxAirPressureByLicenseNumber(licenseNumber);
                    userChoice = 2;
                    Console.WriteLine("Tires Filled Successfully.");
                    this.displayBackToMenuOption();
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = showBadInputMessageTryAgainAndGetMenu();
                }
                catch (OutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = showBadInputMessageTryAgainAndGetMenu();
                }
            }
        }


        private void showLicensePlatesInGarage(int i_UserFilterChoice, Garage i_Garage)
        {
            Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus status = Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus.Default;

            if (i_UserFilterChoice == 1)
            {
                status = getStatusOfVehicle();
            }

            string licensePlatesByStatus = i_Garage.GetListOfLicenseNumberByStatus(status);

            if (licensePlatesByStatus.Length != 0)
            {
                Console.WriteLine(licensePlatesByStatus);
            }
            else
            {
                Console.WriteLine("Vehicles With the Requested Status Do Not Exist Right Now.");
            }

            displayBackToMenuOption();
        }

        
        private List<string> getResponsesAboutVehicle(List<string> i_Questions)
        {
            List<string> answers = new List<string>(i_Questions.Capacity);

            foreach (string question in i_Questions)
            {
                Console.Write(question);
                answers.Add(Console.ReadLine());
            }

            return answers;
        }

        private Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus getStatusOfVehicle()
        {
            Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus status;
            Console.Write(@"Choose Vehicle Status: 
1. In repair
2. Repaired
3. Paid
Choice: ");
            int chosenVehicleStatus = (int)getAndValidateInputFromUserInRange(1, 3);
            status = (Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus)chosenVehicleStatus;

            return status;
        }

        private void updateStatusOfVehicle(Garage i_Garage)
        {
            Console.WriteLine("Updating Vehicle Status..." + Environment.NewLine);
            Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus status = this.getStatusOfVehicle();
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = this.getVehicleLicenseNumberFromUser();
                    i_Garage.SetVehicleGarageStatus(licenseNumber, status);
                    userChoice = 2;
                    Console.WriteLine("Vehicle Status Changed Successfully!");
                    this.displayBackToMenuOption();
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = showBadInputMessageTryAgainAndGetMenu();
                }
            }
        }

        private int showBadInputMessageTryAgainAndGetMenu()
        {
            Console.WriteLine(@"Bad Input. Choose From The Following Options: 

1. Enter Input 
2. Back To Menu
Enter Your Choice: ");
            int userChoice = (int)getAndValidateInputFromUserInRange(1, 2);

            return userChoice;
        }

        private void powerUpVehicle(Garage i_Garage, Engine.eEngineType i_EngineType)
        {
            int userMenuChoice = 1;

            while (userMenuChoice == 1)
            {
                try
                {
                    string licenseNumber = getVehicleLicenseNumberFromUser();
                    this.askForFuelOctanAndFuelAmountToFill(out int userFuelTypeChoice, out float quantityToFuelOrCharge, i_EngineType);
                    i_Garage.FuelOrChargeVehicle(i_EngineType, licenseNumber, quantityToFuelOrCharge, (Engine.FuelBasedEngine.eFuelOctan)userFuelTypeChoice);
                    userMenuChoice = 2;
                    Console.WriteLine("Engine of vehicle succesfully filled up");
                    displayBackToMenuOption();
                    break;
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (OutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);

                    if (exception.MinValue != exception.MaxValue)
                    {
                        Console.WriteLine("Value ranges are {0} to {1}", exception.MinValue, exception.MaxValue);
                    }
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                userMenuChoice = this.showBadInputMessageTryAgainAndGetMenu();
            }
        }


        private void exitingScreen(out bool o_IsGargeOpen)
        {
            Console.WriteLine("Closing Garage");
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(1000);
                Console.Write('.');
            }
            o_IsGargeOpen = false;
            Console.WriteLine("");
        }

    }
}
