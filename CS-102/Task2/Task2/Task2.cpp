// Write a program to check whether a given letter is vowel or not. (vowels: a, e, i, o, u)

// Linkage
#include <iostream>

// Entry point of the program
int main()
{
	// Variable declaration
    char vowel = ' ', vowelParsed = ' '; 

    // User input for vowel
	std::cout << "Enter a letter: ";
    std::cin >> vowel;

	vowelParsed = tolower(vowel); // Parse vowel to lowercase before check

	// Check letter for vowel
	switch (vowelParsed)
	{
		// Vowels
		case 'a':
		case 'e':
		case 'i':
		case 'o':
		case 'u':
			std::cout << "\t\"" << vowel << " - vowel\"\n";
			break;
		// Default (not a vowel)
		default:
			std::cout << "\t\"" << vowel << " - not a vowel\"\n";
			break;
	}

	return 0; // (return code 0) program completed without errors 
}
