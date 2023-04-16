/*
 *  Autor: Saulo José/@AppleBoy
 *  Data:  15-04-2023
 *
 *  Sumário:
 *    - [0x01_Main]       >> Função Main(), principal, que pega o tamanho no input pelo usuário.
 *    - [0x02_Celula]     >> Classe para ser um conjunto de dados para entrar na Fila.
 *        - [0x02:1]      >> Construtor da Celula.
 *    - [0x03_Fila]       >> Classe onde é representada a Fila Estática.
 *        - [0x03:1]      >> Construtor da Fila (Note que há um input de inteiro `TAMANHO`).
 *    - [0x04_Attr]       >> Atributos do programa principal.
 *    - [0x05_Continue]   >> Essa função é só para esperar pelo input qualquer do usuário antes de continuar certa tarefa.
 *    - [0x07_Menu]       >> Função que realiza o display do menu de escolhas.
 *    - [0x08_Inserir]    >> Função para inserir uma Celula na Fila.
 *        - [0x08:1]      >> Input do usuário para criar a Celula.
 *        - [0x08:2]      >> Inserção da Celula criada na Fila.
 *    - [0x09_Remover]    >> Função para remover a Celula do topo dentro da Fila, caso existir.
 *    - [0x0a_Mostrar]    >> Essa função só irá mostrar a Celula na posição X, que receber, da Fila.
 *    - [0x0b_Topo]       >> Função que irá mostrar a Celula no topo da Fila.
 *    - [0x0c_Primeiro]   >> Função que irá mostrar a primeira Celula da Fila.
 *    - [0x0d_Pesquisar]  >> Outro método que irá buscar na Fila por uma Celula inserida pelo usuário.
 *
 *    ---                                                                                   EXTRAS   ---
 *
 *    - [0x06_Opcoes]     >> Função para criar uma Fila de opções coloridas.
 *    - [0x0e_Cor]        >> Essa função, reconhece se o SO é Windows ou (provavelmente) Unix, e troca a cor por:
 *        - [0x0e:1]      >> - Função nativa do C# (Windows).
 *        - [0x0e:2]      >> - Utiliza códigos de Escape ANSI (Unix).
 *    - [0x0f_Ansi]       >> Essa função retorna de acordo com a Fila definida, os valores para cada cor.
 *    OBS:                   Paleta de Cores é diminuída para ter compatibilidade com os valores de fonte e fundo.
 *
 *    ---                                                                                   RESUMO   ---
 *
 *    Como pode ver, esse código reutiliza várias das funções declaradas anteriormente com as Pilhas, para achar as
 *    modificações importantes sobre essas funções reutilizadas, só basta procurar pela flag [0x10_MOD], com exceção
 *    da função [0x0c_Primeiro], que é autoexplicativo. É mais fácil pensar na Fila como um vetor, mas que recebe no
 *    último e que remove o primeiro.
 *
 */

using System;
using System.Reflection;


//[0x02_Celula]
public class Celula
{
  public string nome;
  public int codigo;

  //[0x02:1]
  public Celula(int cdg, string nme)
  {
    this.codigo = cdg;
    this.nome = nme;
  }
}

//[0x03_Lista]
public class Lista
{
  public Celula[] dados;
  public int topo;      // realmente necessário?

  //[0x03:1]
  public Lista(int TAMANHO)
  {
    this.topo = -1;
    dados = new Celula[TAMANHO];

    for (int i = 0; i < TAMANHO; i++)
    {
      this.dados[i] = new Celula(-1, "");
    }
  }
}

public class ListaEstatica
{

  //[0x04_Attr]
  public int TAMANHO;
  public Lista ListaEst;

  public ListaEstatica(int _tam)
  {
    this.TAMANHO = _tam;
    this.ListaEst = new Lista(TAMANHO);
  }

  //[0x05_Continue]
  public void Continuar()
  {
    SetCor("Blue", "default");    // >> [0x0e_Cor]
    Console.WriteLine("Digite qualquer tecla para continuar...");
    SetCor("default", "");
    Console.ReadKey();
  }

  //[0x06_Opcoes]
  public void Opcoes(int num, string text)
  {
    SetCor("Yellow", "default");
    Console.Write(num);
    SetCor("default", "");
    Console.WriteLine(" - "+text);
  }

