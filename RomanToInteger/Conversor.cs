public class Conversor
{
    public static readonly string[] BASE = { "I", "V", "X", "L", "C", "D", "M" };
    public static readonly int[] BASE_NUM = { 1, 5, 10, 50, 100, 500, 1000 };

    public static int ReturnPositionInBase(string value)
    {
        for (int i = 0; i < BASE.Length; i++)
            if (value == BASE[i])
                return i;

        return -1;
    }

    public static int RomamToIntegerV2(string number)
    {
        var resultado = 0;
        int posicaoAtual, basePosicaoAnterior =0, posicaoAnterior = 0;
        bool acumular = false;
        int valorAcumulado = 0;
        var operacao = 0;


        for (int i = 0; i <= number.Length-1; i++)
        {
            if (i == 0)
            {
                posicaoAnterior = ReturnPositionInBase(number.Substring(i, 1));
                resultado = BASE_NUM[posicaoAnterior];
                continue;
            }

            posicaoAtual = ReturnPositionInBase(number.Substring(i, 1));

            if (posicaoAnterior == posicaoAtual && acumular == false)
            {
                resultado += BASE_NUM[posicaoAnterior];
                continue;
            }

            if (posicaoAnterior != posicaoAtual && acumular == false)
            {
                acumular = true;
                valorAcumulado += BASE_NUM[posicaoAtual];

                operacao = posicaoAnterior < posicaoAtual ? 0 : 1;

                basePosicaoAnterior = posicaoAnterior;
                posicaoAnterior = posicaoAtual;

                continue;
            }

            if (posicaoAnterior == posicaoAtual && acumular)
            {
                valorAcumulado += BASE_NUM[posicaoAtual];
                continue;
            }

            if (posicaoAnterior != posicaoAtual && acumular)
            {
                if (basePosicaoAnterior < posicaoAnterior)
                {
                    resultado -= BASE_NUM[basePosicaoAnterior];
                    valorAcumulado -= BASE_NUM[basePosicaoAnterior];
                }                    

                if (posicaoAnterior < posicaoAtual)
                    resultado += BASE_NUM[posicaoAtual] - valorAcumulado;
                else
                    resultado += valorAcumulado;

                valorAcumulado = 0;
                acumular = false;

                if (posicaoAtual < posicaoAnterior)
                    i--;
            }
        }

        if (valorAcumulado > 0)
            if (operacao == 0)
                resultado = valorAcumulado - resultado;
            else
                resultado += valorAcumulado;

        return resultado;
    }

    private static void Main(string[] args)
    {
        Console.WriteLine(RomamToIntegerV2("MCMXCIV"));  
    }
}