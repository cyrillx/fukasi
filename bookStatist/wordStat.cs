using System;

namespace bookStatist {
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

        public void count(uint a = 1) {
            entries += a;
        }

    }
}
