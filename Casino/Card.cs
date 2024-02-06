using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino;

internal class Card
{
    private int hodnota;
    Random radnom;
    public Card()
    {
        radnom = new Random();
        hodnota = radnom.Next(1, 12);
    }

    public int Vypis()
    {
        Console.WriteLine(hodnota);
        return hodnota;
    }
}
