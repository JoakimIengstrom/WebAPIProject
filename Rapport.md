# Rapport WebAPI del 1

### Är vår inlämning RESTfull eller inte?

Spontant upplever jag att detta APIprojektet strävar efter att vara Rest och RESTfull, men att det finns inte alla delar för att nå dit. 

* Uniform interface – Det är just nu en likformad API där vi egentligen bara använder en URL. 
Vi jobbar genom hela med GET, POSt och Queryparameter som begärnsar vår GET vid behov. 

* Client–server - Som det funkar nu så ser bara klienten olika URLs och detta uppfyller kraven för en Client-Server. 
Sedan går det att diskutera vad som skall visas i den och vad som bör vara dolt. T ex ID diskussionen vi hade, när den bör vara med och inte. 

* Stateless – Datan är inte sekvensiell de kommer som separata anropp, den sparar heller inte ner något. 

* Cacheable – Så som det är byggt ny så cashar vi inget då vi inte har några metorder för det. 
Vi har heller inget behov att göra det för att klara testerna i nuvarande projekt. 

* Layered system – Vårt system jobbar utifrån detta även om på liten skala, det som jag vet är att jag inte kan ändra saker genom en POST utan istället skapa nya. 

* Code on demand (optional) - APIn här kan inte köras och som tur var är ju detta optional. Den uppfyller inte det just nu. 

Det är väldigt svårt att sätta fingret på vad vår API räknas som då den inte har tillräckligt med delar för att kunna uppnå dessa sex sakerna,  
vilket enligt vissa gör att den kallas som men att strukturen pekar mot REST och RESTfull, 
har suttit och läst lite skillnaderna mellan de två och tar med mig att OM jag kan bygga den REST är den lite säkrare. 










