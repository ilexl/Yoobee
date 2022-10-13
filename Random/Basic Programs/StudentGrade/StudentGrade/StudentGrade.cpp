#include <iostream>
#include <cstring>
#include <string>

int main()
{
    std::cout << "-----Grader-----\n";
    
    int a1, a2, a3, rollNumber = -1, total;
    std::string name = " ", result;

    // Get name
    while (true) {
        try {
            std::cout << "Enter your name : ";
            std::getline(std::cin, name);
            if (name == " ") {
                throw ("Name cannot be empty...\n");
            }
            if (!name.empty()) {
                break;
            }
            else {
                throw ("Name cannot be empty...\n");
            }
        }
        catch (std::string error) {
            std::cout << error << "\n";
        }
        catch (...) {
            std::cout << "Name cannot be empty...\n";
        }
    }
    
    // Get rollNumber
    while (true) {
        try {
            std::cout << "Enter roll number : ";
            std::string temp;
            std::getline(std::cin, temp);
            rollNumber = stoi(temp);
            if (rollNumber != -1 && rollNumber != NULL) {
                break;
            }
            else {
                throw ("Roll number cannot be empty...\n");
            }
        }
        catch (std::string error) {
            std::cout << error << "\n";
        }
        catch (...) {
            std::cout << "Input ERROR...\n";
        }
    }

    // Get a1
    while (true) {
        try {
            std::cout << "Enter A1 mark (0-20): ";
            std::string temp;
            std::getline(std::cin, temp);
            a1 = stoi(temp);
            if (a1 >= 0 && a1 <= 20) {
                break;
            }
            else {
                throw ("Mark must be a value between 0-20...\n");
            }
            if (a1 != NULL) {
                break;
            }
            else {
                throw ("Mark cannot be empty...\n");
            }
        }
        catch (std::string error) {
            std::cout << error << "\n";
        }
        catch (...) {
            std::cout << "Input ERROR...\n";
        }
    }

    // Get a2
    while (true) {
        try {
            std::cout << "Enter A2 mark (0-40): ";
            std::string temp;
            std::getline(std::cin, temp);
            a2 = stoi(temp);
            if (a2 >= 0 && a2 <= 40) {
                break;
            }
            else {
                throw ("Mark must be a value between 0-40...\n");
            }
            if (a2 != NULL) {
                break;
            }
            else {
                throw ("Mark cannot be empty...\n");
            }
        }
        catch (std::string error) {
            std::cout << error << "\n";
        }
        catch (...) {
            std::cout << "Input ERROR...\n";
        }
    }
    
    // Get a3
    while (true) {
        try {
            std::cout << "Enter A3 mark (0-40): ";
            std::string temp;
            std::getline(std::cin, temp);
            a3 = stoi(temp);
            if (a3 >= 0 && a3 <= 40) {
                break;
            }
            else {
                throw ("Mark must be a value between 0-40...\n");
            }
            if (a1 != NULL) {
                break;
            }
            else {
                throw ("Mark cannot be empty...\n");
            }
        }
        catch (std::string error) {
            std::cout << error << "\n";
        }
        catch (...) {
            std::cout << "Input ERROR...\n";
        }
    }

    std::cout << "\n-----Mark Sheet-----\n";
 
    std::cout << "Name : " << name << "\n";
    std::cout << "Roll # : " << rollNumber << "\n";
    std::cout << "A1 Mark : " << a1 << "/20" << "\n";
    std::cout << "A2 Mark : " << a2 << "/40" << "\n";
    std::cout << "A3 Mark : " << a3 << "/40" << "\n";

    total = a1 + a2 + a3;

    if (total >= 50) {
        result = "Pass";
    }
    else {
        result = "Fail";
    }

    std::cout << "Total : " << total << "/100" << "\n";
    std::cout << "Result : " << result << "\n";

    return 0;
}