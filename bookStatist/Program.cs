using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
/* Статистика вхождений слов в тексте (книге)
* wordStat конкретный набор букв
* bookStat список слов wordStat
*/

namespace bookStatist {
    class Program {
        static void Main(string[] args) {
            String src, dst;
            if (args.Length == 0) return;
            src = args[0];

            // создать объект bookStat
            bookStat bs = new bookStat();
            bs.load(args[0]);

            bs.print();

            if (args.Length < 2) return;
            dst = args[1];
            bs.save(dst);
        }

    }

    class bookStat {
        List<wordStat> words;
        public bookStat() {
            words = new List<wordStat>();
        }

        public void count(String s) {
            s = s.ToLower();
            foreach (wordStat ws in words) {
                if (ws.isIn(s)) {
                    ws.count();
                    return;
                }
            }
            wordStat newws = new wordStat(s);
            newws.count();
            words.Add(newws);
        }

        public void print() {
            foreach (wordStat ws in words) {
                String s = String.Format("{0}: {1}", ws.word, ws.entries);
                Console.WriteLine(s);
            }
        }

        public bool load(string src) {
            // открыть текст
            StreamReader streamReader;
            try {
                streamReader = new StreamReader(src);
            }
            catch {
                Console.WriteLine("File does not exist!");
                return false;
            }
            string str = streamReader.ReadToEnd();

            // разделить на слова
            Regex regex = new Regex("[a-zA-Z]+");
            MatchCollection mc = regex.Matches(str);
            List<string> ls = new List<string>();
            Array ar = Array.CreateInstance(typeof(Match), mc.Count);
            mc.CopyTo(ar, 0);
            foreach (Match m in mc) {
                ls.Add(m.Value);
            }
            ls.Sort();

            for (int i = 0; i < ls.Count; i++) {
                count(ls[i].ToString());
            }

            return true;
        }

        public bool save(string dst) {
            StreamWriter streamWriter;
            try {
                streamWriter = new StreamWriter(dst);
            }
            catch {
                return false;
            }

            foreach (wordStat ws in words) {
                string s = String.Format("{0} {1}", ws.word, ws.entries);
                streamWriter.WriteLine(s);
            }
            streamWriter.Close();

            return true;
        }
    }

    public class wordStat {
        public String word;
        public uint entries;
        public wordStat(string s, uint e = 0) {
            word = s;
            entries = e;
        }

        public bool isIn(String s) {
            return s == word;
        }

        public void count() {
            entries++;
        }
    }
}
