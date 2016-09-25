using System;

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

            if (args.Length == 3) {
                bookStat extender = new bookStat();
                extender.load(args[2]);
                bs = bs + extender;
            }
            bs.save(dst);
        }
    }
}
