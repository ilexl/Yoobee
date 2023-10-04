// Section 2 -- Task 3 -- Alexander Legner -- 270168960
#include <iostream>
#include <string>

using std::cout;
using std::string;

/// <summary>
/// this class is repsonsible for complex number calculations and output
/// </summary>
class ComplexNumber {
private:
    int realValue;
    int imaginaryValue;
    int identifier;
public:
    ComplexNumber(int r, int i, int id) : realValue(r), imaginaryValue(i), identifier(id) {}; // constuctor with chaining

    // (a + bi) + (c + di) = (a + c) + (b + d)i
    ComplexNumber operator+(const ComplexNumber& other) {
        int realTemp, imaginaryTemp, idTemp;
        realTemp = this->realValue + other.realValue;
        imaginaryTemp = this->imaginaryValue + other.imaginaryValue;
        idTemp = this->identifier + other.identifier;
        return ComplexNumber(realTemp, imaginaryTemp, idTemp);
    }
    // (a + bi) - (c + di) = (a - c) + (b - d)i
    ComplexNumber operator-(const ComplexNumber& other) {
        int realTemp, imaginaryTemp, idTemp;
        realTemp = this->realValue - other.realValue;
        imaginaryTemp = this->imaginaryValue - other.imaginaryValue;
        idTemp = this->identifier + other.identifier;
        return ComplexNumber(realTemp, imaginaryTemp, idTemp);
    }
    // (a + bi) * (c + di) = (ac - bd) + (ad + bc)i
    ComplexNumber operator*(const ComplexNumber& other) {
        int realTemp, imaginaryTemp, idTemp;
        realTemp = (this->realValue * other.realValue) - (this->imaginaryValue * other.imaginaryValue);
        imaginaryTemp = (this->realValue * other.imaginaryValue) + (this->imaginaryValue * other.realValue);
        idTemp = this->identifier + other.identifier;
        return ComplexNumber(realTemp, imaginaryTemp, idTemp);
    }

    /// <summary>
    /// basic output of the complex number
    /// </summary>
    void output() {
        cout << "c" << identifier << " : " << realValue;
        (imaginaryValue < 0) ? cout << " - " : cout << " + "; // output + or - based on positive or negative value
        cout << abs(imaginaryValue) << "i\n"; // always output postive number as + or - is sorted above
    }
};

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
/// entry point of the program
/// </summary>
/// <returns>return code</returns>
int main()
{
    // defined 1st complex number
    ComplexNumber first = ComplexNumber(3, 2, 1);
    cout << "1st complex number is "; first.output(); cout << "\n"; // inform user of 1st complex number

    // create 2nd complex number based off user input
    int secondReal = getInt("enter 2nd complex number values : \nenter real value : ");
    int secondImaginary = getInt("enter imaginary value : ");
    ComplexNumber second = ComplexNumber(secondReal, secondImaginary, 2);

    // large prompt for user selection
    cout << "\nchoose operation from menu : \n";
    cout << "1. Addition\n";
    cout << "2. Subtraction\n";
    cout << "3. Multiplication\n";
    cout << "4. Exit\n";

    int selection = 0;
    while (selection != 4) {
        selection = getInt("\nenter your option : "); // input
        ComplexNumber third = ComplexNumber(0, 0, 0); // temporary third number

        // operation based on selection
        if (selection == 1) {
            third = first + second;
        }
        else if (selection == 2) {
            third = first - second;
        }
        else if (selection == 3) {
            third = first * second;
        } 
        else if (selection == 4) {
            break;
        }
        else {
            cout << "\ninvalid input...\n\n";
        }
        
        // output operation values
        // ** note it will never output when selection == 4 or exit
        first.output();
        second.output();
        third.output();
    }
    
    return 0; // exit the program without errors
}