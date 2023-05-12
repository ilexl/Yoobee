#include <iostream>
#include <string>
#include <vector>

using std::cin;
using std::cout;
using std::vector;
using std::string;

int main()
{
    cout << "Welcome to the password manager!\n";

    vector<string> usernames;
    vector<string> passwords;

    bool running = true;
    while (running) {
        cout << "\noptions:\n";
        cout << "\"show\" - shows all passwords\n";
        cout << "\"add\" - add password to the vault\n";
        cout << "\"exit\" - exits the program\n";

        string input;
        std::getline(cin >> std::ws, input);
         
        if (input == "e" || input == "exit" || input == "Exit" || input == "E") {
            running = false;
            continue;
        }

        if (input == "a" || input == "add" || input == "Add" || input == "A") {
            cout << "Enter username : ";
            string username;
            std::getline(cin >> std::ws, username);
            usernames.push_back(username);

            cout << "Enter password : ";
            string password;
            std::getline(cin >> std::ws, password);
            passwords.push_back(password);
        }

        if (input == "s" || input == "show" || input == "Show" || input == "S") {
            for (int i = 0; i < usernames.size(); i++) {
                cout << "Username : " << usernames[i] << "\nPassword : " << passwords[i] << "\n\n";
            }
        }
    }
}
