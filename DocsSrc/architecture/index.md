# Game Architecture documentation
## Game engine
The web application's logic is placed in the GameEngine in one class called Game. The reason for only having a single class in the GameEngine is because the game in question (i.e. Tic tac toe)
has a rather limited scope, meaning the game setup and the rules aren't that advanced. Also there was no reason to split up the logic in many different classes because all methods in the class are within 
the same responsibility scope; they handle the setup of the game and it's rules. Since our Game class is written so that a Game object is always constructed with a gameboard that consist of 3 x 3 fields 
it would not be possible to use the Game class for another Game with a similar setup but with a different amount of fields on the gameboard, not without some changes in the constructor setup. To make it 
more reusable the constructor could have taken two parameters that would have defined the size of the two-dimensional array. Apart from this, one can argue for the re-usability in the most of the other 
methods in the class, at least for games that are based on a coordinate system. For example the methods that checks for wins on different angles of the board, methods HasWinner, PlaceMark, etc. could 
easily be used for other similar games or with just minor adjustments to the the methods.

## Mail folder
This folder contains two classes: MailService and the interface IMailService. MailService handles all logic that are related to the sending of mail through the webserver. There are two methods in the class:
####SendEmail
Method that takes two strings: nickname and email as parameters. In the method an instance of SMTP-server is created to be used for sending emails to game players when it is their turn to play (during a 
specific game session).
####IsMailOK
Method that takes one string: email as parameter. Contains a regex to check that the email sent in are correctly formated.

Both of the mail methods are used within our controller GameSession, an instance of the MailService class is created and is used within the PlaceMark method to send out an email to the opponent in the game
whenever the current player at a specific state in the game has placed a mark on the gameboard.

## Model View Controller
### Model
In the Model folder we created two classes that handles data and business logic connected to the scenario when two players at a specific time are playing the game, the classes are: GameSession and Player. 
The data and business logic in these classes can't really be considered to be a part of the logic for the game itself and its rules, therefore they were placed in the Model folder, they are more closely
affiliated with the controller and the views.
####GameSession
Connects Game class with the rest of the data and logic that is needed to create and keep track of a specific game. Holds an instance of the Game class with the logic and the rules of the game,
a list that is to contain the two players in the specific game. To be able to connect players to a specific game session a GameID is also needed. To handle the fact that a game needs to have 
two players to get started, the variable "currentState" was created and instantiated to the value "waiting" which is changed to "started" as soon as a second player has joined the game. The 
constructor for GameSession class takes a GameID as parameter, the property GameID is set to the value sent in when creating the object and within the constructor the Game object is also 
instantiated. To handle needed interactions with a specific ongoing game we created the methods: JoinGame, StartGame and GameOver. Each of these methods checks the prerequisites for their 
respective scenario, for example for a player to be able to join a game the game can't yet have two players and the state of the specific game needs to be "waiting". The GameOver method 
needs to keep check of three different scenarios since a game can be over if one player leaves it, if the gameboard is full of marks but no one is the winner or if there is a winner. For a 
game to start the property GameFull needs to have the value true and the current state of the game needs to be "waiting".
####Player
A class consisting of properties only, i.e. attributes that defines a unique player of a game. For example a specific player object needs to hold data on which Game the player is participating in, 
this is accomplished through the property GameID.

### View
To clarify that our three View classes: FirstPage, ShowGameboard and GameNotFound are connected to specific games between two players i.e. gamesessions (not the
httpsessions), we created a folder for them, called GameSession.

####FirstPage
The view class that renders the GUI for the game lobby, i.e. the first page of the game website where players are welcomed and get the opportunity to create new games or through a list of
open games choose a game to participate in. To give an example of how the FirstPage view is structured: it contains a helper-method that interacts with the model and controller through
a list of ongoing GameSessions (games), the method describes that for each of the open games we should put it in a drop-down list where we show the nickname of the player who started the
game, and in the background each post in the list also has a specific GameID connected to it. The helper method is then used within our html code where we declare: Html.BeginForm("JoinGame", 
"GameSession"), after the declaration of the fields that are to take input of the second players name and emailadress we call the helper method to get our list of open games that the player 
can choose to join. In the head of this html/razor block we define that we are using the actionresult method JoinGame to be able to handle the webserver request correctly. The other part of 
this view is for defining the content to show the user when she wants to create a new game: Html.BeginForm("CreateGame", "GameSession"), here we are using the actionresult method CreateGame 
to be able to correctly handle users' requests for creating new games.

