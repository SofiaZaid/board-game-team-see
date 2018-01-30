# Dokumentation
## Game engine
Vår webapplikation utgår i från vår Game klass. Där ligger all logik som har med spelet att göra. För att strukturera upp vårt projekt har vi valt att separera vår game-logik till en egen solution som heter gameengine. I GameEngine har vi konstruerat spellogiken utifrån hur det faktiska spelet Tre-i-rad fungerar oavsett huruvida det spelas via en digital lösning eller som ett traditionellt brädspel. 

## MailMappen
Denna mapp innehåller en klass: MailService som hanterar all logik som har med ett mailutskick att göra. I klassen finns enbart en metod: SendEmail vilken tar nickname och emailadress som argument. I metoden skapas en SMTP-server som används för att skicka ut mail till den spelare vars tur det är i spelet. Metoden anropas i controllern GameSessionController, i ActionResult-metoden "PlaceMark". Inuti denna metod anropas metoden SendEmail på ett objekt av typen MailService och utifrån att genom en lista tagit fram vilken spelare som för tillfället lägger ut en markör på brädet kan vi säga att nästa spelare i listan är motspelaren (opponentPlayer), motspelaren är då den som det kommer att skickas ut ett mail till (om att det är dennes tur att spela).

## Model View Controller
### Model
I vår Modelmapp har vi skapat två klasser för att hantera affärslogik som inte i diekt mening är kopplad till logiken för spelet och dess regler: "GameSession" och "Player". Klassen GameSession hanterar både spelet (Game) och den övriga logik som krävs för att hålla reda på en specifik GameSession. Dels behövs naturligtvis ett Game-objekt som innehåller logiken för spelet och dess regler, dels behövs en lista som ska innehålla de två spelarna som deltar i spelsessionen, för att kunna identifiera en specifik session behöver vi även ett "GameID". När ett spel skapas/ en spelsession initieras av en spelare (användare) är spelet ej ännu startat, därav finns variabeln "currentState", den initieras till "waiting" för att spelet ej ska kunna startas innan en andra spelare har anslutit sig. I konstruktorn för GameSession tas ett GameID in som parameter, ett Game-objekt instansieras, parameterns värde tilldelas till propertyn "GameID" samt att listobjektet "PlayersInSpecificGame" instansieras. 
För att hantera nödvändiga interaktioner med spelet utifrån ett Sessionsperspektiv skapade vi metoderna: JoinGame, StartGame och GameOver. JoinGame kontrollerar att det finns möjlighet att ansluta till en specifik spelsession, dvs. där finns ännu ej två spelare i sessionen och spelet är i staten "waiting". StartGame kontrollerar att "GameIsFull" har värdet "true", dvs. spelsessionen har två spelare och att spelsessionens state är "waiting", genom metoden ändras värdet på "currentState" ändras till "started"- spelsessionen är då startad. 
GameOver kontrollerar om någon av tre möjliga utfall är sanna: att en pågående spelsession inte längre har två spelare (en person har lämnat sessionen eller har av annan anledning hoppat ur sessionen), spelet har en vinnare eller spelbrädet är fullagt utan att någon har vunnit. 
Klassen Player är en klass bestående av enbart properties: egenskaper som kan knytas till en specifik spelare. Ett spelarobjekt får t.ex. ett specifikt Game-objekt kopplat till sig via propertyn "GameID". 

### View
För att tydliggöra att våra två Views båda är knutna till spelsessioner skapade vi en undermapp med namnet "GameSession". Vårt program har två Views: FirstPage och ShowGameBoard. 
**FirstPage**
Den view som renderar lobbyn för spelet, dvs. den sida där man välkommnas till spelet och kan skapa en ny spelsession eller ur en lista välja en redan skapad spelsession som man 
vill delta i. I "FirstPage" konstruerade vi en helper-metod för att kunna rendera en lista över redan skapade spel som en andra spelare kan ansluta till. Denna helpermetod anropas 
inuti vår responsmetod Html.BeginForm("JoinGame", "GameSession"), en lista över öppna spel kommer då att visas i en drop-down meny. I "FirstPage" definierar vi även att när 
ActionResult-metoden "CreateGame" körs så ska värdena i dem två textfälten där man matar in nickname respektive mailadress för spelare X (spelaren som skapar spelsessionen) sparas 
till ett spelarobjekt. Sparandet av dessa värden sker indirekt, genom interaktionen med controllern och actionresult-metoden "CreateGame".

