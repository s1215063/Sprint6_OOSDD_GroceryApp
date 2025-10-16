# GroceryApp sprint5

Gitflow Workflow

Voor dit project word er gebruik gemaakt van de Gitflow methode om de ontwikkeling gestructureerd en overzichtelijk te houden.

Binnen Gitflow worden de volgende branches gebruikt:


## main
Bevat de stabiele, productierijpe code. Alles in deze branch is getest en klaar om in productie te gebruiken.


## develop
De integratiebranch waarin alle nieuwe features samengevoegd worden. Hier staat altijd de laatste werkende ontwikkelversie.


## feature/…
Voor iedere nieuwe user case of functionaliteit wordt een aparte feature branch aangemaakt.

- feature/UC18 → voor de uitwerking van Use Case 18.
- feature/UC19 → voor de uitwerking van Use Case 19.


Zodra een feature klaar is, wordt deze terug samengevoegd in de develop branch.


## release/…
Wanneer een nieuwe versie bijna klaar is, wordt er een release branch aangemaakt vanuit develop. Hierin worden enkel nog documentatie-updates gedaan, zodat de release stabiel wordt.


## hotfix/…
 Voor dringende fouten in de main branch die snel opgelost moeten worden, zonder te wachten op een nieuwe release.



# New features
 
## UC17 Boodschappenlijst in database 
- Boodschappenlijsten worden nu in de database opgeslagen.

## UC18 BoodschappenlijstItems in database.  
- De producten in booschappenlijsten worden nu in de database opgeslagen.  

## UC19 Product in database en nieuw product aanmaken
- Producten worden nu opgeslagen in de database
- Gebruikers met Admin rechten hebben de toeging tot "NewProductView"
- Gebruikers met Admin rechten kunnen nieuwe producten toevoegen aan de database





