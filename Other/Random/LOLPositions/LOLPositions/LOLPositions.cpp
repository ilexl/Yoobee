#include <iostream>
#include <string>

using std::cin;
using std::cout;
using std::string;

bool GetPosition(bool* posTaken, string* posName, int* posplayer, int PosCount, int Player);

int main()
{
	const int positionsCount = 5;

	bool PositionsTaken[positionsCount] = { false, false , false , false , false };
	string PositionsNames[positionsCount] = { "Top", "Mid", "Jungle", "Adc", "Support" };
	int PositionsPlayer[positionsCount] = { -1, -1, -1, -1, -1 };


	for (int user = 0; user < positionsCount; user++) {
		cout << "Player " << user + 1 << "\n\n";
		bool invalid = true;
		do {
			invalid = GetPosition(&PositionsTaken[0], &PositionsNames[0], &PositionsPlayer[0], positionsCount, user);
			if (invalid) {
				cout << "\nInvalid Selection...\n\n";
			}
			else {
				cout << "\n";
			}
		} while (invalid);
	}

	cout << "Positions\n\n";
	for (int position = 0; position < positionsCount; position++) {
		cout << "Position " << PositionsNames[position] << " - Player " << PositionsPlayer[position] + 1 << "\n";
	}

}

bool GetPosition(bool *posTaken, string *posName, int *posplayer, int PosCount, int Player) {
	cout << "Current Positions\n\n";
	for (int i = 0; i < PosCount; i++) {
		cout << i << ". " << "Position " << posName[i] << " - ";
		posTaken[i] ? cout << "TAKEN" : cout << "AVALIBLE";
		cout << "\n";
	}
	int input = -1;
	cout << "\nWhich position would you like to select : ";
	cin >> input;

	if (input < 0 || input >= PosCount) {
		return true;
	}
	else {
		if (posTaken[input]) {
			return true;
		}
		else {
			posTaken[input] = true;
			posplayer[input] = Player;
			return false;
		}
	}
}
