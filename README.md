# Loony Bin

There is an entity Patient defined by the following JSON:

```json
{
    "name":
    {
        "id" : "d8ff176f-bd0a-4b8e-b329-871952e32e1f",
        "family": "Smith",
        "given": [ "John", "Henry" ]
    },
    "gender": "male",
    "birthDate": "2024-01-13T18:25:43",
}
```

The mandatory fields are name.family and birthDate.

Gender values are male, female, other, unknown.

Create a REST API application with the following methods:
1. Create
2. Update
3. Soft delete
4. Get by ID
5. Search by birthdate like here: https://www.hl7.org/fhir/search.html#date

Use ASP.NET WebAPI, EntityFramework and any SQL database on your choice with the code-first approach.

Add Docker Compose support.

Keep it simple yet functional (e.g. no need to do complex multi-level logging, but tracking errors in transactions is a good idea).
