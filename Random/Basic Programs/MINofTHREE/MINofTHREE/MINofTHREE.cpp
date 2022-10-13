#include <iostream>
#include <cmath>

int main()
{
    double a = 0, b = 0, c = 0;

    std::cout << "This program determines the smallest of 3 inputs!\n";

    std::cout << "Value A : ";
    std::cin >> a;

    std::cout << "Value B : ";
    std::cin >> b;

    std::cout << "Value C : ";
    std::cin >> c;
    
    double smallest = a;

    if (b < smallest)
        smallest = b;
    if (c < smallest)
        smallest = c;

    std::cout << "Smallest value is : " << smallest;
    
    return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
