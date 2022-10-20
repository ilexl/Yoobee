/*
Task 6
Write a program to generate a “Personal Expenses Tracking System “to track the daily and weekly expenses. 
Create a structure to manage the data. The structure must include the date, transport cost, meal cost, entertainment, and others.
Daily expenses must be calculated and displayed by accessing the “view daily expenses “option.
Application should allow recording a minimum of three different days expenses.
Weekly expenses should be calculated and displayed
Manage constructor to initialize the members
Manage structure array to record multiple days expenses
Generate daily reports and weekly reports
Manage functions to get input and generate reports like daily and weekly expenses
Manage relevant menu items to access the features.
*/

#include <iostream>
#include <string>
#include <vector>

using std::cin;
using std::cout;
using std::string;
using std::vector;
using std::getline;
using std::ws;
using std::endl;

// Expense data type with repsective members
struct Expense {
	int day;
	int week;
	double costTransport;
	double costMeal;
	double costEntertainment;
	double costOther;

	Expense(int d, int w, double other, double entertainment, double meal, double transport) {
		this->day = d;
		this->week = w;
		this->costTransport = transport;
		this->costMeal = meal;
		this->costEntertainment = entertainment;
		this->costOther = other;
	}
};

// Function prototypes
double getDouble(string prompt, bool newLine);
int getInt(string prompt, bool newLine);
string getString(string prompt, bool newLine);
void Error(string msg);
void GenerateReport(vector<Expense> expenses, int week, int day);
void GenerateReport(vector<Expense> expenses, int week);

int main() {
	vector<Expense> expenses; // Create expense list

	// Console output
	cout << "----------------------\n";
	cout << "Personal Expenses Tracking System";

	bool exitProgram = false;
	do {
		// Display main menu options
		cout << "\n\nOptions\n"
			<< "1. Create daily expense\n"
			<< "2. Generate daily report\n"
			<< "3. Generate weekly report\n"
			<< "4. Exit\n";
		int selection = getInt("Selection : ", false); // Get int selection
		cout << endl; // console spacing
		// Check input is valid and do action if required
		switch (selection) {
		case 1: {
			// Create expense

			// Get expense date
			int week = -1; 
			do {
				week = getInt("Enter week number : ", false);
				if (week < 1) { Error("Week must be greater than 0..."); }
			} while (week < 1);
			int day = 0;
			do {
				day = getInt("Enter day (1-7 for monday-sunday respectively) : ", false);
				if (day < 1) { Error("Day must be greater than 0..."); }
				if (day > 7) { Error("Day must be less than 7..."); }
			} while (day < 1 || day > 7);

			// Check if date exist already
			bool duplicate = false;
			for (int i = 0; i < expenses.size(); i++) {
				Expense expense = expenses[i];
				if (expense.week == week) {
					if (expense.day == day) {
						Error("Warning : This will overwrite you current expense for this date...");
						duplicate = true;
						bool invalidConfirm = true;
						while (invalidConfirm) {
							string confirmation = getString("Continue y or n : ", false);
							if (confirmation[0] == 'y' || confirmation[0] == 'Y') {
								duplicate = false;
								invalidConfirm = false;
								expenses.erase(expenses.begin() + i);
							}
							else if (confirmation[0] == 'n' || confirmation[0] == 'N') {
								duplicate = true;
								invalidConfirm = false;
							}
							else {
								Error("Invalid input...");
								invalidConfirm = true;
							}
						}
					}
				}
			}

			// Cancel new expense if user doesn't want to overwrite
			if (duplicate) {
				cout << "\nReturning to main menu...\n\n";
				break;
			}

			// Get expense
			Expense expense = Expense(day, week, 
				getDouble("Enter other costs for this day         : ", false),
				getDouble("Enter entertainment costs for this day : ", false),
				getDouble("Enter meal costs for this day          : ", false),
				getDouble("Enter transport costs for this day     : ", false));

			// Add expense to all expenses
			expenses.push_back(expense);

			break;
		}
		case 2: {
			// Generete daily report

			// Get possible weeks
			vector<int> weeks;
			for (Expense expense : expenses) {
				bool alreadyExist = false;
				for (int week : weeks) {
					if (week == expense.week) {
						alreadyExist = true;
					}
				}
				if (!alreadyExist) {
					weeks.push_back(expense.week);
				}
			}

			if (weeks.size() == 0) {
				Error("No expenses stored...");
				break;
			}

			// Get input for possible weeks
			int week = 0;
			bool invalidWeek = true;
			do {
				cout << "Choose from the avalible weeks : \n";
				for (int _week : weeks) {
					cout << "Week " << _week << endl;
				}

				int weekSelection = getInt("Selection : ", false);
				for (int _week : weeks) {
					if (weekSelection == _week) {
						invalidWeek = false;
					}
				}

				if (invalidWeek) {
					Error("Not a valid week from selection...");
				}
				else {
					week = weekSelection;
				}

			} while (invalidWeek || week == -1);

			// Get possible days
			vector<int> days;
			for (Expense expense : expenses) {
				if (expense.week == week) {
					days.push_back(expense.day);
				}
			}

			cout << endl; // console spacing

			// Get input for possible days
			int day = -1;
			bool invalidDay = true;
			do {
				cout << "Choose from the avalible days : \n";
				for (int day : days) {
					cout << "Day " << day << endl;
				}

				int daySelection = getInt("Selection : ", false);
				for (int _day : days) {
					if (daySelection == _day) {
						invalidDay = false;
					}
				}

				if (invalidDay) {
					Error("Not a valid day from selection...");
				}
				else {
					day = daySelection;
				}

			} while (invalidDay);
			
			GenerateReport(expenses, week, day); // Generate and display report
			
			break;
		}
		case 3: {
			// Generate weekly report

			// Get possible weeks
			vector<int> weeks;
			for (Expense expense : expenses) {
				bool alreadyExist = false;
				for (int week : weeks) {
					if (week == expense.week) {
						alreadyExist = true;
					}
				}
				if (!alreadyExist) {
					weeks.push_back(expense.week);
				}
			}

			if (weeks.size() == 0) {
				Error("No expenses stored...");
				break;
			}

			// Get input for possible weeks
			int week = -1;
			bool invalidWeek = true;
			do {
				cout << "Choose from the avalible weeks : \n";
				for (int _week : weeks) {
					cout << "Week " << _week << endl;
				}

				int weekSelection = getInt("Selection : ", false);
				for (int _week : weeks) {
					if (weekSelection == _week) {
						invalidWeek = false;
					}
				}

				if (invalidWeek) {
					Error("Not a valid week from selection...");
				}
				else {
					week = weekSelection;
				}

			} while (invalidWeek);

			// week is valid here
			
			GenerateReport(expenses, week); // Generate and display weekly report
			break;
		}
		case 4: {
			// Exits the program 

			exitProgram = true; // stops loop
			cout << "Exiting...\n"; // console output
			break;
		}
		default: {
			Error("Not a valid selection...");
			break;
		}
		}
	} while (!exitProgram); // loops until exit selection

	return 0;
}

