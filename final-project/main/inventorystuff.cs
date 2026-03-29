namespace main;

public class Supply
{
    public string Name { get; set; }
    public int Amount {get; set; }
    public string Type {get; set;}

    public Supply(string name, int amount, string type)
    {
        this.Name = name;
        this.Amount = amount;
        this.Type = type;
    }

    public override string ToString()
    {
        return "Type: " + this.Type + " Name: " + this.Name + " Amount: " + this.Amount;
    }
}

public class SupplyLog
{
    public List<Supply> Supplies { get; }

    public SupplyLog()
    {
    this.Supplies = new List<Supply>();     
    }

    public void AddSupply(Supply newSupply)
    {
        this.Supplies.Add(newSupply);
        SynchronizeSupplies();
    }

    public void RemoveSupply(Supply supplyToRemove)
    {
        this.Supplies.Remove(supplyToRemove);
        SynchronizeSupplies();
    }

    public void SynchronizeSupplies()
    {
        if(File.Exists("Supply_List.txt")){
            File.Delete("Supply_List.txt");
        }

        foreach (Supply supply in this.Supplies)
        {
            File.AppendAllText("Supply_List.txt", supply.Name + ',' + supply.Amount + ',' + supply.Type +Environment.NewLine);
        }
    }

    
}