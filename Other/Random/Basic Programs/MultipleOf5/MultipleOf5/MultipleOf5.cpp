#include <iostream>
#include <string>

int main()
{
    int start, end, multiple = 5;
    bool passed;

    std::cout << "This program prints out the multiple of 5 set with beginning and end inputs!!\n";
    do {
        passed = false;
        try {
            std::string temp = "";

            std::cout << "Enter start value : ";
            std::getline(std::cin, temp);
            start = stoi(temp);
            temp = "";

            std::cout << "Enter end value : ";
            std::getline(std::cin, temp);
            end = stoi(temp);
            temp = "";
            
            
            if (start >= end) {
                throw std::invalid_argument("Value/s are wrong");
            }
            passed = true;
        }
        catch (...) {
            std::cout << "Invalid input/s... \n";
        }
    } while (!passed);

    for (int i = start; i <= end; i++) {
        if (i == end) {
            std::cout << end;
        }
        else if (i % 5 == 0) {
            std::cout << i << ", ";
        }
    }
   
    return 0;
}
