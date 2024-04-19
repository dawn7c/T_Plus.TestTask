namespace T_Plus.RepairCostProgram.Validation
{
    public class RepairCostValidation
    {
        public ValidationResult CheckArgs(string[] args)
        {
            if (args.Length <= 2)
            {
                return new ValidationResult(false, "Usage ThermalId, log");
            }
            return new ValidationResult(true, "Good");
        }

        public ValidationResult ValidateParse(string[] args) 
        {
            Guid thermalNodeId;
            if (!Guid.TryParse(args[0], out thermalNodeId))
            {
                return new ValidationResult(false, "Invalid thermalNodeId. Please provide a valid GUID.");
            }
            return new ValidationResult(true, "ThermalNodeId is valid.");
        }
    }
}
