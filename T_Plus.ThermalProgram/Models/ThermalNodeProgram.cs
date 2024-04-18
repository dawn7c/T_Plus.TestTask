namespace T_Plus.ThermalProgram.Models
{
    public class ThermalNodeProgram
    {
        public Guid ThermalNodeId { get; set; }
        public string ThermalNodeName { get; set; }
        public double RepairCost { get; set; }
        public DateTime DateModified { get; set; }
    }
}
