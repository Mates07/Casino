using System.Data.SqlTypes;

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
        odpoved.Trim();
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
        Menu2(money);
    }

    public void Menu2(int money)
    {
        Console.WriteLine("Co za hru si přeješ hrát, BlackJack nebo Ruletu");
        string odpoved = Console.ReadLine();
        if (odpoved == "BlackJack")
        {
            BlackJack(money);
        }
        else
        {
            Ruleta(money);
        }
    }

    public void BlackJack(int money)
    {
        while (true)
        {
            Console.WriteLine("Zadejte sázku (minimum 1$ maximum 1000$), pokud chcete hru ukončit zadejte end, nebo pokud chcete hru změnit zadejte change");
            string odpoved = Console.ReadLine();
            odpoved.Trim();
            if (odpoved == "end")
            {
                Console.WriteLine("Hra se ukončuje");
                break;
            }
            else if (odpoved == "money")
            {
                Console.WriteLine($"Váš zůstatek na účtu je {money}$");
            }
            else if (odpoved == "change")
            {
                Menu2(money);
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
                odpoved.Trim();
                if (soucet > 21)
                {
                    Console.WriteLine("Prohrál jsi překročil jsi maximální počet bodů");
                    money -= sazka;
                    break;
                }
                
                if (odpoved == "ano") 
                {
                    i--;
                }
                else
                {
                    money = NpcBlackJack(money, sazka, soucet);
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

                else if (NpcSoucet == soucet)
                {
                    if (NpcSoucet >= 15)
                    {
                        Console.WriteLine("Máme stejně je to tedy nerozhodné, vracím ti peníze, které jsi vsadil.");
                        Console.WriteLine($"Na účtu máš tedy {money}$");
                    }
                    else
                    {
                        i--;
                    }
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

    public void Ruleta(int money)
    {
        while (true)
        {
            Console.WriteLine("Dobrá pojďme hrát ruletu");
            Console.WriteLine("Kolik si přeješ vsadit (minimu 1$ a maximum 1000$) pro zobrazení zůstatku zadej money a pokud chceš hrát zadej end, nebo pokud chceš hru změnit zadej change");
            string odpoved = Console.ReadLine();
            odpoved.Trim();
            if ( odpoved == "money")
            {
                Console.WriteLine($"Na účtu máš {money}$");
            }
            else if ( odpoved == "change") 
            {
                Menu2(money);
            }
            else if (odpoved == "end")
            {
                break;
            }
            else
            {
                int sazka = SafelyConvertToInt(odpoved);
                if (sazka > money || sazka > 1000 || sazka < 1)
                {
                    Console.WriteLine("Neplatná sázka");
                }
                else
                {
                    Ruleta2(money, sazka);
                }
            }

        }
        
    }
    public int Ruleta2(int money, int sazka)
    {
        Random random = new Random();
        Console.WriteLine("Vyber na co chceš vsadit (čísla 1 - 36, skupiny 1 - 3, barvy)");
        string odpoved = Console.ReadLine();
        odpoved.Trim();
        if ( odpoved == "čísla")
        {
            Console.WriteLine("Na jaké číslo si přejete vsadit 1 - 36");
            odpoved = Console.ReadLine();
            int cislo = random.Next(0, 37);
            int input = SafelyConvertToInt(odpoved);
            if (cislo == input)
            {
                Console.WriteLine("Vyhrál jsi 4 násobek své sázky gratuluji");
                sazka *= 4;
                money += sazka;
            }
            else
            {
                Console.WriteLine($"Prohrál jsi padlo číslo {cislo}");
            }
        }
        else if ( odpoved == "skupiny")
        {
            Console.WriteLine("Na jakou skupinu si přejete vsadit 1 - 3");
            int cislo = random.Next(0, 37);
            odpoved = Console.ReadLine();
            int input = SafelyConvertToInt(odpoved);
            if (input == 1)
            {
                if (cislo == 1 || cislo == 4 || cislo == 10 || cislo == 13 || cislo == 16 || cislo == 19 || cislo == 22 || cislo == 25 || cislo == 28 || cislo == 31 || cislo == 34)
                {
                    Console.WriteLine("Gratuluji vyhrál jsi 3 násobek vkladu");
                    sazka *= 3;
                    money += sazka;
                }
                else
                {
                    Console.WriteLine("Bohužel to nevyšlo, třeba příště");
                    money -= sazka;
                }

            }
            else if (input == 2)
            {
                if (cislo == 2 || cislo == 5 || cislo == 8 || cislo == 11 || cislo == 14 || cislo == 17 || cislo == 20 || cislo == 23 || cislo == 26 || cislo == 29 || cislo == 32 || cislo == 35)
                {
                    Console.WriteLine("Gratuluji vyhrál jsi 3 násobek vkladu");
                    sazka *= 3;
                    money += sazka;
                }
                else
                {
                    Console.WriteLine("Bohužel to nevyšlo, třeba příště");
                    money -= sazka;
                }
            }
            else
            {
                if (cislo == 3 || cislo == 6 || cislo == 9 || cislo == 12 || cislo == 15 || cislo == 18 || cislo == 21 || cislo == 24 || cislo == 27 || cislo == 30 || cislo == 33 || cislo == 36)
                {
                    Console.WriteLine("Gratuluji vyhrál jsi 3 násobek vkladu");
                    sazka *= 3;
                    money += sazka;
                }
                else 
                {
                    Console.WriteLine("Bohužel to nevyšlo, třeba příště");
                    money -= sazka;
                }
            }
        }
        else
        {
            Console.WriteLine("Pokud jsi vsadil na nulu jsi opravdová sigma");
            int cislo = random.Next(0, 36);
            if (cislo == 0)
            {
                Console.WriteLine("Velká gratulace právě jsi z 10 násobil svůj vklad");
                sazka *= 10;
                money += sazka;
            }
            else
            {
                Console.WriteLine("Třeba příště");
                money -= sazka;
            }
        }
        return money;
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

}
