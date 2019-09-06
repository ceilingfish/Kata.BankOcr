namespace Kata.BankOcr.Core
{
    /// <summary>
    /// A validation result to represent a valid account number
    /// </summary>
    public class ValidAccountNumberValidationResult : IAccountNumberValidationResult
    {
        public static ValidAccountNumberValidationResult Default { get; } = new ValidAccountNumberValidationResult();

        public bool IsValid => true;

        public string Description => string.Empty;
    }
}
