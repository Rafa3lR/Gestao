using System.Runtime.CompilerServices;
using System.IO;
using System;

internal class Program
{
    
    private static void Main(string[] args)
    {
        int quantVenda = 0, escMenuPrincipal = 10, posic = 0, escVenda = 1000001, escMenuCadastro = 5, escCadastro;
        string confirmacao, relatorio = "", relatorioFinal = "", pause, encerrado;
        float totalGeral = 0;
        int[] quantEstoque = new int[1000001], quantEstoqueVenda = new int[1000001];
        float[] preco = new float[1000001], precoVenda = new float[1000001];
        string[] nomeProdutos = new string[1000001], nomeProdutosVenda = new string[1000001];

        //Adicionar caminho dos arquivos de texto
        string desctxt = @"";
        string precotxt = @"";
        string quanttxt = @"";
        string relatoriostxt = @"";
        string relatorioBackuptxt = @"";
        string encerradotxt = @"";
        string totalGeraltxt = @"";


        using (StreamReader sr = new StreamReader(desctxt))
        {
            string descricao;
            while ((descricao = sr.ReadLine()) != null)
            {
                nomeProdutos[posic] = descricao;
                posic++;
            }
        }
        using (StreamReader sr = new StreamReader(precotxt))
        {
            posic = 0;
            string precos;
            while ((precos = sr.ReadLine()) != null)
            {
                preco[posic] = Convert.ToInt32(precos);
                posic++;
            }
        }
        using (StreamReader sr = new StreamReader (quanttxt))
        {
            posic = 0;
            string quant;
            while ((quant = sr.ReadLine()) != null)
            {
                quantEstoque[posic] = Convert.ToInt32(quant);
                posic++;
            }
        }
        using (StreamReader sr = new StreamReader(encerradotxt))
        {
           encerrado = sr.ReadLine();
        }
        if (encerrado == "0")
        {
            using (StreamReader sr = new StreamReader(relatorioBackuptxt))
            {
                string relatorios1;
                while ((relatorios1 = sr.ReadLine()) != null)
                {
                    relatorioFinal += relatorios1 + "\n";
                }
            }
            using (StreamReader sr = new StreamReader(totalGeraltxt))
            {
                totalGeral = Convert.ToSingle(sr.ReadLine());
            }
        }
        using (StreamWriter sw = new StreamWriter(encerradotxt))
        {
            sw.Write("0");
        }

        while (escMenuPrincipal != 4)
        {   
            Console.Clear();
            Console.WriteLine("Bem vindo!");
            Console.WriteLine("\nEscolha uma das opções:");
            Console.WriteLine("|  1. Venda      |  2. Estoque   |");
            Console.WriteLine("|  3. Relatório  |  4. Encerrar  |");
            Console.WriteLine("Código: ");
            try
            {
                escMenuPrincipal = Convert.ToInt32(Console.ReadLine());


                //Venda de produtos
                if (escMenuPrincipal == 1)
                {
                    float totalLocal = 0;
                    relatorio = "";
                    quantVenda = 0;
                    escVenda = 1000001;
                    Console.Clear();
                    while (escVenda != -2)
                    {
                        Console.WriteLine("Vendas");
                        for (int i = 0; i < 1000000; i++)
                        {
                            if (quantEstoqueVenda[i] > 0)
                            {
                                Console.WriteLine("\n" + i + "  -  " + nomeProdutosVenda[i] + "  -  " + precoVenda[i] + "  -  " + quantEstoqueVenda[i]);
                            }
                        }
                        Console.WriteLine("\nTotal: " + totalLocal);
                        Console.WriteLine("\n|  -1. Consultar tabela de produtos  |  -2. Encerrar pedido  |");
                        Console.WriteLine("|  -3. Cancelar produto              |  Código do produto    |");
                        Console.WriteLine("Cod: ");
                        try
                        {
                            escVenda = Convert.ToInt32(Console.ReadLine());
                        }
                        catch { }

                        if (escVenda >= 0 && escVenda < 1000000)
                        {
                            if (preco[escVenda] > 0.00)
                            {
                                Console.WriteLine("\n" + nomeProdutos[escVenda] + " - " + preco[escVenda]);
                                Console.WriteLine("Confirmar (s/n): ");
                                confirmacao = Console.ReadLine();

                                if (confirmacao == "s")
                                {
                                    if (quantEstoque[escVenda] > 0)
                                    {
                                        quantVenda = quantEstoque[escVenda] + 1;
                                        while (quantVenda > quantEstoque[escVenda])
                                        {
                                            Console.WriteLine("Digite a quantidade: ");
                                            try
                                            {
                                                quantVenda = Convert.ToInt32(Console.ReadLine());
                                            }
                                            catch { }
                                            Console.Clear();
                                            if (quantVenda > quantEstoque[escVenda])
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Saldo insuficiente");
                                            }
                                        }
                                        quantEstoque[escVenda] -= quantVenda;
                                        nomeProdutosVenda[escVenda] = nomeProdutos[escVenda];
                                        quantEstoqueVenda[escVenda] += quantVenda;
                                        precoVenda[escVenda] += (preco[escVenda] * quantVenda);
                                        totalLocal += preco[escVenda] * quantVenda;
                                        totalGeral += preco[escVenda] * quantVenda;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Saldo insuficiente");
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nProduto não existe");
                                Console.WriteLine("Enter para continuar");
                                pause = Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        else if (escVenda == -1)
                        {
                            Console.Clear();
                            Console.WriteLine("(Cod  -  Descrição  -  Preço  -  Quantidade)\n");
                            Console.Write("==============================================");
                            for (int i = 0; i < 1000000; i++)
                            {
                                if (quantEstoque[i] > 0)
                                {
                                    Console.WriteLine("\n" + i + "  -  " + nomeProdutos[i] + "  -  " + preco[i] + "  -  " + quantEstoque[i]);
                                }
                            }
                            Console.WriteLine("==============================================");
                            Console.WriteLine("\nEnter para continuar");
                            pause = Console.ReadLine();
                            Console.Clear();
                        }
                        else if (escVenda == -2)
                        {
                            Console.Clear();
                        }
                        else if (escVenda == -3)
                        {
                            Console.WriteLine("Cod do produto a ser excluído: ");
                            try
                            {
                                escVenda = Convert.ToInt32(Console.ReadLine());
                            }
                            catch { }
                            if (escVenda >= 0 && escVenda < 1000000)
                            {
                                quantEstoque[escVenda] += quantEstoqueVenda[escVenda];
                                nomeProdutosVenda[escVenda] = "";
                                quantEstoqueVenda[escVenda] = 0;
                                precoVenda[escVenda] = 0;
                                totalLocal -= preco[escVenda] * quantVenda;
                                totalGeral -= preco[escVenda] * quantVenda;
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("\nProduto não existe");
                                Console.WriteLine("Enter para continuar");
                                pause = Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nProduto não existe");
                            Console.WriteLine("Enter para continuar");
                            pause = Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    for (int i = 0; i < 1000000; i++)
                    {
                        if (quantEstoqueVenda[i] > 0)
                        {
                            relatorio += "\n" + i + "  -  " + nomeProdutosVenda[i] + "  -  " + precoVenda[i] + "  -  " + quantEstoqueVenda[i];
                        }
                        nomeProdutosVenda[i] = "";
                        quantEstoqueVenda[i] = 0;
                        precoVenda[i] = 0;
                    }

                    using (StreamWriter sw = new StreamWriter(desctxt))
                    {
                        sw.Write("");
                    }
                    using (StreamWriter sw = new StreamWriter(precotxt))
                    {
                        sw.Write("");
                    }
                    using (StreamWriter sw = new StreamWriter(quanttxt))
                    {
                        sw.Write("");
                    }

                    for (int i = 0; i < 1000000; i++)
                    {
                        if (quantEstoque[i] > 0)
                        {
                            using (StreamWriter sw = File.AppendText(desctxt))
                            {
                                sw.WriteLine(nomeProdutos[i]);
                            }
                            using (StreamWriter sw = File.AppendText(precotxt))
                            {
                                sw.WriteLine(preco[i]);
                            }
                            using (StreamWriter sw = File.AppendText(quanttxt))
                            {
                                sw.WriteLine(quantEstoque[i]);
                            }
                        }
                    }

                    if (quantVenda > 0)
                    {
                        relatorioFinal += relatorio;

                        using (StreamWriter sw = new StreamWriter(relatorioBackuptxt))
                        {
                            sw.Write(relatorioFinal);
                        }
                        using (StreamWriter sw = new StreamWriter(totalGeraltxt))
                        {
                            sw.Write(totalGeral);
                        }

                    }
                }

                //Cadastro de produtos
                if (escMenuPrincipal == 2)
                {
                    escMenuCadastro = 5;
                    while (escMenuCadastro != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("ESTOQUE\n");
                        Console.WriteLine("(Cod  -  Descrição  -  Preço  -  Quantidade)\n");
                        Console.Write("==============================================");
                        for (int i = 0; i < 1000000; i++)
                        {
                            if (quantEstoque[i] > 0)
                            {
                                Console.WriteLine("\n" + i + "  -  " + nomeProdutos[i] + "  -  " + preco[i] + "  -  " + quantEstoque[i]);
                            }
                        }
                        Console.WriteLine("==============================================");
                        Console.WriteLine("\n|  1. Cadastrar produto  |  2. Reajuste de preço  |  3. Devolução     |");
                        Console.WriteLine(  "|  4. Adicionar saldo    |  5. Excluir produto    |  0. Menu inicial  |");
                        Console.WriteLine("Código: ");
                        try
                        {
                            escMenuCadastro = Convert.ToInt32(Console.ReadLine());
                        }
                        catch { }
                        if (escMenuCadastro == 1)
                        {
                            if (posic < 1000000)
                            {
                                Console.Clear();
                                Console.WriteLine("Digite o nome do produto: ");
                                nomeProdutos[posic] = Console.ReadLine();
                                Console.WriteLine("Digite o valor do produto: ");
                                try
                                {
                                    preco[posic] = Convert.ToSingle(Console.ReadLine());
                                }
                                catch { }
                                Console.WriteLine("Digite a quantidade: ");
                                try
                                {
                                    quantEstoque[posic] = Convert.ToInt32(Console.ReadLine());
                                }
                                catch { }

                                using (StreamWriter sw = File.AppendText(desctxt))
                                {
                                    sw.WriteLine(nomeProdutos[posic]);
                                }
                                using (StreamWriter sw = File.AppendText(precotxt))
                                {
                                    sw.WriteLine(preco[posic]);
                                }
                                using (StreamWriter sw = File.AppendText(quanttxt))
                                {
                                    sw.WriteLine(quantEstoque[posic]);
                                }

                                posic++;
                            }
                            else if (posic > 999999)
                            {
                                Console.WriteLine("\nLimite alcançado");
                                Console.WriteLine("Enter para voltar");
                                pause = Console.ReadLine();
                            }
                        }
                        else if (escMenuCadastro == 2)
                        {
                            Console.WriteLine("Digite o cod do produto: ");
                            try
                            {
                                escCadastro = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Digite o novo Valor: ");
                                preco[escCadastro] = Convert.ToSingle(Console.ReadLine());
                            }
                            catch { }

                            using (StreamWriter sw = new StreamWriter(desctxt))
                            {
                                sw.Write("");
                            }
                            using (StreamWriter sw = new StreamWriter(precotxt))
                            {
                                sw.Write("");
                            }
                            using (StreamWriter sw = new StreamWriter(quanttxt))
                            {
                                sw.Write("");
                            }

                            for (int i = 0; i < 1000000; i++)
                            {
                                if (quantEstoque[i] > 0)
                                {
                                    using (StreamWriter sw = File.AppendText(desctxt))
                                    {
                                        sw.WriteLine(nomeProdutos[i]);
                                    }
                                    using (StreamWriter sw = File.AppendText(precotxt))
                                    {
                                        sw.WriteLine(preco[i]);
                                    }
                                    using (StreamWriter sw = File.AppendText(quanttxt))
                                    {
                                        sw.WriteLine(quantEstoque[i]);
                                    }
                                }
                            }
                        }
                        else if (escMenuCadastro == 3)
                        {
                            int quantDevo = 0;
                            escCadastro = 1000001;
                            Console.WriteLine("Digite o cod do Produto: ");
                            try
                            {
                                escCadastro = Convert.ToInt32(Console.ReadLine());                           
                            }
                            catch { }
                            Console.WriteLine("Digite a quantidade: ");
                            try
                            {
                                quantDevo = Convert.ToInt32(Console.ReadLine());
                            }
                            catch { }
                            relatorioFinal += "\n" + escCadastro + "  -  " + nomeProdutos[escCadastro] + "  -  " + ((preco[escCadastro] * quantDevo) * -1) + "  -  " + quantDevo;
                            totalGeral -= preco[escCadastro] * quantDevo;
                            quantEstoque[escCadastro] += quantDevo;

                            using (StreamWriter sw = new StreamWriter(desctxt))
                            {
                                sw.Write("");
                            }
                            using (StreamWriter sw = new StreamWriter(precotxt))
                            {
                                sw.Write("");
                            }
                            using (StreamWriter sw = new StreamWriter(quanttxt))
                            {
                                sw.Write("");
                            }

                            for (int i = 0; i < 1000000; i++)
                            {
                                if (quantEstoque[i] > 0)
                                {
                                    using (StreamWriter sw = File.AppendText(desctxt))
                                    {
                                        sw.WriteLine(nomeProdutos[i]);
                                    }
                                    using (StreamWriter sw = File.AppendText(precotxt))
                                    {
                                        sw.WriteLine(preco[i]);
                                    }
                                    using (StreamWriter sw = File.AppendText(quanttxt))
                                    {
                                        sw.WriteLine(quantEstoque[i]);
                                    }
                                }
                            }
                            using (StreamWriter sw = new StreamWriter(relatorioBackuptxt))
                            {
                                sw.Write(relatorioFinal);
                            }
                            using (StreamWriter sw = new StreamWriter(totalGeraltxt))
                            {
                                sw.Write(totalGeral);
                            }
                        }
                        else if (escMenuCadastro == 4)
                        {
                            int quantLocal = 0;
                            Console.WriteLine("Digite o cod do produto: ");
                            try
                            {
                                escCadastro = Convert.ToInt32(Console.ReadLine());
                                if (escCadastro >= 0 && escCadastro < 1000000)
                                {
                                    if (preco[escCadastro] > 0)
                                    {
                                        Console.WriteLine("Digite a quantidade: ");
                                        try
                                        {
                                            quantLocal = Convert.ToInt32(Console.ReadLine());
                                            quantEstoque[escCadastro] += quantLocal;
                                        }
                                        catch { }

                                        using (StreamWriter sw = new StreamWriter(desctxt))
                                        {
                                            sw.Write("");
                                        }
                                        using (StreamWriter sw = new StreamWriter(precotxt))
                                        {
                                            sw.Write("");
                                        }
                                        using (StreamWriter sw = new StreamWriter(quanttxt))
                                        {
                                            sw.Write("");
                                        }

                                        for (int i = 0; i < 1000000; i++)
                                        {
                                            if (quantEstoque[i] > 0)
                                            {
                                                using (StreamWriter sw = File.AppendText(desctxt))
                                                {
                                                    sw.WriteLine(nomeProdutos[i]);
                                                }
                                                using (StreamWriter sw = File.AppendText(precotxt))
                                                {
                                                    sw.WriteLine(preco[i]);
                                                }
                                                using (StreamWriter sw = File.AppendText(quanttxt))
                                                {
                                                    sw.WriteLine(quantEstoque[i]);
                                                }
                                            }
                                        }
                                    }
                                    else if (preco[escCadastro] <= 0)
                                    {
                                        Console.WriteLine("\nProduto não existe");
                                        Console.WriteLine("Enter para continuar");
                                        pause = Console.ReadLine();
                                        Console.Clear();
                                    }
                                }
                            }
                            catch { }
                        }
                        else if (escMenuCadastro == 5)
                        {
                            if (posic > 0 || escMenuCadastro == 4)
                            {
                                Console.WriteLine("Digite o codigo do produto a ser excluído: ");
                                try
                                {
                                    escCadastro = Convert.ToInt32(Console.ReadLine());
                                    for (int i = escCadastro; i < 1000000; i++)
                                    {
                                        nomeProdutos[i] = nomeProdutos[i + 1];
                                        preco[i] = preco[i + 1];
                                        quantEstoque[i] = quantEstoque[i + 1];
                                    }
                                    if (posic > 0)
                                    {
                                        posic--;
                                    }
                                }
                                catch { }

                                using (StreamWriter sw = new StreamWriter(desctxt))
                                {
                                    sw.Write("");
                                }
                                using (StreamWriter sw = new StreamWriter(precotxt))
                                {
                                    sw.Write("");
                                }
                                using (StreamWriter sw = new StreamWriter(quanttxt))
                                {
                                    sw.Write("");
                                }

                                for (int i = 0; i < 1000000; i++)
                                {
                                    if (quantEstoque[i] > 0)
                                    {
                                        using (StreamWriter sw = File.AppendText(desctxt))
                                        {
                                            sw.WriteLine(nomeProdutos[i]);
                                        }
                                        using (StreamWriter sw = File.AppendText(precotxt))
                                        {
                                            sw.WriteLine(preco[i]);
                                        }
                                        using (StreamWriter sw = File.AppendText(quanttxt))
                                        {
                                            sw.WriteLine(quantEstoque[i]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNão existe produto para ser excluído");
                                Console.WriteLine("Enter para continuar");
                                pause = Console.ReadLine();
                                Console.Clear();
                            }
                        }
                    }
                }

                //Relatório
                if (escMenuPrincipal == 3)
                {
                    Console.Clear();
                    Console.WriteLine("RELATORIO DE VENDAS \n");
                    Console.WriteLine("(Cod  -  Descrição  -  Preço  -  Quantidade)\n");
                    Console.Write("==============================================");
                    Console.WriteLine(relatorioFinal);
                    Console.WriteLine("==============================================");
                    Console.WriteLine("Total: " + totalGeral);
                    Console.WriteLine("\nEnter para voltar");
                    pause = Console.ReadLine();
                }
            }
            catch { }
        }

        //Fim do programa
        Console.Clear();
        Console.WriteLine("RELATORIO DE VENDAS \n");
        Console.WriteLine("(Cod  -  Descrição  -  Preço  -  Quantidade)\n");
        Console.Write("==============================================");
        Console.WriteLine(relatorioFinal);
        Console.WriteLine("==============================================");
        Console.WriteLine("Total: " + totalGeral);
        
        using (StreamWriter sw = File.AppendText(relatoriostxt))
        {
            sw.WriteLine("");
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine("RELATORIO DE VENDAS \n");
            sw.WriteLine("(Cod  -  Descrição  -  Preço  -  Quantidade)\n");
            sw.Write("==============================================");
            sw.WriteLine(relatorioFinal);
            sw.WriteLine("==============================================");
            sw.WriteLine("Total: " + totalGeral);
        }
        using (StreamWriter sw = new StreamWriter(encerradotxt))
        {
            sw.Write("1");
        }
        using (StreamWriter sw = new StreamWriter(relatorioBackuptxt))
        {
            sw.Write("");
        }
        using (StreamWriter sw = new StreamWriter(totalGeraltxt))
        {
            sw.Write("0");
        }
        pause = Console.ReadLine();
    }
}