// Gets a double from the console
double getDouble(string prompt, bool newLine) {
	bool invalidInput = true;
	while (invalidInput) {
		try {
			string rawString = getString(prompt, newLine);
			double d = stod(rawString);
			invalidInput = false;
			return d;
		}
		catch (...) {
			Error("Number not entered...");
		}
	}	
}

// Gets an int from the console with decimals cut off
int getInt(string prompt, bool newLine) {
	bool invalidInput = true;
	while (invalidInput) {
		try {
			string rawString = getString(prompt, newLine);
			int i = stod(rawString);
			invalidInput = false;
			return i;
		}
		catch (...) {
			Error("Number not entered...");
		}
	}
}

// Gets a string from the console with ws included
string getString(string prompt, bool newLine) {
	cout << prompt;
	if (newLine) cout << '\n';

	string rawInput;
	getline(cin >> ws, rawInput);
	return rawInput;
}

// Displays error to user
void Error(string msg) {
   cout << '\n'
		<< "----------------------\n"
		<< msg << '\n'
		<< "----------------------\n"
		<< '\n';
}

// Generates a report
void GenerateReport(vector<Expense> expenses, int week, int day) {
	cout << "\n\nExpenses for week " << week << ", day " << day << " : \n";
	Expense target = Expense(day, week, 0, 0, 0, 0);
	for (Expense expense : expenses) {
		if (expense.week == week && expense.day == day) {
			target = expense;
		}
	}

	// output target for correct expense
	cout << "Transport costs     : $" << target.costTransport << endl;
	cout << "Meal costs          : $" << target.costMeal << endl;
	cout << "Entertainment costs : $" << target.costEntertainment << endl;
	cout << "Other costs         : $" << target.costOther << endl;
	cout << "-----------------------------\n";
	cout << "Total costs         : $" << (target.costEntertainment + target.costOther + target.costMeal + target.costTransport);
}

// Generates a report
void GenerateReport(vector<Expense> expenses, int week) {
	cout << "\n\nExpenses for week " << week << " : \n";
	Expense target = Expense(0, week, 0, 0, 0, 0);
	for (Expense expense : expenses) {
		if (expense.week == week) {
			target.costTransport += expense.costTransport;
			target.costMeal += expense.costMeal;
			target.costEntertainment += expense.costEntertainment;
			target.costOther += expense.costOther;
		}
	}

	// output target for total
	cout << "Total Transport costs     : $" << target.costTransport << endl;
	cout << "Total Meal costs          : $" << target.costMeal << endl;
	cout << "Total Entertainment costs : $" << target.costEntertainment << endl;
	cout << "Total Other costs         : $" << target.costOther << endl;
	cout << "-----------------------------\n";
	cout << "Total costs for week      : $" << (target.costEntertainment + target.costOther + target.costMeal + target.costTransport);
}
