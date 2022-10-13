// Write a program to evalutate the expression
// z = x^2 + y^2
#include <iostream>
#include <cmath>

int main()
{
    double z = 0, x = 0, y = 0;
    std::cout << "z = x^2 + y^2" << std::endl;
    
    std::cout << "x = ";
    std::cin >> x;

    std::cout << "y = ";
    std::cin >> y;

    z = (pow(x, 2) + pow(y, 2));

    std::cout << "z = " << z << std::endl;

    return 0;
}

