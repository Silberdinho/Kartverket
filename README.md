# Kartverket2025



Struktur og oppsett: 
- Del 1: Introduksjon til prosjektet.
- Del 2: Praktisk informasjon før du tester prosjektet. (VIKTIG!)
- Del 3: Steg for steg; Forklaringer av nettsidens funksjoner - Privatbruker. 
- Del 4: Steg for steg; Forklaringer av nettsidens funksjoner - Caseworker. 
- Del 5: Forklaringer av nettsidens database - MariaDb.
- Del 6: Unittesting. 



Del 1, 

Introduksjon -->

Kartverket 2025 er det vi har kalt får vårt prosjekt fra i fjor, som er vårt 3 semester. Vi som IT-studenter fikk som semesteroppgave å kunne utvikle en nettisde for kartverket, hvor prosjektet skulle inneholde en nettside som kna brukes for å motta beskjeder om eventuelle feil i Norge kartet, samtidig som at ansatte som jobber for kartverket skulle være sikker på at disse beskjedene enten var feil eller ikke.

Som en gruppe på fire studentmedlemmer har vi slått oss sammen for å komme med en løsningen for oppgaven vi har fått, hvor disse løsningene bygger kravene fra kartverket og eksamen i IS-202. For å gjøre prosjektet lettere og strukturert, delte vi oss inn i to grupper, hvor gruppe1 arbeider mer med frontpage og design delen av nettisiden, mens gruppe2 jobber med backend som dreier seg om security systemet av nettsiden og generelt det som går ut på databasen.

Vi har klart å utvikle en nettside som er veldig likt til kartvekrets nettiside utseende messig. Gruppa har klart å dekke til kravet som inneholder å lage en side for både kartbrukere og caseworkere. Hvor du som bruker av kartet kan benytte deg din bruker side, mens casewroker har sin egen side.

Når det gjelder innlogging har vi utviklet en innloggingspage hvor det er mulig å kunne lage en "account" inne på nettside, hvor du enten kan registrere deg eller logge inn som en allerede eksisterende bruker. I tillegg til at dersom du ikke får logget deg inn via en eksisterende konto, har du mulighet til å bruke "forgot password" funksjons knappen.

Etter at en kartbruker har loggen seg inn eller registrert seg inn på nettsiden vår, vil du få muligheten til å sende endring i kart eller eventuelle feil. Du har mulighet til å sette nøyaktig posisjonpå kartet for å være mer nøyaktig eller så kan du bare skrive skrive inn adresse. På høyre side øverst vil du også få en menubar hvor du kan se på case logger du har sendt inn.

Designet for vår nettside følger lik design som Kaartverkets orginale nettside, og dette er grunnet for å ha mest lik implementasjon så bruker kan gjenkjenne bedre og blir lettere for deres bruk av nettside. Dette startet vi med tidlig i prosjektet når vi lagde prototype for vår nettside.



Del 2, 

Praktisk informasjon før du tester prosjektet:  -->

WINDOWS:

STEG 1: Cloning av prosjektet gjøres via gruppens public Github Repository: https://github.com/Silberdinho/Kartverket til Visual Studio 2022. 

STEG 2: ÅPNE DOCKER DESKTOP. 

STEG 3: Bygg applikasjonen med docker compose. (Her er det viktig at "tannhjulet" står på docker-compose og at "build"-knappen står på Docker Compose. 

STEG 4: Applikasjonen og databasen skal nå kjøre via docker-compose i Docker desktop på to forskjellige ports.  

STEG 5: Test ut applikasjonen og databasen. Under finner du info med tanke på testing av applikasjonen og databasen. Det vil også være litt informasjon om hva som kan gjøres hvis du skulle oppleve problemer. Håper du får en hyggelig opplevelse :)


MAC: 

STEG 1. Kopier HTTPS link til repositorien fra Github, i dette tilfellet https://github.com/Silberdinho/Kartverket.git. Deretter åpnes Rider og klikk på Get from VCS øverts i høyre hjørne, hvor linken skal limes inn for å klone hele repositorien på Rider og datamaskinen.

STEG 2: ÅPNE DOCKER DESKTOP. 

STEG 3: Bygg applikasjonen med docker compose. (Her er det viktig at "tannhjulet" står på docker-compose og at "build"-knappen står på Docker Compose. 

STEG 4: Applikasjonen og databasen skal nå kjøre via docker-compose i Docker desktop på to forskjellige ports.  

STEG 5: Test ut applikasjonen og databasen. Under finner du info med tanke på testing av applikasjonen og databasen. Det vil også være litt informasjon om hva som kan gjøres hvis du skulle oppleve problemer. Håper du får en hyggelig opplevelse :)


DATABASEINLOGGING:

STEG 1: Gå til Docker Desktop.

STEG 2: Gp til Docker Terminal

STEG 3: SKRIV INN FØLGENDE: docker exec -it (KOPIER INN KONTEINER ID TIL MARIADB_NEW) bash

STEG 4: SKRIV INN FØLGENDE: mariadb -u root -p

STEG 5: SKRIV INN FØLGENDE PASSORD: hei123


STEG 5: BEVEG DEG INNE I DATBASEN. HER ER EKSEMPLER PÅ NOEN NYTTIGE KOMMANDOER:
- show databases; 
- use Kartverket;
- show tables; 
- F.EKS: select * from AreaChanges; (Registrerte kartendringer som er blitt innsendt)
- F.EKS: select * from AspNetUsers; (For å se brukerdata som er registrert på nettsiden)


ANNEN INFO: 
- I noen tilfeller vil det være nødvendig å legge inn Secrets.json. Secrets.json er sendt til proffessor på github og kan legges inn i prosjektet hvis det skulle være nødvendig. 




