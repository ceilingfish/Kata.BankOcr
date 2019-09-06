# README

This command line utility can be run in one of two modes, either to emit results to the screen, or to a file. Both read results in from an input file. To display to the console, use the following command:

    dotnet run --project src\Kata.BankOcr.Console\Kata.BankOcr.Console.csproj --input src\Kata.BankOcr.Console\Data\usecase1.extended.txt

To output to a file, execute the following:

    dotnet run --project src\Kata.BankOcr.Console\Kata.BankOcr.Console.csproj --input src\Kata.BankOcr.Console\Data\usecase1.extended.txt --output out.txt
