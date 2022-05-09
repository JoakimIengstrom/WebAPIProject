# WebAPI_inlamnignsuppgift1-2

### Beskriv hur du löser att olika Geo Comment versioner behövde föras in i databsen. <br>Hade man kunnat lösa det på något annat sätt? 

Text

---
### Beskriv ett annat sätt du hade kunnat versionera i stället för att använda en query parameter. <br>På vilket sätt hade det varit bättre/sämre för detta projekt?

Vi skulle kunna göra det i route eller header. Om vi använder route i en controller så får den en specifik plats, <br>
vilket gör att den är enklare att hitta. En queryparameter gör att den kan hamna lite vart som i en routing och blir lite lättare att tappa bort.

Om jag istället lägger den i header så kommer den inte att synas i URLn och fungera ganska likt en query. Det finns ju en fördel med det då du kan använda <br>
sakliga namn, men också inte lika tydligt kanske?

---
### Ge exempel och förklaring på när man vill ha berhöighetskontroll <br>för en webbapi och när man inte vill ha det. 

Text

---


