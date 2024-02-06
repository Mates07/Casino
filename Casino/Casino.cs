namespace Casino;

internal class Casino
{
    public void Menu()
    {
        int moneyA = 500;
        int moneyB = 1000;
        int MoneyC = 1500;
        int money;
        Console.WriteLine("Ahoj, vítej v mém online casinu, vyber svůj vklad A = 500, B = 1000, C = 1500");
        string odpoved = Console.ReadLine();
        if (odpoved == "A")
        {
            money = moneyA;
        }
        else if (odpoved == "B")
        {
            money = moneyB;
        }
        else
        {
            money = MoneyC;
        }

        Console.WriteLine("Co za hru si přeješ hrát, BlackJack nebo BlackJack");
        odpoved = Console.ReadLine();
        if (odpoved == "BlackJack")
        {
            BlackJack(money);
        }
        else
        { 
            BlackJack(money);
        }
    }

    public void BlackJack(int money)
    {
        while (true)
        {
            Console.WriteLine("Zadejte sázku (minimum 1$ maximum 1000$), pokud chcete hru ukončit zadejte end");
            string odpoved = Console.ReadLine();
            if (odpoved == "end")
            {
                Console.WriteLine("Hra se ukončuje");
                break;
            }
            else if (odpoved == "money")
            {
                Console.WriteLine($"Váš zůstatek na účtu je {money}$");
            }
            else
            {
                int sazka = SafelyConvertToInt(odpoved);
                if (sazka <= 0 || sazka > 1000 || sazka > money)
                {
                    Console.WriteLine("Sázka je neplatná");
                }
                else
                {
                    money = BlackJack2(money, sazka);
                }
            }
        }
    }

    public int BlackJack2(int money, int sazka)
    {
        int soucet = 0;
        int cards;
        for (int i = 0; i < 2; i++)
        {
            Card card = new Card();
            cards = card.Vypis();
            soucet += cards;
            if (i == 1)
            {
                Console.WriteLine($"soucet vasich karet je {soucet}, Přejete si vzít další kartu?");
                string odpoved = Console.ReadLine();
                if (soucet > 21)
                {
                    Console.WriteLine("Prohrál jsi překročil jsi maximální počet bodů");
                    money -= sazka;
                    break;
                }
                
                if (odpoved == "ne") 
                { 
                 money = NpcBlackJack(money, sazka, soucet);  
                }
                else
                {
                    i--;
                }
            }
        }
        return money;
    }

   public int NpcBlackJack(int money, int sazka, int soucet)
    {
        int NpcSoucet = 0;
        int cards;
        for (int i = 0; i < 2; i++)
        {
            Card card = new Card();
            cards = card.Vypis();
            NpcSoucet += cards;
            if (i == 1)
            {
                int rozdil = soucet - NpcSoucet;
                if (NpcSoucet > soucet && NpcSoucet < 22)
                {
                    Console.WriteLine($"Mám vyší součet o {NpcSoucet - soucet} body/ů");
                    money -= sazka;
                    Console.WriteLine($"Nový zůstatek účtu je {money}$");
                }
                else if (NpcSoucet < soucet)
                {
                    i--;
                }
                else
                {
                    Console.WriteLine("Vyhrál jsi");
                    money += sazka;
                    Console.WriteLine($"Nový zůstatek na účtu je {money}$");
                }
            }
        }
        return money;
    }

    public void Ruleta()
    {

    }

    public int SafelyConvertToInt(string x)
    {
        if (int.TryParse(x, out int result))
        {
            return result;
        }
        else
        {
            return 0;
        }
    }

    public int Matematika(int x)
    {
        int y = 2 * x;
        return y;
    }

}
