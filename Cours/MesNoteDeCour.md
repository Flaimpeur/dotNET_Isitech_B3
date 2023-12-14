# Isitech B3 RPI D - .NET

## Préparation

Avoir installer .NET Core 8.0 ou sup

```bash
dotnet --version // la version la plus recente installer

dotnet --list-sdks //liste des SDK installés
```

## Cours

.NET est un framework de dev cross-platform et open-source conçu par Microsoft

## Mes notes :

L'API d'un objet c'est comment on veut que l'objet intéragisse avec le monde extérieur, avec l'utilisateur

CRUD = creat read update delete


### Tuple :

La fonctionnalité tuples fournit une syntaxe concise pour regrouper plusieurs éléments de données dans une structure de données légère.

exemple :

```cs
(double, int) t1 = (4.5, 3);
Console.WriteLine($"Tuple with elements {t1.Item1} and {t1.Item2}.");
// Output:
// Tuple with elements 4.5 and 3.

(double Sum, int Count) t2 = (4.5, 3);
Console.WriteLine($"Sum of {t2.Count} elements is {t2.Sum}.");
// Output:
// Sum of 3 elements is 4.5.
```

