#include <iostream>

int main()
{
    int passengersToday = 0, seatsLeft = 0, busCapacity = 50;
    std::cout << "Hello Welcome to the Passenger Calculator!\n";
    std::cout << "Enter the amount of passengers collected today : ";
    std::cin >> passengersToday;

    while (true) {
        if (passengersToday - 50 >= 0) {
            passengersToday -= 50;
        }
        else {
            break;
        }
    }

    seatsLeft = busCapacity - passengersToday;

    std::cout << "There are " << seatsLeft << " seats left on the final bus!";

    return 0;
}
