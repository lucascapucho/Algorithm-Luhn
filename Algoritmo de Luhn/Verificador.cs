using System;

/*
 * Solução: verificador de cartão de crédito
 * Made by: Lucas Capucho de Araujo
 * Sem TDD 
 * O algoritmo abaixo recebe nº cartões e o mesmo utiliza o algoritmo de Luhn para verificar a válidade dos cartões
 */

namespace Algoritmo_de_Luhn
{
    class Verificador
    {
        static void Main(string[] args)
        {
            long[] creditCards;
            Console.WriteLine("----Algoritmo de Luhn----\n");
            Console.WriteLine("Insira a quantidade de cartões que serão verificados: ");
            try
            {
                creditCards = new long[Convert.ToInt16(Console.ReadLine())];
            }
            catch (Exception)
            {
                creditCards = new long[1];
            }

            Console.WriteLine("\nEntradas " + creditCards.Length + ": ");
            for (int i = 0; i < creditCards.Length; i++)
            {
                try
                {
                    creditCards[i] = Convert.ToInt64(Console.ReadLine().Replace(" ", "").Replace("-", ""));
                }
                catch (Exception)
                { }
            }

            Console.WriteLine("\nSaídas: ");
            for (int i = 0; i < creditCards.Length; i++)
            {
                ValidateCard(creditCards[i]);

            }
            Console.Read();
        }

        private static void ValidateCard(long creditCard)
        {
            char[] reversedCard = ReverseInt(creditCard).ToString().ToCharArray();
            int[] newCard = new int[reversedCard.Length]; //Cartão com os digitos alterados
            int total = 0;
            double aux = 0;

            for (int i = 1; i < reversedCard.Length; i += 2)
            {
                aux = Char.GetNumericValue(reversedCard[i]) * 2;
                if (aux > 9)
                {
                    reversedCard[i] = Convert.ToChar(((int)aux - 9));
                }
                else
                {
                    reversedCard[i] = Convert.ToChar((int)aux);
                }
            }

            for (int i = 0; i < reversedCard.Length; i++)
            {

                if ((int)Char.GetNumericValue(reversedCard[i]) < 0)
                {
                    newCard[i] = reversedCard[i];
                    total += newCard[i];
                }
                else
                {
                    newCard[i] = (int)Char.GetNumericValue(reversedCard[i]);
                    total += newCard[i];
                }
            }
            Console.WriteLine(TypeCard(creditCard) + creditCard + ValidInvalid(total));
        } //Realiza o algoritmo de Luhn
        private static long ReverseInt(long Word)
        {
            char[] vetor = Word.ToString().ToCharArray();
            Array.Reverse(vetor);
            return Int64.Parse(new String(vetor));
        } //Inverte a sequência de números
        private static string TypeCard(long card)
        {
            string auxCard = Convert.ToString(card);
            string cardType;
            if (auxCard.Length > 1)
            {
                if ((auxCard.Remove(2) == "34" || auxCard.Remove(2) == "37") && auxCard.Length == 15)
                {
                    cardType = "AMEX: ";
                }
                else if (auxCard.Remove(4) == "6011" && auxCard.Length == 16)
                {
                    cardType = "Discover: ";
                }
                else if ((Convert.ToInt64(auxCard.Remove(2)) >= 51 && Convert.ToInt64(auxCard.Remove(2)) <= 55)
                    && auxCard.Length == 16)
                {
                    cardType = "MasterCard: ";
                }
                else if (auxCard.Remove(1) == "4" && (auxCard.Length == 16 || auxCard.Length == 13))
                {
                    cardType = "Visa: ";
                }
                else
                {
                    cardType = "Desconhecido: ";
                }
            }
            else
            {
                cardType = "Desconhecido: ";
            }
            return cardType;
        } //informa o tipo de cartão digitado
        private static string ValidInvalid(int total)
        {
            if (total > 0)
            {
                switch (total % 10)
                {
                    case 0:
                        return " (válido)";
                    default:
                        return " (inválido)";
                }
            }
            else
            {
                return " (inválido)";
            }
        } //Verifica se o cartão é válido ou inválido
    }
}
