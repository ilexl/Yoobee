#include <iostream>
#include <ctime>
#include <string>
#include <conio.h>
#include <cmath>
#include <cstdlib>

using std::cout;
using std::cin;
using std::string;
using std::endl;

int GetRandomNumber(int max, bool maxExclusive = true);

struct Vector2
{
	int x;
	int y;
};

struct AdventureStats
{
	int health;
	int weapon;
};

int main()
{

#pragma region Init
	cout << "\nLoading Adventure Game...\n\n";

	// Variables
	const int MAPWIDTH = 9;
	const int MAPHEIGHT = 9;

	// Map data
	// 0 = nothing
	// S = starting pos (also nothing)
	// W## = Weapon + index#
	// E## = Enemy  + index#
	// I## = Item   + index#
	// B## = Boss   + index# 

	const string MD_NOTHING = "0";
	const string MD_STARTPOS = "S";
	const string MD_WEAPON = "W";
	const string MD_ENEMY = "E";
	const string MD_ITEM = "I";
	const string MD_BOSS = "B";

	string mapData[MAPHEIGHT][MAPWIDTH] = {
		{"W04","0","0","E04","B02","E04","0","E04","W04"},
		{"0","E03","E03","E03","0","0","E02","B02","0"},
		{"E03","B02","0","B01","I01","0","0","E04","0"},
		{"0","0","E03","E02","W01","E02","E03","0","0"},
		{"B03","E03","E03","E01","S","E01","0","E03","E04"},
		{"0","0","E03","E01","0","B01","W02","0","0"},
		{"E04","B02","0","E03","E02","0","0","E03","0"},
		{"E04","E04","0","E03","0","E03","E03","E03","0"},
		{"W04","E04","0","0","E04","B02","0","E04","W04"}
	};

	// Explored data
	// 0 = Not explored (FOG OF WAR)
	// 1 = Explored -> Get type from map data
	// 2 = Currently at that point

	string mapExplored[MAPHEIGHT][MAPWIDTH] = {
		{"0","0","0","0","0","0","0","0","0"},
		{"0","0","0","0","0","0","0","0","0"},
		{"0","0","0","0","0","0","0","0","0"},
		{"0","0","0","0","1","0","0","0","0"},
		{"0","0","0","1","1","1","0","0","0"},
		{"0","0","0","0","1","0","0","0","0"},
		{"0","0","0","0","0","0","0","0","0"},
		{"0","0","0","0","0","0","0","0","0"},
		{"0","0","0","0","0","0","0","0","0"}
	};

	const int weaponsDamage[] = { 1, 5, 10, 20, 50 };
	const string weaponsName[] = { "fists", "wooden sword", "iron sword", "bronze sword", "gladiator sword" };

	const int enemyHealth[] = { 2, 8, 16, 32, 64 };
	const int enemyDamage[] = { 1, 2, 4, 16, 32 };
	const string enemyName[] = { "Bandit", "Archer", "Assassin","Knight", "Warewolf" };

	const int bossHealth[] = { 25, 50, 1000 };
	const int bossDamage[] = { 20, 30, 100 };
	const string bossName[] = { "King", "Emperor", "DEATH" };

	const int itemHealth[] = { 5, 10, 20, 50, 100 };
	const string itemName[] = { "Bread", "Meat", "Vegetables", "Health Potion", "Life Elixer" };


	// ------------------------------------------------------------------------------
	// Character initialisation

	string name = "";
	cout << "Enter your name adventurer : ";
	std::getline(cin >> std::ws, name);

	Vector2 playerPos;

	// memory allocation values
	playerPos.x = -1;
	playerPos.y = -1;

	// Get start Pos from map
	for (int y = 0; y < MAPHEIGHT; y++) {
		for (int x = 0; x < MAPWIDTH; x++) {
			if (mapData[y][x] == MD_STARTPOS) {
				playerPos.x = x;
				playerPos.y = y;
				mapExplored[y][x] = "2"; // Set current pos
			}
		}
	}

	// Stats
	AdventureStats playerStats;
	playerStats.health = 100;
	playerStats.weapon = 0;

	// ------------------------------------------------------------------------
	// Game loop

	bool playing = true;
#pragma endregion
	while (playing) {
		if (playerStats.health <= 0) {
			playing = false;
			cout << "\n\nYOU DIED!!!\n";
			break;
		}


		// Display Character Stats
		cout << "\n\n\n\n\nAdventurer : " << name << endl;
		cout << "Health : " << playerStats.health << endl;
		cout << "Weapon : " << weaponsName[playerStats.weapon] << " -> " << weaponsDamage[playerStats.weapon] << endl;
		cout << endl;

		// Display Map
		cout << "Map : \n";
		for (int y = 0; y < MAPHEIGHT; y++) {
			for (int x = 0; x < MAPWIDTH; x++) {
				if (mapExplored[y][x] == "0") {
					cout << "?   ";
				}
				else if (mapExplored[y][x] == "1") {
					// Map data
					// 0 = nothing
					// S = starting pos (also nothing)
					// W# = Weapon + index#
					// E# = Enemy  + index#
					// I# = Item   + index#
					// B# = Boss   + index# 
					switch (mapData[y][x][0]) {
					case '0':
						cout << ' ' << "   ";
						break;
					case 'S':
					case 's':
						cout << "S   ";
						break;
					case 'W':
					case 'w':
						cout << "W   ";
						break;
					case 'E':
					case 'e':
						cout << "E   ";
						break;
					case 'I':
					case 'i':
						cout << "I   ";
						break;
					case 'B':
					case 'b':
						cout << "B  ";
						break;
					default:
						cout << "$   ";
						break;
					}
				}
				else if (mapExplored[y][x] == "2") {
					cout << "P   ";
				}
				else {
					cout << "$   ";
				}
			}
			cout << endl << endl;
		}
		cout << endl;

		// Give Options
		bool invalidInput = true;
		char input = '\0';
		while (invalidInput) {
			cout << "Options : \n";

			cout << "W - Move Up\n";
			cout << "A - Move Left\n";
			cout << "S - Move Down\n";
			cout << "D - Move Right\n";
			cout << "N - Do nothing\n";
			cout << "E - Exit\n";

			cout << "What would you like to do? : ";

			cin >> input;

			switch (input) {
			case 'W':
			case 'w':
			case 'A':
			case 'a':
			case 'S':
			case 's':
			case 'D':
			case 'd':
			case 'N':
			case 'n':
			case 'E':
			case 'e':
				invalidInput = false;
				break;
			default:
				cout << "\nThat is not a valid input...\n\n";
				break;
			}
		}
		// MAKE INPUT CAPITALS -------------------------------
		if (input >= 'a') {
			input = input - 32;
		}

		//cout << "\nYOU SELECTED " << input << "\n\n";

		char currentEvent = 'N';
		int currentEventIndex = -1;

		// Output new map and do event
		switch (input) {
		case 'W':

			if (playerPos.y == 0) {
				cout << "You cannot go any higher...\n";
			}
			else {
				mapExplored[playerPos.y][playerPos.x] = "1"; // Set current pos to explored pos

				playerPos.y -= 1; // Set new Y value
				mapExplored[playerPos.y][playerPos.x] = "2"; // Set new pos to curret pos

				// Check new pos in next switch (:
			}
			break;
		case 'A':
			if (playerPos.x == 0) {
				cout << "You cannot go any further left...\n";
			}
			else {
				mapExplored[playerPos.y][playerPos.x] = "1"; // Set current pos to explored pos

				playerPos.x -= 1; // Set new Y value
				mapExplored[playerPos.y][playerPos.x] = "2"; // Set new pos to curret pos

				// Check new pos in next switch (:
			}
			break;
		case 'S':
			if (playerPos.y == MAPHEIGHT - 1) {
				cout << "You cannot go any lower...\n";
			}
			else {
				mapExplored[playerPos.y][playerPos.x] = "1"; // Set current pos to explored pos

				playerPos.y += 1; // Set new Y value
				mapExplored[playerPos.y][playerPos.x] = "2"; // Set new pos to curret pos

				// Check new pos in next switch (:
			}
			break;
		case 'D':
			if (playerPos.x == MAPWIDTH - 1) {
				cout << "You cannot go any further right...\n";
			}
			else {
				mapExplored[playerPos.y][playerPos.x] = "1"; // Set current pos to explored pos

				playerPos.x += 1; // Set new Y value
				mapExplored[playerPos.y][playerPos.x] = "2"; // Set new pos to curret pos

				// Check new pos in next switch (:
			}
			break;
		case 'N':
			break;
		case 'E':
			playing = false;
			cout << "Exiting Game...\n";
			break;
		default:
			break;
		}

		cout << "New Map : \n";
		for (int y = 0; y < MAPHEIGHT; y++) {
			for (int x = 0; x < MAPWIDTH; x++) {
				if (mapExplored[y][x] == "0") {
					cout << "?   ";
				}
				else if (mapExplored[y][x] == "1") {
					// Map data
					// 0 = nothing
					// S = starting pos (also nothing)
					// W# = Weapon + index#
					// E# = Enemy  + index#
					// I# = Item   + index#
					// B# = Boss   + index# 
					switch (mapData[y][x][0]) {
					case '0':
						cout << ' ' << "   ";
						break;
					case 'S':
					case 's':
						cout << "S   ";
						break;
					case 'W':
					case 'w':
						cout << "W   ";
						break;
					case 'E':
					case 'e':
						cout << "E   ";
						break;
					case 'I':
					case 'i':
						cout << "I   ";
						break;
					case 'B':
					case 'b':
						cout << "B  ";
						break;
					default:
						cout << "$   ";
						break;
					}
				}
				else if (mapExplored[y][x] == "2") {
					cout << "P   ";
				}
				else {
					cout << "$   ";
				}
			}
			cout << endl << endl;
		}
		cout << endl;

		switch (input) {
		case 'W':
		case 'A':
		case 'S':
		case 'D':
		case 'N':
			cout << "You found ";
			if (mapData[playerPos.y][playerPos.x][0] == '0') {
				cout << "nothing...\n";
			}
			else if (mapData[playerPos.y][playerPos.x][0] == 'S') {
				cout << "nothing, however you are where you started originally?\n";
			}
			else if (mapData[playerPos.y][playerPos.x][0] == 'W') {
				// Weapon
				cout << "a weapon : ";
				string temp = "  ";
				temp[0] = mapData[playerPos.y][playerPos.x][1];
				temp[1] = mapData[playerPos.y][playerPos.x][2];
				int weapon = std::stoi(temp);
				cout << weaponsName[weapon] << " -> " << weaponsDamage[weapon] << " damage" << endl;
				currentEventIndex = weapon;
				currentEvent = 'W';
			}
			else if (mapData[playerPos.y][playerPos.x][0] == 'E') {
				// Enemy
				cout << "an enemy : ";
				string temp = "  ";
				temp[0] = mapData[playerPos.y][playerPos.x][1];
				temp[1] = mapData[playerPos.y][playerPos.x][2];
				int enemy = std::stoi(temp);
				cout << enemyName[enemy] << " -> " << enemyHealth[enemy] << " health, " << enemyDamage[enemy] << " damage" << endl;
				currentEventIndex = enemy;
				currentEvent = 'E';
			}
			else if (mapData[playerPos.y][playerPos.x][0] == 'I') {
				// Item
				cout << "an item : ";
				string temp = "  ";
				temp[0] = mapData[playerPos.y][playerPos.x][1];
				temp[1] = mapData[playerPos.y][playerPos.x][2];
				int item = std::stoi(temp);
				cout << itemName[item] << " -> " << itemHealth[item] << " health points" << endl;
				currentEventIndex = item;
				currentEvent = 'I';
			}
			else if (mapData[playerPos.y][playerPos.x][0] == 'B') {
				// Boss
				cout << "a boss : ";
				string temp = "  ";
				temp[0] = mapData[playerPos.y][playerPos.x][1];
				temp[1] = mapData[playerPos.y][playerPos.x][2];
				int boss = std::stoi(temp);
				cout << bossName[boss] << " -> " << bossHealth[boss] << " health, " << bossDamage[boss] << endl;
				currentEventIndex = boss;
				currentEvent = 'B';
			}
			else {
				cout << "idk..????\n";
			}

			break;
		default:
			break;
		}

		switch (currentEvent) {

		case 'N':
		{
			cout << "Nothing to do here...\n";
			break;
		}
		case 'W':
		{
			char weaponPickUp = ' ';
			bool invalidWeaponPickUp = true;
			while (invalidWeaponPickUp) {
				cout << "Would you like to pick up this weapon? (Y or N)\nRemember, picking up a weapon destroys your current one\nPick Up : ";
				cin >> weaponPickUp;
				if (weaponPickUp == 'y' || weaponPickUp == 'n') {
					weaponPickUp -= 32;
				}

				if (weaponPickUp == 'Y' || weaponPickUp == 'N') {
					invalidWeaponPickUp = false;
				}
				else {
					cout << "\nInvalid Input...\n\n";
				}
			}
			if (weaponPickUp == 'Y') {
				cout << "You picked up " << weaponsName[currentEventIndex] << " -> " << weaponsDamage[currentEventIndex] << " damage" << endl;
				playerStats.weapon = currentEventIndex;
				mapData[playerPos.y][playerPos.x] = "0";
			}
			else {
				cout << "You left the weapon where it was...\n";
			}
			break;
		}
		case 'I':
		{
			char inputPickUp = ' ';
			bool invalidInputPickUp = true;
			while (invalidInputPickUp) {
				cout << "Would you like to pick up this item? (Y or N)\nRemember, you can NOT go above 100 health\nPick Up : ";
				cin >> inputPickUp;
				if (inputPickUp == 'y' || inputPickUp == 'n') {
					inputPickUp -= 32;
				}

				if (inputPickUp == 'Y' || inputPickUp == 'N') {
					invalidInputPickUp = false;
				}
				else {
					cout << "\nInvalid Input...\n\n";
				}
			}
			if (inputPickUp == 'Y') {
				cout << "You picked up " << itemName[currentEventIndex] << " + " << itemHealth[currentEventIndex] << " health" << endl;
				playerStats.health = playerStats.health + itemHealth[currentEventIndex];
				if (playerStats.health > 100) {
					playerStats.health = 100;
				}
				cout << "Your health is now " << playerStats.health << " health points\n";
				mapData[playerPos.y][playerPos.x] = "0";
			}
			else {
				cout << "You left the item where it was...\n";
			}
			break;
		}

		case 'E':
		{
			bool invalidInputEnemy = true;
			char InputEnemy = ' ';
			while (invalidInputEnemy)
			{
				cout << "A " << enemyName[currentEventIndex] << " (" << enemyHealth[currentEventIndex] << " health, " << enemyDamage[currentEventIndex] << " damage)" << " approaches you!\n";
				cout << "You can:\n\n";
				cout << "H - Hide.....\n";
				cout << "F - Fight it!\n";
				cout << "\nWhat do you do? : ";

				cin >> InputEnemy;

				if (InputEnemy == 'h' || InputEnemy == 'f') {
					InputEnemy -= 32;
				} // Capitalise input no matter what...
				if (InputEnemy == 'H' || InputEnemy == 'F') {
					invalidInputEnemy = false;
				}
				else {
					cout << "\nInvalid Input...\n\n";
				}

			}
			if (InputEnemy == 'H') {
				// Chance to change to F
				cout << "You try to hide...\n";
				int randomChance = GetRandomNumber(1, false); // 0 or 1
				bool hideSuccessful = (bool)randomChance;
				if (hideSuccessful) {
					cout << "You hid successfully! But the " << enemyName[currentEventIndex] << " is still in the area...\n";
				}
				else {
					cout << "You failed to hide! You must now fight the " << enemyName[currentEventIndex] << endl;
					InputEnemy = 'F';
				}
			}
			if (InputEnemy == 'F') {
				cout << "Now fighting the " << enemyName[currentEventIndex] << "\n";
				bool fighting = true;
				int EnemyHealthRemaining = enemyHealth[currentEventIndex];
				while (fighting) {
					// Print out
					cout << "\nEnemy :\n";
					cout << enemyName[currentEventIndex] << endl;
					cout << EnemyHealthRemaining << " health remaining" << endl;
					cout << enemyDamage[currentEventIndex] << " damage" << endl;
					cout << endl;
					cout << "Player :\n";
					cout << name << endl;
					cout << playerStats.health << " health remaining" << endl;
					cout << weaponsDamage[playerStats.weapon] << " damage" << endl;
					cout << endl;

					// Give player options (attack, dodge, heal)
					bool invalidInputFight = true;
					char inputFight = ' ';
					while (invalidInputFight) {
						cout << "Options: \n";
						cout << "A - Attack\n";
						cout << "B - Block\n(If successful = extra turn)\n";
						cout << "H - Heal (+10 Health)\n";
						cout << "What do you do : ";
						cin >> inputFight;

						if (inputFight == 'a' || inputFight == 'b' || inputFight == 'h') {
							inputFight -= 32;
						}
						if (inputFight == 'A' || inputFight == 'B' || inputFight == 'H') {
							invalidInputFight = false;
						}
						else {
							cout << "\nInvalid Input...\n\n";
						}
					}

					if (inputFight == 'A') {
						cout << "You attacked for " << weaponsDamage[playerStats.weapon] << " damage!\n";
						EnemyHealthRemaining -= weaponsDamage[playerStats.weapon];
					}

					bool block = false;
					if (inputFight == 'B') {
						// Block
						int randomChanceBlock = GetRandomNumber(1, false); // 0 or 1 random
						block = (bool)randomChanceBlock;

					}

					if (inputFight == 'H') {
						// Heal
						cout << "You healed for +10 Health!\n";

						playerStats.health += 10;
						if (playerStats.health > 100) {
							playerStats.health = 100;
						}
					}

					if (inputFight == 'B' && block == false) {
						cout << "You failed to block the attack...\n";
					}

					// Enemy attack here (:
					if (block) {
						cout << "You successfully blocked the attack...\n";

						// Get new input??
						bool invalidInputFightB = true;
						char inputFightB = ' ';
						while (invalidInputFightB) {
							cout << "Options: \n";
							cout << "A - Attack\n";
							cout << "H - Heal (+10 Health)\n";
							cout << "What do you do : ";
							cin >> inputFightB;

							if (inputFightB == 'a' || inputFightB == 'h') {
								inputFightB -= 32;
							}
							if (inputFightB == 'A' || inputFightB == 'H') {
								invalidInputFightB = false;
							}
							else {
								cout << "\nInvalid Input...\n\n";
							}
						}

						if (inputFightB == 'A') {
							cout << "You attacked for " << weaponsDamage[playerStats.weapon] << " damage!\n";
							EnemyHealthRemaining -= weaponsDamage[playerStats.weapon];
						}
						if (inputFightB == 'H') {
							// Heal
							cout << "You healed for +10 Health!\n";
							playerStats.health += 10;
							if (playerStats.health > 100) {
								playerStats.health = 100;
							}
						}
					}
					else {
						if (EnemyHealthRemaining > 0) {
							// Enemy attack?
							cout << enemyName[currentEventIndex] << " attacked for " << enemyDamage[currentEventIndex] << " damage" << endl;
							playerStats.health -= enemyDamage[currentEventIndex];
						}
					}

					if (EnemyHealthRemaining <= 0) {
						fighting = false;
					}
					if (playerStats.health <= 0) {
						fighting = 0;
					}
				}

				if (playerStats.health <= 0) {
					cout << "The " << enemyName[currentEventIndex] << " defeated you!!\n";
				}
				else {
					cout << "You defeated the " << enemyName[currentEventIndex] << "!!\n";
					mapData[playerPos.y][playerPos.x] = "0";
				}
				cout << "\nEnd of round for the fight, press enter to continue...\n";
				_getch();
			}
			break;
		}

		case 'B':
		{
			bool invalidInputBoss = true;
			char InputBoss = ' ';
			cout << "\nBOSS FIGHT!!!\n\n";
			while (invalidInputBoss) {

				cout << "A " << bossName[currentEventIndex] << " (" << bossHealth[currentEventIndex] << " health, " << bossDamage[currentEventIndex] << " damage)" << " approaches you!\n";
				cout << "You can:\n\n";
				cout << "H - Hide.....\n";
				cout << "F - Fight it!\n";
				cout << "\nWhat do you do? : ";

				cin >> InputBoss;

				if (InputBoss == 'h' || InputBoss == 'f') {
					InputBoss -= 32;
				} // Capitalise input no matter what...
				if (InputBoss == 'H' || InputBoss == 'F') {
					invalidInputBoss = false;
				}
				else {
					cout << "\nInvalid Input...\n\n";
				}

			}
			if (InputBoss == 'H') {
				// Chance to change to F
				cout << "You try to hide...\n";
				int randomChance = GetRandomNumber(1, false); // 0 or 1
				bool hideSuccessful = (bool)randomChance;
				if (hideSuccessful) {
					cout << "You hid successfully! But the " << bossName[currentEventIndex] << " is still in the area...\n";
				}
				else {
					cout << "You failed to hide! You must now fight the " << bossName[currentEventIndex] << endl;
					InputBoss = 'F';
				}
			}
			if (InputBoss == 'F') {
				cout << "Now fighting the " << bossName[currentEventIndex] << "\n";
				bool fighting = true;
				int EnemyHealthRemaining = bossHealth[currentEventIndex];
				while (fighting) {
					// Print out
					cout << "\nBOSS :\n";
					cout << bossName[currentEventIndex] << endl;
					cout << EnemyHealthRemaining << " health remaining" << endl;
					cout << bossDamage[currentEventIndex] << " damage" << endl;
					cout << endl;
					cout << "Player :\n";
					cout << name << endl;
					cout << playerStats.health << " health remaining" << endl;
					cout << weaponsDamage[playerStats.weapon] << " damage" << endl;
					cout << endl;

					// Give player options (attack, dodge, heal)
					bool invalidInputFight = true;
					char inputFight = ' ';
					while (invalidInputFight) {
						cout << "Options: \n";
						cout << "A - Attack\n";
						cout << "B - Block\n(If successful = extra turn)\n";
						cout << "H - Heal (+10 Health)\n";
						cout << "What do you do : ";
						cin >> inputFight;

						if (inputFight == 'a' || inputFight == 'b' || inputFight == 'h') {
							inputFight -= 32;
						}
						if (inputFight == 'A' || inputFight == 'B' || inputFight == 'H') {
							invalidInputFight = false;
						}
						else {
							cout << "\nInvalid Input...\n\n";
						}
					}

					if (inputFight == 'A') {
						cout << "You attacked for " << weaponsDamage[playerStats.weapon] << " damage!\n";
						EnemyHealthRemaining -= weaponsDamage[playerStats.weapon];
					}

					bool block = false;
					if (inputFight == 'B') {
						// Block
						int randomChanceBlock = GetRandomNumber(1, false); // 0 or 1 random
						block = (bool)randomChanceBlock;

					}

					if (inputFight == 'H') {
						// Heal
						cout << "You healed for +10 Health!\n";

						playerStats.health += 10;
						if (playerStats.health > 100) {
							playerStats.health = 100;
						}
					}

					if (inputFight == 'B' && block == false) {
						cout << "You failed to block the attack...\n";
					}

					// Enemy attack here (:
					if (block) {
						cout << "You successfully blocked the attack...\n";

						// Get new input??
						bool invalidInputFightB = true;
						char inputFightB = ' ';
						while (invalidInputFightB) {
							cout << "Options: \n";
							cout << "A - Attack\n";
							cout << "H - Heal (+10 Health)\n";
							cout << "What do you do : ";
							cin >> inputFightB;

							if (inputFightB == 'a' || inputFightB == 'h') {
								inputFightB -= 32;
							}
							if (inputFightB == 'A' || inputFightB == 'H') {
								invalidInputFightB = false;
							}
							else {
								cout << "\nInvalid Input...\n\n";
							}
						}

						if (inputFightB == 'A') {
							cout << "You attacked for " << weaponsDamage[playerStats.weapon] << " damage!\n";
							EnemyHealthRemaining -= weaponsDamage[playerStats.weapon];
						}
						if (inputFightB == 'H') {
							// Heal
							cout << "You healed for +10 Health!\n";
							playerStats.health += 10;
							if (playerStats.health > 100) {
								playerStats.health = 100;
							}
						}
					}
					else {
						if (EnemyHealthRemaining > 0) {
							// Enemy attack?
							cout << bossName[currentEventIndex] << " attacked for " << bossDamage[currentEventIndex] << " damage" << endl;
							playerStats.health -= bossDamage[currentEventIndex];
						}
					}

					if (EnemyHealthRemaining <= 0) {
						fighting = false;
					}
					if (playerStats.health <= 0) {
						fighting = 0;
					}
				}

				if (playerStats.health <= 0) {
					cout << "The " << bossName[currentEventIndex] << " defeated you!!\n";
				}
				else {
					cout << "You defeated the " << bossName[currentEventIndex] << "!!\n";
					mapData[playerPos.y][playerPos.x] = "0";
				}
				cout << "\nEnd of round for the fight, press enter to continue...\n";
				_getch();
			}
			break;
		}
		default:
		{
			cout << "Nothing to do here...\n";
			break;
		}
		}

		cout << "\nEnd of round, press enter to continue...\n";
		_getch();
	}

}

int GetRandomNumber(int max, bool maxExclusive) {
	int randomNumber = 0;
	if (maxExclusive) {
		randomNumber = rand() % max;
	}
	else {
		randomNumber = rand() % (max + 1);
	}

	return randomNumber;
}