#include <iostream>
#include <ctime>
#include <string>
#include <conio.h>
#include <windows.h>
#include <sstream>
#include <cmath>
#include <iomanip>
#include <cstdlib>
#include <gl\gl.h>
#include <gl\glu.h>
#include "GL/freeglut.h"
#pragma comment(lib, "OpenGL32.lib")

using std::cout;
using std::cin;
using std::string;
using std::endl;

int GetRandomNumber(int max, bool maxExclusive = true);
void TicTacToe();
void NumberGuessing();
void Worldle();
void AdventureGame();

// Pong 
void Pong();
void drawPong();
void updatePong(int value);
void vec2_norm(float& x, float& y);
void updateBall();
void keyboard();
void drawRect(float x, float y, float width, float height);
std::string int2str(int x);
void drawText(float x, float y, std::string text);
void enable2D(int width, int height);
int width = 500;
int height = 200;
int interval = 1000 / 60;
int score_left = 0;
int score_right = 0;
float ball_pos_x = width / 2;
float ball_pos_y = height / 2;
float ball_dir_x = -1.0f;
float ball_dir_y = 0.0f;
int ball_size = 8;
int ball_speed = 2;
int racket_width = 10;
int racket_height = 80;
int racket_speed = 3;
float racket_left_x = 10.0f;
float racket_left_y = 50.0f;
float racket_right_x = width - racket_width - 10;
float racket_right_y = 50;

#define VK_W 0x57
#define VK_S 0x53

void HangMan();
void drawHangMan();
void drawInvertedHangMan();
void drawWalkMan();
void showMenuHM();

string pickQuestion(int questionNumber);
string correctAnswer(int questionNumber);

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
	srand(time(NULL));

	bool loop = true;
	while (loop)
	{
		cout << "What game do you want to play?\n";

		cout << "1 - Tic Tac Toe\n";
		cout << "2 - Number Guessing\n";
		cout << "3 - Worldle\n";
		cout << "4 - Adventure Game\n";
		cout << "5 - Pong\n";
		cout << "6 - Hangman\n";

		cout << "0 - Exit\n";

		int userInput = 0;
		cin >> userInput;

		switch (userInput)
		{
			case 0:
				loop = false;
				break;

			case 1:
				TicTacToe();
				break;

			case 2:
				NumberGuessing();
				break;

			case 3:
				Worldle();
				break;

			case 4:
				AdventureGame();
				break;
			case 5:
				Pong();
				break;
			case 6:
				HangMan();
				break;
			default:
				cout << "\nThat is not a valid option :(\n\n";
				userInput = -1;
				break;
		}

	}

	cout << "Thanks for playing (:\n";
	return 0;
}

