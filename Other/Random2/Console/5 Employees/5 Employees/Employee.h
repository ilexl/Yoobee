#pragma once
#include <iostream>

using std::string;

class Employee
{
	int id;
	string name;
	int age;
	string GetString();
	int GetInt();
public:
	void getData();
	void displayData();
	Employee(int id);
	Employee();
};

