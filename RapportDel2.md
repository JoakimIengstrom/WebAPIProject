# WebAPI_inlamnignsuppgift1-2

### Beskriv hur du löser att olika Geo Comment versioner behövde föras in i databasen. <br>Hade man kunnat lösa det på något annat sätt? 

Jag låter ju mitt program spara ner värden i databasen baserat på requests där du matar in värde som sparas ner. HÄR finns en stor nackdel i hur jag jobbat, jag kör allt idag genom min model och har inte jobbat med DTOer. Detta gör att min databas idag är utsatt för risken att någon manuellt skickar in information som kan skriva över en användares data. Så helt klart ett annat sätt att jobba med är DTOer som jobbar med hämtning utan att spara ner det till originalet. 

Jag har gjort hälften av det genom att lägga till en body i min model, men har gjort det lätt just nu för att ha en ren och enkel kod. Vill jobba med att utveckla detta framåt, kanske redan i nästa kurs. 

---
### Beskriv ett annat sätt du hade kunnat versionera i stället för att använda en query parameter. <br>På vilket sätt hade det varit bättre/sämre för detta projekt?

Sätten jag kan använda som inte är query parameter är URL och Header. Det finns liknader mellan query och URL. URL är relativt likt, du sätter dess värde i routen och jobbar på ett liknande sätt. Största skillnaden jag hittar är att du kan chasa URLn enkelt och query är lättare att göra en default till senaste versionen. Bortsett från det har ju båda nackdelar. Query är lite svårare att requesta en routing, medans en URL får ett ganska stort "footprint". 

Header är lite annorlunda, du behöver ha en header i varje test och det gör att det blir mer jobb, speciellt i stora APIer. MEN det ger en väldigt ren URL infon du vill ha. 

---
### Ge exempel och förklaring på när man vill ha behörighetskontroll <br>för en webbapi och när man inte vill ha det. 

Det finns ju lite exempel där man vill ha denna kontrollen, och dels är det för att skydda information på olika sätt. Dels att inte bara ge ut allt till någon som inte har reggat sig, du kan styra vad som ses och visas beroende på nivån av "medlemskap" som du har på en säg streamingtjänst. Du kan se till att en vanlig använder bara hittar information om sig själv, medans en högre behörighet som en admin kan jobba med mer data och information. 

---


