#include <iostream>
#include <string>
#include "Bank.h"

using std::cout;
using std::cin;
using std::getline;
using std::ws;
using std::string;
using std::stof;

void Bank::Start()
{
	cout << "Enter your initial amount present in your acount : ";
	string _bal = "";
	getline(cin >> ws, _bal);
	try {
		bal = stof(_bal);
		if (bal < 0) {
			float fe = stof("break");
		} // error if val is invalid
	}
	catch(...){
		cout << "\nPlease enter a valid number above 0...\n\n;";
		Start();
	}
}

void Bank::Withdraw()
{
	cout << "Enter the amount you would like to withdraw : ";
	string _withdraw = "";
	getline(cin >> ws, _withdraw);
	try {
		int withdraw = stof(_withdraw);
		if (withdraw < 0 || withdraw > bal) {
			float fe = stof("break");
		} // error if val is invalid

		bal -= withdraw;
		cout << "$" << withdraw << " succefully withdrew!!\n" << "$" << bal << " balance remaining...\n";
	}
	catch (...) {
		cout << "\nPlease enter a valid value to withdraw...\n\n;";
		Withdraw();
	}
}

void Bank::Deposit()
{
	cout << "Enter the amount you would like to deposit : ";
	string _deposit = "";
	getline(cin >> ws, _deposit);
	try {
		int deposit = stof(_deposit);
		if (deposit < 0) {
			float fe = stof("break");
		} // error if val is invalid

		bal += deposit;
		cout << "$" << deposit << " succefully deposited!!\n" << "$" << bal << " balance remaining...\n";
	}
	catch (...) {
		cout << "\nPlease enter a valid value to deposit...\n\n";
		Withdraw();
	}
}
