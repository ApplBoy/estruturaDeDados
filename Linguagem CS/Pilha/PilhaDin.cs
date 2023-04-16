/*
 *  Autor: Saulo José/@ApplBoy
 *  Data:  04-04-2023
 * 
 *  Sumário:
 *    - [0x01_Main]       >> Função Main(), principal.
 *    - [0x02_Celula]     >> Classe para ser um conjunto de dados para entrar na Pilha.
 *        - [0x02:1]      >> Construtor da Celula.
 *    - [0x03_Pilha]      >> Classe onde é representada a Pilha Dinâmica.
 *        - [0x03:1]      >> Construtor da Pilha.
 *    - [0x04_Attr]       >> Atributos do programa principal.
 *    - [0x05_Continue]   >> Essa função é só para esperar pelo input qualquer do usuário antes de continuar certa tarefa.
 *    - [0x07_Menu]       >> Função que realiza o display do menu de escolhas.
 *    - [0x08_Inserir]    >> Função para inserir uma Celula na Pilha.
 *        - [0x08:1]      >> Input do usuário para criar a Celula.
 *        - [0x08:2]      >> Inserção da Celula criada na Pilha.
 *    - [0x09_Remover]    >> Função para remover a Celula do topo dentro da Pilha, caso existir.
 *    - [0x0a_Mostrar]    >> Essa função só irá mostrar a Celula na posição X, que receber, da Pilha.
 *    - [0x0b_Topo]       >> Função que irá mostrar a Celula no topo da Pilha.
 *    - [0x0c_Pesquisar]  >> Outro método que irá buscar na Pilha por uma Celula inserida pelo usuário.
 *
 *    ---                                                                                   EXTRAS   ---
 *
 *    - [0x06_Opcoes]     >> Função para criar uma lista de opções coloridas.
 *    - [0x0d_Cor]        >> Essa função, reconhece se o SO é Windows ou (provavelmente) Unix, e troca a cor por:
 *        - [0x0d:1]      >> - Função nativa do C# (Windows).
 *        - [0x0d:2]      >> - Utiliza códigos de Escape ANSI (Unix).
 *    - [0x0e_Ansi]       >> Essa função retorna de acordo com a lista definida, os valores para cada cor.
 *    - [0x0f_GetSet]            >> Getters (Comentado) e Setters.
 *    OBS:                   Paleta de Cores é diminuída para ter compatibilidade com os valores de fonte e fundo.
 *
 *    ---                                                                                   RESUMO   ---
 *
 *    Esse código agora tem uma estrutura diferente que o anterior, como podemos ver, Pilha agora não aceita mais um 
 *    TAMANHO e utiliza Lists para automatizar o processo de adicionar dinamicamente Celulas. Caso não queira usar
 *    List, remova o comentário nas funções marcadas com flag [0x10_FLAG], e comentar as partes de List abaixo.
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
  public Celula(int cdg, string nme) {
    this.codigo = cdg;
    this.nome = nme;
  }
}

//[0x03_Pilha]
public class Pilha
{
  //[0x10_FLAG]
  // public Celula[] dados;
  // public int topo=0;
  public List<Celula> dados;

  public Pilha()
  {
    //[0x10_FLAG]
    // this.dados = new Celula[5];
    this.dados = new List<Celula>();
  }

  //[0x10_FLAG] (Não há substituição)
  // public void Redimensionar(int novoTamanho)
  // {
  //   Celula[] novoVetor = new Celula[novoTamanho];
  //   Array.Copy(dados, novoVetor, Math.Min(novoTamanho, dados.Length));
  //   this.dados = novoVetor;
  // }
   

  public void Push(Celula item)
  {
    //[0x10_FLAG]
    // if (topo == dados.Length - 1) {
    //   // pilha está cheia, redimensiona
    //   Redimensionar(dados.Length * 2);
    // }
    // topo++;
    // dados[topo] = item;
    
    dados.Add(item);
  }

  public Celula Pop()
  {
    //[0x10_FLAG]
    // if (topo == 0) {
    //   return null;
    // }
    // Celula item = dados[topo];
    // dados[topo] = null;
    // topo--;
    // if (topo < dados.Length/4) {
    //   // pilha está com tamanho muito grande, redimensiona
    //   Redimensionar(Math.Max(dados.Length/2, 1));
    // }
    
    if (dados.Count < 0)
    {
        throw new InvalidOperationException("A Pilha Está Vazia");
    }
    Celula item = dados[dados.Count-1];
    dados.RemoveAt(dados.Count-1);
    
    return item;
  }
}

public class PilhaControlador  {

  //[0x04_Attr]
  public Pilha pilhaDin;

  public PilhaControlador()
  {
    this.pilhaDin = new Pilha();
  }

  //[0x05_Continue]
  public void Continuar()
  {
    SetCor("Blue", "default");    // >> [0x0d_Cor]
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
    Opcoes(4, "Pesquisar um elemento.");
    while (selecao < 0 || selecao > 5)
      while (!int.TryParse(Console.ReadLine(), out selecao));
    return selecao;
  }

  //[0x08_Inserir]
  public void Inserir()
  {
    Console.Clear();
    //[0x08:1]
    SetCor("Blue", "default");
    //[0x10_FLAG]
    // Console.WriteLine("Elementos Criados: "+(pilhaDin.topo)+"\n");
    Console.WriteLine("Elementos Criados: "+(pilhaDin.dados.Count)+"\n");
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
    pilhaDin.Push(new Celula(codigoAux, nomeAux));

    //[0x10_FLAG]
    // if (pilhaDin.dados[pilhaDin.topo].codigo != codigoAux)
    if (pilhaDin.dados[pilhaDin.dados.Count-1].codigo != codigoAux)
      throw new System.Exception("Falha na criação do elemento");

    SetCor("Green", "default");
    Console.WriteLine("Elemento inserido com Sucesso!");
    Continuar();
  }

  //[0x09_Remover]
  public void Remover()
  {
    Console.Clear();
    //[0x10_FLAG]
    // if (pilhaDin.topo < 1)
    if (pilhaDin.dados.Count-1 < 0)
    {
      SetCor("Red", "default");
      Console.WriteLine("ERRO! A PILHA ESTÁ VAZIA!");
      SetCor("default", "");
      Continuar();
      return;
    }

    Console.WriteLine("Removendo o Elemento do topo...");

    //[0x10_FLAG]
    // if (pilhaDin.dados[pilhaDin.topo] != pilhaDin.Pop())
    if (pilhaDin.dados[pilhaDin.dados.Count-1] != pilhaDin.Pop())
      throw new System.Exception("Falha ao remover um elemento");

    SetCor("Green", "default");
    Console.WriteLine("Elemento removido com Sucesso!");
    Continuar();
  }

  //[0x0a_Mostrar]
  public void Mostrar(int index)
  {
    Console.WriteLine("O Topo da Pilha possui os seguintes valores:\n"
                    + " - Nome:   "+pilhaDin.dados[index].nome   +"\n"
                    + " - Código: "+pilhaDin.dados[index].codigo);
  }

  //[0x0b_Topo]
  public void VerTopo()
  {
    Console.Clear();
    //[0x10_FLAG]
    // if (pilhaDin.topo < 1)
    if (pilhaDin.dados.Count-1 < 0)
    {
      SetCor("Red", "default");
      Console.WriteLine("ERRO! A PILHA ESTÁ VAZIA!");
      SetCor("default", "");
      Continuar();
      return;
    }

    //[0x10_FLAG]
    // Mostrar(pilhaDin.topo);
    Mostrar(pilhaDin.dados.Count-1);
    Continuar();
  }

  //[0x0c_Pesquisar]
  public void Pesquisar()
  {
    Console.Clear();
    Console.WriteLine("Digite o CÓDIGO do Elemento (Caso queira procurar pelo NOME, digite nada):");
    int codigoAux;
    if (!int.TryParse(Console.ReadLine(), out codigoAux))
    {
      Console.WriteLine("Digite o NOME do Elemento:");  // Pesquisa pelo NOME v
      string? nomeAux = Console.ReadLine();

      //[0x10_FLAG]
      // for (int i = 0; i <= pilhaDin.topo; i++)
      for (int i = 0; i <= pilhaDin.dados.Count-1; i++)
      {
        if (nomeAux == pilhaDin.dados[i].nome)
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
      //[0x10_FLAG]
      // for (int i = 1; i <= pilhaDin.topo; i++)
      for (int i = 0; i <= pilhaDin.dados.Count-1; i++)          // Pesquisa pelo CÓDIGO v
      {
        if (codigoAux == pilhaDin.dados[i].codigo)
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

  //[0x0d_Cor]
  public void SetCor(string colorFg, string colorBg)
  {
    if (OperatingSystem.IsWindows())
    {
      if (colorFg != "default")
      {
        //[0x0d:1]
        if (colorBg == "default") { colorBg = "Black"; }
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
        //[0x0d:2]
        if (colorBg == "default")
        {
          int CorFgANSI = GetCodigoANSI(colorFg);
          Console.Write($"\x1b[{CorFgANSI}m");
          return;
        } else {
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

  //[0x0e_Ansi]
  public int GetCodigoANSI(string cor)
  {
    switch (cor) {
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
    var Controller = new PilhaControlador();

    int opcao;
    bool run = true;
    while (run)
    {
      opcao = Controller.Menu();   // >> [0x07_Menu]
      switch(opcao)
      {
        case 1:
          Controller.Inserir();    // >> [0x08_Inserir]
          break;
        case 2:
          Controller.Remover();    // >> [0x09_Remover]
          break;
        case 3:
          Controller.VerTopo();    // >> [0x0b_Topo]
          break;
        case 4:
          Controller.Pesquisar();  // >> [0x0c_Pesquisar]
          break;
        case 0:
          run = !run;
          break;
      }
    }
  }
}
