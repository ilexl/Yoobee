// Write a program which evalutes the following expression:
// Z = ax + (b-c)(c-d)/y

/*
Author: Alex Legner
Contact: 027168960@yoobeestudent.ac.nz
OS: Windows 10
Compiler: VS 2019
*/

// Linkage & Preprocessor

#include <iostream> // For console IO


// Function declararion

double GetInput(std::string promt);
double CalculateABCDXYZ(double a, double x, double b, double c, double d, double y);


// Entry Point to program
int main()
{
    // Variables declaration
    double a = 0, b = 0, c = 0, d = 0, x = 0, y = 0, z = 0;

    std::cout << "Solve for Z = a*x + ((b-c)*(c-d) / y)\n"; // Title

    // Get all user input
    a = GetInput("a = ");
    x = GetInput("x = ");
    b = GetInput("b = ");
    c = GetInput("c = ");
    d = GetInput("d = ");
    y = GetInput("y = ");

    
    z = CalculateABCDXYZ(a, x, b, c, d, y); // Calculate complex equation : Z = ax + (b-c)(c-d)/y

    std::cout << "z = " << z << std::endl; // Output result

    return 0; // Exit program
}


// Reusable IO with a defined promt
// Returns double (user input console)
double GetInput(std::string promt) {
    double get = 0; // Initialise var get

    // IO to get input
    std::cout << promt; // Defined promt
    std::cin >> get;
    std::cout << "\n";

    return get; // Returns input (double)
}


// Calculates complex equation
// Z = ax + (b-c)(c-d)/y
double CalculateABCDXYZ(double a, double x, double b, double c, double d, double y) {
    double z = 0; // Initialise var z
    z = a * x + ((b - c) * (c - d) / y); // Calculate
    return z; // Return result
}