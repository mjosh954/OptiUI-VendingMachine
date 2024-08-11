// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using VendingMachineOOO.Application;
using VendingMachineOOO.Domain;
using VendingMachineOOO.Infrastructure;

IServiceCollection services = new ServiceCollection();
services.AddSingleton<IClock>(new Clock())
    .AddTransient<ILedger, Ledger>()
    .AddSingleton(new Bank(Bank.CreateDefaultTill()))
    .AddTransient<IVendingMachine, VendingMachine>()
    .AddSingleton<ICoffeeFactory>(new CoffeeFactory())
    .AddSingleton<IPriceCalculator, PriceCalculator>()
    .AddSingleton(new Prices());

var provider = services.BuildServiceProvider();



var vendingMachine = provider.GetRequiredService<IVendingMachine>();

vendingMachine.SelectCoffee(CoffeeSize.Large);