  //[0x07_Menu]
  public int Menu()
  {
    int selecao = -1;
    Console.Clear();
    SetCor("Red", "White");
    Console.WriteLine("DIGITE 0 PARA SAIR\n");
    SetCor("default", "");
    Console.WriteLine("Selecione uma das opções abaixo:");
    Opcoes(1, "Inserir um novo elemento.");   // >> [0x06_Opcoes]
    Opcoes(2, "Remover um elemento.");
    Opcoes(3, "Visualizar no topo.");
    Opcoes(4, "Visualizar no primeiro.");
    Opcoes(5, "Pesquisar um elemento.");
    while (selecao < 0 || selecao > 6)
      while (!int.TryParse(Console.ReadLine(), out selecao));
    return selecao;
  }

  //[0x08_Inserir]
  public void Inserir()
  {
    Console.Clear();
    if (ListaEst.topo >= TAMANHO-1)
    {
      SetCor("Red", "default");
      Console.WriteLine("ERRO! TAMANHO MÁXIMO ALCANÇADO!");
      SetCor("default", "");
      Continuar();    // >> [0x05_Continue]
      return;
    }

    //[0x08:1]
    SetCor("Blue", "default");
    Console.WriteLine("Elementos Criados: "+(ListaEst.topo+1)+"\n");
    SetCor("default", "");
    Console.WriteLine("Digite o Nome do Elemento:");
    string? nomeAux = Console.ReadLine();
    while (nomeAux == null)
      nomeAux = Console.ReadLine();

    Console.WriteLine("Digite o Número do Elemento:");
    int codigoAux;
    while (!int.TryParse(Console.ReadLine(), out codigoAux));
    while (codigoAux == -1)
    {
      SetCor("Red", "default");
      Console.WriteLine("ERRO! VALOR RESERVADO PELO PROGRAMA, USE OUTRO VALOR PARA O ELEMENTO!");
      while (!int.TryParse(Console.ReadLine(), out codigoAux));
    }
    SetCor("default", "");

    //[0x08:2]
    ListaEst.dados[++ListaEst.topo].nome = nomeAux;
    ListaEst.dados[ListaEst.topo].codigo = codigoAux;

    if (ListaEst.dados[ListaEst.topo].codigo != codigoAux)
      throw new System.Exception("Falha na criação do elemento");

    SetCor("Green", "default");
    Console.WriteLine("Elemento inserido com Sucesso!");
    Continuar();
  }

  //[0x09_Remover]
  public void Remover()
  {
    Console.Clear();
    if (ListaEst.topo < 0)
    {
      SetCor("Red", "default");
      Console.WriteLine("ERRO! A Lista ESTÁ VAZIA!");
      SetCor("default", "");
      Continuar();
      return;
    }

    Console.WriteLine("Removendo o Elemento do topo...");

    //[0x10_MOD]
    int ultimo = 0;
    for (int i = 0; i < ListaEst.topo; i++, ultimo++)
    {
      ListaEst.dados[i].nome   =  ListaEst.dados[i+1].nome;
      ListaEst.dados[i].codigo =  ListaEst.dados[i+1].codigo;
    }
    // Sobresecreve o primeiro valor com o valor seguinte e assim segue até chegar no topo.

    ListaEst.dados[ultimo].nome = "";
    ListaEst.dados[ultimo].codigo = -1;

    if (ListaEst.dados[ListaEst.topo--].codigo != -1)
      throw new System.Exception("Falha ao remover um elemento");

    SetCor("Green", "default");
    Console.WriteLine("Elemento removido com Sucesso!");
    Continuar();
  }

  //[0x0a_Mostrar]
  public void Mostrar(int index)
  {
    Console.WriteLine("O Topo da Lista possui os seguintes valores:\n"
                    + " - Nome:   "+ListaEst.dados[index].nome   +"\n"
                    + " - Código: "+ListaEst.dados[index].codigo);
  }

  //[0x0b_Topo]
  public void VerTopo()
  {
    Console.Clear();
    if (ListaEst.topo < 0)
    {
      SetCor("Red", "default");
      Console.WriteLine("ERRO! A Lista ESTÁ VAZIA!");
      SetCor("default", "");
      Continuar();
      return;
    }

    Mostrar(ListaEst.topo);
    Continuar();
  }

