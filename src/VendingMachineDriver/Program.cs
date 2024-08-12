// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using VendingMachineApp.Application;
using VendingMachineApp.Domain;
using VendingMachineApp.Infrastructure;

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