####ShowGameBoard
Handles the GUI and response to user interaction with the website when a player is already part of a game (and has a session at the webserver). To be able to show a correct representation of
the gameboard according to which player marks that are currently placed on it, the helper method ShowMarkAt(int x, int y) was created. This method interacts with the model through the property 
SpecificGame within the GameSession object, through this property we can call the Game class method GetMarkAt, that returns the player mark that are to be found on a specific field of the gameboard. 
The helper method is called for every button within our html code Html.BeginForm("PlaceMark", "GameSession", id = Model.GameID) where we also need to provide the GameId of the game the user is 
interacting with. The rest of the ShowGameBoard view is defining what is to be shown on the website if one of the players has won, the game is over because no one has won but there are no longer 
any empty fields on the board. Here we are interacting with the GameSession class by calling methods like GameOver in the GameSession class. To be able to correctly show on the screen which player's 
turn it is to play we use the property PlayersInSpecificGame from the GameSession class, defining which index we want to fetch data from depending on if it's player one's or player two's turn. Also 
to indicate for a player that their isn't yet any opponent in the game the property GameFull in the model is used, if the game hasn't yet got two players the text "Waiting for players..." is to be 
shown to the user.

####GameNotFound
Simple view that only contains one html tag to show the message "This game doesn't exist" and a tag that creates a button that redirects the user back to the game lobby when it's clicked.

### Controller 
We chose to only have one controller class in our web application, partly because it simplifies the interaction between the different actionresult methods that are used for our three views,
partly because the amount of methods needed to create the desired interaction possibilities for our application was limitied. 
**Actionresult methods in the controller:**
####FirstPage
Here we create a reference to a Player object: currentPlayer, the value of this variable is set to the value in the HTTP-session object. This is the step where the individual player gets her session
with the webserver. The method checks if the value in currentPlayer is not null and if the specific game associated with the player is not over, if the game is ongoing the controller method RedicretToBoard
is called, it fetches the players ongoing session and by interacting with the view shows the ongoing game (or the game waiting for a second player) to the user. If there isn't yet two players in the game, 
the game is added to the list of open games, which will be shown to the next user who goes to the FirstPage (game lobby). 

####CreateGame
Defines how to handle requests when a user clicks on the button "new game" on the firstpage of the website (game lobby). A new GameSession object is created, gets a randomized GameID and is added
to the controllers dictionary GameSessions. Also the Player object representing the first player in a game is created and the method JoinGame is called to add the player to the game. To be able to
associate a user/player with a specific web session the player object is then assigned to the httpsession object. To show the created game to the user the method then calls the controller method 
RedirectToBoard that show the gameboard for the specific game.

####JoinGame 
Defines how to handled requests when a user wants to join a specific game. If the whole number (id) sent into the method is not null, the GameSession object for the second player should get this
number as it's GameID. A second player object is created and gets its properties assigned, likewise as in the CreateGame action method, the method JoinGame is called on the GameSession object to
add the second player to the game and the game is added to the dictionary GameSessions. Then the method redirects to the view of the current game's gameboard. If the GameID didn't exist (i.e. the
game doesn't exist, the user is instead re-directed to the lobby page (by "Redicrect("/")" which redirects to the root webpage .

####PlaceMark
Defines how to handle requests to the webserver considering one of the most essential parts of the game: when a player wants to place her mark on the gameboard. Parameters to the method are number (id)
and string (coordinates). To know which game it is that the user is playing in the method references a GameSession object and uses the give id parameter to find the correct GameSession in the GameSessions
dictionary. Then we check if the player trying to make the move in the game is in fact the currentPlayer at the moment, if so the method PlaceMark is called on the property SpecificGame of the GameSession
object to place the mark on the field of the board that has the give X and Y coordinates, otherwise the user is redirected to the gameboard of the current game (no new mark is placed on the gameboard). 
When a mark has been placed on the gameboard we check who the opponent player is and use the MailService class and its SendEmail method to send out an email to the next player telling her that it is her 
turn to play. 

####ShowGameBoard
Handles all requests and the sending of responses concerning the correct representation of the gameboard at any given time during an ongoing game and httpsession. Checks if the id given when calling the method 
is not in the GameSessions Dictionary (i.e. the game doesn't exist).If the game doesn't exist, checks if there is a player who has an ongoing httpsession with the id of the "nonexisting" or broken game and if 
this is the case, removes the player from the httpsession and re-direct the user to the error page GameNotFound. If the id given when calling the method is in fact a key associated with an existing game in the 
GameSession dictionary, the view ShowGameBoard is instead returned to the user through the web browser. As described above many of the other action methods are interacting with this particular action method, 
since many of them needs to show the current updated state of the gameboard after correctly handling user input (at any given time during an ongoing game).

####RedirectToBoard
Method used to redirect request to the gameboard associated with the specific game, according to the id given when calling the method.

####GameNotFound
Simple method only used to return the likewise simple view GameNotFound. It is called within the ShowGameBoard to send the correct response to the browser when a user tries to reach a gameboard for an invalid or
nonexisting game. 


