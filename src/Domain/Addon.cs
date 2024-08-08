namespace VendingMachineOOO.Domain;

public abstract record Addon(decimal Cost);
public record Sugar() : Addon(0.25m);
public record Cream() : Addon(0.50m);
