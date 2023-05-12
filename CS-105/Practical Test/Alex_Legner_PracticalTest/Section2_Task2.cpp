// Section 2 -- Task 2 -- Alexander Legner -- 270168960

#include <iostream>
#include <string>
#include <vector>

using std::string;
using std::vector;
using std::cout;

/// <summary>
/// VideoGame class is responsable for holding common parts of the video game catelog
/// </summary>
class VideoGame {
protected:
	string title;
	float price;
public:
	VideoGame(string t, float p) : title(t), price(p) {} // Constructor
	/// <summary>
	/// displays the video game catelog details - override by derived classes
	/// </summary>
	virtual void display() = 0; // Pure virtual function to be overriden by derived classes
};

/// <summary>
/// Computer Game which derives from a VideoGame
/// </summary>
class ComputerGame : public VideoGame {
private:
	string operatingSystemType; // computer games has an operating system
public:
	ComputerGame(string t, float p, string o): operatingSystemType(o), VideoGame(t, p) {} // Constructor and chaining
	/// <summary>
	/// output the computer game catelog data
	/// </summary>
	void display() override {
		cout << "Title : " << title << "\n";
		cout << "Price : " << price << "\n";
		cout << "OS Type : " << operatingSystemType << "\n";
	}
};

/// <summary>
/// Console Game which derives from a VideoGame
/// </summary>
class ConsoleGame : public VideoGame {
private:
	string consoleType; // computer games has a console type
public:
	ConsoleGame(string t, float p, string c) : consoleType(c), VideoGame(t, p) {} // Constructor and chaining
	/// <summary>
	/// output the console game catelog data
	/// </summary>
	void display() override {
		cout << "Title : " << title << "\n";
		cout << "Price : " << price << "\n";
		cout << "Console Type : " << consoleType << "\n";
	}
};

/// <summary>
/// Displays a simple error to the user
/// </summary>
void ErrorInput() {
	cout << "Invalid Input...\n";
}

/// <summary>
/// gets input from the user without crashing
/// </summary>
/// <param name="prompt">a prompt to display to the user before getting input</param>
/// <returns>raw user input</returns>
string GetRawInput(string prompt) {
	// console input
	cout << prompt; // out put prompt to user
	string rawInput;
	std::getline(std::cin >> std::ws, rawInput); // get console input without whitespace and errors
	return rawInput; // return the raw input
}

/// <summary>
/// gets all valid data for a video game of a user defined type
/// </summary>
/// <returns>a valid video game</returns>
VideoGame* GetNewVideoGame() {
	string typeInput, titleInput, priceInput, otherInput, titleInputPrompt; // needed to create a new video game
	float priceChecked; // needed to create a new video game
	
	while (true) { // loop until a valid input is gained
		typeInput = GetRawInput("Do you want to enter data for a Computer Game or a Console Game (o / c) : "); // input
		
		// checks input is valid
		if (typeInput == "o" || typeInput == "O") {
			titleInputPrompt = "computer";
			break;
		}
		else if (typeInput == "c" || typeInput == "C") {
			titleInputPrompt = "console";
			break;
		}
		else {
			ErrorInput();
		}
	}
	titleInput = GetRawInput("Please enter title of " + titleInputPrompt + " game : "); // input
	while (true) { // loop until a valid input is gained
		try {
			priceInput = GetRawInput("Please enter price : ");
			priceChecked = stof(priceInput);
			if (priceChecked < 0) {
				throw std::exception("Invalid input - cannot be negative number");
			}
			break;
		}
		catch (...) {
			ErrorInput();
		}
	}
	otherInput = (titleInputPrompt == "console") ? GetRawInput("Please enter console type : ") : GetRawInput("Please enter operating system type : "); // input based on type

	VideoGame* newVideoGame; // empty pointer
	
	// create new video game of correct type with data gained above
	if (titleInputPrompt == "console") {
		newVideoGame = new ConsoleGame(titleInput, priceChecked, otherInput);
	}
	else {
		newVideoGame = new ComputerGame(titleInput, priceChecked, otherInput);
	}

	return newVideoGame; // return final outcome
}

/// <summary>
/// entry point to the program
/// </summary>
/// <returns>return code</returns>
int main()
{
	cout << "\t\tVideo Games Data Entry\n"; // title
	cout << "**********************************************************\n\n";
	
	vector<VideoGame*>* videoGames = new vector<VideoGame*>(); // pointer to vector of pointer to videogames
	
	string addNewVideoGame = "y"; // loop input
	while (addNewVideoGame != "N" && addNewVideoGame != "n") { // loop while getting input
		videoGames->push_back(GetNewVideoGame()); // create new video game with user input and add to vector
		while (true) { // get valid input
			addNewVideoGame = GetRawInput("Do you want to add another item (y / n) : "); // prompt / input
			// checks input is valid
			if (addNewVideoGame == "y" || addNewVideoGame == "Y" || addNewVideoGame == "n" || addNewVideoGame == "N") {	break; }			
			else { ErrorInput(); }
		}
	}
	
	// after input - output all videogames
	cout << "\nVideo Games List:\n";
	cout << "*******************************\n";
	for (VideoGame* videogame : *videoGames) {
		videogame->display();
		cout << "*******************************\n";
	}

	// delete unused memory
	for (VideoGame* videogame : *videoGames) {
		delete videogame;
		videogame = nullptr;
	}
	videoGames->clear();
	delete videoGames;

	return 0; // exit the program without errors
}