**ShowGameBoard**
Hanterar GUI:t och användarens interaktion med detta när en spelare väl är delaktig i en spelsession. För att en korrekt representation av spelbrädet ska visas utifrån vilka markörer 
som för tillfället finns placerade på brädet skapades helper-metoden "showMarkAt(int x, int y)". Denna metod interagerar med modellen genom propertyn "SpecificGame" i GameSession-objektet 
som refererar till ett specifikt "Game"-objekt, genom denna property kan vi anropa metoden "GetMarkAt" ur "Game"-klassen och beroende på vilken markör som finns på ett specifikt fält ska 
antingen X, O eller ingenting visas. I responsmetoden "Html.BeginForm("PlaceMark","GameSession", id = Model.GameID)" anropas helper-metoden "GetMarkAt" på varje button-objekt för att korrekt 
markör ska visas för varje knapp/fält vid varje givet tillfälle. Detta html-form interagerar med actionresult-metoden "placemark" för att korrekt markör ska visas på det fält där en spelare
placerat ut en markör. I ShowGameBoard-view hanteras även vad som ska visas på webbsidan när någon av de två spelarna har vunnit, eller ifall 
spelet är över och det ej finns någon vinnare, detta hanteras genom anrop till metoden "GameOver" i "GameSession"-klassen. För att indikera för en spelare att spelet ej är startat, dvs. att 
det ännu ej finns en andra spelare ansluten anropas metoden "GameFull" i "GameSession"-klassen, om denna metod utvärderas till false ska texten "waiting for players..." visas. För att även 
visa en korrekt representation av vems tur det är att spela kontrolleras först ifall metoden "HasWinner" utvärderas till false, i sådana fall kontrolleras det vems tur det är genom att på 
"SpecificGame" anropa metoden "WhoIsWinner". Utifrån vilken av spelarmarkörerna det är som vunnit hämtar vi spelarens NickName ur listan "PlayersInSpecificGame" i "GameSession"-objektet. 
		
		
### Controller 
Vi valde att enbart implementera en enda controller-klass, dels för att förenkla interaktionen mellan olika actionresult-metoder som interagerar med våra två olika views: 
FirstPage och ShowGameBoard, dels för att det var ett begränsat antal metoder som krävdes för att skapa önskade interaktionsmöjligheter med vår applikation. 
**Actionresultmetoder i vår controller:**
####FirstPage
Skapar en referens till ett Playerobjekt: "currentplayer", värdet som finns lagrat i HTTP-sessionsobjektet tilldelas till denna variabel. Metoden kontrollerar ifall värdet i "currentPlayer" 
inte är null och om den specifika spelsessionen i så fall inte är över, ifall spelsessionen är pågående anropas metoden "RedirectToBoard" som hämtar spelarens pågående spel och omdirigerar så 
att detta visas i webbläsaren. Om det ännu ej finns två spelare i den pågående spelsessionen läggs spelet även till i listan över öppna spel, som visas för nästa person som kommer till Firstpage.

####CreateGame
Hanterar requests då någon klickar på knappen "new game" på Firstpage (spelets lobby). Ett spelid skapas och tilldelas som unikt id/nyckel för den individuella spel-
sessionen. För att vi ska kunna spara representationen av spelsessionen läggs nyckeln gameid samt värdet newGame (spelsessionsobjektet) till i dictionaryn "GameSessions". I nästa steg skapas spelarobjektet
och därefter anropas actionresult-metoden "JoinGame" för att ansluta spelaren till spelsessionen och spelaren sparas sedan i sessionens state. 

####JoinGame 
Hanterar requests då någon vill ansluta till en spelsession. Ifall ID:et finns i listan över spelsessioner, dvs. det existerar en session så ska 
spelsessionsinstansen som spelaren kan joina få samma id. Därefter skapas ett "Player"-objekt och dess properties tilldelas. 
Först efter att detta genomförts ansluts den andra spelaren till spelsessionen, spelarobjektet kopplas också till sessionen och vi returnerar den spelplan som hör samman med sessionen
genom anrop till metoden "RedirectToBoard". Om spelsessionen ej existerar sedan innan (id:et finns ej) omdirigeras man istället till spelets lobby- Firstpage (det vill säga: spelsessionen
som man försökte ansluta till fanns ej).

####PlaceMark
Hanterar requests till webservern rörande en av de viktigaste aspekterna i spelet: att som spelare placera ut en av sina markörer på
spelplanen. Som argument tas ett id och en sträng av koordinater. För att veta vilken spelsession det är som en markör ska placeras ut
i tilldelar vi GameSessionen med det id vi får in via metoden till en instans av GameSession-klassen. Ifall spelaren som
försöker placera ut sin markör är nuvarande "currentPlayer" eller om spelet ej är fullt (det ej finns två spelare i den specifika sessionen redan)
omdirigeras man till spelplanen så som den ser ut för tillfället. I annat fall delas strängen "coordinates" upp och metoden "PlaceMark" anropas på
game-objektet som finns i den specifika spelsessionen för att markören ska placeras ut. När en markör har placerats ut ska ett mail skickas till 
nästa spelare och meddela att det nu är dennas tur att spela. 

####RedirectToBoard
Hjälpmetod i controllern som används för att omdirigera en request till det "game" som hör ihop med en specifik spelsession utifrån det id
som metoden får in som argument.



