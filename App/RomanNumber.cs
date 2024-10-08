﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int Value)
    {
        public static RomanNumber Parse(string input) => RomanNumberParser.Parse(input);

        public override string ToString() {
            Dictionary<int, string> ranges = new()
            {
                {1, "I" },
                {4, "IV" },
                {5, "V" },
                {9, "IX" },
                {10, "X" },
            };
            int n = Value;
            StringBuilder sb = new();
            foreach (int range in ranges.Keys.OrderByDescending(k => k)) {
                while (n >= range) {
                    n -= range;
                    sb.Append(ranges[range]);
                }
            }
            return sb.ToString();
        }

        /*public static RomanNumber Parse(string input) => input switch {
            "II" => new(2),
            "III" => new(3),
            "N" => new(0),
            "I" => new(1),
            "V" => new(5),
            "X" => new(10),
            "L" => new(50),
            "C" => new(100),
            "D" => new(500),
            "1" => throw new ArgumentException(),
            _ => new(1000),
        };*/

        public static int DigitalValue(char digit) => digit switch {
            'N' => 0,
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            'M' => 1000,
            _ => throw new ArgumentException($"'RomanNumber.DigitalValue': argument 'digit' has invalid value '{digit}'")
        };

        /*public static void CheckSequence(string input) {
            List<char> repetableSymbols = new List<char>() { 'I', 'X', 'C', 'M' };
            List<char> unrepeatableSymbols = new List<char>() { 'V', 'L', 'D' };
            int lessCount = 0;
            for (int j = 0; j < input.Length; j++) {
                int position = j;
                int currentValue = DigitalValue(input[j]);
                for (int i = 0; i < position; i++) {
                    if (DigitalValue(input[i]) < currentValue) {
                        lessCount++;
                    }
                    else break;
                    if (lessCount > 1) {
                        throw new FormatException($"RomanNumber.Parse({input}) error: more than one less digit before {input[position]} at position {position}. Invalid input {input}");
                    }
                }
                lessCount = 0;
            }
        }*/

        public RomanNumber Sum(RomanNumber rn) => this with { Value = Value + rn.Value};
    }
}
