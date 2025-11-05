# Premium Calculator

This is a simple insurance premium calculator I built using .NET for the backend and Angular for the frontend. You enter some basic info and it calculates your monthly premium based on your occupation and age.

## What it does

You fill out a form with:
- Your name
- Age (next birthday)
- Date of birth (mm/YYYY format)
- Your occupation (pick from a dropdown)
- Death sum insured amount

Then click the "Calculate Premium" button and it spits out your monthly premium. Pretty straightforward.

The calculation uses this formula:
```
Monthly Premium = (Death Cover × Occupation Rating Factor × Age) / 1000 × 12
```

Different jobs have different risk ratings, so a doctor pays less than a farmer (makes sense, right?). Here's what each occupation is rated:

- **Cleaner** - Light Manual (11.50)
- **Doctor** - Professional (1.5) 
- **Author** - White Collar (2.25)
- **Farmer** - Heavy Manual (31.75)
- **Mechanic** - Heavy Manual (31.75)
- **Florist** - Light Manual (11.50)
- **Other** - Heavy Manual (31.75)

## Getting it running

You'll need:
- .NET 8 SDK installed
- Node.js (v18 or newer should work)
- Angular CLI (v17)

### Backend setup

First, get the API running:

```bash
cd PremiumCalculator.API
```

If you haven't already, restore the packages:
```bash
dotnet restore
```

Then run it. I've got a script that works around the OneDrive sync issues (if you're in OneDrive like me):
```powershell
.\run.ps1
```

Or if you prefer to run it directly:
```bash
dotnet run --urls "http://localhost:4112"
```

The API should start on port 4112. Once it's running, you can check out the Swagger docs at `http://localhost:4112/swagger` - pretty useful for testing the endpoints.

**OneDrive issue?** If you get "Access is denied" errors when running `dotnet run`, it's probably OneDrive locking the .exe file. The `run.ps1` script uses `dotnet exec` instead which avoids that problem. Or just run PowerShell as admin, that usually works too.

### Frontend setup

Open a new terminal and:

```bash
cd PremiumCalculator.Frontend
npm install
npm start
```

It'll start on port 4111. The app should open automatically in your browser, or just go to `http://localhost:4111`.

Make sure the backend is running first, otherwise the API calls won't work.

## API stuff

There are two endpoints:

- `GET /api/premium/occupations` - Returns the list of occupations with their ratings
- `POST /api/premium/calculate` - Calculates the premium

Example request:
```json
{
  "name": "John Doe",
  "ageNextBirthday": 35,
  "dateOfBirth": "01/1990",
  "usualOccupation": "Doctor",
  "deathSumInsured": 100000
}
```

Example response:
```json
{
  "monthlyPremium": 43.75,
  "occupationRating": "Professional",
  "occupationRatingFactor": 1.5
}
```

## Notes

- All fields are required - the form won't let you submit until everything's filled in
- The submit button triggers the calculation (no auto-calculation on field changes)
- Form validation checks that age is between 1-120, date format is mm/YYYY, and sum insured is positive
- CORS is set up to allow requests from the Angular app on port 4111

That's about it. If you run into issues, check the TROUBLESHOOTING.md file in the API folder - I wrote that up after dealing with some annoying OneDrive problems.
