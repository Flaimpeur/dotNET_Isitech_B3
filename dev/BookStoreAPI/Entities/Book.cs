namespace BookStoreAPI.Entities
{
    public class Book
    {


        // Une prop mets a dispostion des accesseurs (getters et setters)
        // ceci est une property
        public int Id { get; set; }
        public required string Title { get; init; }
        public string? Author { get; set; }
        public string? Genre {get; set;}

        public string Abstract { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true; // Ajout de la propriété IsAvailable pour savoir si un livre est disponible pour le louer ou non
        // J'ai voulu mettre en place une location mais je n'ai pas réussi dans le temsp impartie


    }
}