
# Overlapssystem

Vi har udviklet et overlapssystem til et bosted, der sikrer effektiv overlevering af beboerinformation mellem medarbejdere ved vagtskifte.
Systemet gør det muligt at registrere, opdatere og dele information om beboere, medicin, opgaver og hændelser på en struktureret måde.
Systemet er bygget som en client-server løsning med en Blazor frontend, et REST API og en lagdelt backend-arkitektur (Clean Structure) bestående af Application-, Shared-, Domain- og Infrastructure-lag. Systemet anvender JWT-baseret autentificering og rollebaseret adgangskontrol.

Lavet af: Ninna Lundberg, Mikkel Simon Ølting Jensen, Melinda Søholt Allen og Mia Engstrøm Madsen (Team 5) 


    
## Opsætningsvejledning

1. Vi henviser til SQL-filen (der indeholder vores fulde SQL-script) i dette repository, hvis I vil prøve at køre systemet fra egen computer og database.

2. Tilføj dit eget servernavn til connectionstring der ligger i appsettings.json i Blazor og API

3. Seedede brugerroller til test: 

Brugerrolle simpel:
UserName: Slot
PassWord: Slot123@

Brugerrolle medarbejder:
UserName: Medarbejder1
Password: Test123@

Brugerrolle Administrator:
UserName: Admin1
Password: Test123@