  //[0x0c_Primeiro]
  public void VerPrimeiro()
  {
    Console.Clear();
    if (ListaEst.topo < 0)
    {
      SetCor("Red", "default");
      Console.WriteLine("ERRO! A Lista ESTÁ VAZIA!");
      SetCor("default", "");
      Continuar();
      return;
    }

    Mostrar(0);
    Continuar();
  }

  //[0x0d_Pesquisar]
  public void Pesquisar()
  {
    Console.Clear();
    Console.WriteLine("Digite o CÓDIGO do Elemento (Caso queira procurar pelo NOME, digite -1):");
    int codigoAux;
    while (!int.TryParse(Console.ReadLine(), out codigoAux));
    if (codigoAux == -1)
    {
      Console.WriteLine("Digite o NOME do Elemento:");  // Pesquisa pelo NOME v
      string? nomeAux = Console.ReadLine();

      for (int i = 0; i <= ListaEst.topo; i++)
      {
        if (nomeAux == ListaEst.dados[i].nome)
        {
          SetCor("Green", "default");
          Console.WriteLine("Elemento Encontrado!");
          Mostrar(i);
          Continuar();
          return;
        }
      }
      SetCor("Red", "default");
      Console.WriteLine("Elemento Não Foi Encontrado!");
      Continuar();
    }
    else
    {
      for (int i = 0; i <= ListaEst.topo; i++)          // Pesquisa pelo CÓDIGO v
      {
        if (codigoAux == ListaEst.dados[i].codigo)
        {
          SetCor("Green", "default");
          Console.WriteLine("Elemento Encontrado!");
          Mostrar(i);
          Continuar();
          return;
        }
      }
      SetCor("Red", "default");
      Console.WriteLine("Elemento Não Foi Encontrado!");
      Continuar();
    }
  }

  //[0x0e_Cor]
  public void SetCor(string colorFg, string colorBg)
  {
    if (OperatingSystem.IsWindows())
    {
      if (colorFg != "default")
      {
        //[0x0e:1]
        if (colorBg == "default")
            colorBg = "Black";

        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorFg);
        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorBg);
        return;
      }
      else
      {
        Console.ResetColor();
        return;
      }
    }
    else
    {
      if (colorFg != "default")
      {
        //[0x0e:2]
        if (colorBg == "default")
        {
          int CorFgANSI = GetCodigoANSI(colorFg);
          Console.Write($"\x1b[{CorFgANSI}m");
          return;
        }
        else
        {
          int CorFgANSI = GetCodigoANSI(colorFg);
          int CorBgANSI = GetCodigoANSI(colorBg) + 10;
          Console.Write($"\x1b[{CorFgANSI};{CorBgANSI}m");
          return;
        }
      }
      else
      {
        Console.Write("\x1b[0m");
      }
    }
  }

  //[0x0f_Ansi]
  public int GetCodigoANSI(string cor)
  {
    switch (cor)
    {
      case "Black":
        return 30;
      case "Red":
        return 31;
      case "Green":
        return 32;
      case "Yellow":
        return 33;
      case "Blue":
        return 34;
      case "Magenta":
        return 35;
      case "Cyan":
        return 36;
      case "White":
        return 37;
      default:
        throw new ArgumentException("Invalid color: ",cor);
    }
  }
}

public class MainClass
{
  //[0x01_Main]
  static public void Main()
  {
    Console.Clear();
    Console.WriteLine("Insira a Quantidade de Celulas que a Lista Suporta:");
    int inicializador;
    while (!int.TryParse(Console.ReadLine(), out inicializador));
    var Controller = new ListaEstatica(inicializador);

    int opcao;
    bool run = true;
    while (run)
    {
      opcao = Controller.Menu();    // >> [0x07_Menu]
      switch(opcao)
      {
        case 1:
          Controller.Inserir();     // >> [0x08_Inserir]
          break;
        case 2:
          Controller.Remover();     // >> [0x09_Remover]
          break;
        case 3:
          Controller.VerTopo();     // >> [0x0b_Topo]
          break;
        case 4:
          Controller.VerPrimeiro(); // >> [0x0c_Primeiro]
          break;
        case 5:
          Controller.Pesquisar();   // >> [0x0d_Pesquisar]
          break;
        case 0:
          run = !run;
          break;
      }
    }
  }
}

