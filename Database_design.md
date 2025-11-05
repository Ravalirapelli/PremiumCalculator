# Database Design

This document describes the database schema for the Premium Calculator application.

## Overview

The database consists of three main tables to support the premium calculation functionality:
- **Members** - Stores member information and calculated premiums
- **Occupations** - Lists available occupations
- **OccupationRatings** - Defines rating types and their associated factors

---

## Tables

### 1. Members

Stores member information and their calculated premium amounts.

| Column Name | Data Type | Constraints | Description |
|------------|-----------|-------------|-------------|
| `MemberId` | `INT` | PRIMARY KEY, IDENTITY | Unique identifier for each member |
| `Name` | `VARCHAR(100)` | NOT NULL | Member's full name |
| `AgeNextBirthday` | `INT` | NOT NULL | Member's age next birthday |
| `DateOfBirth` | `DATE` | NOT NULL | Member's date of birth |
| `OccupationId` | `INT` | FOREIGN KEY → Occupations | References the member's occupation |
| `DeathSumInsured` | `DECIMAL(18,2)` | NOT NULL | Amount of death cover selected |
| `MonthlyPremium` | `DECIMAL(18,2)` | NOT NULL | Calculated monthly premium amount |

**Relationships:**
- `OccupationId` → `Occupations.OccupationId`

---

### 2. Occupations

Lists all available occupations that members can select.

| Column Name | Data Type | Constraints | Description |
|------------|-----------|-------------|-------------|
| `OccupationId` | `INT` | PRIMARY KEY, IDENTITY | Unique identifier for each occupation |
| `OccupationName` | `VARCHAR(100)` | NOT NULL, UNIQUE | Name of the occupation (e.g., Doctor, Cleaner, Farmer) |
| `RatingId` | `INT` | FOREIGN KEY → OccupationRatings | References the rating type for this occupation |

**Relationships:**
- `RatingId` → `OccupationRatings.RatingId`


---

### 3. OccupationRatings

Defines the different rating types and their associated factors used in premium calculations.

| Column Name | Data Type | Constraints | Description |
|------------|-----------|-------------|-------------|
| `RatingId` | `INT` | PRIMARY KEY, IDENTITY | Unique identifier for each rating type |
| `RatingName` | `VARCHAR(50)` | NOT NULL, UNIQUE | Rating type name (e.g., Professional, White Collar, Light Manual, Heavy Manual) |
| `RatingFactor` | `DECIMAL(10,2)` | NOT NULL | Factor used in premium calculation formula |



## Entity Relationship Diagram (Conceptual)

```
┌─────────────────────┐         ┌──────────────────┐
│      Members        │         │   Occupations    │
├─────────────────────┤         ├──────────────────┤
│ MemberId (PK)       │◄────────│ OccupationId (PK)│
│ Name                │         │ OccupationName   │
│ AgeNextBirthday     │         │ RatingId (FK)     │
│ DateOfBirth         │         └────────┬──────────┘
│ OccupationId (FK)   │                  │
│ DeathSumInsured     │                  │
│ MonthlyPremium      │                  │
└─────────────────────┘                  │
                                         │
                                         ▼
                                ┌──────────────────┐
                                │ OccupationRatings│
                                ├──────────────────┤
                                │ RatingId (PK)    │
                                │ RatingName        │
                                │ RatingFactor      │
                                └──────────────────┘
```

---

## Notes

- All tables use `INT IDENTITY` for primary keys to ensure unique, auto-incrementing values
- Foreign key relationships ensure referential integrity
- `DECIMAL(18,2)` is used for monetary values to ensure precision
- `DECIMAL(10,2)` is sufficient for rating factors as they are typically small decimal values
- The `MonthlyPremium` is stored in the Members table for historical record-keeping
