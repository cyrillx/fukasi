using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace bookStatist {
    class bookStat {
        List<wordStat> words;
        public bookStat() {
            words = new List<wordStat>();
        }

        public void count(String s, uint a = 1) {
            s = s.ToLower();
            foreach (wordStat ws in words) {
                if (ws.isIn(s)) {
                    ws.count(a);
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

        public wordStat getWSByWord(string s) {
            foreach (wordStat ws in words) {
                if (ws.word.Equals(s)) {
                    // слово найдено списке, возвращаем
                    return ws;
                }
            }
            wordStat WS = new wordStat(s);
            words.Add(WS);
            return WS;
        }

        public static bookStat operator +(bookStat BS0, bookStat BS1) {
            foreach (wordStat WS in BS1.words) {
                string S = WS.word;
                uint en = WS.entries;
                BS0.getWSByWord(S).count(en);
            }


            return BS0;
        }
    }
}
