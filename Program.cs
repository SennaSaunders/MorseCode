using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Morse_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            MorseController mc = new MorseController(true);
            
            
			mc.Run();
			
		}
    }
}

class MorseController
{
	private char exitChar = '*';
	private string input = "";
	private string invalidInputString = "";

	private MorseAlphabet alphabet;
	private List<MorseChar> sentence;

	public MorseController(bool includeNumbers)
	{
		alphabet = new MorseAlphabet(includeNumbers);
	}

	public void Run()
	{
		Console.WriteLine("Hello! Welcome to the Morse Code Demo.");
		while (true)
		{
			HandleInput();
		}
	}

	private void Instructions()
	{
		Console.WriteLine("Enter " + exitChar + " to quit.");
		Console.WriteLine("Enter some words or numbers to hear it repeated back in morse code:");
	}

	private bool GetInput()
	{
		Instructions();
		input = Console.ReadLine();

		if (input.Length > 0)
		{
			return true;
		}
		Console.WriteLine("Invalid input. Try again.");

		return GetInput();
	}

	private void ParseInputString()
	{
		sentence = new List<MorseChar>();
		for (int j = 0; j < input.Length; j++)		//going through the input
		{
			bool validLetter = false;			//defaults to not correct input

			while (validLetter == false)
			{
				for (int i = 0; i < alphabet.getAlphabet().Count; i++)
				{
					if (input[j].ToString().ToUpper().Equals(alphabet.getAlphabet()[i].alphaNumChar.ToString()) || input[j].ToString().Equals(" "))
					{
						validLetter = true;
						if (input[j].ToString().Equals(" "))
						{
							sentence.Add(null); //adding spaces to the sentence for pauses	
						}
						else
						{
							sentence.Add(alphabet.getAlphabet()[i]);	//adding valid letters so the morse code for characters can be easily accessed later
						}
						
						break;
					}
				}

				if (!validLetter)
				{
					invalidInputString += input[j];
					input = input.Remove(j, 1);
					j--;
					break;
				}
			}
		}
	}

	private void BeepSentence()
	{
		int oneTimeUnit = 150;
		int frequency = 1000;
		//for each character in string
		Console.Write("Morse Code: ");

		foreach (var c in sentence)
		{
			int waitTime;
			Stopwatch time;
			if (c != null)
			{
				waitTime = oneTimeUnit * (int)TimeUnits.Long;
				foreach (var morseBeep in c.morseCodeChar)
				{
					Console.Write(morseBeep.symbol);
					Console.Beep(frequency, oneTimeUnit * (int)morseBeep.duration);
					
					time = Stopwatch.StartNew();
					while (true) { if (time.ElapsedMilliseconds >= waitTime) { break; } }
				}
			}
			else
			{
				Console.Write("   ");
				waitTime = oneTimeUnit * (int)TimeUnits.Break;
				time = Stopwatch.StartNew();
				while (true) { if (time.ElapsedMilliseconds >= waitTime) { break; } }
			}
			Console.Write(" ");
		}
		Console.Write("\n\n");
	}

	private bool HandleInput()
	{
		GetInput();
		if (input.StartsWith(exitChar))
		{
			Environment.Exit(0);
		}
		else
		{
			ParseInputString();
		}

		Console.WriteLine("Valid Input: " + input);
		Console.WriteLine("Invalid Input: " + invalidInputString);
		BeepSentence();

		return true;
	}
}


class MorseAlphabet
{
	public MorseAlphabet(bool includeNumbers)
	{
		letters = getLetters();
		alphabet = letters;
		if (includeNumbers)
		{
			numbers = getNumbers();
			alphabet.AddRange(numbers);
		}
	}

	Dit dit = new Dit();
	Dah dah = new Dah();

	private List<MorseChar> letters;
	private List<MorseChar> numbers;
	private List<MorseChar> alphabet;

	public List<MorseChar> getAlphabet() { return alphabet; }

