// Section 1 -- Task 2 -- Alexander Legner -- 270168960

#include <iostream>
#include <vector>
#include <string>

using std::vector;
using std::cout;
using std::string;
using std::to_string;

int getInt(std::string prompt);

class Cost {
private:
    int dollar, cent;
    static int count;

    /// <summary>
    /// shows cost in correct format $345.65
    /// </summary>
    static void showCost(int d, int c) {
        string output = "$";
        output += to_string(d) + ".";
        output += to_string(c);
        if (to_string(c).length() == 1) {
            output += "0";
        }
        cout << "total cost : " << output << "\n";
    }

public:
    /// <summary>
    /// constructor creates a blank cost
    /// </summary>
    Cost() {
        dollar = 0;
        cent = 0;
        count++;
    }

    /// <summary>
    /// gets the cost from the user input
    /// </summary>
    void readCost() {
        dollar = getInt("\nEnter dollars : ");
        cent = getInt("Enter cents : ");
    }

    /// <summary>
    /// displays the total number of items ordered
    /// </summary>
    static void showCount() {
        cout << "\ntotal number of items ordered : " << count << "\n";
    }

    /// <summary>
    /// sums all the items cost passes as an argument and returns total cost
    /// </summary>
    /// <param name="v">all the items cost</param>
    static void computeTotalCost(vector<Cost>& v) {
        int totalDollars = 0;
        int totalCents = 0;
        for (Cost c : v) {
            totalDollars += c.dollar;
            totalCents += c.cent;
        }
        // check if divisable then continue
        while (totalCents - 100 > 0) {
            totalCents -= 100;
            totalDollars++;
        }

        showCost(totalDollars, totalCents);
    }
};

int Cost::count = 0; // initialise the count to zero

/// <summary>
/// gets a valid int from a user without crashing
/// </summary>
/// <param name="prompt">prompt to show user before getting input</param>
/// <returns>valid int from user input</returns>
int getInt(std::string prompt)
{
    bool inputValid = false; // Initialize loop condition
    int validInt; // Assign memory for the valid value
    while (!inputValid) { // Loop while input is invalid
        std::cout << prompt; // Prompt
        std::string rawInput;
        std::getline(std::cin >> std::ws, rawInput);

        try {
            // Check for decimals
            int len = rawInput.find_last_of('.'); // Gets the amount of decimal places - i.e 1.3 is 1dp 
            if (len > 0) { // Stops any decimals
                throw std::invalid_argument("Input cannot have decimals...");
            }

            validInt = std::stoi(rawInput); // Convert input to int
            inputValid = true; // Stop the loop as the input was valid 
        }
        catch (...) {
            std::cout << "Invalid input...\n\n"; // Inform users of any errors and loop
        }
    }
    return validInt;
}

/// <summary>
/// entry point to the program
/// </summary>
/// <returns>return code</returns>
int main()
{
    int items = 0; // used in the loop to get the correct amount of items
    items = getInt("how many items are you purchasing today : ");
    
    vector<Cost> allCosts; // stores all costs
    for (int i = 0; i < items; i++) {
        Cost c = Cost(); // create empty cost
        c.readCost(); // get user input for cost
        allCosts.push_back(c); // add cost to all costs
    }
    Cost::showCount(); // show the count 
    Cost::computeTotalCost(allCosts); // show total amount after calculating
}


