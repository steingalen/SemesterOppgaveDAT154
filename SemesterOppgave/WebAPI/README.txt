For å få WebAPI til å kjøre må man:
  1. Høyreklikke på filen Application.config (Ligger i WebAPI/) Velg exclude from project
  2. Høyreklikk på WEbAPI prosjektet. Velg Set as start up.
  3. Run.
  
For å hente data fra webapi trenger man bare å ha kjørt webserveren 1 gang. 
Deretter kan man cancle den. 
Å velge sitt eget prosjekt som start up og kjøre det. 
WebAPI'et er fortsatt tilgjengelig.

Sette mock data inn i database
  1. Velg Tools -> NuGet-Package-Manager -> PAckage Manager Console.
  2. Velg WebAPI under Default project dropdown
  3. Skriv inn Update-Database 
  4. Klikk enter  
