#include <iostream>
#include <cctype>
#include <string>
#include <vector>
#include <sstream>

int main()
{
    const bool DEBUG = false;
    if (DEBUG) { std::cout << "DEGUG Enabled \n"; }

    std::cout << "Enter students names, seperated by a space ' ' eg. -> Alex James etc...\n";
    std::string input;
    std::getline(std::cin, input);

    const char separator = ' ';
    std::vector<std::string> studentNames;
    std::stringstream streamData(input);

    std::string val;

    while (std::getline(streamData, val, separator)) {
        studentNames.push_back(val);
    }

    for (std::string name : studentNames) {
        for (int i = 0; i < name.length(); i++) {
            name[i] = tolower(name[i]);
        }
    }

    int whileLoopCounter = 0;
    bool swapped = true;
    while (swapped) {
        swapped = false;
        int letter = 0;
        if (DEBUG) { std::cout << "DEGUG while loop iteration "<< whileLoopCounter << "\n"; }
        if (DEBUG) { std::cout << "DEGUG There are " << studentNames.size() << " students\n"; }
        for (int i = 0; i < studentNames.size(); i++) {
            if (DEBUG) { std::cout << "DEGUG looking @ " << studentNames[i] << "\n"; }
            if (i != studentNames.size() - 1) { // Check not last
                if (DEBUG) { std::cout << studentNames[i] << " is not the last element in the list (: \n"; }
                bool sameLetter = false;
                if (DEBUG) { std::cout << "DEGUG sameLetter = " << sameLetter << "\n"; }
                try {
                    if (studentNames[i][letter] == studentNames[i + 1][letter]) { // check 
                        if (DEBUG) { std::cout << "DEGUG " << studentNames[i] << " letter " << letter << " = "  << studentNames[i][letter] << " == IS THE SAME AS == " << studentNames[i] << " letter " << letter << " = " << studentNames[i][letter] << "\n"; }
                        sameLetter = true;
                        if (DEBUG) { std::cout << "DEGUG sameLetter = " << sameLetter << "\n"; }
                        while (sameLetter) {
                            i++;
                            if (studentNames[i][letter] == studentNames[i + 1][letter]) {
                                continue;
                            }
                            else if ((int)studentNames[i][letter] < (int)studentNames[i + 1][letter]) {
                                sameLetter = false;
                                break;
                            }
                            else {
                                swapped = true;
                                std::string temp;
                                temp = studentNames[i];
                                studentNames[i] = studentNames[i + 1];
                                studentNames[i + 1] = temp;
                                swapped = true;
                                sameLetter = false;
                                break;
                            }
                        }
                        continue;
                    }
                    else { // if not same letter switch accordingly
                        if (DEBUG) { std::cout << "DEGUG " << studentNames[i] << " letter " << 0 << " = " << studentNames[i][0] << " != IS NOT THE SAME AS != " << studentNames[i+1] << " letter " << 0 << " = " << studentNames[i+1][0] << "\n"; }

                        if (DEBUG) { std::cout << "DEBUG comparing " << (int)studentNames[i][0] << " & " << (int)studentNames[i + 1][0] << "\n"; }

                        if ((int)studentNames[i][0] < (int)studentNames[i + 1][0]) {
                            sameLetter = false;
                            continue;
                        }
                        else {
                            swapped = true;
                            std::string temp;
                            temp = studentNames[i];
                            studentNames[i] = studentNames[i + 1];
                            studentNames[i + 1] = temp;
                            swapped = true;
                            sameLetter = false;
                            continue;
                        }
                    }
                }
                catch (...) {
                    if (sameLetter) {
                        if (studentNames[i].length() == studentNames[i + 1].length()) { // if length is same
                            continue;
                        }
                        else if (studentNames[i].length() <= studentNames[i + 1].length()) {
                            continue;
                        } // No swap if same name is the smaller first
                        else {
                            std::string temp;
                            temp = studentNames[i];
                            studentNames[i] = studentNames[i + 1];
                            studentNames[i + 1] = temp;
                            swapped = true;
                            continue;
                        } // Swap if second is smaller
                    } // All for same letter
                }
            }
        }
        whileLoopCounter++;
    }

    std::cout << "Students names Alphabetical Order : \n";

    for (std::string name : studentNames) {
        std::cout << name << "\n";
    }

    return 0;
}