void TicTacToe() {

	cout << "\nLoading Tic Tac Toe...\n\n";

	cout << "Player = O\n";
	cout << "Computer = X\n";

	const int boardSize = 3;
	bool playing = true;
	int board[boardSize][boardSize] =
	{
		{0, 0, 0},
		{0, 0, 0},
		{0, 0, 0}
	};

	cout << endl;

	int win = 0; // 0 for none, 1 for player 1, 2 for player 2
	bool endGame = false;


	while (playing) {
		
		

		// Check if board is full
		bool boardFull = true;
		for (int row = 0; row < boardSize; row++) {
			for (int collumn = 0; collumn < boardSize; collumn++) {
				if (board[row][collumn] == 0) {
					boardFull = false;
				}
			}
		}

		if ((win == 0 && boardFull) || win != 0) {
			// Print the current board
			for (int row = 0; row < boardSize; row++) {
				for (int collumn = 0; collumn < boardSize; collumn++) {
					if (board[row][collumn] == 1) {
						cout << "O";
					}
					else if (board[row][collumn] == 2) {
						cout << "X";
					}
					else {
						cout << " ";
					}
					if (collumn < boardSize - 1) {
						cout << " | ";
					}
				}
				if (row < boardSize - 1) {
					cout << "\n---------\n";
				}

			}
			cout << endl << endl;
		}

		if (win == 0 && boardFull) {
			cout << "\nSTALEMATE\n\n";
			endGame = true;
		}
		else if (win == 1) {
			cout << "\nPlayer WINS!!!\n\n";
			endGame = true;
		}
		else if (win == 2) {
			cout << "\nComputer WINS!!!\n\n";
			endGame = true;
		}

		if (boardFull) {
			endGame = true;
		}

		if (endGame) {
			playing = false;
			break;
		}




		// Print the current board
		for (int row = 0; row < boardSize; row++) {
			for (int collumn = 0; collumn < boardSize; collumn++) {
				if (board[row][collumn] == 1) {
					cout << "O";
				}
				else if (board[row][collumn] == 2) {
					cout << "X";
				}
				else {
					cout << " ";
				}
				if (collumn < boardSize - 1) {
					cout << " | ";
				}
			}
			if (row < boardSize - 1) {
				cout << "\n---------\n";
			}
			
		}
		cout << endl << endl;
		
		// Input for player 1
		bool turnPlayed = false;
		do {
			int rowInput;
			int collumnInput;
			do {
				cout << "Enter row (0,1,2): ";
				cin >> rowInput;
			} while (rowInput < 0 || rowInput >= boardSize);

			do {
				cout << "Enter collumn (0,1,2): ";
				cin >> collumnInput;
			} while (collumnInput < 0 || collumnInput >= boardSize);

			if (board[rowInput][collumnInput] == 0) {
				board[rowInput][collumnInput] = 1;
				turnPlayed = true;
			}
			else {
				cout << endl << "INVALID INPUT..." << endl << endl;
				turnPlayed = false;
			}
		} while (turnPlayed == false);
		
		// Print the current board
		for (int row = 0; row < boardSize; row++) {
			for (int collumn = 0; collumn < boardSize; collumn++) {
				if (board[row][collumn] == 1) {
					cout << "O";
				}
				else if (board[row][collumn] == 2) {
					cout << "X";
				}
				else {
					cout << " ";
				}
				if (collumn < boardSize - 1) {
					cout << " | ";
				}
			}
			if (row < boardSize - 1) {
				cout << "\n---------\n";
			}

		}
		cout << endl << endl;

		// Check if board is full
		boardFull = true;
		for (int row = 0; row < boardSize; row++) {
			for (int collumn = 0; collumn < boardSize; collumn++) {
				if (board[row][collumn] == 0) {
					boardFull = false;
				}
			}
		}


		// Player 2 turn (computer)
		if (boardFull == false) {
			bool computerPlayed = false;
			do {
				int rowInput;
				int collumnInput;
				do {
					rowInput = GetRandomNumber(boardSize);
				} while (rowInput < 0 || rowInput >= boardSize);
				do {
					collumnInput = GetRandomNumber(boardSize);
				} while (collumnInput < 0 || collumnInput >= boardSize);

				if (board[rowInput][collumnInput] == 0) {
					board[rowInput][collumnInput] = 2;
					computerPlayed = true;
				}
				else {
					computerPlayed = false;
				}
			} while (computerPlayed == false);

		}
		
		// Check if someone won
		// ------------------------------------------------------------------------

		int Lines[8][3];
		int lineInput = 0;

		

		// each row
		for (int row = 0; row < boardSize; row++) {
			for (int collumn = 0; collumn < boardSize; collumn++) {
				Lines[lineInput][collumn] = board[row][collumn];
			}
			lineInput++;
		}

		// each collumn
		for (int collumn = 0; collumn < boardSize; collumn++) {
			for (int row = 0; row < boardSize; row++){
				Lines[lineInput][row] = board[row][collumn];
			}
			lineInput++;
		}

		// diagnals
		Lines[lineInput][0] = board[0][0];
		Lines[lineInput][1] = board[1][1];
		Lines[lineInput][2] = board[2][2];

		lineInput++;

		Lines[lineInput][0] = board[0][2];
		Lines[lineInput][1] = board[1][1];
		Lines[lineInput][2] = board[2][0];


		// do check
		for (int check = 0; check < 8; check++) {
			bool noDetected = false;
			bool oneDetected = false;
			bool twoDetected = false;
			for (int line = 0; line < boardSize; line++) {
				if (Lines[check][line] == 0) {
					noDetected = true;
				}
				if (Lines[check][line] == 1) {
					oneDetected = true;
				}
				if (Lines[check][line] == 2) {
					twoDetected = true;
				}
			}
			if (oneDetected == true && noDetected == false && twoDetected == false) {
				win = 1;
			}
			if (twoDetected == true && noDetected == false && oneDetected == false) {
				win = 2;
			}
		}
	};
}

void NumberGuessing() {
	cout << "\nLoading Number Guesser...\n\n";
	
	bool playing = true;
	bool selectingDifficulty = true;
	int difficultySelection = -1;
	
	// Select difficulty
	while (selectingDifficulty) {
		cout << "1 - Easy    1 - 10\n";
		cout << "2 - Medium  1 - 25\n";
		cout << "3 - Hard    1 - 50\n";

		cout << "\n0 - Exit\n";

		cout << "\nEnter difficulty : ";
		cin >> difficultySelection;
		cout << endl;

		switch (difficultySelection) {
			case 1:
			case 2:
			case 3:
				selectingDifficulty = false;
				break;
			case 0:
				selectingDifficulty = false;
				playing = false;
				return;
			default:
				cout << "\nThat is not a valid option :(\n\n";
				break;
		}
	}

	// Select random number
	int randomNumber = -1;
	switch (difficultySelection) {
		case 1:
			randomNumber = GetRandomNumber(10, false);
			break;
		case 2:
			randomNumber = GetRandomNumber(25, false);
			break;
		case 3:
			randomNumber = GetRandomNumber(50, false);
			break;
		default:
			playing = false;
			return;
			break;
	}
	cout << "OKAY, I have though of a number (:\n";

	int guesses = 5;

	// Guessing here
	while (playing) {
		int guess = -1;
		cout << "\nYou have " << guesses << " guesses left!\n\n";

		cout << "Enter your guess between ";
		switch (difficultySelection) {
			case 1:
				cout << "1 and 10";
				break;
			case 2:
				cout << "1 and 25";
				break;
			case 3:
				cout << "1 and 50";
				break;
			default:
				break;
		}
		cout << ".\n" << "Guess : ";

		bool invalidGuess = true;
		while (invalidGuess) {
			cin >> guess;
			switch (difficultySelection) {
				case 1:
					if (guess > 10) {
						invalidGuess = true;
					}
					else {
						invalidGuess = false;
					}
					break;
				case 2:
					if (guess > 25) {
						invalidGuess = true;
					}
					else {
						invalidGuess = false;
					}
					break;
				case 3:
					if (guess > 50) {
						invalidGuess = true;
					}
					else {
						invalidGuess = false;
					}
					break;
				default:
					break;
			}

			if (guess <= 0) {
				invalidGuess = true;
			}

			if (invalidGuess) {
				cout << "\nThat is not a valid option :(\n\n";
				cout << "Guess : ";
			}
			else {
				guesses--;
			}
		}



		if (guess == randomNumber) {
			cout << "That is correct!!!\n";
			cout << "YOU WIN!!!\n\n";
			playing = false;
			return;
		}
		else {
			cout << "That is NOT correct...\n";
			cout << "My number is ";

			int difference = std::abs(guess - randomNumber);
			if (difference <= 5) {
				cout << "a LITTLE ";
			}
			else if (difference > 5 && difference <= 10) {
				// do nothing
			}
			else {
				cout << "a LOT ";
			}

			int polarity = guess - randomNumber;
			if (polarity < 0) {
				cout << "LARGER";
			}
			else {
				cout << "SMALLER";
			}

			cout << " than your guess.\n";
		}


		if (guesses == 0) {
			// Do end game (:
			cout << "My number was " << randomNumber << endl;
			cout << "YOU LOSE!!!\n\n";
			playing = false;
		}
	}

}

