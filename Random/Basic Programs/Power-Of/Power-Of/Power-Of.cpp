#include <iostream>
#include <cmath>

int main()
{
    std::cout << "a^x Calculator!\n";

    double a = 0, x = 0, output = 0;

    std::cout << "Enter base a = ";
    std::cin >> a;

    std::cout << "Enter power x = ";
    std::cin >> x;

    output = pow(a, x);

    std::cout << "a^x = " << a << "^" << x << " = " << output;

    return 0;
}
