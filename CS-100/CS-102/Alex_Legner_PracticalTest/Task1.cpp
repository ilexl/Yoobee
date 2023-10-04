// Write a program to evaluate the expression of integers z = ax+by+c 

// Linkage
#include <iostream>

// Entry point of the program
int main()
{
    // variable declaration
    int a, b, c, x, y, z = 0; 
    

    // User Input
    
    // value a
    std::cout << "Enter a value: ";
    std::cin >> a;

    // value b
    std::cout << "Enter b value: ";
    std::cin >> b;

    // value c
    std::cout << "Enter c value: ";
    std::cin >> c;

    // value x
    std::cout << "Enter x value: ";
    std::cin >> x;

    // value y
    std::cout << "Enter y value: ";
    std::cin >> y;


    // Display Output
    std::cout << "Given expression: z = a*x+b*y+c\n";
    
    // z calculation for expression z = a*x+b*y+c
    z = (a * x) + (b * y) + c;

    // Z Output
    std::cout << "Z value is " << z;

    
    return 0; // (return code 0) program completed without errors 
}
