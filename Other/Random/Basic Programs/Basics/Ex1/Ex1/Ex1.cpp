// Write a program to solve the following expression:
// z = ax + b

#include <iostream>

int main()
{
    double z, a, x, b;
    
    std::cout << "Enter values to solve z = a*x + b\n";
    
    std::cout << "a=";
    std::cin >> a;

    std::cout << "x=";
    std::cin >> x;

    std::cout << "b=";
    std::cin >> b;

    z = (a * x) + b;
    std::cout << "z = a*x + b" << std::endl;
    std::cout << "z = " << a << "*" << x << " + " << b << std::endl;
    std::cout << "z = " << z << std::endl;

    return 0;
}
