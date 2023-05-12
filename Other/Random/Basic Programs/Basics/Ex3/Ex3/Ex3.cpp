// Write a program to calculate simple interest
// SI = P(1 + r*t)
// P - principle, r - interest rate, t - time in years
#include <iostream>

int main()
{
    double simpleInterst = 0, principle = 0, interestRate = 0, time = 0;
    std::cout << "Simple Interest Calculator" << std::endl;

    std::cout << "Principle = ";
    std::cin >> principle;

    std::cout << "Interest Rate (0.00 - 1.00) = ";
    std::cin >> interestRate;
    
    std::cout << "Time = ";
    std::cin >> time;

    simpleInterst = principle * (1 + (interestRate * time));

    std::cout << "Simple Interest = " << simpleInterst;

    return 0;
}
