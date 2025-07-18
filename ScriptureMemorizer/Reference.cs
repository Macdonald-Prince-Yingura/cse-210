public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    public Reference(string referenceText)
    {
        // Example: Proverbs 3:5-6 or John 3:16
        var parts = referenceText.Split(' ');
        _book = parts[0];
        var verseParts = parts[1].Split(':');
        _chapter = int.Parse(verseParts[0]);

        var verseRange = verseParts[1].Split('-');
        _startVerse = int.Parse(verseRange[0]);
        _endVerse = verseRange.Length > 1 ? int.Parse(verseRange[1]) : _startVerse;
    }

    public override string ToString()
    {
        return _startVerse == _endVerse ?
            $"{_book} {_chapter}:{_startVerse}" :
            $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}
