# Dokumentation
* Game engine
Vår webapplikation utgår i från vår Game klass. Där ligger all logik som har med spelet att göra. För att strukturera upp vårt projekt har vi valt att separera vår game-logik till en egen solution som heter gameengine. 

* MailMappen
innehåller en klass som hanterar all logik som har med ett mailutskick att göra.


* Model View Controller
	* Model
	I själva webapplikationen utgår vi utifrån två objekt som ligger i models-mappen. Det första objektet fick namnet Gamesession. Gamesession ansvarar för att starta upp ett spel med två player objekt, samt hanterar all logik kring en specifik spel session. Player objektet innehåller data om spelarna.  
	* View
		I vår mapp view har vi views som vi valt att lägga till en game session  mapp för att tydliggöra att de views tillhör vår game session model. Firstpage är vår startsida, showgameborad view som visar spelbrädet. 
		
	* Controller 
Vi har döp vår controller klass till gamesessioncontroller. De
