﻿namespace DesignPatterns.SOLID
{
    /// <summary>
    /// A single class is responsible for one thing
    /// </summary>
    internal class SingleResponsibility
    {
        private class Journal
        {
            private static int count = 0;
            private readonly List<string> entries = new();
        
            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count; //memento pattern
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

            //If you want to save the Journal, do not implement here
            //This class should be responsible for one thing
        }

        private static class Persistence
        {
            public static void SaveToFile(Journal j, string fiName, bool overwrite = false)
            {
                if (overwrite || !File.Exists(fiName))
                    File.WriteAllText(fiName, j.ToString());
            }
        }

        public static void Demo()
        {
            Journal journal = new Journal();
            journal.AddEntry("This is my first line");
            journal.AddEntry("This is my second line");
            Console.WriteLine(journal);

            //Saved in bin/Debug
            string fiName = "journal.txt";
            Persistence.SaveToFile(journal, fiName, true);
        }
    }
}