void Worldle() {
	cout << "\nLoading Worldle...\n\n";

	const int WORDLISTSIZE = 2315;
	const int WORDSIZE = 5;
	const int WORDSGUESSES = 6;
	string * WORDLIST = new string [2315] {
		"aback","abase","abate","abbey","abbot","abhor","abide","abled","abode","abort","about","above","abuse","abyss","acorn","acrid","actor","acute","adage","adapt","adept","admin","admit","adobe","adopt","adore","adorn","adult","affix","afire","afoot","afoul","after","again","agape","agate","agent","agile","aging","aglow","agony","agora","agree","ahead","aider","aisle","alarm","album","alert","algae","alibi","alien","align","alike","alive","allay","alley","allot","allow","alloy","aloft","alone","along","aloof","aloud","alpha","altar","alter","amass","amaze","amber","amble","amend","amiss","amity","among","ample","amply","amuse","angel","anger","angle","angry","angst","anime","ankle","annex","annoy","annul","anode","antic","anvil","aorta","apart","aphid","aping","apnea","apple","apply","apron","aptly","arbor","ardor","arena","argue","arise","armor","aroma","arose","array","arrow","arson","artsy","ascot","ashen","aside","askew","assay","asset","atoll","atone","attic","audio","audit","augur","aunty","avail","avert","avian","avoid","await","awake","award","aware","awash","awful","awoke","axial","axiom","axion","azure","bacon","badge","badly","bagel","baggy","baker","baler","balmy","banal","banjo","barge","baron","basal","basic","basil","basin","basis","baste","batch","bathe","baton","batty","bawdy","bayou","beach","beady","beard","beast","beech","beefy","befit","began","begat","beget","begin","begun","being","belch","belie","belle","belly","below","bench","beret","berry","berth","beset","betel","bevel","bezel","bible","bicep","biddy","bigot","bilge","billy","binge","bingo","biome","birch","birth","bison","bitty","black","blade","blame","bland","blank","blare","blast","blaze","bleak","bleat","bleed","bleep","blend","bless","blimp","blind","blink","bliss","blitz","bloat","block","bloke","blond","blood","bloom","blown","bluer","bluff","blunt","blurb","blurt","blush","board","boast","bobby","boney","bongo","bonus","booby","boost","booth","booty","booze","boozy","borax","borne","bosom","bossy","botch","bough","boule","bound","bowel","boxer","brace","braid","brain","brake","brand","brash","brass","brave","bravo","brawl","brawn","bread","break","breed","briar","bribe","brick","bride","brief","brine","bring","brink","briny","brisk","broad","broil","broke","brood","brook","broom","broth","brown","brunt","brush","brute","buddy","budge","buggy","bugle","build","built","bulge","bulky","bully","bunch","bunny","burly","burnt","burst","bused","bushy","butch","butte","buxom","buyer","bylaw","cabal","cabby","cabin","cable","cacao","cache","cacti","caddy","cadet","cagey","cairn","camel","cameo","canal","candy","canny","canoe","canon","caper","caput","carat","cargo","carol","carry","carve","caste","catch","cater","catty","caulk","cause","cavil","cease","cedar","cello","chafe","chaff","chain","chair","chalk","champ","chant","chaos","chard","charm","chart","chase","chasm","cheap","cheat","check","cheek","cheer","chess","chest","chick","chide","chief","child","chili","chill","chime","china","chirp","chock","choir","choke","chord","chore","chose","chuck","chump","chunk","churn","chute","cider","cigar","cinch","circa","civic","civil","clack","claim","clamp","clang","clank","clash","clasp","class","clean","clear","cleat","cleft","clerk","click","cliff","climb","cling","clink","cloak","clock","clone","close","cloth","cloud","clout","clove","clown","cluck","clued","clump","clung","coach","coast","cobra","cocoa","colon","color","comet","comfy","comic","comma","conch","condo","conic","copse","coral","corer","corny","couch","cough","could","count","coupe","court","coven","cover","covet","covey","cower","coyly","crack","craft","cramp","crane","crank","crash","crass","crate","crave","crawl","craze","crazy","creak","cream","credo","creed","creek","creep","creme","crepe","crept","cress","crest","crick","cried","crier","crime","crimp","crisp","croak","crock","crone","crony","crook","cross","croup","crowd","crown","crude","cruel","crumb","crump","crush","crust","crypt","cubic","cumin","curio","curly","curry","curse","curve","curvy","cutie","cyber","cycle","cynic","daddy","daily","dairy","daisy","dally","dance","dandy","datum","daunt","dealt","death","debar","debit","debug","debut","decal","decay","decor","decoy","decry","defer","deign","deity","delay","delta","delve","demon","demur","denim","dense","depot","depth","derby","deter","detox","deuce","devil","diary","dicey","digit","dilly","dimly","diner","dingo","dingy","diode","dirge","dirty","disco","ditch","ditto","ditty","diver","dizzy","dodge","dodgy","dogma","doing","dolly","donor","donut","dopey","doubt","dough","dowdy","dowel","downy","dowry","dozen","draft","drain","drake","drama","drank","drape","drawl","drawn","dread","dream","dress","dried","drier","drift","drill","drink","drive","droit","droll","drone","drool","droop","dross","drove","drown","druid","drunk","dryer","dryly","duchy","dully","dummy","dumpy","dunce","dusky","dusty","dutch","duvet","dwarf","dwell","dwelt","dying","eager","eagle","early","earth","easel","eaten","eater","ebony","eclat","edict","edify","eerie","egret","eight","eject","eking","elate","elbow","elder","elect","elegy","elfin","elide","elite","elope","elude","email","embed","ember","emcee","empty","enact","endow","enema","enemy","enjoy","ennui","ensue","enter","entry","envoy","epoch","epoxy","equal","equip","erase","erect","erode","error","erupt","essay","ester","ether","ethic","ethos","etude","evade","event","every","evict","evoke","exact","exalt","excel","exert","exile","exist","expel","extol","extra","exult","eying","fable","facet","faint","fairy","faith","false","fancy","fanny","farce","fatal","fatty","fault","fauna","favor","feast","fecal","feign","fella","felon","femme","femur","fence","feral","ferry","fetal","fetch","fetid","fetus","fever","fewer","fiber","fibre","ficus","field","fiend","fiery","fifth","fifty","fight","filer","filet","filly","filmy","filth","final","finch","finer","first","fishy","fixer","fizzy","fjord","flack","flail","flair","flake","flaky","flame","flank","flare","flash","flask","fleck","fleet","flesh","flick","flier","fling","flint","flirt","float","flock","flood","floor","flora","floss","flour","flout","flown","fluff","fluid","fluke","flume","flung","flunk","flush","flute","flyer","foamy","focal","focus","foggy","foist","folio","folly","foray","force","forge","forgo","forte","forth","forty","forum","found","foyer","frail","frame","frank","fraud","freak","freed","freer","fresh","friar","fried","frill","frisk","fritz","frock","frond","front","frost","froth","frown","froze","fruit","fudge","fugue","fully","fungi","funky","funny","furor","furry","fussy","fuzzy","gaffe","gaily","gamer","gamma","gamut","gassy","gaudy","gauge","gaunt","gauze","gavel","gawky","gayer","gayly","gazer","gecko","geeky","geese","genie","genre","ghost","ghoul","giant","giddy","gipsy","girly","girth","given","giver","glade","gland","glare","glass","glaze","gleam","glean","glide","glint","gloat","globe","gloom","glory","gloss","glove","glyph","gnash","gnome","godly","going","golem","golly","gonad","goner","goody","gooey","goofy","goose","gorge","gouge","gourd","grace","grade","graft","grail","grain","grand","grant","grape","graph","grasp","grass","grate","grave","gravy","graze","great","greed","green","greet","grief","grill","grime","grimy","grind","gripe","groan","groin","groom","grope","gross","group","grout","grove","growl","grown","gruel","gruff","grunt","guard","guava","guess","guest","guide","guild","guile","guilt","guise","gulch","gully","gumbo","gummy","guppy","gusto","gusty","gypsy","habit","hairy","halve","handy","happy","hardy","harem","harpy","harry","harsh","haste","hasty","hatch","hater","haunt","haute","haven","havoc","hazel","heady","heard","heart","heath","heave","heavy","hedge","hefty","heist","helix","hello","hence","heron","hilly","hinge","hippo","hippy","hitch","hoard","hobby","hoist","holly","homer","honey","honor","horde","horny","horse","hotel","hotly","hound","house","hovel","hover","howdy","human","humid","humor","humph","humus","hunch","hunky","hurry","husky","hussy","hutch","hydro","hyena","hymen","hyper","icily","icing","ideal","idiom","idiot","idler","idyll","igloo","iliac","image","imbue","impel","imply","inane","inbox","incur","index","inept","inert","infer","ingot","inlay","inlet","inner","input","inter","intro","ionic","irate","irony","islet","issue","itchy","ivory","jaunt","jazzy","jelly","jerky","jetty","jewel","jiffy","joint","joist","joker","jolly","joust","judge","juice","juicy","jumbo","jumpy","junta","junto","juror","kappa","karma","kayak","kebab","khaki","kinky","kiosk","kitty","knack","knave","knead","kneed","kneel","knelt","knife","knock","knoll","known","koala","krill","label","labor","laden","ladle","lager","lance","lanky","lapel","lapse","large","larva","lasso","latch","later","lathe","latte","laugh","layer","leach","leafy","leaky","leant","leapt","learn","lease","leash","least","leave","ledge","leech","leery","lefty","legal","leggy","lemon","lemur","leper","level","lever","libel","liege","light","liken","lilac","limbo","limit","linen","liner","lingo","lipid","lithe","liver","livid","llama","loamy","loath","lobby","local","locus","lodge","lofty","logic","login","loopy","loose","lorry","loser","louse","lousy","lover","lower","lowly","loyal","lucid","lucky","lumen","lumpy","lunar","lunch","lunge","lupus","lurch","lurid","lusty","lying","lymph","lynch","lyric","macaw","macho","macro","madam","madly","mafia","magic","magma","maize","major","maker","mambo","mamma","mammy","manga","mange","mango","mangy","mania","manic","manly","manor","maple","march","marry","marsh","mason","masse","match","matey","mauve","maxim","maybe","mayor","mealy","meant","meaty","mecca","medal","media","medic","melee","melon","mercy","merge","merit","merry","metal","meter","metro","micro","midge","midst","might","milky","mimic","mince","miner","minim","minor","minty","minus","mirth","miser","missy","mocha","modal","model","modem","mogul","moist","molar","moldy","money","month","moody","moose","moral","moron","morph","mossy","motel","motif","motor","motto","moult","mound","mount","mourn","mouse","mouth","mover","movie","mower","mucky","mucus","muddy","mulch","mummy","munch","mural","murky","mushy","music","musky","musty","myrrh","nadir","naive","nanny","nasal","nasty","natal","naval","navel","needy","neigh","nerdy","nerve","never","newer","newly","nicer","niche","niece","night","ninja","ninny","ninth","noble","nobly","noise","noisy","nomad","noose","north","nosey","notch","novel","nudge","nurse","nutty","nylon","nymph","oaken","obese","occur","ocean","octal","octet","odder","oddly","offal","offer","often","olden","older","olive","ombre","omega","onion","onset","opera","opine","opium","optic","orbit","order","organ","other","otter","ought","ounce","outdo","outer","outgo","ovary","ovate","overt","ovine","ovoid","owing","owner","oxide","ozone","paddy","pagan","paint","paler","palsy","panel","panic","pansy","papal","paper","parer","parka","parry","parse","party","pasta","paste","pasty","patch","patio","patsy","patty","pause","payee","payer","peace","peach","pearl","pecan","pedal","penal","pence","penne","penny","perch","peril","perky","pesky","pesto","petal","petty","phase","phone","phony","photo","piano","picky","piece","piety","piggy","pilot","pinch","piney","pinky","pinto","piper","pique","pitch","pithy","pivot","pixel","pixie","pizza","place","plaid","plain","plait","plane","plank","plant","plate","plaza","plead","pleat","plied","plier","pluck","plumb","plume","plump","plunk","plush","poesy","point","poise","poker","polar","polka","polyp","pooch","poppy","porch","poser","posit","posse","pouch","pound","pouty","power","prank","prawn","preen","press","price","prick","pride","pried","prime","primo","print","prior","prism","privy","prize","probe","prone","prong","proof","prose","proud","prove","prowl","proxy","prude","prune","psalm","pubic","pudgy","puffy","pulpy","pulse","punch","pupal","pupil","puppy","puree","purer","purge","purse","pushy","putty","pygmy","quack","quail","quake","qualm","quark","quart","quash","quasi","queen","queer","quell","query","quest","queue","quick","quiet","quill","quilt","quirk","quite","quota","quote","quoth","rabbi","rabid","racer","radar","radii","radio","rainy","raise","rajah","rally","ralph","ramen","ranch","randy","range","rapid","rarer","raspy","ratio","ratty","raven","rayon","razor","reach","react","ready","realm","rearm","rebar","rebel","rebus","rebut","recap","recur","recut","reedy","refer","refit","regal","rehab","reign","relax","relay","relic","remit","renal","renew","repay","repel","reply","rerun","reset","resin","retch","retro","retry","reuse","revel","revue","rhino","rhyme","rider","ridge","rifle","right","rigid","rigor","rinse","ripen","riper","risen","riser","risky","rival","river","rivet","roach","roast","robin","robot","rocky","rodeo","roger","rogue","roomy","roost","rotor","rouge","rough","round","rouse","route","rover","rowdy","rower","royal","ruddy","ruder","rugby","ruler","rumba","rumor","rupee","rural","rusty","sadly","safer","saint","salad","sally","salon","salsa","salty","salve","salvo","sandy","saner","sappy","sassy","satin","satyr","sauce","saucy","sauna","saute","savor","savoy","savvy","scald","scale","scalp","scaly","scamp","scant","scare","scarf","scary","scene","scent","scion","scoff","scold","scone","scoop","scope","score","scorn","scour","scout","scowl","scram","scrap","scree","screw","scrub","scrum","scuba","sedan","seedy","segue","seize","semen","sense","sepia","serif","serum","serve","setup","seven","sever","sewer","shack","shade","shady","shaft","shake","shaky","shale","shall","shalt","shame","shank","shape","shard","share","shark","sharp","shave","shawl","shear","sheen","sheep","sheer","sheet","sheik","shelf","shell","shied","shift","shine","shiny","shire","shirk","shirt","shoal","shock","shone","shook","shoot","shore","shorn","short","shout","shove","shown","showy","shrew","shrub","shrug","shuck","shunt","shush","shyly","siege","sieve","sight","sigma","silky","silly","since","sinew","singe","siren","sissy","sixth","sixty","skate","skier","skiff","skill","skimp","skirt","skulk","skull","skunk","slack","slain","slang","slant","slash","slate","slave","sleek","sleep","sleet","slept","slice","slick","slide","slime","slimy","sling","slink","sloop","slope","slosh","sloth","slump","slung","slunk","slurp","slush","slyly","smack","small","smart","smash","smear","smell","smelt","smile","smirk","smite","smith","smock","smoke","smoky","smote","snack","snail","snake","snaky","snare","snarl","sneak","sneer","snide","sniff","snipe","snoop","snore","snort","snout","snowy","snuck","snuff","soapy","sober","soggy","solar","solid","solve","sonar","sonic","sooth","sooty","sorry","sound","south","sower","space","spade","spank","spare","spark","spasm","spawn","speak","spear","speck","speed","spell","spelt","spend","spent","sperm","spice","spicy","spied","spiel","spike","spiky","spill","spilt","spine","spiny","spire","spite","splat","split","spoil","spoke","spoof","spook","spool","spoon","spore","sport","spout","spray","spree","sprig","spunk","spurn","spurt","squad","squat","squib","stack","staff","stage","staid","stain","stair","stake","stale","stalk","stall","stamp","stand","stank","stare","stark","start","stash","state","stave","stead","steak","steal","steam","steed","steel","steep","steer","stein","stern","stick","stiff","still","stilt","sting","stink","stint","stock","stoic","stoke","stole","stomp","stone","stony","stood","stool","stoop","store","stork","storm","story","stout","stove","strap","straw","stray","strip","strut","stuck","study","stuff","stump","stung","stunk","stunt","style","suave","sugar","suing","suite","sulky","sully","sumac","sunny","super","surer","surge","surly","sushi","swami","swamp","swarm","swash","swath","swear","sweat","sweep","sweet","swell","swept","swift","swill","swine","swing","swirl","swish","swoon","swoop","sword","swore","sworn","swung","synod","syrup","tabby","table","taboo","tacit","tacky","taffy","taint","taken","taker","tally","talon","tamer","tango","tangy","taper","tapir","tardy","tarot","taste","tasty","tatty","taunt","tawny","teach","teary","tease","teddy","teeth","tempo","tenet","tenor","tense","tenth","tepee","tepid","terra","terse","testy","thank","theft","their","theme","there","these","theta","thick","thief","thigh","thing","think","third","thong","thorn","those","three","threw","throb","throw","thrum","thumb","thump","thyme","tiara","tibia","tidal","tiger","tight","tilde","timer","timid","tipsy","titan","tithe","title","toast","today","toddy","token","tonal","tonga","tonic","tooth","topaz","topic","torch","torso","torus","total","totem","touch","tough","towel","tower","toxic","toxin","trace","track","tract","trade","trail","train","trait","tramp","trash","trawl","tread","treat","trend","triad","trial","tribe","trice","trick","tried","tripe","trite","troll","troop","trope","trout","trove","truce","truck","truer","truly","trump","trunk","truss","trust","truth","tryst","tubal","tuber","tulip","tulle","tumor","tunic","turbo","tutor","twang","tweak","tweed","tweet","twice","twine","twirl","twist","twixt","tying","udder","ulcer","ultra","umbra","uncle","uncut","under","undid","undue","unfed","unfit","unify","union","unite","unity","unlit","unmet","unset","untie","until","unwed","unzip","upper","upset","urban","urine","usage","usher","using","usual","usurp","utile","utter","vague","valet","valid","valor","value","valve","vapid","vapor","vault","vaunt","vegan","venom","venue","verge","verse","verso","verve","vicar","video","vigil","vigor","villa","vinyl","viola","viper","viral","virus","visit","visor","vista","vital","vivid","vixen","vocal","vodka","vogue","voice","voila","vomit","voter","vouch","vowel","vying","wacky","wafer","wager","wagon","waist","waive","waltz","warty","waste","watch","water","waver","waxen","weary","weave","wedge","weedy","weigh","weird","welch","welsh","wench","whack","whale","wharf","wheat","wheel","whelp","where","which","whiff","while","whine","whiny","whirl","whisk","white","whole","whoop","whose","widen","wider","widow","width","wield","wight","willy","wimpy","wince","winch","windy","wiser","wispy","witch","witty","woken","woman","women","woody","wooer","wooly","woozy","wordy","world","worry","worse","worst","worth","would","wound","woven","wrack","wrath","wreak","wreck","wrest","wring","wrist","write","wrong","wrote","wrung","wryly","yacht","yearn","yeast","yield","young","youth","zebra","zesty","zonal"
	}; // Word list added to heap
	string WordToGuess;

	string randomWord = WORDLIST[GetRandomNumber(WORDLISTSIZE)];
	//cout << "DEBUG - Word is " << randomWord << endl;
	
	char guesses[WORDSGUESSES][WORDSIZE] = {
		{'\0','\0','\0','\0','\0'},
		{'\0','\0','\0','\0','\0'},
		{'\0','\0','\0','\0','\0'},
		{'\0','\0','\0','\0','\0'},
		{'\0','\0','\0','\0','\0'},
		{'\0','\0','\0','\0','\0'}
	};

	bool playing = true;
	int guessesLeft = WORDSGUESSES;
	while (playing) {
		// print all guesses
		for (int word = 0; word < WORDSGUESSES; word++) {
			for (int letter = 0; letter < WORDSIZE; letter++) {
				char display = guesses[word][letter];
				if (display == '\0') {
					cout << " _ ";
				}
				else {
					cout << ' ';
					if (randomWord[letter] == display) {
						printf("\x1B[32m%c\033[0m", display); // green text for correct all
					}
					else if (randomWord.find(display) != string::npos) {
						printf("\x1B[33m%c\033[0m", display); // yellow text for correct but wrong pos
					}
					else {
						cout << display;
					}
					cout << ' ';
				}
			}
			cout << endl;
		}

		// get next word input if needed
		if (guessesLeft == 0) {
			cout << "\nYOU LOSE!!\nThe word was : " << randomWord << endl;
			playing = false;
		}
		else {
			// Guess here
			bool invalidInput = true;
			string input = "";
			while (invalidInput) {
				cout << "Enter a 5 letter word :\n";
				std::getline(cin >> std::ws, input);
				bool passed = false;
				if (input.length() == WORDSIZE) {
					bool allLetters = true;
					for (int i = 0; i < WORDSIZE; i++) {
						if (isalpha(input[i])) {
							if (input[i] < 'a') {
								input[i] = input[i] + 32;
							}
						}
						else {
							allLetters = false;
						}
					}
					if (allLetters) {
						passed = true;
						invalidInput = false;
					}

				}
				if (passed == false) {
					cout << "\nINVALID INPUT...\n\n";
				}
			}

			int wordInputIndex = WORDSGUESSES - guessesLeft;
			for (int letter = 0; letter < WORDSIZE; letter++) {
				guesses[wordInputIndex][letter] = input[letter];
			}
			guessesLeft--;

			if (input == randomWord) {
				// print all guesses
				for (int word = 0; word < WORDSGUESSES; word++) {
					for (int letter = 0; letter < WORDSIZE; letter++) {
						char display = guesses[word][letter];
						if (display == '\0') {
							cout << " _ ";
						}
						else {
							cout << ' ';
							if (randomWord[letter] == display) {
								printf("\x1B[32m%c\033[0m", display); // green text for correct all
							}
							else if (randomWord.find(display) != string::npos) {
								printf("\x1B[33m%c\033[0m", display); // yellow text for correct but wrong pos
							}
							else {
								cout << display;
							}
							cout << ' ';
						}
					}
					cout << endl;
				}
				cout << "\nYOU WIN!!!!!\n\n";
				playing = false;
			}
		}

		// if end game break
	}

	// printf("\x1B[32m%c\033[0m", 'a'); (GREEN TEXT)

	delete[] WORDLIST; // Word list removed from heap
}

