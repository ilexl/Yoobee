#include <iostream>
#include <cmath>

int main()
{
    double input = 0, output = 0;
    std::cout << "Enter a value to SQRT : ";
    std::cin >> input;

    output = sqrt(input);

    std::cout << "sqrt(" << input << ") = " << output;

    return 0;
}

