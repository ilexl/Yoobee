#include <iostream>
#include <string>
#include <cmath>

int main()
{
    long input, value = 1;
    std::string out = "", temp = "";
    
    std::cout << "Factorial Calculator!\n";
    
    while (true) {
        try {
            temp = "";
            std::cout << "Enter the factorial to calculate (1 -> 31) : ";
            std::getline(std::cin, temp);

            if (temp.find(".") != std::string::npos) {
                throw std::invalid_argument("invalid input...");
            }

            input = stol(temp);

            if (input <= 0 || input > 31) {
                throw std::invalid_argument("invalid input...");
            }
           
            break;
        }
        catch(...){
            std::cout << "Invalid input... \n";
        }
    }
    
    input = int(input);
    for (long i = 1; i <= input; i++) {
        if (i == input) {
            out.append(std::to_string(i));
        }
        else {
            out.append(std::to_string(i) + " * ");
        }
        
        value *= i;
    }

    std::cout << input << "! = " << out << " = " << value;

    return 0;
}