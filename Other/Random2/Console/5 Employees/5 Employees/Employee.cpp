#include "Employee.h"
#include <iostream>
#include <string>

using std::string;
using std::cout;
using std::cin;
using std::getline;
using std::ws;
using std::stoi;

string Employee::GetString()
{
	string input = "";
	getline(cin >> ws, input);
	return input;
}

int Employee::GetInt()
{
	while (true) {
		string RawInput = GetString();
		int final = 0;
		try {
			final = stoi(RawInput);
			return final;
		}
		catch (...) {
			cout << "\nNot a valid number...\n\n";
		}
	}
}

void Employee::getData()
{
	cout << "Employee " << id << " details:\n";
	cout << "Name : ";
	name = GetString();
	cout << "Age : ";
	age = GetInt();
	cout << std::endl;
}

void Employee::displayData()
{
	cout << "Employee " << id << " details:\n";
	cout << "Name : " << name << "\n";
	cout << "Age : " << age << "\n";
	cout << std::endl;
}

Employee::Employee(int id)
{
	this->id = id;
	name = "";
	age = 0;
}

Employee::Employee()
{
	id = 0;
	name = "";
	age = 0;
}