// Write a program to find the area of a circle
// a = PI * r^2
#include <iostream>

int main()
{
    double a = 0, r = 0;
    const double PI = 3.1415;

    std::cout << "Find area of the circle!" << std::endl;
    std::cout << "Radius = ";
    std::cin >> r;

    a = PI * (r * r);
    std::cout << "Area = " << a << std::endl;

    return 0;
}

