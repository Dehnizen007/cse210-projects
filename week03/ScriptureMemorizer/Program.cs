using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, 16);

        Scripture scripture = new Scripture(
            reference,
            "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"
        );

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden.");
                break;
            }

            Console.WriteLine("Press Enter to continue or type 'quit' to finish:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            int wordsToHide = scripture.RandomWordsToHide();
            scripture.HideRandomWords(wordsToHide);
        }
    }
}

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;

        _words = text
            .Split(' ')
            .Select(word => new Word(word))
            .ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        List<Word> visibleWords = _words
            .Where(word => !word.IsHidden())
            .ToList();

        for (int i = 0; i < numberToHide && visibleWords.Count > 0; i++)
        {
            int randomIndex = _random.Next(visibleWords.Count);

            visibleWords[randomIndex].Hide();

            visibleWords.RemoveAt(randomIndex);
        }
    }

    public int RandomWordsToHide()
    {
        return _random.Next(1, 6);
    }

    public string GetDisplayText()
    {
        string result = _reference.GetDisplayText() + Environment.NewLine;

        foreach (Word word in _words)
        {
            result += word.GetDisplayText() + " ";
        }

        return result.Trim();
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }
}

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_verse == _endVerse)
        {
            return $"{_book} {_chapter}:{_verse}";
        }

        return $"{_book} {_chapter}:{_verse}-{_endVerse}";
    }
}

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);
        }

        return _text;
    }
}