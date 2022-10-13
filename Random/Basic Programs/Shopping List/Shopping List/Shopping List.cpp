#include <string>
#include <iostream>
#include <vector>

int main()
{
    std::cout << "Hello welcome to your shopping list!\n";
    std::cout << "Enter item to add or 'end' to end the shopping list...\n";

    std::vector < std::string > shoppingList = {};

    bool MoreInput = true;
    std::string input = "";
    do {
        std::cout << "Input : ";
        std::getline(std::cin >> std::ws, input);
        if (input == "end") {
            MoreInput = false;
        }
        else {
            shoppingList.push_back(input);
            input = "";
        }

    } while (MoreInput);

    
    std::cout << "\nShopping List : \n";
    for (std::string item : shoppingList) {
        std::cout << item << "\n";
    }

    std::cout << "\nSearch an item in the list to find its index or type 'end'...\n";

    MoreInput = true;
    input = "";
    do {
        std::cout << "Input : ";
        std::getline(std::cin >> std::ws, input);
        if (input == "end") {
            MoreInput = false;
            continue;
        }
        else {
            bool found = false;
            int foundIndex = 0;
            for (int i = 0; i < shoppingList.size(); i++) {
                if (shoppingList[i] == input) {
                    found = true;
                    foundIndex = i;
                }
            }

            if (found) {
                std::cout << shoppingList[foundIndex] << " is found in index " << foundIndex << "\n";
            }
            else {
                std::cout << input << " is NOT found in the shopping list...\n";
            }

            input = "";
        }

    } while (MoreInput);

    std::cout << "Thank you for using your shopping list (:\n";

    return 0;
}

