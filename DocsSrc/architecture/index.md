# Dokumentation
* Game engine
Vår webapplikation utgår i från vår Game klass. Där ligger all logik som har med spelet att göra. För att strukturera upp vårt projekt har vi valt att separera vår game-logik till en egen solution som heter gameengine. I GameEngine har vi konstruerat spellogiken utifrån hur det faktiska spelet Tre-i-rad fungerar oavsett huruvida det spelas via en digital lösning eller som ett traditionellt brädspel. 

* MailMappen
Denna mapp innehåller en klass: MailService som hanterar all logik som har med ett mailutskick att göra. I klassen finns enbart en metod: SendEmail vilken tar nickname och emailadress som argument. I metoden skapas en SMTP-server som används för att skicka ut mail till den spelare vars tur det är i spelet. Metoden anropas i controllern GameSessionController, i ActionResult-metoden "PlaceMark". Inuti denna metod anropas metoden SendEmail på ett objekt av typen MailService och utifrån att genom en lista tagit fram vilken spelare som för tillfället lägger ut en markör på brädet kan vi säga att nästa spelare i listan är motspelaren (opponentPlayer), motspelaren är då den som det kommer att skickas ut ett mail till (om att det är dennes tur att spela).

* Model View Controller
	* Model
	I vår Modelmapp har vi skapat två klasser för att hantera affärslogik som inte i diekt mening är kopplad till logiken för spelet och dess regler: "GameSession" och "Player". Klassen GameSession hanterar både spelet (Game) och den övriga logik som krävs för att hålla reda på en specifik GameSession. Dels behövs naturligtvis ett Game-objekt som innehåller logiken för spelet och dess regler, dels behövs en lista som ska innehålla de två spelarna som deltar i spelsessionen, för att kunna identifiera en specifik session behöver vi även ett "GameID". När ett spel skapas/ en spelsession initieras av en spelare (användare) är spelet ej ännu startat, därav finns variabeln "currentState", den initieras till "waiting" för att spelet ej ska kunna startas innan en andra spelare har anslutit sig. I konstruktorn för GameSession tas ett GameID in som parameter, ett Game-objekt instansieras, parameterns värde tilldelas till propertyn "GameID" samt att listobjektet "PlayersInSpecificGame" instansieras. 
För att hantera nödvändiga interaktioner med spelet utifrån ett Sessionsperspektiv skapade vi metoderna: JoinGame, StartGame och GameOver. JoinGame kontrollerar att det finns möjlighet att ansluta till en specifik spelsession, dvs. där finns ännu ej två spelare i sessionen och spelet är i staten "waiting". StartGame kontrollerar att "GameIsFull" har värdet "true", dvs. spelsessionen har två spelare och att spelsessionens state är "waiting", genom metoden ändras värdet på "currentState" ändras till "started"- spelsessionen är då startad. 
GameOver kontrollerar om någon av tre möjliga utfall är sanna: att en pågående spelsession inte längre har två spelare (en person har lämnat sessionen eller har av annan anledning hoppat ur sessionen), spelet har en vinnare eller spelbrädet är fullagt utan att någon har vunnit. 
Klassen Player är en klass bestående av enbart properties: egenskaper som kan knytas till en specifik spelare. Ett spelarobjekt får t.ex. ett specifikt Game-objekt kopplat till sig via propertyn "GameID". 
	
	I webapplikationen utgår vi utifrån två objekt som ligger i models-mappen. Det första objektet fick namnet Gamesession. Gamesession ansvarar för att starta upp ett spel med två player objekt, samt hanterar all logik kring en specifik spel session. Player objektet innehåller data om spelarna.  
	* View
		I vår mapp view har vi views som vi valt att lägga till en game session  mapp för att tydliggöra att de views tillhör vår game session model. Firstpage är vår startsida, showgameborad view som visar spelbrädet. 
		
	* Controller 
Vi har döp vår controller klass till gamesessioncontroller. De