	private List<MorseChar> getLetters()
	{
		List<MorseChar> letters = new List<MorseChar>();
		letters.Add(new MorseChar('A', new List<MorseBeep>() {dit, dah}));				//  .-
		letters.Add(new MorseChar('B', new List<MorseBeep>() {dah, dit, dit, dit}));		//  -...
		letters.Add(new MorseChar('C', new List<MorseBeep>() {dah, dit, dah, dit}));		//  -.-.
		letters.Add(new MorseChar('D', new List<MorseBeep>() {dah, dit, dit}));			//  -..
		letters.Add(new MorseChar('E', new List<MorseBeep>() {dit}));						//  .
		letters.Add(new MorseChar('F', new List<MorseBeep>() {dit, dit, dah, dit}));		//  ..-.
		letters.Add(new MorseChar('G', new List<MorseBeep>() {dah, dah, dit}));			//  --.
		letters.Add(new MorseChar('H', new List<MorseBeep>() {dit, dit, dit, dit}));		//  ....
		letters.Add(new MorseChar('I', new List<MorseBeep>() {dit, dit}));				//  ..
		letters.Add(new MorseChar('J', new List<MorseBeep>() {dit, dah, dah, dah}));		//  .---
		letters.Add(new MorseChar('K', new List<MorseBeep>() {dah, dit, dah}));			//  -.-  
		letters.Add(new MorseChar('L', new List<MorseBeep>() {dit, dah, dit, dit}));		//  .-..
		letters.Add(new MorseChar('M', new List<MorseBeep>() {dah, dah}));				//	--
		letters.Add(new MorseChar('N', new List<MorseBeep>() {dah, dit}));				//	-.
		letters.Add(new MorseChar('O', new List<MorseBeep>() {dah, dah, dah}));			//	---
		letters.Add(new MorseChar('P', new List<MorseBeep>() {dit, dah, dah, dit}));		//	.--.
		letters.Add(new MorseChar('Q', new List<MorseBeep>() {dah, dah, dit, dah}));		//	--.-
		letters.Add(new MorseChar('R', new List<MorseBeep>() {dit, dah, dit}));			//	.-.
		letters.Add(new MorseChar('S', new List<MorseBeep>() {dit, dit, dit}));			//	...
		letters.Add(new MorseChar('T', new List<MorseBeep>() {dah}));						//	-
		letters.Add(new MorseChar('U', new List<MorseBeep>() {dit, dit, dah}));			//	../
		letters.Add(new MorseChar('V', new List<MorseBeep>() {dit, dit, dit, dah}));		//	...-
		letters.Add(new MorseChar('W', new List<MorseBeep>() {dit, dah, dah}));			//	.--
		letters.Add(new MorseChar('X', new List<MorseBeep>() {dah, dit, dit, dah}));		//	-..-
		letters.Add(new MorseChar('Y', new List<MorseBeep>() {dah, dit, dah, dah}));		//	-.--
		letters.Add(new MorseChar('Z', new List<MorseBeep>() {dah, dah, dit, dit}));		//	--..

		return letters;
	}

	private List<MorseChar> getNumbers()
	{
		List<MorseChar> numbers = new List<MorseChar>();

		numbers.Add(new MorseChar('1', new List<MorseBeep>() { dit, dah, dah, dah, dah}));
		numbers.Add(new MorseChar('2', new List<MorseBeep>() { dit, dit, dah, dah, dah }));
		numbers.Add(new MorseChar('3', new List<MorseBeep>() { dit, dit, dit, dah, dah }));
		numbers.Add(new MorseChar('4', new List<MorseBeep>() { dit, dit, dit, dit, dah }));
		numbers.Add(new MorseChar('5', new List<MorseBeep>() { dit, dit, dit, dit, dit }));
		numbers.Add(new MorseChar('6', new List<MorseBeep>() { dah, dit, dit, dit, dit }));
		numbers.Add(new MorseChar('7', new List<MorseBeep>() { dah, dah, dit, dit, dit }));
		numbers.Add(new MorseChar('8', new List<MorseBeep>() { dah, dah, dah, dit, dit }));
		numbers.Add(new MorseChar('9', new List<MorseBeep>() { dah, dah, dah, dah, dit }));
		numbers.Add(new MorseChar('0', new List<MorseBeep>() { dah, dah, dah, dah, dah }));

		return numbers;
	}
}

class MorseChar
{
	public MorseChar(char alphaNumChar, List<MorseBeep> morseCodeChar)
	{
		this.alphaNumChar = alphaNumChar;
		this.morseCodeChar = morseCodeChar;
	}

	public List<MorseBeep> morseCodeChar;
	public char alphaNumChar;
}

enum TimeUnits
{
    Short = 1,
    Long = 3,
    Break = 7
}

class MorseBeep
{
    public TimeUnits duration;
    public char symbol;

    public MorseBeep(TimeUnits duration, char symbol)
    {
        this.duration = duration;
        this.symbol = symbol;
    }
}

class Dit : MorseBeep
{
    public Dit() : base(TimeUnits.Short, '.') { }
}

class Dah : MorseBeep
{
    public Dah() : base(TimeUnits.Long, '-') { }
}
