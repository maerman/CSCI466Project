HOW TO USE THE LOGIN MENU

The login interface will display when the user runs the system.

To login with an existing account enter username and password in the input fields
titled "Username" and "Password", then press the Login button to activate the login method
"UserLogin" in the Login script that gets and validates the user's credentials.

To create an account press the Create Account button that opens a news interface
in which the user can submit their new account information with the CreateAccount method
in the Login script that ckecks the validity of the user's inforation and records a new
account to the database.

To play without an account, press the PLay Demo button.

The user can close the program by pressing the Quit button


HOW TO USE THE MAIN MENU

To begin a game the user must press the "new game" button which will activate the NewGame
method that will set the gamestate to the new game interface.

To load a game from a save file the user must press the "load game" button which will activate the LoadGame
method that will set the gamestate to the load game interface, displaying the save files.

To view a saved replay the user must press the "watch replay" button which will activate the WatchReplay
method that will set the gamestate to the load replay interface, displaying the saved replays.

To view the leaderboards the user must press the "leaderboards" button which will activate the Leadboard
method that open the game's website displaying the leaderboards.

To change the game's settings the user must press the "options" button which will activate the Options
method that will set the gamestate to the options menu interface.

To read about the game the user must press the "about" button which will activate the About
method that will set the gamestate to the about interface.

To close the game the user must press the "quit" button.


HOW TO USE THE NEW GAME MENU

To start a game the user must select a toggle from each row. The first row sets the number
of players and whether the players can compete or the players will cooperate.
The second row sets the game type that the player(s) will be loaded into.
The third row sets the difficulty of the gameplay.

Once a toggle from each row has been selected, the user can start the game by pressing the
"start" button.

The user can go back to the main menu interface by pressing the "back" button.


HOW TO USE PAUSE MENU

To use the pause menu, the user must be in a level gamestate whereby they press the 
escape key to activate the pause interface which is handled by Unity's input manager.

To unpause, press the "Resume" button where the "Unpause" method in the
PauseGame script is activated that sets the current gamestate back to the previous gamestate.

Options button will activate the "Options" method in the PauseGame script which will set 
the gamestate to the options menu interface(In Progress).

The Exit button will activate the "Exit" method in the PauseGame script which will set 
the gamestate to the main menu interface.


HOW TO USE GAMEOVER MENU

Gameover menu will display when player's health reaches zero.

To restart the level, press the "restart" button that will activate 
the "Restart" method in the GameOver script.

To save the replay of the level, type a name into the input field labeled "Save Name"
then press the Save Replay button that will activate 
the "saveGame" method in the GameOver script and will save the recording to the hardrive.

The Quit button will activate the "Exit" method in the Gameover script which will set 
the gamestate to the main menu interface.


HOW TO USE THE LEVELCOMPLETE MENU

To access the levelcomplete menu, the user must complete the level's objective in which
an interface will display telling them the level is over.

To go to the next level, press the Continue button which activates the "continue" method
in the LevelComplete script that loads the next level.

To save the game, type a name into the input field labeled "Save Name"
then press the save game button which activates the "saveGame" method in the LevelComplete script
that save the named file to the hardrive. 

To add the score achieved in the level press the Add Score to Leaderboard button to activate
the "addToLeaderboard" method in the LevelComplete script that records the score to the
database.

To save the replay of the level, type a name into the input field labeled "Save Name"
then press the Save Replay button that will activate 
the "saveReplay" method in the LevelComplete script and will save the recording to the hardrive.

The Exit button will activate the "quit" method in the LevelComplete script which will set 
the gamestate to the main menu interface.

