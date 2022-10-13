#include <iostream>
#include <string>

using std::cout;
using std::cin;
using std::string;

struct Details {
    string username;
    int age;
    string favFruit;
};

string GetString(string consoleOut);
int GetInt(string consoleOut);
char GetChar(string consoleOut);
void ShowDetails(Details& details);

int main()
{

    Details userDetails;
    char correct = 'n';

    do { 
        cout << "Confirm your details : \n\n";

        userDetails.username = GetString("Username  : ");
        userDetails.age = GetInt("Age       : ");
        userDetails.favFruit = GetString("Fav Fruit : ");

        cout << "\n\n";
        ShowDetails(userDetails);

        correct = GetChar("\nAre these details correct?\n\n");
        if (correct == 'Y') {
            correct = 'y';
        }
        if (correct != 'y') {
            correct = 'n';
        }

    } while (correct == 'n');


    cout << "Your details are : \n\n";
    ShowDetails(userDetails);

    return 0;
}

string GetString(string consoleOut) {
    cout << consoleOut;
    string input;
    std::getline(cin >> std::ws, input);
    return input;
}
int GetInt(string consoleOut) {
    cout << consoleOut;
    int input;
    cin >> input;
    return input;
}
char GetChar(string consoleOut) {
    cout << consoleOut;
    char input;
    cin >> input;
    return input;
}
void ShowDetails(Details& details) {
    cout << "Username  : " << details.username << "\n";
    cout << "Age       : " << details.age << "\n";
    cout << "Fav Fruit : " << details.favFruit << "\n";
}