Del 3, 

Steg for steg; Forklaringer av nettsidens funksjoner - Privatbruker -->

Steg 1) Her er bilde av forsiden til vår nettside, hvor man kommer til enkel og praktis side, hvor noe av det første man ser er en knapp hvor man har mulighet til å kunne melde inn endring i kart. Øverst på sidne til høyre er det en login og register knapp, hvor du kan også logge deg inn i en eksisterende kartbruker/caseworker eller registrere deg som en ny kartbruker.

Steg 2) Når du trykker på "melde inn endringer" knappen vil du komme til denne sidne hvor du velger om du er en privatperson eller ansatt.

Steg 3) Når du som en privatkunde vil logge inn i en eksisterende konto kommer du frem til denne siden, hvor du skriver inn username og passordet ditt. Dersom du ikke har laget en konto vil du kunne få et forslag i grønn skrift under passord, hvor du har mulighet til å registrere som en ny konto. I tillegg kan du endre passordet ditt hvis du har glemt passordet du lagde.

Stef 4) Som en ny bruker må du registrere deg og lage en konto, og da vil du få opp denne registrerings siden opp. Du fyller ut Navn, Etternavn, Brukernavn, E-postadresse, passord og gjenta passord. Husk at det er viktig å legge til en virkende/eksisterende e-postadresse, fordi hvis du glemmer passordet ditt senere

Steg 5) Etter at du har registrert deg ferdig som en bruker vil du få opp en side med et kart hvor til høyre ligger det detalj informasjon om endringene du vil sende inn, for eksempel som breddegrad, engdegrad, eskrivelse, kommunenavn og fylke. Etter at du har fylt ut boksene kan du sende inn endringene.

Steg 6) Etter dette vil du få opp denne grønne boksen, som bekrefter at endringene du har sendt har gått gjennom og blitt sendt inn med meldingen "Your report has been submitted successfully!", deretter kan du trykke på blå knappen under for å navigere deg tilbake til hjemmesiden.

Steg 7) Det er en knapp øverst til venstre hvor det står mine saker, hvor du kan se loggen til sakene du har sendt inn. Du kan for eksempel se på saksbehnadlingsstatus og saksbehandler, og vente til du får svar fra en caseworker om endringer.

Steg 8) Når du skal et nytt password så får du opp dette, hvor du skriver inn en eksisterende e-postadresse, får å kunne få reset confirmation på email.

Steg 9) Nå vil du få opp "Forgot password confirmation", som forteller deg at du skal nå kunne få en melding under som sier sjekk emailen din for å kunne tilbakestille din password.

Steg 10) Slik vil emailen se ut når du får den, også må du trykke på den blåe skriften som står "here" som navigerer deg tilbake til nettsiden.

Steg 11) Når du kommer til denne siden, kan du endre passordet ditt.

Steg 12) Senere vil du få opp dette som en bekreftelse på at du har endret din password, så kan du trykke på logge inn også skrive det nye passordet ditt. Slik fungerer det for både privat brukere og caseworkers når det gjelder endring av password.



Del 4,

Steg for steg; Forklaringer av nettsidens funksjoner - Caseworker -->

Steg 1) Når du vil logge inn som en caseworker, så må du logge inn via ansatt bruker som inneholder Username og password. Du får også tilgang til "forgot password" hvis du glemmer passordet ditt, og da trengs det e-postadresse hvor du får confirmation for å lage et nytt passord.

Steg 2) Forsiden til en caseworker ser nesten helt lik ut som på en Privat bruker sin forside, forskjellen er bare at øverst til høyre ligger det sak oversikt på caseworker forsiden, men ellers er mye av det lik ut.

Steg 3) Inne på Sak Oversikt vil en caseworker kunne få opp alle saker med deres rapportID, endringer, beskrivelse, kommunenavn, fylkenavn, status og handlinger. PÅ handlinger så har en caseworker mulighet til å kunne edit, delete eller finish en sak. Privatbruker vil kunne se oppdateringen i status, dersom du sletter, endrer eller blir ferdig med en sak (dette er som en sporingsstatus for privatkunder for å kunne holde seg oppdaterte med sakene sine).

Steg 4) Når du trykker på Register Area Change vil du som caseworker kunne få muligheten til å endre på de endringene som må endres på kartet etter at du godkjenner en tilfeldig "endring i kart" sak. Når du trykker på save changes vil kartet oppdateres og se riktig ut.



Del 5:

 Forklaringer av nettsidens database - MariaDb -->

Steg 1) Her er databasen vår med MariaDB, hvor du får tabellene i kartverket som i dette tilfelle er 10 rader.

Steg 2) Når du skriver inn "SELECT * FROM AreaChanges;" får du opp area changes i databasen samtidig som du får opp informasjonen.

Steg 3) Når du skriver inn "SELECT * FROM AspNetRoles;" får du opp AspNetRoles databasen med all informasjon, og da vil du kunne se PrivatUsers og Caseworkers.

Steg 4) Når du skriver inn "SELECT * FROM StatusState;" vil du kunne få opp statusen til de ulike sakene, som for eksempel hvor mange saker som for eksempel ikke behandlet eller under behandling.

Steg 5) Når du skriver inn "SELECT * FROM AspNetUserRoles;" får du opp de ulike UserID´ene opp, med dems unike RoleID.



Del 6, 

Unittesting -->

Unitestingen bekrefter for oss at isolerte deler av koden fungerer. I dette prosjektet har vi testet noen av metodene til HomeControlleren. Praktisk informasjon til testing finner du i del 2. 