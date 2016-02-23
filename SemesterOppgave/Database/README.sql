/*
	PUBLISHE DATABASE
		- Høyreklikk på Database i Solution Explorer
		- Klikk på publish
		- Load profile
		- Velg /SemesterOppgave/Database/DatabaseProfile.publish.xml
		- Klikk publish

	KOBLE TIL DATABASE
		- Velg fra meny View -> Server Explorer
		- Connect to database
		- (Hvis flere forskjellige server valg) Velg Microsoft Sql server
		- Server name = (localdb)\MSSQLLocalDB
		- Under "Select or enter a database" Velg Database 
		- Test connection
		- OK

	LEGGE INN DATA
		- Under server explorer
		- Høyreklikk på enten selve databasen eller tables
		- New Query
		- Lim inn fra InsertMockData.sql fra Scripts mappen
		- Trykk ctrl+shift+e

	NUKE ALL DATA
		- Under server explorer
		- Høyreklikk på enten selve databasen eller tables
		- New Query
		- Lim inn fra NUKEDB.sql fra Scripts mappen
		- Trykk ctrl+shift+e
*/

