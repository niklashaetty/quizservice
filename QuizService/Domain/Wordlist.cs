namespace Domain;

public static class Wordlist
{
    public static string GenerateQuizName()
    {
        var random = new Random();
        return Adjective[random.Next(0, Adjective.Count - 1)] + Adjective[random.Next(0, Adjective.Count - 1)] +
               Nouns[random.Next(0, Nouns.Count - 1)] + Quiz[random.Next(0, Quiz.Count - 1)];
    }

    private static readonly List<string> Quiz = new List<string>()
    {
        "Quiz",
        "Questionnaire",
        "Exam",
        "Test",
        "Party",
        "Query",
        "Check",
        "Trivia",
        "PanelGame",
        "TestOfKnowledge",
        "Different",
        "Competition",
        "Grilling",
    };

    private static readonly List<string> Nouns = new List<string>()
    {
        "Good",
        "New",
        "Fast",
        "Long",
        "Great",
        "Little",
        "Own",
        "Other",
        "Old",
        "Right",
        "Different",
        "Large",
        "Small",
        "Important",
        "Young"
    };

    private static readonly List<string> Adjective = new List<string>()
    {
        "Cautious",
        "Charming",
        "Correct",
        "Clever",
        "Creepy",
        "Determined",
        "Dizzy",
        "Defeated",
        "Difficult",
        "Distinct",
        "Doubtful",
        "Dull",
        "Delightful",
        "Dangerous",
        "Combative",
        "Cloudy",
        "Clean",
        "Brave",
        "Brigh",
        "Brainy",
        "Blushing",
        "Clusmy",
        "Crowded",
        "Condemned",
        "Crazy",
        "Confused",
        "Anxious",
        "Amused",
        "Agreeable",
        "Adventurous",
        "Adorable",
        "Bewildered",
        "Average",
        "Beautiful",
        "Faithful",
        "Fierce",
        "Friendly",
        "Gleaming",
        "Glorious",
        "Nimble",
        "Healthy",
        "Homely",
        "Outstanding",
        "Mysterious",
        "Naughty",
        "Smiling",
        "Sparkling",
        "Successful",
        "Shiny",
        "Splendid",
        "Super",
        "Quaint",
        "Puzzled",
        "Spotless",
        "Vivacious",
        "Weary",
        "Zealous",
    };
}