
# Overlapssystem

Vi har udviklet et overlapssystem til et bosted, der sikrer effektiv overlevering af beboerinformation mellem medarbejdere ved vagtskifte.
Systemet gør det muligt at registrere, opdatere og dele information om beboere, medicin, opgaver og hændelser på en struktureret måde.
Systemet er bygget som en client-server løsning med en Blazor frontend, et REST API og en lagdelt backend-arkitektur (Clean Structure) bestående af Application-, Shared-, Domain- og Infrastructure-lag. Systemet anvender JWT-baseret autentificering og rollebaseret adgangskontrol.

Lavet af: Ninna Lundberg, Mikkel Simon Ølting Jensen, Melinda Søholt Allen og Mia Engstrøm Madsen (Team 5) 




## Installation

Install my-project with npm

```bash
  npm install my-project
  cd my-project
```
    
## Usage/Examples

```javascript
import Component from 'my-project'

function App() {
  return <Component />
}
```
## Seed brugere
var users = new[]
{
    new { UserName = "Slot", Password = "Slot123@", FirstName = "Slot", LastName = "Slottet", DepartmentId = 1, Role = "Simpel" },
    new { UserName = "Skov", Password = "Skov123@", FirstName = "Skov", LastName = "Skoven", DepartmentId = 2, Role = "Simpel" },
    new { UserName = "Medarbejder1", Password = "Test123@", FirstName = "Afdeling1", LastName = "Afdeling1", DepartmentId = 1, Role = "Medarbejder" },
    new { UserName = "Medarbejder2", Password = "Test123@", FirstName = "Afdeling2", LastName = "Afdeling2", DepartmentId = 2, Role = "Medarbejder" },
    new { UserName = "Admin1", Password = "Test123@", FirstName = "Admin1", LastName = "Admin1", DepartmentId = 1, Role = "Administrator" },
    new { UserName = "Admin2", Password = "Test123@", FirstName = "Admin2", LastName = "Admin2", DepartmentId = 2, Role = "Administrator" },
};


## Features

- Different Departments
- Resident Cards
- Resident Status
- Resident Mood
- Resident Risk Level
- Resident Activities
- Resident Familie Note
- Resident Employee
- Department Tasks
- PN Medicine
- Medicine Time
- Special Events
- Shopping
- Employee Phones
- Autherization & Authentication
- Audit Trail
- 


## Authors

- [@octokatherine](https://www.github.com/octokatherine)


## Usage/Examples

```javascript
import Component from 'my-project'

function App() {
  return <Component />
}
```

