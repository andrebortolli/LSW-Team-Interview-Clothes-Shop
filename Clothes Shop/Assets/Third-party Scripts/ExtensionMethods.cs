﻿//Code Source: https://stackoverflow.com/questions/4398270/how-to-split-string-preserving-whole-words/17571171#17571171 by JonLord.
using System;
using System.Linq;

public static class ExtensionMethods
{
    public static string[] Wrap(this string text, int max)
    {
        var charCount = 0;
        var lines = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return lines.GroupBy(w => (charCount += (((charCount % max) + w.Length + 1 >= max)
                        ? max - (charCount % max) : 0) + w.Length + 1) / max)
                    .Select(g => string.Join(" ", g.ToArray()))
                    .ToArray();
    }
}
