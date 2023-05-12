#include <iostream>
#include <cmath>

int main()
{
    int value = 0;
    std::cout << "This program determines if a value is odd OR even!\n";

    std::cout << "Enter value : ";
    std::cin >> value;
    
    if(value % 2 == 0) {
        // Even value
        std::cout << "Value (" << value << ") is EVEN (:";
    }
    else {
        // Odd value
        std::cout << "Value (" << value << ") is ODD (:";
    }
    return 0;
}