void AdventureGame() {
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
	
	const int enemyHealth[] = {2, 8, 16, 32, 64};
	const int enemyDamage[] = {1, 2, 4, 16, 32};
	const string enemyName[] = {"Bandit", "Archer", "Assassin","Knight", "Warewolf"};

	const int bossHealth[] = {25, 50, 1000};
	const int bossDamage[] = {20, 30, 100};
	const string bossName[] = {"King", "Emperor", "DEATH"};

	const int itemHealth[] = {5, 10, 20, 50, 100};
	const string itemName[] = {"Bread", "Meat", "Vegetables", "Health Potion", "Life Elixer"};


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
				while (invalidInputBoss){
				
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

void Pong() {
	score_left = 0;
	score_right = 0;
	ball_pos_x = width / 2;
	ball_pos_y = height / 2;
	ball_dir_x = -1.0f;
	ball_dir_y = 0.0f;
	ball_size = 8;
	ball_speed = 5;
	racket_width = 10;
	racket_height = 80;
	racket_speed = 3;
	racket_left_x = 10.0f;
	racket_left_y = 50.0f;
	racket_right_x = width - racket_width - 10;
	racket_right_y = 50;



	char* myargv[1];
	int myargc = 1;
	myargv[0] = _strdup("Pong");

	glutInit(&myargc, myargv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB | GLUT_DEPTH);
	glutInitWindowSize(width, height);
	glutCreateWindow("Pong");

	glutSetOption(GLUT_ACTION_ON_WINDOW_CLOSE, GLUT_ACTION_GLUTMAINLOOP_RETURNS);

	// Register callback functions
	glutDisplayFunc(drawPong);
	glutTimerFunc(interval, updatePong, 0);

	// setup scene to 2d mode and set draw color to white
	enable2D(width, height);
	glColor3f(1.0f, 1.0f, 1.0f);

	// start the whole thing
	glutMainLoop();
}

void drawPong() {
	// clear (has to be done at the beginning)
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();

	// draw ball
	drawRect(ball_pos_x - ball_size / 2, ball_pos_y - ball_size / 2, ball_size, ball_size);

	// draw rackets
	drawRect(racket_left_x, racket_left_y, racket_width, racket_height);
	drawRect(racket_right_x, racket_right_y, racket_width, racket_height);

	// draw score
	drawText(width / 2 - 10, height - 15,
		int2str(score_left) + ":" + int2str(score_right));

	// swap buffers (has to be done at the end)
	glutSwapBuffers();
}

void updatePong(int value) {
	// input handling
	keyboard();

	// update ball
	updateBall();

	// Call update() again in 'interval' milliseconds
	glutTimerFunc(interval, updatePong, 0);

	// Redisplay frame
	glutPostRedisplay();
}

void vec2_norm(float& x, float& y) {
	// sets a vectors length to 1 (which means that x + y == 1)
	float length = sqrt((x * x) + (y * y));
	if (length != 0.0f) {
		length = 1.0f / length;
		x *= length;
		y *= length;
	}
}

void updateBall() {
	// fly a bit
	ball_pos_x += ball_dir_x * ball_speed;
	ball_pos_y += ball_dir_y * ball_speed;

	// hit by left racket?
	if (ball_pos_x < racket_left_x + racket_width &&
		ball_pos_x > racket_left_x &&
		ball_pos_y < racket_left_y + racket_height &&
		ball_pos_y > racket_left_y) {
		// set fly direction depending on where it hit the racket
		// (t is 0.5 if hit at top, 0 at center, -0.5 at bottom)
		float t = ((ball_pos_y - racket_left_y) / racket_height) - 0.5f;
		ball_dir_x = fabs(ball_dir_x); // force it to be positive
		ball_dir_y = t;
	}

	// hit by right racket?
	if (ball_pos_x > racket_right_x &&
		ball_pos_x < racket_right_x + racket_width &&
		ball_pos_y < racket_right_y + racket_height &&
		ball_pos_y > racket_right_y) {
		// set fly direction depending on where it hit the racket
		// (t is 0.5 if hit at top, 0 at center, -0.5 at bottom)
		float t = ((ball_pos_y - racket_right_y) / racket_height) - 0.5f;
		ball_dir_x = -fabs(ball_dir_x); // force it to be negative
		ball_dir_y = t;
	}

	// hit left wall?
	if (ball_pos_x < 0) {
		++score_right;
		ball_pos_x = width / 2;
		ball_pos_y = height / 2;
		ball_dir_x = fabs(ball_dir_x); // force it to be positive
		ball_dir_y = 0;
	}

	// hit right wall?
	if (ball_pos_x > width) {
		++score_left;
		ball_pos_x = width / 2;
		ball_pos_y = height / 2;
		ball_dir_x = -fabs(ball_dir_x); // force it to be negative
		ball_dir_y = 0;
	}

	// hit top wall?
	if (ball_pos_y > height) {
		ball_dir_y = -fabs(ball_dir_y); // force it to be negative
	}

	// hit bottom wall?
	if (ball_pos_y < 0) {
		ball_dir_y = fabs(ball_dir_y); // force it to be positive
	}

	// make sure that length of dir stays at 1
	vec2_norm(ball_dir_x, ball_dir_y);
}

void keyboard() {
	// left racket
	if (GetAsyncKeyState(VK_W)) racket_left_y += racket_speed;
	if (GetAsyncKeyState(VK_S)) racket_left_y -= racket_speed;

	// right racket
	if (GetAsyncKeyState(VK_UP)) racket_right_y += racket_speed;
	if (GetAsyncKeyState(VK_DOWN)) racket_right_y -= racket_speed;
}

void drawRect(float x, float y, float width, float height) {
	glBegin(GL_QUADS);
	glVertex2f(x, y);
	glVertex2f(x + width, y);
	glVertex2f(x + width, y + height);
	glVertex2f(x, y + height);
	glEnd();
}

void drawText(float x, float y, std::string text) {
	glRasterPos2f(x, y);
	glutBitmapString(GLUT_BITMAP_8_BY_13, (const unsigned char*)text.c_str());
}

std::string int2str(int x) {
	// converts int to string
	std::stringstream ss;
	ss << x;
	return ss.str();
}

void enable2D(int width, int height) {
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0.0f, width, 0.0f, height, 0.0f, 1.0f);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
}

void HangMan() {
	// variable definitions
	string answer = "";
	string rightAnswer = "";
	bool answeredCorrectly = false;
	int randomNum = 0;

	// generate random number to feed into pickQuestion() function and the correctAnswer() function
	randomNum = 1 + (rand() % 4);

	// Assign function to a variable
	rightAnswer = correctAnswer(randomNum); // get the correct answer to the question as per the random number

	showMenuHM(); // show the main menu

	for (int i = 0; i < 3; i++)
	{
		cout << "\nQUESTION: " << pickQuestion(randomNum); // pick the question based on the random number generated
		cin >> answer;

		if (answer == rightAnswer)
		{
			answeredCorrectly = true; // flagged the user has correctly answered the question
			break;
		}
		else
		{
			cout << "Sorry, try again";
		}
	}

	if (answeredCorrectly)
	{
		cout << "\nYour guess is correct\n";
		drawWalkMan();
	}
	else
	{
		cout << "\nYour guess is wrong\n";
		drawInvertedHangMan();
	}
}

void drawHangMan()
/****************************
 * Purpose: Draws the hangman
 ****************************/
{
	cout << "  +----+\n";
	cout << "  |    |\n";
	cout << "  |    o\n";
	cout << "  |   \\|/\n";
	cout << "  |    |\n";
	cout << "  |   / \\\n";

	for (int i = 0; i < 2; i++)
	{
		cout << "  |\n";
	}
	for (int j = 0; j < 5; j++)
	{
		cout << "-";
	}
	cout << "\n";
}

void drawInvertedHangMan()
/**************************************
 * Purpose: Draws the inverted hangman
 *************************************/

{
	cout << "  +----+\n";
	cout << "  |    |\n";
	cout << "  |   /|\\\n";
	cout << "  |    |\n";
	cout << "  |   /|\\\n";
	cout << "  |    o\n";

	for (int i = 0; i < 2; i++)
	{
		cout << "  |\n";
	}
	for (int j = 0; j < 5; j++)
	{
		cout << "-";
	}
	cout << "\n";
}

void drawWalkMan()
/********************************
 * Purpose: Draws the walking man
 ********************************/

{
	cout << " o\n";
	cout << "\\|/\n";
	cout << " |\n";
	cout << "/ \\\n";
	cout << "\n";
}

string pickQuestion(int questionNumber)
/***********************************************************************************************
 * Purpose: Picks the question based on the passed parameter that serves as the question number
 ***********************************************************************************************/

{
	string question = "";
	switch (questionNumber)
	{
	case 1:
		question = "Guess the missing letter in Yo_bee?: ";
		break;
	case 2:
		question = "How many 's' are there in the word Mi******ppi (a state in the Southeastern region of the United States): ";
		break;
	case 3:
		question = "It's cold in winter, but in spring the ______________ is beautiful.: ";
		break;
	case 4:
		question = "Why are you crying? What ______________ ? ";
		break;
	default:
		question = "";
	}
	return question;
}

string correctAnswer(int questionNumber)
/**********************************************************************
 * Purpose: Returns the correct answer for a particular question number
 **********************************************************************/

{
	string answer = "";
	switch (questionNumber)
	{
	case 1:
		answer = "o";
		break;
	case 2:
		answer = "4";
		break;
	case 3:
		answer = "weather";
		break;
	case 4:
		answer = "happened";
		break;
	default:
		answer = "";
	}

	return answer;
}

void showMenuHM()
/*********************************************************************************************************
 * Purpose: Shows the welcome message and displays the hangman. This is the initial screen of the program.
 ********************************************************************************************************/

{
	string answer = "";
	cout << "==================================================================\n";
	cout << "Welcome to play hangman. This game is to check your spelling skill\n";
	cout << "==================================================================\n";
	drawHangMan();
}