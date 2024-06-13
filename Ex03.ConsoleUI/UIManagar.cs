
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
            userChoice = getAndValidateInputFromUser(1, 2); 
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


        private int getAndValidateInputFromUser(int i_MinValue, int i_MaxValue)
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
                    //getVehicleInformationFromUser
                    break;
                case 2:
                    break;

            }
        }



    }
}
