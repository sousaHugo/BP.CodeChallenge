namespace BluePrism.TechnicalTest.Services.Processing
{
    internal class WordInfo
    {
        public string Word { get; private set; }
        public int TotalSteps { get; private set; }
        public WordInfo? PreviousWord { get; private set; }

        public WordInfo(string Word, int TotalSteps, WordInfo PreviousWord)
        {
            this.Word = Word;
            this.TotalSteps = TotalSteps;
            this.PreviousWord = PreviousWord;
        }
        public WordInfo(string Word, int TotalSteps)
        {
            this.Word = Word;
            this.TotalSteps = TotalSteps;
        }
    }
}
