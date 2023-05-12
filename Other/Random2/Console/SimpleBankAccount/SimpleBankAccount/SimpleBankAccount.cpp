// SimpleBankAccount.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <string>
#include "bank.h"

using std::cout;
using std::cin;
using std::getline;
using std::ws;
using std::string;
using std::stoi;

int main()
{
	Bank bank = Bank();
	bank.Start();

	int option = 0;
	while (option != 3)
	{
		cout << "1. Withdraw\n";
		cout << "2. Deposit\n";
		cout << "3. Exit\n";
		cout << "Selection : ";

		string input = "";
		getline(cin >> ws, input);

		try {
			option = stoi(input);
			if (option < 1 || option > 3) {
				int fe = stoi("break");
			} // break if not valid selection

			if (option == 1) {
				bank.Withdraw();
			}
			if (option == 2) {
				bank.Deposit();
			}
		}
		catch (...) {
			cout << "\nPlease enter a vaild selection...\n\n";
		}
	}
	return 0;
}

